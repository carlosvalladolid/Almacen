using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.Entidad.Almacen;

namespace Activos.AccesoDatos.Almacen
{
   public class TipoDocumentoAcceso:Base
    {
         protected int _ErrorId;
        protected string _DescripcionError;

        /// <summary>
        ///     Número de error, en caso de que haya ocurrido uno. Cero por default.
        /// </summary>
        public int ErrorId
        {
            get { return _ErrorId; }
        }

        /// <summary>
        ///     Descripción de error, en caso de que haya ocurrido uno. Empty por default.
        /// </summary>
        public string DescripcionError
        {
            get { return _DescripcionError; }
        }

        /// <summary>
        ///     Constructor de la clase.
        /// </summary>
        public TipoDocumentoAcceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
        }

        #region "Métodos"
            /// <summary>
            ///     Busca los TipoDocumento en la base de datos.
            /// </summary>
            /// <param name="TipoDocumentoEntidad">Entidad de TipoDocumento.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            public DataSet SeleccionarTipoDocumento(TipoDocumentoEntidad TipoDocumentoEntidad, string CadenaConexion)
            {
                DataSet Resultado = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("SeleccionarTipoDocumentoProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("TipoDocumentoId", SqlDbType.SmallInt);
                    Parametro.Value = TipoDocumentoEntidad.TipoDocumentoId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                    Parametro.Value = TipoDocumentoEntidad.Nombre;
                    Comando.Parameters.Add(Parametro);

                    Adaptador = new SqlDataAdapter(Comando);

                    Conexion.Open();
                    Adaptador.Fill(Resultado);
                    Conexion.Close();

                    return Resultado;
                }
                catch (SqlException Excepcion)
                {
                    _ErrorId = Excepcion.Number;
                    _DescripcionError = Excepcion.Message;

                    return Resultado;
                }
            }
        #endregion
    }




    }
