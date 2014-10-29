using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Incluir.ControlesWeb
{
    public partial class ControlBuscarEmpleado : System.Web.UI.UserControl
    {
        #region "Eventos"

            protected void BotonCancelarBusquedaEmpleado_Click(object sender, EventArgs e)
            {
                OcultarControlPanel();
            }

            protected void BotonBusquedaEmpleado_Click(object sender, EventArgs e)
            {
                BuscarEmpleado();
            }

            protected void imgCancelarBuscarEmpleado_Click(object sender, ImageClickEventArgs e)
            {
                OcultarControlPanel();
            }

            protected void TablaEmpleado_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaEmpleadoEventoComando(e);
            }

            protected void TablaEmpleado_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaEmpleado.PageIndex = e.NewPageIndex;
                BuscarEmpleado();
            }

            protected void Page_Load(object sender, EventArgs e)
            {

            }

        #endregion

        #region "Métodos"

            public void InicioControl(Int16 TipoBusqueda)
            {
                TipoBusquedaHidden.Value = TipoBusqueda.ToString();
                SeleccionarDepartamento();
                SeleccionarEdificio();
                SeleccionarPuesto();
                MostrarControlPanel();
                LimpiarControl();
                BuscarEmpleado();
            }

            protected void LimpiarControl()
            {
                NombreBusqueda.Text = "";
                CorreoElectronicoBusqueda.Text = "";
                DepartamentoBusqueda.SelectedIndex = 0;
                EdificioBusqueda.SelectedIndex = 0;
                PuestoBusqueda.SelectedIndex = 0;
                EtiquetaControlBuscarEmpleadoMensaje.Text = "";
            }

            public void MostrarControlPanel()
            {
                pnlFondoBuscarEmpleado.Visible = true;
                pnlControlBuscarEmpleado.Visible = true;
            }

            protected void OcultarControlPanel()
            {
                pnlFondoBuscarEmpleado.Visible = false;
                pnlControlBuscarEmpleado.Visible = false;
            }

            protected void BuscarEmpleado()
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.DepartamentoId = Int16.Parse(DepartamentoBusqueda.SelectedValue);
                EmpleadoEntidadObjeto.EdificioId = Int16.Parse(EdificioBusqueda.SelectedValue);
                EmpleadoEntidadObjeto.PuestoId = Int16.Parse(PuestoBusqueda.SelectedValue);
                EmpleadoEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
                EmpleadoEntidadObjeto.TrabajoEmail = CorreoElectronicoBusqueda.Text.Trim();

                BuscarEmpleado(EmpleadoEntidadObjeto);
            }

            protected void BuscarEmpleado(EmpleadoEntidad EmpleadoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

                Resultado = EmpleadoProcesoNegocio.SeleccionarEmpleado(EmpleadoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaEmpleado.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaEmpleado.CssClass = ConstantePrograma.ClaseTabla;

                    TablaEmpleado.DataSource = Resultado.ResultadoDatos;
                    TablaEmpleado.DataBind();
                }
                else
                {
                    EtiquetaControlBuscarEmpleadoMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void TablaEmpleadoEventoComando(GridViewCommandEventArgs e)
            {
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 EmpleadoId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaEmpleado.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaEmpleado.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        EmpleadoId = Int16.Parse(TablaEmpleado.DataKeys[intFila]["EmpleadoId"].ToString());

                        switch (Int16.Parse(TipoBusquedaHidden.Value))
                        {
                            case (Int16)ConstantePrograma.TipoBusquedaEmpleado.Empleado:
                                this.Page.GetType().InvokeMember("BuscarEmpleadoPorId", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { EmpleadoId });
                                break;

                            case (Int16)ConstantePrograma.TipoBusquedaEmpleado.Solicitante:
                                this.Page.GetType().InvokeMember("BuscarSolicitantePorId", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { EmpleadoId });
                                break;

                            default:
                                // Do nothing
                                break;
                        }

                        OcultarControlPanel();
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

            protected void SeleccionarDepartamento()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DepartamentoEntidad DepartamentoEntidadObjeto = new DepartamentoEntidad();
                DepartamentoProceso DepartamentoProcesoObjeto = new DepartamentoProceso();

                //DepartamentoEntidadObjeto.EstatusId = (int)ConstantePrograma.EstatusDepartamentos.Activo;

                Resultado = DepartamentoProcesoObjeto.SeleccionarDepartamento(DepartamentoEntidadObjeto);

                DepartamentoBusqueda.DataValueField = "DepartamentoId";
                DepartamentoBusqueda.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {

                    DepartamentoBusqueda.DataSource = Resultado.ResultadoDatos;
                    DepartamentoBusqueda.DataBind();
                }
                else
                {
                    EtiquetaControlBuscarEmpleadoMensaje.Text = TextoError.ErrorGenerico;
                }

                DepartamentoBusqueda.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void SeleccionarEdificio()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EdificioEntidad EdificioEntidadObjeto = new EdificioEntidad();
                EdificioProceso EdificioProcesoObjeto = new EdificioProceso();

                //EdificioEntidadObjeto.EstatusId = (int)ConstantePrograma.EstatusEdificio.Activo;

                Resultado = EdificioProcesoObjeto.SeleccionarEdificio(EdificioEntidadObjeto);

                EdificioBusqueda.DataValueField = "EdificioId";
                EdificioBusqueda.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {

                    EdificioBusqueda.DataSource = Resultado.ResultadoDatos;
                    EdificioBusqueda.DataBind();
                }
                else
                {
                    EtiquetaControlBuscarEmpleadoMensaje.Text = TextoError.ErrorGenerico;
                }

                EdificioBusqueda.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void SeleccionarPuesto()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                PuestoEntidad PuestoEntidadObjeto = new PuestoEntidad();
                PuestoProceso PuestoProcesoObjeto = new PuestoProceso();

                //PuestoEntidadObjeto.EstatusId = (int)ConstantePrograma.EstatusPuestos.Activo;

                Resultado = PuestoProcesoObjeto.SeleccionarPuesto(PuestoEntidadObjeto);

                PuestoBusqueda.DataValueField = "PuestoId";
                PuestoBusqueda.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {

                    PuestoBusqueda.DataSource = Resultado.ResultadoDatos;
                    PuestoBusqueda.DataBind();
                }
                else
                {
                    EtiquetaControlBuscarEmpleadoMensaje.Text = TextoError.ErrorGenerico;
                }

                PuestoBusqueda.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }
            
        #endregion
    }
}