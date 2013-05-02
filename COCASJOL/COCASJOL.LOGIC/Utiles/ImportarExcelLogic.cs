using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;
using System.Transactions;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Utiles
{
    /// <summary>
    /// Clase con logica de Importar Excel
    /// </summary>
    public class ImportarExcelLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(ImportarExcelLogic).Name);

        /// <summary>
        /// Datos de Excel
        /// </summary>
        private DataTable excelDt;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ImportarExcelLogic() { }

        /// <summary>
        /// Importa los datos de socios desde una hoja de Excel.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="CREADO_POR"></param>
        /// <returns>Mensaje de error de importación.</returns>
        public string SociosCargarDatos(string path, string CREADO_POR)
        {
            int x = 2;

            try 
	        {	        
	        	string connection = System.Configuration.ConfigurationManager.ConnectionStrings["excelConnection"].ConnectionString;

                string connectionFormatted = String.Format(connection, path);

                using (OleDbConnection oledbConnection = new OleDbConnection(connectionFormatted))
                {
                    this.excelDt = new DataTable();

                    oledbConnection.Open();

                    string strSheetname = "";

                    DataTable dtSheetNames = oledbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    
                    if (dtSheetNames.Rows.Count > 0)
                    {
                        strSheetname = dtSheetNames.Rows[0]["TABLE_NAME"].ToString();
                    }

                    string strExcelSheetSelect = System.Configuration.ConfigurationManager.AppSettings["excelSheetSelect"];

                    string excelSheetSelect = String.Format(strExcelSheetSelect, strSheetname);

                    OleDbCommand cmd = new OleDbCommand(excelSheetSelect, oledbConnection);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                    adapter.Fill(this.excelDt);
                }
	        }
	        catch (Exception ex)
	        {
	        	log.Fatal("Error fatal al importar socio. Conexion Excel Fallo.", ex);
	        	throw;
	        }

            string message = "";

            try
            {
                if (this.excelDt.Rows[1][0].ToString().Equals("Código"))
                {
                    using (var scope1 = new TransactionScope())
                    {
                        using (var db = new colinasEntities())
                        {
                            for (; x < this.excelDt.Rows.Count; x++)
                            {
                                DataRow row = this.excelDt.Rows[x];

                                socio socioImportado = new socio();
                                socioImportado.SOCIOS_ID = row[0].ToString();

                                bool validId = System.Text.RegularExpressions.Regex.IsMatch(socioImportado.SOCIOS_ID, "^[A-za-z]+[0-9]+$");

                                if (!validId)
                                {
                                    message = "Código de socio no valido";
                                    throw new Exception(message);
                                }

                                var socioQuery = from s in db.socios
                                                 where s.SOCIOS_ID == socioImportado.SOCIOS_ID
                                                 select s;

                                socio socioExistente = socioQuery.FirstOrDefault();

                                if (socioExistente != null)
                                {
                                    message = "Código de socio existente.";
                                    throw new Exception(message);
                                }

                                string letras = new string(socioImportado.SOCIOS_ID.Where(char.IsLetter).ToArray());
                                string numero = new string(socioImportado.SOCIOS_ID.Where(char.IsDigit).ToArray());


                                var codigoQuery = from c in db.codigo
                                                  where c.CODIGO_LETRA == letras
                                                  select c;

                                codigo cod = codigoQuery.FirstOrDefault();

                                if (cod != null)
                                {
                                    int codNum = 0;

                                    if (int.TryParse(numero, out codNum))
                                    {
                                        if (cod.CODIGO_NUMERO <= codNum)
                                        {
                                            cod.CODIGO_NUMERO = codNum + 1;
                                        }
                                    }
                                    else
                                    {
                                        message = "Error en código de socio. Numero invalido";
                                        throw new Exception(message);
                                    }

                                }


                                socioImportado.SOCIOS_PRIMER_NOMBRE = row[1].ToString();
                                socioImportado.SOCIOS_SEGUNDO_NOMBRE = row[2].ToString();
                                socioImportado.SOCIOS_PRIMER_APELLIDO = row[3].ToString();
                                socioImportado.SOCIOS_SEGUNDO_APELLIDO = row[4].ToString();
                                socioImportado.SOCIOS_RESIDENCIA = row[5].ToString();
                                socioImportado.SOCIOS_ESTADO_CIVIL = row[6].ToString();
                                socioImportado.SOCIOS_LUGAR_DE_NACIMIENTO = row[7].ToString();

                                DateTime SOCIOS_FECHA_DE_NACIMIENTO = new DateTime();
                                DateTime.TryParse(row[8].ToString(), out SOCIOS_FECHA_DE_NACIMIENTO);
                                socioImportado.SOCIOS_FECHA_DE_NACIMIENTO = SOCIOS_FECHA_DE_NACIMIENTO;

                                socioImportado.SOCIOS_NIVEL_EDUCATIVO = row[9].ToString();
                                socioImportado.SOCIOS_PROFESION = row[10].ToString();
                                socioImportado.SOCIOS_RTN = row[11].ToString();
                                socioImportado.SOCIOS_TELEFONO = row[12].ToString();
                                socioImportado.SOCIOS_IDENTIDAD = row[13].ToString();
                                socioImportado.SOCIOS_LUGAR_DE_EMISION = row[14].ToString();

                                DateTime SOCIOS_FECHA_DE_EMISION = new DateTime();
                                DateTime.TryParse(row[15].ToString(), out SOCIOS_FECHA_DE_EMISION);
                                socioImportado.SOCIOS_FECHA_DE_EMISION = SOCIOS_FECHA_DE_EMISION;

                                socioImportado.SOCIOS_ESTATUS = row[16].ToString().Equals("Si") || row[16].ToString().Equals("SI") || row[16].ToString().Equals("S") || row[16].ToString().Equals("s") ? 1 : 0;
                                socioImportado.SOCIOS_APORTACION_ORD = row[17].ToString().Equals("Si") || row[17].ToString().Equals("SI") || row[17].ToString().Equals("S") || row[17].ToString().Equals("s") ? true : false;
                                socioImportado.SOCIOS_APORTACION_EXTRAORD = row[18].ToString().Equals("Si") || row[18].ToString().Equals("SI") || row[18].ToString().Equals("S") || row[18].ToString().Equals("s") ? true : false;
                                socioImportado.SOCIOS_APORTACION_EXTRAORD_COOP = row[19].ToString().Equals("Si") || row[19].ToString().Equals("SI") || row[19].ToString().Equals("S") || row[19].ToString().Equals("s") ? true : false;

                                int SOCIOS_APORTACION_EXTRAORD_COUNT = 0;
                                int.TryParse(row[20].ToString(), out SOCIOS_APORTACION_EXTRAORD_COUNT);
                                socioImportado.SOCIOS_APORTACION_EXTRAORD_COUNT = SOCIOS_APORTACION_EXTRAORD_COUNT;

                                socioImportado.SOCIOS_APORTACION_INTERES_S_APORTACION = row[21].ToString().Equals("Si") || row[21].ToString().Equals("SI") || row[21].ToString().Equals("S") || row[21].ToString().Equals("s") ? true : false;

                                socioImportado.socios_generales = new socio_general();
                                socioImportado.socios_generales.GENERAL_CARNET_IHCAFE = row[22].ToString();
                                socioImportado.socios_generales.GENERAL_ORGANIZACION_SECUNDARIA = row[23].ToString();
                                socioImportado.socios_generales.GENERAL_NUMERO_CARNET = row[24].ToString();
                                socioImportado.socios_generales.GENERAL_EMPRESA_LABORA = row[25].ToString();
                                socioImportado.socios_generales.GENERAL_EMPRESA_CARGO = row[26].ToString();
                                socioImportado.socios_generales.GENERAL_EMPRESA_DIRECCION = row[27].ToString();
                                socioImportado.socios_generales.GENERAL_EMPRESA_TELEFONO = row[28].ToString();
                                socioImportado.socios_generales.GENERAL_SEGURO = row[29].ToString();

                                DateTime GENERAL_FECHA_ACEPTACION = new DateTime();
                                DateTime.TryParse(row[30].ToString(), out GENERAL_FECHA_ACEPTACION);
                                socioImportado.socios_generales.GENERAL_FECHA_ACEPTACION = GENERAL_FECHA_ACEPTACION;

                                socioImportado.socios_produccion = new socio_produccion();
                                socioImportado.socios_produccion.PRODUCCION_UBICACION_FINCA = row[31].ToString();
                                socioImportado.socios_produccion.PRODUCCION_AREA = row[32].ToString();
                                socioImportado.socios_produccion.PRODUCCION_VARIEDAD = row[33].ToString();
                                socioImportado.socios_produccion.PRODUCCION_ALTURA = row[34].ToString();
                                socioImportado.socios_produccion.PRODUCCION_DISTANCIA = row[35].ToString();

                                int PRODUCCION_ANUAL = 0;
                                int.TryParse(row[36].ToString(), out PRODUCCION_ANUAL);
                                socioImportado.socios_produccion.PRODUCCION_ANUAL = PRODUCCION_ANUAL;

                                socioImportado.socios_produccion.PRODUCCION_BENEFICIO_PROPIO = row[37].ToString();
                                socioImportado.socios_produccion.PRODUCCION_ANALISIS_SUELO = row[38].ToString();
                                socioImportado.socios_produccion.PRODUCCION_TIPO_INSUMOS = row[39].ToString();

                                int PRODUCCION_MANZANAS_CULTIVADAS = 0;
                                int.TryParse(row[40].ToString(), out PRODUCCION_MANZANAS_CULTIVADAS);
                                socioImportado.socios_produccion.PRODUCCION_MANZANAS_CULTIVADAS = PRODUCCION_MANZANAS_CULTIVADAS;

                                socioImportado.CREADO_POR = socioImportado.MODIFICADO_POR = CREADO_POR;
                                socioImportado.FECHA_CREACION = DateTime.Today;
                                socioImportado.FECHA_MODIFICACION = socioImportado.FECHA_CREACION;

                                db.socios.AddObject(socioImportado);
                            }

                            db.SaveChanges();
                            scope1.Complete();
                        }
                        message = "Importacion realizada Exitosamente.";
                    }
                }
                else
                    message = "La plantilla se encuentra en formato invalido o esta vacia.";

                return message;
            }
            catch (Exception ex)
            {
                log.Error("Error al importar socio.", ex);
                return String.Format("Error en la linea {0}", x + 2);
            }
        }
    }
}
