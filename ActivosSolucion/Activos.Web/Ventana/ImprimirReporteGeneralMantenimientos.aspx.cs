using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

using Activos.Comun.Constante;
using Activos.Comun.Fecha;
using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Mantenimiento;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Mantenimiento;


namespace Activos.Web.Ventana
{
    public partial class ImprimirReporteGeneralMantenimientos : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        #endregion

        #region Métodos

        protected void Inicio() 
        {
            if (!Page.IsPostBack)
            {
                MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto = new MantenimientoActivoEntidad();
                MostrarTitulo();

                string FechaDesde = string.Empty;
                string FechaHasta = string.Empty;
                string EstatusId = string.Empty;
                string EstatusNombre = string.Empty;
                string TipoMantenimientoId = string.Empty;
                string TipoMantenimientoNombre= string.Empty;
                string TipoReporte = string.Empty;
                string FechaActual = string.Empty;

                //Se obtienen los datos de los filtros de la pantalla padre
                FechaDesde = Request.Form["ctl00$ContenedorCuerpo$FechaDesdeHidden"].ToString();
                FechaHasta = Request.Form["ctl00$ContenedorCuerpo$FechaHastaHidden"].ToString();
                EstatusId = Request.Form["ctl00$ContenedorCuerpo$EstatusIdHidden"].ToString();
                EstatusNombre = Request.Form["ctl00$ContenedorCuerpo$EstatusNombreHidden"].ToString();
                TipoMantenimientoId = Request.Form["ctl00$ContenedorCuerpo$TipoMantenimientoIdHidden"].ToString();
                TipoMantenimientoNombre = Request.Form["ctl00$ContenedorCuerpo$TipoMantenimientoNombreHidden"].ToString();
                TipoReporte = Request.Form["ctl00$ContenedorCuerpo$TipoReporteHidden"].ToString();

                MantenimientoActivoEntidadObjeto.TipoReporte = Int16.Parse(TipoReporte);
                MantenimientoActivoEntidadObjeto.TipoMantenimientoId = Int16.Parse(TipoMantenimientoId);
                MantenimientoActivoEntidadObjeto.EstatusId = Int16.Parse(EstatusId);
                MantenimientoActivoEntidadObjeto.TipoMovimientoId = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;

                if (FechaDesde != "")
                    MantenimientoActivoEntidadObjeto.StrFechaInicio = FormatoFecha.AsignarFormato(FechaDesde, ConstantePrograma.UniversalFormatoFecha);

                if (FechaHasta != "")
                    MantenimientoActivoEntidadObjeto.StrFechaFin = FormatoFecha.AsignarFormato(FechaHasta, ConstantePrograma.UniversalFormatoFecha);


                if (FechaDesde != "" & FechaHasta != "")
                {
                    FechasLabel.Text = FechaDesde + " al " + FechaHasta;
                }
                else
                {
                    if (FechaDesde != "" & FechaHasta == "")
                        FechasLabel.Text = "Desde " + FechaDesde;

                    if (FechaDesde == "" & FechaHasta != "")
                        FechasLabel.Text = "Hasta " + FechaHasta;

                }

                EstatusLabel.Text = EstatusNombre;
                TipoMantenimientoLabel.Text = TipoMantenimientoNombre;
                if (TipoReporte == "0")
                {
                    TipoReporteLabel.Text = "Concentrado";
                }
                else 
                { 
                    TipoReporteLabel.Text = "Desglosado";
                }

                FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
                FechaLabel.Text = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);

                LlenarTablas(MantenimientoActivoEntidadObjeto);

                
            }
        }

        protected void LlenarTablas(MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto)
        {
            MantenimientoActivoProceso MantenimientoActivoProcesoObjeto = new MantenimientoActivoProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            Resultado = MantenimientoActivoProcesoObjeto.SeleccionarMantenimientoReporteGeneral(MantenimientoActivoEntidadObjeto);

            if (Resultado.ErrorId == 0)
            {

                if (MantenimientoActivoEntidadObjeto.TipoReporte == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaConcentrados.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaConcentrados.CssClass = ConstantePrograma.ClaseTablaImpresion;

                    DivTablaReporteConcentrado.Visible = true;
                    TablaConcentrados.DataSource = Resultado.ResultadoDatos;
                    TablaConcentrados.DataBind();
                }
                else
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaDesglosados.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaDesglosados.CssClass = ConstantePrograma.ClaseTablaImpresion;

                    DivTablaReporteDesglosado.Visible = true;
                    TablaDesglosados.DataSource = Resultado.ResultadoDatos;
                    TablaDesglosados.DataBind();
                }
            }
            
        }

        protected void MostrarTitulo()
        {

            Master.MostrarTitulo("GENERAL DE MANTENIMIENTOS", "REPORTE");
        }

        #endregion

        
    }
}
