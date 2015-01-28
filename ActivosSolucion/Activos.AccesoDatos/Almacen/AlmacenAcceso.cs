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

            public ResultadoEntidad EliminarProducto(string CadenaProductoId, string CadenaConexion)
            {
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("EliminarProductoProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("CadenaProductoId", SqlDbType.VarChar);
                    Parametro.Value = CadenaProductoId;
                    Comando.Parameters.Add(Parametro);

                    Conexion.Open();
                    Comando.ExecuteNonQuery();
                    Conexion.Close();

                    Resultado.ErrorId = (int)ConstantePrograma.Producto.EliminadoExitosamente;

                    return Resultado;
                }
                catch (SqlException sqlEx)
                {
                    Resultado.ErrorId = sqlEx.Number;
                    Resultado.DescripcionError = sqlEx.Message;

                    return Resultado;
                }
            }

            public ResultadoEntidad InsertarProducto(AlmacenEntidad AlmacenEntidadObjeto, string CadenaConexion)
            {
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("InsertarProductoProcedimiento", Conexion);
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

            public ResultadoEntidad SeleccionarProductoparaEditar(AlmacenEntidad AlmacenEntidadObjeto, string CadenaConexion)
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
