using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Catalogo;
using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;

namespace Activos.ProcesoNegocio.Catalogo
{
    public class PuestoProceso : Base
    {
        public bool BuscarPuestoDuplicado(PuestoEntidad PuestoObjetoEntidad)
        {
            bool ExistePuesto = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            PuestoEntidad BuscarPuestoObjetoEntidad = new PuestoEntidad();

            BuscarPuestoObjetoEntidad.BuscarNombre = Comparar.EstandarizarCadena(PuestoObjetoEntidad.Nombre);
            BuscarPuestoObjetoEntidad.DependenciaId = PuestoObjetoEntidad.DependenciaId;

            Resultado = SeleccionarPuesto(BuscarPuestoObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["PuestoId"].ToString()) != PuestoObjetoEntidad.PuestoId)
                    ExistePuesto = true;
                else
                    ExistePuesto = false;
            }

            return ExistePuesto;
        }

        protected ResultadoEntidad EliminarPuesto(string CadenaPuestoId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            PuestoAcceso PuestoAccesoObjeto = new PuestoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = PuestoAccesoObjeto.EliminarPuesto(CadenaPuestoId, CadenaConexion);

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad EliminarPuesto(PuestoEntidad PuestoObjetoEntidad)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

            // Validar que los puestos no contengan información relacionada con otras tablas
            if (TieneRelacionesElPuesto(PuestoObjetoEntidad.CadenaPuestoId))
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Puesto.PuestoTieneRegistrosRelacionados;
                ResultadoEntidadObjeto.DescripcionError = TextoError.PuestoTieneRegistrosRelacionados;
            }
            else
            {
                // Si se pasaron todas las validaciones, hay que borrar el o los edificios seleccionados
                ResultadoEntidadObjeto = EliminarPuesto(PuestoObjetoEntidad.CadenaPuestoId);
            }

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad SeleccionarPuesto(PuestoEntidad PuestoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            PuestoAcceso PuestoAccesoObjeto = new PuestoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = PuestoAccesoObjeto.SeleccionarPuesto(PuestoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        protected bool TieneEmpleadosRelacionados(string CadenaPuestoId)
        {
            bool TieneRelaciones = false;
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

            TieneRelaciones = EmpleadoProcesoObjeto.SeleccionarEmpleadoPuestosRelacionados(CadenaPuestoId);

            return TieneRelaciones;
        }

        protected bool TieneJefesRelacionados(string CadenaPuestoId)
        {
            bool TieneRelaciones = false;
            JefeProceso JefeProcesoObjeto = new JefeProceso();

            TieneRelaciones = JefeProcesoObjeto.SeleccionarJefePuestosRelacionados(CadenaPuestoId);

            return TieneRelaciones;
        }

        protected bool TieneRelacionesElPuesto(string CadenaPuestoId)
        {
            bool TieneRelacionesElPuesto = false;

            // Revisar relaciones con Empleado
            if (TieneEmpleadosRelacionados(CadenaPuestoId))
                return true;

            // Revisar relaciones con Jefe
            if (TieneJefesRelacionados(CadenaPuestoId))
                return true;

            return TieneRelacionesElPuesto;
        }

        public ResultadoEntidad GuardarPuesto(PuestoEntidad PuestoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            PuestoAcceso PuestoAccesoObjeto = new PuestoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            if (BuscarPuestoDuplicado(PuestoObjetoEntidad) == false)
            {
                if (PuestoObjetoEntidad.PuestoId == 0)
                {
                    Resultado = PuestoAccesoObjeto.InsertarPuesto(PuestoObjetoEntidad, CadenaConexion);
                }
                else
                {
                    Resultado = PuestoAccesoObjeto.ActualizarPuesto(PuestoObjetoEntidad, CadenaConexion);
                }
            }
            else
            {
                Resultado.ErrorId = (int)ConstantePrograma.Puesto.PuestoConNombreDuplicado;
                Resultado.DescripcionError = TextoError.PuestoConNombreDuplicado;
            }

            return Resultado;
        }

    }
}
