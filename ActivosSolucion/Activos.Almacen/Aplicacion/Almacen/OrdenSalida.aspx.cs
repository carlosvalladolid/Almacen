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

namespace Almacen.Web.Aplicacion.Almacen
{
    public partial class OrdenSalida : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
            {

            }

            protected void ImagenBuscarPreOrden_Click(object sender, ImageClickEventArgs e)
            {

            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }
        #endregion

        #region "Métodos"
            private void Inicio()
            {
                if (Page.IsPostBack)
                    return;

                TablaOrden.DataSource = null;
                TablaOrden.DataBind();
            }
        #endregion
    }
}
