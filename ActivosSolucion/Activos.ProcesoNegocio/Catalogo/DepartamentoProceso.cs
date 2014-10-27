using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Activos.AccesoDatos.Catalogo;
using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;

namespace Activos.ProcesoNegocio.Catalogo
{
    public class DepartamentoProceso : Base
    {
        public ResultadoEntidad GuardarDepartamento(DepartamentoEntidad DepartamentoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
            DepartamentoAcceso DepartamentoAccesoObjeto = new DepartamentoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoValidacion = ValidarDepartamento(DepartamentoObjetoEntidad);

            if (ResultadoValidacion.ErrorId == 0)
            {
                if (DepartamentoObjetoEntidad.DepartamentoId == 0)
                {
                    Resultado = DepartamentoAccesoObjeto.InsertarDepartamento(DepartamentoObjetoEntidad, CadenaConexion);
                }
                else
                {
                    Resultado = DepartamentoAccesoObjeto.ActualizarDepartamento(DepartamentoObjetoEntidad, CadenaConexion);
                }
            }
            else
            {
                Resultado = ResultadoValidacion;
            }

            return Resultado;
        }

        public ResultadoEntidad SeleccionarDepartamento(DepartamentoEntidad DepartamentoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            DepartamentoAcceso DepartamentoAccesoObjeto = new DepartamentoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = DepartamentoAccesoObjeto.SeleccionarDepartamento(DepartamentoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public bool BuscarDepartamentoNombreDuplicado(DepartamentoEntidad DepartamentoObjetoEntidad)
        {
            bool ExisteDepartamento = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            DepartamentoEntidad BuscarDepartamentoObjetoEntidad = new DepartamentoEntidad();

            BuscarDepartamentoObjetoEntidad.BuscarNombre = DepartamentoObjetoEntidad.Nombre;

            Resultado = SeleccionarDepartamento(BuscarDepartamentoObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["DepartamentoId"].ToString()) != DepartamentoObjetoEntidad.DepartamentoId)
                    ExisteDepartamento = true;
                else
                    ExisteDepartamento = false;
            }
            return ExisteDepartamento;
        }

        protected bool TieneJefesRelacionados(string CadenaDepartamentoId)
        {
            bool TieneRelaciones = false;
            JefeProceso JefeProcesoObjeto = new JefeProceso();

            TieneRelaciones = JefeProcesoObjeto.SeleccionarJefeDepartamentosRelacionados(CadenaDepartamentoId);

            return TieneRelaciones;
        }

        protected bool TieneEmpleadosRelacionados(string CadenaDepartamentoId)
        {
            bool TieneRelaciones = false;
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

            TieneRelaciones = EmpleadoProcesoObjeto.SeleccionarEmpleadoDepartamentosRelacionados(CadenaDepartamentoId);

            return TieneRelaciones;
        }

        protected bool TieneRelacionesElDepartamento(string CadenaDepartamentoId)
        {
            bool TieneRelacionesElEmpleado = false;

            // Revisar relaciones con Jefes
            if (TieneJefesRelacionados(CadenaDepartamentoId))
                return true;

            // Revisar relaciones con Empleados
            if (TieneEmpleadosRelacionados(CadenaDepartamentoId))
                return true;


            return TieneRelacionesElEmpleado;
        }

        protected ResultadoEntidad EliminarDepartamento(string CadenaDepartamentoId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            DepartamentoAcceso DepartamentoAccesoObjeto = new DepartamentoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = DepartamentoAccesoObjeto.EliminarDepartamento(CadenaDepartamentoId, CadenaConexion);

            return ResultadoEntidadObjeto;
        }

        protected ResultadoEntidad ValidarDepartamento(DepartamentoEntidad DepartamentoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();

            
            // Revisar si ya existe un Departamento con ese nombre
            if (BuscarDepartamentoNombreDuplicado(DepartamentoObjetoEntidad))
            {
                Resultado.ErrorId = (int)ConstantePrograma.Departamento.DepartamentoConNombreDuplicado;
                Resultado.DescripcionError = TextoError.DepartamentoConNombreDuplicado;
                return Resultado;
            }

            return Resultado;
        }

        public bool SeleccionarDepartamentoUsuariosRelacionados(string CadenaUsuarioId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            DepartamentoAcceso DepartamentoAccesoObjeto = new DepartamentoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = DepartamentoAccesoObjeto.SeleccionarDepartamentoUsuariosRelacionados(CadenaUsuarioId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public bool SeleccionarDepartamentoDireccionesRelacionados(string CadenaDireccionId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            DepartamentoAcceso DepartamentoAccesoObjeto = new DepartamentoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = DepartamentoAccesoObjeto.SeleccionarDepartamentoDireccionesRelacionados(CadenaDireccionId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public ResultadoEntidad EliminarDepartamento(DepartamentoEntidad DepartamentoObjetoEntidad)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

            // Validar que los Departamento no contengan información relacionada con otras tablas
            if (TieneRelacionesElDepartamento(DepartamentoObjetoEntidad.CadenaDepartamentoId))
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Departamento.DepartamentoTieneRegistrosRelacionados;
                ResultadoEntidadObjeto.DescripcionError = TextoError.DepartamentoTieneRegistrosRelacionados;
            }
            else
            {
                // Si se pasaron todas las validaciones, hay que borrar el o los departamentos seleccionados
                ResultadoEntidadObjeto = EliminarDepartamento(DepartamentoObjetoEntidad.CadenaDepartamentoId);
            }

            return ResultadoEntidadObjeto;
        }
    }
}
