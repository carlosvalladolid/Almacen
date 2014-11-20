using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Almacen;
using Activos.Comun.Constante;
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
            public void GuardaProductoOrdenTemp()
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
                    if (_OrdenDetalleEntidad.OrdenId == "")
                    {
                        _OrdenDetalleEntidad.OrdenId = Guid.NewGuid().ToString();

                        GuardaProductoOrdenEncabezadoTemp(Conexion, Transaccion, _OrdenDetalleEntidad);
                    }

                    // Guardar encabezado temporal
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

                    return;
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
            /// <param name="Conexion"></param>
            /// <param name="Transaccion"></param>
            /// <param name="PreOrdenEntidad"></param>
            private void GuardaProductoOrdenDetalleTemp(SqlConnection Conexion, SqlTransaction Transaccion, OrdenDetalleEntidad OrdenDetalleEntidad)
            {
                OrdenAcceso OrdenAcceso = new OrdenAcceso();

                OrdenAcceso.InsertaProductoOrdenDetalleTemp(Conexion, Transaccion, _OrdenDetalleEntidad);

                _ErrorId = OrdenAcceso.ErrorId;
                _DescripcionError = OrdenAcceso.DescripcionError;
            }

            /// <summary>
            ///     
            /// </summary>
            /// <param name="Conexion"></param>
            /// <param name="Transaccion"></param>
            /// <param name="PreOrdenEntidad"></param>
            private void GuardaProductoOrdenEncabezadoTemp(SqlConnection Conexion, SqlTransaction Transaccion, OrdenDetalleEntidad OrdenDetalleEntidad)
            {
                OrdenAcceso OrdenAcceso = new OrdenAcceso();

                OrdenAcceso.InsertaProductoOrdenEncabezadoTemp(Conexion, Transaccion, _OrdenDetalleEntidad);

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
            ///     
            /// </summary>
            /// <returns></returns>
            private bool ValidarOrdenTemp()
            {
                //if (_OrdenDetalleEntidad.PreOrdenId == "")
                //{
                //    _ErrorId = (int)TextoError.Orden.PreOrdenIdVacio;
                //    _DescripcionError = TextoError.OrdenConPreOrdenIdVacio;
                //    return false;
                //}

                return true;
            }
        #endregion
    }
}
