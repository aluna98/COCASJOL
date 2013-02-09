using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;
using System.Transactions;

using COCASJOL.LOGIC.Utiles;

namespace COCASJOL.LOGIC.Inventario.Ingresos
{
    public class NotaDePesoEnAdministracionLogic : NotaDePesoLogic
    {

        public NotaDePesoEnAdministracionLogic() : base("") { }
        public NotaDePesoEnAdministracionLogic(string ESTADOS_LLAVE) : base(ESTADOS_LLAVE) { }


        public void ActualizarNotaDePeso(int NOTAS_ID, int ESTADOS_NOTA_ID, string MODIFICADO_POR)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    using (var scope1 = new TransactionScope())
                    {
                        EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);
                        var n = db.GetObjectByKey(k);
                        nota_de_peso note = (nota_de_peso)n;

                        note.ESTADOS_NOTA_ID = ESTADOS_NOTA_ID;
                        note.MODIFICADO_POR = MODIFICADO_POR;
                        note.FECHA_MODIFICACION = DateTime.Today;

                        db.SaveChanges();

                        // verificar cambio de estado
                        if (note.estados_nota_de_peso.ESTADOS_NOTA_LLAVE != "ADMINISTRACION")
                        {
                            // notificar a usuarios

                            string ESTADO_NOTA_LLAVE = note.estados_nota_de_peso.ESTADOS_NOTA_LLAVE;
                            this.NotificarUsuarios("NOTAS" + ESTADO_NOTA_LLAVE, "MANT_NOTASPESOEN" + ESTADO_NOTA_LLAVE, note, db);
                        }

                        scope1.Complete();
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void RegistrarNotaDePeso(int NOTAS_ID, int ESTADO_ID, string MODIFICADO_POR)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    using (var scope1 = new TransactionScope())
                    {
                        EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);
                        var n = db.GetObjectByKey(k);
                        nota_de_peso note = (nota_de_peso)n;

                        note.ESTADOS_NOTA_ID = ESTADO_ID;
                        note.MODIFICADO_POR = MODIFICADO_POR;
                        note.FECHA_MODIFICACION = DateTime.Today;

                        db.SaveChanges();

                        if (note.estados_nota_de_peso.ESTADOS_NOTA_LLAVE == "ADMINISTRACION")
                        {
                            InventarioDeCafeLogic inventariodecafelogic = new InventarioDeCafeLogic();
                            note.TRANSACCION_NUMERO = inventariodecafelogic.InsertarTransaccionInventarioDeCafeDeSocio(note, db);
                            db.SaveChanges();
                        }

                        scope1.Complete();
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
