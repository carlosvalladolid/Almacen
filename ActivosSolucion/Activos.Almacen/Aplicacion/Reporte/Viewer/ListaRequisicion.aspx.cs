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
    public partial class ListaRequisicion : System.Web.UI.Page
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
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.AlmacenListaDeRequisiciones);
                SeleccionarListaRequisicion();              
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
             

        protected void SeleccionarListaRequisicion()
        {
            //ResultadoEntidad Resultado = new ResultadoEntidad();           
            RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

            RequisicionProcesoNegocio.RequisicionEntidad.RequisicionId = "";
            RequisicionProcesoNegocio.RequisicionEntidad.Clave = "";
            RequisicionProcesoNegocio.RequisicionEntidad.Nombre = "";
            RequisicionProcesoNegocio.RequisicionEntidad.FechaInicial = "";
            RequisicionProcesoNegocio.RequisicionEntidad.FechaFinal = "";
            RequisicionProcesoNegocio.RequisicionEntidad.EstatusId = 0;

           // Resultado = RequisicionProcesoNegocio.SeleccionarRequisicionSalida();
           RequisicionProcesoNegocio.SeleccionarRequisicionSalida();

           if (RequisicionProcesoNegocio.ErrorId != 0)
           {
               MostrarMensaje(RequisicionProcesoNegocio.DescripcionError, ConstantePrograma.TipoErrorAlerta);
               return;
           }

                ReportDataSource ParametroFuenteDatos = new ReportDataSource("ListaRequisicionDS_ListaRequisicionDT", RequisicionProcesoNegocio.ResultadoDatos.Tables[0]);

                ListaRequisicionReportViewer1.LocalReport.DataSources.Clear();
                ListaRequisicionReportViewer1.LocalReport.DataSources.Add(ParametroFuenteDatos);
                ListaRequisicionReportViewer1.LocalReport.Refresh();
          
        }

       
        #endregion
    }
}
