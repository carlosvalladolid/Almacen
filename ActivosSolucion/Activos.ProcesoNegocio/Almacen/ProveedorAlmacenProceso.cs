using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Almacen;
using Activos.Comun.Cadenas;
using Activos.Comun.Constante;
using Activos.Entidad.Almacen;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Almacen;

namespace Activos.ProcesoNegocio.Almacen
{
  public  class ProveedorAlmacenProceso:Base
    {
        private int _ErrorId;
        private string _DescripcionError;
        DataSet _ResultadoDatos;
        ProveedorAlmacenEntidad _ProveedorAlmacenEntidad;

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
        public ProveedorAlmacenEntidad ProveedorAlmacenEntidad
        {
            get { return _ProveedorAlmacenEntidad; }
            set { _ProveedorAlmacenEntidad = value; }
        }

        /// <summary>
        ///     Constructor de la clase
        /// </summary>
        public ProveedorAlmacenProceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _ResultadoDatos = null;
            _ProveedorAlmacenEntidad = new ProveedorAlmacenEntidad();
        }

        #region "Métodos"
            /// <summary>
            ///     Busca los proveedores en la base de datos.
            /// </summary>
            //public void SeleccionarProveedor()
            //{
            //    string CadenaConexion = string.Empty;
            //    ProveedorAlmacenAcceso ProveedorAlmacenAcceso = new ProveedorAlmacenAcceso();

            //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            //    _ResultadoDatos = ProveedorAlmacenAcceso.SeleccionarProveedor(_ProveedorAlmacenEntidad, CadenaConexion);
               
            //    _ErrorId = ProveedorAlmacenAcceso.ErrorId;
            //    _DescripcionError = ProveedorAlmacenAcceso.DescripcionError;
            //}

            //public ResultadoEntidad GuardarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenObjetoEntidad)
            //{
            //    string CadenaConexion = string.Empty;
            //    ResultadoEntidad Resultado = new ResultadoEntidad();
            //    ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
            //    ProveedorAlmacenAcceso ProveedorAlmacenAccesoObjeto = new ProveedorAlmacenAcceso();

            //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            //    ResultadoValidacion = ValidarProveedor(ProveedorAlmacenObjetoEntidad);

            //    if (ResultadoValidacion.ErrorId == 0)
            //    {
            //        if (ProveedorAlmacenObjetoEntidad.ProveedorId == 0)
            //        {
            //            Resultado = ProveedorAlmacenAccesoObjeto.InsertarProveedor(ProveedorAlmacenObjetoEntidad, CadenaConexion);
            //        }
            //        else
            //        {
            //            Resultado = ProveedorAlmacenAccesoObjeto.ActualizarProveedor(ProveedorAlmacenObjetoEntidad, CadenaConexion);
            //        }
            //    }
            //    else
            //    {
            //        Resultado = ResultadoValidacion;
            //    }

            //    return Resultado;
            //}

            //public ResultadoEntidad SeleccionarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenObjetoEntidad)
            //{
            //    string CadenaConexion = string.Empty;
            //    ResultadoEntidad Resultado = new ResultadoEntidad();
            //    ProveedorAlmacenAcceso ProveedorAlmacenAccesoObjeto = new ProveedorAlmacenAcceso();

            //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            //    Resultado = ProveedorAlmacenAccesoObjeto.SeleccionarProveedor(ProveedorAlmacenObjetoEntidad, CadenaConexion);

            //    return Resultado;
            //}
        #endregion
    }
}

