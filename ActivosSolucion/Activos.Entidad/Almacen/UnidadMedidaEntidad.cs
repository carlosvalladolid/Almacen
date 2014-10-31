using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Almacen
{
  public  class UnidadMedidaEntidad:Base
    {


      private string _UnidadMedidaId;
      private string _Nombre;

      public UnidadMedidaEntidad()
      {
          _UnidadMedidaId = string.Empty;
          _Nombre = string.Empty;      
      }

      public string UnidadMedidaId
      {
          get { return _UnidadMedidaId; }
          set { _UnidadMedidaId = value; }
      }

      
      public string Nombre
      {
          get { return _Nombre; }
          set { _Nombre = value; }
      }


    }
}
