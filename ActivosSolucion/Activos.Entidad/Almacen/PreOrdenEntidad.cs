﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{
  public  class PreOrdenEntidad:Base
    {
        private string _PreOrdenId ;
        private Int32 _EmpleadoId;
        private Int32 _JefeId;
        private Int16 _EstatusId;
        private string _Clave;
        private string _ProductoId ;
        private Int16 _Cantidad;
        private string _FechaPreOrden;

        private string _SesionId;
      
        public PreOrdenEntidad()
        {
            _PreOrdenId = string.Empty;
            _EmpleadoId = 0;
            _JefeId = 0;
            _EstatusId = 0;
            _Clave = string.Empty;
            _ProductoId = string.Empty;
            _Cantidad = 0;
            _FechaPreOrden = string.Empty;

            _SesionId = string.Empty;
        }
      
        public string PreOrdenId
        {
            get { return _PreOrdenId; }
            set { _PreOrdenId = value; }
        }
      
        public Int32 EmpleadoId
        {
            get { return _EmpleadoId; }
            set { _EmpleadoId = value; }
        }


        public Int32 JefeId
        {
            get { return _JefeId; }
            set { _JefeId = value; }
        }


        public Int16 EstatusId
        {
            get { return _EstatusId; }
            set { _EstatusId = value; }
        }


        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }


        public string ProductoId
        {
            get { return _ProductoId; }
            set { _ProductoId = value; }
        }

        public Int16 Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public string FechaPreOrden
        {
            get { return _FechaPreOrden; }
            set { _FechaPreOrden = value; }
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
