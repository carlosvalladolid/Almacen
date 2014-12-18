using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{
    public class OrdenDetalleEntidad : OrdenEntidad
    {
        private string _ProductoId;
        private Int16 _Cantidad;

        private string[,] _ProductoIdArray;

        public OrdenDetalleEntidad()
        {
            _ProductoId = string.Empty;
            _Cantidad = 0;
        }

        /// <summary>
        ///     Identificador del producto.
        /// </summary>
        public string ProductoId
        {
            get { return _ProductoId; }
            set { _ProductoId = value; }
        }

        /// <summary>
        ///     Identificador del producto.
        /// </summary>
        public Int16 Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        /// <summary>
        ///     Arreglo con los identificadores de los productos.
        /// </summary>
        public string[,] ProductoIdArray
        {
            get { return _ProductoIdArray; }
            set { _ProductoIdArray = value; }
        }
    }
}
