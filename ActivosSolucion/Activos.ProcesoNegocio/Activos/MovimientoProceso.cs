using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Catalogo;
using Activos.AccesoDatos.Activos;
using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Activos;

namespace Activos.ProcesoNegocio.Activos
{
    public class MovimientoProceso : Base
    {
        public ResultadoEntidad ActualizarMovimientoTipoAsignacion(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            Resultado = MovimientoAccesoObjeto.ActualizarMovimientoTipoAsignacion(Conexion, Transaccion, ActivoEntidadObjeto);

            return Resultado;
        }

        public ResultadoEntidad InsertarMovimientoTipoAlta(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            Resultado = MovimientoAccesoObjeto.InsertarMovimientoTipoAlta(Conexion, Transaccion, ActivoEntidadObjeto);

            return Resultado;
        }

        public ResultadoEntidad InsertarMovimientoTipoAsignacion(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoEntidadObjeto)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            Resultado = MovimientoAccesoObjeto.InsertarMovimientoTipoAsignacion(Conexion, Transaccion, ActivoEntidadObjeto);

            return Resultado;
        }

        public ResultadoEntidad InsertarMovimientoAsignacionDesdeTemporal(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MovimientoAccesoObjeto.InsertarMovimientoAsignacionDesdeTemporal(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad InsertarMovimientoAsignacionGeneral(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MovimientoAccesoObjeto.InsertarMovimientoAsignacionGeneral(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }  

        public ResultadoEntidad SeleccionarMovimientoBaja(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            Resultado = MovimientoAccesoObjeto.SeleccionarMovimientoBaja(Conexion, Transaccion, ActivoObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoBaja(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            SqlTransaction Transaccion;
            SqlConnection Conexion;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();
            Transaccion = Conexion.BeginTransaction();

            Resultado = SeleccionarMovimientoBaja(Conexion, Transaccion, ActivoObjetoEntidad);

            Transaccion.Commit();

            Conexion.Close();

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoAsignacion(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            Resultado = MovimientoAccesoObjeto.SeleccionarMovimientoAsignacion(Conexion, Transaccion, ActivoObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoAsignacionAccesorios(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MovimientoAccesoObjeto.SeleccionarMovimientoAsignacionAccesorios(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoPorDocumento(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MovimientoAccesoObjeto.SeleccionarMovimientoPorDocumento(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoPorActivo(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MovimientoAccesoObjeto.SeleccionarMovimientoPorActivo(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoPorAccesorio(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MovimientoAccesoObjeto.SeleccionarMovimientoPorAccesorio(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoAsignacionMantenimientos(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MovimientoAccesoObjeto.SeleccionarMovimientoAsignacionMantenimientos(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoReporteActivoGeneral(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MovimientoAccesoObjeto.SeleccionarMovimientoReporteActivoGeneral(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoReporteActivoEstatusAsignado(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MovimientoAccesoObjeto.SeleccionarMovimientoReporteActivoEstatusAsignado(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoReporteActivoEstatusSalida(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MovimientoAccesoObjeto.SeleccionarMovimientoReporteActivoEstatusSalida(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoReporteActivoPorEmpleado(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MovimientoAccesoObjeto.SeleccionarMovimientoReporteActivoPorEmpleado(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarAsignacionPorEmpleado(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            SqlTransaction Transaccion;
            SqlConnection Conexion;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();
            Transaccion = Conexion.BeginTransaction();

            Resultado = SeleccionarMovimientoAsignacion(Conexion, Transaccion, ActivoObjetoEntidad);

            Conexion.Close();

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoSalida(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            Resultado = MovimientoAccesoObjeto.SeleccionarMovimientoSalida(Conexion, Transaccion, ActivoObjetoEntidad);

            return Resultado;
        }

        public bool SeleccionarMovimientoEmpleadosRelacionados(string CadenaEmpleadoId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            MovimientoAcceso MovimientoAccesoObjeto = new MovimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            ResultadoEntidadObjeto = MovimientoAccesoObjeto.SeleccionarMovimientoEmpleadosRelacionados(CadenaEmpleadoId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public ResultadoEntidad TransferirActivos(DataTable ActivosSeleccionados, Int16 EmpleadoOrigenID, Int16 EmpleadoDestinoID, Int16 UsuarioID, string SesionId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoTransferirObjetoEntidad = new ActivoEntidad();
            ActivoEntidad ActivoAsignarObjetoEntidad = new ActivoEntidad();
            TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoEntidadObjeto = new TemporalTransferenciaActivoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            //Primero se eliminan los registros de la tabla TemporalAsignacionActivo que sean de la SesionId
            Resultado = EliminarTemporalTransferenciaActivo(SesionId);

            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalTransferenciaActivo.TemporalTransferenciaActivoEliminadoCorrectamente)
            {
                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

                Conexion = new SqlConnection(CadenaConexion);
                Conexion.Open();

                Transaccion = Conexion.BeginTransaction();

                //Se barre el datatable con los activos seleccionados para transferir
                foreach (DataRow dtRegistro in ActivosSeleccionados.Rows)
                {
                    ActivoTransferirObjetoEntidad = new ActivoEntidad();
                    ActivoAsignarObjetoEntidad = new ActivoEntidad();

                    //Primero se desasigna el Activo del empleado origen

                    ActivoTransferirObjetoEntidad.ActivoId = int.Parse(dtRegistro["ActivoId"].ToString());
                    ActivoTransferirObjetoEntidad.EmpleadoId = EmpleadoOrigenID;
                    ActivoTransferirObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;

                    Resultado = ActualizarMovimientoTipoAsignacion(Conexion, Transaccion, ActivoTransferirObjetoEntidad);

                    if (Resultado.ErrorId == (int)ConstantePrograma.Movimiento.MovimientoAsignacionEditadoCorrectamente)
                    {
                        //Se valida que se haya editado solo un registro
                        if (Resultado.NuevoRegistroId == 1)
                        {
                            //Se inserta el TemporalTransferenciaActivo para la impresion
                            TemporalTransferenciaActivoEntidadObjeto.SesionId = SesionId;
                            TemporalTransferenciaActivoEntidadObjeto.ActivoId = int.Parse(dtRegistro["ActivoId"].ToString());
                            TemporalTransferenciaActivoEntidadObjeto.CondicionId = Int16.Parse(dtRegistro["CondicionId"].ToString());
                            TemporalTransferenciaActivoEntidadObjeto.UbicacionActivoId = Int16.Parse(dtRegistro["UbicacionActivoId"].ToString());

                            Resultado = InsertarTemporalTransferenciaActivo(Conexion, Transaccion, TemporalTransferenciaActivoEntidadObjeto);

                            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalTransferenciaActivo.TemporalTransferenciaActivoGuardadoCorrectamente)
                            {
                                //Si el Activo se desasigno correctamente y se inserto el temporal ahora se asigna al empleado destino
                                ActivoAsignarObjetoEntidad.ActivoId = int.Parse(dtRegistro["ActivoId"].ToString());
                                ActivoAsignarObjetoEntidad.EmpleadoId = EmpleadoDestinoID;
                                ActivoAsignarObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;
                                ActivoAsignarObjetoEntidad.CondicionId = Int16.Parse(dtRegistro["CondicionId"].ToString());
                                ActivoAsignarObjetoEntidad.UbicacionActivoId = Int16.Parse(dtRegistro["UbicacionActivoId"].ToString());
                                ActivoAsignarObjetoEntidad.UsuarioId = UsuarioID;

                                Resultado = InsertarMovimientoTipoAsignacion(Conexion, Transaccion, ActivoAsignarObjetoEntidad);

                                if (Resultado.ErrorId != (int)ConstantePrograma.Movimiento.MovimientoAsignacionGuardadoCorrectamente)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                            
                        }
                        else
                        {
                            Resultado.DescripcionError = "Ocurrio un error inesperado.";
                            break;
                        }

                    }
                    else
                    {
                        break;
                    }

                }

                if (Resultado.ErrorId == (int)ConstantePrograma.Movimiento.MovimientoAsignacionGuardadoCorrectamente)
                    Transaccion.Commit();
                else
                    Transaccion.Rollback();

                Conexion.Close();
            }
            else
            {
                Resultado.DescripcionError = "Ocurrio un error inesperado.";
            }

            return Resultado;
        }

        public ResultadoEntidad EliminarTemporalTransferenciaActivo(string SesionId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoEntidadObjeto = new TemporalTransferenciaActivoEntidad();
            TemporalTransferenciaActivoProceso TemporalTransferenciaActivoProcesoObjeto = new TemporalTransferenciaActivoProceso();

            TemporalTransferenciaActivoEntidadObjeto.SesionId = SesionId;

            Resultado = TemporalTransferenciaActivoProcesoObjeto.EliminarTemporalTransferenciaActivo(TemporalTransferenciaActivoEntidadObjeto);

            return Resultado;
        }

        public ResultadoEntidad InsertarTemporalTransferenciaActivo(SqlConnection Conexion, SqlTransaction Transaccion, TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoEntidadObjeto)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalTransferenciaActivoProceso TemporalTransferenciaActivoProcesoObjeto = new TemporalTransferenciaActivoProceso();

            Resultado = TemporalTransferenciaActivoProcesoObjeto.InsertarTemporalTransferenciaActivo(Conexion, Transaccion, TemporalTransferenciaActivoEntidadObjeto);

            return Resultado;
        }

        public ResultadoEntidad GuardarAsignacionGeneral(ActivoEntidad ActivoRecibidoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            AccesorioEntidad AccesorioObjetoEntidad = new AccesorioEntidad();
            AccesorioProceso AccesorioProcesoNegocio = new AccesorioProceso();

            //Validamos que ninguno de los activos del documento este asignado a un empleado
            ActivoObjetoEntidad.CompraId = ActivoRecibidoObjetoEntidad.CompraId;
            ActivoObjetoEntidad.TipoDeMovimiento = (int)ConstantePrograma.TipoMovimiento.Asignacion;

            Resultado = SeleccionarMovimientoPorDocumento(ActivoObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
            {
                //Ahora se valida que ninguno de los activos del documento este asignado a otro activo
                AccesorioObjetoEntidad.CompraId = ActivoRecibidoObjetoEntidad.CompraId;
                Resultado = AccesorioProcesoNegocio.SeleccionarAccesorioPorDocumento(AccesorioObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    //Si pasó la validación, ahora se insertan los movimientos de asignacion
                    Resultado = InsertarMovimientoAsignacionGeneral(ActivoRecibidoObjetoEntidad);
                }
                else
                {
                    Resultado.DescripcionError = TextoError.DocumentoProcesado;
                }
            }
            else
            {
                Resultado.DescripcionError = TextoError.DocumentoProcesado;
            }

            return Resultado;
        }

    }
}
