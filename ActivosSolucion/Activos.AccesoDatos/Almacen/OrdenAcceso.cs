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
            ///     Guarda el detalle de una orden de compra.
            /// </summary>
            /// <param name="Conexion">Conexión actual a la base de datos.</param>
            /// <param name="Transaccion">Transacción actual a la base de datos.</param>
            /// <param name="OrdenDetalleEntidad">Entidad del detalle de una orden de compra.</param>
            public void InsertarOrdenDetalle(SqlConnection Conexion, SqlTransaction Transaccion, OrdenEntidad OrdenEncabezadoEntidad)
            {
                SqlCommand Commando;
                SqlParameter Parameter;

                try
                {
                    Commando = new SqlCommand("InsertaOrdenDetalle", Conexion);
                    Commando.CommandType = CommandType.StoredProcedure;

                    Commando.Transaction = Transaccion;

                    Parameter = new SqlParameter("OrdenId", SqlDbType.VarChar);
                    Parameter.Value = OrdenEncabezadoEntidad.OrdenId;
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
            ///     Guarda el encabezado de una orden de compra.
            /// </summary>
            /// <param name="Conexion">Conexión actual a la base de datos.</param>
            /// <param name="Transaccion">Transacción actual a la base de datos.</param>
            /// <param name="OrdenDetalleEntidad">Entidad del detalle de una orden de compra.</param>
            public void InsertarOrdenEncabezado(SqlConnection Conexion, SqlTransaction Transaccion, OrdenEntidad OrdenEncabezadoEntidad)
            {
                SqlCommand Commando;
                SqlParameter Parameter;

                try
                {
                    Commando = new SqlCommand("InsertaOrdenEncabezado", Conexion);
                    Commando.CommandType = CommandType.StoredProcedure;

                    Commando.Transaction = Transaccion;

                    Parameter = new SqlParameter("OrdenId", SqlDbType.VarChar);
                    Parameter.Value = OrdenEncabezadoEntidad.OrdenId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("EmpleadoId", SqlDbType.VarChar);
                    Parameter.Value = OrdenEncabezadoEntidad.EmpleadoId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("JefeId", SqlDbType.VarChar);
                    Parameter.Value = OrdenEncabezadoEntidad.JefeId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("ProveedorId", SqlDbType.VarChar);
                    Parameter.Value = OrdenEncabezadoEntidad.ProveedorId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("EstatusId", SqlDbType.VarChar);
                    Parameter.Value = OrdenEncabezadoEntidad.EstatusId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("FechaOrden", SqlDbType.VarChar);
                    Parameter.Value = OrdenEncabezadoEntidad.FechaOrden;
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
            ///     Guarda el detalle de una orden de compra temporal.
            /// </summary>
            /// <param name="Conexion">Conexión actual a la base de datos.</param>
            /// <param name="Transaccion">Transacción actual a la base de datos.</param>
            /// <param name="OrdenDetalleEntidad">Entidad del detalle de una orden de compra.</param>
            public void InsertarProductoOrdenDetalleTemp(SqlConnection Conexion, SqlTransaction Transaccion, OrdenDetalleEntidad OrdenDetalleEntidad)
            {
                SqlCommand Commando;
                SqlParameter Parameter;

                try
                {
                    Commando = new SqlCommand("InsertaProductoOrdenDetalleTemp", Conexion);
                    Commando.CommandType = CommandType.StoredProcedure;

                    Commando.Transaction = Transaccion;

                    Parameter = new SqlParameter("OrdenId", SqlDbType.VarChar);
                    Parameter.Value = OrdenDetalleEntidad.OrdenId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                    Parameter.Value = OrdenDetalleEntidad.PreOrdenId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parameter.Value = OrdenDetalleEntidad.ProductoId;
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
            ///     Guarda el encabezado de una orden de compra temporal.
            /// </summary>
            /// <param name="Conexion">Conexión actual a la base de datos.</param>
            /// <param name="Transaccion">Transacción actual a la base de datos.</param>
            /// <param name="OrdenDetalleEntidad">Entidad del detalle de una orden de compra.</param>
            public void InsertarProductoOrdenEncabezadoTemp(SqlConnection Conexion, SqlTransaction Transaccion, OrdenDetalleEntidad OrdenDetalleEntidad)
            {
                SqlCommand Commando;
                SqlParameter Parameter;

                try
                {
                    Commando = new SqlCommand("InsertaProductoOrdenEncabezadoTemp", Conexion);
                    Commando.CommandType = CommandType.StoredProcedure;

                    Commando.Transaction = Transaccion;

                    Parameter = new SqlParameter("OrdenId", SqlDbType.VarChar);
                    Parameter.Value = OrdenDetalleEntidad.OrdenId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                    Parameter.Value = OrdenDetalleEntidad.PreOrdenId;
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
            /// <param name="OrdenDetalleEntidad"></param>
            /// <param name="CadenaConexion"></param>
            /// <returns></returns>
            public DataSet SeleccionarBusquedaOrdenCompra(OrdenEntidad OrdenDetalleEntidad, string CadenaConexion)
            {
                DataSet Resultado = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("SeleccionarOrdenEncabezadoProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("Clave", SqlDbType.VarChar);
                    Parametro.Value = OrdenDetalleEntidad.Clave;
                    Comando.Parameters.Add(Parametro);

                    //Parametro = new SqlParameter("OrdenId", SqlDbType.VarChar);
                    //Parametro.Value = OrdenDetalleEntidad.OrdenId;
                    //Comando.Parameters.Add(Parametro);

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

            /// <summary>
            ///     Realiza una búsqueda del detalle de una orden de compra temporal.
            /// </summary>
            /// <param name="OrdenDetalleEntidad">Entidad de la orden de compra.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            public DataSet SeleccionarOrdenDetalleTemp(OrdenEntidad OrdenDetalleEntidad, string CadenaConexion)
            {
                DataSet Resultado = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("SeleccionarOrdenDetalleTemp", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("OrdenId", SqlDbType.VarChar);
                    Parametro.Value = OrdenDetalleEntidad.OrdenId;
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
