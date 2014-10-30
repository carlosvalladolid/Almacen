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

namespace Almacen.Web.Incluir.Plantilla
{
    public partial class PlantillaPrivada : System.Web.UI.MasterPage
    {
        public LinkButton AdvancedSearchFromMasterPageLink
        {
            get { return AdvancedSearchLink; }
        }

        public LinkButton DeleteFromMasterPageLink
        {
            get { return DeleteRecordLink; }
        }

        public LinkButton NewFromMasterPageLink
        {
            get { return NewRecordLink; }
        }

        #region "Eventos"
            protected void AdvancedSearchLink_Click(Object sender, System.EventArgs e)
            {

            }

            protected void DeleteRecordLink_Click(object sender, EventArgs e)
            {

            }

            protected void NewRecordLink_Click(Object sender, System.EventArgs e)
            {

            }

            protected void Page_Load(object sender, EventArgs e)
            {

            }
        #endregion

        #region "Métodos"

        #endregion
    }
}
