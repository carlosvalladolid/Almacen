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
    public class MantenimientoEmpleadoAcceso : Base
    {

        public ResultadoEntidad EliminarMantenimientoEmpleado(SqlConnection Conexion, SqlTransaction Transaccion, MantenimientoEmpleadoEntidad MantenimientoEmpleadoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarMantenimientoEmpleadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("MantenimientoId", SqlDbType.Int);
                Parametro.Value = MantenimientoEmpleadoEntidadObjeto.MantenimientoId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.MantenimientoEmpleado.EliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarMantenimientoEmpleado(SqlConnection Conexion, SqlTransaction Transaccion, MantenimientoEmpleadoEntidad MantenimientoEmpleadoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarMantenimientoEmpleadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("SesionId", SqlDbType.NChar);
                Parametro.Value = MantenimientoEmpleadoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MantenimientoId", SqlDbType.Int);
                Parametro.Value = MantenimientoEmpleadoEntidadObjeto.MantenimientoId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.MantenimientoEmpleado.GuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarMantenimientoEmpleado(MantenimientoEmpleadoEntidad MantenimientoEmpleadoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarMantenimientoEmpleadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("MantenimientoId", SqlDbType.Int);
                Parametro.Value = MantenimientoEmpleadoEntidadObjeto.MantenimientoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = MantenimientoEmpleadoEntidadObjeto.EmpleadoId;
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
