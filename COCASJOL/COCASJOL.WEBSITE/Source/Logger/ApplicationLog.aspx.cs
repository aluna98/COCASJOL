using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Web;
using log4net;
using System.Data.SQLite;

namespace COCASJOL.WEBSITE.Source.Logger
{
    public partial class ApplicationLog : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(ApplicationLog).Name);
        Dictionary<string, DbType> filter = new Dictionary<string, DbType>();

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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de bitacora de aplicacion.", ex);
                throw;
            }
        }

        private void InitilizeFilters()
        {
            try
            {
                filter.Clear();
                filter.Add("appdomain", DbType.String);
                filter.Add("aspnetcache", DbType.String);
                filter.Add("aspnetcontext", DbType.String);
                filter.Add("aspnetrequest", DbType.String);
                filter.Add("aspnetsession", DbType.String);
                filter.Add("date", DbType.String);
                filter.Add("exception", DbType.String);
                filter.Add("file", DbType.String);
                filter.Add("identity", DbType.String);
                filter.Add("location", DbType.String);
                filter.Add("level", DbType.String);
                filter.Add("line", DbType.Int32);
                filter.Add("logger", DbType.String);
                filter.Add("message", DbType.String);
                filter.Add("method", DbType.String);
                filter.Add("ndc", DbType.String);
                filter.Add("property", DbType.String);
                filter.Add("stacktrace", DbType.String);
                filter.Add("stacktracedetail", DbType.String);
                filter.Add("timestamp", DbType.Double);
                filter.Add("thread", DbType.String);
                filter.Add("type", DbType.String);
                filter.Add("username", DbType.String);
                filter.Add("utcdate", DbType.DateTime);
                filter.Add("appfree1", DbType.String);
                filter.Add("appfree2", DbType.String);
                filter.Add("appfree3", DbType.String);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al inicializar mapa de tipos de filtros.", ex);
                throw;
            }
        }

        protected void stLog_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                ApplyFilter(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar bitacora de aplicacion.", ex);
                throw;
            }
        }

        void CreateDateTimeFilters(StringBuilder sqlBuilder, SQLiteCommand command)
        {
            try
            {
                string dateFrom = Request.Form["ff_date_from"];
                string dateTo = Request.Form["ff_date_to"];
                string timeFrom = Request.Form["ff_time_from"];
                string timeTo = Request.Form["ff_time_to"];

                System.Globalization.CultureInfo extCulture = new System.Globalization.CultureInfo(ResourceManager1.Locale, false);
                if (dateFrom == null || dateTo == null)
                {
                    return;
                }

                DateTime from = DateTime.Now;
                DateTime to = DateTime.Now;
                if (dateFrom != "")
                {
                    from = DateTime.Parse(dateFrom, extCulture);
                    if (timeFrom != "")
                        from = from.Add(DateTime.Parse(timeFrom, extCulture).TimeOfDay);
                }
                if (dateTo != "")
                {
                    to = DateTime.Parse(dateTo, extCulture);
                    if (timeTo != "")
                        to = to.Add(DateTime.Parse(timeTo, extCulture).TimeOfDay);
                }

                if (dateFrom != "" && dateTo != "")
                {
                    sqlBuilder.Append(" AND date between @dateFrom AND @dateTo");
                    command.Parameters.Add(new SQLiteParameter { ParameterName = "@dateFrom", Value = from, DbType = DbType.DateTime, Direction = ParameterDirection.Input });
                    command.Parameters.Add(new SQLiteParameter { ParameterName = "@dateTo", Value = from, DbType = DbType.DateTime, Direction = ParameterDirection.Input });
                }
                else if (dateFrom != "" && dateTo == "")
                {
                    sqlBuilder.Append(" AND date >= @dateFrom");
                    command.Parameters.Add(new SQLiteParameter { ParameterName = "@dateFrom", Value = from, DbType = DbType.DateTime, Direction = ParameterDirection.Input });
                }
                else if (dateFrom == "" && dateTo != "")
                {
                    sqlBuilder.Append(" AND date =< @dateTo");
                    command.Parameters.Add(new SQLiteParameter { ParameterName = "@dateTo", Value = from, DbType = DbType.DateTime, Direction = ParameterDirection.Input });
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al crear filtros de fecha y hora.", ex);
                throw;
            }
        }

        private SQLiteParameter AddFilterParameter(SQLiteCommand command, string paramName, DbType paramType, object paramValue)
        {
            try
            {
                SQLiteParameter param = command.CreateParameter();
                param.ParameterName = paramName;

                if (paramType == DbType.String)
                    param.Value = string.Format("%{0}%", paramValue);
                else
                    param.Value = string.Format("{0}", paramValue);

                command.Parameters.Add(param);
                return param;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al agregar parametro de filtro.", ex);
                throw;
            }
        }

        private SQLiteCommand PrepareFilters(SQLiteConnection connection, string sql)
        {
            try
            {
                StringBuilder sqlBuilder = new StringBuilder(sql);
                SQLiteCommand command = connection.CreateCommand();
                string value = "";
                foreach (KeyValuePair<string, DbType> entry in filter)
                {
                    value = Request.Form["ff_" + entry.Key.ToString()];
                    if (value != null && value != string.Empty && value != "*")
                    {
                        if (entry.Value == DbType.String)
                            sqlBuilder.AppendFormat(" AND {0} LIKE @{1}", entry.Key, entry.Key);
                        else
                            sqlBuilder.AppendFormat(" AND {0} = @{1}", entry.Key, entry.Key);


                        AddFilterParameter(command, entry.Key, entry.Value, value);
                    }
                }
                CreateDateTimeFilters(sqlBuilder, command);
                command.CommandText = sqlBuilder.ToString();
                return command;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al preparar filtros.", ex);
                throw;
            }
        }

        private DataTable GetDataTable()
        {
            try
            {
                System.Globalization.CultureInfo extCulture =  new System.Globalization.CultureInfo(ResourceManager1.Locale, false);
                DataTable dt = new DataTable("log4net");
                InitilizeFilters();
                string query = System.Configuration.ConfigurationManager.AppSettings["AppLogSelectQuery"];
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["log4netConnection"].ConnectionString;

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand cmd = PrepareFilters(conn, query);

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

                    adapter.Fill(dt);
                }

                return dt;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener datatable de bitacora de aplicacion.", ex);
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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al aplicar filtros de bitacora de aplicacion.", ex);
                throw;
            }
        }
    }
}