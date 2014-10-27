using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Activos.Entidad.General;
using Activos.Entidad.Mantenimiento;
using Activos.Entidad.Activos;
using Activos.Comun.Constante;

namespace Activos.AccesoDatos.Mantenimiento
{
    public class TemporalMantenimientoActivoAcceso : Base
    {

        public ResultadoEntidad EliminarTemporalMantenimientoActivo(TemporalMantenimientoActivoEntidad TemporalMantenimientoActivoEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarTemporalMantenimientoActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.NChar);
                Parametro.Value = TemporalMantenimientoActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TemporalMantenimientoActivoId", SqlDbType.Int);
                Parametro.Value = TemporalMantenimientoActivoEntidadObjeto.TemporalMantenimientoActivoId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalMantenimientoActivo.EliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarTemporalMantenimientoActivo(TemporalMantenimientoActivoEntidad TemporalMantenimientoActivoEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarTemporalMantenimientoActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("ActivosXML", SqlDbType.Xml);
                Parametro.Value = TemporalMantenimientoActivoEntidadObjeto.ActivosXML;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SesionId", SqlDbType.NChar);
                Parametro.Value = TemporalMantenimientoActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAsistenciaId", SqlDbType.SmallInt);
                Parametro.Value = TemporalMantenimientoActivoEntidadObjeto.TipoAsistenciaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMantenimientoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalMantenimientoActivoEntidadObjeto.TipoMantenimientoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = TemporalMantenimientoActivoEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = TemporalMantenimientoActivoEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalMantenimientoActivo.GuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarTemporalMantenimientoActivo(TemporalMantenimientoActivoEntidad TemporalMantenimientoActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarTemporalMantenimientoActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.NChar);
                Parametro.Value = TemporalMantenimientoActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = TemporalMantenimientoActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAsistenciaId", SqlDbType.SmallInt);
                Parametro.Value = TemporalMantenimientoActivoEntidadObjeto.TipoAsistenciaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = TemporalMantenimientoActivoEntidadObjeto.EstatusId;
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
