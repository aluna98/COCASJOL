using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;
using System.Data.Odbc;
using System.Transactions;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Socios
{
    public class SociosLogic
    {
        private static ILog log = LogManager.GetLogger(typeof(SociosLogic).Name);

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
                catch (Exception ex)
                {
                    log.Fatal("Error fatal al obtener socios.", ex);
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
                log.Fatal("Error fatal al obtener beneficiarios de socio.", ex);
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
                                    where s.SOCIOS_ESTATUS >= 1
                                    select s;
                        return query.ToList<socio>();
                    }
                }
                catch (Exception ex)
                {
                    log.Fatal("Error fatal al obtener socios activos.", ex);
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
            string BENEFICIARIO_PORCENTAJE)
        {
                try
                {
                    using (var db = new colinasEntities())
                    {
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
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Fatal("Error fatal al actualizar beneficiario de socio.", ex);
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
            try
            {
                using (var scope1 = new TransactionScope())
                {
                    using (var db = new colinasEntities())
                    {
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

                        scope1.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar socio.", ex);
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
            try
            {
                string NuevoCodigo = "";
                using (var scope1 = new TransactionScope())
                {
                    using (var db = new colinasEntities())
                    {
                        string letra = SOCIOS_PRIMER_NOMBRE.Substring(0, 1);

                        var query = from cod in db.codigo
                                    where cod.CODIGO_LETRA == letra
                                    select cod;

                        codigo c = query.First();
                        NuevoCodigo = c.CODIGO_LETRA + c.CODIGO_NUMERO;
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

                        scope1.Complete();
                    }
                }

                string nombreCompleto = SOCIOS_PRIMER_NOMBRE + " ";
                if (SOCIOS_SEGUNDO_NOMBRE != "")
                    nombreCompleto += SOCIOS_SEGUNDO_NOMBRE + " ";
                if (SOCIOS_PRIMER_APELLIDO != "")
                    nombreCompleto += SOCIOS_PRIMER_APELLIDO + " ";
                if (SOCIOS_SEGUNDO_APELLIDO != "")
                    nombreCompleto += SOCIOS_SEGUNDO_APELLIDO + " ";
                InsertSociosDBISAM(NuevoCodigo, nombreCompleto);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar socio.", ex);
                throw;
            }
        }

        public void InsertarBeneficiario
            (string SOCIOS_ID,
            string BENEFICIARIO_IDENTIFICACION,
            string BENEFICIARIO_NOMBRE,
            string BENEFICIARIO_PARENTEZCO,
            string BENEFICIARIO_NACIMIENTO,
            string BENEFICIARIO_LUGAR_NACIMIENTO,
            string BENEFICIARIO_PORCENTAJE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
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
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar beneficiario de socio.", ex);
                throw;
            }
        }

        #endregion insert

        #region Delete

        public void EliminarSocio(string SOCIOS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from soc in db.socios
                                where soc.SOCIOS_ID == SOCIOS_ID
                                select soc;

                    socio socio = query.First();

                    db.DeleteObject(socio);

                    db.SaveChanges(); 
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar socio.", ex);
                throw;
            }
        }

        public void EliminarBeneficiario(string SOCIO_ID, string BENEFICIARIO_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from ben in db.beneficiario_x_socio
                                where ben.SOCIOS_ID == SOCIO_ID && ben.BENEFICIARIO_IDENTIFICACION == BENEFICIARIO_ID
                                select ben;

                    beneficiario_x_socio beneficiario = query.First();
                    db.DeleteObject(beneficiario);
                    db.SaveChanges(); 
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar beneficiario de socio.", ex);
                throw;
            }
        }

        #endregion

        #region Disable
        public void DeshabilitarSocio(string SOCIOS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from soc in db.socios
                                where soc.SOCIOS_ID == SOCIOS_ID
                                select soc;

                    socio socio = query.First();
                    socio.SOCIOS_ESTATUS = 0;
                    db.SaveChanges(); 
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al deshabilitar socio.", ex);
                throw;
            }
        }
        #endregion

        #region Enable
        public void HabilitarSocio(string SOCIOS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from soc in db.socios
                                where soc.SOCIOS_ID == SOCIOS_ID
                                select soc;

                    socio socio = query.First();
                    socio.SOCIOS_ESTATUS = 1;
                    db.SaveChanges(); 
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al habilitar socio.", ex);
                throw;
            }
        }
        #endregion

        #region Metodos

        public bool BuscarId(string SOCIOS_ID, string BENEFICIARIO_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
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
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al buscar id de socio.", ex);
                throw;
            }
        }

        public bool CienPorciento(string SOCIOS_ID, int PORCENTAJE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
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
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al validar porcentaje de beneficiario de socio.", ex);
                throw;
            }
        }

        public bool Igual100(string SOCIOS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
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
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al validar porcentaje al 100% de beneficiario de socio.", ex);
                throw;
            }
        }

        public string Antiguedad(string SOCIOS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from nuevo in db.socios_generales
                                where nuevo.SOCIOS_ID == SOCIOS_ID
                                select nuevo;
                    socio_general socgrl = query.First();

                    DateTime aceptacion = (DateTime)socgrl.GENERAL_FECHA_ACEPTACION;
                    string tiempo = "";
                    //Años
                    int Anhos = DateTime.Now.Year - aceptacion.Year;
                    if (DateTime.Now.Month < aceptacion.Month)
                    {
                        Anhos--;
                    }
                    else
                    {
                        if (DateTime.Now.Month == aceptacion.Month)
                        {
                            if (DateTime.Now.Day < aceptacion.Day)
                            {
                                Anhos--;
                            }
                        }
                    }
                    if (Anhos > 0)
                    {
                        if (Anhos > 1)
                            tiempo = Convert.ToString(Anhos) + " años ";
                        else
                            tiempo = Convert.ToString(Anhos) + " año ";
                    }
                    //Meses
                    int mes = DateTime.Now.Month - aceptacion.Month;
                    if (mes > 0)
                    {
                        if (mes == 1)
                            tiempo += Convert.ToString(mes) + " mes ";
                        else
                            tiempo += Convert.ToString(mes) + " meses ";
                    }
                    else
                    {
                        int dias = DateTime.Now.Day - aceptacion.Day;
                        if (dias == 1)
                        {
                            tiempo = "Un dia";
                        }
                        else
                        {
                            tiempo = Convert.ToString(dias) + "dias";
                        }
                    }
                    return tiempo; 
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener antiguedad de socio.", ex);
                throw;
            }
        }

        public static bool EsNuevo(string SOCIOS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.socios", "SOCIOS_ID", SOCIOS_ID);

                    var s = db.GetObjectByKey(k);

                    socio soc = (socio)s;

                    if (soc.SOCIOS_ESTATUS == 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al comprobar si es socio nuevo.", ex);
                throw;
            }
        }

        public static bool DebePagarAportacionOrdinaria(string SOCIOS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.socios", "SOCIOS_ID", SOCIOS_ID);

                    var s = db.GetObjectByKey(k);

                    socio soc = (socio)s;

                    if (soc.SOCIOS_APORTACION_ORD == false)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al comprobar si el socio debe pagar aportacion ordinaria.", ex);
                throw;
            }
        }

        public static bool DebePagarAportacionExtraordinaria(string SOCIOS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.socios", "SOCIOS_ID", SOCIOS_ID);

                    var s = db.GetObjectByKey(k);

                    socio soc = (socio)s;

                    if (soc.SOCIOS_APORTACION_EXTRAORD == false)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al comprobar si el socio debe pagar aportacion extraordinaria.", ex);
                throw;
            }
        }

        public static void PagarGastoDeIngreso(string SOCIOS_ID, colinasEntities db)
        {
            try
            {
                EntityKey k = new EntityKey("colinasEntities.socios", "SOCIOS_ID", SOCIOS_ID);

                var s = db.GetObjectByKey(k);

                socio soc = (socio)s;

                soc.SOCIOS_ESTATUS = 2;//Ya pago cuota de ingreso anual
                //SOCIOS_ESTATUS == 0 -> Desactivado
                //SOCIOS_ESTATUS == 1 -> Nuevo
                //SOCIOS_ESTATUS == 2 -> Gastos de ingreso pagados

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al pagar gasto de ingreso.", ex);
                throw;
            }
        }

        public static void PagarAportacionOrdinaria(string SOCIOS_ID, colinasEntities db)
        {
            try
            {
                EntityKey k = new EntityKey("colinasEntities.socios", "SOCIOS_ID", SOCIOS_ID);

                var s = db.GetObjectByKey(k);

                socio soc = (socio)s;

                soc.SOCIOS_APORTACION_ORD = true;//Ya se pago la aportacion extraordinaria anual

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al pagar aportacion ordinaria.", ex);
                throw;
            }
        }

        public static bool PagarAportacionExtraordinaria(string SOCIOS_ID, int SOCIOS_APORTACION_EXTRAORD_COOP_COUNT, colinasEntities db)
        {
            try
            {
                bool result = false;


                EntityKey k = new EntityKey("colinasEntities.socios", "SOCIOS_ID", SOCIOS_ID);

                var s = db.GetObjectByKey(k);

                socio soc = (socio)s;

                if (soc.SOCIOS_APORTACION_EXTRAORD_COUNT >= SOCIOS_APORTACION_EXTRAORD_COOP_COUNT)
                    soc.SOCIOS_APORTACION_EXTRAORD_COOP = true;//Ya se pagaron las aportaciones extraordinaras para cooperativa

                if (soc.SOCIOS_APORTACION_EXTRAORD_COOP == true)
                    result = true;

                soc.SOCIOS_APORTACION_EXTRAORD = true;//Ya se pago la aportacion extraordinaria anual
                soc.SOCIOS_APORTACION_EXTRAORD_COUNT++;

                db.SaveChanges();

                return result;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al pagar aportacion extraordinaria.", ex);
                throw;
            }
        }

        public static void PagarAportacionInteresesSobreAportaciones(string SOCIOS_ID, colinasEntities db)
        {
            try
            {
                EntityKey k = new EntityKey("colinasEntities.socios", "SOCIOS_ID", SOCIOS_ID);

                var s = db.GetObjectByKey(k);

                socio soc = (socio)s;

                soc.SOCIOS_APORTACION_INTERES_S_APORTACION = true;//Ya se pago la aportacion intereses sobre aportaciones anual

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al pagar intereses sobre aportaciones.", ex);
                throw;
            }
        }

        public static void InsertSociosDBISAM(string id, string nombre)
        {
            OdbcTransaction transaction = null;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["A2DBISAM"].ConnectionString;

            bool activo = true;
            int zero = 0;

            string parameterChar = System.Configuration.ConfigurationManager.AppSettings["A2DBISAM_ParameterChar"];
            string queryStringCliente = System.Configuration.ConfigurationManager.AppSettings["A2DBISAM_InsertarCliente"];
            string queryStringProveedor = System.Configuration.ConfigurationManager.AppSettings["A2DBISAM_InsertarProveedor"];

            string queryCliente = string.Format(queryStringCliente, parameterChar);
            string queryProveedor = string.Format(queryStringProveedor, parameterChar);

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    OdbcCommand myCommandCliente = new OdbcCommand(queryCliente, connection);
                    myCommandCliente.Parameters.Clear();
                    myCommandCliente.Parameters.Add(new OdbcParameter { Value = id, DbType = DbType.String, Direction = ParameterDirection.Input });
                    myCommandCliente.Parameters.Add(new OdbcParameter { Value = nombre, DbType = DbType.String, Direction = ParameterDirection.Input });
                    myCommandCliente.Parameters.Add(new OdbcParameter { Value = activo, DbType = DbType.Boolean, Direction = ParameterDirection.Input });
                    myCommandCliente.Parameters.Add(new OdbcParameter { Value = zero, DbType = DbType.String, Direction = ParameterDirection.Input });
                    myCommandCliente.Parameters.Add(new OdbcParameter { Value = id, DbType = DbType.String, Direction = ParameterDirection.Input });
                    myCommandCliente.Parameters.Add(new OdbcParameter { Value = id, DbType = DbType.String, Direction = ParameterDirection.Input });


                    OdbcCommand myCommandProveedor = new OdbcCommand(queryProveedor, connection);
                    myCommandProveedor.Parameters.Clear();
                    myCommandProveedor.Parameters.Add(new OdbcParameter { Value = id, DbType = DbType.String, Direction = ParameterDirection.Input });
                    myCommandProveedor.Parameters.Add(new OdbcParameter { Value = nombre, DbType = DbType.String, Direction = ParameterDirection.Input });
                    myCommandProveedor.Parameters.Add(new OdbcParameter { Value = activo, DbType = DbType.Boolean, Direction = ParameterDirection.Input });
                    myCommandProveedor.Parameters.Add(new OdbcParameter { Value = zero, DbType = DbType.String, Direction = ParameterDirection.Input });
                    myCommandProveedor.Parameters.Add(new OdbcParameter { Value = id, DbType = DbType.String, Direction = ParameterDirection.Input });
                    myCommandProveedor.Parameters.Add(new OdbcParameter { Value = id, DbType = DbType.String, Direction = ParameterDirection.Input });

                    connection.Open();
                    transaction = connection.BeginTransaction();

                    myCommandCliente.Transaction = transaction;
                    myCommandProveedor.Transaction = transaction;

                    myCommandCliente.ExecuteNonQuery();
                    myCommandProveedor.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                string errorMsg = string.Format("Error al obtener socios desde DBISAM. ConnectionString: {0} - SQLInsertCliente: {1} - SQLInsertProveedor: {2}", connectionString, queryCliente, queryProveedor);
                log.Error(errorMsg, ex);
            }
        }

        #endregion
    }
}