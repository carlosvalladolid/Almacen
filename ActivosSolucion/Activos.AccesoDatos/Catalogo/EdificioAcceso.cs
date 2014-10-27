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
    public class EdificioAcceso : Base
    {

        public ResultadoEntidad ActualizarEdificio(EdificioEntidad EdificioEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarEdificioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("EdificioId", SqlDbType.SmallInt);
                Parametro.Value = EdificioEntidadObjeto.EdificioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CiudadId", SqlDbType.SmallInt);
                Parametro.Value = EdificioEntidadObjeto.CiudadId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = EdificioEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdModifico", SqlDbType.SmallInt);
                Parametro.Value = EdificioEntidadObjeto.UsuarioIdModifico;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Calle", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.Calle;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Numero", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.Numero;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Colonia", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.Colonia;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroInt", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.NumeroInt;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoPostal", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.CodigoPostal;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NombreArrendado", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.NombreArrendado;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TelefonoArrendado", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.TelefonoArrendado;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmailArrendado", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.EmailArrendado;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Edificio.EdificioGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad EliminarEdificio(string CadenaEdificioId, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarEdificioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaEdificioId", SqlDbType.VarChar);
                Parametro.Value = CadenaEdificioId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Edificio.EliminacionExitosa;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarEdificio(EdificioEntidad EdificioEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarEdificioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CiudadId", SqlDbType.SmallInt);
                Parametro.Value = EdificioEntidadObjeto.CiudadId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = EdificioEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = EdificioEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Calle", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.Calle;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Numero", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.Numero;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Colonia", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.Colonia;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroInt", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.NumeroInt;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoPostal", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.CodigoPostal;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NombreArrendado", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.NombreArrendado;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TelefonoArrendado", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.TelefonoArrendado;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmailArrendado", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.EmailArrendado;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Edificio.EdificioGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarEdificio(EdificioEntidad EdificioEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarEdificioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("EdificioId", SqlDbType.SmallInt);
                Parametro.Value = EdificioEntidadObjeto.EdificioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstadoId", SqlDbType.SmallInt);
                Parametro.Value = EdificioEntidadObjeto.EstadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CiudadId", SqlDbType.SmallInt);
                Parametro.Value = EdificioEntidadObjeto.CiudadId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = EdificioEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Calle", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.Calle;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoPostal", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.CodigoPostal;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("BusquedaRapida", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.BusquedaRapida;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("BuscarNombre", SqlDbType.VarChar);
                Parametro.Value = EdificioEntidadObjeto.BuscarNombre;
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

        public ResultadoEntidad SeleccionarEdificioUsuariosRelacionados(string CadenaUsuarioId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarEdificioUsuariosRelacionadosProcedimiento", Conexion);
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
    }
}
