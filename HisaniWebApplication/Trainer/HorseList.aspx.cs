using System;
using System.Data;
using System.Data.SqlClient;
using HisaniWebApplication.Models;

namespace HisaniWebApplication.Trainer
{
    public partial class HorseList : System.Web.UI.Page
    {
        private CommonFunctions.Commonfn fn = new CommonFunctions.Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadHorseList();
            }
        }

        private void LoadHorseList()
        {
            try
            {
                string trainerEmail = Session["TrainerEmail"] as string; // Ensure TrainerEmail is set in the session
                if (string.IsNullOrEmpty(trainerEmail))
                {
                    Response.Redirect("~/Authentication/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                // Query to fetch horses associated with the trainer's stable
                string query = "SELECT HorseID, HorseName FROM Horse WHERE StableID = (SELECT StableID FROM Stable WHERE TrainerEmail = @TrainerEmail)";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@TrainerEmail", trainerEmail);

                DataTable dt = fn.Fetch(cmd);
                if (dt.Rows.Count > 0)
                {
                    RepeaterHorseList.DataSource = dt;
                    RepeaterHorseList.DataBind();
                }
                else
                {
                    // If no horses are found, show a message
                    RepeaterHorseList.Visible = false;
                    // Optionally add a label to display a message like "No horses found."
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, display error message, etc.)
                Response.Write("Error: " + ex.Message);
            }
        }
    }
}
