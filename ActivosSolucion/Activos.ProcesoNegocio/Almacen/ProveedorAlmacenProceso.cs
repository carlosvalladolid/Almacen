using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public class ProveedorAlmacenProceso : Base
    {
        public bool BuscarProveedorNombreDuplicado(ProveedorAlmacenEntidad ProveedorAlmacenObjetoEntidad)
        {
            bool ExisteProveedor = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ProveedorAlmacenEntidad BuscarProveedorObjetoEntidad = new ProveedorAlmacenEntidad();

            BuscarProveedorObjetoEntidad.BuscarNombre = Comparar.EstandarizarCadena(ProveedorAlmacenObjetoEntidad.Nombre);

            Resultado = SeleccionarProveedor(BuscarProveedorObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ProveedorId"].ToString()) != ProveedorAlmacenObjetoEntidad.ProveedorId)
                    ExisteProveedor = true;
                else
                    ExisteProveedor = false;
            }
            return ExisteProveedor;
        }




        //protected ResultadoEntidad EliminarProveedor(string CadenaProveedorId)
        //{
        //    string CadenaConexion = string.Empty;
        //    ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
        //    ProveedorAlmacenAcceso ProveedorAlmacenAccesoObjeto = new ProveedorAlmacenAcceso();

        //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

        //    ResultadoEntidadObjeto = ProveedorAlmacenAccesoObjeto.EliminarProveedor(CadenaProveedorId, CadenaConexion);

        //    return ResultadoEntidadObjeto;
        //}

        //public ResultadoEntidad EliminarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenObjetoEntidad)
        //{
        //    ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

        //    //// Validar que los proveedores no contengan información relacionada con otras tablas
        //    //if (ProveedorAlmacenObjetoEntidad.CadenaProveedorId == true)
        //    //{
        //    ////    ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Proveedor.ProveedorTieneRegistrosRelacionados;
        //    ////    ResultadoEntidadObjeto.DescripcionError = TextoError.ProveedorTieneRegistrosRelacionados;
        //    //}
        //    //else
        //    {
        //        // Si se pasaron todas las validaciones, hay que borrar el o los proveedores seleccionados
        //        ResultadoEntidadObjeto = EliminarProveedor(ProveedorAlmacenObjetoEntidad);
        //    }

        //    return ResultadoEntidadObjeto;
        //}

        //protected bool TieneRelacionesElProveedor(string CadenaProveedorId)
        //{
        //    bool TieneRelacionesElProveedor = false;

        //    // Revisar relaciones con Compra
        //    if (TieneComprasRelacionados(CadenaProveedorId))
        //        return true;

        //    return TieneRelacionesElProveedor;
        //}

        //protected bool TieneComprasRelacionados(string CadenaProveedorId)
        //{
        //    bool TieneRelaciones = false;
        //    CompraProceso CompraProcesoObjeto = new CompraProceso();

        //    TieneRelaciones = CompraProcesoObjeto.SeleccionarCompraProveedoresRelacionados(CadenaProveedorId);

        //    return TieneRelaciones;
        //}

        public ResultadoEntidad GuardarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
            ProveedorAlmacenAcceso ProveedorAlmacenAccesoObjeto = new ProveedorAlmacenAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            ResultadoValidacion = ValidarProveedor(ProveedorAlmacenObjetoEntidad);

            if (ResultadoValidacion.ErrorId == 0)
            {
                if (ProveedorAlmacenObjetoEntidad.ProveedorId == 0)
                {
                    Resultado = ProveedorAlmacenAccesoObjeto.InsertarProveedor(ProveedorAlmacenObjetoEntidad, CadenaConexion);
                }
                else
                {
                    Resultado = ProveedorAlmacenAccesoObjeto.ActualizarProveedor(ProveedorAlmacenObjetoEntidad, CadenaConexion);
                }
            }
            else
            {
                Resultado = ResultadoValidacion;
            }

            return Resultado;
        }

        public ResultadoEntidad SeleccionarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ProveedorAlmacenAcceso ProveedorAlmacenAccesoObjeto = new ProveedorAlmacenAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            Resultado = ProveedorAlmacenAccesoObjeto.SeleccionarProveedor(ProveedorAlmacenObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        protected ResultadoEntidad ValidarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();

            // Revisar si ya existe un proveedor con ese nombre
            if (BuscarProveedorNombreDuplicado(ProveedorAlmacenObjetoEntidad))
            {
                Resultado.ErrorId = (int)ConstantePrograma.Proveedor.ProveedorConNombreDuplicado;
                Resultado.DescripcionError = TextoError.ProveedorConNombreDuplicado;
                return Resultado;
            }

            return Resultado;
        }
    }

}