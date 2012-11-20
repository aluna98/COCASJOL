using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

namespace COCASJOL.Website
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string currentPage = base.Request.QueryString["aspxerrorpath"];
                ExtNet.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = "Ocurrio un error inesperado.",
                    Buttons = MessageBox.Button.OK,
                    Handler = "window.location = '" + currentPage + "'"
                });
            }
            catch (Exception ex)
            {
                //log
                throw;
            }
        }
    }
}