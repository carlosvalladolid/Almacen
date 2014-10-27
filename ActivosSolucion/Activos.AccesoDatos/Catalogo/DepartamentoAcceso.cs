using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
using Activos.Comun.Constante;

namespace Activos.AccesoDatos.Catalogo
{
    public class DepartamentoAcceso : Base
    {
        public ResultadoEntidad ActualizarDepartamento(DepartamentoEntidad DepartamentoEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarDepartamentoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("DepartamentoId", SqlDbType.SmallInt);
                Parametro.Value = DepartamentoEntidadObjeto.DepartamentoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("DireccionId", SqlDbType.SmallInt);
                Parametro.Value = DepartamentoEntidadObjeto.DireccionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = DepartamentoEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdModifico", SqlDbType.SmallInt);
                Parametro.Value = DepartamentoEntidadObjeto.UsuarioIdModifico;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = DepartamentoEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Departamento.DepartamentoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad EliminarDepartamento(string CadenaDepartamentosId, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarDepartamentoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaDepartamentosId", SqlDbType.VarChar);
                Parametro.Value = CadenaDepartamentosId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Departamento.EliminacionExitosa;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarDepartamento(DepartamentoEntidad DepartamentoEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarDepartamentoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("DireccionId", SqlDbType.SmallInt);
                Parametro.Value = DepartamentoEntidadObjeto.DireccionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = DepartamentoEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = DepartamentoEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = DepartamentoEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Departamento.DepartamentoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarDepartamento(DepartamentoEntidad DepartamentoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarDepartamentoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("DepartamentoId", SqlDbType.SmallInt);
                Parametro.Value = DepartamentoEntidadObjeto.DepartamentoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("DireccionId", SqlDbType.SmallInt);
                Parametro.Value = DepartamentoEntidadObjeto.DireccionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = DepartamentoEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = DepartamentoEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("BusquedaRapida", SqlDbType.VarChar);
                Parametro.Value = DepartamentoEntidadObjeto.BusquedaRapida;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("BuscarNombre", SqlDbType.VarChar);
                Parametro.Value = DepartamentoEntidadObjeto.BuscarNombre;
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

        public ResultadoEntidad SeleccionarDepartamentoUsuariosRelacionados(string CadenaUsuarioId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarDepartamentoUsuariosRelacionadosProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaUsuarioId", SqlDbType.VarChar);
                Parametro.Value = CadenaUsuarioId;
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

        public ResultadoEntidad SeleccionarDepartamentoDireccionesRelacionados(string CadenaDireccionId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarDepartamentoDireccionesRelacionadosProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaDireccionId", SqlDbType.VarChar);
                Parametro.Value = CadenaDireccionId;
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
