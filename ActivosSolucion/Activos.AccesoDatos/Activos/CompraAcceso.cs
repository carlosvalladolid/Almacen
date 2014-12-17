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
    public class CompraAcceso : Base
    {
        public ResultadoEntidad InsertarCompra(SqlConnection Conexion, SqlTransaction Transaccion, CompraEntidad CompraEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarCompraProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = CompraEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("JefeId", SqlDbType.SmallInt);
                Parametro.Value = CompraEntidadObjeto.JefeId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ProveedorId", SqlDbType.SmallInt);
                Parametro.Value = CompraEntidadObjeto.ProveedorId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoDocumentoId", SqlDbType.SmallInt);
                Parametro.Value = CompraEntidadObjeto.TipoDocumentoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = CompraEntidadObjeto.UsuarioIdInserto;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CompraFolio", SqlDbType.VarChar);
                Parametro.Value = CompraEntidadObjeto.CompraFolio;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("FechaCompra", SqlDbType.VarChar);
                Parametro.Value = CompraEntidadObjeto.FechaCompra;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Monto", SqlDbType.Decimal);
                Parametro.Value = CompraEntidadObjeto.Monto;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("OrdenCompra", SqlDbType.VarChar);
                Parametro.Value = CompraEntidadObjeto.OrdenCompra;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("FechaOc", SqlDbType.VarChar);
                Parametro.Value = CompraEntidadObjeto.FechaOc;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

                Resultado.ErrorId = (int)ConstantePrograma.Compra.CompraGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarCompra(CompraEntidad CompraEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarCompraProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CompraId", SqlDbType.SmallInt);
                Parametro.Value = CompraEntidadObjeto.CompraId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = CompraEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("JefeId", SqlDbType.SmallInt);
                Parametro.Value = CompraEntidadObjeto.JefeId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ProveedorId", SqlDbType.SmallInt);
                Parametro.Value = CompraEntidadObjeto.ProveedorId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoDocumentoId", SqlDbType.SmallInt);
                Parametro.Value = CompraEntidadObjeto.TipoDocumentoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CompraFolio", SqlDbType.VarChar);
                Parametro.Value = CompraEntidadObjeto.CompraFolio;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("OrdenCompra", SqlDbType.VarChar);
                Parametro.Value = CompraEntidadObjeto.OrdenCompra;
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

        public ResultadoEntidad SeleccionarCompraEmpleadosRelacionados(string CadenaEmpleadoId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarCompraEmpleadosRelacionadosProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaEmpleadoId", SqlDbType.VarChar);
                Parametro.Value = CadenaEmpleadoId;
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

        public ResultadoEntidad SeleccionarCompraProveedoresRelacionados(string CadenaProveedorId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarCompraProveedoresRelacionadosProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaProveedorId", SqlDbType.VarChar);
                Parametro.Value = CadenaProveedorId;
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
