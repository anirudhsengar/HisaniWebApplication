using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Data.SqlClient;
using HisaniWebApplication.Models;

namespace HisaniWebApplication.Authentication
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        CommonFunctions.Commonfn commonfn = new CommonFunctions.Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Text = string.Empty;
            }
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string userType = ddlUserType.SelectedValue;

            // Input Validation
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(userType))
            {
                lblMessage.Text = "All fields are required.";
                return;
            }

            // Email Format Validation
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                lblMessage.Text = "Please enter a valid email address.";
                return;
            }

            // Password Validation
            if (password.Length < 8)
            {
                lblMessage.Text = "Password must be at least 8 characters long.";
                return;
            }

            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit))
            {
                lblMessage.Text = "Password must contain at least one uppercase letter, one lowercase letter, and one number.";
                return;
            }

            if (password != confirmPassword)
            {
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            try
            {
                // Check if the user already exists
                SqlCommand checkUserCmd = new SqlCommand("SELECT COUNT(*) FROM [Users] WHERE Email = @Email");
                checkUserCmd.Parameters.AddWithValue("@Email", email);
                int userExists = commonfn.FetchScalar(checkUserCmd);

                if (userExists > 0)
                {
                    lblMessage.Text = "An account with this email already exists.";
                    return;
                }


                // Insert new user
                SqlCommand insertCmd = new SqlCommand("INSERT INTO [Users] (Email, Password, TypeOfUser) VALUES (@Email, @Password, @TypeOfUser)");
                insertCmd.Parameters.AddWithValue("@Email", email);
                insertCmd.Parameters.AddWithValue("@Password", password); // Store hashed password
                insertCmd.Parameters.AddWithValue("@TypeOfUser", userType);

                commonfn.ExecuteQuery(insertCmd);

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Account created successfully. Redirecting to login...";
                Response.AddHeader("REFRESH", "3;URL=Login.aspx"); // Redirect to login after 3 seconds
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred while creating the account. Please try again.";
                // Optionally log the exception for debugging purposes.
                // Example: Logger.LogError(ex);
            }
        }

        /// <summary>
        /// Hashes the input password using SHA256.
        /// </summary>
        /// <param name="password">The plain text password.</param>
        /// <returns>The hashed password as a hexadecimal string.</returns>
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
