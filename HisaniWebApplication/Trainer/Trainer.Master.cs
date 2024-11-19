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
            }
        }

        private void SetStableLink()
        {
            try
            {
                // Directly retrieve the trainer's email from the session (since it's guaranteed to be present)
                string trainerEmail = Session["TrainerEmail"] as string;

                // Use the trainerEmail to check the stable existence in the database
                string checkStableQuery = $"SELECT COUNT(*) FROM Stable WHERE TrainerEmail = '{trainerEmail}'";
                DataTable dt = fn.Fetch(checkStableQuery);

                // Check if a stable exists for the trainer's email
                if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
                {
                    stableLink.HRef = "StableDetails.aspx"; // If stable exists
                }
                else
                {
                    stableLink.HRef = "StableAdd.aspx"; // If no stable exists
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void SetVetLink()
        {
            try
            {
                string trainerEmail = Session["TrainerEmail"] as string;// Replace with actual session value

                // Query to get the current stable ID for the trainer
                string stableQuery = $"SELECT StableID FROM Stable WHERE TrainerEmail = '{trainerEmail}'";
                DataTable dtStable = fn.Fetch(stableQuery);

                if (dtStable.Rows.Count == 0)
                {
                    vetLink.HRef = "StableAdd.aspx";
                    return;
                }

                int stableID = Convert.ToInt32(dtStable.Rows[0]["StableID"]);

                // Query to check if a vet is assigned to the current stable
                string checkVetQuery = $"SELECT COUNT(*) FROM Vet WHERE StableID = {stableID}";
                DataTable dtVet = fn.Fetch(checkVetQuery);

                // Check if a vet exists for the stable
                if (dtVet.Rows.Count > 0 && Convert.ToInt32(dtVet.Rows[0][0]) > 0)
                {
                    vetLink.HRef = "VetDetails.aspx"; // If vet exists for this stable
                }
                else
                {
                    vetLink.HRef = "VetAdd.aspx"; // If no vet exists for this stable
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void SetHorseList()
        {
            string trainerEmail = Session["TrainerEmail"] as string;// Replace with actual session value

            // Query to get the current stable ID for the trainer
            string stableQuery = $"SELECT StableID FROM Stable WHERE TrainerEmail = '{trainerEmail}'";
            DataTable dtStable = fn.Fetch(stableQuery);

            if (dtStable.Rows.Count == 0)
            {
                horseLink.HRef = "StableAdd.aspx";
            }
            else
            {
                horseLink.HRef = "HorseList.aspx";
            }
        }

        private void SetRecordsList()
        {
            string trainerEmail = Session["TrainerEmail"] as string;// Replace with actual session value

            // Query to get the current stable ID for the trainer
            string stableQuery = $"SELECT StableID FROM Stable WHERE TrainerEmail = '{trainerEmail}'";
            DataTable dtStable = fn.Fetch(stableQuery);

            if (dtStable.Rows.Count == 0)
            {
                recordsLink.HRef = "StableAdd.aspx";
            } 
            else
            {
                recordsLink.HRef = "VetRecordDisplay.aspx";
            }
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