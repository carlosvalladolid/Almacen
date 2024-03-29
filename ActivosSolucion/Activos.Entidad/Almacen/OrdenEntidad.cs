﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{

    public class OrdenEntidad : Base
    {
        private string _OrdenId;
        private string _PreOrdenId;
        private string _EmpleadoId;
        private string _JefeId;
        private Int16 _ProveedorId;
        private int _EstatusId;
        private string _Clave;
        private string _FechaOrden;
        private string _FechaInserto;

        private string _SesionId;

        public OrdenEntidad()
        {
            _OrdenId = string.Empty;
            _PreOrdenId = string.Empty;
            _EmpleadoId = string.Empty;
            _JefeId = string.Empty;
            _ProveedorId = 0;
            _EstatusId = 0;
            _Clave = string.Empty;
            _FechaOrden = string.Empty;
            _FechaInserto = string.Empty;

            _SesionId = string.Empty;
        }

        /// <summary>
        ///     Identificador de orden de compra.
        /// </summary>
        public string OrdenId
        {
            get { return _OrdenId; }
            set { _OrdenId = value; }
        }

        /// <summary>
        ///     Identificador de la preorden de compra.
        /// </summary>
        public string PreOrdenId
        {
            get { return _PreOrdenId; }
            set { _PreOrdenId = value; }
        }

        /// <summary>
        ///     Identificador de empleado.
        /// </summary>
        public string EmpleadoId
        {
            get { return _EmpleadoId; }
            set { _EmpleadoId = value; }
        }

        /// <summary>
        ///     Identificador de jefe directo.
        /// </summary>
        public string JefeId
        {
            get { return _JefeId; }
            set { _JefeId = value; }
        }

        /// <summary>
        ///     Identificador del proveedor.
        /// </summary>
        public Int16 ProveedorId
        {
            get { return _ProveedorId; }
            set { _ProveedorId = value; }
        }

        /// <summary>
        ///     Identificador del estatus.
        /// </summary>
        public int EstatusId
        {
            get { return _EstatusId; }
            set { _EstatusId = value; }
        }

        /// <summary>
        ///     Folio de la orden de compra.
        /// </summary>
        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }

        /// <summary>
        ///     Fecha de la orden de compra.
        /// </summary>
        public string FechaOrden
        {
            get { return _FechaOrden; }
            set { _FechaOrden = value; }
        }

        /// <summary>
        ///     Fecha en que se guardó el registro.
        /// </summary>
        public string FechaInserto
        {
            get { return _FechaInserto; }
            set { _FechaInserto = value; }
        }

        /// <summary>
        ///     Identificador de la sesión de usuario.
        /// </summary>
        public string SesionId
        {
            get { return _SesionId; }
            set { _SesionId = value; }
        }
    }
}
