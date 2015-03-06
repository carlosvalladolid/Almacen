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

namespace Almacen.Web.Aplicacion
{
    public partial class Inicio : System.Web.UI.Page
    {
        #region "Events"
            protected void Page_Load(object sender, EventArgs e)
            {
                Principal();
            }
        #endregion

        #region "Methods"
            protected void Principal()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.AlmacenPaginaInicio);
                    SelectMensaje();
                    //Master.DisplayLinks((int)ProgramConstant.Pages.IndexControl);
                }
            }

            protected void SelectMensaje()
            {
                MessageGrid.CssClass = "TablaVacia";

                MessageGrid.DataSource = null;
                MessageGrid.DataBind();
            }
        #endregion
    }
}
