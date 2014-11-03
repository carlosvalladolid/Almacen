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

          
            if (BuscarProductoDuplicada(AlmacenObjetoEntidad) == false)
            {
                  if (AlmacenObjetoEntidad.ProductoId =="0")
                  {
                   
                      AlmacenObjetoEntidad.ProductoId = Guid.NewGuid().ToString();

                      Resultado = AlmacenAccesoObjeto.InsertarProducto(AlmacenObjetoEntidad, CadenaConexion);
                  }
                  else
                  {
                      Resultado = AlmacenAccesoObjeto.ActualizarProducto(AlmacenObjetoEntidad, CadenaConexion);
                  }
            }
            else
            {
                Resultado.ErrorId = (int)ConstantePrograma.Producto.ProductoTieneRegistroDuplicado;
                Resultado.DescripcionError = TextoError.ProductoConNombreDuplicado;
            }

            return Resultado;
      }

      protected ResultadoEntidad EliminarProducto(string CadenaProductoId)
      {
          string CadenaConexion = string.Empty;
          ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
          AlmacenAcceso AlmacenAccesoObjeto = new AlmacenAcceso();

          CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

          ResultadoEntidadObjeto = AlmacenAccesoObjeto.EliminarProducto(CadenaProductoId, CadenaConexion);

          return ResultadoEntidadObjeto;
      }

      public ResultadoEntidad EliminarProducto(AlmacenEntidad AlmacenObjetoEntidad)
      {
          ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

          // Validar que las marcas no contengan información relacionada con otras tablas
          if (TieneRelacioneselProducto(AlmacenObjetoEntidad.CadenaProductoId))
          {
              ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Producto.PuestoTieneRegistrosRelacionados;
              ResultadoEntidadObjeto.DescripcionError = TextoError.MarcaTieneRegistrosRelacionados;
          }
          else
          {
              // Si se pasaron todas las validaciones, hay que borrar los Productos seleccionadas
              ResultadoEntidadObjeto = EliminarProducto(AlmacenObjetoEntidad.CadenaProductoId);
          }

          return ResultadoEntidadObjeto;
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

      public ResultadoEntidad SeleccionarProductoparaEditar(AlmacenEntidad AlmacenObjetoEntidad)
      {
          string CadenaConexion = string.Empty;
          ResultadoEntidad Resultado = new ResultadoEntidad();
          AlmacenAcceso AlmacenAccesoObjeto = new AlmacenAcceso();

          CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

          Resultado = AlmacenAccesoObjeto.SeleccionarProductoparaEditar(AlmacenObjetoEntidad, CadenaConexion);

          return Resultado;
      }

      public bool BuscarProductoDuplicada(AlmacenEntidad AlmacenObjetoEntidad)
      {
          bool ExisteProducto = false;
          ResultadoEntidad Resultado = new ResultadoEntidad();
          AlmacenEntidad BuscarAlmacenObjetoEntidad = new AlmacenEntidad();

          BuscarAlmacenObjetoEntidad.BuscarNombre = Comparar.EstandarizarCadena(AlmacenObjetoEntidad.Descripcion);

          Resultado = SeleccionarProducto(BuscarAlmacenObjetoEntidad);

          if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
          {
              if (string.Format(Resultado.ResultadoDatos.Tables[0].Rows[0]["Clave"].ToString()) != AlmacenObjetoEntidad.Clave)
                  ExisteProducto = true;
              else
                  ExisteProducto = false;
          }

          return ExisteProducto;
      }

      protected bool TieneProductosRelacionados(string CadenaProductoId)
      {
          bool TieneRelaciones = false;
          //AlmacenProceso AlmacenProcesoObjeto = new AlmacenProceso();

          //TieneRelaciones =AlmacenProcesoObjeto.SeleccionarProductosRelacionados(CadenaProductoId);

          return TieneRelaciones;
      }

      protected bool TieneRelacioneselProducto(string CadenaProductoId)
      {
          bool TieneRelacioneselProducto = false;

          // Revisar relaciones con Almacen
          if (TieneProductosRelacionados(CadenaProductoId))
              return true;

          return TieneRelacioneselProducto;
      }
    }
}
