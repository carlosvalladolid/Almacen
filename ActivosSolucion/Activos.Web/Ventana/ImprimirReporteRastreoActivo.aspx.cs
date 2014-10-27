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
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Ventana
{
    public partial class ImprimirReporteRastreoActivo : System.Web.UI.Page
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
                    ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

                    MostrarTitulo();

                    string FechaActual = string.Empty;
                    string CodigoBarras = string.Empty;
                    string Descripcion = string.Empty;
                    string NumeroSerie = string.Empty;
                    string Modelo = string.Empty;
                    string Marca = string.Empty;
                    string Direccion = string.Empty;
                    string Departamento = string.Empty;
                    string Edificio = string.Empty;
                    string FolioDocumento = string.Empty;
                    string Proveedor = string.Empty;
                    string ActivoId = string.Empty;

                    //Se obtienen los datos de los filtros de la pantalla padre

                    CodigoBarras = Request.Form["ctl00$ContenedorCuerpo$CodigoBarrasHidden"].ToString();
                    Descripcion = Request.Form["ctl00$ContenedorCuerpo$DescripcionHidden"].ToString();
                    NumeroSerie = Request.Form["ctl00$ContenedorCuerpo$NumeroSerieHidden"].ToString();
                    Modelo = Request.Form["ctl00$ContenedorCuerpo$ModeloHidden"].ToString();
                    Marca = Request.Form["ctl00$ContenedorCuerpo$MarcaHidden"].ToString();
                    Direccion = Request.Form["ctl00$ContenedorCuerpo$DireccionHidden"].ToString();
                    Departamento = Request.Form["ctl00$ContenedorCuerpo$DepartamentoHidden"].ToString();
                    Edificio = Request.Form["ctl00$ContenedorCuerpo$EdificioHidden"].ToString();
                    FolioDocumento = Request.Form["ctl00$ContenedorCuerpo$FolioDocumentoHidden"].ToString();
                    Proveedor = Request.Form["ctl00$ContenedorCuerpo$ProveedorHidden"].ToString();
                    ActivoId = Request.Form["ctl00$ContenedorCuerpo$ActivoIdHidden"].ToString();
                    

                    //Se llenan los datos del encabezado con los filtros
                    CodigoBarrasLabel.Text = CodigoBarras;
                    DescripcionLabel.Text = Descripcion;
                    NumeroSerieLabel.Text = NumeroSerie;
                    ModeloLabel.Text = Modelo;
                    MarcaLabel.Text = Marca;
                    DireccionLabel.Text = Direccion;
                    DepartamentoLabel.Text = Departamento;
                    EdificioLabel.Text = Edificio;
                    FolioDocumentoLabel.Text = FolioDocumento;
                    ProveedorLabel.Text = Proveedor;
                   
                    //Se llena el grid
                    SeleccionarMovimientos(int.Parse(ActivoId));

                    FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
                    FechaLabel.Text = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);

                }
            }

            protected void SeleccionarMovimientos(int ActivoId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();

                ActivoObjetoEntidad.ActivoId = ActivoId;

                Resultado = MovimientoProcesoObjeto.SeleccionarMovimientoPorActivo(ActivoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaMovimientos.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaMovimientos.CssClass = ConstantePrograma.ClaseTablaImpresion;

                    TablaMovimientos.DataSource = Resultado.ResultadoDatos;
                    TablaMovimientos.DataBind();
                }

            }   

            protected void MostrarTitulo()
            {

                Master.MostrarTitulo("RASTREO DE ACTIVOS", "REPORTE");
            }

        #endregion
    }
}
