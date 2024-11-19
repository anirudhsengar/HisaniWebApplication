using HisaniWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static HisaniWebApplication.Models.CommonFunctions;

namespace HisaniWebApplication.Trainer
{
    public partial class StableAdd : Page
    {
        CommonFunctions.Commonfn fn = new CommonFunctions.Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Optional: any Page_Load logic
        }

        protected void btnAddStable_Click(object sender, EventArgs e)
        {
            try
            {
                string stableName = StableName.Text.Trim();
                string location = Location.Text.Trim();
                int capacity = Math.Max(0, int.Parse(Capacity.Text.Trim())); // Ensure capacity is 0 or higher
                string trainerEmail = Session["TrainerEmail"] as string; // You can replace this with the actual trainer's email from session or another source
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                string query = "INSERT INTO Stable (StableName, Location, Capacity, TrainerEmail) " +
                               "VALUES (@StableName, @Location, @Capacity, @TrainerEmail)";

                // Create and open the connection
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HisaniDB"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters
                        cmd.Parameters.AddWithValue("@StableName", stableName);
                        cmd.Parameters.AddWithValue("@Location", location);
                        cmd.Parameters.AddWithValue("@Capacity", capacity);
                        cmd.Parameters.AddWithValue("@TrainerEmail", trainerEmail); // Ensure TrainerEmail is included

                        // Open connection and execute the query
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Redirect or show a success message
                Response.Redirect("StableDetails.aspx"); // Or another page you want to redirect to
            }
            catch (Exception ex)
            {
                // Handle the error (log it or show a message)
                Response.Write("Error adding stable: " + ex.Message);
            }
        }
    }
}