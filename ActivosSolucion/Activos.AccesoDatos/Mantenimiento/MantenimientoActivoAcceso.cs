using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Activos.Entidad.General;
using Activos.Entidad.Activos;
using Activos.Entidad.Mantenimiento;
using Activos.Comun.Constante;

namespace Activos.AccesoDatos.Mantenimiento
{
    public class MantenimientoActivoAcceso : Base
    {
        public ResultadoEntidad InsertarMantenimientoActivo(SqlConnection Conexion, SqlTransaction Transaccion, MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarMantenimientoActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("SesionId", SqlDbType.NChar);
                Parametro.Value = MantenimientoActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MantenimientoId", SqlDbType.Int);
                Parametro.Value = MantenimientoActivoEntidadObjeto.MantenimientoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.MantenimientoActivo.GuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarMantenimientoActivo(MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarMantenimientoActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.NChar);
                Parametro.Value = MantenimientoActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MantenimientoActivoId", SqlDbType.Int);
                Parametro.Value = MantenimientoActivoEntidadObjeto.MantenimientoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MantenimientoId", SqlDbType.Int);
                Parametro.Value = MantenimientoActivoEntidadObjeto.MantenimientoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = MantenimientoActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAsistenciaId", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.TipoAsistenciaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.EstatusId;
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

        public ResultadoEntidad SeleccionarMantenimientoReporteGeneral(MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarReporteMantenimientoGeneralProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("StrFechaInicio", SqlDbType.VarChar);
                Parametro.Value = MantenimientoActivoEntidadObjeto.StrFechaInicio;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("StrFechaFin", SqlDbType.VarChar);
                Parametro.Value = MantenimientoActivoEntidadObjeto.StrFechaFin;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoReporte", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.TipoReporte;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMantenimientoId", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.TipoMantenimientoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.TipoMovimientoId;
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

        public ResultadoEntidad SeleccionarMantenimientoReportePorActivo(MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarReporteMantenimientoPorActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("StrFechaInicio", SqlDbType.VarChar);
                Parametro.Value = MantenimientoActivoEntidadObjeto.StrFechaInicio;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("StrFechaFin", SqlDbType.VarChar);
                Parametro.Value = MantenimientoActivoEntidadObjeto.StrFechaFin;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = MantenimientoActivoEntidadObjeto.ActivoId;
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

        public ResultadoEntidad SeleccionarMantenimientoReportePorTecnico(MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarReporteMantenimientoPorTecnicoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("StrFechaInicio", SqlDbType.VarChar);
                Parametro.Value = MantenimientoActivoEntidadObjeto.StrFechaInicio;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("StrFechaFin", SqlDbType.VarChar);
                Parametro.Value = MantenimientoActivoEntidadObjeto.StrFechaFin;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoReporte", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.TipoReporte;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMantenimientoId", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.TipoMantenimientoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.TipoMovimientoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoActivoEntidadObjeto.EmpleadoId;
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
