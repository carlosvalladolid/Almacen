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
    public class TemporalMantenimientoEmpleadoAcceso : Base
    {
        public ResultadoEntidad EliminarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarTemporalMantenimientoEmpleadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.NChar);
                Parametro.Value = TemporalMantenimientoEmpleadoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalMantenimientoEmpleadoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalMantenimientoEmpleado.EliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarTemporalMantenimientoEmpleadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.NChar);
                Parametro.Value = TemporalMantenimientoEmpleadoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalMantenimientoEmpleadoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = TemporalMantenimientoEmpleadoEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalMantenimientoEmpleado.GuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarTemporalMantenimientoEmpleadoAnteriores(TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarTemporalMantenimientoEmpleadoAnterioresProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.NChar);
                Parametro.Value = TemporalMantenimientoEmpleadoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MantenimientoId", SqlDbType.Int);
                Parametro.Value = TemporalMantenimientoEmpleadoEntidadObjeto.MantenimientoId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalMantenimientoEmpleado.GuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarTemporalMantenimientoEmpleadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.NChar);
                Parametro.Value = TemporalMantenimientoEmpleadoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalMantenimientoEmpleadoEntidadObjeto.EmpleadoId;
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
