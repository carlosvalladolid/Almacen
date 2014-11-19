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

    public class OrdenProceso : Base
    {
   
            private int _ErrorId;
            private string _DescripcionError;
            DataSet _ResultadoDatos;
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
                _OrdenEntidad = new OrdenEntidad();
            }

            #region "Métodos"


          

            #endregion
        }
    }
