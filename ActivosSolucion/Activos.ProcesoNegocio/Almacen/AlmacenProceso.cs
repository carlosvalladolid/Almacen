using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

//using Activos.AccesoDatos.Catalogo;
using Activos.AccesoDatos.Almacen;
using Activos.ProcesoNegocio.Almacen;
using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Almacen;

namespace Activos.ProcesoNegocio.Almacen
{
    public class AlmacenProceso:Base
    {
        private int _ErrorId;
        private string _DescripcionError;
        private string _OrigenError;
        DataSet _ResultadoDatos;
        AlmacenEntidad _ProductoEntidad;

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
        ///     Lugar donde se originó el error.
        /// </summary>
        public string OrigenError
        {
            get { return _OrigenError; }
        }

        /// <summary>
        ///     DataSet con el resultado de la base de datos.
        /// </summary>
        public DataSet ResultadoDatos
        {
            get { return _ResultadoDatos; }
        }

        /// <summary>
        ///     Entidad del proceso.
        /// </summary>
        public AlmacenEntidad ProductoEntidad
        {
            get { return _ProductoEntidad; }
            set { _ProductoEntidad = value; }
        }

        /// <summary>
        ///     Constructor de la clase
        /// </summary>
        public AlmacenProceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _OrigenError = string.Empty;
            _ResultadoDatos = null;
            _ProductoEntidad = new AlmacenEntidad();
        }

        #region "Métodos"
            public bool BuscarProductoDuplicada(AlmacenEntidad AlmacenObjetoEntidad)
            {
                bool ExisteProducto = false;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenEntidad BuscarAlmacenObjetoEntidad = new AlmacenEntidad();

                BuscarAlmacenObjetoEntidad.BuscarNombre = Comparar.EstandarizarCadena(AlmacenObjetoEntidad.Descripcion);

                Resultado = SeleccionarProducto(BuscarAlmacenObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    if (string.Format(Resultado.ResultadoDatos.Tables[0].Rows[0]["Clave"].ToString()) != AlmacenObjetoEntidad.Clave)
                    ExisteProducto = true;
                    else
                    ExisteProducto = false;
                }

                return ExisteProducto;
            }

            private void EliminarExistenciaProducto(SqlConnection Conexion, SqlTransaction Transaccion, string CadenaProductoId)
            {
                AlmacenAcceso AlmacenAcceso = new AlmacenAcceso();

                AlmacenAcceso.EliminarExistenciaProducto(Conexion, Transaccion, CadenaProductoId);

                _ErrorId = AlmacenAcceso.ErrorId;
                _DescripcionError = AlmacenAcceso.DescripcionError;
            }

            private void EliminarProducto(SqlConnection Conexion, SqlTransaction Transaccion, string CadenaProductoId)
            {
                AlmacenAcceso AlmacenAccesoObjeto = new AlmacenAcceso();

                AlmacenAccesoObjeto.EliminarProducto(Conexion, Transaccion, CadenaProductoId);

                _ErrorId = AlmacenAccesoObjeto.ErrorId;
                _DescripcionError = AlmacenAccesoObjeto.DescripcionError;
            }

            /// <summary>
            ///     Elimina la información de los productos enviados como parámetro.
            /// </summary>
            /// <param name="CadenaProductoId">Cadena de caracteres con los identificadores de los productos separados por comas.</param>
            public void EliminarProducto(string CadenaProductoId)
            {
                SqlConnection Conexion = new SqlConnection(SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen));
                SqlTransaction Transaccion;

                // Validar que las marcas no contengan información relacionada con otras tablas
                if (TieneRelacionesElProducto(CadenaProductoId))
                {
                    _ErrorId = (int)ConstantePrograma.Producto.TieneInformacionRelacionada;
                    _DescripcionError = TextoError.ProductoTieneRegistrosRelacionados.Replace("{0}", _OrigenError);
                    return;
                }

                Conexion.Open();
                Transaccion = Conexion.BeginTransaction();

                try
                {
                    // Si se pasaron las validaciones, hay que borrar la existencia del producto
                    EliminarExistenciaProducto(Conexion, Transaccion, CadenaProductoId);

                    if (_ErrorId != 0)
                    {
                        Transaccion.Rollback();
                        Conexion.Close();
                        return;
                    }

                    // Ahora se deben borrar los Productos seleccionadas
                    EliminarProducto(Conexion, Transaccion, CadenaProductoId);

                    if (_ErrorId == 0)
                        Transaccion.Commit();
                    else
                        Transaccion.Rollback();

                    Conexion.Close();
                }
                catch
                {
                    if (Conexion.State == ConnectionState.Open)
                    {
                        Transaccion.Rollback();
                        Conexion.Close();
                    }
                }
            }

            public ResultadoEntidad GuardarProducto(AlmacenEntidad AlmacenObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
                AlmacenAcceso AlmacenAcceso = new AlmacenAcceso();
                SqlTransaction Transaccion;
                SqlConnection Conexion = new SqlConnection(SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen));

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                // Si es una actualización se lleva a cabo sin transacción
                if (AlmacenObjetoEntidad.ProductoId != "")
                {
                    Resultado = AlmacenAcceso.ActualizarProducto(AlmacenObjetoEntidad, CadenaConexion);
                    return Resultado;
                }

                if (BuscarProductoDuplicada(AlmacenObjetoEntidad))
                {
                    Resultado.ErrorId = (int)ConstantePrograma.Producto.ProductoTieneRegistroDuplicado;
                    Resultado.DescripcionError = TextoError.ProductoConNombreDuplicado;
                    return Resultado;
                }

                // Si es un producto nuevo hay que manejar transacción
                Conexion.Open();

                Transaccion = Conexion.BeginTransaction();

                try
                {
                    AlmacenObjetoEntidad.ProductoId = Guid.NewGuid().ToString();


                    // Primero se guarda la información del producto
                    Resultado = AlmacenAcceso.InsertarProducto(Conexion, Transaccion, AlmacenObjetoEntidad);

                    if (Resultado.ErrorId != (int)ConstantePrograma.Producto.ProductoGuardadoCorrectamente)
                    {
                        Transaccion.Rollback();
                        Conexion.Close();
                        return Resultado;
                    }

                    // Luego la información de la existencia
                    AlmacenAcceso.InsertarProductoExistencia(Conexion, Transaccion, AlmacenObjetoEntidad);

                    if (AlmacenAcceso.ErrorId == 0)
                        Transaccion.Commit();
                    else
                    {
                        Transaccion.Rollback();

                        Resultado.ErrorId = AlmacenAcceso.ErrorId;
                        Resultado.DescripcionError = AlmacenAcceso.DescripcionError;
                    }

                    Conexion.Close();

                    return Resultado;
                }
                catch (Exception Exception)
                {
                    Resultado.ErrorId = (int)TextoError.Error.Generico;
                    Resultado.DescripcionError = Exception.Message;

                    if (Conexion.State == ConnectionState.Open)
                    {
                        Transaccion.Rollback();
                        Conexion.Close();
                    }

                    return Resultado;
                }
            }
          
            public ResultadoEntidad SeleccionarProducto(AlmacenEntidad AlmacenObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenAcceso AlmacenAccesoObjeto = new AlmacenAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Resultado = AlmacenAccesoObjeto.SeleccionarProducto(AlmacenObjetoEntidad, CadenaConexion);

                return Resultado;
            }

            public ResultadoEntidad SeleccionarExistenciaProducto(AlmacenEntidad AlmacenObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenAcceso AlmacenAccesoObjeto = new AlmacenAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Resultado = AlmacenAccesoObjeto.SeleccionarReporteExistenciaProducto(AlmacenObjetoEntidad, CadenaConexion);

                return Resultado;
            }


            public ResultadoEntidad SeleccionarConsumoPorDireccion(AlmacenEntidad AlmacenObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenAcceso AlmacenAccesoObjeto = new AlmacenAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Resultado = AlmacenAccesoObjeto.SeleccionarReporteConsumoPorDireccion(AlmacenObjetoEntidad, CadenaConexion);

                return Resultado;
            }


            public ResultadoEntidad SeleccionarProductoparaEditar(AlmacenEntidad AlmacenObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenAcceso AlmacenAccesoObjeto = new AlmacenAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Resultado = AlmacenAccesoObjeto.SeleccionarProductoParaEditar(AlmacenObjetoEntidad, CadenaConexion);

                return Resultado;
            }

            /// <summary>
            ///     Busca la existencia de un producto.
            /// </summary>
            public int SeleccionarProductoExistencia(string ProductoId)
            {
                string CadenaConexion = string.Empty;
                AlmacenAcceso ProductoAcceso = new AlmacenAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                _ResultadoDatos = ProductoAcceso.SeleccionarProductoExistencia(ProductoId, CadenaConexion);

                if (ProductoAcceso.ErrorId != 0)
                {
                    _ErrorId = ProductoAcceso.ErrorId;
                    _DescripcionError = ProductoAcceso.DescripcionError;

                    return 0;
                }

                if (_ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    _ErrorId = (int)TextoError.Requisicion.ProductoSinRegistroDeExistencia;
                    _DescripcionError = TextoError.ProductoSinRegistroDeExistencia;

                    return 0;
                }

                return int.Parse(_ResultadoDatos.Tables[0].Rows[0]["Existencia"].ToString());
            }

            /// <summary>
            ///     Busca la cantidad máxima permitida para la requisición de un producto.
            /// </summary>
            /// <returns>Máximo permitido.</returns>
            public int SeleccionarProductoMaximoPermitido(string ProductoId)
            {
                string CadenaConexion = string.Empty;
                AlmacenAcceso ProductoAcceso = new AlmacenAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                _ResultadoDatos = ProductoAcceso.SeleccionarProductoMaximoPermitido(ProductoId, CadenaConexion);

                if (ProductoAcceso.ErrorId != 0)
                {
                    _ErrorId = ProductoAcceso.ErrorId;
                    _DescripcionError = ProductoAcceso.DescripcionError;

                    return 0;
                }

                if (_ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    _ErrorId = (int)TextoError.Requisicion.ProductoNoEncontrado;
                    _DescripcionError = TextoError.ProductoNoEncontrado;

                    return 0;
                }

                return int.Parse(_ResultadoDatos.Tables[0].Rows[0]["Maximo"].ToString());
            }

            protected bool TieneRelacionesElProducto(string CadenaProductoId)
            {
                DataSet ProductoResultado = new DataSet();
                AlmacenAcceso AlmacenAcceso = new AlmacenAcceso();

                ProductoResultado = AlmacenAcceso.SeleccionarRelacionesProducto(CadenaProductoId, SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen));

                if (AlmacenAcceso.ErrorId != 0)
                {
                    _ErrorId = AlmacenAcceso.ErrorId;
                    _DescripcionError = AlmacenAcceso.DescripcionError;

                    return false;
                }

                if (ProductoResultado.Tables.Count == 0)
                    return false;

                if (ProductoResultado.Tables[0].Rows.Count == 0)
                    return false;

                _OrigenError = ProductoResultado.Tables[0].Rows[0]["TablaRelacionada"].ToString();

                return true;
            }
        #endregion
    }
}
