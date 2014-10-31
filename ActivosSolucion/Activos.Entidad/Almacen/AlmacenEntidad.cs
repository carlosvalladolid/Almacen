using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{

    public class AlmacenEntidad : Base
    {

        private string _ProductoId;
        private Int16 _FamiliaId;
        private Int16 _SubFamiliaId;
        private Int16 _MarcaId;
        private string _UnidadMedidaId;
        private bool _EstatusId;
        private string _Clave;
        private string _Descripcion;
        private Int16 _Minimo;
        private Int16 _Maximo;
        private Int16 _MaximoPermitido;


        //*********************************

        private string _BusquedaRapida;             // Texto de búsqueda en el catálogo de edificios
         private string _BuscarNombre;               // Campo para buscar subfamilias por nombre exacto

        public AlmacenEntidad()
        {
            //_ProductoId = Guid.NewGuid().ToString();
            _ProductoId = Guid.Empty.ToString();
            _FamiliaId = 0;
            _SubFamiliaId = 0;
            _MarcaId = 0;
            _UnidadMedidaId = Guid.Empty.ToString();
            _EstatusId = true;
            _Clave = string.Empty;
            _Descripcion = string.Empty;
            _Minimo = 0;
            _Maximo = 0;
            _MaximoPermitido = 0;

            //**************************

            _BusquedaRapida = string.Empty;        
            _BuscarNombre = string.Empty;

        }


        public string ProductoId
        {
            get { return _ProductoId; }
            set { _ProductoId = value; }
        }


        public Int16 FamiliaId
        {
            get { return _FamiliaId; }
            set { _FamiliaId = value; }
        }


        public Int16 SubFamiliaId
        {
            get { return _SubFamiliaId; }
            set { _SubFamiliaId = value; }
        }

        public Int16 MarcaId
        {
            get { return _MarcaId; }
            set { _MarcaId = value; }
        }


        public string UnidadMedidaId
        {
            get { return _UnidadMedidaId; }
            set { _UnidadMedidaId = value; }
        }


        public Boolean EstatusId
        {
            get { return _EstatusId; }
            set { _EstatusId = value; }
        }


        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }


        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }


        public Int16 Minimo
        {
            get { return _Minimo; }
            set { _Minimo = value; }
        }

        public Int16 Maximo
        {
            get { return _Maximo; }
            set { _Maximo = value; }
        }


        public Int16 MaximoPermitido
        {
            get { return _MaximoPermitido; }
            set { _MaximoPermitido = value; }
        }
        

        public string BusquedaRapida
        {
            get { return _BusquedaRapida; }
            set { _BusquedaRapida = value; }
        }

        
        public string BuscarNombre
        {
            get { return _BuscarNombre; }
            set { _BuscarNombre = value; }
        }



    }
}