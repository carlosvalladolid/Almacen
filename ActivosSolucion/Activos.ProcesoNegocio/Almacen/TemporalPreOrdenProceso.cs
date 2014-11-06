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
           string CadenaConexion = string.Empty;       
           ResultadoEntidad Resultado = new ResultadoEntidad();
           ResultadoEntidad ResultadoActivoDuplicado = new ResultadoEntidad();
           SqlTransaction Transaccion;
           SqlConnection Conexion;

           CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

           Conexion = new SqlConnection(CadenaConexion);
           Conexion.Open();

           Transaccion = Conexion.BeginTransaction();

           //TemporalPreOrdenObjetoEntidad.PreOrdenId = TemporalPre

           Resultado = GuardarTemporalPreOrdenEncabezadoTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);

           // Si el Producto temporal ENCABEZADO fue creado o editado exitosamente 
           if (Resultado.ErrorId == (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenGuardadoCorrectamente)
           {


               Resultado = GuardarTemporalPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);

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


       public ResultadoEntidad GuardarTemporalPreOrdenEncabezadoTemp(SqlConnection Conexion, SqlTransaction Transaccion, TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
       {
           ResultadoEntidad Resultado = new ResultadoEntidad();
           TemporalPreOrdenAcceso TemporalPreOrdenAccesoObjeto = new TemporalPreOrdenAcceso();

           if (TemporalPreOrdenObjetoEntidad.PreOrdenId == "")
           {
               TemporalPreOrdenObjetoEntidad.PreOrdenId = Guid.NewGuid().ToString();
               Resultado = TemporalPreOrdenAccesoObjeto.InsertarTemporalPreOrdenEncabezadoTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
           }
           //else
           //{
           //    //Resultado = TemporalPreOrdenAccesoObjeto.ActualizarTemporalPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
           //}
           return Resultado;
       }




       public ResultadoEntidad GuardarTemporalPreOrdenDetalleTemp(SqlConnection Conexion, SqlTransaction Transaccion, TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
       {
           ResultadoEntidad Resultado = new ResultadoEntidad();
           TemporalPreOrdenAcceso TemporalPreOrdenAccesoObjeto = new TemporalPreOrdenAcceso();

           if (TemporalPreOrdenObjetoEntidad.TemporalPreOrdenId == "0")
           {
               TemporalPreOrdenObjetoEntidad.PreOrdenId = "BUSCAR LA VARIABLE PARA INSERTAR";

               Resultado = TemporalPreOrdenAccesoObjeto.InsertarTemporalPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
           }
           //else
           //{
           //    //Resultado = TemporalPreOrdenAccesoObjeto.ActualizarTemporalPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
           //}
           return Resultado;
       }

    

    }
}
