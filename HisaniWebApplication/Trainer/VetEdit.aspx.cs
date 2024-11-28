using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static HisaniWebApplication.Models.CommonFunctions;

namespace HisaniWebApplication.Trainer
{
    public partial class VetEdit : System.Web.UI.Page
    {
        Commonfn fn = new Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure the page is not reloading the form data on postback
            if (!IsPostBack)
            {
                // Retrieve the vet email from the query string or session
                string vetEmail = Request.QueryString["email"];
                if (string.IsNullOrEmpty(vetEmail))
                {
                    Response.Write("Error: No vet specified.");
                    return;
                }

                // Fetch vet details using the email
                PopulateVetDetails(vetEmail);
            }
        }

        private void PopulateVetDetails(string vetEmail)
        {
            try
            {
                // SQL query to fetch the vet details based on email
                string query = "SELECT * FROM Vet WHERE Email = @VetEmail";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@VetEmail", vetEmail);

                DataTable vetDetails = fn.Fetch(cmd); // Fetch data from database

                if (vetDetails.Rows.Count > 0)
                {
                    DataRow vet = vetDetails.Rows[0];
                    vetName.Text = vet["VetName"].ToString();
                    vetSpeciality.Text = vet["Speciality"].ToString();
                    vetContact.Text = vet["Contact"].ToString();
                    vetEmailText.Text = vet["Email"].ToString(); // Assuming this field is editable in case needed
                }
                else
                {
                    Response.Write("Error: Vet not found.");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error fetching vet details: " + ex.Message);
            }
        }

        protected void btnEditVet_Click(object sender, EventArgs e)
        {
            string vetEmail = vetEmailText.Text;  // Get the email from the textbox
            string name = vetName.Text;           // Get the name from the textbox
            string speciality = vetSpeciality.Text; // Get the speciality from the textbox
            string contact = vetContact.Text;     // Get the contact number from the textbox

            try
            {
                // Validate form inputs
                if (string.IsNullOrEmpty(vetEmail) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(speciality) || string.IsNullOrEmpty(contact))
                {
                    Response.Write("All fields are required.");
                    return;
                }

                if (!long.TryParse(contact, out long contactNumber))
                {
                    Response.Write("Invalid contact number.");
                    return;
                }

                // Update the vet details
                string updateQuery = "UPDATE Vet SET VetName = @VetName, Speciality = @Speciality, Contact = @Contact WHERE Email = @Email";
                SqlCommand updateCmd = new SqlCommand(updateQuery);
                updateCmd.Parameters.AddWithValue("@VetName", name);
                updateCmd.Parameters.AddWithValue("@Speciality", speciality);
                updateCmd.Parameters.AddWithValue("@Contact", contact);
                updateCmd.Parameters.AddWithValue("@Email", vetEmail);

                fn.ExecuteQuery(updateCmd); // Execute update query

                // Redirect to the VetDetails page or wherever needed
                Response.Redirect("VetDetails.aspx?email=" + vetEmail);
            }
            catch (Exception ex)
            {
                Response.Write("Error saving vet details: " + ex.Message);
            }
        }
    }
}
