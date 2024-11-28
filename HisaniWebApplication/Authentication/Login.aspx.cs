using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HisaniWebApplication.Models;
using System.Collections;

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

            // Email Format Validation
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                lblMessage.Text = "Please enter a valid email address.";
                return;
            }

            try
            {
                // Query to check if user exists and verify credentials
                SqlCommand loginCmd = new SqlCommand("SELECT TypeOfUser FROM [Users] WHERE Email = @Email AND Password = @Password");
                loginCmd.Parameters.AddWithValue("@Email", email);
                loginCmd.Parameters.AddWithValue("@Password", password); // Replace with hashed password check.

                DataTable dt = commonfn.Fetch(loginCmd);

                if (dt.Rows.Count > 0)
                {
                    string userType = dt.Rows[0]["TypeOfUser"].ToString();

                    if (userType.Equals("Trainer", StringComparison.OrdinalIgnoreCase))
                    {
                        // Trainer login logic
                        UpdateLoginStats();
                        Session["TrainerEmail"] = email;
                        Response.Redirect("~/Trainer/TrainerHome.aspx");
                    }
                    else if (userType.Equals("Vet", StringComparison.OrdinalIgnoreCase))
                    {
                        // Check if the vet has a stable assigned
                        SqlCommand vetStableCmd = new SqlCommand("SELECT StableID FROM Stable WHERE VetEmail = @VetEmail");
                        vetStableCmd.Parameters.AddWithValue("@VetEmail", email);

                        DataTable stableTable = commonfn.Fetch(vetStableCmd);

                        if (stableTable == null || stableTable.Rows.Count == 0)
                        {
                            lblMessage.Text = "You cannot log in as you do not have a stable assigned. Please contact the administrator.";
                            return;
                        }

                        // Vet login logic if a stable is assigned
                        UpdateLoginStats();
                        Session["VetEmail"] = email;
                        Response.Redirect("~/Vet/VetDashboard.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid email or password.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred during login. Please try again.";
                // Log exception for debugging (optional)
            }
        }

        private void UpdateLoginStats()
        {
            // Increment login statistics
            string query = "UPDATE WebsiteStats SET Logins = Logins + 1";
            SqlCommand cmd = new SqlCommand(query);
            commonfn.ExecuteQuery(cmd);
        }

    }
}
