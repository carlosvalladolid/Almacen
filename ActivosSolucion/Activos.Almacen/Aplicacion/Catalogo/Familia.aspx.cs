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

using Activos.Comun.Cadenas;
using Activos.Comun.Constante;
using Activos.Entidad.Almacen;
using Activos.Entidad.Catalogo;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Almacen;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Seguridad;

namespace Almacen.Web.Aplicacion.Catalogo
{
    public partial class Familia : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonBusqueda_Click(object sender, EventArgs e)
            {
                //BusquedaAvanzada();
            }

            protected void BotonBusquedaRapida_Click(object sender, ImageClickEventArgs e)
            {

                //BusquedaRapida();
            }

            protected void BotonCancelarBusqueda_Click(object sender, EventArgs e)
            {
                CambiarBusquedaAvanzada();
            }

            protected void BotonCancelarNuevo_Click(object sender, EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void BotonGuardar_Click(object sender, EventArgs e)
            {
                GuardarFamilia();
            }

            protected void BusquedaAvanzadaLink_Click(Object sender, System.EventArgs e)
            {
                CambiarBusquedaAvanzada();
            }

            protected void EliminarRegistroLink_Click(Object sender, System.EventArgs e)
            {
                //EliminarMarca();
            }

            protected void NuevoRegistro_Click(Object sender, System.EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

            protected void TablaFamilia_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {

            }

            protected void TablaFamilia_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaFamiliaRowCommand(e);
            }
        #endregion

        #region "Métodos"
            private void BusquedaAvanzada()
            {
                //MarcaEntidad MarcaEntidadObjeto = new MarcaEntidad();

                //TextoBusquedaRapida.Text = "";
                //MarcaEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();

                //SeleccionarMarca(MarcaEntidadObjeto);
            }

            private void BusquedaRapida()
            {
                //MarcaEntidad MarcaEntidadObjeto = new MarcaEntidad();

                //MarcaEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

                //SeleccionarMarca(MarcaEntidadObjeto);
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
            }

            protected void GuardarFamilia()
            {
                FamiliaEntidad FamiliaObjetoEntidad = new FamiliaEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                FamiliaObjetoEntidad.FamiliaId = Int16.Parse(FamiliaIdHidden.Value);
                FamiliaObjetoEntidad.DependenciaId = Int16.Parse(DependenciaNuevo.SelectedValue);
                FamiliaObjetoEntidad.EstatusId = Int16.Parse(EstatusNuevo.SelectedValue);
                FamiliaObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
                FamiliaObjetoEntidad.UsuarioIdModifico = UsuarioSessionEntidad.UsuarioId;
                FamiliaObjetoEntidad.Nombre = NombreNuevo.Text.Trim();

                GuardarFamilia(FamiliaObjetoEntidad);
            }

            protected void GuardarFamilia(FamiliaEntidad FamiliaObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                FamiliaProceso FamiliaProcesoNegocio = new FamiliaProceso();

                Resultado = FamiliaProcesoNegocio.GuardarFamilia(FamiliaObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Familia.FamiliaGuardadoCorrectamente)
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

            private void Inicio()
            {
                Master.NuevoRegistroMaster.Click += new EventHandler(NuevoRegistro_Click);
                Master.BusquedaAvanzadaMaster.Click += new EventHandler(BusquedaAvanzadaLink_Click);
                Master.EliminarRegistroMaster.Click += new EventHandler(EliminarRegistroLink_Click);

                if (!Page.IsPostBack)
                {
                    SeleccionarEstatusNuevo();
                    SeleccionarDependencia();
                    SeleccionarFamilia();
                }
            }

            private void LimpiarNuevoRegistro()
            {

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

            protected void SeleccionarDependencia()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DependenciaEntidad DependenciaEntidadObjeto = new DependenciaEntidad();
                DependenciaProceso DependenciaProcesoObjeto = new DependenciaProceso();

                Resultado = DependenciaProcesoObjeto.SeleccionarDependencia(DependenciaEntidadObjeto);

                DependenciaNuevo.DataValueField = "DependenciaId";
                DependenciaNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    DependenciaNuevo.DataSource = Resultado.ResultadoDatos;
                    DependenciaNuevo.DataBind();
                }
                else
                {
                    // ToDo: Manejar mensajes de error
                    //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                DependenciaNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarDependenciaNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DependenciaEntidad DependenciaEntidadObjeto = new DependenciaEntidad();
                DependenciaProceso DependenciaProcesoObjeto = new DependenciaProceso();

                Resultado = DependenciaProcesoObjeto.SeleccionarDependencia(DependenciaEntidadObjeto);

                DependenciaNuevo.DataValueField = "DependenciaId";
                DependenciaNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    DependenciaNuevo.DataSource = Resultado.ResultadoDatos;
                    DependenciaNuevo.DataBind();
                }
                else
                {
                    // ToDo: Manejar mensajes de error
                    //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                DependenciaNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarEstatusNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
                EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

                EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Familia;

                Resultado = EstatusProcesoObjeto.SeleccionarEstatus(EstatusEntidadObjeto);

                EstatusNuevo.DataValueField = "EstatusId";
                EstatusNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    EstatusNuevo.DataSource = Resultado.ResultadoDatos;
                    EstatusNuevo.DataBind();
                }
                else
                {
                    // ToDo: Manejar mensajes de error
                    //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                EstatusNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarFamilia()
            {
                FamiliaEntidad FamiliaEntidad = new FamiliaEntidad();

                SeleccionarFamilia(FamiliaEntidad);
            }

            private void SeleccionarFamilia(FamiliaEntidad FamiliaObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                FamiliaProceso FamiliaProcesoNegocio = new FamiliaProceso();

                Resultado = FamiliaProcesoNegocio.SeleccionarFamilia(FamiliaObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaFamilia.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaFamilia.CssClass = ConstantePrograma.ClaseTabla;

                    TablaFamilia.DataSource = Resultado.ResultadoDatos;
                    TablaFamilia.DataBind();
                }
                else
                {
                    // ToDo: Manejar mensajes de error
                    //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarFamiliaParaEditar(FamiliaEntidad FamiliaObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                FamiliaProceso FamiliaProcesoNegocio = new FamiliaProceso();

                Resultado = FamiliaProcesoNegocio.SeleccionarFamilia(FamiliaObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    DependenciaNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["DependenciaId"].ToString();
                    EstatusNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString();
                    NombreNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
                    CambiarEditarRegistro();
                }
                else
                {
                    // ToDo: Manejar mensajes de error
                    //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            private void TablaFamiliaRowCommand(GridViewCommandEventArgs e)
            {
                FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 FamiliaId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaFamilia.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaFamilia.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        FamiliaId = Int16.Parse(TablaFamilia.DataKeys[intFila]["FamiliaId"].ToString());
                        FamiliaEntidadObjeto.FamiliaId = FamiliaId;
                        FamiliaIdHidden.Value = FamiliaId.ToString();
                        SeleccionarFamiliaParaEditar(FamiliaEntidadObjeto);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }
        #endregion
    }
}
