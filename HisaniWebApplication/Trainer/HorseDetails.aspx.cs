using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HisaniWebApplication.Models;

namespace HisaniWebApplication.Trainer
{
    public partial class HorseDetails : System.Web.UI.Page
    {
        private CommonFunctions.Commonfn fn = new CommonFunctions.Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string horseID = Request.QueryString["HorseID"];
                if (string.IsNullOrEmpty(horseID))
                {
                    Response.Redirect("~/Trainer/HorseList.aspx");
                    return;
                }
                LoadHorseDetails(horseID);
            }
        }

        private void LoadHorseDetails(string horseID)
        {
            try
            {
                string query = "SELECT HorseName, HorseBreed, Sex, DateOfBirth FROM Horse WHERE HorseID = @HorseID";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@HorseID", horseID);

                DataTable dt = fn.Fetch(cmd);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblHorseName.Text = row["HorseName"].ToString();
                    lblHorseBreed.Text = row["HorseBreed"].ToString();
                    lblHorseSex.Text = row["Sex"].ToString();
                    lblHorseDOB.Text = Convert.ToDateTime(row["DateOfBirth"]).ToString("yyyy-MM-dd");
                }
                else
                {
                    Response.Redirect("~/Trainer/HorseList.aspx");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnEditHorse_Click(object sender, EventArgs e)
        {
            string horseID = Request.QueryString["HorseID"];
            Response.Redirect($"~/Trainer/HorseEdit.aspx?HorseID={horseID}");
        }

        protected void btnDeleteHorse_Click(object sender, EventArgs e)
        {
            string horseID = Request.QueryString["HorseID"];
            try
            {
                string query = "DELETE FROM Horse WHERE HorseID = @HorseID";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@HorseID", horseID);

                fn.ExecuteQuery(cmd);
                Response.Redirect("~/Trainer/HorseList.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }
    }
}
