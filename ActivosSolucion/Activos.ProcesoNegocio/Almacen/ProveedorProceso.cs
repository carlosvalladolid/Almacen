using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Almacen;
using Activos.Comun.Constante;
using Activos.Entidad.Almacen;

namespace Activos.ProcesoNegocio.Almacen
{
    public class ProveedorProceso : Base
    {
        private int _ErrorId;
        private string _DescripcionError;
        DataSet _ResultadoDatos;
        ProveedorEntidad _ProveedorEntidad;

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
        public ProveedorEntidad ProveedorEntidad
        {
            get { return _ProveedorEntidad; }
            set { _ProveedorEntidad = value; }
        }

        /// <summary>
        ///     Constructor de la clase
        /// </summary>
        public ProveedorProceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _ResultadoDatos = null;
            _ProveedorEntidad = new ProveedorEntidad();
        }

        #region "Métodos"
            /// <summary>
            ///     Busca los proveedores en la base de datos.
            /// </summary>
            public void SeleccionarProveedor()
            {
                string CadenaConexion = string.Empty;
                ProveedorAcceso ProveedorAcceso = new ProveedorAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                _ResultadoDatos = ProveedorAcceso.SeleccionarProveedor(_ProveedorEntidad, CadenaConexion);

                _ErrorId = ProveedorAcceso.ErrorId;
                _DescripcionError = ProveedorAcceso.DescripcionError;
            }
        #endregion
    }
}
