using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Almacen;
using Activos.Comun.Constante;
using Activos.Entidad.Almacen;

namespace Activos.AccesoDatos.Almacen
{
    public class OrdenAcceso : Base
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
        public OrdenAcceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _ResultadoDatos = null;
        }

        #region "Métodos"
            /// <summary>
            ///     
            /// </summary>
            /// <param name="Conexion"></param>
            /// <param name="Transaccion"></param>
            /// <param name="PreOrdenEntidad"></param>
            public void InsertaProductoOrdenEncabezadoTemp(SqlConnection Conexion, SqlTransaction Transaccion, PreOrdenEntidad PreOrdenEntidad)
            {
                SqlCommand Commando;
                SqlParameter Parameter;

                try
                {
                    Commando = new SqlCommand("InsertaProductoOrdenEncabezadoTemp", Conexion);
                    Commando.CommandType = CommandType.StoredProcedure;

                    Commando.Transaction = Transaccion;

                    Parameter = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                    Parameter.Value = PreOrdenEntidad.PreOrdenId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parameter.Value = PreOrdenEntidad.ProductoId;
                    Commando.Parameters.Add(Parameter);

                    Commando.ExecuteNonQuery();
                }
                catch (SqlException Exception)
                {
                    _ErrorId = Exception.Number;
                    _DescripcionError = Exception.Message;
                }
            }

            /// <summary>
            ///     
            /// </summary>
            /// <param name="Conexion"></param>
            /// <param name="Transaccion"></param>
            /// <param name="PreOrdenEntidad"></param>
            public void InsertaProductoOrdenDetalleTemp(SqlConnection Conexion, SqlTransaction Transaccion, PreOrdenEntidad PreOrdenEntidad)
            {
                SqlCommand Commando;
                SqlParameter Parameter;

                try
                {
                    Commando = new SqlCommand("InsertaProductoOrdenDetalleTemp", Conexion);
                    Commando.CommandType = CommandType.StoredProcedure;

                    Commando.Transaction = Transaccion;

                    Parameter = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                    Parameter.Value = PreOrdenEntidad.PreOrdenId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parameter.Value = PreOrdenEntidad.ProductoId;
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
