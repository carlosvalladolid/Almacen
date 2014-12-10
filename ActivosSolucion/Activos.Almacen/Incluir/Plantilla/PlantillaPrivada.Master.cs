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

using Activos.Entidad.Seguridad;

namespace Almacen.Web.Incluir.Plantilla
{
    public partial class PlantillaPrivada : System.Web.UI.MasterPage
    {
        public LinkButton BusquedaAvanzadaMaster
        {
            get { return BusquedaAvanzadaLink; }
        }

        public LinkButton EliminarRegistroMaster
        {
            get { return EliminarRegistroLink; }
        }

        public LinkButton NuevoRegistroMaster
        {
            get { return NuevoRegistroLink; }
        }

        #region "Eventos"
            protected void BusquedaAvanzadaLink_Click(Object sender, System.EventArgs e)
            {

            }

            protected void EliminarRegistroLink_Click(object sender, EventArgs e)
            {

            }

            protected void NuevoRegistroLink_Click(Object sender, System.EventArgs e)
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
                UsuarioEntidad UsuarioEntidad = new UsuarioEntidad();

                try
                {
                    UsuarioEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                    UserNameLabel.Text = UsuarioEntidad.Nombre + " " + UsuarioEntidad.ApellidoPaterno;

                    EliminarRegistroLink.Attributes.Add("onclick", "return ValidarCasillas()");
                }
                catch
                {
                    Response.Redirect("/Inicio.aspx");
                }
            }
        #endregion
    }
}
