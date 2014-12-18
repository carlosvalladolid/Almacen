using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;

namespace Activos.AccesoDatos.Seguridad
{
    public class UsuarioAcceso : Base
    {
        public ResultadoEntidad ActualizarUsuario(UsuarioEntidad UsuarioEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad ResultadoObjetoEntidad = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarUsuarioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("UsuarioId", SqlDbType.SmallInt);
                Parametro.Value = UsuarioEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("RolId", SqlDbType.SmallInt);
                Parametro.Value = UsuarioEntidadObjeto.RolId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = UsuarioEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdModifico", SqlDbType.SmallInt);
                Parametro.Value = UsuarioEntidadObjeto.UsuarioIdModifico;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = UsuarioEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                Parametro.Value = UsuarioEntidadObjeto.ApellidoPaterno;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                Parametro.Value = UsuarioEntidadObjeto.ApellidoMaterno;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                ResultadoObjetoEntidad.ErrorId = (int)ConstantePrograma.Usuario.GuardadoExitoso;

                return ResultadoObjetoEntidad;
            }
            catch (SqlException Excepcion)
            {
                ResultadoObjetoEntidad.ErrorId = Excepcion.Number;
                ResultadoObjetoEntidad.DescripcionError = Excepcion.Message;

                return ResultadoObjetoEntidad;
            }
        }

        public ResultadoEntidad CambiarContrasenia(UsuarioEntidad UsuarioEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad ResultadoObjetoEntidad = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("CambiarContraseniaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("UsuarioId", SqlDbType.SmallInt);
                Parametro.Value = UsuarioEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Contrasenia", SqlDbType.VarChar);
                Parametro.Value = UsuarioEntidadObjeto.NuevaContrasenia;
                Comando.Parameters.Add(Parametro);

                Adaptador = new SqlDataAdapter(Comando);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                ResultadoObjetoEntidad.ErrorId = (int)ConstantePrograma.Usuario.GuardadoExitoso;

                return ResultadoObjetoEntidad;
            }
            catch (SqlException Excepcion)
            {
                ResultadoObjetoEntidad.ErrorId = Excepcion.Number;
                ResultadoObjetoEntidad.DescripcionError = Excepcion.Message;

                return ResultadoObjetoEntidad;
            }
        }

        public ResultadoEntidad EliminarUsuario(SqlConnection Conexion, SqlTransaction Transaccion, Int16 UsuarioId)
        {
            SqlCommand Comando = new SqlCommand();
            SqlParameter Parametro;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarUsuarioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("UsuarioId", SqlDbType.SmallInt);
                Parametro.Value = UsuarioId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Usuario.EliminacionExitosa;

                return ResultadoEntidadObjeto;
            }
            catch (SqlException Excepcion)
            {
                ResultadoEntidadObjeto.ErrorId = Excepcion.Number;
                ResultadoEntidadObjeto.DescripcionError = Excepcion.Message;

                return ResultadoEntidadObjeto;
            }
        }

        public ResultadoEntidad InsertarUsuario(UsuarioEntidad UsuarioEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad ResultadoObjetoEntidad = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarUsuarioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("RolId", SqlDbType.SmallInt);
                Parametro.Value = UsuarioEntidadObjeto.RolId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = UsuarioEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = UsuarioEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = UsuarioEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                Parametro.Value = UsuarioEntidadObjeto.ApellidoPaterno;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                Parametro.Value = UsuarioEntidadObjeto.ApellidoMaterno;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CuentaUsuario", SqlDbType.VarChar);
                Parametro.Value = UsuarioEntidadObjeto.CuentaUsuario;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Contrasenia", SqlDbType.VarChar);
                Parametro.Value = UsuarioEntidadObjeto.Contrasenia;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                ResultadoObjetoEntidad.ErrorId = (int)ConstantePrograma.Usuario.GuardadoExitoso;

                return ResultadoObjetoEntidad;
            }
            catch (SqlException Excepcion)
            {
                ResultadoObjetoEntidad.ErrorId = Excepcion.Number;
                ResultadoObjetoEntidad.DescripcionError = Excepcion.Message;

                return ResultadoObjetoEntidad;
            }
        }

        public ResultadoEntidad SeleccionarUsuario(UsuarioEntidad UsuarioEntidadObject, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarUsuarioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("UsuarioId", SqlDbType.SmallInt);
                Parametro.Value = UsuarioEntidadObject.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("RolId", SqlDbType.SmallInt);
                Parametro.Value = UsuarioEntidadObject.RolId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = UsuarioEntidadObject.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = UsuarioEntidadObject.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CuentaUsuario", SqlDbType.VarChar);
                Parametro.Value = UsuarioEntidadObject.CuentaUsuario;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("BusquedaRapida", SqlDbType.VarChar);
                Parametro.Value = UsuarioEntidadObject.BusquedaRapida;
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
