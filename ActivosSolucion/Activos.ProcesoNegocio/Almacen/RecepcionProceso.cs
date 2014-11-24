using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


using Activos.AccesoDatos.Almacen;
using Activos.ProcesoNegocio.Almacen;
using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Almacen;


namespace Activos.ProcesoNegocio.Almacen
{
   public class RecepcionProceso:Base
    {      
       public ResultadoEntidad AgregarRecepcionDetalle(RecepcionEntidad RecepcionObjetoEntidad)
       {
           string CadenaConexion = string.Empty;
           ResultadoEntidad Resultado = new ResultadoEntidad();
           ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
           RecepcionAcceso RecepcionAccesoObjeto = new RecepcionAcceso();

           CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);
         
           if (RecepcionObjetoEntidad.TemporalRecepcionId == "0")
               {
                   RecepcionObjetoEntidad.RecepcionId = Guid.NewGuid().ToString();
                   Resultado = RecepcionAccesoObjeto.InsertarRecepcionDetalle(RecepcionObjetoEntidad, CadenaConexion);
               }
               else
               {
                  // Resultado = RecepcionAccesoObjeto.ActualizarProducto(RecepcionObjetoEntidad, CadenaConexion);
               }
         
           return Resultado;
       }


     
      public ResultadoEntidad SeleccionaRecepcion(RecepcionEntidad RecepcionObjetoEntidad)
      {
          string CadenaConexion = string.Empty;
          ResultadoEntidad Resultado = new ResultadoEntidad();
          RecepcionAcceso RecepcionAccesoObjeto = new RecepcionAcceso();

          CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

          Resultado = RecepcionAccesoObjeto.SeleccionarRecepcionDetalle(RecepcionObjetoEntidad, CadenaConexion);

          return Resultado;
      }



      public ResultadoEntidad AgregarRecepcionEncabezado(RecepcionEntidad RecepcionObjetoEntidad)
      {
          string CadenaConexion = string.Empty;
          ResultadoEntidad Resultado = new ResultadoEntidad();
          ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
          RecepcionAcceso RecepcionAccesoObjeto = new RecepcionAcceso();

          CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

          if (RecepcionObjetoEntidad.TemporalRecepcionId == "0")
          {
             
              Resultado = RecepcionAccesoObjeto.InsertarRecepcionEncabezado(RecepcionObjetoEntidad, CadenaConexion);
          }
          else
          {
              // Resultado = RecepcionAccesoObjeto.ActualizarProducto(RecepcionObjetoEntidad, CadenaConexion);
          }

          return Resultado;
      }




    }
}
