using System;
using System.Data;
using System.Web.UI;
using static HisaniWebApplication.Models.CommonFunctions;

namespace HisaniWebApplication.Trainer
{
    public partial class TrainerMaster : MasterPage
    {
        Commonfn fn = new Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            string trainerEmail = Session["TrainerEmail"] as string;

            if (string.IsNullOrEmpty(trainerEmail))
            {
                Response.Redirect("~/Authentication/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }

            if (!IsPostBack)
            {
                SetStableLink();
                SetVetLink();
                SetHorseList();
                SetRecordsList();
                HighlightCurrentPage();
                StyleLogoutLink();
            }
        }

        private void HighlightCurrentPage()
        {
            string currentPage = Request.Url.AbsolutePath.ToLower();

            // Use absolute paths for comparison
            if (currentPage.Contains("/trainerhome.aspx"))
            {
                dashboardLink.Attributes.Add("class", "active");
            }
            else if (currentPage.Contains("/stabledetails.aspx") || currentPage.Contains("/stableadd.aspx"))
            {
                stableLink.Attributes.Add("class", "active");
            }
            else if (currentPage.Contains("/horselist.aspx"))
            {
                horseLink.Attributes.Add("class", "active");
            }
            else if (currentPage.Contains("/vetdetails.aspx") || currentPage.Contains("/vetadd.aspx"))
            {
                vetLink.Attributes.Add("class", "active");
            }
            else if (currentPage.Contains("/vetrecorddisplay.aspx"))
            {
                recordsLink.Attributes.Add("class", "active");
            }
        }

        private void StyleLogoutLink()
        {
            btnLogout.CssClass = "logout-link";
        }


        private void SetStableLink()
        {
            try
            {
                string trainerEmail = Session["TrainerEmail"] as string;
                string checkStableQuery = $"SELECT COUNT(*) FROM Stable WHERE TrainerEmail = '{trainerEmail}'";
                DataTable dt = fn.Fetch(checkStableQuery);

                stableLink.HRef = (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
                    ? "StableDetails.aspx"
                    : "StableAdd.aspx";
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
            }
        }

        private void SetVetLink()
        {
            try
            {
                string trainerEmail = Session["TrainerEmail"] as string;
                string stableQuery = $"SELECT StableID FROM Stable WHERE TrainerEmail = '{trainerEmail}'";
                DataTable dtStable = fn.Fetch(stableQuery);

                if (dtStable.Rows.Count == 0)
                {
                    vetLink.HRef = "StableAdd.aspx";
                    return;
                }

                int stableID = Convert.ToInt32(dtStable.Rows[0]["StableID"]);
                string checkVetQuery = $"SELECT COUNT(*) FROM Vet WHERE StableID = {stableID}";
                DataTable dtVet = fn.Fetch(checkVetQuery);

                vetLink.HRef = (dtVet.Rows.Count > 0 && Convert.ToInt32(dtVet.Rows[0][0]) > 0)
                    ? "VetDetails.aspx"
                    : "VetAdd.aspx";
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
            }
        }

        private void SetHorseList()
        {
            string trainerEmail = Session["TrainerEmail"] as string;
            string stableQuery = $"SELECT StableID FROM Stable WHERE TrainerEmail = '{trainerEmail}'";
            DataTable dtStable = fn.Fetch(stableQuery);

            horseLink.HRef = (dtStable.Rows.Count > 0) ? "HorseList.aspx" : "StableAdd.aspx";
        }

        private void SetRecordsList()
        {
            string trainerEmail = Session["TrainerEmail"] as string;
            string stableQuery = $"SELECT StableID FROM Stable WHERE TrainerEmail = '{trainerEmail}'";
            DataTable dtStable = fn.Fetch(stableQuery);

            recordsLink.HRef = (dtStable.Rows.Count > 0) ? "VetRecordDisplay.aspx" : "StableAdd.aspx";
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            // Clear session variables
            Session.Clear();

            // Abandon the current session
            Session.Abandon();

            // Redirect to the login page
            Response.Redirect("~/Authentication/Login.aspx");
        }
    }
}
