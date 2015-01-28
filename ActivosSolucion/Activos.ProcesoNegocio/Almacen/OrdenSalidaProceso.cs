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
    public class OrdenSalidaProceso : Base
    {
        private int _ErrorId;
        private string _DescripcionError;
        DataSet _ResultadoDatos;
        OrdenSalidaEntidad _OrdenSalidaEncabezadoEntidad;
        OrdenSalidaDetalleEntidad _OrdenSalidaDetalleEntidad;

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
        public OrdenSalidaEntidad OrdenSalidaEncabezadoEntidad
        {
            get { return _OrdenSalidaEncabezadoEntidad; }
            set { _OrdenSalidaEncabezadoEntidad = value; }
        }

        /// <summary>
        ///     Entidad del proceso.
        /// </summary>
        public OrdenSalidaDetalleEntidad OrdenSalidaDetalleEntidad
        {
            get { return _OrdenSalidaDetalleEntidad; }
            set { _OrdenSalidaDetalleEntidad = value; }
        }

        /// <summary>
        ///     Constructor de la clase
        /// </summary>
        public OrdenSalidaProceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _ResultadoDatos = null;
            _OrdenSalidaEncabezadoEntidad = new OrdenSalidaEntidad();
            _OrdenSalidaDetalleEntidad = new OrdenSalidaDetalleEntidad();
        }

        #region "Métodos"
            
        #endregion
    }
}
