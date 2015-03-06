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
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Almacen;

namespace Almacen.Web.Aplicacion.Catalogo
{
    public partial class Producto : System.Web.UI.Page
    {
        #region "Eventos"

            protected void BotonBusqueda_Click(object sender, EventArgs e)
            {
                BusquedaAvanzada();
            }         

            protected void BotonBusquedaRapida_Click(object sender, ImageClickEventArgs e)
            {
                DescripcionBusqueda.Text = SearchText.Text.ToString();
                BusquedaAvanzada();
            }

            protected void BotonCancelar_Click(object sender, ImageClickEventArgs e)
            {
                OcultarPanelNuevo();
            }

            protected void BotonCancelarBusqueda_Click(object sender, EventArgs e)
            {
                CambiarBusquedaAvanzada();
            }

            protected void BotonCancelarNuevo_Click(object sender, EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
            {
                GuardarProducto();
            }

            protected void BotonLimpiar_Click(object sender, ImageClickEventArgs e)
            {
                LimpiarNuevoRegistro();
            }

            protected void BotonLimpiarBusqueda_Click(object sender, ImageClickEventArgs e)
            {
                LimpiarBusquedaRegistro();
            }

            protected void BusquedaAvanzadaLink_Click(Object sender, System.EventArgs e)
            {
                CambiarBusquedaAvanzada();
            }

            protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarSubfamilia();
            }

            protected void EliminarRegistroLink_Click(Object sender, System.EventArgs e)
            {
                EliminarProducto();
            }

            protected void NuevoRegistro_Click(Object sender, System.EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }        

            protected void TablaProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaProducto.PageIndex = e.NewPageIndex;
                BusquedaAvanzada();
            }

            protected void TablaProducto_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaProductoEventoComando(e);
            }
        #endregion

        #region "Métodos"
            private void Inicio()
            {
                //Opciones
                Master.NuevoRegistroMaster.Click += new EventHandler(NuevoRegistro_Click);
                Master.BusquedaAvanzadaMaster.Click += new EventHandler(BusquedaAvanzadaLink_Click);
                Master.EliminarRegistroMaster.Click += new EventHandler(EliminarRegistroLink_Click);

                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.AlmacenProductos);

                    SeleccionarFamilia();
                    SeleccionarSubfamilia();
                    SeleccionarMarca();
                    SeleccionarUnidadMedida();
                    BusquedaAvanzada();
                }
            }

            private void BusquedaAvanzada()
            {
                AlmacenEntidad AlmacenEntidadObjeto = new AlmacenEntidad();

                AlmacenEntidadObjeto.Clave = ClaveBusqueda.Text.Trim();
                AlmacenEntidadObjeto.Descripcion = DescripcionBusqueda.Text.Trim();
                //AlmacenEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

                SeleccionarProducto(AlmacenEntidadObjeto);
            }

            private void CambiarBusquedaAvanzada()
            {
                PanelBusquedaAvanzada.Visible = !PanelBusquedaAvanzada.Visible;
                PanelNuevoRegistro.Visible = false;
            }

            private void CambiarEditarRegistro()
            {
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = true;
            }

            private void CambiarNuevoRegistro()
            {
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = !PanelNuevoRegistro.Visible;
                LimpiarNuevoRegistro();
                ExistenciaInicialBox.Enabled = true;
            }

            private void EliminarProducto()
            {
                string CadenaProductoId = string.Empty;
                AlmacenProceso AlmacenProcesoObjeto = new AlmacenProceso();

                CadenaProductoId = ObtenerCadenaProductoId();

                AlmacenProcesoObjeto.EliminarProducto(CadenaProductoId);

                if (AlmacenProcesoObjeto.ErrorId == 0)
                {
                    MostrarMensaje(TextoInfo.MensajeBorradoGenerico, ConstantePrograma.TipoMensajeAlerta);

                    BusquedaAvanzada();
                }
                else
                    MostrarMensaje(AlmacenProcesoObjeto.DescripcionError, ConstantePrograma.TipoErrorAlerta);
            }

            private void GuardarProducto()
            {
                AlmacenEntidad AlmacenObjetoEntidad = new AlmacenEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                AlmacenObjetoEntidad.ProductoId = ProductoIdHidden.Value;
                AlmacenObjetoEntidad.Clave = ClaveNuevo.Text.Trim();
                AlmacenObjetoEntidad.FamiliaId = Int16.Parse(FamiliaIdNuevo.SelectedValue);
                AlmacenObjetoEntidad.SubFamiliaId = Int16.Parse(SubFamiliaIdNuevo.SelectedValue);
                AlmacenObjetoEntidad.MarcaId = Int16.Parse(MarcaIdNuevo.SelectedValue);
                AlmacenObjetoEntidad.Descripcion = DescripcionNuevo.Text.Trim();

                if (MinimoNuevo.Text == "")
                    AlmacenObjetoEntidad.Minimo = 0;
                else
                    AlmacenObjetoEntidad.Minimo = Int16.Parse(MinimoNuevo.Text.Trim());

                if (MaximoNuevo.Text == "")
                    AlmacenObjetoEntidad.Maximo = 0;
                else
                    AlmacenObjetoEntidad.Maximo = Int16.Parse(MaximoNuevo.Text.Trim());

                if (MaximoPermitivoNuevo.Text == "")
                    AlmacenObjetoEntidad.MaximoPermitido = 0;
                else
                    AlmacenObjetoEntidad.MaximoPermitido = Int16.Parse(MaximoPermitivoNuevo.Text.Trim());

                if (ExistenciaInicialBox.Text == "")
                    AlmacenObjetoEntidad.ExistenciaInicial = 0;
                else
                    AlmacenObjetoEntidad.ExistenciaInicial = Int16.Parse(ExistenciaInicialBox.Text.Trim());

                AlmacenObjetoEntidad.UnidadMedidaId = (UnidaddeMedidaIdNuevo.SelectedValue);
                    
                if (EstatusProductoNuevo.Checked == true)
                    AlmacenObjetoEntidad.EstatusId = (int)ConstantePrograma.EstatusProducto.Activo;
                else
                    AlmacenObjetoEntidad.EstatusId = (int)ConstantePrograma.EstatusProducto.Inactivo;

                GuardarProducto(AlmacenObjetoEntidad);
            }

            private void GuardarProducto(AlmacenEntidad AlmacenObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

                Resultado = AlmacenProcesoNegocio.GuardarProducto(AlmacenObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Producto.ProductoGuardadoCorrectamente)
                {
                    LimpiarNuevoRegistro();
                    OcultarPanelNuevo();
                    BusquedaAvanzada();
                }
                else
                    MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);
            }

            private void LimpiarBusquedaRegistro()
            {
                ClaveBusqueda.Text = "";
                DescripcionBusqueda.Text = "";
                EtiquetaMensaje.Text = "";
            }

            private void LimpiarNuevoRegistro()
            {
                ClaveNuevo.Text = "";
                DescripcionNuevo.Text = "";
                FamiliaIdNuevo.SelectedIndex = 0;
                SeleccionarSubfamilia();
                MarcaIdNuevo.SelectedIndex = 0;
                MaximoNuevo.Text = "";
                MinimoNuevo.Text = "";
                UnidaddeMedidaIdNuevo.SelectedIndex = 0;
                EstatusProductoNuevo.Checked = true;
                MaximoPermitivoNuevo.Text = "";
                ProductoIdHidden.Value = "";
                ExistenciaInicialBox.Text = "";
                EtiquetaMensaje.Text = "";
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

            private string ObtenerCadenaProductoId()
            {
                StringBuilder CadenaProductoId = new StringBuilder();
                CheckBox CasillaEliminar;

                CadenaProductoId.Append(",");

                foreach (GridViewRow Registro in TablaProducto.Rows)
                {
                    CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                    if (CasillaEliminar.Checked)
                    {
                        CadenaProductoId.Append(TablaProducto.DataKeys[Registro.RowIndex]["ProductoId"].ToString());
                        CadenaProductoId.Append(",");
                    }
                }

                return CadenaProductoId.ToString();
            }

            private void OcultarPanelNuevo()
            {
                PanelNuevoRegistro.Visible = false;
                PanelBusquedaAvanzada.Visible = false;
            }

            private void SeleccionarFamilia()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
                FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

                //FamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusFamilia.Activo;

                Resultado = FamiliaProcesoObjeto.SeleccionarFamilia(FamiliaEntidadObjeto);

                FamiliaIdNuevo.DataValueField = "FamiliaId";
                FamiliaIdNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    FamiliaIdNuevo.DataSource = Resultado.ResultadoDatos;
                    FamiliaIdNuevo.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                FamiliaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarMarca()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MarcaEntidad MarcaEntidadObjeto = new MarcaEntidad();
                MarcaProceso MarcaProcesoObjeto = new MarcaProceso();

                //MarcaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusMarca.Activo;

                Resultado = MarcaProcesoObjeto.SeleccionarMarca(MarcaEntidadObjeto);

                MarcaIdNuevo.DataValueField = "MarcaId";
                MarcaIdNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    MarcaIdNuevo.DataSource = Resultado.ResultadoDatos;
                    MarcaIdNuevo.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                MarcaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarProducto(AlmacenEntidad AlmacenObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

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
                }

            }

            private void SeleccionarProductoParaEditar(AlmacenEntidad AlmacenObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

                Resultado = AlmacenProcesoNegocio.SeleccionarProductoparaEditar(AlmacenObjetoEntidad);

                if (Resultado.ErrorId != 0)
                {
                    MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                ClaveNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Clave"].ToString();
                FamiliaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString();
                SeleccionarSubfamilia();
                SubFamiliaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["SubFamiliaId"].ToString();
                MarcaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["MarcaId"].ToString();
                DescripcionNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreProducto"].ToString();
                MinimoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Minimo"].ToString();
                MaximoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Maximo"].ToString();
                MaximoPermitivoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["MaximoPermitido"].ToString();
                UnidaddeMedidaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["UnidadMedidaId"].ToString();

                if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString()) == (int)ConstantePrograma.EstatusProducto.Activo)
                    EstatusProductoNuevo.Checked = true;
                else
                    EstatusProductoNuevo.Checked = false;

                CambiarEditarRegistro();

                ExistenciaInicialBox.Enabled = false;
            }

            private void SeleccionarSubfamilia()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();
                SubFamiliaProceso SubFamiliaProcesoObjeto = new SubFamiliaProceso();

                //SubFamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusSubFamilia.Activo;
                SubFamiliaEntidadObjeto.FamiliaId = Int16.Parse(FamiliaIdNuevo.SelectedValue);

                if (SubFamiliaEntidadObjeto.FamiliaId == 0)
                {
                    SubFamiliaIdNuevo.Items.Clear();
                }
                else
                {
                    Resultado = SubFamiliaProcesoObjeto.SeleccionarSubFamilia(SubFamiliaEntidadObjeto);

                    SubFamiliaIdNuevo.DataValueField = "SubFamiliaId";
                    SubFamiliaIdNuevo.DataTextField = "Nombre";

                    if (Resultado.ErrorId == 0)
                    {
                        SubFamiliaIdNuevo.DataSource = Resultado.ResultadoDatos;
                        SubFamiliaIdNuevo.DataBind();
                    }
                    else
                    {
                        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    }
                }

                SubFamiliaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarUnidadMedida()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                UnidadMedidaEntidad UnidadMedidaEntidadObjeto = new UnidadMedidaEntidad();
                UnidadMedidaProceso UnidadMedidaProcesoObjeto = new UnidadMedidaProceso();

                //MarcaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusMarca.Activo;

                Resultado = UnidadMedidaProcesoObjeto.SeleccionarUnidadMedida(UnidadMedidaEntidadObjeto);


                UnidaddeMedidaIdNuevo.DataValueField = "UnidadMedidaId";
                UnidaddeMedidaIdNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    UnidaddeMedidaIdNuevo.DataSource = Resultado.ResultadoDatos;
                    UnidaddeMedidaIdNuevo.DataBind();
                }
                else
                    MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);

                UnidaddeMedidaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarTextoError()
            {
                ClaveRequerido.ErrorMessage = TextoError.ClaveProducto + "<br />";
                FamiliaIdRequerido.ErrorMessage = TextoError.FamiliaProducto + "<br />";
                SubFamiliaIdRequerido.ErrorMessage = TextoError.SubFamiliaProducto + "<br />";
                DescripcionRequerido.ErrorMessage = TextoError.DescripcionProducto + "<br />";
                MinimoRequerido.ErrorMessage = TextoError.MinimoProducto + "<br />";
                MaximoPermitidoRequerido.ErrorMessage = TextoError.MaximoPermitidoProducto + "<br />";
                UnidaddeMedidaIdRequerido.ErrorMessage = TextoError.UnidaddeMedidaProducto + "<br />";
                MaximoPermitidoRequerido.ErrorMessage = TextoError.MaximoPermitidoProducto + "<br />";
            }

            private void TablaProductoEventoComando(GridViewCommandEventArgs e)
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

                        SeleccionarProductoParaEditar(AlmacenEntidadObjeto);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }
        #endregion
    }
}
