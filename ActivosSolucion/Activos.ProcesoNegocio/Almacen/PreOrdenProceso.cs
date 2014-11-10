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
    public class PreOrdenProceso : Base
    {

       //public ResultadoEntidad GuardarPreOrden(PreOrdenEntidad PreOrdenObjetoEntidad)
       // {
       //     string CadenaConexion = string.Empty;
       //     ResultadoEntidad Resultado = new ResultadoEntidad();
       //     TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad = new TemporalPreOrdenEntidad();
       //     TemporalPreOrdenProceso TemporalPreOrdenProcesoNegocio = new TemporalPreOrdenProceso();           
          
       //     PreOrdenAcceso PreOrdenAccesoObjeto = new PreOrdenAcceso();
       //     SqlTransaction Transaccion;
       //     SqlConnection Conexion;


       //     CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);
       //     Conexion = new SqlConnection(CadenaConexion);
       //     Conexion.Open();
       //     Transaccion = Conexion.BeginTransaction();

       //     try
       //     {

       //         if (PreOrdenObjetoEntidad.PreOrdenId == "")
       //         {
       //             TemporalPreOrdenObjetoEntidad.TemporalPreOrdenId = PreOrdenObjetoEntidad.PreOrdenId;

       //             Resultado = PreOrdenAccesoObjeto.InsertarPreOrdenEncabezado(Conexion, Transaccion, PreOrdenObjetoEntidad);

       //             if (Resultado.ErrorId == (int)ConstantePrograma.PreOrden.PreOrdenGuardadoCorrectamente)
       //             {

       //                Resultado = PreOrdenAccesoObjeto.SeleccionarPreOrdenDetalle(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);

       //                 if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
       //                 {
       //                     //Se edita el poducto
       //                   //  Resultado = PreOrdenAccesoObjeto.ActualizaPreOrdenDetalle(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
       //                 }
       //                 else
       //                 {
       //                     //Se inserta el poducto
       //                     Resultado = PreOrdenAccesoObjeto.InsertarPreOrdenDetalle(Conexion, Transaccion, PreOrdenObjetoEntidad);
       //                 }

       //                 if (Resultado.ErrorId == (int)ConstantePrograma.PreOrden.PreOrdenGuardadoCorrectamente)
       //                 {
       //                     Transaccion.Commit();
       //                 }

       //                 else
       //                 {
       //                     Transaccion.Rollback();
       //                 }


       //             }

       //             else
       //             {
       //                 Transaccion.Rollback();
       //             }


       //         }
       //         else
       //         {
       //             Conexion.Close();
       //             return Resultado;
       //         }
            
       //     }
       //     catch (Exception EX)
       //     {
       //         Transaccion.Rollback();

       //         if (Conexion.State == ConnectionState.Open)
       //         {
       //             Conexion.Close();
       //         }
       //         Resultado.DescripcionError = EX.Message;
       //         return Resultado;

       //     }


        
       //   }







    }
}

