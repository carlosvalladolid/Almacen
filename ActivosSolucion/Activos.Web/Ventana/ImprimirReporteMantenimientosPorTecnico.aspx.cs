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
using Activos.Entidad.Mantenimiento;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Mantenimiento;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Ventana
{
    public partial class ImprimirReporteMantenimientosPorTecnico : System.Web.UI.Page
    {
        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        #endregion

        #region "Métodos"

        protected void Inicio()
        {
            if (!Page.IsPostBack)
            {
                MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto = new MantenimientoActivoEntidad();

                MostrarTitulo();

                string FechaActual = string.Empty;
                string FechaInicio = string.Empty;
                string FechaFin = string.Empty;

                string EstatusId = string.Empty;
                string EstatusNombre = string.Empty;
                string TipoMantenimientoId = string.Empty;
                string TipoMantenimientoNombre = string.Empty;
                string EmpleadoId = string.Empty;
                string EmpleadoNombre = string.Empty;
                string TipoReporte = string.Empty;

                //Se obtienen los datos de los filtros de la pantalla padre
                FechaInicio = Request.Form["ctl00$ContenedorCuerpo$FechaDesdeHidden"].ToString();
                FechaFin = Request.Form["ctl00$ContenedorCuerpo$FechaHastaHidden"].ToString();
                EstatusId = Request.Form["ctl00$ContenedorCuerpo$EstatusIdHidden"].ToString();
                EstatusNombre = Request.Form["ctl00$ContenedorCuerpo$EstatusNombreHidden"].ToString();
                TipoMantenimientoId = Request.Form["ctl00$ContenedorCuerpo$TipoMantenimientoIdHidden"].ToString();
                TipoMantenimientoNombre = Request.Form["ctl00$ContenedorCuerpo$TipoMantenimientoNombreHidden"].ToString();
                EmpleadoId = Request.Form["ctl00$ContenedorCuerpo$EmpleadoIdHidden"].ToString();
                EmpleadoNombre = Request.Form["ctl00$ContenedorCuerpo$EmpleadoNombreHidden"].ToString();
                TipoReporte = Request.Form["ctl00$ContenedorCuerpo$TipoReporteHidden"].ToString();

                //Se llenan los datos del encabezado con los filtros
                EstatusLabel.Text = EstatusNombre;
                TipoMantenimientoLabel.Text = TipoMantenimientoNombre;
                TecnicoAtendioLabel.Text = EmpleadoNombre;

                if (TipoReporte == "1")
                    TipoReporteLabel.Text = "Desglosado";
                else
                    TipoReporteLabel.Text = "Concentrado";

                if (FechaInicio != "" & FechaFin != "")
                {
                    FechasLabel.Text = FechaInicio + " al " + FechaFin;
                }
                else
                {
                    if (FechaInicio != "" & FechaFin == "")
                        FechasLabel.Text = "Desde " + FechaInicio;

                    if (FechaInicio == "" & FechaFin != "")
                        FechasLabel.Text = "Hasta " + FechaFin;

                }

                //Se buscan los activos
                MantenimientoActivoEntidadObjeto.EstatusId = Int16.Parse(EstatusId);
                MantenimientoActivoEntidadObjeto.TipoReporte = Int16.Parse(TipoReporte);
                MantenimientoActivoEntidadObjeto.TipoMantenimientoId = Int16.Parse(TipoMantenimientoId);
                MantenimientoActivoEntidadObjeto.TipoMovimientoId = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;
                MantenimientoActivoEntidadObjeto.EmpleadoId = Int16.Parse(EmpleadoId);

                if (FechaInicio != "")
                    MantenimientoActivoEntidadObjeto.StrFechaInicio = FormatoFecha.AsignarFormato(FechaInicio, ConstantePrograma.UniversalFormatoFecha);

                if (FechaFin != "")
                    MantenimientoActivoEntidadObjeto.StrFechaFin = FormatoFecha.AsignarFormato(FechaFin, ConstantePrograma.UniversalFormatoFecha);


                SeleccionarMantenimientos(MantenimientoActivoEntidadObjeto);

                FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
                FechaLabel.Text = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);

            }
        }

        protected void MostrarTitulo()
        {

            Master.MostrarTitulo("MANTENIMIENTOS POR TÉCNICO", "REPORTE");
        }

        protected void SeleccionarMantenimientos(MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto)
        {
            MantenimientoActivoProceso MantenimientoActivoProcesoObjeto = new MantenimientoActivoProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            Resultado = MantenimientoActivoProcesoObjeto.SeleccionarMantenimientoReportePorTecnico(MantenimientoActivoEntidadObjeto);

            if (Resultado.ErrorId == 0)
            {
                if (MantenimientoActivoEntidadObjeto.TipoReporte == 1)
                {
                    DivTablaDesglosado.Visible = true;

                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaDesglosado.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaDesglosado.CssClass = ConstantePrograma.ClaseTablaImpresion;

                    TablaDesglosado.DataSource = Resultado.ResultadoDatos;
                    TablaDesglosado.DataBind();
                }
                else
                {
                    DivTablaConcentrado.Visible = true;

                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaConcentrado.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaConcentrado.CssClass = ConstantePrograma.ClaseTablaImpresion;

                    TablaConcentrado.DataSource = Resultado.ResultadoDatos;
                    TablaConcentrado.DataBind();

                }

                
            }


        }

        #endregion
    }
}
