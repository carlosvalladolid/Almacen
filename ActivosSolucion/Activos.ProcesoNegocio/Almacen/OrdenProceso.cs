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

namespace Activos.ProcesoNegocio.Almacen
{
    public class OrdenProceso : Base
    {
        private int _ErrorId;
        private string _DescripcionError;
        DataSet _ResultadoDatos;
        OrdenEntidad _OrdenEncabezadoEntidad;
        OrdenDetalleEntidad _OrdenDetalleEntidad;

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
        ///     Entidad del proceso.
        /// </summary>
        public OrdenEntidad OrdenEncabezadoEntidad
        {
            get { return _OrdenEncabezadoEntidad; }
            set { _OrdenEncabezadoEntidad = value; }
        }

        /// <summary>
        ///     Entidad del proceso.
        /// </summary>
        public OrdenDetalleEntidad OrdenDetalleEntidad
        {
            get { return _OrdenDetalleEntidad; }
            set { _OrdenDetalleEntidad = value; }
        }

        /// <summary>
        ///     Constructor de la clase
        /// </summary>
        public OrdenProceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _ResultadoDatos = null;
            _OrdenEncabezadoEntidad = new OrdenEntidad();
            _OrdenDetalleEntidad = new OrdenDetalleEntidad();
        }

        #region "Métodos"
            /// <summary>
            ///     
            /// </summary>
            public void GuardarOrden()
            {
                int Contador = 0;
                string CadenaConexion = string.Empty;
                SqlTransaction Transaccion;
                SqlConnection Conexion = new SqlConnection(SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen));

                // Validar información
                if (!ValidarOrden())
                    return;

                Conexion.Open();

                Transaccion = Conexion.BeginTransaction();

                try
                {
                    // Guardar encabezado temporal
                    GuardarOrdenEncabezado(Conexion, Transaccion, _OrdenEncabezadoEntidad);

                    // Si todo salió bien, guardar el detalle temporal
                    if (_ErrorId != 0)
                    {
                        Transaccion.Rollback();
                        Conexion.Close();
                        return;
                    }

                    for (Contador = 0; Contador < _OrdenDetalleEntidad.ProductoIdArray.GetLength(0); Contador++)
                    {
                        GuardarOrdenDetalle(Conexion, Transaccion, _OrdenEncabezadoEntidad.OrdenId, _OrdenDetalleEntidad.ProductoIdArray[Contador, 0], int.Parse(_OrdenDetalleEntidad.ProductoIdArray[Contador, 1]));

                        if (_ErrorId != 0)
                            break;
                    }

                    if (_ErrorId == 0)
                        Transaccion.Commit();
                    else
                        Transaccion.Rollback();

                    Conexion.Close();

                    // Borrar información de la tabla temporal

                }
                catch (Exception Exception)
                {
                    _ErrorId = (int)TextoError.Error.Generico;
                    _DescripcionError = Exception.Message;

                    if (Conexion.State == ConnectionState.Open)
                    {
                        Transaccion.Rollback();
                        Conexion.Close();
                    }
                }
            }

            /// <summary>
            ///     Guarda el detalle de una orden de compra.
            /// </summary>
            /// <param name="Conexion">Conexión actual a la base de datos.</param>
            /// <param name="Transaccion">Transacción actual a la base de datos.</param>
            /// <param name="OrdenEncabezadoEntidad">Entidad del encabezado de una orden de compra.</param>
            private void GuardarOrdenDetalle(SqlConnection Conexion, SqlTransaction Transaccion, string OrdenId, string ProductoId, int Cantidad)
            {
                OrdenAcceso OrdenAcceso = new OrdenAcceso();

                OrdenAcceso.InsertarOrdenDetalle(Conexion, Transaccion, OrdenId, ProductoId, Cantidad);

                _ErrorId = OrdenAcceso.ErrorId;
                _DescripcionError = OrdenAcceso.DescripcionError;
            }

            /// <summary>
            ///     Guarda el encabezado de una orden de compra.
            /// </summary>
            /// <param name="Conexion">Conexión actual a la base de datos.</param>
            /// <param name="Transaccion">Transacción actual a la base de datos.</param>
            /// <param name="OrdenEncabezadoEntidad">Entidad del encabezado de una orden de compra.</param>
            private void GuardarOrdenEncabezado(SqlConnection Conexion, SqlTransaction Transaccion, OrdenEntidad OrdenEncabezadoEntidad)
            {
                OrdenAcceso OrdenAcceso = new OrdenAcceso();

                OrdenAcceso.InsertarOrdenEncabezado(Conexion, Transaccion, OrdenEncabezadoEntidad);

                _ErrorId = OrdenAcceso.ErrorId;
                _DescripcionError = OrdenAcceso.DescripcionError;
            }

            /// <summary>
            ///     Guarda el detalle de una orden de compra temporal.
            /// </summary>
            /// <param name="Conexion">Conexión actual a la base de datos.</param>
            /// <param name="Transaccion">Transacción actual a la base de datos.</param>
            /// <param name="OrdenDetalleEntidad">Entidad del detalle de una orden de compra.</param>
            private void GuardaProductoOrdenDetalleTemp(SqlConnection Conexion, SqlTransaction Transaccion, OrdenDetalleEntidad OrdenDetalleEntidad)
            {
                OrdenAcceso OrdenAcceso = new OrdenAcceso();

                OrdenAcceso.InsertarProductoOrdenDetalleTemp(Conexion, Transaccion, _OrdenDetalleEntidad);

                _ErrorId = OrdenAcceso.ErrorId;
                _DescripcionError = OrdenAcceso.DescripcionError;
            }

            /// <summary>
            ///     Guarda el encabezado de una orden de compra temporal.
            /// </summary>
            /// <param name="Conexion">Conexión actual a la base de datos.</param>
            /// <param name="Transaccion">Transacción actual a la base de datos.</param>
            /// <param name="OrdenDetalleEntidad">Entidad del detalle de una orden de compra.</param>
            private void GuardarProductoOrdenEncabezadoTemp(SqlConnection Conexion, SqlTransaction Transaccion, OrdenDetalleEntidad OrdenDetalleEntidad)
            {
                OrdenAcceso OrdenAcceso = new OrdenAcceso();

                OrdenAcceso.InsertarProductoOrdenEncabezadoTemp(Conexion, Transaccion, _OrdenDetalleEntidad);

                _ErrorId = OrdenAcceso.ErrorId;
                _DescripcionError = OrdenAcceso.DescripcionError;
            }

            /// <summary>
            ///     Guarda la información de una orden temporal.
            /// </summary>
            public void GuardarProductoOrdenTemp()
            {
                string CadenaConexion = string.Empty;
                SqlTransaction Transaccion;
                SqlConnection Conexion = new SqlConnection(SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen));

                // Validar información
                if (!ValidarOrdenTemp())
                    return;

                Conexion.Open();

                Transaccion = Conexion.BeginTransaction();

                try
                {
                    // Guardar encabezado temporal
                    if (_OrdenDetalleEntidad.OrdenId == "")
                    {
                        _OrdenDetalleEntidad.OrdenId = Guid.NewGuid().ToString();

                        GuardarProductoOrdenEncabezadoTemp(Conexion, Transaccion, _OrdenDetalleEntidad);
                    }

                    if (_ErrorId != 0)
                    {
                        Transaccion.Rollback();
                        Conexion.Close();
                        return;
                    }

                    // Si todo salió bien, guardar el detalle temporal
                    GuardaProductoOrdenDetalleTemp(Conexion, Transaccion, _OrdenDetalleEntidad);

                    if (_ErrorId == 0)
                        Transaccion.Commit();
                    else
                        Transaccion.Rollback();

                    Conexion.Close();
                }
                catch (Exception Exception)
                {
                    _ErrorId = (int)TextoError.Error.Generico;
                    _DescripcionError = Exception.Message;

                    if (Conexion.State == ConnectionState.Open)
                    {
                        Transaccion.Rollback();
                        Conexion.Close();
                    }
                }
            }

            /// <summary>
            ///     
            /// </summary>
            public void SeleccionarBusquedaOrdenCompra()
            {
                string CadenaConexion = string.Empty;
                OrdenAcceso OrdenAcceso = new OrdenAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                _ResultadoDatos = OrdenAcceso.SeleccionarBusquedaOrdenCompra(_OrdenEncabezadoEntidad, CadenaConexion);

                _ErrorId = OrdenAcceso.ErrorId;
                _DescripcionError = OrdenAcceso.DescripcionError;
            }

            /// <summary>
            ///     Realiza una búsqueda del detalle de una orden de compra temporal.
            /// </summary>
            public void SeleccionarOrdenDetalleTemp()
            {
                string CadenaConexion = string.Empty;
                OrdenAcceso OrdenAcceso = new OrdenAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                _ResultadoDatos = OrdenAcceso.SeleccionarOrdenDetalleTemp(_OrdenEncabezadoEntidad, CadenaConexion);

                _ErrorId = OrdenAcceso.ErrorId;
                _DescripcionError = OrdenAcceso.DescripcionError;
            }

            /// <summary>
            ///     Busca órdenes de compra que coincidan con los parámetros enviados.
            /// </summary>
            public void SeleccionarOrdenEncabezado()
            {
                string CadenaConexion = string.Empty;
                OrdenAcceso OrdenAcceso = new OrdenAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                _ResultadoDatos = OrdenAcceso.SeleccionarOrdenEncabezado(_OrdenEncabezadoEntidad, CadenaConexion);

                _ErrorId = OrdenAcceso.ErrorId;
                _DescripcionError = OrdenAcceso.DescripcionError;
            }

            /// <summary>
            ///     
            /// </summary>
            public bool ValidarOrden()
            {


                return true;
            }

            /// <summary>
            ///     
            /// </summary>
            /// <returns></returns>
            private bool ValidarOrdenTemp()
            {
                if (_OrdenDetalleEntidad.PreOrdenId == "")
                {
                    _ErrorId = (int)TextoError.Orden.PreOrdenIdVacio;
                    _DescripcionError = TextoError.OrdenConPreOrdenIdVacio;
                    return false;
                }

                return true;
            }


            /// <summary>
            ///     Obtiene la info de la tabla OrdenEncabezado
            /// </summary>
            public ResultadoEntidad SeleccionarOrdenEncabezadoPorOrdenId()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                string CadenaConexion = string.Empty;
                OrdenAcceso OrdenAcceso = new OrdenAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Resultado = OrdenAcceso.SeleccionarOrdenEncabezadoPorOrdenId(this.OrdenEncabezadoEntidad.OrdenId, CadenaConexion);

                return Resultado;
            }

            /// <summary>
            ///     Obtiene la info de la tabla OrdenEncabezado por rangos de fecha
            /// </summary>
            public ResultadoEntidad SeleccionarOrdenEncabezadoPorRangoFechas(string Clave,DateTime FechaInicio,DateTime FechaFin)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                OrdenAcceso OrdenAcceso = new OrdenAcceso();
                string CadenaConexion = String.Empty;
                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Resultado = OrdenAcceso.SeleccionarOrdenEncabezadoBusquedaPorRangoFechas(Clave, FechaInicio, FechaFin, CadenaConexion);

                return Resultado;
            }
        #endregion
    }
}
