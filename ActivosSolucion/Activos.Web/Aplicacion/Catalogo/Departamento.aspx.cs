using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Aplicacion.Catalogo
{
    public partial class Departamento : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonBusqueda_Click(object sender, EventArgs e)
            {
                BusquedaAvanzada();
            }

            protected void BusquedaAvanzadaLink_Click(object sender, EventArgs e)
            {
                CambiarBusquedaAvanzada();
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
                GuardarDepartamento();
            }

            protected void EliminarRegistroLink_Click(object sender, EventArgs e)
            {
                EliminarDepartamento();
            }

            protected void ImagenBuscar_Click(object sender, ImageClickEventArgs e)
            {
                BusquedaRapida();
            }

            protected void NuevoRegistroLink_Click(object sender, EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void TablaDepartamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaDepartamento.PageIndex = e.NewPageIndex;
                BusquedaAvanzada();
            }

            protected void TablaDepartamento_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaDepartamentoEventoComando(e);
            }
            
            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }
        #endregion

        #region "Métodos"

            protected void BusquedaAvanzada()
            {
                DepartamentoEntidad DepartamentoEntidadObjeto = new DepartamentoEntidad();

                DepartamentoEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
                DepartamentoEntidadObjeto.DireccionId = Int16.Parse(DireccionBusqueda.SelectedValue);
                DepartamentoEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

                SeleccionarDepartamento(DepartamentoEntidadObjeto);
            }

            protected void CambiarBusquedaAvanzada()
            {
                PanelBusquedaAvanzada.Visible = !PanelBusquedaAvanzada.Visible;
                PanelNuevoRegistro.Visible = false;
            }

            protected void BusquedaRapida()
            {
                DepartamentoEntidad DepartamentoEntidadObjeto = new DepartamentoEntidad();

                DepartamentoEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

                SeleccionarDepartamento(DepartamentoEntidadObjeto);
            }

            protected void CambiarNuevoRegistro()
            {
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = !PanelNuevoRegistro.Visible;
                LimpiarNuevoRegistro();
            }

            protected void CambiarEditarRegistro()
            {
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = true;
            }

            protected void EliminarDepartamento()
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                DepartamentoEntidad DepartamentoEntidadObjeto = new DepartamentoEntidad();

                DepartamentoEntidadObjeto.CadenaDepartamentoId = ObtenerCadenaDepartamentoId();

                EliminarDepartamento(DepartamentoEntidadObjeto);
            }

            protected void EliminarDepartamento(DepartamentoEntidad DepartamentoObjetoEntidad)
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                DepartamentoProceso DepartamentoProcesoObjeto = new DepartamentoProceso();

                ResultadoEntidadObjeto = DepartamentoProcesoObjeto.EliminarDepartamento(DepartamentoObjetoEntidad);

                if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Departamento.EliminacionExitosa)
                {
                    EtiquetaMensaje.Text = "";

                    BusquedaAvanzada();
                }
                else
                {
                    EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
                }
            }

            protected void GuardarDepartamento()
            {
                DepartamentoEntidad DepartamentoObjetoEntidad = new DepartamentoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                DepartamentoObjetoEntidad.DepartamentoId = Int16.Parse(DepartamentoIdHidden.Value);
                DepartamentoObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
                DepartamentoObjetoEntidad.UsuarioIdModifico = UsuarioSessionEntidad.UsuarioId;
                DepartamentoObjetoEntidad.DireccionId = Int16.Parse(DireccionNuevo.SelectedValue);
                DepartamentoObjetoEntidad.Nombre = NombreNuevo.Text.Trim();
                DepartamentoObjetoEntidad.EstatusId = Int16.Parse(EstatusNuevo.SelectedValue);
                GuardarDepartamento(DepartamentoObjetoEntidad);
            }

            protected void GuardarDepartamento(DepartamentoEntidad DepartamentoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DepartamentoProceso DepartamentoProcesoNegocio = new DepartamentoProceso();

                Resultado = DepartamentoProcesoNegocio.GuardarDepartamento(DepartamentoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Departamento.DepartamentoGuardadoCorrectamente)
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
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.Departamentos);

                    SeleccionarEstatusNuevo();
                    SeleccionarDireccion();
                    BusquedaAvanzada();
                    SeleccionarTextoError();

                    EliminarRegistroLink.Attributes.Add("onclick", "return ValidarCasillas()");
                }
            }

            protected void LimpiarNuevoRegistro()
            {
                NombreNuevo.Text = "";
                EstatusNuevo.SelectedValue = "0";
                DireccionNuevo.SelectedValue = "0";
                EtiquetaMensaje.Text = "";
                DepartamentoIdHidden.Value = "0";
            }

            protected string ObtenerCadenaDepartamentoId()
            {
                StringBuilder CadenaDepartamentoId = new StringBuilder();
                CheckBox CasillaEliminar;

                CadenaDepartamentoId.Append(",");

                foreach (GridViewRow Registro in TablaDepartamento.Rows)
                {
                    CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                    if (CasillaEliminar.Checked)
                    {
                        CadenaDepartamentoId.Append(TablaDepartamento.DataKeys[Registro.RowIndex]["DepartamentoId"].ToString());
                        CadenaDepartamentoId.Append(",");
                    }
                }

                return CadenaDepartamentoId.ToString();
            }

            protected void SeleccionarDepartamento(DepartamentoEntidad DepartamentoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DepartamentoProceso DepartamentoProcesoNegocio = new DepartamentoProceso();

                Resultado = DepartamentoProcesoNegocio.SeleccionarDepartamento(DepartamentoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaDepartamento.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaDepartamento.CssClass = ConstantePrograma.ClaseTabla;

                    TablaDepartamento.DataSource = Resultado.ResultadoDatos;
                    TablaDepartamento.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarDepartamentoParaEditar(DepartamentoEntidad DepartamentoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DepartamentoProceso DepartamentoProcesoNegocio = new DepartamentoProceso();

                Resultado = DepartamentoProcesoNegocio.SeleccionarDepartamento(DepartamentoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    NombreNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
                    EstatusNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString();
                    DireccionNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["DireccionId"].ToString();
                    CambiarEditarRegistro();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
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

            protected void SeleccionarEstatusNuevo()
            {
                 ResultadoEntidad Resultado = new ResultadoEntidad();
                 EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
                 EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

                 EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Departamentos;

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

            protected void SeleccionarTextoError()
            {
                NombreRequerido.ErrorMessage = TextoError.DepartamentoNombre + "<br />";
                EstatusRequerido.ErrorMessage = TextoError.DepartamentoEstatus + "<br />";
                DireccionRequerido.ErrorMessage = TextoError.DepartamentoDireccion + "<br />";
            }

            protected void TablaDepartamentoEventoComando(GridViewCommandEventArgs e)
            {
                DepartamentoEntidad DepartamentoEntidadObjeto = new DepartamentoEntidad();
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 DepartamentoId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaDepartamento.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaDepartamento.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        DepartamentoId = Int16.Parse(TablaDepartamento.DataKeys[intFila]["DepartamentoId"].ToString());
                        DepartamentoIdHidden.Value = DepartamentoId.ToString();
                        DepartamentoEntidadObjeto.DepartamentoId = DepartamentoId;
                        SeleccionarDepartamentoParaEditar(DepartamentoEntidadObjeto);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }
        #endregion
    }
}
