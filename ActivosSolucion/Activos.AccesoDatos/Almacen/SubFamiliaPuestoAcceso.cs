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
   public class SubFamiliaPuestoAcceso:Base
    {
       public ResultadoEntidad SeleccionarSubFamiliaPuesto(SubFamiliaPuestoEntidad SubFamiliaPuestoEntidadObjeto, string CadenaConexion)
       {
           DataSet ResultadoDatos = new DataSet();
           SqlConnection Conexion = new SqlConnection(CadenaConexion);
           SqlCommand Comando;
           SqlParameter Parametro;
           SqlDataAdapter Adaptador;
           ResultadoEntidad Resultado = new ResultadoEntidad();

           try
           {
               Comando = new SqlCommand("SeleccionarPuestoProcedimiento", Conexion);
               Comando.CommandType = CommandType.StoredProcedure;

               //Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
               //Parametro.Value = SubFamiliaPuestoEntidadObjeto.SubFamiliaId;
               //Comando.Parameters.Add(Parametro);

               //Parametro = new SqlParameter("FamiliaId", SqlDbType.SmallInt);
               //Parametro.Value = SubFamiliaPuestoEntidadObjeto.FamiliaId;
               //Comando.Parameters.Add(Parametro);              

               Adaptador = new SqlDataAdapter(Comando);
               ResultadoDatos = new DataSet();

               Conexion.Open();
               Adaptador.Fill(ResultadoDatos);
               Conexion.Close();

               Resultado.ResultadoDatos = ResultadoDatos;

               return Resultado;
           }
           catch (SqlException Excepcion)
           {
               Resultado.ErrorId = Excepcion.Number;
               Resultado.DescripcionError = Excepcion.Message;

               return Resultado;
           }
       }


       public ResultadoEntidad InsertarSubFamiliaPuesto(SqlConnection Conexion, SqlTransaction Transaccion, SubFamiliaPuestoEntidad SubFamiliaPuestoEntidadObjeto)
       {
           SqlCommand Comando;
           SqlParameter Parametro;
           ResultadoEntidad Resultado = new ResultadoEntidad();

           try
           {
               Comando = new SqlCommand("InsertarSubFamiliaPuesto", Conexion);
               Comando.CommandType = CommandType.StoredProcedure;

               Comando.Transaction = Transaccion;

               Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
               Parametro.Value = SubFamiliaPuestoEntidadObjeto.SubFamiliaId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("CadenaPuestoXML", SqlDbType.Xml);
               Parametro.Value = SubFamiliaPuestoEntidadObjeto.CadenaPuestoXML;
               Comando.Parameters.Add(Parametro);

               Comando.ExecuteNonQuery();

               Resultado.ErrorId = (int)ConstantePrograma.SubFamilia.SubFamiliaGuardadoCorrectamente;

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
