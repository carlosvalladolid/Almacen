using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Activos.AccesoDatos.Catalogo;
using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;

namespace Activos.ProcesoNegocio.Catalogo
{
    public class DireccionProceso : Base
    {
        public bool BuscarDireccionDuplicada(DireccionEntidad DireccionObjetoEntidad)
        {
            bool ExisteDireccion = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            DireccionEntidad BuscarDireccionObjetoEntidad = new DireccionEntidad();

            BuscarDireccionObjetoEntidad.BuscarNombre = Comparar.EstandarizarCadena(DireccionObjetoEntidad.Nombre);

            Resultado = SeleccionarDireccion(BuscarDireccionObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["DireccionId"].ToString()) != DireccionObjetoEntidad.DireccionId)
                    ExisteDireccion = true;
                else
                    ExisteDireccion = false;
            }

            return ExisteDireccion;
        }

        protected ResultadoEntidad EliminarDireccion(string CadenaDireccionId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            DireccionAcceso DireccionAccesoObjeto = new DireccionAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = DireccionAccesoObjeto.EliminarDireccion(CadenaDireccionId, CadenaConexion);

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad EliminarDireccion(DireccionEntidad DireccionObjetoEntidad)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

            // Validar que las direcciones no contengan información relacionada con otras tablas
            if (TieneRelacionesLaDireccion(DireccionObjetoEntidad.CadenaDireccionId))
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Direccion.DireccionTieneRegistrosRelacionados;
                ResultadoEntidadObjeto.DescripcionError = TextoError.DireccionTieneRegistrosRelacionados;
            }
            else
            {
                // Si se pasaron todas las validaciones, hay que borrar la o las direcciones seleccionadas
                ResultadoEntidadObjeto = EliminarDireccion(DireccionObjetoEntidad.CadenaDireccionId);
            }

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad GuardarDireccion(DireccionEntidad DireccionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            DireccionAcceso DireccionAccesoObjeto = new DireccionAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            if (BuscarDireccionDuplicada(DireccionObjetoEntidad) == false)
            {
                if (DireccionObjetoEntidad.DireccionId == 0)
                {
                    Resultado = DireccionAccesoObjeto.InsertarDireccion(DireccionObjetoEntidad, CadenaConexion);
                }
                else
                {
                    Resultado = DireccionAccesoObjeto.ActualizarDireccion(DireccionObjetoEntidad, CadenaConexion);
                }
            }
            else
            {
                Resultado.ErrorId = (int)ConstantePrograma.Direccion.DireccionConNombreDuplicado;
                Resultado.DescripcionError = TextoError.DireccionConNombreDuplicado;
            }

            return Resultado;
        }

        public ResultadoEntidad SeleccionarDireccion(DireccionEntidad DireccionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            DireccionAcceso DireccionAccesoObjeto = new DireccionAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = DireccionAccesoObjeto.SeleccionarDireccion(DireccionObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public bool SeleccionarDireccionUsuariosRelacionados(string CadenaUsuarioId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            DireccionAcceso DireccionAccesoObjeto = new DireccionAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = DireccionAccesoObjeto.SeleccionarDireccionUsuariosRelacionados(CadenaUsuarioId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        protected bool TieneDepartamentosRelacionados(string CadenaDireccionId)
        {
            bool TieneRelaciones = false;
            DepartamentoProceso DepartamentoProcesoObjeto = new DepartamentoProceso();

            TieneRelaciones = DepartamentoProcesoObjeto.SeleccionarDepartamentoDireccionesRelacionados(CadenaDireccionId);

            return TieneRelaciones;
        }

        protected bool TieneJefesRelacionados(string CadenaDireccionId)
        {
            bool TieneRelaciones = false;
            JefeProceso JefeProcesoObjeto = new JefeProceso();

            TieneRelaciones = JefeProcesoObjeto.SeleccionarJefeDireccionesRelacionados(CadenaDireccionId);

            return TieneRelaciones;
        }

        protected bool TieneRelacionesLaDireccion(string CadenaDireccionId)
        {
            bool TieneRelacionesLaDireccion = false;

            // Revisar relaciones con Departamento
            if (TieneDepartamentosRelacionados(CadenaDireccionId))
                return true;

            // Revisar relaciones con Jefe
            if (TieneJefesRelacionados(CadenaDireccionId))
                return true;

            return TieneRelacionesLaDireccion;
        }

    }
}


