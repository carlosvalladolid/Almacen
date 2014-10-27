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
    public partial class ReporteGeneralActivo : System.Web.UI.Page
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
                    string FamiliaId = string.Empty;
                    string FamiliaNombre = string.Empty;
                    string SubFamiliaId = string.Empty;
                    string SubFamiliaNombre = string.Empty;
                    string MarcaId = string.Empty;
                    string MarcaNombre = string.Empty;
                    string Modelo = string.Empty;
                    string FechaInicio = string.Empty;
                    string FechaFin = string.Empty;
                    string ProveedorId = string.Empty;
                    string ProveedorNombre = string.Empty;
                    string FolioDocumento = string.Empty;
                    string DireccionId = string.Empty;
                    string DireccionNombre = string.Empty;
                    string DepartamentoId = string.Empty;
                    string DepartamentoNombre = string.Empty;

                    //Se obtienen los datos de los filtros de la pantalla padre

                    FamiliaId = Request.Form["ctl00$ContenedorCuerpo$FamiliaIdHidden"].ToString();
                    FamiliaNombre = Request.Form["ctl00$ContenedorCuerpo$FamiliaNombreHidden"].ToString();
                    SubFamiliaId = Request.Form["ctl00$ContenedorCuerpo$SubfamiliaIdHidden"].ToString();
                    SubFamiliaNombre = Request.Form["ctl00$ContenedorCuerpo$SubfamiliaNombreHidden"].ToString();
                    MarcaId = Request.Form["ctl00$ContenedorCuerpo$MarcaIdHidden"].ToString();
                    MarcaNombre = Request.Form["ctl00$ContenedorCuerpo$MarcaNombreHidden"].ToString();
                    Modelo = Request.Form["ctl00$ContenedorCuerpo$ModeloHidden"].ToString();
                    FechaInicio = Request.Form["ctl00$ContenedorCuerpo$FechaInicioHidden"].ToString();
                    FechaFin = Request.Form["ctl00$ContenedorCuerpo$FechaFinHidden"].ToString();
                    ProveedorId = Request.Form["ctl00$ContenedorCuerpo$ProveedorIdHidden"].ToString();
                    ProveedorNombre = Request.Form["ctl00$ContenedorCuerpo$ProveedorNombreHidden"].ToString();
                    FolioDocumento = Request.Form["ctl00$ContenedorCuerpo$FolioDocumentoHidden"].ToString();
                    DireccionId = Request.Form["ctl00$ContenedorCuerpo$DireccionIdHidden"].ToString();
                    DireccionNombre = Request.Form["ctl00$ContenedorCuerpo$DireccionNombreHidden"].ToString();
                    DepartamentoId = Request.Form["ctl00$ContenedorCuerpo$DepartamentoIdHidden"].ToString();
                    DepartamentoNombre = Request.Form["ctl00$ContenedorCuerpo$DepartamentoNombreHidden"].ToString();

                    //Se llenan los datos del encabezado con los filtros
                    FamiliaLabel.Text = FamiliaNombre;
                    SubfamiliaLabel.Text = SubFamiliaNombre;
                    MarcaLabel.Text = MarcaNombre;
                    ModeloLabel.Text = Modelo;

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

                    ProveedorLabel.Text = ProveedorNombre;
                    FolioDocumentoLabel.Text = FolioDocumento;
                    DireccionLabel.Text = DireccionNombre;
                    DepartamentoLabel.Text = DepartamentoNombre;

                    //Se buscan los activos
                    ActivoEntidadObjeto.FamiliaId = Int16.Parse(FamiliaId);
                    ActivoEntidadObjeto.SubFamiliaId = Int16.Parse(SubFamiliaId);
                    ActivoEntidadObjeto.MarcaId = Int16.Parse(MarcaId);
                    ActivoEntidadObjeto.Modelo = Modelo;

                    if (FechaInicio != "")
                        ActivoEntidadObjeto.StrFechaInicio = FormatoFecha.AsignarFormato(FechaInicio, ConstantePrograma.UniversalFormatoFecha);

                    if (FechaFin != "")
                        ActivoEntidadObjeto.StrFechaFin = FormatoFecha.AsignarFormato(FechaFin, ConstantePrograma.UniversalFormatoFecha);

                    ActivoEntidadObjeto.ProveedorId = Int16.Parse(ProveedorId);
                    ActivoEntidadObjeto.CompraFolio = FolioDocumento;
                    ActivoEntidadObjeto.DireccionId = Int16.Parse(DireccionId);
                    ActivoEntidadObjeto.DepartamentoId = Int16.Parse(DepartamentoId);
                    ActivoEntidadObjeto.TipoActivoId = ObtenerTipoActivoId();
                    ActivoEntidadObjeto.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;

                    SeleccionarActivos(ActivoEntidadObjeto);

                    FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
                    FechaLabel.Text = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);

                }
            } 

            protected Int16 ObtenerTipoActivoId()
            {
                Int16 TipoActivoId = 0;
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                switch (UsuarioSessionEntidad.RolId)
                {
                    case (Int16)ConstantePrograma.RolUsuario.Administrador:
                        TipoActivoId = 0;
                        break;

                    case (Int16)ConstantePrograma.RolUsuario.Almacenista:
                        TipoActivoId = 0;
                        break;

                    case (Int16)ConstantePrograma.RolUsuario.Mantenimientos:
                        TipoActivoId = 0;
                        break;

                    case (Int16)ConstantePrograma.RolUsuario.ActivosMobiliario:
                        TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.Mobiliario;
                        break;

                    case (Int16)ConstantePrograma.RolUsuario.ActivosEquipoComputo:
                        TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.EquipoComputo;
                        break;

                    case (Int16)ConstantePrograma.RolUsuario.ActivosVehiculo:
                        TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.Vehiculo;
                        break;

                    case (Int16)ConstantePrograma.RolUsuario.ActivosOperacionYMantenimiento:
                        TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.OperaciónYMantenimiento;
                        break;
                }

                return TipoActivoId;
            }

            protected void MostrarTitulo()
            {

                Master.MostrarTitulo("GENERAL DE ACTIVOS", "REPORTE");
            }

            protected void SeleccionarActivos(ActivoEntidad ActivoEntidadObjeto)
            {
                MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();
                ResultadoEntidad Resultado = new ResultadoEntidad();

                Resultado = MovimientoProcesoObjeto.SeleccionarMovimientoReporteActivoGeneral(ActivoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaActivos.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaActivos.CssClass = ConstantePrograma.ClaseTablaImpresion;

                    TablaActivos.DataSource = Resultado.ResultadoDatos;
                    TablaActivos.DataBind();
                }


            }

        #endregion
    }
}
