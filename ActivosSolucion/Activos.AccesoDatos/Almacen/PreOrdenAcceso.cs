using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.Comun.Constante;

namespace Activos.AccesoDatos.Almacen
{
   public class PreOrdenAcceso:Base
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
        public PreOrdenAcceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
        }

        #region "Métodos"
            public ResultadoEntidad InsertarPreOrdenDetalle(SqlConnection Conexion, SqlTransaction Transaccion, PreOrdenEntidad PreOrdenEntidadObjeto)
            {
                SqlCommand Comando;
                SqlParameter Parametro;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("InsertarPreOrdenDetalleProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Comando.Transaction = Transaccion;

                    Parametro = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                    Parametro.Value = PreOrdenEntidadObjeto.PreOrdenId;
                    Comando.Parameters.Add(Parametro);

                    Comando.ExecuteNonQuery();
                    Resultado.ErrorId = (int)ConstantePrograma.PreOrden.PreOrdenGuardadoCorrectamente;

                    return Resultado;
                }
                catch (SqlException sqlEx)
                {
                    Resultado.ErrorId = sqlEx.Number;
                    Resultado.DescripcionError = sqlEx.Message;

                    return Resultado;
                }
            }

            public ResultadoEntidad InsertarPreOrdenEncabezado(SqlConnection Conexion, SqlTransaction Transaccion, PreOrdenEntidad PreOrdenEntidadObjeto)
            {
                SqlCommand Comando;
                SqlParameter Parametro;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("InsertarPreOrdenEncabezadoProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Comando.Transaction = Transaccion;

                    Parametro = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                    Parametro.Value = PreOrdenEntidadObjeto.PreOrdenId;
                    Comando.Parameters.Add(Parametro);

                    Comando.ExecuteNonQuery();

                    Resultado.ErrorId = (int)ConstantePrograma.PreOrden.PreOrdenGuardadoCorrectamente;

                    return Resultado;
                }
                catch (SqlException sqlEx)
                {
                    Resultado.ErrorId = sqlEx.Number;
                    Resultado.DescripcionError = sqlEx.Message;

                    return Resultado;
                }
            }

            public DataSet SeleccionarClaveProductoPreOrden(PreOrdenEntidad PreOrdenEntidad, string CadenaConexion)
            {
                DataSet Resultado = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("SeleccioarClavePreOrdenProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("Clave", SqlDbType.VarChar);
                    Parametro.Value = PreOrdenEntidad.Clave;
                    Comando.Parameters.Add(Parametro);

                    //Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                    //Parametro.Value = PreOrdenEntidad.SesionId;
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
            ///     Realiza una búsqueda del detalle de una preorden.
            /// </summary>
            /// <param name="PreOrdenEntidad">Entidad de una preorden.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            public DataSet SeleccionarPreOrdenDetalle(PreOrdenEntidad PreOrdenEntidad, string CadenaConexion)
            {
                DataSet Resultado = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("SeleccionarPreOrdenDetalleProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                    Parametro.Value = PreOrdenEntidad.PreOrdenId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parametro.Value = PreOrdenEntidad.ProductoId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("Clave", SqlDbType.VarChar);
                    Parametro.Value = PreOrdenEntidad.Clave;
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

            public ResultadoEntidad SeleccionarPreOrdenDetalle(SqlConnection Conexion, SqlTransaction Transaccion, TemporalPreOrdenEntidad TempPreOrdenDetalleObjetoEntidad)
            {
                DataSet ResultadoDatos = new DataSet();
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("SeleccionarPreOrdenDetalleProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Comando.Transaction = Transaccion;

                    Parametro = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                    Parametro.Value = TempPreOrdenDetalleObjetoEntidad.PreOrdenId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parametro.Value = TempPreOrdenDetalleObjetoEntidad.ProductoId;
                    Comando.Parameters.Add(Parametro);

                    Adaptador = new SqlDataAdapter(Comando);
                    ResultadoDatos = new DataSet();

                    Adaptador.Fill(ResultadoDatos);

                    Resultado.ResultadoDatos = ResultadoDatos;

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
            ///     Realiza una búsqueda del detalle de una preorden que no cuente con una orden de compra tomando en cuenta las tablas temporales.
            /// </summary>
            /// <param name="PreOrdenEntidad">Entidad de la preorden.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            public DataSet SeleccionarPreOrdenDetalleSinOrden(PreOrdenEntidad PreOrdenEntidad, string CadenaConexion)
            {
                DataSet Resultado = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("SeleccionarPreOrdenDetalleSinOrden", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("Clave", SqlDbType.VarChar);
                    Parametro.Value = PreOrdenEntidad.Clave;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                    Parametro.Value = PreOrdenEntidad.SesionId;
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
            ///     Busca los productos de una preorden que todavía no es relacionada a una orden de compra temporal.
            /// </summary>
            /// <param name="PreOrdenEntidad">Entidad de la preorden.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            public DataSet SeleccionarPreOrdenSinOrden(PreOrdenEntidad PreOrdenEntidad, string CadenaConexion)
            {
                DataSet Resultado = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("SeleccionarPreOrdenSinOrden", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("Clave", SqlDbType.VarChar);
                    Parametro.Value = PreOrdenEntidad.Clave;
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
