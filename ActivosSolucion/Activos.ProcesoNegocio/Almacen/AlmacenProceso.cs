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
  public class AlmacenProceso:Base
    {
      public ResultadoEntidad GuardarProducto(AlmacenEntidad AlmacenObjetoEntidad)
      {
          string CadenaConexion = string.Empty;
          ResultadoEntidad Resultado = new ResultadoEntidad();
          ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
          AlmacenAcceso AlmacenAccesoObjeto = new AlmacenAcceso();

          CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);


          if (AlmacenObjetoEntidad.ProductoId == "")
          {
              //AlmacenObjetoEntidad.ProductoId = Guid.NewGuid().ToString();

              Resultado = AlmacenAccesoObjeto.InsertarProducto(AlmacenObjetoEntidad, CadenaConexion);
          }
          else
          {
              Resultado = AlmacenAccesoObjeto.ActualizarProducto(AlmacenObjetoEntidad, CadenaConexion);
          }


          return Resultado;
      }

      public ResultadoEntidad SeleccionarProducto(AlmacenEntidad AlmacenObjetoEntidad)
      {
          string CadenaConexion = string.Empty;
          ResultadoEntidad Resultado = new ResultadoEntidad();
          AlmacenAcceso AlmacenAccesoObjeto = new AlmacenAcceso();

          CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

          Resultado = AlmacenAccesoObjeto.SeleccionarProducto(AlmacenObjetoEntidad, CadenaConexion);

          return Resultado;
      }


    }
}
