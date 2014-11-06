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

using Activos.Comun.Constante;
using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Almacen.Web.Incluir.ControlesWeb
{
    public partial class ControlMenu : System.Web.UI.UserControl
    {
        #region "Eventos"
            protected void Page_Load(object sender, EventArgs e)
            {
                PageLoad();
            }
        #endregion

        #region "Métodos"
            private void PageLoad()
            {

            }
        #endregion
    }
}