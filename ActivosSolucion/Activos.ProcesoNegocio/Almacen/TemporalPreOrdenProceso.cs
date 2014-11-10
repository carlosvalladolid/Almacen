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
   public class TemporalPreOrdenProceso:Base
    {
       public ResultadoEntidad AgregarTemporalPreOrden(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
       {
           TemporalPreOrdenAcceso TemporalPreOrdenAccesoObjeto = new TemporalPreOrdenAcceso();
           string CadenaConexion = string.Empty;       
           ResultadoEntidad Resultado = new ResultadoEntidad();
           ResultadoEntidad ResultadoPreOrdenDuplicado = new ResultadoEntidad();
           SqlTransaction Transaccion;
           SqlConnection Conexion;

           CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);
          
           Conexion = new SqlConnection(CadenaConexion);
           Conexion.Open();

           Transaccion = Conexion.BeginTransaction();
           try
           {
               if (TemporalPreOrdenObjetoEntidad.PreOrdenId == "")
               {
                   TemporalPreOrdenObjetoEntidad.PreOrdenId = Guid.NewGuid().ToString();

                   Resultado = TemporalPreOrdenAccesoObjeto.InsertarTemporalPreOrdenEncabezadoTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
               }
               else
               {
                   //Editar encabezado
                   Resultado = TemporalPreOrdenAccesoObjeto.ActualizarPreOrdenEncabezadoTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
               }

               if (Resultado.ErrorId == (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenGuardadoCorrectamente)
               {
                   Resultado = TemporalPreOrdenAccesoObjeto.SeleccionarPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);

                   if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                   {
                       //Se edita el poducto
                       Resultado = TemporalPreOrdenAccesoObjeto.ActualizarPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
                   }
                   else
                   {
                       //Se inserta el poducto
                       Resultado = TemporalPreOrdenAccesoObjeto.InsertarTemporalPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
                   }

                   if (Resultado.ErrorId == (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenGuardadoCorrectamente)
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
           catch (Exception EX )
           {
               Transaccion.Rollback();

               if(Conexion.State == ConnectionState.Open)
               { 
                   Conexion.Close(); 
               }
               Resultado.DescripcionError = EX.Message;
               return Resultado;
           
           }
       }

       public ResultadoEntidad SeleccionarPreOrdenDetalleTemp(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
       {
           string CadenaConexion = string.Empty;
           TemporalPreOrdenAcceso TemporalPreOrdenAccesoObjeto = new TemporalPreOrdenAcceso();
           ResultadoEntidad Resultado = new ResultadoEntidad();

           SqlTransaction Transaccion;
           SqlConnection Conexion;

           CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

           Conexion = new SqlConnection(CadenaConexion);
           Conexion.Open();

           Transaccion = Conexion.BeginTransaction();

           try
           {
               Resultado = TemporalPreOrdenAccesoObjeto.SeleccionarPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);

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

       //public ResultadoEntidad GuardarTemporalPreOrdenEncabezadoTemp(SqlConnection Conexion, SqlTransaction Transaccion, TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
       //{
       //    ResultadoEntidad Resultado = new ResultadoEntidad();
       //    TemporalPreOrdenAcceso TemporalPreOrdenAccesoObjeto = new TemporalPreOrdenAcceso();

       //    if (TemporalPreOrdenObjetoEntidad.PreOrdenId == "")
       //    {
       //        TemporalPreOrdenObjetoEntidad.PreOrdenId = Guid.NewGuid().ToString();              
       //        //TemporalPreOrdenObjetoEntidad.TemporalPreOrdenId = TemporalPreOrdenObjetoEntidad.PreOrdenId;

       //        Resultado = TemporalPreOrdenAccesoObjeto.InsertarTemporalPreOrdenEncabezadoTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
       //    }
       //    else 
       //    {
       //        //Resultado = TemporalPreOrdenAccesoObjeto.ActualizarTemporalPreOrdenEncabezadoTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
           
       //    }
       //    return Resultado;
       //}

       //public ResultadoEntidad GuardarTemporalPreOrdenDetalleTemp(SqlConnection Conexion, SqlTransaction Transaccion, TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
       //{
       //    ResultadoEntidad Resultado = new ResultadoEntidad();
       //    TemporalPreOrdenAcceso TemporalPreOrdenAccesoObjeto = new TemporalPreOrdenAcceso();

       //    if (BuscarTemporalPreOrdenDetalle(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad) == true)
       //    {
       //        Resultado = TemporalPreOrdenAccesoObjeto.InsertarTemporalPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
       //    }
       //    else
       //    {
       //        //Resultado = TemporalPreOrdenAccesoObjeto.ActualizarTemporalPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
       //    }
       //    return Resultado;
       //}



       //public ResultadoEntidad ValidarPreOrdenDuplicado(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
       //{
       //    ResultadoEntidad Resultado = new ResultadoEntidad();

       //    if (BuscarTemporalPreOrdenDetalle(TemporalPreOrdenObjetoEntidad) == true)
       //    {   // Se busca si ya existe una Preorden con ese numero de numero, si es diferente de nada
       //        Resultado.ErrorId = (int)ConstantePrograma.PreOrden.PreOrdenTieneRegistroDuplicado;
       //        Resultado.DescripcionError = TextoError.ProductoConNombreDuplicado;
       //        return Resultado;           }

         
       //    return Resultado;
       //}


    }
}
