using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Last modified 2019-04-16
namespace CasinoWebApp
{
    public partial class MainMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }

        protected void btnPlay_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AuthenticatedPages/Blackjack.aspx");
        }

        protected void btnHowToPlay_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HowToPlay.aspx");
        }

        protected void btnLeaderboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Leaderboard.aspx");
        }
    }
}