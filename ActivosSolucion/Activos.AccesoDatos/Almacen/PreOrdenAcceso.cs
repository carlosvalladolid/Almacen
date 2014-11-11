using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.Comun.Constante;


namespace Activos.AccesoDatos.Almacen
{
   public class PreOrdenAcceso:Base
    {

        public ResultadoEntidad InsertarPreOrdenDetalle(SqlConnection Conexion, SqlTransaction Transaccion, PreOrdenEntidad PreOrdenEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarPreOrdenDetalleProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                Parametro.Value = PreOrdenEntidadObjeto.PreOrdenId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();
                Resultado.ErrorId = (int)ConstantePrograma.PreOrden.PreOrdenGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarPreOrdenEncabezado(SqlConnection Conexion, SqlTransaction Transaccion, PreOrdenEntidad PreOrdenEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarPreOrdenEncabezadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                Parametro.Value = PreOrdenEntidadObjeto.PreOrdenId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.PreOrden.PreOrdenGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

       public ResultadoEntidad SeleccionarPreOrdenDetalle(SqlConnection Conexion, SqlTransaction Transaccion, TemporalPreOrdenEntidad TempPreOrdenDetalleObjetoEntidad)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarPreOrdenDetalleProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("PreOrdenId", SqlDbType.VarChar);
                Parametro.Value = TempPreOrdenDetalleObjetoEntidad.PreOrdenId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                Parametro.Value = TempPreOrdenDetalleObjetoEntidad.ProductoId;
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




    }
}
