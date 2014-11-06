using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.Comun.Constante;

namespace Activos.AccesoDatos.Almacen
{
   public class TemporalPreOrdenAcceso:Base
    {

       public ResultadoEntidad InsertarTemporalPreOrdenDetalleTemp(SqlConnection Conexion, SqlTransaction Transaccion, TemporalPreOrdenEntidad TemporalPreOrdenEntidadObjeto)
       {
           SqlCommand Comando;
           SqlParameter Parametro;
           ResultadoEntidad Resultado = new ResultadoEntidad();

           try
           {
               Comando = new SqlCommand("InsertarPreOrdenDetalleTempProcedimiento", Conexion);
               Comando.CommandType = CommandType.StoredProcedure;

               Comando.Transaction = Transaccion;

               Parametro = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
               Parametro.Value = TemporalPreOrdenEntidadObjeto.PreOrdenId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
               Parametro.Value = TemporalPreOrdenEntidadObjeto.ProductoId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Cantidad", SqlDbType.Int);
               Parametro.Value = TemporalPreOrdenEntidadObjeto.Cantidad;
               Comando.Parameters.Add(Parametro);


               Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

               Resultado.ErrorId = (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenGuardadoCorrectamente;

               return Resultado;
           }
           catch (SqlException sqlEx)
           {
               Resultado.ErrorId = sqlEx.Number;
               Resultado.DescripcionError = sqlEx.Message;

               return Resultado;
           }
       }

       public ResultadoEntidad InsertarTemporalPreOrdenEncabezadoTemp(SqlConnection Conexion, SqlTransaction Transaccion, TemporalPreOrdenEntidad TemporalPreOrdenEntidadObjeto)
       {
           SqlCommand Comando;
           SqlParameter Parametro;
           ResultadoEntidad Resultado = new ResultadoEntidad();

           try
           {
               Comando = new SqlCommand("InsertarPreOrdenEncabezadoTempProcedimiento", Conexion);
               Comando.CommandType = CommandType.StoredProcedure;

               Comando.Transaction = Transaccion;

               Parametro = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
               Parametro.Value = TemporalPreOrdenEntidadObjeto.PreOrdenId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("EmpleadoId", SqlDbType.Int);
               Parametro.Value = TemporalPreOrdenEntidadObjeto.EmpleadoId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("JefeId", SqlDbType.Int);
               Parametro.Value = TemporalPreOrdenEntidadObjeto.JefeId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
               Parametro.Value = TemporalPreOrdenEntidadObjeto.EstatusId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Clave", SqlDbType.Int);
               Parametro.Value = TemporalPreOrdenEntidadObjeto.Clave;
               Comando.Parameters.Add(Parametro);

               
               Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

               Resultado.ErrorId = (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenGuardadoCorrectamente;

               return Resultado;
           }
           catch (SqlException sqlEx)
           {
               Resultado.ErrorId = sqlEx.Number;
               Resultado.DescripcionError = sqlEx.Message;

               return Resultado;
           }
       }
       
       
    }
}
