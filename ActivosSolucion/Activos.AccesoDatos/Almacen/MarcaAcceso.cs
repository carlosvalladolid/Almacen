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
   public class MarcaAcceso:Base
    {
       public ResultadoEntidad SeleccionarMarca(MarcaEntidad MarcaEntidadObjeto, string CadenaConexion)
       {
           DataSet ResultadoDatos = new DataSet();
           SqlConnection Conexion = new SqlConnection(CadenaConexion);
           SqlCommand Comando;
           SqlParameter Parametro;
           SqlDataAdapter Adaptador;
           ResultadoEntidad Resultado = new ResultadoEntidad();

           try
           {
               Comando = new SqlCommand("SeleccionarMarcaProcedimiento", Conexion);
               Comando.CommandType = CommandType.StoredProcedure;

               Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
               Parametro.Value = MarcaEntidadObjeto.MarcaId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("DependenciaId", SqlDbType.SmallInt);
               Parametro.Value = MarcaEntidadObjeto.DependenciaId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
               Parametro.Value = MarcaEntidadObjeto.Nombre;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
               Parametro.Value = MarcaEntidadObjeto.EstatusId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("BusquedaRapida", SqlDbType.VarChar);
               Parametro.Value = MarcaEntidadObjeto.BusquedaRapida;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("BuscarNombre", SqlDbType.VarChar);
               Parametro.Value = MarcaEntidadObjeto.BuscarNombre;
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
