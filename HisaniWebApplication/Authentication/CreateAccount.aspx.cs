using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
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
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                lblMessage.Text = "All fields are required.";
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
                insertCmd.Parameters.AddWithValue("@Password", password); // Consider hashing passwords for security.
                insertCmd.Parameters.AddWithValue("@TypeOfUser", userType);

                commonfn.ExecuteQuery(insertCmd);

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Account created successfully. Redirecting to login...";
                Response.AddHeader("REFRESH", "3;URL=Login.aspx"); // Redirect to login after 3 seconds
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred while creating the account. Please try again." + ex;
                // Optionally log the exception for debugging purposes.
            }
        }
    }
}
