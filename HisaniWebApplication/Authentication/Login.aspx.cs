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
    public partial class Login : System.Web.UI.Page
    {
        CommonFunctions.Commonfn commonfn = new CommonFunctions.Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Text = string.Empty;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Input Validation
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter both email and password.";
                return;
            }

            try
            {
                // Query to check if user exists and verify credentials
                SqlCommand loginCmd = new SqlCommand("SELECT TypeOfUser FROM [Users] WHERE Email = @Email AND Password = @Password");
                loginCmd.Parameters.AddWithValue("@Email", email);
                loginCmd.Parameters.AddWithValue("@Password", password); // Consider hashing passwords for comparison.

                DataTable dt = commonfn.Fetch(loginCmd);

                if (dt.Rows.Count > 0)
                {
                    // User exists
                    string userType = dt.Rows[0]["TypeOfUser"].ToString();

                    // Set session variable based on the user's role
                    if (userType.Equals("Trainer", StringComparison.OrdinalIgnoreCase))
                    {
                        Session["TrainerEmail"] = email; // Save trainer email in session
                    }
                    else if (userType.Equals("Vet", StringComparison.OrdinalIgnoreCase))
                    {
                        Session["VetEmail"] = email; // Save vet email in session
                    }

                    // Redirect based on the user type (optional)
                    if (userType == "Trainer")
                    {
                        Response.Redirect("~/Trainer/TrainerHome.aspx");
                    }
                    else if (userType == "Vet")
                    {
                        Response.Redirect("~/Vet/VetDashboard.aspx");
                    }
                }
                else
                {
                    // Invalid credentials
                    lblMessage.Text = "Invalid email or password.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred during login. Please try again.";
                // Optionally log the exception for debugging purposes.
            }
        }
    }
}
