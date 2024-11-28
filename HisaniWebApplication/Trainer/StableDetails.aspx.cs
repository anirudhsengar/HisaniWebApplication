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
                string trainerEmail = Session["TrainerEmail"] as string;

                // Step 1: Retrieve Stable IDs for the trainer
                string query = "SELECT StableID FROM Stable WHERE TrainerEmail = @TrainerEmail";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@TrainerEmail", trainerEmail);
                DataTable stableDetails = fn.Fetch(cmd);

                if (stableDetails.Rows.Count > 0)
                {
                    foreach (DataRow row in stableDetails.Rows)
                    {
                        string stableID = row["StableID"].ToString();

                        // Step 2: Delete records associated with the stable
                        string deleteRecordsQuery = "DELETE FROM Records WHERE StableID = @StableID";
                        SqlCommand cmd1 = new SqlCommand(deleteRecordsQuery);
                        cmd1.Parameters.AddWithValue("@StableID", stableID);
                        fn.ExecuteQuery(cmd1);

                        // Step 3: Delete horses associated with the stable
                        string deleteHorsesQuery = "DELETE FROM Horse WHERE StableID = @StableID";
                        SqlCommand cmd2 = new SqlCommand(deleteHorsesQuery);
                        cmd2.Parameters.AddWithValue("@StableID", stableID);
                        fn.ExecuteQuery(cmd2);
                    }

                    // Step 4: Delete the stable itself
                    string deleteStableQuery = "DELETE FROM Stable WHERE TrainerEmail = @TrainerEmail";
                    SqlCommand cmd3 = new SqlCommand(deleteStableQuery);
                    cmd3.Parameters.AddWithValue("@TrainerEmail", trainerEmail);
                    fn.ExecuteQuery(cmd3);

                    // Redirect to StableAdd.aspx after successful deletion
                    Response.Redirect("StableAdd.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblMessage.Text = "No stables found for the trainer.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
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
                lblMessage.Text = ex.ToString();
            }
        }

        
    }
}