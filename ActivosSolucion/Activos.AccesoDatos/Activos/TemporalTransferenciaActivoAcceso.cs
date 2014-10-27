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
    public class TemporalTransferenciaActivoAcceso : Base
    {

        public ResultadoEntidad EliminarTemporalTransferenciaActivo(TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarTemporalTransferenciaActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = TemporalTransferenciaActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalTransferenciaActivo.TemporalTransferenciaActivoEliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarTemporalTransferenciaActivo(SqlConnection Conexion, SqlTransaction Transaccion, TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarTemporalTransferenciaActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = TemporalTransferenciaActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = TemporalTransferenciaActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = TemporalTransferenciaActivoEntidadObjeto.CondicionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UbicacionActivoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalTransferenciaActivoEntidadObjeto.UbicacionActivoId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalTransferenciaActivo.TemporalTransferenciaActivoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarTemporalTransferenciaActivoPorDocumento(TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarTemporalTransferenciaActivoPorDocumentoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = TemporalTransferenciaActivoEntidadObjeto.SesionId;
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

        public ResultadoEntidad SeleccionarTemporalTransferenciaActivo(TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarTemporalTransferenciaActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = TemporalTransferenciaActivoEntidadObjeto.SesionId;
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
