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
using Activos.ProcesoNegocio.Seguridad;

namespace Activos.Web.Aplicacion.Catalogo
{
    public partial class Usuario : System.Web.UI.Page
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
                GuardarUsuario();
            }

            protected void EliminarRegistroLink_Click(object sender, EventArgs e)
            {
                EliminarUsuario();
            }

            protected void ImagenBuscar_Click(object sender, ImageClickEventArgs e)
            {
                BusquedaRapida();
            }

            protected void NuevoRegistroLink_Click(object sender, EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

            protected void TablaUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                SeleccionarUsuarioPorId(e);
            }
        #endregion

        #region "Métodos"
            protected void BusquedaAvanzada()
            {
                UsuarioEntidad UsuarioEntidadObjeto = new UsuarioEntidad();

                UsuarioEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
                UsuarioEntidadObjeto.CuentaUsuario = CuentaUsuarioBusqueda.Text.Trim();
                UsuarioEntidadObjeto.RolId = Int16.Parse(RolBusqueda.SelectedValue);
                UsuarioEntidadObjeto.EstatusId = Int16.Parse(EstatusBusqueda.SelectedValue);

                SeleccionarUsuario(UsuarioEntidadObjeto);
            }

            protected void BusquedaRapida()
            {
                UsuarioEntidad UsuarioEntidadObjeto = new UsuarioEntidad();

                UsuarioEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

                SeleccionarUsuario(UsuarioEntidadObjeto);
            }

            protected void CambiarBusquedaAvanzada()
            {
                PanelBusquedaAvanzada.Visible = !PanelBusquedaAvanzada.Visible;
                PanelNuevoRegistro.Visible = false;
            }

            protected void CambiarNuevoRegistro()
            {
                LimiparCampos();

                PanelNuevoRegistro.CssClass = ConstantePrograma.ClaseNuevoRegistro;
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = !PanelNuevoRegistro.Visible;
            }

            protected void EliminarUsuario()
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                UsuarioEntidad UsuarioEntidadObjeto = new UsuarioEntidad();
                UsuarioEntidad UsuarioSesion = new UsuarioEntidad();

                UsuarioSesion = (UsuarioEntidad)Session["UsuarioEntidad"];

                UsuarioEntidadObjeto.UsuarioId = UsuarioSesion.UsuarioId;
                UsuarioEntidadObjeto.CadenaUsuarioId = ObtenerCadenaUsuarioId();

                EliminarUsuario(UsuarioEntidadObjeto);
            }

            protected void EliminarUsuario(UsuarioEntidad UsuarioEntidadObjeto)
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                UsuarioProceso UsuarioProcesoObjeto = new UsuarioProceso();

                ResultadoEntidadObjeto = UsuarioProcesoObjeto.EliminarUsuario(UsuarioEntidadObjeto);

                if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Usuario.EliminacionExitosa)
                {
                    EtiquetaMensaje.Text = "";

                    SeleccionarUsuario();
                }
                else
                {
                    EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
                }
            }

            protected void GuardarUsuario()
            {
                UsuarioEntidad UsuarioObjetoEntidad = new UsuarioEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                UsuarioObjetoEntidad.UsuarioId = Int16.Parse(UsuarioIdHidden.Value);
                UsuarioObjetoEntidad.AplicacionId = (int)ConstantePrograma.AplicacionId.Activos;
                UsuarioObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
                UsuarioObjetoEntidad.UsuarioIdModifico = UsuarioSessionEntidad.UsuarioId;
                UsuarioObjetoEntidad.Nombre = NombreNuevo.Text.Trim();
                UsuarioObjetoEntidad.ApellidoPaterno = ApellidoPaternoNuevo.Text.Trim();
                UsuarioObjetoEntidad.ApellidoMaterno = ApellidoMaternoNuevo.Text.Trim();
                UsuarioObjetoEntidad.CuentaUsuario = CuentaUsuarioNuevo.Text.Trim();
                UsuarioObjetoEntidad.RolId = Int16.Parse(RolNuevo.SelectedValue);
                UsuarioObjetoEntidad.EstatusId = Int16.Parse(EstatusNuevo.SelectedValue);

                GuardarUsuario(UsuarioObjetoEntidad);
            }

            protected void GuardarUsuario(UsuarioEntidad UsuarioObjetoEntidad)
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                UsuarioProceso UsuarioProcesoObjeto = new UsuarioProceso();

                ResultadoEntidadObjeto = UsuarioProcesoObjeto.GuardarUsuario(UsuarioObjetoEntidad);

                if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Usuario.GuardadoExitoso)
                {
                    EtiquetaMensaje.Text = "";

                    LimiparCampos();
                    SeleccionarUsuario();

                    PanelNuevoRegistro.Visible = false;
                }
                else
                {
                    EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
                }
            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.Usuarios);

                    SeleccionarRolBusqueda();
                    SeleccionarRolNuevo();
                    SeleccionarEstatusBusqueda();
                    SeleccionarEstatusNuevo();
                    SeleccionarUsuario();
                    SeleccionarTextoError();

                    EliminarRegistroLink.Attributes.Add("onclick", "return ValidarCasillas()");
                }
            }

            protected void LimiparCampos()
            {
                NombreNuevo.Text = "";
                ApellidoPaternoNuevo.Text = "";
                ApellidoMaternoNuevo.Text = "";
                CuentaUsuarioNuevo.Text = "";
                RolNuevo.SelectedIndex = 0;
                EstatusNuevo.SelectedIndex = 0;
                UsuarioIdHidden.Value = "0";
            }

            protected string ObtenerCadenaUsuarioId()
            {
                StringBuilder CadenaUsuarioId = new StringBuilder();
                CheckBox CasillaEliminar;

                CadenaUsuarioId.Append(",");

                foreach (GridViewRow Registro in TablaUsuario.Rows)
                {
                    CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                    if (CasillaEliminar.Checked)
                    {
                        CadenaUsuarioId.Append(TablaUsuario.DataKeys[Registro.RowIndex]["UsuarioId"].ToString());
                        CadenaUsuarioId.Append(",");
                    }
                }

                return CadenaUsuarioId.ToString();
            }

            protected void SeleccionarEstatusBusqueda()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
                EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

                EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Usuario;

                Resultado = EstatusProcesoObjeto.SeleccionarEstatus(EstatusEntidadObjeto);

                EstatusBusqueda.DataValueField = "EstatusId";
                EstatusBusqueda.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    EstatusBusqueda.DataSource = Resultado.ResultadoDatos;
                    EstatusBusqueda.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                EstatusBusqueda.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void SeleccionarEstatusNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
                EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

                EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Usuario;

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

            protected void SeleccionarRolBusqueda()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RolEntidad RolEntidadObjeto = new RolEntidad();
                RolProceso RolProcesoObjeto = new RolProceso();

                Resultado = RolProcesoObjeto.SeleccionarRol(RolEntidadObjeto);

                RolBusqueda.DataValueField = "RolId";
                RolBusqueda.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    RolBusqueda.DataSource = Resultado.ResultadoDatos;
                    RolBusqueda.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                RolBusqueda.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void SeleccionarRolNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RolEntidad RolEntidadObjeto = new RolEntidad();
                RolProceso RolProcesoObjeto = new RolProceso();

                Resultado = RolProcesoObjeto.SeleccionarRol(RolEntidadObjeto);

                RolNuevo.DataValueField = "RolId";
                RolNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    RolNuevo.DataSource = Resultado.ResultadoDatos;
                    RolNuevo.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                RolNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarTextoError()
            {
                NombreRequerido.ErrorMessage = TextoError.UsuarioNombre + "<br />";
                ApellidoPaternoRequerido.ErrorMessage = TextoError.UsuarioApellidoPaterno + "<br />";
                CuentaUsuarioRequerido.ErrorMessage = TextoError.UsuarioCuenta + "<br />";
                CuentaUsuarioExpresion.ErrorMessage = TextoError.UsuarioCuentaInvalida + "<br />";
                RolRequerido.ErrorMessage = TextoError.UsuarioCuentaInvalida + "<br />";
                EstatusRequerido.ErrorMessage = TextoError.UsuarioCuentaInvalida + "<br />";
            }

            protected void SeleccionarUsuario()
            {
                UsuarioEntidad UsuarioEntidadObjeto = new UsuarioEntidad();

                SeleccionarUsuario(UsuarioEntidadObjeto);
            }

            protected void SeleccionarUsuario(UsuarioEntidad UsuarioEntidadObjeto)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                UsuarioProceso UsuarioProcesoNegocio = new UsuarioProceso();

                Resultado = UsuarioProcesoNegocio.SeleccionarUsuario(UsuarioEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaUsuario.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaUsuario.CssClass = ConstantePrograma.ClaseTabla;

                    TablaUsuario.DataSource = Resultado.ResultadoDatos;
                    TablaUsuario.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarUsuarioPorId(GridViewCommandEventArgs Evento)
            {
                Int16 Registro = 0;
                Int16 UsuarioId = 0;
                string Comando = string.Empty;

                Registro = Int16.Parse(Evento.CommandArgument.ToString());
                Comando = Evento.CommandName.ToString();

                switch (Comando)
                {
                    case "Select":
                        UsuarioId = Int16.Parse(TablaUsuario.DataKeys[Registro]["UsuarioId"].ToString());

                        SeleccionarUsuarioPorId(UsuarioId);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

            protected void SeleccionarUsuarioPorId(Int16 UsuarioId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                UsuarioEntidad UsuarioEntidadObjeto = new UsuarioEntidad();
                UsuarioProceso UsuarioProcesoNegocio = new UsuarioProceso();

                UsuarioEntidadObjeto.UsuarioId = UsuarioId;

                Resultado = UsuarioProcesoNegocio.SeleccionarUsuario(UsuarioEntidadObjeto);

                PanelNuevoRegistro.CssClass = ConstantePrograma.ClaseEditarRegistro;

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    {
                        LimiparCampos();

                        EtiquetaMensaje.Text = TextoError.UsuarioNoExiste;
                    }
                    else
                    {
                        NombreNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
                        ApellidoPaternoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["ApellidoPaterno"].ToString();
                        ApellidoMaternoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["ApellidoMaterno"].ToString();
                        CuentaUsuarioNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CuentaUsuario"].ToString();
                        RolNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["RolId"].ToString();
                        EstatusNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString();

                        UsuarioIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["UsuarioId"].ToString();

                        PanelBusquedaAvanzada.Visible = false;
                        PanelNuevoRegistro.Visible = true;
                    }
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }
        #endregion
    }
}
