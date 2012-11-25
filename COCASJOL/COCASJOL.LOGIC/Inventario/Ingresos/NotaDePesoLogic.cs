using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COCASJOL.LOGIC.Inventario.Ingresos
{
    public class NotaDePesoLogic
    {
        public static IEnumerable<object> GetSocios()
        {
            return new COCASJOL.LOGIC.Socios.SociosLogic().getData().Select( r => new { SOCIOS_ID = r.SOCIOS_ID, SOCIOS_NOMBRE = String.Format( "{0} {1}", r.SOCIOS_PRIMER_NOMBRE, r.SOCIOS_PRIMER_APELLIDO ) } );
        }



        public static void SaveNotaDePeso(string SOCIO_ID, DateTime FECHA,decimal TIPO_CAFE, decimal DESCUENTO,decimal HUMEDAD,Dictionary<string,string>[] detalle)
        {
            var db = new colinasEntities();

            var nota = COCASJOL.LOGIC.nota_de_peso.Createnota_de_peso( 0, SOCIO_ID, TIPO_CAFE, 0, DESCUENTO, HUMEDAD );
            COCASJOL.LOGIC.nota_detalle.Createnota_detalle( 0, 0 );
            foreach ( var item in detalle )
            {
                nota.nota_detalle.Add( new nota_detalle() { DETALLE_PESO = Convert.ToDecimal( item[ "DETALLE_PESO" ] ), DETALLE_CANTIDAD_SACOS = Convert.ToInt32( item[ "DETALLE_CANTIDAD_SACOS" ] ) } );
            }
            db.AddTonota_de_peso( nota );
            db.SaveChanges();
            
        }

        
        public static IEnumerable<Object> GetNotasDePeso( string SOCIO_ID, string SOCIO_NOMBRE, string NOTA_ID, string NOTA_TIPO_CAFE, string NOTA_PORCENTAJE_DEFECTO, string NOTA_CARRO_PROPIO )
        {
            using ( var db =  new colinasEntities() )
            {
                var notas = from n in db.nota_de_peso.Include( "socios" ).AsParallel()
                            //join s in db.socios.AsParallel() on n.SOCIOS_ID equals s.SOCIOS_ID 

                            where
                            ( String.IsNullOrEmpty( SOCIO_ID ) ? true : n.SOCIOS_ID == SOCIO_ID ) &&
                            ( String.IsNullOrEmpty( SOCIO_NOMBRE ) ? true : (
                                        n.socios.SOCIOS_PRIMER_NOMBRE.Contains( SOCIO_NOMBRE ) ||
                                        n.socios.SOCIOS_SEGUNDO_NOMBRE.Contains( SOCIO_NOMBRE ) ||
                                        n.socios.SOCIOS_PRIMER_APELLIDO.Contains( SOCIO_NOMBRE ) ||
                                        n.socios.SOCIOS_SEGUNDO_APELLIDO.Contains( SOCIO_NOMBRE ) ) ) &&
                            ( String.IsNullOrEmpty( NOTA_ID ) ? true : n.NOTA_ID == Convert.ToInt32( NOTA_ID ) ) &&
                            ( String.IsNullOrEmpty( NOTA_TIPO_CAFE ) ? true : n.NOTA_TIPO_CAFE == Convert.ToDecimal( NOTA_TIPO_CAFE ) ) &&
                            ( String.IsNullOrEmpty( NOTA_PORCENTAJE_DEFECTO ) ? true : n.NOTA_PORCENTAJE_DESCUENTO == Convert.ToDecimal( NOTA_PORCENTAJE_DEFECTO ) )
                            select new { NOTA_ID = n.NOTA_ID, SOCIO_ID = n.socios.SOCIOS_ID, SOCIO_NOMBRE = n.socios.SOCIOS_PRIMER_NOMBRE + " " + n.socios.SOCIOS_PRIMER_APELLIDO, NOTA_TIPO_CAFE = n.NOTA_TIPO_CAFE, NOTA_PORCENTAJE_DEFECTO = n.NOTA_PORCENTAJE_DESCUENTO, NOTA_CARRO_PROPIO = false }
                            ;
                //var notas2 = from n in notas.ToList().AsParallel()
                //             join s in db.socios.AsParallel() on n.SOCIOS_ID equals s.SOCIOS_ID
                //             where
                //             String.IsNullOrEmpty( SOCIO_NOMBRE ) ? true : (
                //                        s.SOCIOS_PRIMER_NOMBRE.Contains( SOCIO_NOMBRE ) ||
                //                        s.SOCIOS_SEGUNDO_NOMBRE.Contains( SOCIO_NOMBRE ) ||
                //                        s.SOCIOS_PRIMER_APELLIDO.Contains( SOCIO_NOMBRE ) ||
                //                        s.SOCIOS_SEGUNDO_APELLIDO.Contains( SOCIO_NOMBRE ) )
                //             select new { NOTA_ID = n.NOTA_ID, SOCIO_ID = s.SOCIOS_ID, SOCIO_NOMBRE = s.SOCIOS_PRIMER_NOMBRE + " " + s.SOCIOS_PRIMER_APELLIDO, NOTA_TIPO_CAFE = n.NOTA_TIPO_CAFE, NOTA_PORCENTAJE_DEFECTO = n.NOTA_PORCENTAJE_DESCUENTO, NOTA_CARRO_PROPIO = false }
                //           ;
                return notas.ToList();
            }
            throw new NotImplementedException();
        }
    }
}
