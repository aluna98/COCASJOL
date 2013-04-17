using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using log4net;

namespace COCASJOL.LOGIC.Seguridad
{
    /// <summary>
    /// Clase con logica de Criptografia TripleDES
    /// </summary>
    public class CriptografiaTDES
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(CriptografiaTDES).Name);

        /// <summary>
        /// Proveedor de Servicio de encriptacion TripleDES.
        /// </summary>
        private TripleDESCryptoServiceProvider cryptoProvider;

        /// <summary>
        /// Constructor. Inicializa la llave y el vector de inicialización para encriptar.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="VectorKey"></param>
        public CriptografiaTDES(string Key, string VectorKey)
        {
            try
            {
                cryptoProvider = new TripleDESCryptoServiceProvider();

                cryptoProvider.Key = Encoding.UTF8.GetBytes(Key);
                cryptoProvider.IV = Encoding.UTF8.GetBytes(VectorKey);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al construir CriptografiaTDES.", ex);
                throw;
            }
        }

        /// <summary>
        /// Encriptar texto.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Texto encriptado.</returns>
        public string EncondeString(string key)
        {
            try
            {
                MemoryStream ms;
                CryptoStream cs;
                StreamWriter sw;

                if (key != "")
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(), CryptoStreamMode.Write);
                    sw = new StreamWriter(cs);

                    sw.Write(key);
                    sw.Flush();
                    cs.FlushFinalBlock();
                    ms.Flush();

                    return Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
                }

                return "";
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al encryptar cadena.", ex);
                throw;
            }
        }

        /// <summary>
        /// Desencriptar texto.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Texto desencriptado.</returns>
        public string DecodeString(string key)
        {
            try
            {
                MemoryStream ms;
                CryptoStream cs;
                StreamReader sr;

                byte[] _buffer;

                if (key != "")
                {
                    _buffer = Convert.FromBase64String(key);
                    ms = new MemoryStream(_buffer);
                    cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(), CryptoStreamMode.Read);
                    sr = new StreamReader(cs);

                    return sr.ReadToEnd();
                }

                return "";
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al desencryptar cadena.", ex);
                throw;
            }
        }        
    }
}
