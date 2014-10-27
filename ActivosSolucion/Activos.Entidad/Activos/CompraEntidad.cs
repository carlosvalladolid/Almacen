using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class CompraEntidad : Base
    {

        private int _CompraId;                // Identificador de la compra
        private Int16 _EmpleadoId;              // Identificador del empleado
        private Int16 _JefeId;                  // Identificador del jefe
        private Int16 _ProveedorId;             // Identificador del proveedor
        private Int16 _TipoDocumentoId;         // Identificador del tipo de documento
        private Int16 _UsuarioIdInserto;        // Identificador del usuario que inserto el registro
        private string _CompraFolio;            // Folio del documento
        private string _FechaCompra;            // Fecha del documento
        private decimal _Monto;                 // Monto total de la compra
        private string _OrdenCompra;            // Orden de compra
        private string _FechaOc;                // Fecha de la orden de compra

        //Otros campos
        private int _TemporalCompraId;          // Identificador de la compra temporal

        public CompraEntidad()
        {
            _CompraId = 0;
            _EmpleadoId = 0;
            _JefeId = 0;
            _ProveedorId = 0;
            _TipoDocumentoId = 0;
            _UsuarioIdInserto = 0;
            _CompraFolio = string.Empty;
            _FechaCompra = string.Empty;
            _Monto = 0;
            _OrdenCompra = string.Empty;
            _FechaOc = string.Empty;
            _TemporalCompraId = 0;
        }

        public int CompraId
        {
            get { return _CompraId; }
            set { _CompraId = value; }
        }

        public Int16 EmpleadoId
        {
            get { return _EmpleadoId; }
            set { _EmpleadoId = value; }
        }

        public Int16 JefeId
        {
            get { return _JefeId; }
            set { _JefeId = value; }
        }

        public Int16 ProveedorId
        {
            get { return _ProveedorId; }
            set { _ProveedorId = value; }
        }

        public Int16 TipoDocumentoId
        {
            get { return _TipoDocumentoId; }
            set { _TipoDocumentoId = value; }
        }

        public Int16 UsuarioIdInserto
        {
            get { return _UsuarioIdInserto; }
            set { _UsuarioIdInserto = value; }
        }

        public string CompraFolio
        {
            get { return _CompraFolio; }
            set { _CompraFolio = value; }
        }

        public string FechaCompra
        {
            get { return _FechaCompra; }
            set { _FechaCompra = value; }
        }

        public decimal Monto
        {
            get { return _Monto; }
            set { _Monto = value; }
        }

        public string OrdenCompra
        {
            get { return _OrdenCompra; }
            set { _OrdenCompra = value; }
        }

        public string FechaOc
        {
            get { return _FechaOc; }
            set { _FechaOc = value; }
        }

        public int TemporalCompraId
        {
            get { return _TemporalCompraId; }
            set { _TemporalCompraId = value; }
        }

    }
}
