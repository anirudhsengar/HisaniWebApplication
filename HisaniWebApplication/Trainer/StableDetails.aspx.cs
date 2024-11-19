using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static HisaniWebApplication.Models.CommonFunctions;

namespace HisaniWebApplication.Trainer
{
    public partial class StableDetails : Page
    {
        Commonfn fn = new Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStableDetails();

                // Check if the delete request is confirmed
                if (Request.QueryString["delete"] == "true")
                {
                    DeleteStable();
                }
            }
        }

        private void LoadStableDetails()
        {
            try
            {
                // Assuming trainer's email is stored in session and retrieving StableID based on TrainerEmail
                string trainerEmail = Session["TrainerEmail"] as string; // Replace with Session["TrainerEmail"] for dynamic access
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                // Fetch stable details for the trainer
                string query = "SELECT StableName, Location, Capacity FROM Stable WHERE TrainerEmail = @TrainerEmail";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@TrainerEmail", trainerEmail);
                DataTable stableDetails = fn.Fetch(cmd);

                if (stableDetails.Rows.Count > 0)
                {
                    // Populate the details on the page
                    lblStableName.Text = stableDetails.Rows[0]["StableName"].ToString();
                    lblLocation.Text = stableDetails.Rows[0]["Location"].ToString();
                    lblCapacity.Text = stableDetails.Rows[0]["Capacity"].ToString();
                }
                else
                {
                    // Handle case if no stable details are found
                    lblMessage.Text = "No stable details found for the trainer.";
                }
            }
            catch (Exception ex)
            {
                // Log or display error as necessary
                lblMessage.Text = "Error loading stable details: " + ex.Message;
            }
        }

        // Method to handle the deletion of the stable
        protected void btnDeleteStable_Click(object sender, EventArgs e)
        {
            DeleteStable();
        }

        private void DeleteStable()
        {
            try
            {
                string trainerEmail = "testtrainer@example.com"; // Replace with Session["TrainerEmail"]

                // Delete stable query
                string deleteQuery = "DELETE FROM Stable WHERE TrainerEmail = @TrainerEmail";
                SqlCommand cmd = new SqlCommand(deleteQuery);
                cmd.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

                fn.ExecuteQuery(cmd); // Execute the delete query

                // Redirect to StableAdd.aspx after successful deletion
                Response.Redirect("StableAdd.aspx", false);
                Context.ApplicationInstance.CompleteRequest(); // Ends the current request
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error deleting stable: " + ex.Message;
            }
        }
        protected void btnEditStable_Click(object sender, EventArgs e)
        {
            try
            {
                // Redirect to StableEdit.aspx after successful deletion
                Response.Redirect("StableEdit.aspx", false);
                Context.ApplicationInstance.CompleteRequest(); // Ends the current request
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating stable: " + ex.Message;
            }
        }

        
    }
}