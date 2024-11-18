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
    public partial class Vet_HorseList : System.Web.UI.Page
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
                string vetEmail = Session["vetEmail"] as string; // Ensure vetEmail is set in the session
                if (string.IsNullOrEmpty(vetEmail))
                {
                    // Handle the case where trainer email is missing
                    return;
                }

                // Query to fetch horses associated with the trainer's stable
                string query = "SELECT HorseID, HorseName FROM Horse WHERE StableID = (SELECT StableID FROM Stable WHERE vetEmail = @vetEmail)";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@vetEmail", vetEmail);

                DataTable dt = fn.Fetch(cmd);
                if (dt.Rows.Count > 0)
                {
                    RepeaterHorseList.DataSource = dt;
                    RepeaterHorseList.DataBind();
                }
                else
                {
                    // Optionally handle the case where there are no horses to display
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, display error message, etc.)
            }
        }
    }
}