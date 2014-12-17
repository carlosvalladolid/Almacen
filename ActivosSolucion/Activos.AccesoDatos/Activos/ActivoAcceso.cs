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
    public class ActivoAcceso : Base
    {
        public ResultadoEntidad ActualizarActivoCodigo(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarActivoCodigoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("CadenaGeneralXML", SqlDbType.Xml);
                Parametro.Value = ActivoEntidadObjeto.CadenaGeneralXML;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CadenaParticularXML", SqlDbType.Xml);
                Parametro.Value = ActivoEntidadObjeto.CadenaParticularXML;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.Activo.ActivoEtiquetadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        //public ResultadoEntidad ActualizarActivoEstatus(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        //{
        //    SqlCommand Comando;
        //    SqlParameter Parametro;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();

        //    try
        //    {
        //        Comando = new SqlCommand("ActualizarActivoEstausProcedimiento", Conexion);
        //        Comando.CommandType = CommandType.StoredProcedure;

        //        Comando.Transaction = Transaccion;

        //        Parametro = new SqlParameter("TemporalCompraId", SqlDbType.Int);
        //        Parametro.Value = ActivoEntidadObjeto.TemporalCompraId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
        //        Parametro.Value = ActivoEntidadObjeto.EstatusId;
        //        Comando.Parameters.Add(Parametro);

        //        Comando.ExecuteNonQuery();

        //        Resultado.ErrorId = (int)ConstantePrograma.Activo.ActivoAsignadoCorrectamente;

        //        return Resultado;
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        Resultado.ErrorId = sqlEx.Number;
        //        Resultado.DescripcionError = sqlEx.Message;

        //        return Resultado;
        //    }
        //}

        //public ResultadoEntidad ActualizarActivoAsignar(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        //{
        //    SqlCommand Comando;
        //    SqlParameter Parametro;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();

        //    try
        //    {
        //        Comando = new SqlCommand("ActualizarActivoAsignarProcedimiento", Conexion);
        //        Comando.CommandType = CommandType.StoredProcedure;

        //        Comando.Transaction = Transaccion;

        //        Parametro = new SqlParameter("TemporalAsignacionId", SqlDbType.Int);
        //        Parametro.Value = ActivoEntidadObjeto.TemporalAsignacionId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
        //        Parametro.Value = ActivoEntidadObjeto.EstatusId;
        //        Comando.Parameters.Add(Parametro);

        //        Comando.ExecuteNonQuery();

        //        Resultado.ErrorId = (int)ConstantePrograma.Activo.ActivoAsignadoCorrectamente;

        //        return Resultado;
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        Resultado.ErrorId = sqlEx.Number;
        //        Resultado.DescripcionError = sqlEx.Message;

        //        return Resultado;
        //    }
        //}    

        public ResultadoEntidad ActualizarFechaBajaActivo(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarFechaBajaActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.BajaActivoCorrecta;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad ActualizarFechaSalida(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("ActualizarFechaSalidaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("FechaSalida", SqlDbType.SmallDateTime);
                Parametro.Value = ActivoEntidadObjeto.FechaMovimiento;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.EntradasSalidas.MovimientoCorrecto;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad EliminarActivoTemporalSeleccionado(ActivoEntidad ActivoEntidadObjeto, SqlConnection Conexion)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarTemporalMovimientoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("MovimientoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.MovimientoId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoEliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad EliminarTemporalBajaActivo(ActivoEntidad ActivoEntidadObjeto, SqlConnection Conexion)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarTemporalBajaActivo", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalBajaActivo.TemporalBajaActivoEliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad EliminarTemporalBajaActivoSeleccionado(ActivoEntidad ActivoEntidadObjeto, SqlConnection Conexion)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarTemporalBajaActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("MovimientoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.MovimientoId;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoEliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }
        
        public ResultadoEntidad EliminarTodosTemporalMovimiento(ActivoEntidad ActivoEntidadObjeto, SqlConnection Conexion)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("EliminarTodosTemporalMovimientoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoEliminadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarActivo(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("CompraId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.CompraId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TemporalActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.TemporalActivoId;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

                Resultado.ErrorId = (int)ConstantePrograma.Activo.ActivoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarBajaActivo(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarBajaActivo", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("MovimientoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.MovimientoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoBajaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoBajaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.BajaActivoCorrecta;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarMovimientoBaja(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarMovimientoBajaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoAutorizoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoResguardoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.CondicionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("FechaSalida", SqlDbType.SmallDateTime);
                Parametro.Value = ActivoEntidadObjeto.FechaMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Observacion", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.DescripcionMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

                Resultado.ErrorId = (int)ConstantePrograma.EntradasSalidas.MovimientoCorrecto;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarMovimientoEntradaSalida(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarMovimientoEntradaSalida", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoAutorizoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoResguardoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.CondicionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("FechaEntrada", SqlDbType.SmallDateTime);
                Parametro.Value = ActivoEntidadObjeto.FechaMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Observacion", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.DescripcionMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

                Resultado.ErrorId = (int)ConstantePrograma.EntradasSalidas.MovimientoCorrecto;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarMovimientoTemporal(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarMovimientoTemporal", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoAutorizoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoResguardoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.CondicionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("FechaSalida", SqlDbType.SmallDateTime);
                Parametro.Value = ActivoEntidadObjeto.FechaMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Observacion", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.DescripcionMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

                Resultado.ErrorId = (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarMovimientoTemporalSalida(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto) 
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarMovimientoTemporalSalida", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoAutorizoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoAutorizoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoResguardoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.CondicionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("FechaEntrada", SqlDbType.SmallDateTime);
                Parametro.Value = ActivoEntidadObjeto.FechaMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Observacion", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.DescripcionMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.UsuarioId;
                Comando.Parameters.Add(Parametro);

                Resultado.NuevoRegistroId = int.Parse(Comando.ExecuteScalar().ToString());

                Resultado.ErrorId = (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarSalidaActivo(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarSalidaActivo", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("MovimientoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.MovimientoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoServicioId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoServicioId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ProveedorId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ProveedorId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();
                Resultado.ErrorId = (int)ConstantePrograma.EntradasSalidas.MovimientoCorrecto;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarTipoBajaTemporal(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarTipoBajaTemporal", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("MovimientoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.MovimientoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoBaja", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoBaja;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarActivo(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                // Ojo: Este Stored Procedure se ejecuta ademas en la siguiente funcion en esta misma clase: SeleccionarActivoMismaConexion

                Comando = new SqlCommand("SeleccionarActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CompraId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.CompraId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.SubFamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.MarcaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasGeneral", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.CodigoBarrasGeneral;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasParticular", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.CodigoBarrasParticular;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroSerie", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.NumeroSerie;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Modelo", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Modelo;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Color", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Color;
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

        public ResultadoEntidad SeleccionarActivoAvanzado(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarActivoAvanzadaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("GrupoTipoActivoId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.GrupoTipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasParticular", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.CodigoBarrasParticular;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroSerie", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.NumeroSerie;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Modelo", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Modelo;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Color", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Color;
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

        public ResultadoEntidad SeleccionarActivoCompleto(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarActivoCompletoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CompraId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.CompraId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.SubFamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.MarcaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasGeneral", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.CodigoBarrasGeneral;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasParticular", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.CodigoBarrasParticular;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroSerie", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.NumeroSerie;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Modelo", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Modelo;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Color", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Color;
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

        public ResultadoEntidad SeleccionarActivoMarcasRelacionados(string CadenaMarcaId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarActivoMarcasRelacionadosProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaMarcaId", SqlDbType.VarChar);
                Parametro.Value = CadenaMarcaId;
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

        public ResultadoEntidad SeleccionarActivoMismaConexion(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                // Ojo: Este Stored Procedure se ejecuta ademas en la siguiente funcion en esta misma clase: SeleccionarActivo
                
                Comando = new SqlCommand("SeleccionarActivoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CompraId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.CompraId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.SubFamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.MarcaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasGeneral", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.CodigoBarrasGeneral;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasParticular", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.CodigoBarrasParticular;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroSerie", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.NumeroSerie;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Modelo", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Modelo;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Color", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Color;
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

        public ResultadoEntidad SeleccionarActivoPorCompra(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarActivoPorCompraProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CompraId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.CompraId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoID", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.SubFamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.MarcaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasGeneral", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.CodigoBarrasGeneral;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasParticular", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.CodigoBarrasParticular;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroSerie", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.NumeroSerie;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Modelo", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Modelo;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Color", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Color;
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

        public ResultadoEntidad SeleccionarActivoPorDocumento(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarActivoPorDocumentoProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CompraId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.CompraId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.SubFamiliaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.MarcaId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasGeneral", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.CodigoBarrasGeneral;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("CodigoBarrasParticular", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.CodigoBarrasParticular;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Descripcion;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("NumeroSerie", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.NumeroSerie;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Modelo", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Modelo;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Color", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.Color;
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

        //public ResultadoEntidad SeleccionarActivoPorEstatus(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        //{
        //    DataSet ResultadoDatos = new DataSet();
        //    SqlConnection Conexion = new SqlConnection(CadenaConexion);
        //    SqlCommand Comando;
        //    SqlParameter Parametro;
        //    SqlDataAdapter Adaptador;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();

        //    try
        //    {
        //        Comando = new SqlCommand("SeleccionarActivoPorEstatusProcedimiento", Conexion);
        //        Comando.CommandType = CommandType.StoredProcedure;

        //        Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
        //        Parametro.Value = ActivoEntidadObjeto.ActivoId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("CompraId", SqlDbType.SmallInt);
        //        Parametro.Value = ActivoEntidadObjeto.CompraId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("TipoActivoId", SqlDbType.SmallInt);
        //        Parametro.Value = ActivoEntidadObjeto.TipoActivoId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("SubFamiliaId", SqlDbType.SmallInt);
        //        Parametro.Value = ActivoEntidadObjeto.SubFamiliaId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("MarcaId", SqlDbType.SmallInt);
        //        Parametro.Value = ActivoEntidadObjeto.MarcaId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("CondicionId", SqlDbType.SmallInt);
        //        Parametro.Value = ActivoEntidadObjeto.CondicionId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("CodigoBarrasGeneral", SqlDbType.VarChar);
        //        Parametro.Value = ActivoEntidadObjeto.CodigoBarrasGeneral;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("CodigoBarrasParticular", SqlDbType.VarChar);
        //        Parametro.Value = ActivoEntidadObjeto.CodigoBarrasParticular;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("Descripcion", SqlDbType.VarChar);
        //        Parametro.Value = ActivoEntidadObjeto.Descripcion;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("NumeroSerie", SqlDbType.VarChar);
        //        Parametro.Value = ActivoEntidadObjeto.NumeroSerie;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("Modelo", SqlDbType.VarChar);
        //        Parametro.Value = ActivoEntidadObjeto.Modelo;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("Color", SqlDbType.VarChar);
        //        Parametro.Value = ActivoEntidadObjeto.Color;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("EmpleadoId", SqlDbType.SmallInt);
        //        Parametro.Value = ActivoEntidadObjeto.EmpleadoId;
        //        Comando.Parameters.Add(Parametro);

        //        Parametro = new SqlParameter("GrupoEstatus", SqlDbType.VarChar);
        //        Parametro.Value = ActivoEntidadObjeto.GrupoEstatus;
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

        public ResultadoEntidad SeleccionarActivoReporteEstatusSinAsignar(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarReporteActivoEstatusSinAsignarProcedimiento", Conexion);
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

        public ResultadoEntidad SeleccionarActivoReporteEstatusSinEtiquetar(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarReporteActivoEstatusSinEtiquetarProcedimiento", Conexion);
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

        public ResultadoEntidad SeleccionarActivoSubFamiliasRelacionadas(string CadenaSubFamiliaId, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarActivoSubFamiliasRelacionadasProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CadenaSubFamiliaId", SqlDbType.VarChar);
                Parametro.Value = CadenaSubFamiliaId;
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

        public ResultadoEntidad SeleccionarActivoTemporalMovimiento(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarActivoTemporalProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
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

        public ResultadoEntidad SeleccionarActivoTemporalMovimiento(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarActivoTemporalProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
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

        public ResultadoEntidad SeleccionarMovimientoTemporal(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarTemporalMovimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
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

        public ResultadoEntidad SeleccionarMovimientoTemporalParaAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            DataSet ResultadoDatos = new DataSet();
            
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarTemporalMovimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("SesionId", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("TipoMovimientoId", SqlDbType.SmallInt);
                Parametro.Value = ActivoEntidadObjeto.TipoDeMovimiento;
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

        public ResultadoEntidad SeleccionarMovimientoPorFecha(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarMovimientoPorFechaProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("ActivoId", SqlDbType.Int);
                Parametro.Value = ActivoEntidadObjeto.ActivoId;
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

        public ResultadoEntidad SeleccionarTemporalMovimientoPorSesionId(ActivoEntidad ActivoEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarTemporalMovimientoPorSesionId", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("SesionID", SqlDbType.VarChar);
                Parametro.Value = ActivoEntidadObjeto.SesionId;
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
    }
}
