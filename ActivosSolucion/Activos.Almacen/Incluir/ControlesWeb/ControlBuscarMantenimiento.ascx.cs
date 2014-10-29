using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;

using Activos.Comun.Constante;
using Activos.Entidad.Activos;
using Activos.Entidad.Mantenimiento;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Mantenimiento;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Incluir.ControlesWeb
{
    public partial class ControlBuscarMantenimiento : System.Web.UI.UserControl
    {
        #region "Eventos"

            protected void BotonCancelarBusquedaMantenimiento_Click(object sender, EventArgs e)
            {
                OcultarControlPanel();
            }

            protected void BotonBusquedaMantenimiento_Click(object sender, EventArgs e)
            {
                BuscarMantenimiento();
            }

            protected void imgCancelarBuscarMantenimiento_Click(object sender, ImageClickEventArgs e)
            {
                OcultarControlPanel();
            }
           
            protected void RadioArea_CheckedChanged(object sender, EventArgs e)
            {
                RadiosCambioSeleccion();
            }

            protected void RadioEmpleado_CheckedChanged(object sender, EventArgs e)
            {
                RadiosCambioSeleccion();
            }

            protected void TablaMantenimiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaMantenimiento.PageIndex = e.NewPageIndex;
                BuscarMantenimiento();
            }

            protected void TablaMantenimiento_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaMantenimientoEventoComando(e);
            }

            protected void Page_Load(object sender, EventArgs e)
            {

            }

        #endregion

        #region "Métodos"

            public void InicioControl()
            {
                MostrarControlPanel();
                SeleccionarEmpleadoAsignacion();
                LimpiarControl();
                BuscarMantenimiento();
            }

            protected void BuscarMantenimiento()
            {
                MantenimientoEntidad MantenimientoEntidadObjeto = new MantenimientoEntidad();

                if (FolioBusqueda.Text.Trim() != "")
                {
                    if (Regex.Match(FolioBusqueda.Text.Trim(), @"^[0-9]+$").Success == true)
                    {
                        MantenimientoEntidadObjeto.MantenimientoId = int.Parse(FolioBusqueda.Text.Trim());
                    }
                    else
                    {
                        FolioBusqueda.Text = "";
                        EtiquetaControlBuscarMantenimientoMensaje.Text = "Folio inválido.";
                    }
                }

                if (RadioEmpleado.Checked == true)
                {
                    MantenimientoEntidadObjeto.EmpleadoId = Int16.Parse(ComboAsignacion.SelectedValue);
                    MantenimientoEntidadObjeto.DepartamentoId = -1;
                }
                else
                {
                    MantenimientoEntidadObjeto.EmpleadoId = -1;
                    MantenimientoEntidadObjeto.DepartamentoId = Int16.Parse(ComboAsignacion.SelectedValue);
                }

                MantenimientoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusMantenimientos.Cerrado;

                BuscarMantenimiento(MantenimientoEntidadObjeto);
            }

            protected void BuscarMantenimiento(MantenimientoEntidad MantenimientoEntidadObjeto)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MantenimientoProceso MantenimientoProcesoNegocio = new MantenimientoProceso();

                Resultado = MantenimientoProcesoNegocio.SeleccionarMantenimientoAvanzado(MantenimientoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaMantenimiento.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaMantenimiento.CssClass = ConstantePrograma.ClaseTabla;

                    TablaMantenimiento.DataSource = Resultado.ResultadoDatos;
                    TablaMantenimiento.DataBind();
                }
                else
                {
                    EtiquetaControlBuscarMantenimientoMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void LimpiarControl()
            {
                FolioBusqueda.Text = "";
                RadioEmpleado.Checked = true;
                RadioArea.Checked = false;
                ComboAsignacion.SelectedIndex = 0;

            }

            public void MostrarControlPanel()
            {
                pnlFondoBuscarMantenimiento.Visible = true;
                pnlControlBuscarMantenimiento.Visible = true;
            }

            protected void OcultarControlPanel()
            {
                pnlFondoBuscarMantenimiento.Visible = false;
                pnlControlBuscarMantenimiento.Visible = false;
            }

            protected void RadiosCambioSeleccion()
            {
                if (RadioEmpleado.Checked == true)
                {
                    SeleccionarEmpleadoAsignacion();
                }
                else
                {
                    SeleccionarDepartamentoAsignacion();
                }
            }

            protected void SeleccionarDepartamentoAsignacion()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DepartamentoEntidad DepartamentoEntidadObjeto = new DepartamentoEntidad();
                DepartamentoProceso DepartamentoProcesoObjeto = new DepartamentoProceso();

                DepartamentoEntidadObjeto.EstatusId = (int)ConstantePrograma.EstatusDepartamentos.Activo;

                Resultado = DepartamentoProcesoObjeto.SeleccionarDepartamento(DepartamentoEntidadObjeto);

                ComboAsignacion.DataValueField = "DepartamentoId";
                ComboAsignacion.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    ComboAsignacion.DataSource = Resultado.ResultadoDatos;
                    ComboAsignacion.DataBind();
                }
                else
                {
                    EtiquetaControlBuscarMantenimientoMensaje.Text = TextoError.ErrorGenerico;
                }

                ComboAsignacion.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void SeleccionarEmpleadoAsignacion()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

                EmpleadoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusEmpleados.Activo;

                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                ComboAsignacion.DataValueField = "EmpleadoId";
                ComboAsignacion.DataTextField = "NombreEmpleadoCompleto";

                if (Resultado.ErrorId == 0)
                {
                    ComboAsignacion.DataSource = Resultado.ResultadoDatos;
                    ComboAsignacion.DataBind();
                }
                else
                {
                    EtiquetaControlBuscarMantenimientoMensaje.Text = TextoError.ErrorGenerico;
                }

                ComboAsignacion.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void TablaMantenimientoEventoComando(GridViewCommandEventArgs e)
            {
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                int MantenimientoId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaMantenimiento.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaMantenimiento.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        MantenimientoId = Int16.Parse(TablaMantenimiento.DataKeys[intFila]["MantenimientoId"].ToString());

                        this.Page.GetType().InvokeMember("BuscarMantenimientoPorId", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { MantenimientoId });

                        OcultarControlPanel();
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }
            

        #endregion
    }
}