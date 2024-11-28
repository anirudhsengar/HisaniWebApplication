using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static HisaniWebApplication.Models.CommonFunctions;

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

        // The button click handler
        protected void btnAddVet_Click(object sender, EventArgs e)
        {
            // Call the method to add or update vet
            AddOrUpdateVet();
        }

        private void AddOrUpdateVet()
        {
            try
            {
                // Retrieve form values correctly from the TextBox controls
                string email = vetEmail.Text;           // Get text from TextBox
                string name = vetName.Text;             // Get text from TextBox
                string speciality = vetSpeciality.Text; // Get text from TextBox
                string contact = vetContact.Text;       // Get text from TextBox

                // Validate form inputs
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(speciality) || string.IsNullOrEmpty(contact))
                {
                    Response.Write("All fields are required.");
                    return;
                }

                // Ensure the contact is a valid number
                if (!long.TryParse(contact, out long contactNumber))
                {
                    Response.Write("Invalid contact number.");
                    return;
                }

                // Get trainer's email and current stable ID from session
                string trainerEmail = Session["TrainerEmail"] as string; // Replace with actual session value
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                // Fetch stable ID for the trainer
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

                // Check if a vet with the given email already exists
                string checkVetQuery = "SELECT COUNT(*) FROM Vet WHERE Email = @VetEmail";
                SqlCommand checkVetCommand = new SqlCommand(checkVetQuery);
                checkVetCommand.Parameters.AddWithValue("@VetEmail", email); // Use email variable
                DataTable vetExistsTable = fn.Fetch(checkVetCommand); // Check if vet exists
                bool vetExists = Convert.ToInt32(vetExistsTable.Rows[0][0]) > 0;

                if (vetExists)
                {
                    // If vet exists, update StableID for this vet
                    string updateVetQuery = "UPDATE Vet SET StableID = @StableID WHERE Email = @Email";
                    SqlCommand updateCommand = new SqlCommand(updateVetQuery);
                    updateCommand.Parameters.AddWithValue("@StableID", stableID);
                    updateCommand.Parameters.AddWithValue("@Email", email); // Use email variable
                    fn.ExecuteQuery(updateCommand);

                    // Update the Stable table with the vet's email
                    string updateStableQuery = "UPDATE STABLE SET VetEmail = @VetEmail WHERE StableID = @StableID";
                    SqlCommand updateStableCommand = new SqlCommand(updateStableQuery);
                    updateStableCommand.Parameters.AddWithValue("@VetEmail", email); // Use email variable
                    updateStableCommand.Parameters.AddWithValue("@StableID", stableID);
                    fn.ExecuteQuery(updateStableCommand); // Execute update query

                    Response.Redirect("VetDetails.aspx");
                }
                else
                {
                    // Insert new vet if not exists
                    string insertVetQuery = "INSERT INTO Vet (Email, VetName, Speciality, Contact, StableID) " +
                                            "VALUES (@Email, @VetName, @Speciality, @Contact, @StableID)";
                    SqlCommand insertCommand = new SqlCommand(insertVetQuery);

                    // Add parameters to the insert command
                    insertCommand.Parameters.AddWithValue("@Email", email); // Use email variable
                    insertCommand.Parameters.AddWithValue("@VetName", name); // Use name variable
                    insertCommand.Parameters.AddWithValue("@Speciality", speciality); // Use speciality variable
                    insertCommand.Parameters.AddWithValue("@Contact", contact); // Use contact variable
                    insertCommand.Parameters.AddWithValue("@StableID", stableID);

                    // Update the Stable table with the vet's email
                    string updateStableQuery = "UPDATE STABLE SET VetEmail = @VetEmail WHERE StableID = @StableID";
                    SqlCommand updateStableCommand = new SqlCommand(updateStableQuery);
                    updateStableCommand.Parameters.AddWithValue("@VetEmail", email); // Use email variable
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
