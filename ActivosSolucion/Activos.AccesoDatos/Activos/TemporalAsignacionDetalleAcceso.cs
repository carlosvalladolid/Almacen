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
    public class TemporalAsignacionDetalleAcceso : Base
    {
        public ResultadoEntidad ActualizarTemporalAsignacionDetalle(SqlConnection Conexion, SqlTransaction Transaccion, TemporalAsignacionEntidad TemporalAsignacionEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarTemporalAsignacionDetalleProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("TemporalAsignacionDetalleId", SqlDbType.Int);
                Parametro.Value = TemporalAsignacionEntidadObjeto.TemporalAsignacionDetalleId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = TemporalAsignacionEntidadObjeto.CondicionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UbicacionActivoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalAsignacionEntidadObjeto.UbicacionActivoId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalAsignacion.TemporalAsignacionDetalleGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad EliminarTemporalAsignacionDetalle(TemporalAsignacionEntidad TemporalAsignacionEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarTemporalAsignacionDetalleProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("TemporalAsignacionDetalleId", SqlDbType.Int);
                Parametro.Value = TemporalAsignacionEntidadObjeto.TemporalAsignacionDetalleId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalAsignacion.TemporalAsignacionDetalleEliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarTemporalAsignacionDetalle(SqlConnection Conexion, SqlTransaction Transaccion, TemporalAsignacionEntidad TemporalAsignacionEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarTemporalAsignacionDetalleProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("TemporalAsignacionId", SqlDbType.Int);
                Parametro.Value = TemporalAsignacionEntidadObjeto.TemporalAsignacionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalAsignacionEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = TemporalAsignacionEntidadObjeto.CondicionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UbicacionActivoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalAsignacionEntidadObjeto.UbicacionActivoId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalAsignacion.TemporalAsignacionDetalleGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarTemporalAsignacionDetalle(TemporalAsignacionEntidad TemporalAsignacionEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarTemporalAsignacionDetalleProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("TemporalAsignacionDetalleId", SqlDbType.Int);
                Parametro.Value = TemporalAsignacionEntidadObjeto.TemporalAsignacionDetalleId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TemporalAsignacionId", SqlDbType.Int);
                Parametro.Value = TemporalAsignacionEntidadObjeto.TemporalAsignacionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalAsignacionEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = TemporalAsignacionEntidadObjeto.CondicionId;
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
