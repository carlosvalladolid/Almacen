using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Activos.AccesoDatos.Catalogo;
using Activos.AccesoDatos.Almacen;
using Activos.ProcesoNegocio.Almacen;
using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
//using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;


namespace Activos.ProcesoNegocio.Almacen
{
  public  class UnidadMedidaProceso:Base
    {


      public ResultadoEntidad SeleccionarUnidadMedida(UnidadMedidaEntidad UnidadMedidaObjetoEntidad)
      {
          string CadenaConexion = string.Empty;
          ResultadoEntidad Resultado = new ResultadoEntidad();
          UnidadMedidaAcceso UnidadMedidaAccesoObjeto = new UnidadMedidaAcceso();

          CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

          Resultado = UnidadMedidaAccesoObjeto.SeleccionarUnidadMedida(UnidadMedidaObjetoEntidad, CadenaConexion);

          return Resultado;
      }




    }
}
