using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{
    public  class RequisicionEntidad:Base
    {
        public string _RequisicionId;
        public string _ProductoId;
        public Int16 _Cantidad;
        public Int32 _EmpleadoId;
        public Int32 _JefeId;
        public Int16 _EstatusId;
        public string _Clave;
        public string _TemporalRequisicionId;
        public string _Nombre;

        private string _SesionId;
        private string _FechaInicial;
        private string _FechaFinal;

        public RequisicionEntidad()
        { 
            _RequisicionId = string.Empty;
            _ProductoId = string.Empty;
            _Cantidad = 0;
            _EmpleadoId = 0;
            _JefeId = 0;
            _EstatusId = 0;
            _Clave = string.Empty;
            _TemporalRequisicionId = string.Empty;
            _Nombre = string.Empty;

            _SesionId = string.Empty;
            _FechaInicial = string.Empty;
            _FechaFinal = string.Empty;
        }

        public string RequisicionId
        {
            get { return _RequisicionId; }
            set { _RequisicionId = value; }
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

        public string TemporalRequisicionId
        {
            get { return _TemporalRequisicionId; }
            set { _TemporalRequisicionId = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        /// <summary>
        ///     Identificador de la sesión de usuario.
        /// </summary>
        public string SesionId
        {
            get { return _SesionId; }
            set { _SesionId = value; }
        }

        /// <summary>
        ///     Fecha inicial del rango de búsqueda.
        /// </summary>
        public string FechaInicial
        {
            get { return _FechaInicial; }
            set { _FechaInicial = value; }
        }

        /// <summary>
        ///     Fecha final del rango de búsqueda.
        /// </summary>
        public string FechaFinal
        {
            get { return _FechaFinal; }
            set { _FechaFinal = value; }
        }
    }
}
