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
              // TextoBusquedaRapida.Text = "";
                BusquedaAvanzada();
            }         

            protected void BotonBusquedaRapida_Click(object sender, ImageClickEventArgs e)
            {
                DescripcionBusqueda.Text = "";
                BusquedaAvanzada();
            }

            protected void BusquedaAvanzadaLink_Click(Object sender, System.EventArgs e)
            {
                CambiarBusquedaAvanzada();
            }

            protected void EliminarRegistroLink_Click(Object sender, System.EventArgs e)
            {
                EliminarProducto();
            }

            protected void NuevoRegistro_Click(Object sender, System.EventArgs e)
            {
                CambiarNuevoRegistro();
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

            protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarSubfamilia();
            }

            protected void BotonLimpiarBusqueda_Click(object sender, ImageClickEventArgs e)
            {
                LimpiarBusquedaRegistro();

            }

            protected void BotonLimpiar_Click(object sender, ImageClickEventArgs e)
            {
                LimpiarNuevoRegistro();

            }

            protected void BotonCancelar_Click(object sender, ImageClickEventArgs e)
            {

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
                Master.NuevoRegistroMaster.Click += new EventHandler(NuevoRegistro_Click);
                Master.BusquedaAvanzadaMaster.Click += new EventHandler(BusquedaAvanzadaLink_Click);
                Master.EliminarRegistroMaster.Click += new EventHandler(EliminarRegistroLink_Click);

                if (!Page.IsPostBack)
                {
                    SeleccionarFamilia();
                    SeleccionarSubfamilia();
                    SeleccionarMarca();
                    SeleccionarUnidadMedida();
                    BusquedaAvanzada();
                }
            }

            protected void BusquedaAvanzada()
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

            protected void CambiarEditarRegistro()
            {
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = true;
            }

            private void CambiarNuevoRegistro()
            {
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = !PanelNuevoRegistro.Visible;
                LimpiarNuevoRegistro();
            }

            private void EliminarProducto()
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                AlmacenEntidad AlmacenEntidadObjeto = new AlmacenEntidad();

                AlmacenEntidadObjeto.CadenaProductoId = ObtenerCadenaProductoId();

                EliminarProducto(AlmacenEntidadObjeto);
            }

            private void EliminarProducto(AlmacenEntidad AlmacenEntidadObjeto)
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                AlmacenProceso AlmacenProcesoObjeto = new AlmacenProceso();

                ResultadoEntidadObjeto = AlmacenProcesoObjeto.EliminarProducto(AlmacenEntidadObjeto);

                if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Producto.EliminadoExitosamente)
                {
                    // ToDo: Manejar mensajes de error
                    //EtiquetaMensaje.Text = "";

                    BusquedaAvanzada();
                }
                else
                {
                    // ToDo: Manejar mensajes de error
                    //EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
                }
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

            protected void LimpiarNuevoRegistro()
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
                EtiquetaMensaje.Text = "";
            
            }

            protected void LimpiarBusquedaRegistro()
            { 
                ClaveBusqueda.Text = "";
                DescripcionBusqueda.Text = "";
                EtiquetaMensaje.Text = "";        
            }        

            protected void GuardarProducto()
            {
                AlmacenEntidad AlmacenObjetoEntidad = new AlmacenEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                if (ProductoIdHidden.Value == "")
                {

                    UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                    AlmacenObjetoEntidad.ProductoId = ProductoIdHidden.Value;
                    AlmacenObjetoEntidad.Clave = ClaveNuevo.Text.Trim();
                    AlmacenObjetoEntidad.FamiliaId = Int16.Parse(FamiliaIdNuevo.SelectedValue);
                    AlmacenObjetoEntidad.SubFamiliaId = Int16.Parse(SubFamiliaIdNuevo.SelectedValue);
                    AlmacenObjetoEntidad.MarcaId = Int16.Parse(MarcaIdNuevo.SelectedValue);
                    AlmacenObjetoEntidad.Descripcion = DescripcionNuevo.Text.Trim();

                    if (MinimoNuevo.Text == "")
                    { AlmacenObjetoEntidad.Minimo = 0; }
                    else
                    { AlmacenObjetoEntidad.Minimo = Int16.Parse(MinimoNuevo.Text.Trim()); }

                    
                    if (MaximoNuevo.Text == "")
                    { AlmacenObjetoEntidad.Maximo = 0; }
                    else
                    { AlmacenObjetoEntidad.Maximo = Int16.Parse(MaximoNuevo.Text.Trim()); }

                    if (MaximoPermitivoNuevo.Text == "")
                    { AlmacenObjetoEntidad.MaximoPermitido = 0; }
                    else
                    { AlmacenObjetoEntidad.MaximoPermitido = Int16.Parse(MaximoPermitivoNuevo.Text.Trim()); }


                    AlmacenObjetoEntidad.UnidadMedidaId = (UnidaddeMedidaIdNuevo.SelectedValue);

                    if (EstatusProductoNuevo.Checked == true)
                    { AlmacenObjetoEntidad.EstatusId = true; }
                    else
                    { AlmacenObjetoEntidad.EstatusId = false; }

                    GuardarProducto(AlmacenObjetoEntidad);
                }
                             

            }

            protected void GuardarProducto(AlmacenEntidad AlmacenObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

                Resultado = AlmacenProcesoNegocio.GuardarProducto(AlmacenObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Producto.ProductoGuardadoCorrectamente)
                {
                    LimpiarNuevoRegistro();
                    PanelNuevoRegistro.Visible = false;
                    PanelBusquedaAvanzada.Visible = false;
                    BusquedaAvanzada();
                }
                else
                {
                    MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                }
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

            protected void SeleccionarFamilia()
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

            protected void SeleccionarMarca()
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

            protected void SeleccionarSubfamilia()
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

            protected void SeleccionarUnidadMedida()
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
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                UnidaddeMedidaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void TablaProductoEventoComando(GridViewCommandEventArgs e)
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

            protected void SeleccionarProductoParaEditar(AlmacenEntidad AlmacenObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

                Resultado = AlmacenProcesoNegocio.SeleccionarProductoparaEditar(AlmacenObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    ClaveNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Clave"].ToString();
                    FamiliaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString();
                    SeleccionarSubfamilia();
                    SubFamiliaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["SubFamiliaId"].ToString();
                    MarcaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["MarcaId"].ToString();
                    DescripcionNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                    MinimoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Minimo"].ToString();
                    MaximoNuevo.Text =Resultado.ResultadoDatos.Tables[0].Rows[0]["Maximo"].ToString();
                    MaximoPermitivoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["MaximoPermitido"].ToString();
                    UnidaddeMedidaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["UnidadMedidaId"].ToString();

                    CambiarEditarRegistro();

                  //  if (Boolean.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString()) == true)
                    //{
                     //   EstatusProductoNuevo.Checked = true;
                       
                   }
                
                   
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarTextoError()
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

        #endregion
    }
}
