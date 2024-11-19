using HisaniWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace HisaniWebApplication.Vet
{
    public partial class Vet_RecordAdd : Page
    {
        private CommonFunctions.Commonfn fn = new CommonFunctions.Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // Logic when the form is submitted
            }
        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            // Use unique variable names to avoid conflict with control names
            string selectedPartName = partName.SelectedValue;  // Use the DropDownList control for partName
            string selectedDate = date.Text;  // Use the TextBox control for the date
            string userComments = comments.Text;  // Use the TextBox control for comments

            // Retrieve StableID based on the logged-in Trainer's email
            string vetEmail = Session["vetEmail"] as string; // Replace with actual session-based retrieval
            if (string.IsNullOrEmpty(vetEmail))
            {
                Response.Redirect("~/Authentication/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }

            // Fetch StableID associated with the trainer
            string stableID = GetStableIDByvetEmail(vetEmail);
            if (string.IsNullOrEmpty(stableID))
            {
                Response.Write("Error: StableID not found for the trainer.");
                return;
            }

            try
            {
                // Prepare SQL command to insert the vet record into the database
                string query = "INSERT INTO Records (StableID, PartName, Comment, RecordDate) OUTPUT INSERTED.RecordID VALUES (@StableID, @PartName, @Comment, @RecordDate)";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@StableID", stableID);
                cmd.Parameters.AddWithValue("@PartName", selectedPartName);  // Use unique variable name here
                cmd.Parameters.AddWithValue("@Comment", userComments);  // Use unique variable name here
                cmd.Parameters.AddWithValue("@RecordDate", selectedDate);  // Use unique variable name here

                // Ensure you're getting the result correctly with ExecuteScalar
                object result = fn.FetchScalar(cmd);
                if (result != null)
                {
                    int insertedRecordID = Convert.ToInt32(result);
                    Response.Write("Inserted RecordID: " + insertedRecordID + "<br />");

                    // If you need to ensure the record is saved, check the rows affected
                    if (insertedRecordID > 0)
                    {
                        Response.Redirect("Vet-RecordDisplay.aspx");
                    }
                    else
                    {
                        Response.Write("Error: Inserted record ID is invalid.");
                    }
                }
                else
                {
                    Response.Write("Error: No RecordID returned from query.");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message + "<br />" + ex.StackTrace);
            }
        }



        // Function to get StableID based on the Trainer's email
        private string GetStableIDByvetEmail(string vetEmail)
        {
            string stableID = string.Empty;
            try
            {
                string query = "SELECT StableID FROM Stable WHERE VetEmail = @vetEmail";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@vetEmail", vetEmail);

                DataTable dt = fn.Fetch(cmd);

                // Debug the fetched data
                if (dt.Rows.Count == 0)
                {
                    Response.Write("No stable found for the given trainer email.");
                }
                else
                {
                    stableID = dt.Rows[0]["StableID"].ToString();
                    Response.Write("StableID found: " + stableID + "<br />");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error fetching StableID: " + ex.Message);
            }

            return stableID;
        }
    }
}