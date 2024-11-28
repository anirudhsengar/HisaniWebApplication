using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using Newtonsoft.Json;
using static HisaniWebApplication.Models.CommonFunctions;

namespace HisaniWebApplication.Trainer
{
    public partial class TrainerHome : Page
    {
        Commonfn fn = new Commonfn();

        public string HorseNamesJson { get; set; }
        public string HorseDobJson { get; set; }
        public string HorseGenderLabelsJson { get; set; }
        public string HorseGenderDataJson { get; set; }
        public string DailyRecords { get; set; }
        public string CurrentHorsesCount { get; set; }
        public string TotalUsers { get; set; }
        public string UserDistributionJson { get; set; }
        public string TotalLogins { get; set; }
        public string UploadsJson { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadChartData();
            }
        }

        private void LoadChartData()
        {
            string trainerEmail = Session["TrainerEmail"] as string;

            if (string.IsNullOrEmpty(trainerEmail))
            {
                // If the session variable is missing, redirect to the login page.
                Response.Redirect("~/Login.aspx");
                return;
            }

            // Fetch StableID for the logged-in trainer.
            string query = "SELECT StableID FROM Stable WHERE TrainerEmail = @TrainerEmail";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

            DataTable stableTable = fn.Fetch(cmd);

            // Check if the trainer has any stables.
            if (stableTable == null || stableTable.Rows.Count == 0)
            {
                // If no stables exist, redirect to the Add Stable page.
                Response.Redirect("~/Trainer/StableAdd.aspx");
                return;
            }

            // Trainer has a stable; proceed with data loading.
            int stableID = Convert.ToInt32(stableTable.Rows[0]["StableID"]);

            // Horse Names
            var horseNamesQuery = "SELECT HorseName FROM Horse WHERE StableID = " + stableID;
            HorseNamesJson = JsonConvert.SerializeObject(
                fn.Fetch(new SqlCommand(horseNamesQuery))
                  .AsEnumerable()
                  .Select(r => r["HorseName"].ToString()));

            // Horse DOB
            var dobQuery = "SELECT DateOfBirth FROM Horse WHERE StableID = " + stableID;
            HorseDobJson = JsonConvert.SerializeObject(
                fn.Fetch(new SqlCommand(dobQuery))
                  .AsEnumerable()
                  .Select(r => new { x = Convert.ToDateTime(r["DateOfBirth"]), y = 1 }));

            // Horse Gender
            var genderQuery = "SELECT Sex, COUNT(*) AS Count FROM Horse WHERE StableID = " + stableID + " GROUP BY Sex";
            var dtGender = fn.Fetch(new SqlCommand(genderQuery));
            HorseGenderLabelsJson = JsonConvert.SerializeObject(dtGender.AsEnumerable().Select(r => r["Sex"].ToString()));
            HorseGenderDataJson = JsonConvert.SerializeObject(dtGender.AsEnumerable().Select(r => Convert.ToInt32(r["Count"])));

            // Daily Records
            var dailyRecordsQuery = "SELECT COUNT(*) FROM Records WHERE CAST(RecordDate AS DATE) = CAST(GETDATE() AS DATE) AND StableID = " + stableID;
            DailyRecords = fn.FetchScalar(new SqlCommand(dailyRecordsQuery)).ToString();

            // Current Horses
            var horseCountQuery = "SELECT COUNT(*) FROM Horse WHERE StableID = " + stableID;
            CurrentHorsesCount = fn.FetchScalar(new SqlCommand(horseCountQuery)).ToString();

            // Total Users
            var userCountQuery = "SELECT COUNT(*) FROM Users";
            TotalUsers = fn.FetchScalar(new SqlCommand(userCountQuery)).ToString();

            // User Distribution
            var userDistributionQuery = "SELECT TypeOfUser, COUNT(*) AS Count FROM Users GROUP BY TypeOfUser";
            UserDistributionJson = JsonConvert.SerializeObject(
                fn.Fetch(new SqlCommand(userDistributionQuery))
                  .AsEnumerable()
                  .Select(r => Convert.ToInt32(r["Count"])));

            // Total Logins
            var loginsQuery = "SELECT Logins FROM WebsiteStats";
            TotalLogins = fn.FetchScalar(new SqlCommand(loginsQuery)).ToString();

            // Records and Horses Count
            var recordsCountQuery = "SELECT COUNT(*) AS RecordsCount FROM Records";
            var horsesCountQuery = "SELECT COUNT(*) AS HorsesCount FROM Horse";

            var recordsCount = Convert.ToInt32(fn.FetchScalar(new SqlCommand(recordsCountQuery)));
            var horsesCount = Convert.ToInt32(fn.FetchScalar(new SqlCommand(horsesCountQuery)));

            // Prepare data for chart
            UploadsJson = JsonConvert.SerializeObject(new int[] { recordsCount, horsesCount });
        }

    }
}
