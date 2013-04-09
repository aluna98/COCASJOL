using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COCASJOL.LOGIC;
using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Web;
using COCASJOL.LOGIC.Seguridad; 
using COCASJOL.LOGIC.Prestamos;

namespace COCASJOL.WEBSITE.Source.Prestamos
{
    public partial class Prestamos : COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                COCASJOL.LOGIC.Configuracion.ConfiguracionDeSistemaLogic configLogic = new COCASJOL.LOGIC.Configuracion.ConfiguracionDeSistemaLogic(this.docConfiguracion);
                if (configLogic.VentanasCargarDatos == true)
                {
                    this.PrestamosSt_Reload(null, null);
                }
            }

            string loggedUsr = Session["username"] as string;
            this.LoggedUserHdn.Text = loggedUsr;
        }

        protected void PrestamosSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
            PrestamosLogic prestamo = new PrestamosLogic();
            var store1 = this.PrestamosGridP.GetStore();
            store1.DataSource = prestamo.getData();
            store1.DataBind();
        }

        protected void PrestamosSt_Update(object sender, DirectEventArgs e)
        {
            PrestamosLogic prestamo = new PrestamosLogic();
            int maximo = Convert.ToInt32(e.ExtraParams["PRESTAMOS_CANT_MAXIMA"]);
            int intereses = Convert.ToInt32(e.ExtraParams["PRESTAMOS_INTERES"]);
            int id = Convert.ToInt32(e.ExtraParams["PRESTAMOS_ID"]);
            prestamo.ActualizarPrestamos(id, e.ExtraParams["PRESTAMOS_NOMBRE"], e.ExtraParams["PRESTAMOS_DESCRIPCION"], maximo, intereses, this.LoggedUserHdn.Text);
            this.EditarPrestamosWin.Hide();
            X.Msg.Alert("Prestamos", "El Prestamo se ha actualizado satisfactoriamente.").Show();
        }

        protected void PrestamosSt_Insert(object sender, DirectEventArgs e)
        {
            PrestamosLogic logica = new PrestamosLogic();
            if (!logica.ExistePrestamo(e.ExtraParams["PRESTAMO_NOMBRE"]))
            {
                X.Msg.Alert("Prestamos", "ERROR: El nombre del prestamo ya existe.").Show();
            }
            else
            {
                int maximo = Convert.ToInt32(e.ExtraParams["PRESTAMOS_CANT_MAXIMA"]);
                int intereses = Convert.ToInt32(e.ExtraParams["PRESTAMOS_INTERES"]);
                int id = Convert.ToInt32(e.ExtraParams["PRESTAMOS_ID"]);
                logica.InsertarPrestamo(id, e.ExtraParams["PRESTAMOS_NOMBRE"], e.ExtraParams["PRESTAMOS_DESCRIPCION"], maximo, intereses, this.LoggedUserHdn.Text);
                X.Msg.Alert("Prestamos", "El Prestamo se ha creado satisfactoriamente.").Show();
                AgregarPrestamosWin.Hide();
            }
        }

        protected void PrestamosSt_Delete(object sender, DirectEventArgs e)
        {
            PrestamosLogic logica = new PrestamosLogic();
            RowSelectionModel sm = PrestamosGridP.SelectionModel.Primary as RowSelectionModel;
            int id = Convert.ToInt32(sm.SelectedRow.RecordID);
            logica.EliminarPrestamo(id);
            X.Msg.Alert("Prestamos", "El Prestamo se ha eliminado satisfactoriamente.").Show();
        }
    }
}