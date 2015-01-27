using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Almacen;
using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
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
        public void InsertarOrdenDetalle(SqlConnection Conexion, SqlTransaction Transaccion, string OrdenId, string ProductoId, int Cantidad)
            {
                SqlCommand Commando;
                SqlParameter Parameter;

                try
                {
                    Commando = new SqlCommand("InsertaOrdenDetalle", Conexion);
                    Commando.CommandType = CommandType.StoredProcedure;

                    Commando.Transaction = Transaccion;

                    Parameter = new SqlParameter("OrdenId", SqlDbType.VarChar);
                    Parameter.Value = OrdenId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parameter.Value = ProductoId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("Cantidad", SqlDbType.VarChar);
                    Parameter.Value = Cantidad;
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

                    Parameter = new SqlParameter("SesionId", SqlDbType.VarChar);
                    Parameter.Value = OrdenDetalleEntidad.SesionId;
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

            /// <summary>
            ///     Busca órdenes de compra que coincidan con los parámetros enviados.
            /// </summary>
            /// <param name="OrdenEncabezadoEntidad">Entidad del encabezado de una orden de compra.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            public DataSet SeleccionarOrdenEncabezado(OrdenEntidad OrdenEncabezadoEntidad, string CadenaConexion)
            {
                DataSet Resultado = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("SeleccionarOrdenEncabezado", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("OrdenId", SqlDbType.VarChar);
                    Parametro.Value = OrdenEncabezadoEntidad.OrdenId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                    Parametro.Value = OrdenEncabezadoEntidad.PreOrdenId;
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




            /// <summary>
            ///     Obtiene la información del encabezado de la orden con el ID de la Orden.
            /// </summary>
            /// <param name="PreOrdenEntidad">Entidad de la orden.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            public ResultadoEntidad SeleccionarOrdenEncabezadoPorOrdenId(string OrdenId, string CadenaConexion)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {

                    Comando = new SqlCommand("SeleccionarOrdenEncabezadoPorOrdenId", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("@OrdenId", SqlDbType.VarChar);
                    Parametro.Value = OrdenId;
                    Comando.Parameters.Add(Parametro);

                    Adaptador = new SqlDataAdapter(Comando);

                    Conexion.Open();
                    Adaptador.Fill(Resultado.ResultadoDatos);
                    Conexion.Close();

                    return Resultado;
                }
                catch (SqlException Excepcion)
                {
                    Resultado.ErrorId = Excepcion.Number;
                    Resultado.DescripcionError = Excepcion.Message;

                    return Resultado;
                }
            }

            /// <summary>
            ///    Hace una busqueda de las Ordenes en el sistema 
            /// </summary>
            /// <param name="Clave">Clave de la Orden.</param>
            /// /// <param name="FechaInicio">Fecha de inicio del rango.</param>
            /// /// <param name="FechaFin">Fecha de fin del rango.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            public ResultadoEntidad SeleccionarOrdenEncabezadoBusquedaPorRangoFechas(string Clave, DateTime FechaInicio, DateTime FechaFin, string CadenaConexion)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {

                    Comando = new SqlCommand("SeleccionarOrdenEncabezadoPorRangoDeFechas", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("@Clave", SqlDbType.VarChar);
                    Parametro.Value = Clave;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("@FechaInicio", SqlDbType.DateTime);
                    Parametro.Value = FechaInicio.ToString(ConstantePrograma.SqlServerFormatoFecha);
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("@FechaFin", SqlDbType.DateTime);
                    Parametro.Value = FechaFin.ToString(ConstantePrograma.SqlServerFormatoFecha);
                    Comando.Parameters.Add(Parametro);

                    Adaptador = new SqlDataAdapter(Comando);

                    Conexion.Open();
                    Adaptador.Fill(Resultado.ResultadoDatos);
                    Conexion.Close();

                    return Resultado;
                }
                catch (SqlException Excepcion)
                {
                    Resultado.ErrorId = Excepcion.Number;
                    Resultado.DescripcionError = Excepcion.Message;

                    return Resultado;
                }
            }



            /// <summary>
            ///    Elimina una un producto de la orden de compra de la tabla OrdenDetalleTemp y lo agrega a la tabla de PreOrdenDetalletemp
            /// </summary>
            /// <param name="Clave">Clave de la Orden.</param>
            /// /// <param name="FechaInicio">Fecha de inicio del rango.</param>
            /// /// <param name="FechaFin">Fecha de fin del rango.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            public ResultadoEntidad EliminarProductoOrdenDetalleTemp(string CadenaConexion,OrdenDetalleEntidad OrdenDetalleEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("EliminarProductoOrdenDetalleTemp", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("OrdenId", SqlDbType.VarChar);
                    Parametro.Value = OrdenDetalleEntidad.OrdenId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("@PreOrdenId", SqlDbType.VarChar);
                    Parametro.Value = OrdenDetalleEntidad.PreOrdenId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("@ProductoId", SqlDbType.VarChar);
                    Parametro.Value = OrdenDetalleEntidad.ProductoId;
                    Comando.Parameters.Add(Parametro);

                    Adaptador = new SqlDataAdapter(Comando);

                    Conexion.Open();
                    Adaptador.Fill(Resultado.ResultadoDatos);
                    Conexion.Close();

                    return Resultado;
                }
                catch (SqlException Excepcion)
                {
                    Resultado.ErrorId = Excepcion.Number;
                    Resultado.DescripcionError = Excepcion.Message;

                    return Resultado;
                }
            }

        #endregion
    }
}
