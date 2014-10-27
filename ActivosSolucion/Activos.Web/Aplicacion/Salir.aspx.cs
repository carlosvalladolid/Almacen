using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Activos.Web.Aplicacion
{
    public partial class Salir : System.Web.UI.Page
    {
        #region "Eventos"
            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }
        #endregion

        #region "Métodos"
            protected void Inicio()
            {
                //Session.Abandon();
                //FormsAuthentication.SignOut();

                //Response.Redirect(ConfigurationManager.AppSettings["Activos.Web.LogoutURL"].ToString(), true);
                Response.Redirect("/Inicio.aspx", true);
            }
        #endregion
    }
}
