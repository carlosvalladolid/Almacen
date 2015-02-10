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

using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Almacen;

namespace Activos.Almacen.Aplicacion.Catalogo
{
    public partial class ProveedorAlmacen : System.Web.UI.Page
    {
        #region "Eventos"

        //protected void BotonBusqueda_Click(object sender, EventArgs e)
        //{
        //    BusquedaAvanzada();
        //}         

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        } 
        #endregion

        #region "Métodos"
        private void Inicio()
        {
            //Opciones
            //Master.NuevoRegistroMaster.Click += new EventHandler(NuevoRegistro_Click);
            //Master.BusquedaAvanzadaMaster.Click += new EventHandler(BusquedaAvanzadaLink_Click);
            //Master.EliminarRegistroMaster.Click += new EventHandler(EliminarRegistroLink_Click);

            if (!Page.IsPostBack)
            {
                //SeleccionarFamilia();
                //SeleccionarSubfamilia();
                //SeleccionarMarca();
                //SeleccionarUnidadMedida();
                //BusquedaAvanzada();
            }
        }

        #endregion
    }
}
