using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Comun.Fecha;
using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Aplicacion.Reportes
{
    public partial class ReporteRastreoAccesorio : System.Web.UI.Page
    {
        #region "Eventos"

            protected void BotonBuscarActivo_Click(object sender, EventArgs e)
            {
                BuscarActivo();
            }

            protected void BotonImprimir_Click(object sender, EventArgs e)
            {
                ImprimirReporte();
            }

            protected void BotonLimpiar_Click(object sender, EventArgs e)
            {
                LimpiarPantalla();
                CodigoBarrasBusqueda.Text = "";
                NumeroSerieBusqueda.Text = "";
            }

            protected void TablaMovimientos_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaMovimientos.PageIndex = e.NewPageIndex;
                SeleccionarMovimientos();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

        #endregion

        #region "Métodos"

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Se validan los permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.ReporteRastreoAccesorios);

                    TablaMovimientos.DataSource = null;
                    TablaMovimientos.DataBind();
                }
            }

            protected void ImprimirReporte()
            {
                //Se pasan los valores a los campos ocultos
                CodigoBarrasHidden.Value = CodigoBarras.Text;
                DescripcionHidden.Value = Descripcion.Text;
                NumeroSerieHidden.Value = NumeroSerie.Text;
                ModeloHidden.Value = Modelo.Text;
                MarcaHidden.Value = Marca.Text;
                DireccionHidden.Value = Direccion.Text;
                DepartamentoHidden.Value = Departamento.Text;
                EdificioHidden.Value = Edificio.Text;
                FolioDocumentoHidden.Value = FolioDocumento.Text;
                ProveedorHidden.Value = Proveedor.Text;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "Imprimir()", true);
            }

            protected void BuscarActivo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();

                if (CodigoBarrasBusqueda.Text.Trim() == "" & NumeroSerieBusqueda.Text.Trim() == "")
                {
                    LimpiarPantalla();
                    EtiquetaMensaje.Text = "Favor de ingresar el código de barras y/o el número de serie.";
                }
                else
                {
                    ActivoEntidadObjeto.CodigoBarrasParticular = CodigoBarrasBusqueda.Text.Trim();
                    ActivoEntidadObjeto.NumeroSerie = NumeroSerieBusqueda.Text.Trim();
                    ActivoEntidadObjeto.TipoActivoId = ObtenerTipoActivoId();

                    Resultado = ActivoProcesoObjeto.SeleccionarActivo(ActivoEntidadObjeto);

                    if (Resultado.ErrorId == 0)
                    {
                        if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                        {
                            CodigoBarras.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoBarrasParticular"].ToString();
                            Descripcion.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                            NumeroSerie.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroSerie"].ToString();
                            Modelo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Modelo"].ToString();
                            Marca.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreMarca"].ToString();
                            FolioDocumento.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CompraFolio"].ToString();
                            Proveedor.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["ProveedorNombre"].ToString();
                            ActivoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();
                            ValidarActivoAccesorio();
                        }
                        else
                        {
                            LimpiarPantalla();
                            EtiquetaMensaje.Text = "Activo no encontrado.";
                        }
                    }
                    else
                    {
                        LimpiarPantalla();
                        EtiquetaMensaje.Text = Resultado.DescripcionError;
                    }
                }
            }

            protected void ValidarActivoAccesorio()
            {
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
                AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
                ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

                if (AccesorioProcesoObjeto.ValidarActivoAccesorio(int.Parse(ActivoIdHidden.Value)) == false)
                {
                    LimpiarPantalla();
                    EtiquetaMensaje.Text = "El activo no es un accesorio, favor de utilizar el reporte de Reastreo de activos.";
                }
                else
                {
                    //Si el activo es un accesorio se busca su Direccion, Departamento y Edificio
                    //dependiendo si esta dado de baja, si su activo padre esta asignado y de la ubicacion de piso o bodega de su activo padre
                    EtiquetaMensaje.Text = "";

                    ActivoEntidadObjeto = ActivoProcesoObjeto.ObtenerUbicacionAccesorio(int.Parse(ActivoIdHidden.Value), Int16.Parse(ConfigurationManager.AppSettings["Activos.Web.AlmacenistaEmpleadoId"].ToString()));

                    Direccion.Text = ActivoEntidadObjeto.DireccionNombre;
                    Departamento.Text = ActivoEntidadObjeto.DepartamentoNombre;
                    Edificio.Text = ActivoEntidadObjeto.EdificioNombre;

                    //Ahora se muestran los movimientos del activo
                    SeleccionarMovimientos();
                }


            }

            protected void SeleccionarMovimientos()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();

                ActivoObjetoEntidad.ActivoId = int.Parse(ActivoIdHidden.Value);

                Resultado = MovimientoProcesoObjeto.SeleccionarMovimientoPorAccesorio(ActivoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaMovimientos.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaMovimientos.CssClass = ConstantePrograma.ClaseTabla;

                    TablaMovimientos.DataSource = Resultado.ResultadoDatos;
                    TablaMovimientos.DataBind();

                    BotonImprimir.Enabled = true;
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

            }

            protected void LimpiarPantalla()
            {
                CodigoBarras.Text = "";
                Descripcion.Text = "";
                NumeroSerie.Text = "";
                Modelo.Text = "";
                Marca.Text = "";
                Direccion.Text = "";
                Departamento.Text = "";
                Edificio.Text = "";
                FolioDocumento.Text = "";
                Proveedor.Text = "";

                BotonImprimir.Enabled = false;
                ActivoIdHidden.Value = "0";

                TablaMovimientos.DataSource = null;
                TablaMovimientos.DataBind();

                EtiquetaMensaje.Text = "";
            }

            protected Int16 ObtenerTipoActivoId()
            {
                Int16 TipoActivoId = 0;
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                switch (UsuarioSessionEntidad.RolId)
                {
                    case (Int16)ConstantePrograma.RolUsuario.Administrador:
                        TipoActivoId = 0;
                        break;

                    case (Int16)ConstantePrograma.RolUsuario.Almacenista:
                        TipoActivoId = 0;
                        break;

                    case (Int16)ConstantePrograma.RolUsuario.Mantenimientos:
                        TipoActivoId = 0;
                        break;

                    case (Int16)ConstantePrograma.RolUsuario.ActivosMobiliario:
                        TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.Mobiliario;
                        break;

                    case (Int16)ConstantePrograma.RolUsuario.ActivosEquipoComputo:
                        TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.EquipoComputo;
                        break;

                    case (Int16)ConstantePrograma.RolUsuario.ActivosVehiculo:
                        TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.Vehiculo;
                        break;

                    case (Int16)ConstantePrograma.RolUsuario.ActivosOperacionYMantenimiento:
                        TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.OperaciónYMantenimiento;
                        break;
                }

                return TipoActivoId;
            }

        #endregion
    }
}
