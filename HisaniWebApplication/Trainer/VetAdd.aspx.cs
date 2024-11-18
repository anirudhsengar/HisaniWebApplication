using static HisaniWebApplication.Models.CommonFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace HisaniWebApplication.Trainer
{
    public partial class VetAdd : System.Web.UI.Page
    {
        Commonfn fn = new Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && Request.HttpMethod == "POST")
            {
                AddOrUpdateVet();
            }
        }

        private void AddOrUpdateVet()
        {
            try
            {
                // Retrieve form values
                string vetEmail = Request.Form["vetEmail"];
                string vetName = Request.Form["vetName"];
                string vetSpecialty = Request.Form["vetSpecialty"];
                string vetContact = Request.Form["vetContact"];

                // Get trainer's email and current stable ID
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

                // Check if a vet with the given email exists
                string checkVetQuery = "SELECT COUNT(*) FROM Vet WHERE Email = @VetEmail";
                SqlCommand checkVetCommand = new SqlCommand(checkVetQuery);
                checkVetCommand.Parameters.AddWithValue("@VetEmail", vetEmail);

                DataTable vetExistsTable = fn.Fetch(checkVetCommand); // Check if vet exists
                bool vetExists = Convert.ToInt32(vetExistsTable.Rows[0][0]) > 0;

                if (vetExists)
                {
                    // If vet exists, update StableID
                    string updateVetQuery = "UPDATE Vet SET StableID = @StableID WHERE Email = @Email";
                    SqlCommand updateCommand = new SqlCommand(updateVetQuery);
                    updateCommand.Parameters.AddWithValue("@StableID", stableID);
                    updateCommand.Parameters.AddWithValue("@Email", vetEmail);
                    fn.ExecuteQuery(updateCommand);
                    string updateStableQuery = "UPDATE STABLE SET VetEmail = @VetEmail WHERE StableID = @StableID";
                    SqlCommand updateStableCommand = new SqlCommand(updateStableQuery);
                    updateStableCommand.Parameters.AddWithValue("@VetEmail", vetEmail);
                    updateStableCommand.Parameters.AddWithValue("@StableID", stableID);
                    fn.ExecuteQuery(updateStableCommand);// Execute update query
                    Response.Redirect("VetDetails.aspx");
                }
                else
                {
                    // Insert new vet if not exists
                    string insertVetQuery = "INSERT INTO Vet (Email, VetName, Specialty, Contact, StableID) " +
                                            "VALUES (@Email, @VetName, @Specialty, @Contact, @StableID)";
                    SqlCommand insertCommand = new SqlCommand(insertVetQuery);

                    // Add parameters to the insert command
                    insertCommand.Parameters.AddWithValue("@Email", vetEmail);
                    insertCommand.Parameters.AddWithValue("@VetName", vetName);  // Use VetName instead of Name
                    insertCommand.Parameters.AddWithValue("@Specialty", vetSpecialty);
                    insertCommand.Parameters.AddWithValue("@Contact", vetContact);
                    insertCommand.Parameters.AddWithValue("@StableID", stableID);

                    string updateStableQuery = "UPDATE STABLE SET VetEmail = @VetEmail WHERE StableID = @StableID";
                    SqlCommand updateStableCommand = new SqlCommand(updateStableQuery);
                    updateStableCommand.Parameters.AddWithValue("@VetEmail", vetEmail);
                    updateStableCommand.Parameters.AddWithValue("@StableID", stableID);
                    fn.ExecuteQuery(updateStableCommand);

                    fn.ExecuteQuery(insertCommand); // Execute insert query
                    Response.Redirect("VetDetails.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error adding or updating vet: " + ex.Message);
            }
        }
    }
}

