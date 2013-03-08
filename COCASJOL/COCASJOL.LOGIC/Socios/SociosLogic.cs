using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using System.Data.Odbc;

namespace COCASJOL.LOGIC.Socios
{
    public class SociosLogic
    {
        public SociosLogic() { } 

        #region Select
            public List<socio> getData()
            {
                try
                {
                    using (var db = new colinasEntities())
                    {
                    var query = from s in db.socios.Include("socios_generales").Include("socios_produccion").Include("beneficiario_x_socio")
                                    select s;
                        return query.ToList<socio>();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public List<beneficiario_x_socio> getBenefxSocio(string socioid)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.socios.MergeOption = MergeOption.NoTracking;

                    var query = from ben in db.beneficiario_x_socio
                                where ben.SOCIOS_ID == socioid
                                select ben;

                    return query.ToList<beneficiario_x_socio>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

            public List<socio> getSociosActivos()
            {
                try
                {
                    using (var db = new colinasEntities())
                    {
                        var query = from s in db.socios.Include("socios_generales").Include("socios_produccion").Include("beneficiario_x_socio")
                                    where s.SOCIOS_ESTATUS == 1
                                    select s;
                        return query.ToList<socio>();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        #endregion

        #region Update
        public void ActualizarBeneficiario(
            string SOCIOS_ID,
            string BENEFICIARIO_IDENTIFICACION,
            string BENEFICIARIO_NOMBRE,
            string BENEFICIARIO_PARENTEZCO,
            string BENEFICIARIO_NACIMIENTO,
            string BENEFICIARIO_LUGAR_NACIMIENTO,
            string BENEFICIARIO_PORCENTAJE){
                colinasEntities db = null;
                try
                {
                    db = new colinasEntities();
                    List<beneficiario_x_socio> Lista;
                    var query = from nuevo in db.beneficiario_x_socio
                                where nuevo.SOCIOS_ID == SOCIOS_ID && nuevo.BENEFICIARIO_IDENTIFICACION == BENEFICIARIO_IDENTIFICACION
                                select nuevo;
                    Lista = query.ToList<beneficiario_x_socio>();
                    if (Lista.Count > 0)
                    {
                        beneficiario_x_socio beneficiario = query.First();
                        beneficiario.SOCIOS_ID = SOCIOS_ID;
                        beneficiario.BENEFICIARIO_IDENTIFICACION = BENEFICIARIO_IDENTIFICACION;
                        beneficiario.BENEFICIARIO_LUGAR_NACIMIENTO = BENEFICIARIO_LUGAR_NACIMIENTO;
                        beneficiario.BENEFICIARIO_NACIMIENTO = DateTime.Parse(BENEFICIARIO_NACIMIENTO);
                        beneficiario.BENEFICIARIO_NOMBRE = BENEFICIARIO_NOMBRE;
                        beneficiario.BENEFICIARIO_PORCENTAJE = Convert.ToInt32(BENEFICIARIO_PORCENTAJE);
                        beneficiario.BENEFICIARIO_PARENTEZCO = BENEFICIARIO_PARENTEZCO;
                        db.SaveChanges();
                        db.Dispose();
                    }
                }
                catch (Exception e)
                {
                    if (db != null)
                    {
                        db.Dispose();
                    }
                    throw;
                }
           

        }

        public void ActualizarSocio(
            string SOCIOS_ID,
            string SOCIOS_PRIMER_NOMBRE,
            string SOCIOS_SEGUNDO_NOMBRE,
            string SOCIOS_PRIMER_APELLIDO,
            string SOCIOS_SEGUNDO_APELLIDO,
            string SOCIOS_RESIDENCIA,
            string SOCIOS_ESTADO_CIVIL,
            string SOCIOS_LUGAR_DE_NACIMIENTO,
            string SOCIOS_FECHA_DE_NACIMIENTO,
            string SOCIOS_NIVEL_EDUCATIVO,
            string SOCIOS_IDENTIDAD,
            string SOCIOS_PROFESION,
            string SOCIOS_RTN,
            string SOCIOS_TELEFONO,
            string SOCIOS_LUGAR_DE_EMISION,
            string SOCIOS_FECHA_DE_EMISION,
            string GENERAL_CARNET_IHCAFE,
            string GENERAL_ORGANIZACION_SECUNDARIA,
            string  GENERAL_NUMERO_CARNET,
            string GENERAL_EMPRESA_LABORA,
            string GENERAL_EMPRESA_CARGO,
            string GENERAL_EMPRESA_DIRECCION, 
            string GENERAL_EMPRESA_TELEFONO,
            string GENERAL_SEGURO,
            string GENERAL_FECHA_ACEPTACION,
            string PRODUCCION_UBICACION_FINCA,
            string PRODUCCION_AREA,
            string PRODUCCION_VARIEDAD,
            string PRODUCCION_ALTURA,
            string PRODUCCION_DISTANCIA,
            int PRODUCCION_ANUAL,
            string PRODUCCION_BENEFICIO_PROPIO,
            string PRODUCCION_ANALISIS_SUELO,
            string PRODUCCION_TIPO_INSUMOS,
            int PRODUCCION_MANZANAS_CULTIVADAS,
            string MODIFICADO_POR
            )
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
                var query = from soc in db.socios
                            where soc.SOCIOS_ID == SOCIOS_ID
                            select soc;

                socio socio = query.First();
                socio.SOCIOS_ID = SOCIOS_ID;
                socio.SOCIOS_PRIMER_NOMBRE = SOCIOS_PRIMER_NOMBRE;
                socio.SOCIOS_SEGUNDO_NOMBRE = SOCIOS_SEGUNDO_NOMBRE;
                socio.SOCIOS_PRIMER_APELLIDO = SOCIOS_PRIMER_APELLIDO;
                socio.SOCIOS_SEGUNDO_APELLIDO = SOCIOS_SEGUNDO_APELLIDO;
                socio.SOCIOS_RESIDENCIA = SOCIOS_RESIDENCIA;
                socio.SOCIOS_ESTADO_CIVIL = SOCIOS_ESTADO_CIVIL;
                socio.SOCIOS_LUGAR_DE_NACIMIENTO = SOCIOS_LUGAR_DE_NACIMIENTO;
                socio.SOCIOS_FECHA_DE_NACIMIENTO = DateTime.Parse(SOCIOS_FECHA_DE_NACIMIENTO);
                socio.SOCIOS_NIVEL_EDUCATIVO = SOCIOS_NIVEL_EDUCATIVO;
                socio.SOCIOS_IDENTIDAD = SOCIOS_IDENTIDAD;
                socio.SOCIOS_PROFESION = SOCIOS_PROFESION;
                socio.SOCIOS_RTN = SOCIOS_RTN;
                socio.SOCIOS_TELEFONO = SOCIOS_TELEFONO;
                socio.SOCIOS_LUGAR_DE_EMISION = SOCIOS_LUGAR_DE_EMISION;
                socio.SOCIOS_FECHA_DE_EMISION = DateTime.Parse(SOCIOS_FECHA_DE_EMISION);
                socio.MODIFICADO_POR = MODIFICADO_POR;
                socio.FECHA_MODIFICACION = DateTime.Today;
                socio.socios_generales.GENERAL_CARNET_IHCAFE = GENERAL_CARNET_IHCAFE;
                socio.socios_generales.GENERAL_ORGANIZACION_SECUNDARIA = GENERAL_ORGANIZACION_SECUNDARIA;
                socio.socios_generales.GENERAL_NUMERO_CARNET = GENERAL_NUMERO_CARNET;
                socio.socios_generales.GENERAL_EMPRESA_LABORA = GENERAL_EMPRESA_LABORA;
                socio.socios_generales.GENERAL_EMPRESA_CARGO = GENERAL_EMPRESA_CARGO;
                socio.socios_generales.GENERAL_EMPRESA_DIRECCION = GENERAL_EMPRESA_DIRECCION;
                socio.socios_generales.GENERAL_EMPRESA_TELEFONO = GENERAL_EMPRESA_TELEFONO;
                socio.socios_generales.GENERAL_SEGURO = GENERAL_SEGURO;
                socio.socios_generales.GENERAL_FECHA_ACEPTACION = string.IsNullOrEmpty(GENERAL_FECHA_ACEPTACION) ? default(DateTime) : DateTime.Parse(GENERAL_FECHA_ACEPTACION);
                socio.socios_produccion.PRODUCCION_UBICACION_FINCA = PRODUCCION_UBICACION_FINCA;
                socio.socios_produccion.PRODUCCION_AREA = PRODUCCION_AREA;
                socio.socios_produccion.PRODUCCION_VARIEDAD = PRODUCCION_VARIEDAD;
                socio.socios_produccion.PRODUCCION_ALTURA = PRODUCCION_ALTURA;
                socio.socios_produccion.PRODUCCION_DISTANCIA = PRODUCCION_DISTANCIA;
                socio.socios_produccion.PRODUCCION_ANUAL = PRODUCCION_ANUAL;
                socio.socios_produccion.PRODUCCION_BENEFICIO_PROPIO = PRODUCCION_BENEFICIO_PROPIO;
                socio.socios_produccion.PRODUCCION_ANALISIS_SUELO = PRODUCCION_ANALISIS_SUELO;
                socio.socios_produccion.PRODUCCION_TIPO_INSUMOS = PRODUCCION_TIPO_INSUMOS;
                socio.socios_produccion.PRODUCCION_MANZANAS_CULTIVADAS = PRODUCCION_MANZANAS_CULTIVADAS;
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                throw;
            }
        }
        #endregion update

        #region insert

        public void InsertarSocio(
            string SOCIOS_ID,
            string SOCIOS_PRIMER_NOMBRE,
            string SOCIOS_SEGUNDO_NOMBRE,
            string SOCIOS_PRIMER_APELLIDO,
            string SOCIOS_SEGUNDO_APELLIDO,
            string SOCIOS_RESIDENCIA,
            string SOCIOS_ESTADO_CIVIL,
            string SOCIOS_LUGAR_DE_NACIMIENTO,
            string SOCIOS_FECHA_DE_NACIMIENTO,
            string SOCIOS_NIVEL_EDUCATIVO,
            string SOCIOS_IDENTIDAD,
            string SOCIOS_PROFESION,
            string SOCIOS_RTN,
            string SOCIOS_TELEFONO,
            string SOCIOS_LUGAR_DE_EMISION,
            string SOCIOS_FECHA_DE_EMISION,
            string GENERAL_CARNET_IHCAFE,
            string GENERAL_ORGANIZACION_SECUNDARIA,
            string GENERAL_NUMERO_CARNET,
            string GENERAL_EMPRESA_LABORA,
            string GENERAL_EMPRESA_CARGO,
            string GENERAL_EMPRESA_DIRECCION,
            string GENERAL_EMPRESA_TELEFONO,
            string GENERAL_SEGURO,
            string GENERAL_FECHA_ACEPTACION,
            string PRODUCCION_UBICACION_FINCA,
            string PRODUCCION_AREA,
            string PRODUCCION_VARIEDAD,
            string PRODUCCION_ALTURA,
            string PRODUCCION_DISTANCIA,
            int PRODUCCION_ANUAL,
            string PRODUCCION_BENEFICIO_PROPIO,
            string PRODUCCION_ANALISIS_SUELO,
            string PRODUCCION_TIPO_INSUMOS,
            int PRODUCCION_MANZANAS_CULTIVADAS,
            string CREADO_POR
            )
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
                string letra = SOCIOS_PRIMER_NOMBRE.Substring(0, 1);

                var query = from cod in db.codigo
                            where cod.CODIGO_LETRA == letra
                            select cod;

                codigo c = query.First();
                string NuevoCodigo = c.CODIGO_LETRA + c.CODIGO_NUMERO;
                c.CODIGO_NUMERO = c.CODIGO_NUMERO + 1;
                socio soc = new socio();
                soc.SOCIOS_ID = NuevoCodigo;
                soc.SOCIOS_PRIMER_NOMBRE = SOCIOS_PRIMER_NOMBRE;
                soc.SOCIOS_SEGUNDO_NOMBRE = SOCIOS_SEGUNDO_NOMBRE;
                soc.SOCIOS_PRIMER_APELLIDO = SOCIOS_PRIMER_APELLIDO;
                soc.SOCIOS_SEGUNDO_APELLIDO = SOCIOS_SEGUNDO_APELLIDO;
                soc.SOCIOS_RESIDENCIA = SOCIOS_RESIDENCIA;
                soc.SOCIOS_ESTADO_CIVIL = SOCIOS_ESTADO_CIVIL;
                soc.SOCIOS_LUGAR_DE_NACIMIENTO = SOCIOS_LUGAR_DE_NACIMIENTO;
                soc.SOCIOS_FECHA_DE_NACIMIENTO = DateTime.Parse(SOCIOS_FECHA_DE_NACIMIENTO);
                soc.SOCIOS_NIVEL_EDUCATIVO = SOCIOS_NIVEL_EDUCATIVO;
                soc.SOCIOS_IDENTIDAD = SOCIOS_IDENTIDAD;
                soc.SOCIOS_PROFESION = SOCIOS_PROFESION;
                soc.SOCIOS_RTN = SOCIOS_RTN;
                soc.SOCIOS_TELEFONO = SOCIOS_TELEFONO;
                soc.SOCIOS_LUGAR_DE_EMISION = SOCIOS_LUGAR_DE_EMISION;
                soc.SOCIOS_FECHA_DE_EMISION = DateTime.Parse(SOCIOS_FECHA_DE_EMISION);
                soc.CREADO_POR = CREADO_POR;
                soc.FECHA_CREACION = DateTime.Today;
                soc.MODIFICADO_POR = CREADO_POR;
                soc.FECHA_MODIFICACION = DateTime.Today;
                soc.SOCIOS_ESTATUS = 1;
                db.socios.AddObject(soc);
                socio_general socgen = new socio_general();
                socgen.SOCIOS_ID = NuevoCodigo;
                socgen.GENERAL_CARNET_IHCAFE = GENERAL_CARNET_IHCAFE;
                socgen.GENERAL_ORGANIZACION_SECUNDARIA = GENERAL_ORGANIZACION_SECUNDARIA;
                socgen.GENERAL_NUMERO_CARNET = GENERAL_NUMERO_CARNET;
                socgen.GENERAL_EMPRESA_LABORA = GENERAL_EMPRESA_LABORA;
                socgen.GENERAL_EMPRESA_CARGO = GENERAL_EMPRESA_CARGO;
                socgen.GENERAL_EMPRESA_DIRECCION = GENERAL_EMPRESA_DIRECCION;
                socgen.GENERAL_EMPRESA_TELEFONO = GENERAL_EMPRESA_TELEFONO;
                socgen.GENERAL_SEGURO = GENERAL_SEGURO;
                socgen.GENERAL_FECHA_ACEPTACION = DateTime.Parse(GENERAL_FECHA_ACEPTACION);
                db.socios_generales.AddObject(socgen);
                socio_produccion socprod = new socio_produccion();
                socprod.SOCIOS_ID = NuevoCodigo;
                socprod.PRODUCCION_UBICACION_FINCA = PRODUCCION_UBICACION_FINCA;
                socprod.PRODUCCION_AREA = PRODUCCION_AREA;
                socprod.PRODUCCION_VARIEDAD = PRODUCCION_VARIEDAD;
                socprod.PRODUCCION_ALTURA = PRODUCCION_ALTURA;
                socprod.PRODUCCION_DISTANCIA = PRODUCCION_DISTANCIA;
                socprod.PRODUCCION_ANUAL = PRODUCCION_ANUAL;
                socprod.PRODUCCION_BENEFICIO_PROPIO = PRODUCCION_BENEFICIO_PROPIO;
                socprod.PRODUCCION_ANALISIS_SUELO = PRODUCCION_ANALISIS_SUELO;
                socprod.PRODUCCION_TIPO_INSUMOS = PRODUCCION_TIPO_INSUMOS;
                socprod.PRODUCCION_MANZANAS_CULTIVADAS = PRODUCCION_MANZANAS_CULTIVADAS;
                db.socios_produccion.AddObject(socprod);
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception e)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                throw;
            }
        }

        public void InsertarBeneficiario(string SOCIOS_ID,
            string BENEFICIARIO_IDENTIFICACION,
            string BENEFICIARIO_NOMBRE,
            string BENEFICIARIO_PARENTEZCO,
            string BENEFICIARIO_NACIMIENTO,
            string BENEFICIARIO_LUGAR_NACIMIENTO,
            string BENEFICIARIO_PORCENTAJE)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
                beneficiario_x_socio benef = new beneficiario_x_socio();
                benef.SOCIOS_ID = SOCIOS_ID;
                benef.BENEFICIARIO_IDENTIFICACION = BENEFICIARIO_IDENTIFICACION;
                benef.BENEFICIARIO_NOMBRE = BENEFICIARIO_NOMBRE;
                benef.BENEFICIARIO_PARENTEZCO = BENEFICIARIO_PARENTEZCO;
                benef.BENEFICIARIO_NACIMIENTO = DateTime.Parse(BENEFICIARIO_NACIMIENTO);
                benef.BENEFICIARIO_LUGAR_NACIMIENTO = BENEFICIARIO_LUGAR_NACIMIENTO;
                benef.BENEFICIARIO_PORCENTAJE = Convert.ToInt32(float.Parse(BENEFICIARIO_PORCENTAJE));
                db.beneficiario_x_socio.AddObject(benef);
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception e)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                throw;
            }
        }
        #endregion insert

        #region Delete

        public void EliminarUsuario(string SOCIOS_ID)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();

                var query = from soc in db.socios
                            where soc.SOCIOS_ID == SOCIOS_ID
                            select soc;

                socio socio = query.First();

                db.DeleteObject(socio);

                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {
                if (db != null)
                    db.Dispose();
                throw;
            }
        }

        public void EliminarBeneficiario(string SOCIO_ID, string BENEFICIARIO_ID){
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
                var query = from ben in db.beneficiario_x_socio
                            where ben.SOCIOS_ID == SOCIO_ID && ben.BENEFICIARIO_IDENTIFICACION == BENEFICIARIO_ID
                            select ben;

                beneficiario_x_socio beneficiario = query.First();
                db.DeleteObject(beneficiario);
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception e)
            {
                if (db != null)
                    db.Dispose();
                throw;
            }
        }

        #endregion

        #region Disable
        public void DeshabilitarUsuario(string SOCIOS_ID)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();

                var query = from soc in db.socios
                            where soc.SOCIOS_ID == SOCIOS_ID
                            select soc;

                socio socio = query.First();
                socio.SOCIOS_ESTATUS = 0;
                db.SaveChanges();
                db.Dispose();

            }
            catch (Exception ex)
            {
                if (db != null)
                    db.Dispose();
                throw;
            }
        }
        #endregion

        #region Enable
        public void HabilitarUsuario(string SOCIOS_ID)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();

                var query = from soc in db.socios
                            where soc.SOCIOS_ID == SOCIOS_ID
                            select soc;

                socio socio = query.First();
                socio.SOCIOS_ESTATUS = 1;
                db.SaveChanges();
                db.Dispose();

            }
            catch (Exception ex)
            {
                if (db != null)
                    db.Dispose();
                throw;
            }
        }
        #endregion

        #region Metodos

        public bool BuscarId(string SOCIOS_ID, string BENEFICIARIO_ID){
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
                List<beneficiario_x_socio> lista;
                var query = from nuevo in db.beneficiario_x_socio
                            where nuevo.SOCIOS_ID == SOCIOS_ID && nuevo.BENEFICIARIO_IDENTIFICACION == BENEFICIARIO_ID
                            select nuevo;
                lista = query.ToList<beneficiario_x_socio>();

                if (lista.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                throw;
            }
        }

        public bool CienPorciento(string SOCIOS_ID, int PORCENTAJE)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
                List<beneficiario_x_socio> lista;
                var query = from nuevo in db.beneficiario_x_socio
                            where nuevo.SOCIOS_ID == SOCIOS_ID
                            select nuevo;
                lista = query.ToList<beneficiario_x_socio>();
                int total = PORCENTAJE;
                foreach (beneficiario_x_socio ben in lista)
                {
                    total += ben.BENEFICIARIO_PORCENTAJE.Value;
                }
                if (total > 100)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                throw;
            }
            return true;
        }

        public bool Igual100(string SOCIOS_ID)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
                List<beneficiario_x_socio> lista;
                var query = from nuevo in db.beneficiario_x_socio
                            where nuevo.SOCIOS_ID == SOCIOS_ID
                            select nuevo;
                lista = query.ToList<beneficiario_x_socio>();
                int total = 0;
                foreach (beneficiario_x_socio ben in lista)
                {
                    total += ben.BENEFICIARIO_PORCENTAJE.Value;
                }
                if (total == 100)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                throw;
            }
        }
        #endregion

        public static void getSociosDBISAM()
        {
            try
            {
                string queryString = "select * from sclientes";
                OdbcCommand command = new OdbcCommand(queryString);

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["A2DBISAM"].ConnectionString;

                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    command.Connection = connection;
                    connection.Open();

                    OdbcDataReader reader = command.ExecuteReader();

                    int fCount = reader.FieldCount;

                    string rec = "";

                    while (reader.Read())
                    {
                        for (int i = 0; i < fCount; i++)
                        {
                            rec += " " + (reader.GetValue(i).ToString());
                        }

                        rec += "\n";
                    }

                    OdbcDataAdapter adapter = new OdbcDataAdapter(queryString, connection);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}