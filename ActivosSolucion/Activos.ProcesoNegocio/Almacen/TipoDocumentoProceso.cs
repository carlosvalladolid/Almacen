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
    public class TipoDocumentoProceso:Base
    {

         private int _ErrorId;
        private string _DescripcionError;
        DataSet _ResultadoDatos;
        TipoDocumentoEntidad _TipoDocumentoEntidad;

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
        public  TipoDocumentoEntidad  TipoDocumentoEntidad
        {
            get { return _TipoDocumentoEntidad; }
            set { _TipoDocumentoEntidad = value; }
        }

        /// <summary>
        ///     Constructor de la clase
        /// </summary>
        public  TipoDocumentoProceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _ResultadoDatos = null;
            _TipoDocumentoEntidad = new  TipoDocumentoEntidad();
        }

        #region "Métodos"
            /// <summary>
            ///     Busca los  TipoDocumento en la base de datos.
            /// </summary>
            public void SeleccionarTipoDocumento()
            {
                string CadenaConexion = string.Empty;
                 TipoDocumentoAcceso  TipoDocumentoAcceso = new  TipoDocumentoAcceso();
                
                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                _ResultadoDatos =  TipoDocumentoAcceso.SeleccionarTipoDocumento(_TipoDocumentoEntidad, CadenaConexion);
                _ErrorId =  TipoDocumentoAcceso.ErrorId;                 
                _DescripcionError =  TipoDocumentoAcceso.DescripcionError;

            }
        #endregion
    }


    }

