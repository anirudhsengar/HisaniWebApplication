using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static HisaniWebApplication.Models.CommonFunctions;
using System.Data.SqlClient;

namespace HisaniWebApplication.Vet
{
    public partial class VetDashboard : System.Web.UI.Page
    {
        Commonfn Commonfn = new Commonfn(); // Common functions class instance

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string vetEmail = Session["VetEmail"].ToString();
                if (string.IsNullOrEmpty(vetEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                LoadDashboardData(vetEmail);
            }
        }

        private void LoadDashboardData(string vetEmail)
        {
            try
            {
                // Query for Assigned Horses
                string assignedHorsesQuery = @"
                    SELECT COUNT(*) 
                    FROM Horse 
                    INNER JOIN Stable ON Horse.StableID = Stable.StableID
                    WHERE Stable.VetEmail = @VetEmail";
                using (SqlCommand cmd = new SqlCommand(assignedHorsesQuery))
                {
                    cmd.Parameters.AddWithValue("@VetEmail", vetEmail);
                    int assignedHorsesCount = Convert.ToInt32(Commonfn.FetchScalar(cmd));
                    lblAssignedHorses.Text = assignedHorsesCount.ToString();
                }

                // First, retrieve the StableID associated with the VetEmail
                string stableIdQuery = @"
    SELECT StableID
    FROM Stable
    WHERE VetEmail = @VetEmail";

                string stableId = null;
                using (SqlCommand cmd = new SqlCommand(stableIdQuery))
                {
                    cmd.Parameters.AddWithValue("@VetEmail", vetEmail);
                    stableId = Commonfn.FetchScalar(cmd).ToString();
                }

                // If no StableID was found, exit or handle appropriately
                if (string.IsNullOrEmpty(stableId))
                {
                    // Handle the case where the VetEmail doesn't have an associated StableID
                    lblTotalRecords.Text = "No records found.";
                    lblTodaysRecords.Text = "No records found.";
                    return;
                }

                // Query for Total Records for the specific StableID
                string totalRecordsQuery = @"
    SELECT COUNT(*) 
    FROM Records
    WHERE StableID = @StableID";

                using (SqlCommand cmd = new SqlCommand(totalRecordsQuery))
                {
                    cmd.Parameters.AddWithValue("@StableID", stableId);
                    int totalRecordsCount = Convert.ToInt32(Commonfn.FetchScalar(cmd));
                    lblTotalRecords.Text = totalRecordsCount.ToString();
                }

                // Query for Today's Records for the specific StableID
                string todaysRecordsQuery = @"
    SELECT COUNT(*) 
    FROM Records
    WHERE StableID = @StableID 
    AND CAST(RecordDate AS DATE) = CAST(GETDATE() AS DATE)";

                using (SqlCommand cmd = new SqlCommand(todaysRecordsQuery))
                {
                    cmd.Parameters.AddWithValue("@StableID", stableId);
                    int todaysRecordsCount = Convert.ToInt32(Commonfn.FetchScalar(cmd));
                    lblTodaysRecords.Text = todaysRecordsCount.ToString();
                }


            }
            catch (Exception ex)
            {
                // Log error and show message (optional)
                Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
            }
        }
    }
}