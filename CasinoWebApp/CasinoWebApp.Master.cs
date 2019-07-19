using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CasinoWebApp
{
    public partial class CasinoWebApp : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                LblWelcome.Text = "Welcome " + Context.User.Identity.Name;
                LBLogin.Visible = false;
                LBLogout.Visible = true;
            }
            else
            {
                LblWelcome.Text = "Welcome Guest";
                LBLogin.Visible = true;
                LBLogout.Visible = false;
            }
        }

        protected void LBLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void LBLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    }