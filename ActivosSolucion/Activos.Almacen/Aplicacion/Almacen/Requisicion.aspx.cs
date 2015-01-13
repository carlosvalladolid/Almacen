using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Comun.Fecha;
using Activos.Entidad.Catalogo;
using Activos.Entidad.General;
using Activos.Entidad.Almacen;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Almacen;
using Activos.ProcesoNegocio.Catalogo;

namespace Almacen.Web.Aplicacion.Almacen
{
    public partial class Requisicion : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonAgregar_Click(object sender, ImageClickEventArgs e)
            {
                AgregarDetalleDocumento();
            }

            protected void BotonCancelar_Click(object sender, EventArgs e)
            {
                LimpiarNuevoRegistro();
                LimpiarRequisicion();
            }


            protected void BotonLimpiar_Click(object sender, EventArgs e)
            {
                LimpiarRequisicion();
            }
            protected void BotonCerrarProductoBusqueda_Click(object sender, ImageClickEventArgs e)
            {
                CargaPanelInVisibleProducto();
            }

            protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
            {
                if (Page.IsValid)
                {
                    GuardarRequisicion();
                }
            }

            protected void BotonProductoBusqueda_Click(object sender, ImageClickEventArgs e)
            {
                BuscarProducto();
            }

            protected void ImagenProductoBusqueda_Click(object sender, ImageClickEventArgs e)
            {

                CargaPanelVisibleProducto();

            }

            protected void NuevoRegistro_Click(Object sender, System.EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

            protected void TablaProducto_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaProductoRowCommand(e);
            }

            protected void TablaRequisicion_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaRequisicionEventoComando(e);
            }
        #endregion

        #region "Métodos"
            private void AgregarDetalleDocumento()
            {
                RequisicionEntidad RequisicionObjetoEntidad = new RequisicionEntidad();

                //***********************************************************************
                //RequisicionObjetoEntidad.RequisicionId = TemporalRequisicionIdHidden.Value;
                //RequisicionObjetoEntidad.TemporalRequisicionId = TemporalRequisicionIdHidden.Value;
                //***********************************************************************
                RequisicionObjetoEntidad.RequisicionId = TemporalRequisicionIdHidden.Value;
                RequisicionObjetoEntidad.EstatusId = (int)ConstantePrograma.EstatusRequisicion.Incompleta;
                RequisicionObjetoEntidad.TemporalRequisicionId = TemporalRequisicionIdHidden.Value;
                RequisicionObjetoEntidad.EmpleadoId = Int16.Parse(EmpleadoIdHidden.Value);
                RequisicionObjetoEntidad.JefeId = Int16.Parse(JefeIdHidden.Value);
                RequisicionObjetoEntidad.ProductoId = ProductoIdHidden.Value;
                RequisicionObjetoEntidad.Cantidad = Int16.Parse(CantidadNuevo.Text.Trim());

                AgregarRequisicion(RequisicionObjetoEntidad);
            }

            private void AgregarRequisicion(RequisicionEntidad RequisicionObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

                InsertarRequisicionEncabezadoTemp(RequisicionObjetoEntidad);

                Resultado = RequisicionProcesoNegocio.AgregarTemporalRequisicion(RequisicionObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Requisicion.RequisicionGuardadoCorrectamente)
                {
                    TemporalRequisicionIdHidden.Value = RequisicionObjetoEntidad.RequisicionId;
                    LimpiarRequisicion();
                    SeleccionarRequisicion();
                }
                else
                {
                    MostrarMensaje(RequisicionProcesoNegocio.DescripcionError, ConstantePrograma.TipoErrorAlerta);

                    EtiquetaMensaje.Text = Resultado.DescripcionError;
                }
            }        

            private void BuscarProducto()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenEntidad AlmacenObjetoEntidad = new AlmacenEntidad();
                AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

                AlmacenObjetoEntidad.Clave = ClaveProductoBusqueda.Text.Trim();
                AlmacenObjetoEntidad.Descripcion = NombreProductoBusqueda.Text.Trim();

                Resultado = AlmacenProcesoNegocio.SeleccionarProducto(AlmacenObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaProducto.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaProducto.CssClass = ConstantePrograma.ClaseTabla;

                    TablaProducto.DataSource = Resultado.ResultadoDatos;
                    TablaProducto.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    // MostrarMensaje(AlmacenProcesoNegocio.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                }
            }

            private void CambiarNuevoRegistro()
            {
                //PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistroSolicitante.Visible = !PanelNuevoRegistroSolicitante.Visible;
                LimpiarNuevoRegistro();
            }

            private void CargaPanelVisibleProducto()
            {
                PanelBusquedaProducto.Visible = !PanelBusquedaProducto.Visible;
                pnlFondoBuscarProducto.Visible = !pnlFondoBuscarProducto.Visible;
            }

            private void CargaPanelInVisibleProducto()
            {
                PanelBusquedaProducto.Visible = false;
                pnlFondoBuscarProducto.Visible = false;

            }

            private void CargarInformacionUsuario()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();
                RequisicionEntidad RequisicionObjetoEntidad = new RequisicionEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                RequisicionObjetoEntidad.EmpleadoId = UsuarioSessionEntidad.UsuarioId;

                Resultado = RequisicionProcesoNegocio.SeleccionarEmpleado(RequisicionObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    EmpleadoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                    SolicitanteNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
                    DependenciaNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Dependencia"].ToString();
                    DireccionNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Direccion"].ToString();
                    PuestoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Puesto"].ToString();
                    JefeInmediatoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoJefe"].ToString();
                    JefeIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoIdJefe"].ToString();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    // MostrarMensaje(RequisicionProcesoNegocio.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                }
            }

            private void EliminarProducto(string ProductoId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionEntidad RequisicionObjetoEntidad = new RequisicionEntidad();
                RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

                //if (ProductoIdHidden.Value == ProductoId.ToString())
                //{
                RequisicionObjetoEntidad.ProductoId = ProductoId;
                Resultado = RequisicionProcesoNegocio.CancelarNuevoRequisicion(RequisicionObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Requisicion.EliminadoExitosamente)
                {
                    EtiquetaMensaje.Text = "";
                    SeleccionarRequisicion();

                }
                else
                {
                    EtiquetaMensaje.Text = Resultado.DescripcionError;
                }
                //}

            }

            private void GuardarRequisicion()
            {
                RequisicionEntidad RequisicionObjetoEntidad = new RequisicionEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                if (TemporalRequisicionIdHidden.Value != "")
                {
                    if (TablaRequisicion.Rows.Count > 0)
                    {
                        RequisicionObjetoEntidad.RequisicionId = TemporalRequisicionIdHidden.Value;

                        GuardarRequisicion(RequisicionObjetoEntidad);
                    }
                }
                else
                    MostrarMensaje("Favor de agregar los Productos", ConstantePrograma.TipoErrorAlerta);
            }

            private void GuardarRequisicion(RequisicionEntidad RequisicionObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

                Resultado = RequisicionProcesoNegocio.GuardarRequisicion(RequisicionObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Requisicion.RequisicionGuardadoCorrectamente)
                {
                    MostrarMensaje(TextoInfo.MensajeGuardadoGenerico, ConstantePrograma.TipoMensajeAlerta);
                    LimpiarNuevoRegistro();
                    LimpiarRequisicion();
                }
                else
                {
                    MostrarMensaje(RequisicionProcesoNegocio.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    //   EtiquetaMensaje.Text = Resultado.DescripcionError;
                }
            }        

            private void Inicio()
            {
                if (Page.IsPostBack)
                    return;

                CargarInformacionUsuario();
                BuscarProducto();

                TablaRequisicion.DataSource = null;
                TablaRequisicion.DataBind();
            }

            private void InsertarRequisicionEncabezadoTemp(RequisicionEntidad RequisicionObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

                if (TemporalRequisicionIdHidden.Value == "")
                {
                    Resultado = RequisicionProcesoNegocio.InsertarTemporalRequisicionEncabezado(RequisicionObjetoEntidad);

                    if (Resultado.ErrorId == (int)ConstantePrograma.Requisicion.RequisicionGuardadoCorrectamente)
                    {
                       // LimpiarNuevoRegistro();
                    }
                    else
                    {
                      MostrarMensaje(RequisicionProcesoNegocio.DescripcionError, ConstantePrograma.TipoErrorAlerta); 
                        //EtiquetaMensaje.Text = Resultado.DescripcionError;
                    }
                }
            }

            private void LimpiarNuevoRegistro()
            {
                SolicitanteNuevo.Text = "";
                DependenciaNuevo.Text = "";
                DireccionNuevo.Text = "";
                PuestoNuevo.Text = "";
                JefeInmediatoNuevo.Text = "";
                TemporalRequisicionIdHidden.Value = "";
                EmpleadoIdHidden.Value = "";
                JefeIdHidden.Value = "";

                TablaRequisicion.DataSource = null;
                TablaRequisicion.DataBind();
                EtiquetaMensaje.Text = "";
            }

            private void LimpiarRequisicion()
            {
                ClaveNuevo.Text = "";
                FamiliaIdNuevo.Text = "";
                SubFamiliaIdNuevo.Text = "";
                MarcaIdNuevo.Text = "";
                DescripcionNuevo.Text = "";
                CantidadNuevo.Text = "";
                EtiquetaMensaje.Text = "";
                ProductoIdHidden.Value = "";
            }

            private void MostrarMensaje(string Mensaje, string TipoMensaje)
            {
                StringBuilder FormatoMensaje = new StringBuilder();

                FormatoMensaje.Append("MostrarMensaje(\"");
                FormatoMensaje.Append(Mensaje);
                FormatoMensaje.Append("\", \"");
                FormatoMensaje.Append(TipoMensaje);
                FormatoMensaje.Append("\");");

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Mensaje", Comparar.ReemplazarCadenaJavascript(FormatoMensaje.ToString()), true);
            }

            private void SeleccionarClave()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenEntidad AlmacenEntidadObjeto = new AlmacenEntidad();
                AlmacenProceso AlmacenProcesoObjeto = new AlmacenProceso();
                bool AsignacionPermitida = true;

                AlmacenEntidadObjeto.Clave = ClaveNuevo.Text.Trim();

                Resultado = AlmacenProcesoObjeto.SeleccionarProducto(AlmacenEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        if (AsignacionPermitida == true)
                        {
                            FamiliaIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString();
                            SubFamiliaIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["SubFamiliaId"].ToString();
                            MarcaIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["MarcaId"].ToString();
                            DescripcionNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreProducto"].ToString();
                            CantidadNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["MaximoPermitido"].ToString();
                            ProductoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ProductoId"].ToString();

                        }
                        else
                        {
                            ClaveNuevo.Focus();
                        }

                    }
                    else
                    {
                        ClaveNuevo.Focus();
                    }
                }
                else
                {
                    // LimpiarProducto();
                    //AgregarEtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

            }

            private void SeleccionarProductoMostrar(AlmacenEntidad AlmacenEntidadObjeto)
            {

                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

                Resultado = AlmacenProcesoNegocio.SeleccionarProductoparaEditar(AlmacenEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    ClaveNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Clave"].ToString();
                    FamiliaIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Familia"].ToString();
                    SubFamiliaIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["SubFamilia"].ToString();
                    MarcaIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Marca"].ToString();
                    DescripcionNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreProducto"].ToString();
                    CargaPanelInVisibleProducto();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

            }

            private void SeleccionarRequisicion()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionEntidad RequisicionObjetoEntidad = new RequisicionEntidad();
                RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

                RequisicionObjetoEntidad.RequisicionId = TemporalRequisicionIdHidden.Value;

                Resultado = RequisicionProcesoNegocio.SeleccionaRequisicion(RequisicionObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaRequisicion.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaRequisicion.CssClass = ConstantePrograma.ClaseTabla;



                    TablaRequisicion.DataSource = Resultado.ResultadoDatos;
                    TablaRequisicion.DataBind();

                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            private void TablaProductoRowCommand(GridViewCommandEventArgs e)
            {
                AlmacenEntidad AlmacenEntidadObjeto = new AlmacenEntidad();
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                string ProductoId = "";
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaProducto.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaProducto.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        ProductoId = string.Format(TablaProducto.DataKeys[intFila]["ProductoId"].ToString());
                        AlmacenEntidadObjeto.ProductoId = ProductoId;
                        ProductoIdHidden.Value = ProductoId.ToString();
                        SeleccionarProductoMostrar(AlmacenEntidadObjeto);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

            private void TablaRequisicionEventoComando(GridViewCommandEventArgs e)
            {
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                string ProductoId = string.Empty;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaRequisicion.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaRequisicion.PageIndex));

                switch (strCommand)
                {
                    case "EliminarRequisicion":
                        ProductoId = string.Format(TablaRequisicion.DataKeys[intFila]["ProductoId"].ToString());
                        EliminarProducto(ProductoId);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }
        #endregion
    }
}
