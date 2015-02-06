using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{
   public class SubFamiliaPuestoEntidad:Base
    {
            private Int16 _SubFamiliaId;
            private Int16 _PuestoId;
            private string _Descripcion;
      //***************************************
            private string _CadenaPuestoXML;

        public SubFamiliaPuestoEntidad()
        {
            _SubFamiliaId = 0;
            _PuestoId = 0;
            _Descripcion = string.Empty;
       //**********************************
            _CadenaPuestoXML = string.Empty;
        }


        public Int16 SubFamiliaId
        {
            get { return _SubFamiliaId; }
            set { _SubFamiliaId = value; }
        }

        public Int16 PuestoId
        {
            get { return _PuestoId; }
            set { _PuestoId = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
       
        //public string CadenaPuestoId
        //{
        //    get { return _CadenaPuestoId; }
        //    set { _CadenaPuestoId = value; }
        //}


        public string CadenaPuestoXML
        {
            get { return _CadenaPuestoXML; }
            set { _CadenaPuestoXML = value; }
        }
    }
}
