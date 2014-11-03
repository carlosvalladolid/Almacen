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
    public partial class Marca : System.Web.UI.Page
    {
        #region "Eventos"
            protected void AdvancedSearchLink_Click(Object sender, System.EventArgs e)
            {

            }

            protected void BotonBusquedaRapida_Click(object sender, ImageClickEventArgs e)
            {

            }

            protected void DeleteRecordLink_Click(Object sender, System.EventArgs e)
            {

            }

            protected void NewRecordLink_Click(Object sender, System.EventArgs e)
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

                SeleccionarMarca();
            }

            private void SeleccionarMarca()
            {
                MarcaEntidad MarcaEntidad = new MarcaEntidad();

                SeleccionarMarca(MarcaEntidad);
            }

            protected void SeleccionarMarca(MarcaEntidad MarcaObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MarcaProceso MarcaProcesoNegocio = new MarcaProceso();

                Resultado = MarcaProcesoNegocio.SeleccionarMarca(MarcaObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaMarca.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaMarca.CssClass = ConstantePrograma.ClaseTabla;

                    TablaMarca.DataSource = Resultado.ResultadoDatos;
                    TablaMarca.DataBind();
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
