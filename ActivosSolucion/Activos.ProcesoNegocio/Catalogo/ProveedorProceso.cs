using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Activos.AccesoDatos.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;

namespace Activos.ProcesoNegocio.Catalogo
{
    public class ProveedorProceso : Base
    {
        public bool BuscarProveedorNombreDuplicado(ProveedorEntidad ProveedorObjetoEntidad)
        {
            bool ExisteProveedor = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ProveedorEntidad BuscarProveedorObjetoEntidad = new ProveedorEntidad();

            BuscarProveedorObjetoEntidad.BuscarNombre = Comparar.EstandarizarCadena(ProveedorObjetoEntidad.Nombre);

            Resultado = SeleccionarProveedor(BuscarProveedorObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ProveedorId"].ToString()) != ProveedorObjetoEntidad.ProveedorId)
                    ExisteProveedor = true;
                else
                    ExisteProveedor = false;
            }
            return ExisteProveedor;
        }

        protected ResultadoEntidad EliminarProveedor(string CadenaProveedorId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            ProveedorAcceso ProveedorAccesoObjeto = new ProveedorAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = ProveedorAccesoObjeto.EliminarProveedor(CadenaProveedorId, CadenaConexion);

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad EliminarProveedor(ProveedorEntidad ProveedorObjetoEntidad)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

            // Validar que los proveedores no contengan información relacionada con otras tablas
            if (TieneRelacionesElProveedor(ProveedorObjetoEntidad.CadenaProveedorId))
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Proveedor.ProveedorTieneRegistrosRelacionados;
                ResultadoEntidadObjeto.DescripcionError = TextoError.ProveedorTieneRegistrosRelacionados;
            }
            else
            {
                // Si se pasaron todas las validaciones, hay que borrar el o los proveedores seleccionados
                ResultadoEntidadObjeto = EliminarProveedor(ProveedorObjetoEntidad.CadenaProveedorId);
            }

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad SeleccionarProveedor(ProveedorEntidad ProveedorObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ProveedorAcceso ProveedorAccesoObjeto = new ProveedorAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = ProveedorAccesoObjeto.SeleccionarProveedor(ProveedorObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        protected bool TieneComprasRelacionados(string CadenaProveedorId)
        {
            bool TieneRelaciones = false;
            CompraProceso CompraProcesoObjeto = new CompraProceso();

            TieneRelaciones = CompraProcesoObjeto.SeleccionarCompraProveedoresRelacionados(CadenaProveedorId);

            return TieneRelaciones;
        }

        protected bool TieneRelacionesElProveedor(string CadenaProveedorId)
        {
            bool TieneRelacionesElProveedor = false;

            // Revisar relaciones con Compra
            if (TieneComprasRelacionados(CadenaProveedorId))
                return true;

            return TieneRelacionesElProveedor;
        }

        protected ResultadoEntidad ValidarProveedor(ProveedorEntidad ProveedorObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();

            // Revisar si ya existe un proveedor con ese nombre
            if (BuscarProveedorNombreDuplicado(ProveedorObjetoEntidad))
            {
                Resultado.ErrorId = (int)ConstantePrograma.Proveedor.ProveedorConNombreDuplicado;
                Resultado.DescripcionError = TextoError.ProveedorConNombreDuplicado;
                return Resultado;
            }

            return Resultado;
        }

        public ResultadoEntidad GuardarProveedor(ProveedorEntidad ProveedorObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
            ProveedorAcceso ProveedorAccesoObjeto = new ProveedorAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoValidacion = ValidarProveedor(ProveedorObjetoEntidad);

            if (ResultadoValidacion.ErrorId == 0)
            {
                if (ProveedorObjetoEntidad.ProveedorId == 0)
                {
                    Resultado = ProveedorAccesoObjeto.InsertarProveedor(ProveedorObjetoEntidad, CadenaConexion);
                }
                else
                {
                    Resultado = ProveedorAccesoObjeto.ActualizarProveedor(ProveedorObjetoEntidad, CadenaConexion);
                }
            }
            else
            {
                Resultado = ResultadoValidacion;
            }

            return Resultado;
        }

    }
}
