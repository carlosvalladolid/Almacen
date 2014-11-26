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
   public class RequisicionProceso:Base
    {


       public ResultadoEntidad AgregarRequisicionDetalle(RequisicionEntidad RequisicionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
            RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);


            //****************** aqui entra para revisar que no se agregue la Orden
            ResultadoValidacion = BuscarRequisicionProducto(RequisicionObjetoEntidad);

            if (ResultadoValidacion.ErrorId != 0)
            {
                return ResultadoValidacion;
            }


            //if (ResultadoValidacion.ErrorId != 0)
            //{


            if (RequisicionObjetoEntidad.TemporalRequisicionId == "")
            {
                RequisicionObjetoEntidad.RequisicionId = Guid.NewGuid().ToString();
                Resultado = RequisicionAccesoObjeto.InsertarRequisicionDetalle(RequisicionObjetoEntidad, CadenaConexion);
            }
            else
            {
                Resultado = RequisicionAccesoObjeto.InsertarRequisicionDetalle(RequisicionObjetoEntidad, CadenaConexion);
            }

            // }
            //else 
            //{
            //    Resultado = Resultado.ErrorId = (int)ConstantePrograma.Recepcion.FolioDuplicado;


            //}


            return Resultado;
        }

       public ResultadoEntidad SeleccionaRequisicion(RequisicionEntidad RequisicionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            Resultado = RequisicionAccesoObjeto.SeleccionarRequisicionDetalle(RequisicionObjetoEntidad, CadenaConexion);

            return Resultado;
        }

       public ResultadoEntidad AgregarRequisicionEncabezado(RequisicionEntidad RequisicionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
            RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            if (RequisicionObjetoEntidad.RequisicionId != "")
            {

                Resultado = RequisicionAccesoObjeto.InsertarRequisicionEncabezado(RequisicionObjetoEntidad, CadenaConexion);
            }
            else
            {
                // Resultado = RecepcionAccesoObjeto.ActualizarProducto(RecepcionObjetoEntidad, CadenaConexion);
            }

            return Resultado;
        }

       public ResultadoEntidad CancelarNuevoRequisicion(RequisicionEntidad RequisicionObjetoEntidad)
       {
           string CadenaConexion = string.Empty;
           ResultadoEntidad Resultado = new ResultadoEntidad();
           RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();
           SqlTransaction Transaccion;
           SqlConnection Conexion;

           CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

           Conexion = new SqlConnection(CadenaConexion);
           Conexion.Open();

           Transaccion = Conexion.BeginTransaction();

           try
           {

               //Se elimina la RecepcionDetalle del producto
               if (RequisicionObjetoEntidad.ProductoId != "")
               {

                   Resultado = RequisicionAccesoObjeto.EliminarRequisicionDetalle(Conexion, Transaccion, RequisicionObjetoEntidad);

                   if (Resultado.ErrorId == (int)ConstantePrograma.Requisicion.EliminadoExitosamente)
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

       public ResultadoEntidad BuscarRequisicionProducto(RequisicionEntidad RequisicionObjetoEntidad)
       {
           ResultadoEntidad Resultado = new ResultadoEntidad();

           if (RequisicionObjetoEntidad.TemporalRequisicionId != "")
           {

               if (RequisicionObjetoEntidad.ProductoId != "")
               {
                   Resultado = SeleccionaRequisicion(RequisicionObjetoEntidad);

                   if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                   {
                       Resultado.ErrorId = (int)ConstantePrograma.Requisicion.RequisicionTieneRegistroDuplicado;
                       Resultado.DescripcionError = TextoError.RequisicionDocumentoDuplicado;
                   }

               }
               //return Resultado;
               else
               {

                   Resultado.DescripcionError = TextoError.ErrorGenerico;
               }


           }

           return Resultado;

       }


       public ResultadoEntidad SeleccionarEmpleado(RequisicionEntidad RequisicionObjetoEntidad)
       {
           string CadenaConexion = string.Empty;
           ResultadoEntidad Resultado = new ResultadoEntidad();
           RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

           CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

           Resultado = RequisicionAccesoObjeto.SeleccionarEmpleado(RequisicionObjetoEntidad, CadenaConexion);

           return Resultado;
       }




    }
}
