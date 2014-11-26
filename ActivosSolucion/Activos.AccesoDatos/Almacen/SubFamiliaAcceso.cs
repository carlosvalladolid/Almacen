using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Activos.Entidad.General;
//using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.Comun.Constante;


namespace Activos.AccesoDatos.Almacen
{
   public class SubFamiliaAcceso:Base
    {
       public ResultadoEntidad SeleccionarSubFamilia(SubFamiliaEntidad SubFamiliaEntidadObjeto, string CadenaConexion)
       {
           DataSet ResultadoDatos = new DataSet();
           SqlConnection Conexion = new SqlConnection(CadenaConexion);
           SqlCommand Comando;
           SqlParameter Parametro;
           SqlDataAdapter Adaptador;
           ResultadoEntidad Resultado = new ResultadoEntidad();

           try
           {
               Comando = new SqlCommand("SeleccionarSubFamiliaProcedimiento", Conexion);
               Comando.CommandType = CommandType.StoredProcedure;

               Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
               Parametro.Value = SubFamiliaEntidadObjeto.SubFamiliaId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("FamiliaId", SqlDbType.SmallInt);
               Parametro.Value = SubFamiliaEntidadObjeto.FamiliaId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
               Parametro.Value = SubFamiliaEntidadObjeto.Nombre;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
               Parametro.Value = SubFamiliaEntidadObjeto.EstatusId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("BusquedaRapida", SqlDbType.VarChar);
               Parametro.Value = SubFamiliaEntidadObjeto.BusquedaRapida;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("BuscarNombre", SqlDbType.VarChar);
               Parametro.Value = SubFamiliaEntidadObjeto.BuscarNombre;
               Comando.Parameters.Add(Parametro);

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

       public ResultadoEntidad SeleccionarSubFamiliaFamiliaRelacionadas(string CadenaFamiliaId, string CadenaConexion)
       {
           DataSet ResultadoDatos = new DataSet();
           SqlConnection Conexion = new SqlConnection(CadenaConexion);
           SqlCommand Comando;
           SqlParameter Parametro;
           SqlDataAdapter Adaptador;
           ResultadoEntidad Resultado = new ResultadoEntidad();

           try
           {
               Comando = new SqlCommand("SeleccionarSubfamiliaFamiliasRelacionadasProcedimiento", Conexion);
               Comando.CommandType = CommandType.StoredProcedure;

               Parametro = new SqlParameter("CadenaFamiliaId", SqlDbType.VarChar);
               Parametro.Value = CadenaFamiliaId;
               Comando.Parameters.Add(Parametro);

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
    }
}
