using HisaniWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HisaniWebApplication.Trainer
{
    public partial class StableEdit : System.Web.UI.Page
    {
        private CommonFunctions.Commonfn fn = new CommonFunctions.Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStableDetails();
            }
        }

        // Method to load existing stable details
        private void LoadStableDetails()
        {
            try
            {
                string trainerEmail = Session["TrainerEmail"] as string; // Assuming this is set
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                // Query to fetch stable details by TrainerEmail
                string query = "SELECT StableName, Location, Capacity FROM Stable WHERE TrainerEmail = @TrainerEmail";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

                DataTable dt = fn.Fetch(cmd);

                // Check if stable data is available for this trainer
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    // Setting placeholders using Attributes collection
                    StableName.Text = row["StableName"].ToString();
                    Location.Text = row["Location"].ToString();
                    Capacity.Text = row["Capacity"].ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, perhaps log the error for debugging
            }
        }

        // Method to update stable details
        protected void btnEditStable_Click(object sender, EventArgs e)
        {
            lblMessage.Text = ""; // Clear previous messages

            try
            {
                string trainerEmail = Session["TrainerEmail"] as string;
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    lblMessage.Text = "Trainer email not found in session.";
                    return;
                }

                // Proceed with update if VetEmail exists
                string updateQuery = "UPDATE Stable SET StableName = @StableName, Location = @Location, Capacity = @Capacity WHERE TrainerEmail = @TrainerEmail";
                SqlCommand cmd = new SqlCommand(updateQuery);
                cmd.Parameters.AddWithValue("@StableName", StableName.Text);
                cmd.Parameters.AddWithValue("@Location", Location.Text);
                cmd.Parameters.AddWithValue("@Capacity", int.Parse(Capacity.Text));
                cmd.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

                fn.ExecuteQuery(cmd);

                Response.Redirect("StableDetails.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating stable: " + ex.Message;
            }
        }

    }
}
