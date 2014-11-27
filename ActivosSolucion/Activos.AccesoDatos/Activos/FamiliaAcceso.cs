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
    public class FamiliaAcceso : Base
    {
        public ResultadoEntidad ActualizarFamilia(FamiliaEntidad FamiliaEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarFamiliaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("FamiliaId", SqlDbType.SmallInt);
                Parametro.Value = FamiliaEntidadObjeto.FamiliaId;
                Comando.Parameters.Add(Parametro);
           
                Parametro = new SqlParameter("DependenciaId", SqlDbType.SmallInt);
                Parametro.Value = FamiliaEntidadObjeto.DependenciaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = FamiliaEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = FamiliaEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdModifico", SqlDbType.SmallInt);
                Parametro.Value = FamiliaEntidadObjeto.UsuarioIdModifico;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Familia.FamiliaGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad EliminarFamilia(string CadenaFamiliaId, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarFamiliaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaFamiliaId", SqlDbType.VarChar);
                Parametro.Value = CadenaFamiliaId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Familia.EliminacionExitosa;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarFamilia(FamiliaEntidad FamiliaEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarFamiliaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("DependenciaId", SqlDbType.SmallInt);
                Parametro.Value = FamiliaEntidadObjeto.DependenciaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = FamiliaEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = FamiliaEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = FamiliaEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Familia.FamiliaGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarFamilia(FamiliaEntidad FamiliaEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarFamiliaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("FamiliaId", SqlDbType.SmallInt);
                Parametro.Value = FamiliaEntidadObjeto.FamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("DependenciaId", SqlDbType.SmallInt);
                Parametro.Value = FamiliaEntidadObjeto.DependenciaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                Parametro.Value = FamiliaEntidadObjeto.Nombre;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = FamiliaEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("BusquedaRapida", SqlDbType.VarChar);
                Parametro.Value = FamiliaEntidadObjeto.BusquedaRapida;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("BuscarNombre", SqlDbType.VarChar);
                Parametro.Value = FamiliaEntidadObjeto.BuscarNombre;
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

        public ResultadoEntidad SeleccionarFamiliaUsuariosRelacionados(string CadenaUsuarioId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarFamiliaUsuariosRelacionadosProcedimiento", Conexion);
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
