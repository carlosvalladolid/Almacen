using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Incluir.ControlesWeb
{
    public partial class ControlBuscarJefe : System.Web.UI.UserControl
    {
        #region "Eventos"

            protected void BotonCancelarBusquedaJefe_Click(object sender, EventArgs e)
            {
                OcultarControlPanel();
            }

            protected void BotonBusquedaJefe_Click(object sender, EventArgs e)
            {
                SeleccionarJefe();
            }

            protected void imgCancelarBuscarJefe_Click(object sender, ImageClickEventArgs e)
            {
                OcultarControlPanel();
            }

            protected void TablaJefe_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaJefeEventoComando(e);
            }

            protected void TablaJefe_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaJefe.PageIndex = e.NewPageIndex;
                SeleccionarJefe();
            }

            protected void Page_Load(object sender, EventArgs e)
            {

            }

        #endregion

        #region "Métodos"

            public void InicioControl()
            {
                MostrarControlPanel();
                LimpiarControl();
                SeleccionarJefe();
            }

            protected void LimpiarControl()
            {
                TextoBusqueda.Text = "";
            }

            public void MostrarControlPanel()
            {
                pnlFondo.Visible = true;
                pnlControl.Visible = true;
            }

            protected void OcultarControlPanel()
            {
                pnlFondo.Visible = false;
                pnlControl.Visible = false;
            }

            protected void SeleccionarJefe()
            {
                JefeEntidad JefeEntidadObjeto = new JefeEntidad();

                JefeEntidadObjeto.TextoBusqueda = TextoBusqueda.Text;
                JefeEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusJefes.Activo;
                

                SeleccionarJefe(JefeEntidadObjeto);
            }

            protected void SeleccionarJefe(JefeEntidad JefeEntidadObjeto)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                JefeProceso JefeProcesoNegocio = new JefeProceso();

                Resultado = JefeProcesoNegocio.SeleccionarJefe(JefeEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaJefe.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaJefe.CssClass = ConstantePrograma.ClaseTabla;

                    TablaJefe.DataSource = Resultado.ResultadoDatos;
                    TablaJefe.DataBind();
                }
                else
                {
                    EtiquetaControlMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void TablaJefeEventoComando(GridViewCommandEventArgs e)
            {
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 EmpleadoId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaJefe.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaJefe.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        EmpleadoId = Int16.Parse(TablaJefe.DataKeys[intFila]["EmpleadoId"].ToString());
                        this.Page.GetType().InvokeMember("SeleccionarJefe", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { EmpleadoId });
                        OcultarControlPanel();
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

        #endregion

    }
}