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
    public  class AlmacenAcceso:Base
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
        public AlmacenAcceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
        }

        #region "Métodos"
            public ResultadoEntidad ActualizarProducto(AlmacenEntidad AlmacenEntidadObjeto, string CadenaConexion)
            {
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("ActualizarProductoProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidadObjeto.ProductoId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                    Parametro.Value = AlmacenEntidadObjeto.SubFamiliaId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
                    Parametro.Value = AlmacenEntidadObjeto.MarcaId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("UnidadMedidaId", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidadObjeto.UnidadMedidaId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("EstatusId", SqlDbType.Int);
                    Parametro.Value = AlmacenEntidadObjeto.EstatusId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("Clave", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidadObjeto.Clave;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidadObjeto.Descripcion;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("Minimo", SqlDbType.SmallInt);
                    Parametro.Value = AlmacenEntidadObjeto.Minimo;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("Maximo", SqlDbType.SmallInt);
                    Parametro.Value = AlmacenEntidadObjeto.Maximo;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("MaximoPermitido", SqlDbType.SmallInt);
                    Parametro.Value = AlmacenEntidadObjeto.MaximoPermitido;
                    Comando.Parameters.Add(Parametro);

                    Conexion.Open();
                    Comando.ExecuteNonQuery();
                    Conexion.Close();

                    Resultado.ErrorId = (int)ConstantePrograma.Producto.ProductoGuardadoCorrectamente;

                    return Resultado;
                }
                catch (SqlException sqlEx)
                {
                    Resultado.ErrorId = sqlEx.Number;
                    Resultado.DescripcionError = sqlEx.Message;

                    return Resultado;
                }
            }

            /// <summary>
            ///     Borra los registros de existencia de los productos enviados como parámetro.
            /// </summary>
            /// <param name="Conexion">Conexión actual a la base de datos.</param>
            /// <param name="Transaccion">Transacción actual a la base de datos.</param>
            /// <param name="CadenaProductoId">Cadena de caracteres con los identificadores de los productos separados por comas.</param>
            public void EliminarExistenciaProducto(SqlConnection Conexion, SqlTransaction Transaccion, string CadenaProductoId)
            {
                SqlCommand Commando;
                SqlParameter Parameter;

                try
                {
                    Commando = new SqlCommand("EliminarExistenciaProducto", Conexion);
                    Commando.CommandType = CommandType.StoredProcedure;

                    Commando.Transaction = Transaccion;

                    Parameter = new SqlParameter("CadenaProductoId", SqlDbType.VarChar);
                    Parameter.Value = CadenaProductoId;
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
            ///     Elimina la información de los productos enviados como parámetro.
            /// </summary>
            /// <param name="Conexion">Conexión actual a la base de datos.</param>
            /// <param name="Transaccion">Transacción actual a la base de datos.</param>
            /// <param name="CadenaProductoId">Cadena de caracteres con los identificadores de los productos separados por comas.</param>
            public void EliminarProducto(SqlConnection Conexion, SqlTransaction Transaccion, string CadenaProductoId)
            {
                SqlCommand Commando;
                SqlParameter Parameter;

                try
                {
                    Commando = new SqlCommand("EliminarProductoProcedimiento", Conexion);
                    Commando.CommandType = CommandType.StoredProcedure;

                    Commando.Transaction = Transaccion;

                    Parameter = new SqlParameter("CadenaProductoId", SqlDbType.VarChar);
                    Parameter.Value = CadenaProductoId;
                    Commando.Parameters.Add(Parameter);

                    Commando.ExecuteNonQuery();
                }
                catch (SqlException Exception)
                {
                    _ErrorId = Exception.Number;
                    _DescripcionError = Exception.Message;
                }
            }

            public ResultadoEntidad InsertarProducto(SqlConnection Conexion, SqlTransaction Transaccion, AlmacenEntidad AlmacenEntidad)
            {
                SqlCommand Comando;
                SqlParameter Parametro;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("InsertarProductoProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Comando.Transaction = Transaccion;

                    Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidad.ProductoId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                    Parametro.Value = AlmacenEntidad.SubFamiliaId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
                    Parametro.Value = AlmacenEntidad.MarcaId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("UnidadMedidaId", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidad.UnidadMedidaId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("EstatusId", SqlDbType.Int);
                    Parametro.Value = AlmacenEntidad.EstatusId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("Clave", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidad.Clave;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidad.Descripcion;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("Minimo", SqlDbType.SmallInt);
                    Parametro.Value = AlmacenEntidad.Minimo;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("Maximo", SqlDbType.SmallInt);
                    Parametro.Value = AlmacenEntidad.Maximo;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("MaximoPermitido", SqlDbType.SmallInt);
                    Parametro.Value = AlmacenEntidad.MaximoPermitido;
                    Comando.Parameters.Add(Parametro);

                    Comando.ExecuteNonQuery();

                    Resultado.ErrorId = (int)ConstantePrograma.Producto.ProductoGuardadoCorrectamente;

                    return Resultado;
                }
                catch (SqlException sqlEx)
                {
                    Resultado.ErrorId = sqlEx.Number;
                    Resultado.DescripcionError = sqlEx.Message;

                    return Resultado;
                }
            }

            public void InsertarProductoExistencia(SqlConnection Conexion, SqlTransaction Transaccion, AlmacenEntidad AlmacenEntidad)
            {
                SqlCommand Comando;
                SqlParameter Parametro;

                try
                {
                    Comando = new SqlCommand("InsertarProductoExistencia", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Comando.Transaction = Transaccion;

                    Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidad.ProductoId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("Existencia", SqlDbType.Int);
                    Parametro.Value = AlmacenEntidad.ExistenciaInicial;
                    Comando.Parameters.Add(Parametro);

                    Comando.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    _ErrorId = sqlEx.Number;
                    _DescripcionError = sqlEx.Message;
                }
            }

            public ResultadoEntidad SeleccionarProducto(AlmacenEntidad AlmacenEntidadObjeto, string CadenaConexion)
            {
                DataSet ResultadoDatos = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("SeleccionarProductoProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("Clave", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidadObjeto.Clave;
                    Comando.Parameters.Add(Parametro);
                    
                    Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidadObjeto.Descripcion;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidadObjeto.ProductoId;
                    Comando.Parameters.Add(Parametro);    

                    Parametro = new SqlParameter("BusquedaRapida", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidadObjeto.BusquedaRapida;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("BuscarNombre", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidadObjeto.BuscarNombre;
                    Comando.Parameters.Add(Parametro);

                    Adaptador = new SqlDataAdapter(Comando);
                    ResultadoDatos = new DataSet();

                    Conexion.Open();
                    Adaptador.Fill(ResultadoDatos);
                    Conexion.Close();

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
            ///     Busca la existencia de un producto.
            /// </summary>
            /// <param name="ProductoEntidad">Entidad del producto.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            public DataSet SeleccionarProductoExistencia(string ProductoId, string CadenaConexion)
            {
                DataSet Resultado = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("SeleccionarProductoExistencia", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parametro.Value = ProductoId;
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
            ///     Busca la cantidad máxima permitida para la requisición de un producto.
            /// </summary>
            /// <param name="ProductoId">Identificador del producto.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            public DataSet SeleccionarProductoMaximoPermitido(string ProductoId, string CadenaConexion)
            {
                DataSet Resultado = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("SeleccionarProductoMaximoPermitido", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parametro.Value = ProductoId;
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

            public ResultadoEntidad SeleccionarProductoParaEditar(AlmacenEntidad AlmacenEntidadObjeto, string CadenaConexion)
            {
                DataSet ResultadoDatos = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("SeleccionarProductoparaEditarProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parametro.Value = AlmacenEntidadObjeto.ProductoId;
                    Comando.Parameters.Add(Parametro);
               
                    Adaptador = new SqlDataAdapter(Comando);
                    ResultadoDatos = new DataSet();

                    Conexion.Open();
                    Adaptador.Fill(ResultadoDatos);
                    Conexion.Close();

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

            public DataSet SeleccionarRelacionesProducto(string CadenaProductoId, string CadenaConexion)
            {
                DataSet ResultadoDatos = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("SeleccionarRelacionesProducto", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("CadenaProductoId", SqlDbType.VarChar);
                    Parametro.Value = CadenaProductoId;
                    Comando.Parameters.Add(Parametro);

                    Adaptador = new SqlDataAdapter(Comando);
                    ResultadoDatos = new DataSet();

                    Conexion.Open();
                    Adaptador.Fill(ResultadoDatos);
                    Conexion.Close();

                    return ResultadoDatos;
                }
                catch (SqlException Excepcion)
                {
                    _ErrorId = Excepcion.Number;
                    _DescripcionError = Excepcion.Message;

                    return ResultadoDatos;
                }
            }

            public ResultadoEntidad SeleccionarReporteExistenciaProducto(AlmacenEntidad AlmacenEntidadObjeto, string CadenaConexion)
            {
                DataSet ResultadoDatos = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                //SqlParameter Parametro;
                SqlDataAdapter Adaptador;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("SeleccionarProductoenExistencia", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;                               

                    Adaptador = new SqlDataAdapter(Comando);
                    ResultadoDatos = new DataSet();

                    Conexion.Open();
                    Adaptador.Fill(ResultadoDatos);
                    Conexion.Close();

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
        #endregion
    }
}
