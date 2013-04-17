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

using log4net;

namespace COCASJOL.WEBSITE.Source.Prestamos
{
    public partial class Prestamos : COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(Prestamos).Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de tipos de prestamo.", ex);
                throw;
            }
        }

        protected void PrestamosSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                TiposPrestamoLogic prestamo = new TiposPrestamoLogic();
                var store1 = this.PrestamosGridP.GetStore();
                store1.DataSource = prestamo.getData();
                store1.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar tipos de prestamo.", ex);
                throw;
            }
        }

        protected void PrestamosSt_Update(object sender, DirectEventArgs e)
        {
            try
            {
                TiposPrestamoLogic prestamo = new TiposPrestamoLogic();
                int maximo = Convert.ToInt32(e.ExtraParams["PRESTAMOS_CANT_MAXIMA"]);
                int intereses = Convert.ToInt32(e.ExtraParams["PRESTAMOS_INTERES"]);
                int id = Convert.ToInt32(e.ExtraParams["PRESTAMOS_ID"]);
                prestamo.ActualizarPrestamos(id, e.ExtraParams["PRESTAMOS_NOMBRE"], e.ExtraParams["PRESTAMOS_DESCRIPCION"], maximo, intereses, this.LoggedUserHdn.Text);
                this.EditarPrestamosWin.Hide();
                X.Msg.Alert("Prestamos", "El Prestamo se ha actualizado satisfactoriamente.").Show();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar tipos de prestamo.", ex);
                throw;
            }
        }

        protected void PrestamosSt_Insert(object sender, DirectEventArgs e)
        {
            try
            {
                TiposPrestamoLogic logica = new TiposPrestamoLogic();
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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar tipos de prestamo.", ex);
                throw;
            }
        }

        protected void PrestamosSt_Delete(object sender, DirectEventArgs e)
        {
            try
            {
                TiposPrestamoLogic logica = new TiposPrestamoLogic();
                RowSelectionModel sm = PrestamosGridP.SelectionModel.Primary as RowSelectionModel;
                int id = Convert.ToInt32(sm.SelectedRow.RecordID);
                logica.EliminarPrestamo(id);
                X.Msg.Alert("Prestamos", "El Prestamo se ha eliminado satisfactoriamente.").Show();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar tipos de prestamo.", ex);
                throw;
            }
        }
    }
}