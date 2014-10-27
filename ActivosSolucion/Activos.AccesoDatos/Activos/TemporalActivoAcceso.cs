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
    public class TemporalActivoAcceso : Base
    {

        public ResultadoEntidad ActualizarTemporalActivo(SqlConnection Conexion, SqlTransaction Transaccion, TemporalActivoEntidad TemporalActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarTemporalActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("TemporalActivoId", SqlDbType.Int);
                Parametro.Value = TemporalActivoEntidadObjeto.TemporalActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasGeneral", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.CodigoBarrasGeneral;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasParticular", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.CodigoBarrasParticular;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.CondicionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.TipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.SubFamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.MarcaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroSerie", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.NumeroSerie;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Modelo", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.Modelo;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Color", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.Color;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Monto", SqlDbType.Decimal);
                Parametro.Value = TemporalActivoEntidadObjeto.Monto;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UbicacionActivoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.UbicacionActivoId;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

                Resultado.ErrorId = (int)ConstantePrograma.TemporalActivo.TemporalActivoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad EliminarTemporalActivo(SqlConnection Conexion, SqlTransaction Transaccion, TemporalActivoEntidad TemporalActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarTemporalActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("TemporalCompraId", SqlDbType.Int);
                Parametro.Value = TemporalActivoEntidadObjeto.TemporalCompraId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TemporalActivoId", SqlDbType.Int);
                Parametro.Value = TemporalActivoEntidadObjeto.TemporalActivoId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalActivo.TemporalActivoEliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarTemporalActivo(SqlConnection Conexion, SqlTransaction Transaccion, TemporalActivoEntidad TemporalActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarTemporalActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("TemporalCompraId", SqlDbType.Int);
                Parametro.Value = TemporalActivoEntidadObjeto.TemporalCompraId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasGeneral", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.CodigoBarrasGeneral;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasParticular", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.CodigoBarrasParticular;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.CondicionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.TipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.SubFamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.MarcaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroSerie", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.NumeroSerie;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Modelo", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.Modelo;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Color", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.Color;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Monto", SqlDbType.Decimal);
                Parametro.Value = TemporalActivoEntidadObjeto.Monto;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UbicacionActivoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.UbicacionActivoId;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

                Resultado.ErrorId = (int)ConstantePrograma.TemporalActivo.TemporalActivoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarTemporalActivo(TemporalActivoEntidad TemporalActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarTemporalActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("TemporalActivoId", SqlDbType.Int);
                Parametro.Value = TemporalActivoEntidadObjeto.TemporalActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TemporalCompraId", SqlDbType.Int);
                Parametro.Value = TemporalActivoEntidadObjeto.TemporalCompraId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasGeneral", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.CodigoBarrasGeneral;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasParticular", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.CodigoBarrasParticular;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.CondicionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.TipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.SubFamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
                Parametro.Value = TemporalActivoEntidadObjeto.MarcaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroSerie", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.NumeroSerie;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Modelo", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.Modelo;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Color", SqlDbType.VarChar);
                Parametro.Value = TemporalActivoEntidadObjeto.Color;
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
