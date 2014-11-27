﻿using System;
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
        public string _TemporalRequisicionId;
        public string _Nombre;


        public RequisicionEntidad()
        { 
            _RequisicionId = string.Empty;
            _ProductoId = string.Empty;
            _Cantidad = 0;
            _EmpleadoId = 0;
            _JefeId = 0;
            _EstatusId = 0;
            _TemporalRequisicionId = string.Empty;
            _Nombre = string.Empty;
        
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

    }
}