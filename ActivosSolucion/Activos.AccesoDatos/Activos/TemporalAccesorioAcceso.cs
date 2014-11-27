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
    public class TemporalAccesorioAcceso : Base
    {
        public ResultadoEntidad ActualizarTemporalAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, TemporalAccesorioEntidad TemporalAccesorioEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarTemporalAccesorioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("TemporalAccesorioId", SqlDbType.Int);
                Parametro.Value = TemporalAccesorioEntidadObjeto.TemporalAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TemporalActivoId", SqlDbType.Int);
                Parametro.Value = TemporalAccesorioEntidadObjeto.TemporalActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Estatus", SqlDbType.SmallInt);
                Parametro.Value = TemporalAccesorioEntidadObjeto.Estatus;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("GrupoEstatus", SqlDbType.VarChar);
                Parametro.Value = TemporalAccesorioEntidadObjeto.GrupoEstatus;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEditadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad EliminarTemporalAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, TemporalAccesorioEntidad TemporalAccesorioEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarTemporalAccesorioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("TemporalAccesorioId", SqlDbType.Int);
                Parametro.Value = TemporalAccesorioEntidadObjeto.TemporalAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TemporalActivoId", SqlDbType.Int);
                Parametro.Value = TemporalAccesorioEntidadObjeto.TemporalActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("GrupoEstatus", SqlDbType.VarChar);
                Parametro.Value = TemporalAccesorioEntidadObjeto.GrupoEstatus;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarTemporalAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, TemporalAccesorioEntidad TemporalAccesorioEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarTemporalAccesorioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("TemporalActivoId", SqlDbType.Int);
                Parametro.Value = TemporalAccesorioEntidadObjeto.TemporalActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAccesorioId", SqlDbType.SmallInt);
                Parametro.Value = TemporalAccesorioEntidadObjeto.TipoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoAccesorioId", SqlDbType.Int);
                Parametro.Value = TemporalAccesorioEntidadObjeto.ActivoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = TemporalAccesorioEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Estatus", SqlDbType.SmallInt);
                Parametro.Value = TemporalAccesorioEntidadObjeto.Estatus;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

                Resultado.ErrorId = (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarTemporalAccesorio(TemporalAccesorioEntidad TemporalAccesorioEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarTemporalAccesorioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("TemporalAccesorioId", SqlDbType.Int);
                Parametro.Value = TemporalAccesorioEntidadObjeto.TemporalAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TemporalActivoId", SqlDbType.Int);
                Parametro.Value = TemporalAccesorioEntidadObjeto.TemporalActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAccesorioId", SqlDbType.SmallInt);
                Parametro.Value = TemporalAccesorioEntidadObjeto.TipoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoAccesorioId", SqlDbType.Int);
                Parametro.Value = TemporalAccesorioEntidadObjeto.ActivoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("GrupoEstatus", SqlDbType.VarChar);
                Parametro.Value = TemporalAccesorioEntidadObjeto.GrupoEstatus;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = TemporalAccesorioEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TemporalCompraId", SqlDbType.Int);
                Parametro.Value = TemporalAccesorioEntidadObjeto.TemporalCompraId;
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
