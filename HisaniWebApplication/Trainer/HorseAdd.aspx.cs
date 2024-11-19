using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HisaniWebApplication.Models;

namespace HisaniWebApplication.Trainer
{
    public partial class HorseAdd : System.Web.UI.Page
    {
        private CommonFunctions.Commonfn fn = new CommonFunctions.Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure the user is logged in or the page should not load.
        }

        protected void btnAddHorse_Click(object sender, EventArgs e)
        {
            string horseName = HorseName.Text.Trim();
            string horseBreed = HorseBreed.Text.Trim();
            string horseSex = HorseSex.SelectedValue;
            DateTime dateOfBirth = DateTime.Parse(DateOfBirth.Text); // Date picker returns text in the form of 'YYYY-MM-DD'

            string trainerEmail = Session["TrainerEmail"] as string; // TrainerEmail should be in session
            if (string.IsNullOrEmpty(trainerEmail))
            {
                Response.Redirect("~/Authentication/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }

            try
            {
                // Check if stable exists for this trainer
                string query = "SELECT StableID FROM Stable WHERE TrainerEmail = @TrainerEmail";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@TrainerEmail", trainerEmail);
                object stableIDObj = fn.FetchScalar(cmd);

                if (stableIDObj != null)
                {
                    int stableID = Convert.ToInt32(stableIDObj);

                    // Insert the new horse
                    string insertQuery = "INSERT INTO Horse (StableID, HorseName, HorseBreed, Sex, DateOfBirth) " +
                                         "VALUES (@StableID, @HorseName, @HorseBreed, @Sex, @DateOfBirth)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery);
                    insertCmd.Parameters.AddWithValue("@StableID", stableID);
                    insertCmd.Parameters.AddWithValue("@HorseName", horseName);
                    insertCmd.Parameters.AddWithValue("@HorseBreed", horseBreed);
                    insertCmd.Parameters.AddWithValue("@Sex", horseSex);
                    insertCmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                    fn.ExecuteQuery(insertCmd); // Execute the query to insert the horse into the database

                    // Redirect to the HorseDetails page
                    Response.Redirect("HorseDetails.aspx?HorseID=" + GetHorseID(horseName));
                }
                else
                {
                    // Handle case where stable does not exist for the trainer
                    Response.Write("<script>alert('No stable found for this trainer.');</script>");
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log error, show error message)
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
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
    }
}
