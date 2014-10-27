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
    public class AccesorioAcceso : Base
    {
        public ResultadoEntidad ActualizarAccesorioPorTransferencia(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarAccesorioPorTransferenciaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("GrupoActivoAccesorioId", SqlDbType.VarChar);
                Parametro.Value = AccesorioEntidadObjeto.GrupoActivoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = AccesorioEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.Accesorio.AccesorioGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad EliminarAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarAccesorioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("AccesorioId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.AccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoAccesorioId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAccesorioId", SqlDbType.SmallInt);
                Parametro.Value = AccesorioEntidadObjeto.TipoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = AccesorioEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.Accesorio.AccesorioEliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarAccesorioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TemporalActivoId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.TemporalActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = AccesorioEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.Accesorio.AccesorioGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarAccesorioBajaTemporal(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = AccesorioEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.Accesorio.AccesorioGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }
        
        public ResultadoEntidad InsertarHistorialAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarHistorialAccesorioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("AccesorioId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.AccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoAccesorioId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAccesorioId", SqlDbType.SmallInt);
                Parametro.Value = AccesorioEntidadObjeto.TipoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = AccesorioEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = AccesorioEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.Accesorio.HistorialAccesorioGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarHistorialAccesorioPorTransferencia(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarHistorialAccesorioPorTransferenciaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("GrupoActivoAccesorioId", SqlDbType.VarChar);
                Parametro.Value = AccesorioEntidadObjeto.GrupoActivoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = AccesorioEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.Accesorio.HistorialAccesorioGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarAccesorio(AccesorioEntidad AccesorioEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarAccesorioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("AccesorioId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.AccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoAccesorioId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAccesorioId", SqlDbType.SmallInt);
                Parametro.Value = AccesorioEntidadObjeto.TipoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = AccesorioEntidadObjeto.Descripcion;
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

        public ResultadoEntidad SeleccionarAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioEntidadObjeto)
        {
            DataSet ResultadoDatos = new DataSet();
            
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarAccesorioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("AccesorioId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.AccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoAccesorioId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAccesorioId", SqlDbType.SmallInt);
                Parametro.Value = AccesorioEntidadObjeto.TipoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = AccesorioEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Adaptador = new SqlDataAdapter(Comando);
                ResultadoDatos = new DataSet();

                
                Adaptador.Fill(ResultadoDatos);

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

        public ResultadoEntidad SeleccionarAccesorioParaTransferir(AccesorioEntidad AccesorioEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarAccesorioParaTransferirProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("AccesorioId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.AccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoAccesorioId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAccesorioId", SqlDbType.SmallInt);
                Parametro.Value = AccesorioEntidadObjeto.TipoAccesorioId;
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

        public ResultadoEntidad SeleccionarAccesorioPorDocumento(AccesorioEntidad AccesorioEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarAccesorioPorDocumentoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CompraId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.CompraId;
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

        public ResultadoEntidad SeleccionarHistorialAccesorio(AccesorioEntidad AccesorioEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarHistorialAccesorioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoAccesorioId", SqlDbType.Int);
                Parametro.Value = AccesorioEntidadObjeto.ActivoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAccesorioId", SqlDbType.SmallInt);
                Parametro.Value = AccesorioEntidadObjeto.TipoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = AccesorioEntidadObjeto.Descripcion;
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
