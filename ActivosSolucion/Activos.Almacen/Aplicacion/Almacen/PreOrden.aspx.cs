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


namespace Activos.Almacen.Aplicacion.Almacen
{
    public partial class PreOrden : System.Web.UI.Page
    {
        #region "Eventos"

        protected void AdvancedSearchLink_Click(Object sender, System.EventArgs e)
        {

        }

        protected void BusquedaAvanzadaLink_Click(Object sender, System.EventArgs e)
        {
            CambiarBusquedaAvanzada();
        }

        protected void EliminarRegistroLink_Click(Object sender, System.EventArgs e)
        {
            EliminarPreOrden();
        }

        protected void NuevoRegistro_Click(Object sender, System.EventArgs e)
        {
            CambiarNuevoRegistro();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }    

        #endregion


        #region "Métodos"


        private void Inicio()
        {
          

            if (!Page.IsPostBack)
            {

                TablaPreOrden.DataSource =null;
                TablaPreOrden.DataBind();
                
                // SeleccionarFamilia();
                //SeleccionarSubfamilia();
                //SeleccionarMarca();
                // BusquedaAvanzada();
            }

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
           // LimpiarNuevoRegistro();
        }



        private void EliminarPreOrden()
        { 
        
        }

        #endregion
    }
}
