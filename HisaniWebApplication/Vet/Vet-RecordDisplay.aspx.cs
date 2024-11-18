using HisaniWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HisaniWebApplication.Vet
{
    public partial class Vet_RecordDisplay : Page
    {
        private CommonFunctions.Commonfn fn = new CommonFunctions.Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["deleteRecordId"] != null)
            {
                int recordId = Convert.ToInt32(Request.QueryString["deleteRecordId"]);
                DeleteVetRecord(recordId);  // Delete the record based on the ID
            }

            if (!IsPostBack)
            {
                DisplayVetRecords();  // Display records on page load
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;

            if (!string.IsNullOrEmpty(btnDelete.CommandArgument) && int.TryParse(btnDelete.CommandArgument, out int recordId))
            {
                // Delete the record based on RecordID
                DeleteVetRecord(recordId);
            }
            else
            {
                // Log or handle the error for invalid CommandArgument
                Response.Write("Invalid RecordID. Cannot delete.");
            }
        }



        private void DeleteVetRecord(int recordId)
        {
            try
            {
                // SQL DELETE query to remove the record by RecordID
                string query = "DELETE FROM Records WHERE RecordID = @RecordID";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@RecordID", recordId);

                // Execute the query to delete the record
                fn.ExecuteQuery(cmd);

                // After successful deletion, refresh the page or redirect
                Response.Redirect("Vet-RecordDisplay.aspx"); // Redirect after successful delete
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., log the error or show a message to the user)

            }
        }

        private void DisplayVetRecords()
        {
            // Retrieve the trainer's email (can be obtained from session or any other method)
            string vetEmail = Session["vetEmail"] as string; // Replace with actual email from session
            string stableID = GetStableIDByvetEmail(vetEmail);

            if (string.IsNullOrEmpty(stableID))
            {

                return;
            }

            // Fetch records based on StableID
            string query = "SELECT RecordID, PartName, RecordDate, Comment FROM Records WHERE StableID = @StableID";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@StableID", stableID);

            DataTable dt = fn.Fetch(cmd);
            if (dt.Rows.Count > 0)
            {
                vetRecordRepeater.DataSource = dt;
                vetRecordRepeater.DataBind();
            }
            else
            {
            }
        }

        private string GetStableIDByvetEmail(string vetEmail)
        {
            string stableID = string.Empty;
            string query = "SELECT StableID FROM Stable WHERE VetEmail = @vetEmail";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@vetEmail", vetEmail);

            DataTable dt = fn.Fetch(cmd);
            if (dt.Rows.Count > 0)
            {
                stableID = dt.Rows[0]["StableID"].ToString();
            }

            return stableID;
        }
    }
}