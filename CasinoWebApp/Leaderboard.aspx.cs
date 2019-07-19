using System;
using System.Collections.Generic;
using System.Web.Configuration;
using CasinoLibrary.DAL;
using CasinoLibrary.GameFiles;

// Last modified 2019-04-17
namespace CasinoWebApp
{
    public partial class Leaderboard : System.Web.UI.Page
    {
        string connString = WebConfigurationManager.ConnectionStrings["CasinoDBConnString"].ConnectionString;
        RecordDAO recordDAO;
        List<Record> records;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                recordDAO = new RecordDAO(connString);
                records = recordDAO.ReadAllRecordsSortedByRatio();
                GVLeaderboard.DataSource = records;
                GVLeaderboard.DataBind();
            }
        }
    }
}
