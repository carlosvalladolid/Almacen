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

namespace Activos.Web.Incluir.Plantilla
{
    public partial class PlantillaReporteImpresion : System.Web.UI.MasterPage
    {
        #region "Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();


        }

        #endregion

        #region "Methods"

        protected void Inicio()
        {
            UsuarioEntidad UsuarioEntidadObjeto = new UsuarioEntidad();

            try
            {
                if (!Page.IsPostBack)
                {

                }
            }
            catch
            {
                Response.Redirect("/Aplicacion/Salir.aspx", true);
            }
        }

        public void MostrarTitulo(string TituloInferior, string TituloSuperior)
        {
            TextoInferior.Text = TituloInferior;
            TextoSuperior.Text = TituloSuperior;
        }

        #endregion
    }
}
