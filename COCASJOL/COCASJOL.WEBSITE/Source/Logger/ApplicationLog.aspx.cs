using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Web;

using System.Data.SQLite;

namespace COCASJOL.WEBSITE.Source.Logger
{
    public partial class ApplicationLog : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {

                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;
            }
            catch (Exception)
            {
                //log
                throw;
            }
        }


        protected void stLog_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                ApplyFilter(null, null);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private DataTable GetDataTable()
        {
            try
            {
                DataTable dt = new DataTable("log4net");

                string query = System.Configuration.ConfigurationManager.AppSettings["AppLogSelectQuery"];

                //query +=
                //    string.IsNullOrEmpty(this.ff_level.Text) ? "" : " level like %" + this.ff_level.Text + "%";

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["log4netConnection"].ConnectionString;

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = query;

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                    adapter.Fill(dt);
                }

                return dt;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void ApplyFilter(object sender, DirectEventArgs e)
        {

            try
            {
                DataTable dt = GetDataTable();

                this.AppLogSt.DataSource = dt;
                this.AppLogSt.DataBind();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}