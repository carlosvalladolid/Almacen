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
    public partial class ImprimirRequisicion : System.Web.UI.Page
    {
        #region "Eventos"

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
                string RequisicionId = string.Empty;

                RequisicionId = Request.QueryString["RequisicionId"].ToString();                         
                SeleccionarEncabezadoRequisicion(RequisicionId);
                SeleccionarDetalleRequisicion(RequisicionId);              
                //MostrarTitulo();
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

        protected void SeleccionarDetalleRequisicion(string RequisicionId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RequisicionEntidad RequisicionObjetoEntidad = new RequisicionEntidad();
            RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

            RequisicionObjetoEntidad.RequisicionId = RequisicionId;

            Resultado = RequisicionProcesoNegocio.SeleccionarRequisicionDetalleReporte(RequisicionObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                ReportDataSource ParametroFuenteDatos = new ReportDataSource("ImprimirRequisicionDS_ImprimirProductoDT", Resultado.ResultadoDatos.Tables[0]);

                ImprimirRequisicionReporteViewer.LocalReport.DataSources.Clear();
                ImprimirRequisicionReporteViewer.LocalReport.DataSources.Add(ParametroFuenteDatos);
                ImprimirRequisicionReporteViewer.LocalReport.Refresh();
                
                CantidadProductoLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows.Count.ToString();
               
            }
        }

        protected void SeleccionarEncabezadoRequisicion(string RequisicionId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RequisicionEntidad RequisicionObjetoEntidad = new RequisicionEntidad();
            RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

            RequisicionObjetoEntidad.RequisicionId = RequisicionId;

            Resultado = RequisicionProcesoNegocio.SeleccionarSolicitanteRequisicionReporte(RequisicionObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
               //string Solicitante =  Resultado.ResultadoDatos.Tables[0].Rows[0]["Solicitante"].ToString();


                ReportParameter ParameterSolicitante = new ReportParameter("Solicitante", Resultado.ResultadoDatos.Tables[0].Rows[0]["Solicitante"].ToString(), true);
                ReportParameter ParameterJefeInmediato = new ReportParameter("JefeInmediato", Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoJefe"].ToString(), true);
                ReportParameter ParameterDireccion = new ReportParameter("Direccion", Resultado.ResultadoDatos.Tables[0].Rows[0]["Direccion"].ToString(), true);
                ReportParameter ParameterDependencia = new ReportParameter("Dependencia", Resultado.ResultadoDatos.Tables[0].Rows[0]["Dependencia"].ToString(), true);
                ReportParameter ParameterPuesto = new ReportParameter("Puesto", Resultado.ResultadoDatos.Tables[0].Rows[0]["Puesto"].ToString(), true);
                ReportParameter ParameterFirmaSolicitante = new ReportParameter("FirmaSolicitante", Resultado.ResultadoDatos.Tables[0].Rows[0]["Solicitante"].ToString(), true);
                ReportParameter ParameterEncargadoBodega = new ReportParameter("EncargadoBodega", ConfigurationManager.AppSettings["Almacen.Web.EncargadoBodega"].ToString(), true);

                ReportParameter[] ParameterList = new ReportParameter[] 
                    { 
                    ParameterSolicitante, 
                    ParameterJefeInmediato,
                    ParameterDireccion,
                    ParameterDependencia,
                    ParameterPuesto,
                    ParameterFirmaSolicitante,
                    ParameterEncargadoBodega
                    };
                ImprimirRequisicionReporteViewer.LocalReport.SetParameters(ParameterList);
            }
            else
                MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);
        }
       
        #endregion
        }
    }

