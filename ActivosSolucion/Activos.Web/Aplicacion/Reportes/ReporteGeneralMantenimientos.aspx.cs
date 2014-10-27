using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

using Activos.Comun.Constante;
using Activos.Comun.Fecha;
using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Entidad.Mantenimiento;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Mantenimiento;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Aplicacion.Reportes
{
    public partial class ReporteGeneralMantenimientos : System.Web.UI.Page
    {
        #region Eventos

        protected void BotonImprimir_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ImprimirReporte();
            }
        }

        protected void BotonLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        protected void FechaDesde_Validate(object source, ServerValidateEventArgs args)
        {
            string strFechaDesde = string.Empty;
            DateTime dtEndDate;

            strFechaDesde = FechaDesde.Text.Trim();

            if (strFechaDesde != "")
            {
                if (DateTime.TryParse(strFechaDesde, out dtEndDate))
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void FechaHasta_Validate(object source, ServerValidateEventArgs args)
        {
            string strFechaHasta = string.Empty;
            DateTime dtEndDate;

            strFechaHasta = FechaHasta.Text.Trim();

            if (strFechaHasta != "")
            {
                if (DateTime.TryParse(strFechaHasta, out dtEndDate))
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        #endregion


        #region Métodos

        protected void ImprimirReporte()
        {
            //Se pasan los valores a los campos ocultos
            FechaDesdeHidden.Value = FechaDesde.Text.Trim();
            FechaHastaHidden.Value = FechaHasta.Text.Trim();
            EstatusIdHidden.Value = EstatusId.SelectedValue;
            EstatusNombreHidden.Value = EstatusId.SelectedItem.Text;
            TipoMantenimientoIdHidden.Value = TipoMantenimientoId.SelectedValue;
            TipoMantenimientoNombreHidden.Value = TipoMantenimientoId.SelectedItem.Text;


            if (rbDesglosado.Checked == true)
                TipoReporteHidden.Value = "1";
            else
                TipoReporteHidden.Value = "0";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "Imprimir()", true);
        }

        protected void Inicio() 
        {
            if (!Page.IsPostBack)
            {
                //Se validan los permisos
                Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.ReporteGeneralMantenimientos);

                SeleccionarEstatus();
                SeleccionarTipoMantenimiento();
                SeleccionarTextoError();
                

            }
        
        }

        protected void LimpiarFormulario()
        {
            FechaDesde.Text = "";
            FechaHasta.Text = "";
            EstatusId.SelectedIndex = 0;
            TipoMantenimientoId.SelectedIndex = 0;
            rbDesglosado.Checked = true;
            rbConcentrado.Checked = false;
            EtiquetaMensaje.Text = "";
        }

        protected void SeleccionarEstatus()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
            EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

            EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Mantenimientos;

            Resultado = EstatusProcesoObjeto.SeleccionarEstatus(EstatusEntidadObjeto);

            EstatusId.DataValueField = "EstatusId";
            EstatusId.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                EstatusId.DataSource = Resultado.ResultadoDatos;
                EstatusId.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }
        }

        protected void SeleccionarTipoMantenimiento()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TipoMantenimientoEntidad TipoMantenimientoEntidadObjeto = new TipoMantenimientoEntidad();
            TipoMantenimientoProceso TipoMantenimientoProcesoObjeto = new TipoMantenimientoProceso();

            Resultado = TipoMantenimientoProcesoObjeto.SeleccionarTipoMantenimiento(TipoMantenimientoEntidadObjeto);

            TipoMantenimientoId.DataValueField = "TipoMantenimientoId";
            TipoMantenimientoId.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                TipoMantenimientoId.DataSource = Resultado.ResultadoDatos;
                TipoMantenimientoId.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            TipoMantenimientoId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
        }

        protected void SeleccionarTextoError()
        {
            FechaDesdeValidado.ErrorMessage = TextoError.FechaDesdeInvalido + "<br />";
            FechaHastaValidado.ErrorMessage = TextoError.FechaHastaInvalido + "<br />";
        }

        #endregion

       
    }
}
