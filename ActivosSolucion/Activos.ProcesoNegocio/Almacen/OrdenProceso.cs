using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data;
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
        PreOrdenEntidad _PreOrdenEntidad;
        OrdenEntidad _OrdenEntidad;

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
        public PreOrdenEntidad PreOrdenEntidad
        {
            get { return _PreOrdenEntidad; }
            set { _PreOrdenEntidad = value; }
        }

        /// <summary>
        ///     Entidad del proceso.
        /// </summary>
        public OrdenEntidad OrdenEntidad
        {
            get { return _OrdenEntidad; }
            set { _OrdenEntidad = value; }
        }

        /// <summary>
        ///     Constructor de la clase
        /// </summary>
        public OrdenProceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _ResultadoDatos = null;
            _PreOrdenEntidad = new PreOrdenEntidad();
            _OrdenEntidad = new OrdenEntidad();
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
                    //if(PreOrdenEntidad.Orden)
                    GuardaProductoOrdenEncabezadoTemp(Conexion, Transaccion, _PreOrdenEntidad);

                    // Guardar encabezado temporal
                    if (_ErrorId != 0)
                    {
                        Transaccion.Rollback();
                        Conexion.Close();
                        return;
                    }

                    // Si todo salió bien, guardar el detalle temporal
                    GuardaProductoOrdenDetalleTemp(Conexion, Transaccion, _PreOrdenEntidad);

                    if (_ErrorId == 0)
                        Transaccion.Commit();
                    else
                        Transaccion.Rollback();

                    Conexion.Close();

                    return;
                }
                catch(Exception Exception)
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
            private void GuardaProductoOrdenDetalleTemp(SqlConnection Conexion, SqlTransaction Transaccion, PreOrdenEntidad PreOrdenEntidad)
            {
                OrdenAcceso OrdenAcceso = new OrdenAcceso();

                OrdenAcceso.InsertaProductoOrdenDetalleTemp(Conexion, Transaccion, _PreOrdenEntidad);

                _ErrorId = OrdenAcceso.ErrorId;
                _DescripcionError = OrdenAcceso.DescripcionError;
            }

            /// <summary>
            ///     
            /// </summary>
            /// <param name="Conexion"></param>
            /// <param name="Transaccion"></param>
            /// <param name="PreOrdenEntidad"></param>
            private void GuardaProductoOrdenEncabezadoTemp(SqlConnection Conexion, SqlTransaction Transaccion, PreOrdenEntidad PreOrdenEntidad)
            {
                OrdenAcceso OrdenAcceso = new OrdenAcceso();

                OrdenAcceso.InsertaProductoOrdenEncabezadoTemp(Conexion, Transaccion, _PreOrdenEntidad);

                _ErrorId = OrdenAcceso.ErrorId;
                _DescripcionError = OrdenAcceso.DescripcionError;
            }

            private bool ValidarOrdenTemp()
            {
                if (_PreOrdenEntidad.PreOrdenId == "")
                {
                    _ErrorId = (int)TextoError.Orden.PreOrdenIdVacio;
                    _DescripcionError = TextoError.OrdenConPreOrdenIdVacio;
                    return false;
                }

                return true;
            }
        #endregion
    }
}
