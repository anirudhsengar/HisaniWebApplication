using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using static HisaniWebApplication.Models.CommonFunctions;

namespace HisaniWebApplication.Trainer
{
    public partial class VetDetails : Page
    {
        Commonfn fn = new Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load the vet details if it's not a postback
                LoadVetDetails();
            }
        }

        private void LoadVetDetails()
        {
            try
            {
                // Get trainer's email from session
                string trainerEmail = Session["TrainerEmail"] as string;
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                // Fetch StableID based on TrainerEmail
                string stableQuery = "SELECT StableID FROM Stable WHERE TrainerEmail = @TrainerEmail";
                SqlCommand stableCommand = new SqlCommand(stableQuery);
                stableCommand.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

                DataTable stableTable = fn.Fetch(stableCommand);

                if (stableTable.Rows.Count == 0)
                {
                    Response.Write("Error: No stable found for this trainer.");
                    return;
                }

                int stableID = Convert.ToInt32(stableTable.Rows[0]["StableID"]);

                // Fetch Vet details associated with the stable
                string vetQuery = "SELECT VetName, Speciality, Contact FROM Vet WHERE StableID = @StableID";
                SqlCommand vetCommand = new SqlCommand(vetQuery);
                vetCommand.Parameters.AddWithValue("@StableID", stableID);

                DataTable vetTable = fn.Fetch(vetCommand);

                if (vetTable.Rows.Count > 0)
                {
                    // Display the vet details in the corresponding labels
                    lblVetName.Text = vetTable.Rows[0]["VetName"].ToString();
                    lblVetSpeciality.Text = vetTable.Rows[0]["Speciality"].ToString();
                    lblVetContact.Text = vetTable.Rows[0]["Contact"].ToString();
                }
                else
                {
                    // Display placeholder text when no vet is assigned
                    lblVetName.Text = "No vet assigned.";
                    lblVetSpeciality.Text = "N/A";
                    lblVetContact.Text = "N/A";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading vet details: " + ex.Message;
            }
        }

        protected void btnEditVet_Click(object sender, EventArgs e)
        {
            try
            {
                // Get trainer's email from session
                string trainerEmail = Session["TrainerEmail"] as string;
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                // Fetch StableID based on TrainerEmail
                string stableQuery = "SELECT StableID FROM Stable WHERE TrainerEmail = @TrainerEmail";
                SqlCommand stableCommand = new SqlCommand(stableQuery);
                stableCommand.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

                DataTable stableTable = fn.Fetch(stableCommand);

                if (stableTable.Rows.Count == 0)
                {
                    Response.Write("Error: No stable found for this trainer.");
                    return;
                }

                int stableID = Convert.ToInt32(stableTable.Rows[0]["StableID"]);

                // Fetch Vet details associated with the stable
                string vetQuery = "SELECT Email FROM Vet WHERE StableID = @StableID";
                SqlCommand vetCommand = new SqlCommand(vetQuery);
                vetCommand.Parameters.AddWithValue("@StableID", stableID);

                DataTable vetTable = fn.Fetch(vetCommand);

                if (vetTable.Rows.Count > 0)
                {
                    // Redirect to Edit Vet page with the VetID as a query string parameter
                    string email = vetTable.Rows[0]["Email"].ToString();  // Ensure you're passing the email as a string
                    Response.Redirect($"VetEdit.aspx?email={email}", false);  // Pass it as a string
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    // Display message if no vet is found
                    lblMessage.Text = "No vet assigned to this stable.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error during edit: " + ex.Message;
            }
        }

        protected void btnDeleteVet_Click(object sender, EventArgs e)
        {
            try
            {
                // Get trainer's email from session
                string trainerEmail = Session["TrainerEmail"] as string;
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                // Fetch StableID based on TrainerEmail
                string stableQuery = "SELECT StableID FROM Stable WHERE TrainerEmail = @TrainerEmail";
                SqlCommand stableCommand = new SqlCommand(stableQuery);
                stableCommand.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

                DataTable stableTable = fn.Fetch(stableCommand);

                if (stableTable.Rows.Count == 0)
                {
                    Response.Write("Error: No stable found for this trainer.");
                    return;
                }

                int stableID = Convert.ToInt32(stableTable.Rows[0]["StableID"]);

                // Remove the association of the vet with the stable (set StableID to NULL in Vet table)
                string updateVetQuery = "UPDATE Vet SET StableID = NULL WHERE StableID = @StableID";
                SqlCommand updateVetCommand = new SqlCommand(updateVetQuery);
                updateVetCommand.Parameters.AddWithValue("@StableID", stableID);
                fn.ExecuteQuery(updateVetCommand);

                // Also clear the VetEmail in the Stable table
                string updateStableQuery = "UPDATE Stable SET VetEmail = NULL WHERE StableID = @StableID";
                SqlCommand updateStableCommand = new SqlCommand(updateStableQuery);
                updateStableCommand.Parameters.AddWithValue("@StableID", stableID);
                fn.ExecuteQuery(updateStableCommand);

                // Redirect to the VetAdd page after deletion
                Response.Redirect("VetAdd.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating vet: " + ex.Message;
            }
        }
    }
}
