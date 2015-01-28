using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{
    public class OrdenSalidaEntidad : Base
    {
        private string _OrdenSalidaId;
        private string _RequisicionId;
        private Int16 _EstatusId;
        private int _UsuarioIdInserto;
        private string _Clave;
        private string _FechaInserto;

        public OrdenSalidaEntidad()
        {
            _OrdenSalidaId = string.Empty;
            _RequisicionId = string.Empty;
            _EstatusId = 0;
            _UsuarioIdInserto = 0;
            _Clave = string.Empty;
            _FechaInserto = string.Empty;
        }

        /// <summary>
        ///     Identificador de la orden de salida.
        /// </summary>
        public string OrdenSalidaId
        {
            get { return _OrdenSalidaId; }
            set { _OrdenSalidaId = value; }
        }

        /// <summary>
        ///     Identificador de la requisición.
        /// </summary>
        public string RequisicionId
        {
            get { return _RequisicionId; }
            set { _RequisicionId = value; }
        }

        /// <summary>
        ///     Identificador del estatus.
        /// </summary>
        public Int16 EstatusId
        {
            get { return _EstatusId; }
            set { _EstatusId = value; }
        }

        /// <summary>
        ///     Identificador del usuario que creó el registro.
        /// </summary>
        public int UsuarioIdInserto
        {
            get { return _UsuarioIdInserto; }
            set { _UsuarioIdInserto = value; }
        }

        /// <summary>
        ///     Folio de la orden de salida.
        /// </summary>
        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }

        /// <summary>
        ///     Fecha en que se creó el nuevo registro.
        /// </summary>
        public string FechaInserto
        {
            get { return _FechaInserto; }
            set { _FechaInserto = value; }
        }
    }
}
