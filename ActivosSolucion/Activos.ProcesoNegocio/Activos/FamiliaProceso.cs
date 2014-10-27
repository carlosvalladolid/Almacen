using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Catalogo;
using Activos.AccesoDatos.Activos;
using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Activos;


namespace Activos.ProcesoNegocio.Activos
{
    public class FamiliaProceso : Base
    {
        public bool BuscarFamiliaDuplicada(FamiliaEntidad FamiliaObjetoEntidad)
        {
            bool ExisteFamilia = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            FamiliaEntidad BuscarFamiliaObjetoEntidad = new FamiliaEntidad();

            BuscarFamiliaObjetoEntidad.BuscarNombre = Comparar.EstandarizarCadena(FamiliaObjetoEntidad.Nombre);

            Resultado = SeleccionarFamilia(BuscarFamiliaObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString()) != FamiliaObjetoEntidad.FamiliaId)
                    ExisteFamilia = true;
                else
                    ExisteFamilia = false;

            }

            return ExisteFamilia;
        }

        protected ResultadoEntidad EliminarFamilia(string CadenaFamiliaId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            FamiliaAcceso FamiliaAccesoObjeto = new FamiliaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            ResultadoEntidadObjeto = FamiliaAccesoObjeto.EliminarFamilia(CadenaFamiliaId, CadenaConexion);

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad EliminarFamilia(FamiliaEntidad FamiliaObjetoEntidad)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

            // Validar que las familias no contengan información relacionada con otras tablas
            if (TieneRelacionesLaFamilia(FamiliaObjetoEntidad.CadenaFamiliaId))
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Familia.FamiliaTieneRegistrosRelacionados;
                ResultadoEntidadObjeto.DescripcionError = TextoError.FamiliaTieneRegistrosRelacionados;
            }
            else
            {
                // Si se pasaron todas las validaciones, hay que borrar la o las familias seleccionadas
                ResultadoEntidadObjeto = EliminarFamilia(FamiliaObjetoEntidad.CadenaFamiliaId);
            }

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad GuardarFamilia(FamiliaEntidad FamiliaObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            FamiliaAcceso FamiliaAccesoObjeto = new FamiliaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            if (BuscarFamiliaDuplicada(FamiliaObjetoEntidad) == false)
            {
                if (FamiliaObjetoEntidad.FamiliaId == 0)
                {
                    Resultado = FamiliaAccesoObjeto.InsertarFamilia(FamiliaObjetoEntidad, CadenaConexion);
                }
                else
                {
                    Resultado = FamiliaAccesoObjeto.ActualizarFamilia(FamiliaObjetoEntidad, CadenaConexion);
                }
            }
            else
            {
                Resultado.ErrorId = (int)ConstantePrograma.Familia.FamiliaConNombreDuplicado;
                Resultado.DescripcionError = TextoError.FamiliaConNombreDuplicado;
            }

            return Resultado;
        }

        public ResultadoEntidad SeleccionarFamilia(FamiliaEntidad FamiliaObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            FamiliaAcceso FamiliaAccesoObjeto = new FamiliaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = FamiliaAccesoObjeto.SeleccionarFamilia(FamiliaObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public bool SeleccionarFamiliaUsuariosRelacionados(string CadenaUsuarioId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            FamiliaAcceso FamiliaAccesoObjeto = new FamiliaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            ResultadoEntidadObjeto = FamiliaAccesoObjeto.SeleccionarFamiliaUsuariosRelacionados(CadenaUsuarioId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        protected bool TieneSubfamiliasRelacionadas(string CadenaFamiliaId)
        {
            bool TieneRelaciones = false;
            SubFamiliaProceso SubFamiliaProcesoObjeto = new SubFamiliaProceso();

            TieneRelaciones = SubFamiliaProcesoObjeto.SeleccionarSubFamiliaFamiliaRelacionadas(CadenaFamiliaId);

            return TieneRelaciones;
        }

        protected bool TieneRelacionesLaFamilia(string CadenaFamiliaId)
        {
            bool TieneRelacionesLaFamilia = false;

            // Revisar relaciones con SubFamilia
            if (TieneSubfamiliasRelacionadas(CadenaFamiliaId))
                return true;

            return TieneRelacionesLaFamilia;
        }

    }
}
