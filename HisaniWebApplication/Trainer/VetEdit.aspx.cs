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
    public partial class VetEdit : Page
    {
        Commonfn fn = new Commonfn();

        // Properties to hold vet details
        public string StableID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    // Step 1: Get trainer's email from session (assuming the email is stored in session)
                    string trainerEmail = Session["TrainerEmail"] as string;  // Use Session["TrainerEmail"] for actual session-based email

                    if (string.IsNullOrEmpty(trainerEmail))
                    {
                        Response.Redirect("~/Authentication/Login.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }

                    // Step 2: Query the database to find the stable where this trainer is assigned
                    string stableQuery = "SELECT StableID FROM Stable WHERE TrainerEmail = @TrainerEmail";
                    SqlCommand stableCommand = new SqlCommand(stableQuery);
                    stableCommand.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

                    DataTable stableTable = fn.Fetch(stableCommand); // Fetch stable data

                    if (stableTable.Rows.Count == 0)
                    {
                        Response.Write("Error: No stable found for this trainer.");
                        return;
                    }

                    // Get StableID of the trainer
                    StableID = stableTable.Rows[0]["StableID"].ToString();

                    // Step 3: Query the database to find the vet assigned to this stable
                    string vetQuery = "SELECT * FROM Vet WHERE StableID = @StableID";
                    SqlCommand vetCommand = new SqlCommand(vetQuery);
                    vetCommand.Parameters.AddWithValue("@StableID", StableID);

                    DataTable vetTable = fn.Fetch(vetCommand); // Fetch vet data

                    if (vetTable.Rows.Count > 0)
                    {
                        // Step 4: Display vet data in the form
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
        }

        protected void SaveChanges(object sender, EventArgs e)
        {
            try
            {
                // Step 1: Get the trainer's email from the session (or hardcoded for testing)
                string trainerEmail = Session["TrainerEmail"] as string; // Replace with session value for actual use
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                // Step 2: Query the database to find the StableID for the trainer
                string stableQuery = "SELECT StableID FROM Stable WHERE TrainerEmail = @TrainerEmail";
                SqlCommand stableCommand = new SqlCommand(stableQuery);
                stableCommand.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

                DataTable stableTable = fn.Fetch(stableCommand); // Fetch stable data

                if (stableTable.Rows.Count == 0)
                {
                    Response.Write("Error: No stable found for this trainer.");
                    return;
                }

                string stableID = stableTable.Rows[0]["StableID"].ToString(); // Retrieve StableID for this trainer

                // Step 3: Retrieve form values from the controls on the page
                string vetName = VetName.Text.Trim();
                string vetSpeciality = VetSpeciality.Text.Trim();
                string vetContact = VetContact.Text.Trim();

                if (string.IsNullOrEmpty(vetName) || string.IsNullOrEmpty(vetSpeciality) || string.IsNullOrEmpty(vetContact))
                {
                    Response.Write("Error: Please fill all the fields.");
                    return;
                }

                // Step 4: Update the vet record in the database using the StableID
                string updateQuery = "UPDATE Vet SET VetName = @VetName, Speciality = @Speciality, Contact = @Contact WHERE StableID = @StableID";
                SqlCommand updateCommand = new SqlCommand(updateQuery);

                // Add parameters to the command
                updateCommand.Parameters.AddWithValue("@VetName", vetName);
                updateCommand.Parameters.AddWithValue("@Speciality", vetSpeciality);
                updateCommand.Parameters.AddWithValue("@Contact", vetContact);
                updateCommand.Parameters.AddWithValue("@StableID", stableID); // Add StableID to the query

                fn.ExecuteQuery(updateCommand); // Execute the query

                // Step 5: Redirect to the VetDetails page after successful update
                Response.Redirect("VetDetails.aspx?stableID=" + stableID, false);
                Context.ApplicationInstance.CompleteRequest(); // Ensure the redirect happens immediately
            }
            catch (Exception ex)
            {
                Response.Write("Error updating vet: " + ex.Message);
            }
        }


    }
}
