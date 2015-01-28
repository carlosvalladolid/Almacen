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
            get;
            set;
        }

        public Int16 Cantidad
        {
            get;
            set;
        }
    }
}
