using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

namespace Activos.Web.Aplicacion.Reportes
{
    public partial class ReporteActivosPorEmpleado : System.Web.UI.Page
    {
        #region "Eventos"

            protected void BotonImprimir_Click(object sender, EventArgs e)
            {
                ImprimirReporte();
            }

            protected void BotonLimpiar_Click(object sender, EventArgs e)
            {
                LimpiarFormulario();
            }

            protected void BotonBuscarEmpleado_Click(object sender, EventArgs e)
            {
                ControlBuscarEmpleado.InicioControl((Int16)ConstantePrograma.TipoBusquedaEmpleado.Empleado);
            }

            protected void NumeroEmpleado_TextChanged(object sender, EventArgs e)
            {
                NumeroEmpleadoTextoModificado();
            }

            protected void LinkBuscarEmpleado_Click(object sender, EventArgs e)
            {
                BuscarEmpleadoPorNumero();
            }

            protected void TablaActivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaActivos.PageIndex = e.NewPageIndex;
                BuscarEmpleadoPorNumero();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

        #endregion

        #region "Métodos"

            public void BuscarEmpleadoPorId(Int16 EmpleadoId)
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoId;

                BuscarEmpleado(EmpleadoEntidadObjeto);

                ActualizarTablaReporte.Update();
            }

            protected void BuscarEmpleadoPorNumero()
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.NumeroEmpleado = NumeroEmpleado.Text.Trim();

                BuscarEmpleado(EmpleadoEntidadObjeto);
            }

            protected void BuscarEmpleado(EmpleadoEntidad EmpleadoEntidadObjeto)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        NumeroEmpleado.Enabled = false;
                        BotonImprimir.Enabled = true;
                        EmpleadoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                        ReporteRFC.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["RFC"].ToString();
                        NumeroEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                        ReporteNombreEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                        ReporteDireccion.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreDireccion"].ToString();
                        ReporteDepartamento.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreDepartamento"].ToString();
                        ReportePuesto.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombrePuesto"].ToString();
                        EtiquetaMensaje.Text = "";
                        SeleccionarAsignacion(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString()));
                    }
                    else
                    {
                        LimpiarFormulario();
                        BotonImprimir.Enabled = false;
                        EtiquetaMensaje.Text = TextoError.EmpleadoNoEncontrado;
                    }
                }
                else
                {
                    LimpiarFormulario();
                    BotonImprimir.Enabled = false;
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Se validan los permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.ReporteActivosPorEmpleado);

                    TablaActivos.DataSource = null;
                    TablaActivos.DataBind();
                }
            }

            protected void ImprimirReporte()
            {
                //Se pasan los valores a los campos ocultos
                NumeroEmpleadoHidden.Value = NumeroEmpleado.Text;
                RFCHidden.Value = ReporteRFC.Text;
                NombreEmpleadoHidden.Value = ReporteNombreEmpleado.Text;
                DireccionHidden.Value = ReporteDireccion.Text;
                DepartamentoHidden.Value = ReporteDepartamento.Text;
                PuestoHidden.Value = ReportePuesto.Text;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "Imprimir()", true);
            }

            protected void NumeroEmpleadoTextoModificado()
            {
                if (NumeroEmpleado.Text.Trim() != "")
                {
                    BuscarEmpleadoPorNumero();
                }
                else
                {
                    EmpleadoIdHidden.Value = "0";
                    ReporteNombreEmpleado.Text = "";
                }
            }

            protected void LimpiarFormulario()
            {
                NumeroEmpleado.Text = "";
                ReporteRFC.Text = "";
                ReporteNombreEmpleado.Text = "";
                ReporteDireccion.Text = "";
                ReporteDepartamento.Text = "";
                ReportePuesto.Text = "";

                NumeroEmpleado.Enabled = true;
                BotonImprimir.Enabled = false;

                TablaActivos.DataSource = null;
                TablaActivos.DataBind();

                EmpleadoIdHidden.Value = "0";
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
                        TablaActivos.CssClass = ConstantePrograma.ClaseTabla;

                    TablaActivos.DataSource = Resultado.ResultadoDatos;
                    TablaActivos.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
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

        #endregion
    }
}
