using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Activos;
using Activos.Comun.Constante;

namespace Activos.AccesoDatos.Activos
{
    public class TemporalCompraAcceso : Base
    {
        public ResultadoEntidad InsertarTemporalCompra(TemporalCompraEntidad TemporalCompraEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarTemporalCompraProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("UsuarioId", SqlDbType.SmallInt);
                Parametro.Value = TemporalCompraEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalCompra.TemporalCompraGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }
        
        public ResultadoEntidad LimpiarTemporalTabla(TemporalCompraEntidad TemporalCompraEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarTemporalRecepcionProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("TemporalCompraId", SqlDbType.Int);
                Parametro.Value = TemporalCompraEntidadObjeto.TemporalCompraId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalCompra.TemporalCompraEliminadoCorrectamente;

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
