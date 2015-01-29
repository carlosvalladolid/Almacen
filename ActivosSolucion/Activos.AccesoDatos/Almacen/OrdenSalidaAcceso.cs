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
        public void InsertarOrdenSalidaTemp(SqlConnection Conexion, SqlTransaction Transaccion, OrdenSalidaDetalleEntidad OrdenSalidaDetalleEntidad)
            {
                SqlCommand Commando;
                SqlParameter Parameter;

                try
                {
                    Commando = new SqlCommand("InsertarOrdenSalidaTemp", Conexion);
                    Commando.CommandType = CommandType.StoredProcedure;

                    Commando.Transaction = Transaccion;

                    Parameter = new SqlParameter("OrdenSalidaId", SqlDbType.VarChar);
                    Parameter.Value = OrdenSalidaDetalleEntidad.OrdenSalidaId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("RequisicionId", SqlDbType.VarChar);
                    Parameter.Value = OrdenSalidaDetalleEntidad.RequisicionId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("EstatusId", SqlDbType.VarChar);
                    Parameter.Value = OrdenSalidaDetalleEntidad.EstatusId;
                    Commando.Parameters.Add(Parameter);

                    Parameter = new SqlParameter("UsuarioInserto", SqlDbType.VarChar);
                    Parameter.Value = OrdenSalidaDetalleEntidad.UsuarioIdInserto;
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
        ///     Guarda el detalle de una orden de salida temporal.
        /// </summary>
        /// <param name="Conexion">Conexión actual a la base de datos.</param>
        /// <param name="Transaccion">Transacción actual a la base de datos.</param>
        /// <param name="OrdenDetalleEntidad">Entidad del detalle de una orden de compra.</param>
        public void InsertarOrdenSalidaDetalleTemp(SqlConnection Conexion, SqlTransaction Transaccion, OrdenSalidaDetalleEntidad OrdenSalidaDetalleEntidad)
        {
            SqlCommand Commando;
            SqlParameter Parameter;

            try
            {
                Commando = new SqlCommand("InsertaOrdenSalidaDetalleTemp", Conexion);
                Commando.CommandType = CommandType.StoredProcedure;

                Commando.Transaction = Transaccion;

                Parameter = new SqlParameter("OrdenSalidaId", SqlDbType.VarChar);
                Parameter.Value = OrdenSalidaDetalleEntidad.OrdenSalidaId;
                Commando.Parameters.Add(Parameter);

                Parameter = new SqlParameter("ProductoId", SqlDbType.VarChar);
                Parameter.Value = OrdenSalidaDetalleEntidad.ProductoId;
                Commando.Parameters.Add(Parameter);

                Parameter = new SqlParameter("Cantidad", SqlDbType.VarChar);
                Parameter.Value = OrdenSalidaDetalleEntidad.Cantidad;
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
        ///     Guarda el detalle de la orden de salida en la tabla definitiva
        /// </summary>
        /// <param name="Conexion">Conexión actual a la base de datos.</param>
        /// <param name="Transaccion">Transacción actual a la base de datos.</param>
        /// <param name="OrdenDetalleEntidad">Entidad del detalle de una orden de compra.</param>
        public void InsertarOrdenSalidaDetalle(SqlConnection Conexion, SqlTransaction Transaccion, OrdenSalidaDetalleEntidad OrdenSalidaDetalleEntidad)
        {
            SqlCommand Commando;
            SqlParameter Parameter;

            try
            {
                Commando = new SqlCommand("InsertaOrdenSalidaDetalle", Conexion);
                Commando.CommandType = CommandType.StoredProcedure;

                Commando.Transaction = Transaccion;

                Parameter = new SqlParameter("OrdenSalidaId", SqlDbType.VarChar);
                Parameter.Value = OrdenSalidaDetalleEntidad.OrdenSalidaId;
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
        ///     Guarda el encabezado de la orden de salida en la tabla definitiva
        /// </summary>
        /// <param name="Conexion">Conexión actual a la base de datos.</param>
        /// <param name="Transaccion">Transacción actual a la base de datos.</param>
        /// <param name="OrdenDetalleEntidad">Entidad del detalle de una orden de compra.</param>
        public string InsertarOrdenSalidaEncabezado(SqlConnection Conexion, SqlTransaction Transaccion, OrdenSalidaDetalleEntidad OrdenSalidaDetalleEntidad)
        {
            SqlCommand Commando;
            SqlParameter Parameter;
            int Clave = 0;
            try
            {
                Commando = new SqlCommand("InsertarOrdenSalidaEncabezado", Conexion);
                Commando.CommandType = CommandType.StoredProcedure;

                Commando.Transaction = Transaccion;

                Parameter = new SqlParameter("OrdenSalidaId", SqlDbType.VarChar);
                Parameter.Value = OrdenSalidaDetalleEntidad.OrdenSalidaId;
                Commando.Parameters.Add(Parameter);

                Clave = (int)Commando.ExecuteScalar();

                return Clave.ToString();
            }
            catch (SqlException Exception)
            {
                _ErrorId = Exception.Number;
                _DescripcionError = Exception.Message;
                return Clave.ToString();
            }
        }



        /// <summary>
        ///     Selecciona el detalle temporal de las ordenes de salida
        /// </summary>
        /// <param name="Conexion">Conexión actual a la base de datos.</param>
        /// <param name="Transaccion">Transacción actual a la base de datos.</param>
        /// <param name="OrdenDetalleEntidad">Entidad del detalle de una orden de compra.</param>
        public DataSet SeleccionarOrdenSalidaDetalleTemp(SqlConnection Conexion, string OrdenSalidaId)
        {
            DataSet Resultado = new DataSet();
            SqlCommand Commando;
            SqlParameter Parameter;

            try
            {
                Commando = new SqlCommand("SeleccionarOrdenSalidaDetalleTemp", Conexion);
                Commando.CommandType = CommandType.StoredProcedure;

       
                Parameter = new SqlParameter("OrdenSalidaId", SqlDbType.VarChar);
                Parameter.Value = OrdenSalidaId;
                Commando.Parameters.Add(Parameter);

                SqlDataAdapter Adaptador = new SqlDataAdapter(Commando);
                Adaptador.Fill(Resultado);
                Conexion.Close();

                return Resultado;
            }
            catch (SqlException Exception)
            {
                _ErrorId = Exception.Number;
                _DescripcionError = Exception.Message;

                return Resultado;
            }
        }

        public void BorrarOrdenSalidaDetalleTemp(SqlConnection Conexion, SqlTransaction Transaccion, OrdenSalidaDetalleEntidad OrdenSalidaDetalleEntidad)
        {
            SqlCommand Commando;
            SqlParameter Parameter;

            try
            {
                Commando = new SqlCommand("EliminarOrdenSalidaDetalleTemp", Conexion);
                Commando.CommandType = CommandType.StoredProcedure;

                Commando.Transaction = Transaccion;

                Parameter = new SqlParameter("OrdenSalidaId", SqlDbType.VarChar);
                Parameter.Value = OrdenSalidaDetalleEntidad.OrdenSalidaId;
                Commando.Parameters.Add(Parameter);

                Parameter = new SqlParameter("ProductoId", SqlDbType.VarChar);
                Parameter.Value = OrdenSalidaDetalleEntidad.ProductoId;
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
