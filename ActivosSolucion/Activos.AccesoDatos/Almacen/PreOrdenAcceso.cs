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
   public class PreOrdenAcceso:Base
    {

       public ResultadoEntidad InsertarPreOrdenEncabezadoProcedimiento(PreOrdenEntidad PreOrdenEntidadObjeto, string CadenaConexion)
       {
           SqlConnection Conexion = new SqlConnection(CadenaConexion);
           SqlCommand Comando;
           SqlParameter Parametro;
           ResultadoEntidad Resultado = new ResultadoEntidad();

           try
           {
               Comando = new SqlCommand("InsertarPreOrdenEncabezadoProcedimiento", Conexion);
               Comando.CommandType = CommandType.StoredProcedure;


               Parametro = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
               Parametro.Value = PreOrdenEntidadObjeto.PreOrdenId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("EmpleadoId", SqlDbType.Int);
               Parametro.Value = PreOrdenEntidadObjeto.EmpleadoId;
               Comando.Parameters.Add(Parametro);


               Parametro = new SqlParameter("JefeId", SqlDbType.Int);
               Parametro.Value = PreOrdenEntidadObjeto.JefeId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
               Parametro.Value = PreOrdenEntidadObjeto.EstatusId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("EstatusId", SqlDbType.Int);
               Parametro.Value = PreOrdenEntidadObjeto.EstatusId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Clave", SqlDbType.VarChar);
               Parametro.Value = PreOrdenEntidadObjeto.Clave;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
               Parametro.Value = PreOrdenEntidadObjeto.ProductoId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Cantidad", SqlDbType.Int);
               Parametro.Value = PreOrdenEntidadObjeto.Cantidad;
               Comando.Parameters.Add(Parametro);            

               Conexion.Open();
               Comando.ExecuteNonQuery();
               Conexion.Close();

               Resultado.ErrorId = (int)ConstantePrograma.PreOrden.PreOrdenGuardadoCorrectamente;

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
