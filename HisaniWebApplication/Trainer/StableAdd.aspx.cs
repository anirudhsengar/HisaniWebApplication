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
                int capacity;

                // Server-side validation
                if (string.IsNullOrEmpty(stableName) || stableName.Length < 3)
                {
                    Response.Write("<script>alert('Stable name must be at least 3 characters long.');</script>");
                    return;
                }

                if (string.IsNullOrEmpty(location))
                {
                    Response.Write("<script>alert('Location is required.');</script>");
                    return;
                }

                if (!int.TryParse(Capacity.Text.Trim(), out capacity) || capacity <= 0)
                {
                    Response.Write("<script>alert('Capacity must be a positive number.');</script>");
                    return;
                }

                string trainerEmail = Session["TrainerEmail"] as string;
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                // Database insertion
                string query = "INSERT INTO Stable (StableName, Location, Capacity, TrainerEmail) " +
                               "VALUES (@StableName, @Location, @Capacity, @TrainerEmail)";

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HisaniDB"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@StableName", stableName);
                        cmd.Parameters.AddWithValue("@Location", location);
                        cmd.Parameters.AddWithValue("@Capacity", capacity);
                        cmd.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Success message and redirection
                Response.Redirect("StableDetails.aspx");
            }
            catch (Exception ex)
            {
                // Log or display error
            }
        }

    }
}