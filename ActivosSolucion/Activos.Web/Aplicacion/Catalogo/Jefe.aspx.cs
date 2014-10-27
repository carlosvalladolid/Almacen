using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Aplicacion.Catalogo
{
    public partial class Jefe : System.Web.UI.Page
    {
        #region "Eventos"

            protected void BotonBusqueda_Click(object sender, EventArgs e)
            {
                TextoBusquedaRapida.Text = "";
                BusquedaAvanzada();
            }

            protected void BotonCancelarBusqueda_Click(object sender, EventArgs e)
            {
                CambiarBusquedaAvanzada();
            }

            protected void BotonCancelarNuevo_Click(object sender, EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void BotonGuardar_Click(object sender, EventArgs e)
            {
                GuardarJefe();
            }

            protected void BusquedaAvanzadaLink_Click(object sender, EventArgs e)
            {
                CambiarBusquedaAvanzada();
            }

            protected void DireccionBusqueda_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarDepartamentoBusqueda();
            }

            protected void DireccionNuevo_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarDepartamentoNuevo();
            }

            protected void EliminarRegistroLink_Click(object sender, EventArgs e)
            {
                EliminarJefe();
            }

            protected void ImagenBuscar_Click(object sender, ImageClickEventArgs e)
            {
                BusquedaAvanzada();
            }

            protected void NuevoRegistroLink_Click(object sender, EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void TablaJefe_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaJefe.PageIndex = e.NewPageIndex;
                BusquedaAvanzada();
            }

            protected void TablaJefe_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaJefeEventoComando(e);
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

        #endregion

        #region "Métodos"

            protected void BusquedaAvanzada()
            {
                JefeEntidad JefeEntidadObjeto = new JefeEntidad();

                JefeEntidadObjeto.DireccionId = Int16.Parse(DireccionBusqueda.SelectedValue);
                JefeEntidadObjeto.DepartamentoId = Int16.Parse(DepartamentoBusqueda.SelectedValue);
                JefeEntidadObjeto.PuestoId = Int16.Parse(PuestoBusqueda.SelectedValue);

                JefeEntidadObjeto.TextoBusqueda = TextoBusquedaRapida.Text.Trim();

                SeleccionarJefe(JefeEntidadObjeto);
            }

            protected void CambiarBusquedaAvanzada()
            {
                PanelBusquedaAvanzada.Visible = !PanelBusquedaAvanzada.Visible;
                PanelNuevoRegistro.Visible = false;
            }

            protected void CambiarNuevoRegistro()
            {
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = !PanelNuevoRegistro.Visible;
                LimpiarNuevoRegistro();
                DireccionNuevo.Enabled = true;
                DepartamentoNuevo.Enabled = true;
                PuestoNuevo.Enabled = true;
                EmpleadoNuevo.Enabled = true;
            }

            protected void CambiarEditarRegistro()
            {
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = true;
                DireccionNuevo.Enabled = false;
                DepartamentoNuevo.Enabled = false;
                PuestoNuevo.Enabled = false;
                EmpleadoNuevo.Enabled = false;
            }

            protected void EliminarJefe()
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                JefeEntidad JefeEntidadObjeto = new JefeEntidad();

                JefeEntidadObjeto.CadenaJefeXML = ObtenerCadenaJefeXML();

                EliminarJefe(JefeEntidadObjeto);
            }

            protected void EliminarJefe(JefeEntidad JefeObjetoEntidad)
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                JefeProceso JefeProcesoObjeto = new JefeProceso();

                ResultadoEntidadObjeto = JefeProcesoObjeto.EliminarJefe(JefeObjetoEntidad);

                if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Jefe.EliminacionExitosa)
                {
                    EtiquetaMensaje.Text = "";

                    BusquedaAvanzada();
                }
                else
                {
                    EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
                }
            }

            protected string ObtenerCadenaJefeXML()
            {
                StringBuilder CadenaJefeXML = new StringBuilder();
                CheckBox CasillaEliminar;

                CadenaJefeXML.Append("<row>");

                foreach (GridViewRow Registro in TablaJefe.Rows)
                {
                    CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                    if (CasillaEliminar.Checked)
                    {
                        CadenaJefeXML.Append("<Jefe DireccionId='");
                        CadenaJefeXML.Append(TablaJefe.DataKeys[Registro.RowIndex]["DireccionId"].ToString());
                        CadenaJefeXML.Append("' DepartamentoId='");
                        CadenaJefeXML.Append(TablaJefe.DataKeys[Registro.RowIndex]["DepartamentoId"].ToString());
                        CadenaJefeXML.Append("' PuestoId='");
                        CadenaJefeXML.Append(TablaJefe.DataKeys[Registro.RowIndex]["PuestoId"].ToString());
                        CadenaJefeXML.Append("' EmpleadoId='");
                        CadenaJefeXML.Append(TablaJefe.DataKeys[Registro.RowIndex]["EmpleadoId"].ToString());
                        CadenaJefeXML.Append("'/>");
                    }
                }

                CadenaJefeXML.Append("</row>");

                return CadenaJefeXML.ToString();
            }

            protected void GuardarJefe()
            {
                JefeEntidad JefeEntidadObjeto = new JefeEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                JefeEntidadObjeto.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
                JefeEntidadObjeto.UsuarioIdModifico = UsuarioSessionEntidad.UsuarioId;
                JefeEntidadObjeto.DireccionId = Int16.Parse(DireccionNuevo.SelectedValue);
                JefeEntidadObjeto.DepartamentoId = Int16.Parse(DepartamentoNuevo.SelectedValue);
                JefeEntidadObjeto.PuestoId = Int16.Parse(PuestoNuevo.SelectedValue);
                JefeEntidadObjeto.EmpleadoId = Int16.Parse(EmpleadoNuevo.SelectedValue);
                JefeEntidadObjeto.EstatusId = Int16.Parse(EstatusNuevo.SelectedValue);

                GuardarJefe(JefeEntidadObjeto);
            }

            protected void GuardarJefe(JefeEntidad JefeObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                JefeProceso JefeProcesoNegocio = new JefeProceso();

                Resultado = JefeProcesoNegocio.GuardarJefe(JefeObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Jefe.JefeGuardadoCorrectamente)
                {
                    LimpiarNuevoRegistro();
                    PanelNuevoRegistro.Visible = false;
                    PanelBusquedaAvanzada.Visible = false;
                    BusquedaAvanzada();
                }
                else
                {
                    EtiquetaMensaje.Text = Resultado.DescripcionError;
                }
            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.Jefes);

                    SeleccionarDireccion();
                    SeleccionarDepartamentoBusqueda();
                    SeleccionarDepartamentoNuevo();
                    SeleccionarPuesto();
                    SeleccionarEmpleado();
                    SeleccionarEstatusNuevo();
                    BusquedaAvanzada();
                    SeleccionarTextoError();

                    EliminarRegistroLink.Attributes.Add("onclick", "return ValidarCasillas()");
                }
            }

            protected void LimpiarNuevoRegistro()
            {
                EmpleadoNuevo.SelectedValue = "0";
                EstatusNuevo.SelectedValue = "0";
                DireccionNuevo.SelectedValue = "0";
                SeleccionarDepartamentoNuevo();
                PuestoNuevo.SelectedValue = "0";
                EtiquetaMensaje.Text = "";
            }

            protected void SeleccionarDepartamentoBusqueda()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DepartamentoEntidad DepartamentoEntidadObjeto = new DepartamentoEntidad();
                DepartamentoProceso DepartamentoProcesoObjeto = new DepartamentoProceso();

                DepartamentoEntidadObjeto.DireccionId = Int16.Parse(DireccionBusqueda.SelectedValue);
                //DepartamentoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusDepartamentos.Activo;

                if (DepartamentoEntidadObjeto.DireccionId == 0)
                {
                    DepartamentoBusqueda.Items.Clear();
                }
                else
                {
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
                        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    }
                }

                DepartamentoBusqueda.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void SeleccionarDepartamentoNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DepartamentoEntidad DepartamentoEntidadObjeto = new DepartamentoEntidad();
                DepartamentoProceso DepartamentoProcesoObjeto = new DepartamentoProceso();

                DepartamentoEntidadObjeto.DireccionId = Int16.Parse(DireccionNuevo.SelectedValue);
                //DepartamentoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusDepartamentos.Activo;

                if (DepartamentoEntidadObjeto.DireccionId == 0)
                {
                    DepartamentoNuevo.Items.Clear();
                }
                else
                {
                    Resultado = DepartamentoProcesoObjeto.SeleccionarDepartamento(DepartamentoEntidadObjeto);

                    DepartamentoNuevo.DataValueField = "DepartamentoId";
                    DepartamentoNuevo.DataTextField = "Nombre";

                    if (Resultado.ErrorId == 0)
                    {
                        DepartamentoNuevo.DataSource = Resultado.ResultadoDatos;
                        DepartamentoNuevo.DataBind();
                    }
                    else
                    {
                        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    }
                }

                DepartamentoNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarDireccion()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DireccionEntidad DireccionEntidadObjeto = new DireccionEntidad();
                DireccionProceso DireccionProcesoObjeto = new DireccionProceso();

                //DireccionEntidadObjeto.EstatusId = (int)ConstantePrograma.EstatusDireccion.Activo;

                Resultado = DireccionProcesoObjeto.SeleccionarDireccion(DireccionEntidadObjeto);

                DireccionNuevo.DataValueField = "DireccionId";
                DireccionNuevo.DataTextField = "Nombre";

                DireccionBusqueda.DataValueField = "DireccionId";
                DireccionBusqueda.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    DireccionNuevo.DataSource = Resultado.ResultadoDatos;
                    DireccionNuevo.DataBind();

                    DireccionBusqueda.DataSource = Resultado.ResultadoDatos;
                    DireccionBusqueda.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                DireccionNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
                DireccionBusqueda.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void SeleccionarEmpleado()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

                //EmpleadoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusEmpleados.Activo;

                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                EmpleadoNuevo.DataValueField = "EmpleadoId";
                EmpleadoNuevo.DataTextField = "NombreEmpleadoCompleto";

                if (Resultado.ErrorId == 0)
                {
                    EmpleadoNuevo.DataSource = Resultado.ResultadoDatos;
                    EmpleadoNuevo.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                EmpleadoNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarEstatusNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
                EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

                EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Jefes;

                Resultado = EstatusProcesoObjeto.SeleccionarEstatus(EstatusEntidadObjeto);

                EstatusNuevo.DataValueField = "EstatusId";
                EstatusNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    EstatusNuevo.DataSource = Resultado.ResultadoDatos;
                    EstatusNuevo.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                EstatusNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarJefe(JefeEntidad JefeObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                JefeProceso JefeProcesoNegocio = new JefeProceso();

                Resultado = JefeProcesoNegocio.SeleccionarJefe(JefeObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaJefe.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaJefe.CssClass = ConstantePrograma.ClaseTabla;

                    TablaJefe.DataSource = Resultado.ResultadoDatos;
                    TablaJefe.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarJefeParaEditar(JefeEntidad JefeObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                JefeProceso JefeProcesoNegocio = new JefeProceso();

                Resultado = JefeProcesoNegocio.SeleccionarJefe(JefeObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    DireccionNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["DireccionId"].ToString();
                    SeleccionarDepartamentoNuevo();
                    DepartamentoNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["DepartamentoId"].ToString();
                    PuestoNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["PuestoId"].ToString();
                    EmpleadoNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                    EstatusNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString();
                    CambiarEditarRegistro();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarPuesto()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                PuestoEntidad PuestoEntidadObjeto = new PuestoEntidad();
                PuestoProceso PuestoProcesoObjeto = new PuestoProceso();

                //PuestoEntidadObjeto.EstatusId = (int)ConstantePrograma.EstatusPuestos.Activo;

                Resultado = PuestoProcesoObjeto.SeleccionarPuesto(PuestoEntidadObjeto);

                PuestoNuevo.DataValueField = "PuestoId";
                PuestoNuevo.DataTextField = "Nombre";

                PuestoBusqueda.DataValueField = "PuestoId";
                PuestoBusqueda.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    PuestoNuevo.DataSource = Resultado.ResultadoDatos;
                    PuestoNuevo.DataBind();

                    PuestoBusqueda.DataSource = Resultado.ResultadoDatos;
                    PuestoBusqueda.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                PuestoNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
                PuestoBusqueda.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void SeleccionarTextoError()
            {
                DireccionRequerido.ErrorMessage = TextoError.JefeDireccion + "<br />";
                PuestoRequerido.ErrorMessage = TextoError.JefePuesto + "<br />";
                EmpleadoRequerido.ErrorMessage = TextoError.JefeEmpleado + "<br />";
                EstatusRequerido.ErrorMessage = TextoError.JefeEstatus + "<br />";
            }

            protected void TablaJefeEventoComando(GridViewCommandEventArgs e)
            {
                JefeEntidad JefeEntidadObjeto = new JefeEntidad();
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 DireccionId = 0;
                Int16 DepartamentoId = 0;
                Int16 PuestoId = 0;
                Int16 EmpleadoId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaJefe.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaJefe.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        DireccionId = Int16.Parse(TablaJefe.DataKeys[intFila]["DireccionId"].ToString());
                        DepartamentoId = Int16.Parse(TablaJefe.DataKeys[intFila]["DepartamentoId"].ToString());
                        PuestoId = Int16.Parse(TablaJefe.DataKeys[intFila]["PuestoId"].ToString());
                        EmpleadoId = Int16.Parse(TablaJefe.DataKeys[intFila]["EmpleadoId"].ToString());
                        JefeEntidadObjeto.DireccionId = DireccionId;
                        JefeEntidadObjeto.DepartamentoId = DepartamentoId;
                        JefeEntidadObjeto.PuestoId = PuestoId;
                        JefeEntidadObjeto.EmpleadoId = EmpleadoId;
                        SeleccionarJefeParaEditar(JefeEntidadObjeto);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

        #endregion
    }
}
