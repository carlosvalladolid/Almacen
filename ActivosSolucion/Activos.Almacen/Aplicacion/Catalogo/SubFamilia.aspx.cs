using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Almacen.Web.Aplicacion.Catalogo
{
    public partial class SubFamilia : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonBusqueda_Click(object sender, EventArgs e)
            {
                //TextoBusquedaRapida.Text = "";
                //BusquedaAvanzada();
            }

            protected void BotonBusquedaRapida_Click(object sender, ImageClickEventArgs e)
            {
                //NombreBusqueda.Text = "";
                //BusquedaAvanzada();
            }

            protected void BotonCancelarBusqueda_Click(object sender, EventArgs e)
            {
                //CambiarBusquedaAvanzada();
            }

            protected void BotonCancelarNuevo_Click(object sender, EventArgs e)
            {
                //CambiarNuevoRegistro();
            }

            protected void BotonGuardar_Click(object sender, EventArgs e)
            {
                //GuardarSubFamilia();
            }

            protected void BusquedaAvanzadaLink_Click(Object sender, System.EventArgs e)
            {
                CambiarBusquedaAvanzada();
            }

            protected void EliminarRegistroLink_Click(Object sender, System.EventArgs e)
            {

            }

            protected void NuevoRegistro_Click(Object sender, System.EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

            protected void TablaSubFamilia_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                //TablaSubFamilia.PageIndex = e.NewPageIndex;
                //BusquedaAvanzada();
            }

            protected void TablaSubFamilia_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                //TablaSubFamiliaEventoComando(e);
            }
        #endregion

        #region "Métodos"
            private void CambiarBusquedaAvanzada()
            {
                PanelBusquedaAvanzada.Visible = !PanelBusquedaAvanzada.Visible;
                PanelNuevoRegistro.Visible = false;
            }

            private void CambiarNuevoRegistro()
            {
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = !PanelNuevoRegistro.Visible;
                LimpiarNuevoRegistro();
            }

            private void Inicio()
            {
                Master.NuevoRegistroMaster.Click += new EventHandler(NuevoRegistro_Click);
                Master.BusquedaAvanzadaMaster.Click += new EventHandler(BusquedaAvanzadaLink_Click);
                Master.EliminarRegistroMaster.Click += new EventHandler(EliminarRegistroLink_Click);

                if (!Page.IsPostBack)
                {
                    SeleccionarFamiliaNuevo();
                    //SeleccionarEstatusNuevo();
                    SeleccionarSubFamilia();
                }
            }

            private void LimpiarNuevoRegistro()
            {
                FamiliaNuevo.SelectedIndex = 0;
                EstatusNuevo.SelectedIndex = 0;
                NombreNuevo.Text = "";
                SubFamiliaIdHidden.Value = "0";
            }

            protected void SeleccionarFamiliaNuevo()
            {
                //ResultadoEntidad Resultado = new ResultadoEntidad();
                //FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
                //FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

                ////FamiliaEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Familia;

                //Resultado = FamiliaProcesoObjeto.SeleccionarFamilia(FamiliaEntidadObjeto);

                //FamiliaNuevo.DataValueField = "FamiliaId";
                //FamiliaNuevo.DataTextField = "Nombre";

                //if (Resultado.ErrorId == 0)
                //{
                //    FamiliaNuevo.DataSource = Resultado.ResultadoDatos;
                //    FamiliaNuevo.DataBind();
                //}
                //else
                //{
                //    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                //}

                //FamiliaNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarSubFamilia()
            {
                TablaSubFamilia.DataSource = null;
                TablaSubFamilia.DataBind();
            }
        #endregion
    }
}
