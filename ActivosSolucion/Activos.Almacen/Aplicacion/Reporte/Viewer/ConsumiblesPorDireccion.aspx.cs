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
using Activos.Comun.Fecha;
using Activos.Entidad.Almacen;
using Activos.Entidad.Catalogo;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Almacen;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Seguridad;

namespace Almacen.Web.Aplicacion.Reporte.Viewer
{
    public partial class ConsumiblesPorDireccion : System.Web.UI.Page
    {
        #region "Eventos"
        protected void BotonBusqueda_Click(object sender, EventArgs e)
        {
            seleccionarReporteConsumoPorDireccion();
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
                SeleccionarDireccion();
                SeleccionarEstatus();
                //seleccionarReporteConsumoPorDireccion();
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

        private void seleccionarReporteConsumoPorDireccion()
        {
            AlmacenEntidad AlmacenEntidad = new AlmacenEntidad();
            AlmacenProceso AlmacenProceso = new AlmacenProceso();
            ResultadoEntidad ResultadoDatosEntidad = new ResultadoEntidad();


            AlmacenEntidad.DireccionId = Int16.Parse(DireccionCombo.SelectedValue);
            AlmacenEntidad.EstatusId = Int16.Parse(EstatusCombo.SelectedValue);
            //if (FechaDesde.Text == ""){AlmacenEntidad.FechaInicial = null;}else{AlmacenEntidad.FechaInicial = FechaDesde.Text.Trim();}
            //if (FechaHasta.Text == ""){ AlmacenEntidad.FechaFinal = null; }else{ AlmacenEntidad.FechaFinal = FechaHasta.Text.Trim(); }



            if (FechaDesde.Text != "")
                AlmacenEntidad.FechaInicial = FormatoFecha.AsignarFormato(FechaDesde.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);

            if (FechaHasta.Text != "")
                AlmacenEntidad.FechaFinal = FormatoFecha.AsignarFormato(FechaHasta.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);



            ResultadoDatosEntidad = AlmacenProceso.SeleccionarConsumoPorDireccion(AlmacenEntidad);

            if (ResultadoDatosEntidad.ErrorId == 0)
            {
                ReportDataSource ParametroFuenteDatos = new ReportDataSource("ConsumoPorDireccionDS_ConsumoPorDireccionDT", ResultadoDatosEntidad.ResultadoDatos.Tables[0]);

                ConsumiblePorDireccionReporteViewer.LocalReport.DataSources.Clear();
                ConsumiblePorDireccionReporteViewer.LocalReport.DataSources.Add(ParametroFuenteDatos);
                ConsumiblePorDireccionReporteViewer.LocalReport.Refresh();
            }

            //MostrarMensaje(AlmacenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
        }

        protected void SeleccionarDireccion()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            DireccionEntidad DireccionEntidadObjeto = new DireccionEntidad();
            DireccionProceso DireccionProcesoObjeto = new DireccionProceso();           

            Resultado = DireccionProcesoObjeto.SeleccionarDireccion(DireccionEntidadObjeto);

            DireccionCombo.DataValueField = "DireccionId";
            DireccionCombo.DataTextField = "Nombre";
          

            if (Resultado.ErrorId == 0)
            {
                DireccionCombo.DataSource = Resultado.ResultadoDatos;
                DireccionCombo.DataBind();
             
            }
            else
            {
                //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            DireccionCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));          
        }

        protected void SeleccionarEstatus()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
            EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

            EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Empleados;

            Resultado = EstatusProcesoObjeto.SeleccionarEstatusOrdenSalida(EstatusEntidadObjeto);

            EstatusCombo.DataValueField = "EstatusId";
            EstatusCombo.DataTextField = "Descripcion";

            if (Resultado.ErrorId == 0)
            {
                EstatusCombo.DataSource = Resultado.ResultadoDatos;
                EstatusCombo.DataBind();
            }
            else
            {
                //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            EstatusCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }


        #endregion
    }
}
