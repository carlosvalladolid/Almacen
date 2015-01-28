using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.Entidad.Almacen;

namespace Activos.AccesoDatos.Almacen
{
    public class OrdenSalidaAcceso : Base
    {
        private int _ErrorId;
        private string _DescripcionError;
        DataSet _ResultadoDatos;

        /// <summary>
        ///     Numero de error, en caso de que haya ocurrido uno. Cero por default.
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
        ///     DataSet con el resultado de la base de datos.
        /// </summary>
        public DataSet ResultadoDatos
        {
            get { return _ResultadoDatos; }
        }

        /// <summary>
        ///     Constructor de la clase
        /// </summary>
        public OrdenSalidaAcceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _ResultadoDatos = null;
        }

        #region "Métodos"
            /// <summary>
            ///     Guarda un registro nuevo en la tabla temporal de orden de salida.
            /// </summary>
            /// <param name="Conexion">Conexión actual a la base de datos.</param>
            /// <param name="Transaccion">Transacción actual a la base de datos.</param>
            /// <param name="OrdenSalidaEntidad">Entidad de la orden de salida.</param>
            public void InsertarOrdenSalidaTemp(SqlConnection Conexion, SqlTransaction Transaccion, OrdenSalidaEntidad OrdenSalidaEntidad)
            {
                SqlCommand Commando;
                SqlParameter Parameter;

                try
                {
                    Commando = new SqlCommand("InsertarOrdenSalidaTemp", Conexion);
                    Commando.CommandType = CommandType.StoredProcedure;

                    Commando.Transaction = Transaccion;

                    Parameter = new SqlParameter("OrdenSalidaId", SqlDbType.VarChar);
                    Parameter.Value = OrdenSalidaEntidad.OrdenSalidaId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("RequisicionId", SqlDbType.VarChar);
                    Parameter.Value = OrdenSalidaEntidad.RequisicionId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("EstatusId", SqlDbType.VarChar);
                    Parameter.Value = OrdenSalidaEntidad.EstatusId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("UsuarioIdInserto", SqlDbType.VarChar);
                    Parameter.Value = OrdenSalidaEntidad.UsuarioIdInserto;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("Clave", SqlDbType.VarChar);
                    Parameter.Value = OrdenSalidaEntidad.Clave;
                    Commando.Parameters.Add(Parameter);

                    Commando.ExecuteNonQuery();
                }
                catch (SqlException Exception)
                {
                    _ErrorId = Exception.Number;
                    _DescripcionError = Exception.Message;
                }
            }
        #endregion
    }
}
