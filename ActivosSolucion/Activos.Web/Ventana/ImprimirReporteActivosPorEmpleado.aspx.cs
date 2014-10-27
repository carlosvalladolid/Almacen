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
    public partial class ImprimirReporteActivosPorEmpleado : System.Web.UI.Page
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
                    string EmpleadoId = string.Empty;
                    string RFC = string.Empty;
                    string NumeroEmpleado = string.Empty;
                    string NombreEmpleado = string.Empty;
                    string Direccion = string.Empty;
                    string Departamento = string.Empty;
                    string Puesto = string.Empty;

                    //Se obtienen los datos de los filtros de la pantalla padre

                    EmpleadoId = Request.Form["ctl00$ContenedorCuerpo$EmpleadoIdHidden"].ToString();
                    RFC = Request.Form["ctl00$ContenedorCuerpo$RFCHidden"].ToString();
                    NumeroEmpleado = Request.Form["ctl00$ContenedorCuerpo$NumeroEmpleadoHidden"].ToString();
                    NombreEmpleado = Request.Form["ctl00$ContenedorCuerpo$NombreEmpleadoHidden"].ToString();
                    Direccion = Request.Form["ctl00$ContenedorCuerpo$DireccionHidden"].ToString();
                    Departamento = Request.Form["ctl00$ContenedorCuerpo$DepartamentoHidden"].ToString();
                    Puesto = Request.Form["ctl00$ContenedorCuerpo$PuestoHidden"].ToString();

                    //Se llenan los datos del encabezado con los filtros
                    RFCLabel.Text = RFC;
                    NumeroEmpleadoLabel.Text = NumeroEmpleado;
                    NombreEmpleadoLabel.Text = NombreEmpleado;
                    DireccionLabel.Text = Direccion;
                    DepartamentoLabel.Text = Departamento;
                    PuestoLabel.Text = Puesto;

                    //Se llena el grid
                    SeleccionarAsignacion(Int16.Parse(EmpleadoId));

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

            protected void SeleccionarAsignacion(Int16 EmpleadoId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();

                ActivoObjetoEntidad.EmpleadoId = EmpleadoId;
                ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;
                ActivoObjetoEntidad.TipoAccesorioId = (Int16)ConstantePrograma.TipoAccesorio.ActivoFijo;
                ActivoObjetoEntidad.TipoActivoId = ObtenerTipoActivoId();

                Resultado = MovimientoProcesoObjeto.SeleccionarMovimientoReporteActivoPorEmpleado(ActivoObjetoEntidad);

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

            protected void MostrarTitulo()
            {

                Master.MostrarTitulo("ACTIVOS POR EMPLEADO", "REPORTE");
            }

        #endregion
    }
}
