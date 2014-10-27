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
    public class AsignacionAcceso : Base
    {

        //public ResultadoEntidad SeleccionarAsignacion(AsignacionEntidad AsignacionEntidadObjeto, string CadenaConexion)
        //{
        //    DataSet ResultadoDatos = new DataSet();
        //    SqlConnection Conexion = new SqlConnection(CadenaConexion);
        //    SqlCommand Comando;
        //    SqlParameter Parametro;
        //    SqlDataAdapter Adaptador;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();

        //    try
        //    {
        //        Comando = new SqlCommand("SeleccionarAsignacionProcedimiento", Conexion);
        //        Comando.CommandType = CommandType.StoredProcedure;

        //        Parametro = new SqlParameter("ActivoId", SqlDbType.SmallInt);
        //        Parametro.Value = AsignacionEntidadObjeto.ActivoId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
        //        Parametro.Value = AsignacionEntidadObjeto.EmpleadoId;
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

        //public ResultadoEntidad SeleccionarAsignacionEmpleadosRelacionados(string CadenaEmpleadoId, string CadenaConexion)
        //{
        //    DataSet ResultadoDatos = new DataSet();
        //    SqlConnection Conexion = new SqlConnection(CadenaConexion);
        //    SqlCommand Comando;
        //    SqlParameter Parametro;
        //    SqlDataAdapter Adaptador;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();

        //    try
        //    {
        //        Comando = new SqlCommand("SeleccionarAsignacionEmpleadosRelacionadosProcedimiento", Conexion);
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

        //public ResultadoEntidad InsertarAsignacion(SqlConnection Conexion, SqlTransaction Transaccion, AsignacionEntidad AsignacionEntidadObjeto)
        //{
        //    SqlCommand Comando;
        //    SqlParameter Parametro;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();

        //    try
        //    {
        //        Comando = new SqlCommand("InsertarAsignacionProcedimiento", Conexion);
        //        Comando.CommandType = CommandType.StoredProcedure;

        //        Comando.Transaction = Transaccion;

        //        Parametro = new SqlParameter("ActivoId", SqlDbType.SmallInt);
        //        Parametro.Value = AsignacionEntidadObjeto.ActivoId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
        //        Parametro.Value = AsignacionEntidadObjeto.EmpleadoId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
        //        Parametro.Value = AsignacionEntidadObjeto.UsuarioIdInserto;
        //        Comando.Parameters.Add(Parametro);

        //        Comando.ExecuteNonQuery();

        //        Resultado.ErrorId = (int)ConstantePrograma.Asignacion.AsignacionGuardadoCorrectamente;

        //        return Resultado;
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        Resultado.ErrorId = sqlEx.Number;
        //        Resultado.DescripcionError = sqlEx.Message;

        //        return Resultado;
        //    }
        //}

    }
}
