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
    public class CompraProceso : Base
    {

        public bool BuscarCompraPorFolio(CompraEntidad CompraObjetoEntidad)
        {
            bool ExisteDocumento = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            CompraEntidad BuscarCompraObjetoEntidad = new CompraEntidad();

            BuscarCompraObjetoEntidad.ProveedorId = CompraObjetoEntidad.ProveedorId;
            BuscarCompraObjetoEntidad.TipoDocumentoId = CompraObjetoEntidad.TipoDocumentoId;
            BuscarCompraObjetoEntidad.CompraFolio = CompraObjetoEntidad.CompraFolio;

            Resultado = SeleccionarCompra(BuscarCompraObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                ExisteDocumento = true;

            return ExisteDocumento;
        }

        public ResultadoEntidad GuardarCompra(SqlConnection Conexion, SqlTransaction Transaccion, CompraEntidad CompraObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            CompraAcceso CompraAccesoObjeto = new CompraAcceso();

            Resultado = CompraAccesoObjeto.InsertarCompra(Conexion, Transaccion, CompraObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad GuardarRecepcion(CompraEntidad CompraObjetoEntidad, Int16 TipoMovimientoId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoTemporalActivo = new ResultadoEntidad();
            TemporalActivoEntidad TemporalActivoObjetoEntidad = new TemporalActivoEntidad();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            TemporalActivoProceso TemporalActivoProcesoNegocio = new TemporalActivoProceso();
            ActivoProceso ActivoProcesoNegocio = new ActivoProceso();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            if (BuscarCompraPorFolio(CompraObjetoEntidad) == true)
            {   // Se busca si ya existe una compra de ese proveedor con ese mismo tipo de documento y ese mismo folio
                Resultado.ErrorId = (int)ConstantePrograma.Compra.DocumentoDuplicado;
                Resultado.DescripcionError = TextoError.RecepcionDocumentoDuplicado;
                return Resultado;
            }

            //Se seleccionan todos los activos temporales
            TemporalActivoObjetoEntidad.TemporalCompraId = CompraObjetoEntidad.TemporalCompraId;

            ResultadoTemporalActivo = TemporalActivoProcesoNegocio.SeleccionarTemporalActivo(TemporalActivoObjetoEntidad);

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            Resultado = GuardarCompra(Conexion, Transaccion, CompraObjetoEntidad);

            // Si la compra fue guardada correctamente, se obtiene su ID
            if (Resultado.ErrorId == (int)ConstantePrograma.Compra.CompraGuardadoCorrectamente)
            {
                CompraObjetoEntidad.CompraId = (Int16)Resultado.NuevoRegistroId;

                // Ahora se barren los activos temporales para insertarlos uno por uno
                Resultado = GuardarActivo(Conexion, Transaccion, CompraObjetoEntidad, ResultadoTemporalActivo.ResultadoDatos, TipoMovimientoId);

                if (Resultado.ErrorId == (int)ConstantePrograma.Activo.ActivoGuardadoCorrectamente
                    || Resultado.ErrorId == (int)ConstantePrograma.Accesorio.AccesorioGuardadoCorrectamente)
                {
                    Transaccion.Commit();
                    Resultado.ErrorId = (int)ConstantePrograma.Compra.RecepcionGuardadoCorrectamente;

                    //// Si se insertaron los activos y los accesorios exitosamente, se editan los estatus de los activos acesorios a Asignado
                    //ActivoObjetoEntidad.TemporalCompraId = CompraObjetoEntidad.TemporalCompraId;
                    //ActivoObjetoEntidad.EstatusId = (Int16)ConstantePrograma.EstatusActivos.Asignado;
                    
                    //Resultado = ActivoProcesoNegocio.ActualizarActivoEstatus(Conexion, Transaccion, ActivoObjetoEntidad);

                    //// Si se edito los activos exitosamente termina la transaccion
                    //if (Resultado.ErrorId == (int)ConstantePrograma.Activo.ActivoAsignadoCorrectamente)
                    //{
                    //    Transaccion.Commit();
                    //    Resultado.ErrorId = (int)ConstantePrograma.Compra.RecepcionGuardadoCorrectamente;
                    //}
                    //else
                    //{
                    //    Transaccion.Rollback();
                    //}
                }
                else
                {
                    Transaccion.Rollback();
                }
            }
            else
            {
                Transaccion.Rollback();
            }

            Conexion.Close();

            return Resultado;
        }

        public ResultadoEntidad GuardarActivo(SqlConnection Conexion, SqlTransaction Transaccion, CompraEntidad CompraObjetoEntidad, DataSet dsActivo, Int16 TipoMovimientoId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoMovimiento = new ResultadoEntidad();
            ActivoProceso ActivoProcesoNegocio = new ActivoProceso();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            AsignacionEntidad AsignacionObjetoEntidad = new AsignacionEntidad();
            AccesorioProceso AccesorioProcesoNegocio = new AccesorioProceso();
            AsignacionProceso AsignacionProcesoNegocio = new AsignacionProceso();
            AccesorioEntidad AccesorioObjetoEntidad = new AccesorioEntidad();
            Int16 UsuarioId = 0;
            Int16 ActivoId = 0;

            UsuarioId = CompraObjetoEntidad.UsuarioIdInserto;

            //Se barren los activos y se insertan
            foreach (DataRow dtRegistro in dsActivo.Tables[0].Rows)
            {
                ActivoObjetoEntidad.CompraId = CompraObjetoEntidad.CompraId;
                ActivoObjetoEntidad.TemporalActivoId = int.Parse(dtRegistro["TemporalActivoId"].ToString());
                //ActivoObjetoEntidad.EstatusId = EstatusId;

                Resultado = ActivoProcesoNegocio.GuardarActivo(Conexion, Transaccion, ActivoObjetoEntidad);

                //Si el activo se guardo correctamente se obtiene su ID, se inserta el movimiento de alta y se inserta los accesorios
                if (Resultado.ErrorId == (int)ConstantePrograma.Activo.ActivoGuardadoCorrectamente)
                {
                    ActivoId = (Int16)Resultado.NuevoRegistroId;

                    //Ahora se inserta el movimiento de alta
                    ResultadoMovimiento = GuardarMovimientoAlta(dtRegistro, ActivoId, UsuarioId, Conexion, Transaccion);

                    if (ResultadoMovimiento.ErrorId == (int)ConstantePrograma.Movimiento.MovimientoAltaGuardadoCorrectamente)
                    {
                        //Si el tipo de movimiento es Asignacion (pantalla Historial) se inserta el movimiento de Asignacion
                        if (TipoMovimientoId == (Int16)ConstantePrograma.TipoMovimiento.Asignacion)
                        {
                            //AsignacionObjetoEntidad.ActivoId = (Int16)Resultado.NuevoRegistroId;
                            //AsignacionObjetoEntidad.EmpleadoId = Int16.Parse(dtRegistro["EmpleadoId"].ToString());
                            //AsignacionObjetoEntidad.UsuarioIdInserto = UsuarioId;
                            //Resultado = AsignacionProcesoNegocio.GuardarAsignacion(Conexion, Transaccion, AsignacionObjetoEntidad);
                            ResultadoMovimiento = GuardarMovimientoAsignacion(dtRegistro, ActivoId, UsuarioId, Conexion, Transaccion);
                        }

                        if (ResultadoMovimiento.ErrorId == (int)ConstantePrograma.Movimiento.MovimientoAltaGuardadoCorrectamente
                        || ResultadoMovimiento.ErrorId == (int)ConstantePrograma.Movimiento.MovimientoAsignacionGuardadoCorrectamente)
                        {
                            // Si el tipo de activo es de Vehiculo, entones puede tener accesorios, y se insertan
                            if (Int16.Parse(dtRegistro["TipoActivoId"].ToString()) == (Int16)ConstantePrograma.TipoAtivoConAccesorio.TipoActivoVehículoId)
                            {
                                AccesorioObjetoEntidad.ActivoId = ActivoId;
                                AccesorioObjetoEntidad.TemporalActivoId = int.Parse(dtRegistro["TemporalActivoId"].ToString());
                                AccesorioObjetoEntidad.UsuarioIdInserto = UsuarioId;

                                Resultado = AccesorioProcesoNegocio.GuardarAccesorio(Conexion, Transaccion, AccesorioObjetoEntidad);

                                //Si el accesorio(s) no se guardo correctamente se sale
                                if (Resultado.ErrorId != (int)ConstantePrograma.Accesorio.AccesorioGuardadoCorrectamente)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Resultado.ErrorId = ResultadoMovimiento.ErrorId;
                            break;
                        }
                    }
                    else
                    {
                        Resultado.ErrorId = ResultadoMovimiento.ErrorId;
                        break;
                    }
                    
                }
                else
                {
                    break;
                }
            }

            return Resultado;
        }

        public ResultadoEntidad SeleccionarCompra(CompraEntidad CompraObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            CompraAcceso CompraAccesoObjeto = new CompraAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = CompraAccesoObjeto.SeleccionarCompra(CompraObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public bool SeleccionarCompraEmpleadosRelacionados(string CadenaEmpleadoId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            CompraAcceso CompraAccesoObjeto = new CompraAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            ResultadoEntidadObjeto = CompraAccesoObjeto.SeleccionarCompraEmpleadosRelacionados(CadenaEmpleadoId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public bool SeleccionarCompraProveedoresRelacionados(string CadenaProveedorId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            CompraAcceso CompraAccesoObjeto = new CompraAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            ResultadoEntidadObjeto = CompraAccesoObjeto.SeleccionarCompraProveedoresRelacionados(CadenaProveedorId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public ResultadoEntidad GuardarMovimientoAlta(DataRow dtRegistro, int ActivoID, Int16 UsuarioId, SqlConnection Conexion, SqlTransaction Transaccion)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();

            ActivoObjetoEntidad.ActivoId = ActivoID;
            ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Alta;
            ActivoObjetoEntidad.CondicionId = Int16.Parse(dtRegistro["CondicionId"].ToString());
            ActivoObjetoEntidad.UbicacionActivoId = Int16.Parse(dtRegistro["UbicacionActivoId"].ToString());
            ActivoObjetoEntidad.UsuarioId = UsuarioId;

            Resultado = MovimientoProcesoObjeto.InsertarMovimientoTipoAlta(Conexion, Transaccion, ActivoObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad GuardarMovimientoAsignacion(DataRow dtRegistro, int ActivoID, Int16 UsuarioId, SqlConnection Conexion, SqlTransaction Transaccion)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();

            ActivoObjetoEntidad.ActivoId = ActivoID;
            ActivoObjetoEntidad.EmpleadoId = Int16.Parse(dtRegistro["EmpleadoId"].ToString());
            ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;
            ActivoObjetoEntidad.CondicionId = Int16.Parse(dtRegistro["CondicionId"].ToString());
            ActivoObjetoEntidad.UbicacionActivoId = Int16.Parse(dtRegistro["UbicacionActivoId"].ToString());
            ActivoObjetoEntidad.UsuarioId = UsuarioId;

            Resultado = MovimientoProcesoObjeto.InsertarMovimientoTipoAsignacion(Conexion, Transaccion, ActivoObjetoEntidad);

            return Resultado;
        }

    }
}
