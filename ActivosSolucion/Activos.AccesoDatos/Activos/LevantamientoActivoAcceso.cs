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
    public class LevantamientoActivoAcceso : Base
    {

        //public ResultadoEntidad SeleccionarLevantamientoEmpleadosRelacionados(string CadenaEmpleadoId, string CadenaConexion)
        //{
        //    DataSet ResultadoDatos = new DataSet();
        //    SqlConnection Conexion = new SqlConnection(CadenaConexion);
        //    SqlCommand Comando;
        //    SqlParameter Parametro;
        //    SqlDataAdapter Adaptador;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();

        //    try
        //    {
        //        Comando = new SqlCommand("SeleccionarLevantamientoEmpleadosRelacionadosProcedimiento", Conexion);
        //        Comando.CommandType = CommandType.StoredProcedure;

        //        Parametro = new SqlParameter("CadenaEmpleadoId", SqlDbType.VarChar);
        //        Parametro.Value = CadenaEmpleadoId;
        //        Comando.Parameters.Add(Parametro);

        //        Adaptador = new SqlDataAdapter(Comando);
        //        ResultadoDatos = new DataSet();

        //        Conexion.Open();
        //        Adaptador.Fill(ResultadoDatos);
        //        Conexion.Close();

        //        Resultado.ResultadoDatos = ResultadoDatos;

        //        return Resultado;
        //    }
        //    catch (SqlException Excepcion)
        //    {
        //        Resultado.ErrorId = Excepcion.Number;
        //        Resultado.DescripcionError = Excepcion.Message;

        //        return Resultado;
        //    }
        //}

        //public ResultadoEntidad SeleccionarLevantamientoActivo(LevantamientoActivoEntidad LevantamientoActivoEntidadObjeto, string CadenaConexion)
        //{
        //    DataSet ResultadoDatos = new DataSet();
        //    SqlConnection Conexion = new SqlConnection(CadenaConexion);
        //    SqlCommand Comando;
        //    SqlParameter Parametro;
        //    SqlDataAdapter Adaptador;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();

        //    try
        //    {
        //        Comando = new SqlCommand("SeleccionarLevantamientoActivoProcedimiento", Conexion);
        //        //Comando = new SqlCommand("SeleccionarLevantamientoActivoProcedimiento", Conexion);
        //        Comando.CommandType = CommandType.StoredProcedure;

        //        Parametro = new SqlParameter("ActivoId", SqlDbType.SmallInt);
        //        Parametro.Value = LevantamientoActivoEntidadObjeto.ActivoId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("CodigoBarrasParticular", SqlDbType.VarChar);
        //        Parametro.Value = LevantamientoActivoEntidadObjeto.CodigoBarrasParticular;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
        //        Parametro.Value = LevantamientoActivoEntidadObjeto.EmpleadoId;
        //        Comando.Parameters.Add(Parametro);

        //        //Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
        //        //Parametro.Value = ActivoEntidadObjeto.Descripcion;
        //        //Comando.Parameters.Add(Parametro);

               
        //        Adaptador = new SqlDataAdapter(Comando);
        //        ResultadoDatos = new DataSet();

        //        Conexion.Open();
        //        Adaptador.Fill(ResultadoDatos);
        //        Conexion.Close();

        //        Resultado.ResultadoDatos = ResultadoDatos;

        //        return Resultado;
        //    }
        //    catch (SqlException Excepcion)
        //    {
        //        Resultado.ErrorId = Excepcion.Number;
        //        Resultado.DescripcionError = Excepcion.Message;

        //        return Resultado;
        //    }
        //}

        public ResultadoEntidad SeleccionarLevantamientoReporte(LevantamientoActivoEntidad LevantamientoActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarLevantamientoReporteProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("LevantamientoID", SqlDbType.Int);
                Parametro.Value = LevantamientoActivoEntidadObjeto.LevantamientoID;
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

        public ResultadoEntidad InsertarLevantamiento(SqlConnection Conexion, SqlTransaction Transaccion, LevantamientoActivoEntidad LevantamientoActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarLevantamientoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("LevantamientoID", SqlDbType.Int);
                Parametro.Value = LevantamientoActivoEntidadObjeto.LevantamientoID;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CadenaActivosXML", SqlDbType.Xml);
                Parametro.Value = LevantamientoActivoEntidadObjeto.CadenaActivosXML;
                Comando.Parameters.Add(Parametro);
                
                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.LevantamientoActivo.LevantamientoActivoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarLevantamientoEncabezado(SqlConnection Conexion, SqlTransaction Transaccion, LevantamientoActivoEntidad LevantamientoActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarLevantamientoEncabezadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("UsuarioId", SqlDbType.SmallInt);
                Parametro.Value = LevantamientoActivoEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.Int);
                Parametro.Value = LevantamientoActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

                Resultado.ErrorId = (int)ConstantePrograma.LevantamientoActivo.LevantamientoActivoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }
    
    }
}
