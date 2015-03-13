using Microsoft.Reporting.WebForms;

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

using Activos.Comun.Cadenas;
using Activos.Comun.Constante;
using Activos.Entidad.Almacen;
using Activos.Entidad.Catalogo;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Almacen;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Seguridad;


namespace Activos.Almacen.Aplicacion.Reporte.Viewer
{
    public partial class FechaVencimientoFactura : System.Web.UI.Page
    {
        #region "Eventos"

        protected void BotonBusqueda_Click(object sender, EventArgs e)
        {
            SeleccionarReporteFechaVencimientoFactura();
        }

        protected void BotonCancelarBusqueda_Click(object sender, EventArgs e)
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
            if (!Page.IsPostBack)
            {
                //Validamos permisos
                Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.AlmacenFechaDeVencimientoDeFactura);

                SeleccionarProveedor();
                VencimientoFacturaReportViewer1.Visible = false;
                SeleccionarReporteFechaVencimientoFactura();             
            }

        }

        private void MostrarMensaje(string Mensaje, string TipoMensaje)
        {
            StringBuilder FormatoMensaje = new StringBuilder();

            FormatoMensaje.Append("MostrarMensaje(\"");
            FormatoMensaje.Append(Mensaje);
            FormatoMensaje.Append("\", \"");
            FormatoMensaje.Append(TipoMensaje);
            FormatoMensaje.Append("\");");

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Mensaje", Comparar.ReemplazarCadenaJavascript(FormatoMensaje.ToString()), true);
        }

        protected void SeleccionarReporteFechaVencimientoFactura()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RecepcionEntidad RecepcionObjetoEntidad = new RecepcionEntidad();
            RecepcionProceso RecepcionProcesoNegocio = new RecepcionProceso();

            //if (FechaDesde.Text != "")
            //    RecepcionObjetoEntidad.FechaInicial = FormatoFecha.AsignarFormato(FechaDesde.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);

            //if (FechaHasta.Text != "")
            //    RecepcionObjetoEntidad.FechaFinal = FormatoFecha.AsignarFormato(FechaHasta.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);



            RecepcionObjetoEntidad.ProveedorId = Int16.Parse(ProveedorCombo.SelectedValue);

            Resultado = RecepcionProcesoNegocio.SeleccionarReporteFechaVencimientoFactura(RecepcionObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                ReportDataSource ParametroFuenteDatos = new ReportDataSource("VencimientoFacturaDS_VencimientoFacturaDT", Resultado.ResultadoDatos.Tables[0]);

                VencimientoFacturaReportViewer1.LocalReport.DataSources.Clear();
                VencimientoFacturaReportViewer1.LocalReport.DataSources.Add(ParametroFuenteDatos);
                VencimientoFacturaReportViewer1.LocalReport.Refresh();

            }
        }


        private void SeleccionarProveedor()
        {
            Activos.ProcesoNegocio.Almacen.ProveedorProceso ProveedorProceso = new Activos.ProcesoNegocio.Almacen.ProveedorProceso();

            ProveedorProceso.SeleccionarProveedor();

            ProveedorCombo.DataValueField = "ProveedorId";
            ProveedorCombo.DataTextField = "Nombre";

            if (ProveedorProceso.ErrorId == 0)
            {
                ProveedorCombo.DataSource = ProveedorProceso.ResultadoDatos;
                ProveedorCombo.DataBind();
            }
            else
            {
                // ToDo: Manejar mensajes de error
                //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            ProveedorCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }


        #endregion
    }
}
