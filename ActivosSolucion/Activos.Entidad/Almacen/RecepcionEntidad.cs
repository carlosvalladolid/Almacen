using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{
  public  class RecepcionEntidad:Base
    {

        //campos de RecepcionEncabezado
    public string  _RecepcionId;
	public string _OrdenId;
	public Int32 _EmpleadoId;
	public Int32 _JefeId;
	public Int16 _ProveedorId;
	public Int16 _TipoDocumentoId ;
	public Int16 _EstatusId;
	public string _Clave;
	public string _FechaDocumento;
    public string _FechaVencimiento;
	public decimal _Monto;
    //campos de RecepcionDetalle
	public string _ProductoId;
	public decimal _PrecioUnitario;
    public int _Cantidad;
    public string _TemporalRecepcionId;
    private string _FacturaId;

    public string FacturaId
    {
        get { return _FacturaId; }
        set { _FacturaId = value; }
    }

    public RecepcionEntidad()
        {
            _RecepcionId = string.Empty;
            _OrdenId = string.Empty;
            _EmpleadoId = 0;
            _JefeId = 0;
            _ProveedorId = 0;
            _TipoDocumentoId = 0;
            _EstatusId = 0;
            _Clave = string.Empty;
            _FechaDocumento = string.Empty;
            _FechaVencimiento = string.Empty;
            _Monto = 0;
            _ProductoId = string.Empty;
            _PrecioUnitario = 0;
            _Cantidad = 0;
            _TemporalRecepcionId = string.Empty;
            _FacturaId = string.Empty;
        }

    public string RecepcionId
    {
        get { return _RecepcionId; }
        set { _RecepcionId = value; }
    }

    public string OrdenId 
    {
        get { return _OrdenId; }
        set { _OrdenId = value; }
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


    public string FechaDocumento
    {
        get { return _FechaDocumento; }
        set { _FechaDocumento = value; }
    }


    public string FechaVencimiento
    {
        get { return _FechaVencimiento; }
        set { _FechaVencimiento = value; }
    }
    public decimal Monto
    {
        get { return _Monto; }
        set { _Monto = value; }
    }

    public string ProductoId
    {
        get { return _ProductoId; }
        set { _ProductoId = value; }
    }


    public decimal PrecioUnitario
    {
        get { return _PrecioUnitario; }
        set { _PrecioUnitario = value; }
    }

    public int Cantidad
    {
        get { return _Cantidad; }
        set { _Cantidad = value; }
    }


    public string TemporalRecepcionId
    {
        get { return _TemporalRecepcionId; }
        set { _TemporalRecepcionId = value; }
    }

      
    }
}
