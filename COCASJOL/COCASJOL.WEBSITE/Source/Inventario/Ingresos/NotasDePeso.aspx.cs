using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Inventario.Ingresos; 

namespace COCASJOL.WEBSITE.Source.Inventario.Ingresos
{
    public partial class NotasDePeso : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    string loggedUsr = Session["username"] as string;
                    this.LoggedUserHdn.Text = loggedUsr;
                    this.ValidarCredenciales(typeof(EstadosNotaDePeso).Name);

                    this.AddFechaNotaTxt.SelectedDate = DateTime.Today;
                }
            }
            catch (Exception)
            {
                //log
                throw;
            }
        }

        protected void NotasDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }



        [DirectMethod(RethrowException=true)]
        public void AddNotaDePeso_Click(string Detalles)
        {
            try
            {
                string loggedUser = this.LoggedUserHdn.Text;

                var detalles = JSON.Deserialize<Dictionary<string, string>[]>(Detalles);

                Dictionary<string, string> variables = this.GetVariables(typeof(NotasDePeso).Name);
                //validar variables

                NotaDePesoLogic notadepesologic = new NotaDePesoLogic();


                string pDefecto = this.AddPorcentajeDefectoTxt.Text.Replace("%", "");
                string pHumedad = this.AddPorcentajeHumedadTxt.Text.Replace("%", "");

                notadepesologic.InsertarNotaDePeso
                    (int.Parse(this.AddEstadoNotaCmb.Text),
                    this.AddSociosIdTxt.Text,
                    int.Parse(this.AddClasificacionCafeCmb.Text),
                    this.AddFechaNotaTxt.SelectedDate,
                    this.AddCooperativaRadio.Value == null ? false : true,
                    decimal.Parse(pDefecto),
                    decimal.Parse(pHumedad),
                    decimal.Parse(this.AddSumaPesoBrutoTxt.Text),
                    decimal.Parse(this.AddTaraTxt.Text),
                    int.Parse(this.AddSacosRetenidosTxt.Text),
                    loggedUser,
                    detalles,
                    variables);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}