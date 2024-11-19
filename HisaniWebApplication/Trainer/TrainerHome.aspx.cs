using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static HisaniWebApplication.Models.CommonFunctions;

namespace HisaniWebApplication.Trainer
{
    public partial class TrainerHome : System.Web.UI.Page
    {
        Commonfn fn = new Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStatistics();
            }
        }

        private void LoadStatistics()
        {
            try
            {
                string trainerEmail = Session["TrainerEmail"] as string;
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                // Query for Total Horses
                string horseCountQuery = "SELECT COUNT(*) FROM Horse";
                DataTable dtHorseCount = fn.Fetch(horseCountQuery);
                lblTotalHorses.Text = dtHorseCount.Rows[0][0].ToString();

                // Query for Total Records
                string recordCountQuery = "SELECT COUNT(*) FROM Records";
                DataTable dtRecordCount = fn.Fetch(recordCountQuery);
                lblTotalRecords.Text = dtRecordCount.Rows[0][0].ToString();

                // Query for Today's Records
                string todaysRecordsQuery = "SELECT COUNT(*) FROM Records WHERE CAST(RecordDate AS DATE) = CAST(GETDATE() AS DATE)";
                DataTable dtTodaysRecords = fn.Fetch(todaysRecordsQuery);
                lblTodaysRecords.Text = dtTodaysRecords.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log error)
            }
        }
    }
}