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
    /// <summary>
    /// Clase con logica de Socios
    /// </summary>
    public class SociosLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(SociosLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public SociosLogic() { }

        #region Select

        /// <summary>
        /// Obtiene todos los socios.
        /// </summary>
        /// <returns>Lista de socios.</returns>
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

        /// <summary>
        /// Obtiene los beneficiarios de socio.
        /// </summary>
        /// <param name="socioid"></param>
        /// <returns>Lista de beneficiarios de socio.</returns>
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

        /// <summary>
        /// Obtiene todos los socios activos.
        /// </summary>
        /// <returns>Lista de socios activos.</returns>
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

        public List<socio> getSociosInactivos()
        {
            try
            {
                using (var db= new colinasEntities())
                {
                    var query = from s in db.socios.Include("socios_generales").Include("socios_produccion").Include("beneficiario_x_socio")
                                where s.SOCIOS_ESTATUS < 1
                                select s;
                    return query.ToList<socio>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener socios inactivos.", ex);
                throw;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Actualiza el beneficiario de socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="BENEFICIARIO_IDENTIFICACION"></param>
        /// <param name="BENEFICIARIO_NOMBRE"></param>
        /// <param name="BENEFICIARIO_PARENTEZCO"></param>
        /// <param name="BENEFICIARIO_NACIMIENTO"></param>
        /// <param name="BENEFICIARIO_LUGAR_NACIMIENTO"></param>
        /// <param name="BENEFICIARIO_PORCENTAJE"></param>
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

        /// <summary>
        /// Actualiza el socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="SOCIOS_PRIMER_NOMBRE"></param>
        /// <param name="SOCIOS_SEGUNDO_NOMBRE"></param>
        /// <param name="SOCIOS_PRIMER_APELLIDO"></param>
        /// <param name="SOCIOS_SEGUNDO_APELLIDO"></param>
        /// <param name="SOCIOS_RESIDENCIA"></param>
        /// <param name="SOCIOS_ESTADO_CIVIL"></param>
        /// <param name="SOCIOS_LUGAR_DE_NACIMIENTO"></param>
        /// <param name="SOCIOS_FECHA_DE_NACIMIENTO"></param>
        /// <param name="SOCIOS_NIVEL_EDUCATIVO"></param>
        /// <param name="SOCIOS_IDENTIDAD"></param>
        /// <param name="SOCIOS_PROFESION"></param>
        /// <param name="SOCIOS_RTN"></param>
        /// <param name="SOCIOS_TELEFONO"></param>
        /// <param name="SOCIOS_LUGAR_DE_EMISION"></param>
        /// <param name="SOCIOS_FECHA_DE_EMISION"></param>
        /// <param name="GENERAL_CARNET_IHCAFE"></param>
        /// <param name="GENERAL_ORGANIZACION_SECUNDARIA"></param>
        /// <param name="GENERAL_NUMERO_CARNET"></param>
        /// <param name="GENERAL_EMPRESA_LABORA"></param>
        /// <param name="GENERAL_EMPRESA_CARGO"></param>
        /// <param name="GENERAL_EMPRESA_DIRECCION"></param>
        /// <param name="GENERAL_EMPRESA_TELEFONO"></param>
        /// <param name="GENERAL_SEGURO"></param>
        /// <param name="GENERAL_FECHA_ACEPTACION"></param>
        /// <param name="PRODUCCION_UBICACION_FINCA"></param>
        /// <param name="PRODUCCION_AREA"></param>
        /// <param name="PRODUCCION_VARIEDAD"></param>
        /// <param name="PRODUCCION_ALTURA"></param>
        /// <param name="PRODUCCION_DISTANCIA"></param>
        /// <param name="PRODUCCION_ANUAL"></param>
        /// <param name="PRODUCCION_BENEFICIO_PROPIO"></param>
        /// <param name="PRODUCCION_ANALISIS_SUELO"></param>
        /// <param name="PRODUCCION_TIPO_INSUMOS"></param>
        /// <param name="PRODUCCION_MANZANAS_CULTIVADAS"></param>
        /// <param name="MODIFICADO_POR"></param>
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

        /// <summary>
        /// Insertar el socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="SOCIOS_PRIMER_NOMBRE"></param>
        /// <param name="SOCIOS_SEGUNDO_NOMBRE"></param>
        /// <param name="SOCIOS_PRIMER_APELLIDO"></param>
        /// <param name="SOCIOS_SEGUNDO_APELLIDO"></param>
        /// <param name="SOCIOS_RESIDENCIA"></param>
        /// <param name="SOCIOS_ESTADO_CIVIL"></param>
        /// <param name="SOCIOS_LUGAR_DE_NACIMIENTO"></param>
        /// <param name="SOCIOS_FECHA_DE_NACIMIENTO"></param>
        /// <param name="SOCIOS_NIVEL_EDUCATIVO"></param>
        /// <param name="SOCIOS_IDENTIDAD"></param>
        /// <param name="SOCIOS_PROFESION"></param>
        /// <param name="SOCIOS_RTN"></param>
        /// <param name="SOCIOS_TELEFONO"></param>
        /// <param name="SOCIOS_LUGAR_DE_EMISION"></param>
        /// <param name="SOCIOS_FECHA_DE_EMISION"></param>
        /// <param name="GENERAL_CARNET_IHCAFE"></param>
        /// <param name="GENERAL_ORGANIZACION_SECUNDARIA"></param>
        /// <param name="GENERAL_NUMERO_CARNET"></param>
        /// <param name="GENERAL_EMPRESA_LABORA"></param>
        /// <param name="GENERAL_EMPRESA_CARGO"></param>
        /// <param name="GENERAL_EMPRESA_DIRECCION"></param>
        /// <param name="GENERAL_EMPRESA_TELEFONO"></param>
        /// <param name="GENERAL_SEGURO"></param>
        /// <param name="GENERAL_FECHA_ACEPTACION"></param>
        /// <param name="PRODUCCION_UBICACION_FINCA"></param>
        /// <param name="PRODUCCION_AREA"></param>
        /// <param name="PRODUCCION_VARIEDAD"></param>
        /// <param name="PRODUCCION_ALTURA"></param>
        /// <param name="PRODUCCION_DISTANCIA"></param>
        /// <param name="PRODUCCION_ANUAL"></param>
        /// <param name="PRODUCCION_BENEFICIO_PROPIO"></param>
        /// <param name="PRODUCCION_ANALISIS_SUELO"></param>
        /// <param name="PRODUCCION_TIPO_INSUMOS"></param>
        /// <param name="PRODUCCION_MANZANAS_CULTIVADAS"></param>
        /// <param name="CREADO_POR"></param>
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

        /// <summary>
        /// Insertar el beneficiario de socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="BENEFICIARIO_IDENTIFICACION"></param>
        /// <param name="BENEFICIARIO_NOMBRE"></param>
        /// <param name="BENEFICIARIO_PARENTEZCO"></param>
        /// <param name="BENEFICIARIO_NACIMIENTO"></param>
        /// <param name="BENEFICIARIO_LUGAR_NACIMIENTO"></param>
        /// <param name="BENEFICIARIO_PORCENTAJE"></param>
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

        /// <summary>
        /// Elimina el socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
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

        /// <summary>
        /// Elimina el beneficiario.
        /// </summary>
        /// <param name="SOCIO_ID"></param>
        /// <param name="BENEFICIARIO_ID"></param>
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

        /// <summary>
        /// Desactiva el socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
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

        /// <summary>
        /// Habilita el socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
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

        public enum HabilitarSocios
        {
            Desactivado = 0, MostrarActivos = 1, MostrarTodos = 2
        }

        public static List<object> GetHabilitarSocios()
        {
            try
            {
                List<object> lst = new List<object>
                {
                    new { Text = HabilitarSocios.Desactivado.ToString(), Value = (int)HabilitarSocios.Desactivado },
                    new { Text = HabilitarSocios.MostrarActivos.ToString(), Value = (int)HabilitarSocios.MostrarActivos },
                    new { Text = HabilitarSocios.MostrarTodos.ToString(), Value = (int)HabilitarSocios.MostrarTodos }
                };

                return lst;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados para habilitar estado de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Verfica si el socio ya existe como beneficiario para otro socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="BENEFICIARIO_ID"></param>
        /// <returns>True es benficiario de otro socio. False no es beneficiario.</returns>
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

        /// <summary>
        /// Valida si ya se asigno el cien por ciento a los beneficiarios del socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="PORCENTAJE"></param>
        /// <returns>True si todavia esta por debajo del cien por ciento. False se sobrepaso del porcentaje valido.</returns>
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

        public bool EditCienPorciento(string SOCIOS_ID, string BENEFICIARIO_ID, int PORCENTAJE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    List<beneficiario_x_socio> lista;
                    var query = from nuevo in db.beneficiario_x_socio
                                where nuevo.SOCIOS_ID == SOCIOS_ID &&
                                nuevo.BENEFICIARIO_IDENTIFICACION != BENEFICIARIO_ID
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

        /// <summary>
        /// Verifica si ya se asigno el porcentaje a los benficiarios del socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <returns>True no se ha asignado el porcentaje a los beneficiarios. False ya se asigno el cien por ciento.</returns>
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

        /// <summary>
        /// Obtiene la antiguedad del socio en letras.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <returns>Antiguedad del socio en letras.</returns>
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

        /// <summary>
        /// Verifica si el socio es nuevo.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <returns>True es nuevo. False ya pago las deducciones de ingreso.</returns>
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

        /// <summary>
        /// Verifica si debe pagar aportacion ordinaria.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <returns>True debe pagar aportacion ordinaria. False ya pago la aportación ordinaria anual.</returns>
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

        /// <summary>
        /// Verifica si debe pagar aportacion extraordinaria.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <returns>True debe pagar aportacion extraordinaria. False ya pago la aportación extraordinaria anual.</returns>
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

        /// <summary>
        /// Desactiva el flag de pago de gastos de ingreso.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="db"></param>
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

        /// <summary>
        /// Desactiva el flag de pago de aportacion ordinaria.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="db"></param>
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

        /// <summary>
        /// Desactiva el flag de pago de aportacion extraordinaria.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="SOCIOS_APORTACION_EXTRAORD_COOP_COUNT"></param>
        /// <param name="db"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Desactiva el flag de pago de aportacion intereses sobre aportaciones.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="db"></param>
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

        /// <summary>
        /// Inserta el socio en BD DBISAM.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        public static void InsertSociosDBISAM(string id, string nombre)
        {
            OdbcTransaction transaction = null;
            string errorMsg = "";

            string connectionString = "";

            bool activo = true;
            int zero = 0;

            string parameterChar = "";
            string queryStringCliente = "";
            string queryStringProveedor = "";

            string queryCliente = "";
            string queryProveedor = "";


            try
            {
                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["A2DBISAM"].ConnectionString;

                parameterChar = System.Configuration.ConfigurationManager.AppSettings["A2DBISAM_ParameterChar"];
                queryStringCliente = System.Configuration.ConfigurationManager.AppSettings["A2DBISAM_InsertarCliente"];
                queryStringProveedor = System.Configuration.ConfigurationManager.AppSettings["A2DBISAM_InsertarProveedor"];

                queryCliente = string.Format(queryStringCliente, parameterChar);
                queryProveedor = string.Format(queryStringProveedor, parameterChar);
            }
            catch (Exception ex)
            {
                errorMsg = string.Format("Error al preparar conexion con DBISAM. ConnectionString: {0} - SQLInsertCliente: {1} - SQLInsertProveedor: {2}", connectionString, queryCliente, queryProveedor);
                log.Error(errorMsg, ex);
                return;
            }

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
                if (transaction != null)
                    transaction.Rollback();

                errorMsg = string.Format("Error al obtener socios desde DBISAM. ConnectionString: {0} - SQLInsertCliente: {1} - SQLInsertProveedor: {2}", connectionString, queryCliente, queryProveedor);
                log.Error(errorMsg, ex);
            }
        }

        #endregion
    }
}