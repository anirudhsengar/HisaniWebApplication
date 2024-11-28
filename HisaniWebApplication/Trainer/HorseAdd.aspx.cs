using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using HisaniWebApplication.Models;

namespace HisaniWebApplication.Trainer
{
    public partial class HorseAdd : System.Web.UI.Page
    {
        private CommonFunctions.Commonfn fn = new CommonFunctions.Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure the user is logged in or the page should not load.
            if (Session["TrainerEmail"] == null)
            {
                Response.Redirect("~/Authentication/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnAddHorse_Click(object sender, EventArgs e)
        {
            // Retrieve values from the form
            string horseName = HorseName.Text.Trim();
            string horseBreed = HorseBreed.Text.Trim();
            string horseSex = HorseSex.SelectedValue;
            DateTime dateOfBirth;

            // Validate date of birth (check if it is a valid date and not a future date)
            if (!DateTime.TryParse(DateOfBirth.Text, out dateOfBirth))
            {
                Response.Write("<script>alert('Invalid Date of Birth.');</script>");
                return;
            }

            if (dateOfBirth > DateTime.Now)
            {
                Response.Write("<script>alert('Date of Birth cannot be a future date.');</script>");
                return;
            }

            // Retrieve the logged-in trainer's email from the session
            string trainerEmail = Session["TrainerEmail"] as string;
            if (string.IsNullOrEmpty(trainerEmail))
            {
                Response.Redirect("~/Authentication/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            try
            {
                // Check if a stable exists for this trainer
                string query = "SELECT StableID FROM Stable WHERE TrainerEmail = @TrainerEmail";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@TrainerEmail", trainerEmail);
                object stableIDObj = fn.FetchScalar(cmd);

                if (stableIDObj != null)
                {
                    int stableID = Convert.ToInt32(stableIDObj);

                    // Insert the new horse into the Horse table
                    string insertQuery = "INSERT INTO Horse (StableID, HorseName, HorseBreed, Sex, DateOfBirth) " +
                                         "VALUES (@StableID, @HorseName, @HorseBreed, @Sex, @DateOfBirth)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery);
                    insertCmd.Parameters.AddWithValue("@StableID", stableID);
                    insertCmd.Parameters.AddWithValue("@HorseName", horseName);
                    insertCmd.Parameters.AddWithValue("@HorseBreed", horseBreed);
                    insertCmd.Parameters.AddWithValue("@Sex", horseSex);
                    insertCmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                    fn.ExecuteQuery(insertCmd); // Execute the query to insert the horse into the database

                    // Redirect to the HorseDetails page with the newly added horse's ID
                    int horseID = GetHorseID(horseName);
                    if (horseID > 0)
                    {
                        Response.Redirect("HorseDetails.aspx?HorseID=" + horseID);
                    }
                    else
                    {
                        Response.Write("<script>alert('Error: Could not retrieve Horse ID.');</script>");
                    }
                }
                else
                {
                    // Handle case where stable does not exist for the trainer
                    Response.Write("<script>alert('No stable found for this trainer.');</script>");
                }
            }
            catch (Exception ex)
            {
                // Log exception details for troubleshooting
                // LogError(ex); // Uncomment if you have logging set up

                Response.Write("<script>alert('An error occurred while adding the horse: " + ex.Message + "');</script>");
            }
        }

        private int GetHorseID(string horseName)
        {
            // Query to fetch the HorseID for the recently added horse
            string query = "SELECT HorseID FROM Horse WHERE HorseName = @HorseName";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@HorseName", horseName);
            object result = fn.FetchScalar(cmd);

            return result != null ? Convert.ToInt32(result) : 0;
        }

        // Custom validation method for Date of Birth
        protected void ValidateDateOfBirth(object source, ServerValidateEventArgs args)
        {
            DateTime dateOfBirth;

            // Try to parse the date from the textbox
            if (DateTime.TryParse(DateOfBirth.Text, out dateOfBirth))
            {
                // If the date is in the future, set the validation to false
                if (dateOfBirth > DateTime.Now)
                {
                    args.IsValid = false; // Invalid if the date is in the future
                }
                else
                {
                    args.IsValid = true; // Valid if the date is not in the future
                }
            }
            else
            {
                args.IsValid = false; // Invalid if the date format is incorrect
            }
        }
    }
}
