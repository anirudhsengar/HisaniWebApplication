using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HisaniWebApplication.Vet
{
    public partial class VetMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string vetEmail = Session["VetEmail"] as string;
            if (string.IsNullOrEmpty(vetEmail))
            {
                Response.Redirect("~/Authentication/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            HighlightCurrentPage();
            StyleLogoutLink();
        }

        private void HighlightCurrentPage()
        {
            string currentPage = Request.Url.AbsolutePath.ToLower();

            // Highlight the active link based on the current URL
            if (currentPage.Contains("/vet/vetdashboard.aspx"))
            {
                dashboardLink.Attributes.Add("class", "active");
            }
            else if (currentPage.Contains("/vet/vet-horselist.aspx"))
            {
                horseLink.Attributes.Add("class", "active");
            }
            else if (currentPage.Contains("/vet/vet-recorddisplay.aspx"))
            {
                recordsLink.Attributes.Add("class", "active");
            }
        }

        private void StyleLogoutLink()
        {
            btnLogout.CssClass = "logout-link";
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