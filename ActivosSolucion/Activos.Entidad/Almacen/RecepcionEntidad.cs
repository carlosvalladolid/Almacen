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
	public decimal _Monto;
    //campos de RecepcionDetalle
	public string _ProductoId;
	public decimal _Precio;
    public string _Cantidad;
    public string _TemporalRecepcionId;

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
            _Monto = 0;
            _ProductoId = string.Empty;
            _Precio = 0;
            _Cantidad = string.Empty;
            _TemporalRecepcionId = string.Empty;
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


    public decimal Precio
    {
        get { return _Precio; }
        set { _Precio = value; }
    }

    public string Cantidad
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
