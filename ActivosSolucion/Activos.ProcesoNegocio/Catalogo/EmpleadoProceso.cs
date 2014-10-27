using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;

namespace Activos.ProcesoNegocio.Catalogo
{
    public class EmpleadoProceso : Base
    {

        public ResultadoEntidad GuardarEmpleado(EmpleadoEntidad EmpleadoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
            EmpleadoAcceso EmpleadoAccesoObjeto = new EmpleadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoValidacion = ValidarEmpleado(EmpleadoObjetoEntidad);

            if (ResultadoValidacion.ErrorId == 0)
            {
                if (EmpleadoObjetoEntidad.EmpleadoId == 0)
                {
                    Resultado = EmpleadoAccesoObjeto.InsertarEmpleado(EmpleadoObjetoEntidad, CadenaConexion);
                }
                else
                {
                    Resultado = EmpleadoAccesoObjeto.ActualizarEmpleado(EmpleadoObjetoEntidad, CadenaConexion);
                }
            }
            else
            {
                Resultado = ResultadoValidacion;
            }

            return Resultado;
        }

        public bool BuscarEmpleadoNumeroDuplicado(EmpleadoEntidad EmpleadoObjetoEntidad)
        {
            bool ExisteEmpleado = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad BuscarEmpleadoObjetoEntidad = new EmpleadoEntidad();

            BuscarEmpleadoObjetoEntidad.NumeroEmpleado = EmpleadoObjetoEntidad.NumeroEmpleado;

            Resultado = SeleccionarEmpleado(BuscarEmpleadoObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString()) != EmpleadoObjetoEntidad.EmpleadoId)
                    ExisteEmpleado = true;
                else
                    ExisteEmpleado = false;
            }
            return ExisteEmpleado;
        }

        public bool BuscarEmpleadoNombreDuplicado(EmpleadoEntidad EmpleadoObjetoEntidad)
        {
            bool ExisteEmpleado = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad BuscarEmpleadoObjetoEntidad = new EmpleadoEntidad();

            BuscarEmpleadoObjetoEntidad.BuscarNombre =  Comparar.EstandarizarCadena(EmpleadoObjetoEntidad.Nombre);
            BuscarEmpleadoObjetoEntidad.BuscarApellidoPaterno =  Comparar.EstandarizarCadena(EmpleadoObjetoEntidad.ApellidoPaterno);
            BuscarEmpleadoObjetoEntidad.BuscarApellidoMaterno =  Comparar.EstandarizarCadena(EmpleadoObjetoEntidad.ApellidoMaterno);

            Resultado = SeleccionarEmpleado(BuscarEmpleadoObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString()) != EmpleadoObjetoEntidad.EmpleadoId)
                    ExisteEmpleado = true;
                else
                    ExisteEmpleado = false;
            }
            return ExisteEmpleado;
        }

        protected ResultadoEntidad ValidarEmpleado(EmpleadoEntidad EmpleadoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();

            // Revisar si ya existe un empleado con ese número
            if (BuscarEmpleadoNumeroDuplicado(EmpleadoObjetoEntidad))
            {
                Resultado.ErrorId = (int)ConstantePrograma.Empleado.EmpleadoConNumeroDuplicado;
                Resultado.DescripcionError = TextoError.EmpleadoConNumeroDuplicado;
                return Resultado;
            }

            // Revisar si ya existe un empleado con ese nombre
            if (BuscarEmpleadoNombreDuplicado(EmpleadoObjetoEntidad))
            {
                Resultado.ErrorId = (int)ConstantePrograma.Empleado.EmpleadoConNombreDuplicado;
                Resultado.DescripcionError = TextoError.EmpleadoConNombreDuplicado;
                return Resultado;
            }

            return Resultado;
        }

        public ResultadoEntidad SeleccionarEmpleado(EmpleadoEntidad EmpleadoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoAcceso EmpleadoAccesoObjeto = new EmpleadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = EmpleadoAccesoObjeto.SeleccionarEmpleado(EmpleadoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public bool SeleccionarEmpleadoUsuariosRelacionados(string CadenaUsuarioId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            EmpleadoAcceso EmpleadoAccesoObjeto = new EmpleadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = EmpleadoAccesoObjeto.SeleccionarEmpleadoUsuariosRelacionados(CadenaUsuarioId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public bool SeleccionarEmpleadoEdificioRelacionados(string CadenaEdificioId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            EmpleadoAcceso EmpleadoAccesoObjeto = new EmpleadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = EmpleadoAccesoObjeto.SeleccionarEmpleadoEdificioRelacionados(CadenaEdificioId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public bool SeleccionarEmpleadoPuestosRelacionados(string CadenaPuestoId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            EmpleadoAcceso EmpleadoAccesoObjeto = new EmpleadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = EmpleadoAccesoObjeto.SeleccionarEmpleadoPuestosRelacionados(CadenaPuestoId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public bool SeleccionarEmpleadoDepartamentosRelacionados(string CadenaDepartamentoId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            EmpleadoAcceso EmpleadoAccesoObjeto = new EmpleadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = EmpleadoAccesoObjeto.SeleccionarEmpleadoDepartamentosRelacionados(CadenaDepartamentoId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        protected ResultadoEntidad EliminarEmpleado(string CadenaEmpleadoId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            EmpleadoAcceso EmpleadoAccesoObjeto = new EmpleadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = EmpleadoAccesoObjeto.EliminarEmpleado(CadenaEmpleadoId, CadenaConexion);

            return ResultadoEntidadObjeto;
        }

        //protected bool TieneAsignacionesRelacionados(string CadenaEmpleadoId)
        //{
        //    bool TieneRelaciones = false;
        //    AsignacionProceso AsignacionProcesoObjeto = new AsignacionProceso();

        //    TieneRelaciones = AsignacionProcesoObjeto.SeleccionarAsignacionEmpleadosRelacionados(CadenaEmpleadoId);

        //    return TieneRelaciones;
        //}

        protected bool TieneComprasRelacionados(string CadenaEmpleadoId)
        {
            bool TieneRelaciones = false;
            CompraProceso CompraProcesoObjeto = new CompraProceso();

            TieneRelaciones = CompraProcesoObjeto.SeleccionarCompraEmpleadosRelacionados(CadenaEmpleadoId);

            return TieneRelaciones;
        }

        //protected bool TieneLevantamientosRelacionados(string CadenaEmpleadoId)
        //{
        //    bool TieneRelaciones = false;
        //    LevantamientoActivoProceso LevantamientoActivoProcesoObjeto = new LevantamientoActivoProceso();

        //    TieneRelaciones = LevantamientoActivoProcesoObjeto.SeleccionarLevantamientoEmpleadosRelacionados(CadenaEmpleadoId);

        //    return TieneRelaciones;
        //}

        protected bool TieneMovimientosRelacionados(string CadenaEmpleadoId)
        {
            bool TieneRelaciones = false;
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();

            TieneRelaciones = MovimientoProcesoObjeto.SeleccionarMovimientoEmpleadosRelacionados(CadenaEmpleadoId);

            return TieneRelaciones;
        }

        protected bool TieneJefesRelacionados(string CadenaEmpleadoId)
        {
            bool TieneRelaciones = false;
            JefeProceso JefeProcesoObjeto = new JefeProceso();

            TieneRelaciones = JefeProcesoObjeto.SeleccionarJefeEmpleadosRelacionados(CadenaEmpleadoId);

            return TieneRelaciones;
        }

        protected bool TieneRelacionesElEmpleado(string CadenaEmpleadoId)
        {
            bool TieneRelacionesElEmpleado = false;

            //// Revisar relaciones con Asignacion
            //if (TieneAsignacionesRelacionados(CadenaEmpleadoId))
            //    return true;

            // Revisar relaciones con Compra
            if (TieneComprasRelacionados(CadenaEmpleadoId))
                return true;

            //// Revisar relaciones con Levantamiento
            //if (TieneLevantamientosRelacionados(CadenaEmpleadoId))
            //    return true;

            // Revisar relaciones con Movimiento
            if (TieneMovimientosRelacionados(CadenaEmpleadoId))
                return true;

            // Revisar relaciones con Jefe
            if (TieneJefesRelacionados(CadenaEmpleadoId))
                return true;

            return TieneRelacionesElEmpleado;
        }

        public ResultadoEntidad EliminarEmpleado(EmpleadoEntidad EmpleadoObjetoEntidad)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

            // Validar que los empleados no contengan información relacionada con otras tablas
            if (TieneRelacionesElEmpleado(EmpleadoObjetoEntidad.CadenaEmpleadoId))
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Empleado.EmpleadoTieneRegistrosRelacionados;
                ResultadoEntidadObjeto.DescripcionError = TextoError.EmpleadoTieneRegistrosRelacionados;
            }
            else
            {
                // Si se pasaron todas las validaciones, hay que borrar el o los edificios seleccionados
                ResultadoEntidadObjeto = EliminarEmpleado(EmpleadoObjetoEntidad.CadenaEmpleadoId);
            }

            return ResultadoEntidadObjeto;
        }

    }
}
