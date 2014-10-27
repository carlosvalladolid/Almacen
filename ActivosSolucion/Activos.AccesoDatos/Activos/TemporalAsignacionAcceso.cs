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
    public class TemporalAsignacionAcceso : Base
    {
        public ResultadoEntidad LimpiarTemporalTablaAsignacion(TemporalAsignacionEntidad TemporalAsignacionEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarTemporalAsignacionProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("TemporalAsignacionId", SqlDbType.Int);
                Parametro.Value = TemporalAsignacionEntidadObjeto.TemporalAsignacionId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalAsignacion.TemporalAsignacionEliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarTemporalAsignacion(SqlConnection Conexion, SqlTransaction Transaccion, TemporalAsignacionEntidad TemporalAsignacionEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarTemporalAsignacionProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("UsuarioId", SqlDbType.Int);
                Parametro.Value = TemporalAsignacionEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

                Resultado.ErrorId = (int)ConstantePrograma.TemporalAsignacion.TemporalAsignacionGuardadoCorrectamente;

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
