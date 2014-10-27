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
    public class AsignacionProceso : Base
    {
        //public ResultadoEntidad AsignarActivos(AsignacionEntidad AsignacionObjetoEntidad, Int16 EstatusId)
        //{
        //    string CadenaConexion = string.Empty;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    TemporalAsignacionDetalleProceso TemporalAsignacionDetalleProcesoNegocio = new TemporalAsignacionDetalleProceso();
        //    TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad = new TemporalAsignacionEntidad();
        //    ResultadoEntidad TemporalAsignacionResultado = new ResultadoEntidad();
        //    SqlTransaction Transaccion;
        //    SqlConnection Conexion;

        //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

        //    // Primero se obtienen los activos que fueron agregados a la asignacion temporal
        //    TemporalAsignacionObjetoEntidad.TemporalAsignacionId = AsignacionObjetoEntidad.TemporalAsignacionId;

        //    TemporalAsignacionResultado = TemporalAsignacionDetalleProcesoNegocio.SeleccionarTemporalAsignacionDetalle(TemporalAsignacionObjetoEntidad);

        //    if (TemporalAsignacionResultado.ResultadoDatos.Tables[0].Rows.Count > 0)
        //    {
        //        // Ahora se insertan las asignaciones
        //        Conexion = new SqlConnection(CadenaConexion);
        //        Conexion.Open();

        //        Transaccion = Conexion.BeginTransaction();

        //        Resultado = InsertarAsignacion(Conexion, Transaccion, AsignacionObjetoEntidad, TemporalAsignacionResultado.ResultadoDatos);

        //        // Si las asignaciones se insertaron correctamente, se cambian los estatus de los activos
        //        if (Resultado.ErrorId == (int)ConstantePrograma.Asignacion.AsignacionGuardadoCorrectamente)
        //        {
        //            //Se cambian los estatus, ubicaciones y condiciones de los activos
        //            Resultado = ActualizarActivoAsignar(Conexion, Transaccion, AsignacionObjetoEntidad, EstatusId);

        //            if (Resultado.ErrorId == (int)ConstantePrograma.Activo.ActivoAsignadoCorrectamente)
        //            {
        //                Transaccion.Commit();
        //                Resultado.ErrorId = (int)ConstantePrograma.Asignacion.AsignacionExitosa;
        //            }
        //            else
        //            {
        //                Transaccion.Rollback();
        //            }
        //        }
        //        else
        //        {
        //            Transaccion.Rollback();
        //        }

        //        Conexion.Close();
        //    }
        //    else
        //    {
        //        Resultado.ErrorId = (int)ConstantePrograma.Asignacion.AsignacionTemporalSinActivos;
        //        Resultado.DescripcionError = TextoError.AsignacionTemporalSinActivos;
        //    }

        //    return Resultado;
        //}

        //public ResultadoEntidad ActualizarActivoAsignar(SqlConnection Conexion, SqlTransaction Transaccion, AsignacionEntidad AsignacionObjetoEntidad, Int16 EstatusId)
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
        //    ActivoProceso ActivoProcesoNegocio = new ActivoProceso();

        //    ActivoEntidadObjeto.TemporalAsignacionId = AsignacionObjetoEntidad.TemporalAsignacionId;
        //    ActivoEntidadObjeto.EstatusId = EstatusId;

        //    Resultado = ActivoProcesoNegocio.ActualizarActivoAsignar(Conexion, Transaccion, ActivoEntidadObjeto);

        //    return Resultado;
        //}

        //public ResultadoEntidad InsertarAsignacion(SqlConnection Conexion, SqlTransaction Transaccion, AsignacionEntidad AsignacionObjetoEntidad, DataSet dsTemporalAsignacionDetalle)
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    AsignacionEntidad NuevaAsignacionObjetoEntidad = new AsignacionEntidad();

        //    // Se barre el dataset que contiene los activos agregados a la asignacion temporal
        //    foreach (DataRow dtRegistro in dsTemporalAsignacionDetalle.Tables[0].Rows)
        //    {
        //        // Antes de insertar cada asignacion, primero se valida nuevamente por seguridad que el activo siga sin ser asignado
        //        if (ValidarActivoAsignado(Conexion, Transaccion, Int16.Parse(dtRegistro["ActivoId"].ToString())) == false)
        //        {
        //            NuevaAsignacionObjetoEntidad.ActivoId = Int16.Parse(dtRegistro["ActivoId"].ToString());
        //            NuevaAsignacionObjetoEntidad.EmpleadoId = AsignacionObjetoEntidad.EmpleadoId;
        //            NuevaAsignacionObjetoEntidad.UsuarioIdInserto = AsignacionObjetoEntidad.UsuarioIdInserto;

        //            Resultado = GuardarAsignacion(Conexion, Transaccion, NuevaAsignacionObjetoEntidad);

        //            // Si se inserto la asignacion correctamente se continua con la siguiente hasta terminar, si hubo error se sale de la iteracion
        //            if (Resultado.ErrorId != (int)ConstantePrograma.Asignacion.AsignacionGuardadoCorrectamente)
        //            {
        //                Resultado.DescripcionError = TextoError.ErrorGenerico + ". " + Resultado.DescripcionError;
        //                break;
        //            }
        //        }
        //        else
        //        {
        //            Resultado.ErrorId = (int)ConstantePrograma.Asignacion.ActivoAsignadoBaja;
        //            Resultado.DescripcionError = TextoError.ActivoAsignadoBaja;
        //            break;
        //        }
        
        //    }

        //    return Resultado;
        //}

        //public bool ValidarActivoAsignado(SqlConnection Conexion, SqlTransaction Transaccion, Int16 ActivoId)
        //{
        //    bool ActivoAsignado = false;
        //    ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
        //    ActivoProceso ActivoProcesoNegocio = new ActivoProceso();
        //    ResultadoEntidad Resultado = new ResultadoEntidad();

        //    ActivoEntidadObjeto.ActivoId = ActivoId;

        //    Resultado = ActivoProcesoNegocio.SeleccionarActivoMismaConexion(Conexion, Transaccion, ActivoEntidadObjeto);

        //    if (Resultado.ErrorId == 0)
        //    {
        //        if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
        //        {
        //            if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString()) != (int)ConstantePrograma.EstatusActivos.SinAsignar)
        //            {
        //                ActivoAsignado = true;
        //            }
        //        }
        //        else
        //        {
        //            ActivoAsignado = true;
        //        }
        //    }
        //    else
        //    {
        //        ActivoAsignado = true;
        //    }

        //    return ActivoAsignado;
        //}

        //public ResultadoEntidad GuardarAsignacion(SqlConnection Conexion, SqlTransaction Transaccion, AsignacionEntidad AsignacionObjetoEntidad)
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    AsignacionAcceso AsignacionAccesoObjeto = new AsignacionAcceso();

        //    Resultado = AsignacionAccesoObjeto.InsertarAsignacion(Conexion, Transaccion, AsignacionObjetoEntidad);

        //    return Resultado;
        //}


        //public ResultadoEntidad SeleccionarAsignacion(AsignacionEntidad AsignacionObjetoEntidad)
        //{
        //    string CadenaConexion = string.Empty;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    AsignacionAcceso AsignacionAccesoObjeto = new AsignacionAcceso();

        //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

        //    Resultado = AsignacionAccesoObjeto.SeleccionarAsignacion(AsignacionObjetoEntidad, CadenaConexion);

        //    return Resultado;
        //}

        //public bool SeleccionarAsignacionEmpleadosRelacionados(string CadenaEmpleadoId)
        //{
        //    string CadenaConexion = string.Empty;
        //    ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
        //    AsignacionAcceso AsignacionAccesoObjeto = new AsignacionAcceso();

        //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

        //    ResultadoEntidadObjeto = AsignacionAccesoObjeto.SeleccionarAsignacionEmpleadosRelacionados(CadenaEmpleadoId, CadenaConexion);

        //    if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
        //        return false;
        //    else
        //        return true;
        //}

    }
}
