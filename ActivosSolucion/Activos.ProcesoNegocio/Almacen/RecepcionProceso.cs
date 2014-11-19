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
       public ResultadoEntidad AgregarRecepcion(RecepcionEntidad RecepcionObjetoEntidad)
       {
           RecepcionAcceso RecepcionAccesoObjeto = new RecepcionAcceso();
           string CadenaConexion = string.Empty;
           ResultadoEntidad Resultado = new ResultadoEntidad();
           ResultadoEntidad ResultadoRecepcionDuplicado = new ResultadoEntidad();
           SqlTransaction Transaccion;
           SqlConnection Conexion;

           CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

           ////****************** aqui entra para revisar que no se agregue la PreOrden
           //    ResultadoPreOrdenDuplicado = ValidarPreOrdenDuplicado(TemporalPreOrdenObjetoEntidad);

           //    if (ResultadoPreOrdenDuplicado.ErrorId != 0)
           //    {
           //        return ResultadoPreOrdenDuplicado;
           //    }

           ////**************************************************************************************            
           Conexion = new SqlConnection(CadenaConexion);
           Conexion.Open();

           Transaccion = Conexion.BeginTransaction();
           try
           {
               if (RecepcionObjetoEntidad.RecepcionId == "")
               {
                   RecepcionObjetoEntidad.RecepcionId = Guid.NewGuid().ToString();

                   Resultado = RecepcionAccesoObjeto.InsertarRecepcionDetalle(Conexion, Transaccion, RecepcionObjetoEntidad);
               }
               else
               {
                   //Editar encabezado
                  // Resultado = RecepcionAccesoObjeto.ActualizarRecepcionEncabezado(Conexion, Transaccion, RecepcionObjetoEntidad);
               }

               if (Resultado.ErrorId == (int)ConstantePrograma.Recepcion.RecepcionGuardadoCorrectamente)
               {
                   Resultado = RecepcionAccesoObjeto.SeleccionarRecepcionDetalle(Conexion, Transaccion, RecepcionObjetoEntidad);

                   if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                   {

                       Resultado.ErrorId = ((int)ConstantePrograma.Recepcion.FolioDuplicado);
                       //Se edita el poducto
                       // Resultado = RecepcionAccesoObjeto.ActualizarPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
                   }
                   else
                   {
                       //Se inserta el DetalleDocumento
                       Resultado = RecepcionAccesoObjeto.InsertarRecepcionEncabezado(Conexion, Transaccion, RecepcionObjetoEntidad);
                   }

                   if (Resultado.ErrorId == (int)ConstantePrograma.Recepcion.RecepcionGuardadoCorrectamente)
                   {
                       Transaccion.Commit();
                   }
                   else
                   {
                       Transaccion.Rollback();
                   }

               }
               else
               {
                   Transaccion.Rollback();
               }

               Conexion.Close();

               return Resultado;
           }
           catch (Exception EX)
           {
               Transaccion.Rollback();

               if (Conexion.State == ConnectionState.Open)
               {
                   Conexion.Close();
               }
               Resultado.DescripcionError = EX.Message;
               return Resultado;

           }
       }

       public ResultadoEntidad SeleccionaRecepcion(RecepcionEntidad RecepcionObjetoEntidad)
       {
           string CadenaConexion = string.Empty;
           RecepcionAcceso RecepcionAccesoObjeto = new RecepcionAcceso();
           ResultadoEntidad Resultado = new ResultadoEntidad();

           SqlTransaction Transaccion;
           SqlConnection Conexion;

           CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

           Conexion = new SqlConnection(CadenaConexion);
           Conexion.Open();

           Transaccion = Conexion.BeginTransaction();

           try
           {
               Resultado = RecepcionAccesoObjeto.SeleccionarRecepcionDetalle(Conexion, Transaccion, RecepcionObjetoEntidad);

               return Resultado;
           }
           catch (Exception EX)
           {
               Transaccion.Rollback();

               if (Conexion.State == ConnectionState.Open)
               {
                   Conexion.Close();
               }
               Resultado.DescripcionError = EX.Message;
               return Resultado;

           }

       }
       
    }
}
