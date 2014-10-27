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
    public class MovimientoAcceso : Base
    {
        public ResultadoEntidad ActualizarMovimientoTipoAsignacion(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarMovimientoAsignacionProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.Movimiento.MovimientoAsignacionEditadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarMovimientoTipoAlta(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarMovimientoAltaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.CondicionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UbicacionActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UbicacionActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.Movimiento.MovimientoAltaGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarMovimientoTipoAsignacion(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarMovimientoAsignacionProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.CondicionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UbicacionActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UbicacionActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.Movimiento.MovimientoAsignacionGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarMovimientoBaja(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarMovimientoBajaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ConstantePrograma.TipoMovimiento.Baja;
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

        public ResultadoEntidad SeleccionarMovimientoAsignacion(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarMovimientoAsignacionProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MostrarAsignadosSalida", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.MostrarAsignadosSalida;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoActivoId;
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

        public ResultadoEntidad SeleccionarMovimientoAsignacionMantenimientos(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarMovimientoAsignacionMantenimientoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("DepartamentoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.DepartamentoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SesionId", SqlDbType.NChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
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

        public ResultadoEntidad SeleccionarMovimientoReporteActivoGeneral(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarReporteActivoGeneralProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("FamiliaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.FamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.SubFamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.MarcaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Modelo", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Modelo;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("StrFechaInicio", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.StrFechaInicio;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("StrFechaFin", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.StrFechaFin;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ProveedorId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.ProveedorId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CompraFolio", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.CompraFolio;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("DireccionId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.DireccionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("DepartamentoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.DepartamentoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
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

        public ResultadoEntidad SeleccionarMovimientoReporteActivoEstatusAsignado(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarReporteActivoEstatusAsignadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("FamiliaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.FamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.SubFamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("AlmacenistaEmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.AlmacenistaEmpleadoId;
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

        public ResultadoEntidad SeleccionarMovimientoReporteActivoEstatusSalida(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarReporteActivoEstatusSalidaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("FamiliaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.FamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.SubFamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoServicioId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoServicioId;
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

        public ResultadoEntidad SeleccionarMovimientoReporteActivoPorEmpleado(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarReporteActivoPorEmpleadoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAccesorioId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoActivoId;
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

        public ResultadoEntidad SeleccionarMovimientoAsignacionAccesorios(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarMovimientoAsignacionAcceoriosProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoAccesorioId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoAccesorioId;
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

        public ResultadoEntidad InsertarMovimientoAsignacionDesdeTemporal(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarMovimientoAsignacionDesdeTemporalProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("EmpleadoID", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TemporalAsignacionId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.TemporalAsignacionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Movimiento.MovimientoAsignacionGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarMovimientoAsignacionGeneral(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarMovimientoAsignacionGeneralProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("ActivosXML", SqlDbType.Xml);
                Parametro.Value = ActivoEntidadObjeto.ActivosXML;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoID", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.Movimiento.MovimientoAsignacionGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarMovimientoPorActivo(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarMovimientoPorActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
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

        public ResultadoEntidad SeleccionarMovimientoPorAccesorio(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarMovimientoPorAccesorioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
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

        public ResultadoEntidad SeleccionarMovimientoPorDocumento(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarMovimientoPorDocumentoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CompraId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.CompraId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
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

        public ResultadoEntidad SeleccionarMovimientoSalida(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarMovimientoSalidaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ConstantePrograma.TipoMovimiento.Salida;
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

        public ResultadoEntidad SeleccionarMovimientoEmpleadosRelacionados(string CadenaEmpleadoId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarMovimientoEmpleadosRelacionadosProcedimiento", Conexion);
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

    }
}
