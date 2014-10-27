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
    public class MarcaProceso : Base
    {
        public bool BuscarMarcaDuplicada(MarcaEntidad MarcaObjetoEntidad)
        {
            bool ExisteMarca = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MarcaEntidad BuscarMarcaObjetoEntidad = new MarcaEntidad();

            BuscarMarcaObjetoEntidad.BuscarNombre = Comparar.EstandarizarCadena(MarcaObjetoEntidad.Nombre);

            Resultado = SeleccionarMarca(BuscarMarcaObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["MarcaId"].ToString()) != MarcaObjetoEntidad.MarcaId)
                    ExisteMarca = true;
                else
                    ExisteMarca = false;

            }

            return ExisteMarca;
        }

        protected ResultadoEntidad EliminarMarca(string CadenaMarcaId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            MarcaAcceso MarcaAccesoObjeto = new MarcaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            ResultadoEntidadObjeto = MarcaAccesoObjeto.EliminarMarca(CadenaMarcaId, CadenaConexion);

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad EliminarMarca(MarcaEntidad MarcaObjetoEntidad)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

            // Validar que las marcas no contengan información relacionada con otras tablas
            if (TieneRelacionesLaMarca(MarcaObjetoEntidad.CadenaMarcaId))
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Marca.MarcaTieneRegistrosRelacionados;
                ResultadoEntidadObjeto.DescripcionError = TextoError.MarcaTieneRegistrosRelacionados;
            }
            else
            {
                // Si se pasaron todas las validaciones, hay que borrar la o las marcas seleccionadas
                ResultadoEntidadObjeto = EliminarMarca(MarcaObjetoEntidad.CadenaMarcaId);
            }

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad GuardarMarca(MarcaEntidad MarcaObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MarcaAcceso MarcaAccesoObjeto = new MarcaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            if (BuscarMarcaDuplicada(MarcaObjetoEntidad) == false)
            {
                if (MarcaObjetoEntidad.MarcaId == 0)
                {
                    Resultado = MarcaAccesoObjeto.InsertarMarca(MarcaObjetoEntidad, CadenaConexion);
                }
                else
                {
                    Resultado = MarcaAccesoObjeto.ActualizarMarca(MarcaObjetoEntidad, CadenaConexion);
                }
            }
            else
            {
                Resultado.ErrorId = (int)ConstantePrograma.Marca.MarcaConNombreDuplicado;
                Resultado.DescripcionError = TextoError.MarcaConNombreDuplicado;
            }

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMarca(MarcaEntidad MarcaObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MarcaAcceso MarcaAccesoObjeto = new MarcaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MarcaAccesoObjeto.SeleccionarMarca(MarcaObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        protected bool TieneActivosRelacionados(string CadenaMarcaId)
        {
            bool TieneRelaciones = false;
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();

            TieneRelaciones = ActivoProcesoObjeto.SeleccionarActivoMarcasRelacionados(CadenaMarcaId);

            return TieneRelaciones;
        }

        protected bool TieneRelacionesLaMarca(string CadenaMarcaId)
        {
            bool TieneRelacionesLaMarca = false;

            // Revisar relaciones con Activo
            if (TieneActivosRelacionados(CadenaMarcaId))
                return true;

            return TieneRelacionesLaMarca;
        }

    }
}