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
using Activos.Entidad.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Activos;

namespace Activos.Web.Incluir.ControlesWeb
{
    public partial class ControlImprimirTransferenciaActivo : System.Web.UI.UserControl
    {
        #region "Eventos"

            protected void BotonAceptar_Click(object sender, EventArgs e)
            {
                CerrarControl();
            }

            protected void TablaDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaDocumentosEventoComando(e);
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
                BuscarDocumentos();
            }

            protected void BuscarDocumentos()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoEntidadObjeto = new TemporalTransferenciaActivoEntidad();
                TemporalTransferenciaActivoProceso TemporalTransferenciaActivoProcesoNegocio = new TemporalTransferenciaActivoProceso();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                TemporalTransferenciaActivoEntidadObjeto.SesionId = UsuarioSessionEntidad.SesionId;

                Resultado = TemporalTransferenciaActivoProcesoNegocio.SeleccionarTemporalTransferenciaActivoPorDocumento(TemporalTransferenciaActivoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaDocumentos.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaDocumentos.CssClass = ConstantePrograma.ClaseTabla;

                    TablaDocumentos.DataSource = Resultado.ResultadoDatos;
                    TablaDocumentos.DataBind();
                }
                else
                {
                    ControlImprimirTransferenciaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void LimpiarControl()
            {
                TablaDocumentos.DataSource = null;
                TablaDocumentos.DataBind();
                ControlImprimirTransferenciaMensaje.Text = "";
            }

            public void MostrarControlPanel()
            {
                pnlFondoImprimirTransferencia.Visible = true;
                pnlControlImprimirTransferencia.Visible = true;
            }

            protected void OcultarControlPanel()
            {
                pnlFondoImprimirTransferencia.Visible = false;
                pnlControlImprimirTransferencia.Visible = false;
            }

            protected void CerrarControl()
            {
                LimpiarControl();
                OcultarControlPanel();
                this.Page.GetType().InvokeMember("LimpiarFormulario", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
            }

            protected void TablaDocumentosEventoComando(GridViewCommandEventArgs e)
            {
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                int CompraId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaDocumentos.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaDocumentos.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        CompraId = Int16.Parse(TablaDocumentos.DataKeys[intFila]["CompraId"].ToString());

                        this.Page.GetType().InvokeMember("ImprimirDocumento", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { CompraId });

                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

        #endregion
    }
}