using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Activos.AccesoDatos.Catalogo;
using Activos.AccesoDatos.Almacen;
using Activos.ProcesoNegocio.Almacen;
using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
//using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;


namespace Activos.ProcesoNegocio.Almacen
{
   public class FamiliaProceso:Base
   {
        protected ResultadoEntidad EliminarFamilia(string CadenaFamiliaId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            FamiliaAcceso FamiliaAccesoObjeto = new FamiliaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

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

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            if (FamiliaObjetoEntidad.FamiliaId == 0)
            {
                Resultado = FamiliaAccesoObjeto.InsertarFamilia(FamiliaObjetoEntidad, CadenaConexion);
            }
            else
            {
                Resultado = FamiliaAccesoObjeto.ActualizarFamilia(FamiliaObjetoEntidad, CadenaConexion);
            }

            return Resultado;
        }

        public ResultadoEntidad SeleccionarFamilia(FamiliaEntidad FamiliaObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            FamiliaAcceso FamiliaAccesoObjeto = new FamiliaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            Resultado = FamiliaAccesoObjeto.SeleccionarFamilia(FamiliaObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        protected bool TieneRelacionesLaFamilia(string CadenaFamiliaId)
        {
            bool TieneRelacionesLaFamilia = false;

            // Revisar relaciones con SubFamilia
            if (TieneSubfamiliasRelacionadas(CadenaFamiliaId))
                return true;

            return TieneRelacionesLaFamilia;
        }

        protected bool TieneSubfamiliasRelacionadas(string CadenaFamiliaId)
        {
            bool TieneRelaciones = false;
            SubFamiliaProceso SubFamiliaProcesoObjeto = new SubFamiliaProceso();

            TieneRelaciones = SubFamiliaProcesoObjeto.SeleccionarSubFamiliaFamiliaRelacionadas(CadenaFamiliaId);

            return TieneRelaciones;
        }
    }
}
