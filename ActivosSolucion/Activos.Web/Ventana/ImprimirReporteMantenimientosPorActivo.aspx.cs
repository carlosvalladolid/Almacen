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
    public partial class ImprimirReporteMantenimientosPorActivo : System.Web.UI.Page
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
                MostrarTitulo();

                string FechaActual = string.Empty;
                string ActivoId = string.Empty;
                string FechaDesde = string.Empty;
                string FechaHasta = string.Empty;
                string CodigoBarras = string.Empty;
                string Descripcion = string.Empty;
                string NumeroSerie = string.Empty;
                string Modelo = string.Empty;
                string Marca = string.Empty;
                string FolioDocumento = string.Empty;
                string EmpleadoAsignado = string.Empty;
                string Proveedor = string.Empty;
                string EstatusId = string.Empty;
                string EstatusNombre = string.Empty;
                

                //Se obtienen los datos de los filtros de la pantalla padre

                ActivoId = Request.Form["ctl00$ContenedorCuerpo$ActivoIdHidden"].ToString();
                FechaDesde = Request.Form["ctl00$ContenedorCuerpo$FechaDesdeHidden"].ToString();
                FechaHasta = Request.Form["ctl00$ContenedorCuerpo$FechaHastaHidden"].ToString();
                CodigoBarras = Request.Form["ctl00$ContenedorCuerpo$CodigoBarrasHidden"].ToString();
                Descripcion = Request.Form["ctl00$ContenedorCuerpo$DescripcionHidden"].ToString();
                NumeroSerie = Request.Form["ctl00$ContenedorCuerpo$NumeroSerieHidden"].ToString();
                Modelo = Request.Form["ctl00$ContenedorCuerpo$ModeloHidden"].ToString();
                Marca = Request.Form["ctl00$ContenedorCuerpo$MarcaHidden"].ToString();
                FolioDocumento = Request.Form["ctl00$ContenedorCuerpo$FolioDocumentoHidden"].ToString();
                EmpleadoAsignado = Request.Form["ctl00$ContenedorCuerpo$EmpleadoAsignadoHidden"].ToString();
                Proveedor = Request.Form["ctl00$ContenedorCuerpo$ProveedorHidden"].ToString();
                EstatusNombre = Request.Form["ctl00$ContenedorCuerpo$EstatusNombreHidden"].ToString();
                EstatusId = Request.Form["ctl00$ContenedorCuerpo$EstatusIdHidden"].ToString();

                //Se llenan los datos del encabezado con los filtros


                EstatusLabel.Text = EstatusNombre.Replace("--","");
                CodigoBarrasLabel.Text = CodigoBarras;
                DescripcionLabel.Text = Descripcion;
                NumeroSerieLabel.Text = NumeroSerie;
                ModeloLabel.Text = Modelo;
                MarcaLabel.Text = Marca;
                EmpleadoAsignadoLabel.Text = EmpleadoAsignado;
                FolioDocumentoLabel.Text = FolioDocumento;
                ProveedorLabel.Text = Proveedor;

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
                FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
                FechaLabel.Text = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);

                SeleccionarMantenimientos(ActivoId, EstatusId, FechaDesde, FechaHasta);
            }
        }

        protected void SeleccionarMantenimientos(string ActivoId, string EstatusId, string FechaDesde, string FechaHasta)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto = new MantenimientoActivoEntidad();
            MantenimientoActivoProceso MantenimientoActivoProcesoObjeto = new MantenimientoActivoProceso();

            MantenimientoActivoEntidadObjeto.ActivoId = int.Parse(ActivoId);
            MantenimientoActivoEntidadObjeto.EstatusId = Int16.Parse(EstatusId);
            if (FechaDesde != "")
                MantenimientoActivoEntidadObjeto.StrFechaInicio = FormatoFecha.AsignarFormato(FechaDesde, ConstantePrograma.UniversalFormatoFecha);

            if (FechaHasta != "")
                MantenimientoActivoEntidadObjeto.StrFechaFin = FormatoFecha.AsignarFormato(FechaHasta, ConstantePrograma.UniversalFormatoFecha);




            Resultado = MantenimientoActivoProcesoObjeto.SeleccionarMantenimientoReportePorActivo(MantenimientoActivoEntidadObjeto);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaMantenimientos.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaMantenimientos.CssClass = ConstantePrograma.ClaseTablaImpresion;

                TablaMantenimientos.DataSource = Resultado.ResultadoDatos;
                TablaMantenimientos.DataBind();

            }
        }

        protected void MostrarTitulo()
        {

            Master.MostrarTitulo("MANTENIMIENTOS POR ACTIVO", "REPORTE");
        }

        #endregion

       
    }
}
