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
                //GuardarMarca();
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

            private void Inicio()
            {
                //Master.NuevoRegistroMaster.Click += new EventHandler(NuevoRegistro_Click);
                //Master.BusquedaAvanzadaMaster.Click += new EventHandler(BusquedaAvanzadaLink_Click);
                //Master.EliminarRegistroMaster.Click += new EventHandler(EliminarRegistroLink_Click);

                if (!Page.IsPostBack)
                {
                    SeleccionarFamilia();
                }
            }

            private void LimpiarNuevoRegistro()
            {

            }

            protected void SeleccionarFamilia()
            {
                FamiliaEntidad FamiliaEntidad = new FamiliaEntidad();

                SeleccionarFamilia(FamiliaEntidad);
            }

            protected void SeleccionarFamilia(FamiliaEntidad FamiliaObjetoEntidad)
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
        #endregion
    }
}
