using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{
    public class OrdenSalidaDetalleEntidad : OrdenSalidaEntidad
    {
        private string _ProductoId;
        private Int16 _Cantidad;

        public string ProductoId
        {
            get { return _ProductoId; }
            set { _ProductoId = value; }
        }

        public Int16 Cantidad
        {
            get { return _Cantidad;}
            set { _Cantidad = value; }
        }
    }
}
