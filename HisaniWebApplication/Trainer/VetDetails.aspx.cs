
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                // Check if the delete request is made
                if (Request.QueryString["delete"] == "true")
                {
                    DeleteVet();
                }
                else
                {
                    LoadVetDetails();
                }
            }
        }

        private void LoadVetDetails()
        {
            try
            {
                // Get trainer's email and current stable ID (replace with session value)
                string trainerEmail = Session["TrainerEmail"] as string; // Replace with actual session value
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                string stableQuery = "SELECT StableID FROM Stable WHERE TrainerEmail = @TrainerEmail";
                SqlCommand stableCommand = new SqlCommand(stableQuery);
                stableCommand.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

                DataTable stableTable = fn.Fetch(stableCommand); // Fetch stable data

                if (stableTable.Rows.Count == 0)
                {
                    Response.Write("Error: No stable found for this trainer.");
                    return;
                }

                int stableID = Convert.ToInt32(stableTable.Rows[0]["StableID"]);

                // Fetch vet details based on StableID (assuming only one vet per stable)
                string vetQuery = "SELECT VetName, Speciality, Contact FROM Vet WHERE StableID = @StableID";
                SqlCommand vetCommand = new SqlCommand(vetQuery);
                vetCommand.Parameters.AddWithValue("@StableID", stableID);

                DataTable vetTable = fn.Fetch(vetCommand); // Fetch vet data

                if (vetTable.Rows.Count > 0)
                {
                    // Display vet details
                    VetName.Text = vetTable.Rows[0]["VetName"].ToString();
                    VetSpeciality.Text = vetTable.Rows[0]["Speciality"].ToString();
                    VetContact.Text = vetTable.Rows[0]["Contact"].ToString();
                }
                else
                {
                    // If no vet is found for this stable
                    VetName.Text = "No vet assigned.";
                    VetSpeciality.Text = "N/A";
                    VetContact.Text = "N/A";
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error loading vet details: " + ex.Message);
            }
        }

        private void DeleteVet()
        {
            try
            {
                // Get trainer's email and stable ID (replace with actual session value)
                string trainerEmail = Session["TrainerEmail"] as string; // Replace with actual session value
                string stableQuery = "SELECT StableID FROM Stable WHERE TrainerEmail = @TrainerEmail";
                SqlCommand stableCommand = new SqlCommand(stableQuery);
                stableCommand.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

                DataTable stableTable = fn.Fetch(stableCommand); // Fetch stable data

                if (stableTable.Rows.Count == 0)
                {
                    Response.Write("Error: No stable found for this trainer.");
                    return;
                }

                int stableID = Convert.ToInt32(stableTable.Rows[0]["StableID"]);

                // Update vet to set StableID to NULL
                string updateVetQuery = "UPDATE Vet SET StableID = NULL WHERE StableID = @StableID";
                SqlCommand updateCommand = new SqlCommand(updateVetQuery);
                updateCommand.Parameters.AddWithValue("@StableID", stableID);

                fn.ExecuteQuery(updateCommand); // Execute update query

                string updateStableQuery = "UPDATE STABLE SET VetEmail = NULL WHERE StableID = @StableID";
                SqlCommand updateStableCommand = new SqlCommand(updateStableQuery);
                updateStableCommand.Parameters.AddWithValue("@StableID", stableID);
                fn.ExecuteQuery(updateStableCommand);
                Response.Redirect("VetAdd.aspx", false); // Redirect back after update
                Context.ApplicationInstance.CompleteRequest(); // Ensure redirection happens immediately
            }
            catch (Exception ex)
            {
                Response.Write("Error updating vet: " + ex.Message);
            }
        }

    }
}