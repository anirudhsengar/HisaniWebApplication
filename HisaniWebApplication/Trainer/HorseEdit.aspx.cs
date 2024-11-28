using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HisaniWebApplication.Models;

namespace HisaniWebApplication.Trainer
{
    public partial class HorseEdit : System.Web.UI.Page
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
                cmd.Parameters.AddWithValue("@HorseID", horseID);  // Use the correct HorseID from the query string

                DataTable dt = fn.Fetch(cmd);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    HorseName.Text = row["HorseName"].ToString();
                    HorseBreed.Text = row["HorseBreed"].ToString();
                    HorseSex.SelectedValue = row["Sex"].ToString(); // Ensure dropdown values match database values
                    DateOfBirth.Text = Convert.ToDateTime(row["DateOfBirth"]).ToString("yyyy-MM-dd");
                }
                else
                {
                    Response.Redirect("~/Trainer/HorseList.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string horseID = Request.QueryString["HorseID"];
            if (string.IsNullOrEmpty(horseID))
            {
                Response.Redirect("~/Trainer/HorseList.aspx");
                return;
            }

            try
            {
                string query = "UPDATE Horse SET HorseName = @HorseName, HorseBreed = @HorseBreed, Sex = @HorseSex, DateOfBirth = @DateOfBirth WHERE HorseID = @HorseID";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@HorseID", horseID);
                cmd.Parameters.AddWithValue("@HorseName", HorseName.Text.Trim());
                cmd.Parameters.AddWithValue("@HorseBreed", HorseBreed.Text.Trim());
                cmd.Parameters.AddWithValue("@HorseSex", HorseSex.SelectedValue);
                cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth.Text);

                fn.ExecuteQuery(cmd);
                Response.Redirect($"~/Trainer/HorseDetails.aspx?HorseID={horseID}");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }

        protected void ValidateDateOfBirth(object source, ServerValidateEventArgs args)
        {
            DateTime dateOfBirth = DateTime.Parse(args.Value);
            if (dateOfBirth > DateTime.Now)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}
