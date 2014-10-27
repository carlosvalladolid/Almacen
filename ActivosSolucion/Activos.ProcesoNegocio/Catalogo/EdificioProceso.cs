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
    public class EdificioProceso : Base
    {

        public ResultadoEntidad GuardarEdificio(EdificioEntidad EdificioObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EdificioAcceso EdificioAccesoObjeto = new EdificioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            if (BuscarEdificioDuplicado(EdificioObjetoEntidad) == false)
            {
                if (EdificioObjetoEntidad.EdificioId == 0)
                {
                    Resultado = EdificioAccesoObjeto.InsertarEdificio(EdificioObjetoEntidad, CadenaConexion);
                }
                else
                {
                    Resultado = EdificioAccesoObjeto.ActualizarEdificio(EdificioObjetoEntidad, CadenaConexion);
                }
            }
            else
            {
                Resultado.ErrorId = (int)ConstantePrograma.Edificio.EdificioConNombreDuplicado;
                Resultado.DescripcionError = TextoError.EdificioConNombreDuplicado;
            }
            

            return Resultado;
        }

        public bool BuscarEdificioDuplicado(EdificioEntidad EdificioObjetoEntidad)
        {
            bool ExisteEdificio = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EdificioEntidad BuscarEdificioObjetoEntidad = new EdificioEntidad();

            BuscarEdificioObjetoEntidad.BuscarNombre = Comparar.EstandarizarCadena(EdificioObjetoEntidad.Nombre);

            Resultado = SeleccionarEdificio(BuscarEdificioObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EdificioId"].ToString()) != EdificioObjetoEntidad.EdificioId)
                    ExisteEdificio = true;
                else
                    ExisteEdificio = false;

            }

            return ExisteEdificio;
        }

        public ResultadoEntidad SeleccionarEdificio(EdificioEntidad EdificioObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EdificioAcceso EdificioAccesoObjeto = new EdificioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = EdificioAccesoObjeto.SeleccionarEdificio(EdificioObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public bool SeleccionarEdificioUsuariosRelacionados(string CadenaUsuarioId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            EdificioAcceso EdificioAccesoObjeto = new EdificioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = EdificioAccesoObjeto.SeleccionarEdificioUsuariosRelacionados(CadenaUsuarioId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        protected bool TieneEmpleadosRelacionados(string CadenaEdificioId)
        {
            bool TieneRelaciones = false;
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

            TieneRelaciones = EmpleadoProcesoObjeto.SeleccionarEmpleadoEdificioRelacionados(CadenaEdificioId);

            return TieneRelaciones;
        }

        protected bool TieneRelacionesElEdificio(string CadenaEdificioId)
        {
            bool TieneRelacionesElUsuario = false;

            // Revisar relaciones con Empleado
            if (TieneEmpleadosRelacionados(CadenaEdificioId))
                return true;

            return TieneRelacionesElUsuario;
        }

        protected ResultadoEntidad EliminarEdificio(string CadenaEdificioId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            EdificioAcceso EdificioAccesoObjeto = new EdificioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = EdificioAccesoObjeto.EliminarEdificio(CadenaEdificioId, CadenaConexion);

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad EliminarEdificio(EdificioEntidad EdificioEntidadObjeto)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

            // Validar que los edificios no contengan información relacionada con otras tablas
            if (TieneRelacionesElEdificio(EdificioEntidadObjeto.CadenaEdificioId))
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Edificio.EdificioTieneRegistrosRelacionados;
                ResultadoEntidadObjeto.DescripcionError = TextoError.EdificioTieneRegistrosRelacionados;
            }
            else
            {
                // Si se pasaron todas las validaciones, hay que borrar el o los edificios seleccionados
                ResultadoEntidadObjeto = EliminarEdificio(EdificioEntidadObjeto.CadenaEdificioId);
            }

            return ResultadoEntidadObjeto;
        }

    }
}
