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
            ///     Método para 
            /// </summary>
            /// <param name="OrdenEntidad"></param>
            /// <param name="ConnectionString"></param>
        public void InsertaProductoOrdenTemp(PreOrdenEntidad PreOrdenEntidad, string ConnectionString)
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command;
                SqlParameter Parameter;

                try
                {
                    Command = new SqlCommand("InsertaProductoOrdenTemp", Connection);
                    Command.CommandType = CommandType.StoredProcedure;

                    Parameter = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                    Parameter.Value = PreOrdenEntidad.PreOrdenId;
                    Command.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parameter.Value = PreOrdenEntidad.ProductoId;
                    Command.Parameters.Add(Parameter);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                    Connection.Close();
                }
                catch (SqlException Exception)
                {
                    _ErrorId = Exception.Number;
                    _DescripcionError = Exception.Message;

                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();
                }
            }
        #endregion
    }
}
