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
    public class EmpleadoAcceso : Base
    {

        public ResultadoEntidad ActualizarEmpleado(EmpleadoEntidad EmpleadoEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarEmpleadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CiudadId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.CiudadId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("DepartamentoId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.DepartamentoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EdificioId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.EdificioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoIdJefe", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.EmpleadoIdJefe;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("PuestoId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.PuestoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdModifico", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.UsuarioIdModifico;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.ApellidoPaterno;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.ApellidoMaterno;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("RFC", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.RFC;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Calle", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Calle;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Numero", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Numero;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Colonia", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Colonia;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoPostal", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.CodigoPostal;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TelefonoCasa", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.TelefonoCasa;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Celular", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Celular;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Email", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Email;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroEmpleado", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.NumeroEmpleado;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TelefonoTrabajo", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.TelefonoTrabajo;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TrabajoEmail", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.TrabajoEmail;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Empleado.EmpleadoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad EliminarEmpleado(string CadenaEmpleadoId, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarEmpleadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaEmpleadoId", SqlDbType.VarChar);
                Parametro.Value = CadenaEmpleadoId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Empleado.EliminacionExitosa;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarEmpleado(EmpleadoEntidad EmpleadoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarEmpleadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CiudadId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.CiudadId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("DireccionId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.DireccionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("DepartamentoId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.DepartamentoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EdificioId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.EdificioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoIdJefe", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.EmpleadoIdJefe;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("PuestoId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.PuestoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TrabajoEmail", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.TrabajoEmail;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("BusquedaRapida", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.BusquedaRapida;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroEmpleado", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.NumeroEmpleado;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("BuscarNombre", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.BuscarNombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("BuscarApellidoPaterno", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.BuscarApellidoPaterno;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("BuscarApellidoMaterno", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.BuscarApellidoMaterno;
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

        public ResultadoEntidad InsertarEmpleado(EmpleadoEntidad EmpleadoEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarEmpleadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CiudadId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.CiudadId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("DepartamentoId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.DepartamentoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EdificioId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.EdificioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoIdJefe", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.EmpleadoIdJefe;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("PuestoId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.PuestoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = EmpleadoEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.ApellidoPaterno;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.ApellidoMaterno;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("RFC", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.RFC;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Calle", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Calle;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Numero", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Numero;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Colonia", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Colonia;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoPostal", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.CodigoPostal;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TelefonoCasa", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.TelefonoCasa;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Celular", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Celular;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Email", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.Email;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroEmpleado", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.NumeroEmpleado;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TelefonoTrabajo", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.TelefonoTrabajo;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TrabajoEmail", SqlDbType.VarChar);
                Parametro.Value = EmpleadoEntidadObjeto.TrabajoEmail;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Empleado.EmpleadoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarEmpleadoUsuariosRelacionados(string CadenaUsuarioId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarEmpleadoUsuariosRelacionadosProcedimiento", Conexion);
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

        public ResultadoEntidad SeleccionarEmpleadoEdificioRelacionados(string CadenaEdificioId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarEmpleadoEdificioRelacionadosProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaEdificioId", SqlDbType.VarChar);
                Parametro.Value = CadenaEdificioId;
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

        public ResultadoEntidad SeleccionarEmpleadoDepartamentosRelacionados(string CadenaDepartamentoId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarEmpleadoDepartamentosRelacionadosProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaDepartamentoId", SqlDbType.VarChar);
                Parametro.Value = CadenaDepartamentoId;
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

        public ResultadoEntidad SeleccionarEmpleadoPuestosRelacionados(string CadenaPuestoId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarEmpleadoPuestosRelacionadosProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaPuestoId", SqlDbType.VarChar);
                Parametro.Value = CadenaPuestoId;
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
