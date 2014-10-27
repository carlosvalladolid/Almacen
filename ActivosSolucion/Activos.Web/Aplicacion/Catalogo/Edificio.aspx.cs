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
    public partial class Edificio : System.Web.UI.Page
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
                GuardarEdificio();
            }

            protected void BusquedaAvanzadaLink_Click(object sender, EventArgs e)
            {
                CambiarBusquedaAvanzada();
            }

            protected void EliminarRegistroLink_Click(object sender, EventArgs e)
            {
                EliminarEdificio();
            }

            protected void Estado_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarCiudad();
            }

            protected void EstadoNuevo_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarCiudadNuevo();
            }

            protected void ImagenBuscar_Click(object sender, ImageClickEventArgs e)
            {
                BusquedaAvanzada();
            }

            protected void NuevoRegistroLink_Click(object sender, EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void TablaEdificio_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaEdificio.PageIndex = e.NewPageIndex;
                BusquedaAvanzada();
            }

            protected void TablaEdificio_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaEdificioEventoComando(e);
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }
        #endregion

        #region "Métodos"

            protected void BusquedaAvanzada()
            {
                EdificioEntidad EdificioEntidadObjeto = new EdificioEntidad();

                EdificioEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
                EdificioEntidadObjeto.EstadoId = Int16.Parse(EstadoBusqueda.SelectedValue);
                EdificioEntidadObjeto.CiudadId = Int16.Parse(CiudadBusqueda.SelectedValue);
                EdificioEntidadObjeto.Calle = CalleBusqueda.Text.Trim();
                EdificioEntidadObjeto.CodigoPostal = CodigoPostalBusqueda.Text.Trim();
                EdificioEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

                SeleccionarEdificio(EdificioEntidadObjeto);
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
            }

            protected void CambiarEditarRegistro()
            {
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = true;
            }

            protected void EliminarEdificio()
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                EdificioEntidad EdificioEntidadObjeto = new EdificioEntidad();

                EdificioEntidadObjeto.CadenaEdificioId = ObtenerCadenaEdificioId();

                EliminarEdificio(EdificioEntidadObjeto);
            }

            protected void EliminarEdificio(EdificioEntidad EdificioEntidadObjeto)
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                EdificioProceso EdificioProcesoObjeto = new EdificioProceso();

                ResultadoEntidadObjeto = EdificioProcesoObjeto.EliminarEdificio(EdificioEntidadObjeto);

                if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Edificio.EliminacionExitosa)
                {
                    EtiquetaMensaje.Text = "";

                    BusquedaAvanzada();
                }
                else
                {
                    EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
                }
            }

            protected void GuardarEdificio()
            {
                EdificioEntidad EdificioObjetoEntidad = new EdificioEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                EdificioObjetoEntidad.EdificioId = Int16.Parse(EdificioIdHidden.Value);
                EdificioObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
                EdificioObjetoEntidad.UsuarioIdModifico = UsuarioSessionEntidad.UsuarioId;
                EdificioObjetoEntidad.Nombre = NombreNuevo.Text.Trim();
                EdificioObjetoEntidad.Calle = CalleNuevo.Text.Trim();
                EdificioObjetoEntidad.Numero = NumeroNuevo.Text.Trim();
                EdificioObjetoEntidad.Colonia = ColoniaNuevo.Text.Trim();
                EdificioObjetoEntidad.NumeroInt = NumeroInteriorNuevo.Text.Trim();
                EdificioObjetoEntidad.CodigoPostal = CodigoPostalNuevo.Text.Trim();
                EdificioObjetoEntidad.NombreArrendado = NombreArrendadoNuevo.Text.Trim();
                EdificioObjetoEntidad.TelefonoArrendado = TelefonoArrendadoNuevo.Text.Trim();
                EdificioObjetoEntidad.EmailArrendado = EmailArrendadoNuevo.Text.Trim();
                EdificioObjetoEntidad.EstatusId = Int16.Parse(EstatusNuevo.SelectedValue);
                EdificioObjetoEntidad.CiudadId = Int16.Parse(CiudadNuevo.SelectedValue);

                GuardarEdificio(EdificioObjetoEntidad);
            }

            protected void GuardarEdificio(EdificioEntidad EdificioObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EdificioProceso EdificioProcesoNegocio = new EdificioProceso();

                Resultado = EdificioProcesoNegocio.GuardarEdificio(EdificioObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Edificio.EdificioGuardadoCorrectamente)
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
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.Edificios);

                    SeleccionarEstatusNuevo();
                    SeleccionarEstado();
                    SeleccionarCiudadNuevo();
                    SeleccionarCiudad();
                    BusquedaAvanzada();
                    SeleccionarTextoError();

                    EliminarRegistroLink.Attributes.Add("onclick", "return ValidarCasillas()");
                }
            }

            protected void LimpiarNuevoRegistro()
            {
                NombreNuevo.Text = "";
                CalleNuevo.Text = "";
                NumeroNuevo.Text = "";
                ColoniaNuevo.Text = "";
                NumeroInteriorNuevo.Text = "";
                CodigoPostalNuevo.Text = "";
                NombreArrendadoNuevo.Text = "";
                TelefonoArrendadoNuevo.Text = "";
                EmailArrendadoNuevo.Text = "";
                EstatusNuevo.SelectedValue = "0";
                EstadoNuevo.SelectedValue = "0";
                SeleccionarCiudadNuevo();
                EtiquetaMensaje.Text = "";

                EdificioIdHidden.Value = "0";
            }

            protected string ObtenerCadenaEdificioId()
            {
                StringBuilder CadenaEdificioId = new StringBuilder();
                CheckBox CasillaEliminar;

                CadenaEdificioId.Append(",");

                foreach (GridViewRow Registro in TablaEdificio.Rows)
                {
                    CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                    if (CasillaEliminar.Checked)
                    {
                        CadenaEdificioId.Append(TablaEdificio.DataKeys[Registro.RowIndex]["EdificioId"].ToString());
                        CadenaEdificioId.Append(",");
                    }
                }

                return CadenaEdificioId.ToString();
            }

            protected void SeleccionarEdificio(EdificioEntidad EdificioObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EdificioProceso EdificioProcesoNegocio = new EdificioProceso();

                Resultado = EdificioProcesoNegocio.SeleccionarEdificio(EdificioObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaEdificio.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaEdificio.CssClass = ConstantePrograma.ClaseTabla;

                    TablaEdificio.DataSource = Resultado.ResultadoDatos;
                    TablaEdificio.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarEdificioParaEditar(EdificioEntidad EdificioObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EdificioProceso EdificioProcesoNegocio = new EdificioProceso();

                Resultado = EdificioProcesoNegocio.SeleccionarEdificio(EdificioObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    NombreNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
                    CalleNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Calle"].ToString();
                    NumeroNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Numero"].ToString();
                    ColoniaNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Colonia"].ToString();
                    NumeroInteriorNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroInt"].ToString();
                    CodigoPostalNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoPostal"].ToString();
                    NombreArrendadoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreArrendado"].ToString();
                    TelefonoArrendadoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["TelefonoArrendado"].ToString();
                    EmailArrendadoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmailArrendado"].ToString();
                    EstatusNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString();
                    EstadoNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstadoId"].ToString();
                    SeleccionarCiudadNuevo();
                    CiudadNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["CiudadId"].ToString();
                    CambiarEditarRegistro();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarEstatusNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
                EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

                EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Edificios;

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

            protected void SeleccionarEstado()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EstadoEntidad EstadoEntidadObjeto = new EstadoEntidad();
                EstadoProceso EstadoProcesoObjeto = new EstadoProceso();

                Resultado = EstadoProcesoObjeto.SeleccionarEstado(EstadoEntidadObjeto);

                EstadoNuevo.DataValueField = "EstadoId";
                EstadoNuevo.DataTextField = "Nombre";

                EstadoBusqueda.DataValueField = "EstadoId";
                EstadoBusqueda.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    EstadoNuevo.DataSource = Resultado.ResultadoDatos;
                    EstadoNuevo.DataBind();

                    EstadoBusqueda.DataSource = Resultado.ResultadoDatos;
                    EstadoBusqueda.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                EstadoNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
                EstadoBusqueda.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void SeleccionarCiudadNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                CiudadEntidad CiudadEntidadObjeto = new CiudadEntidad();
                CiudadProceso CiudadProcesoObjeto = new CiudadProceso();

                CiudadEntidadObjeto.EstadoId = Int16.Parse(EstadoNuevo.SelectedValue);

                if (CiudadEntidadObjeto.EstadoId == 0)
                {
                    CiudadNuevo.Items.Clear();
                }
                else
                {
                    Resultado = CiudadProcesoObjeto.SeleccionarCiudad(CiudadEntidadObjeto);

                    CiudadNuevo.DataValueField = "CiudadId";
                    CiudadNuevo.DataTextField = "Nombre";

                    if (Resultado.ErrorId == 0)
                    {
                        CiudadNuevo.DataSource = Resultado.ResultadoDatos;
                        CiudadNuevo.DataBind();
                    }
                    else
                    {
                        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    }
                }

                CiudadNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarCiudad()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                CiudadEntidad CiudadEntidadObjeto = new CiudadEntidad();
                CiudadProceso CiudadProcesoObjeto = new CiudadProceso();

                CiudadEntidadObjeto.EstadoId = Int16.Parse(EstadoBusqueda.SelectedValue);

                if (CiudadEntidadObjeto.EstadoId == 0)
                {
                    CiudadBusqueda.Items.Clear();
                }
                else
                {
                    Resultado = CiudadProcesoObjeto.SeleccionarCiudad(CiudadEntidadObjeto);

                    CiudadBusqueda.DataValueField = "CiudadId";
                    CiudadBusqueda.DataTextField = "Nombre";

                    if (Resultado.ErrorId == 0)
                    {
                        CiudadBusqueda.DataSource = Resultado.ResultadoDatos;
                        CiudadBusqueda.DataBind();
                    }
                    else
                    {
                        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    }
                }

                CiudadBusqueda.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void SeleccionarTextoError()
            {
                NombreRequerido.ErrorMessage = TextoError.EdificioNombre + "<br />";
                EstatusRequerido.ErrorMessage = TextoError.EdificioEstatus + "<br />";
                EstadoRequerido.ErrorMessage = TextoError.EdificioEstado + "<br />";
                CiudadRequerido.ErrorMessage = TextoError.EdificioCiudad + "<br />";
            }

            protected void TablaEdificioEventoComando(GridViewCommandEventArgs e)
            {
                EdificioEntidad EdificioEntidadObjeto = new EdificioEntidad();
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 EdificioId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaEdificio.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaEdificio.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        EdificioId = Int16.Parse(TablaEdificio.DataKeys[intFila]["EdificioId"].ToString());
                        EdificioEntidadObjeto.EdificioId = EdificioId;
                        EdificioIdHidden.Value = EdificioId.ToString();
                        SeleccionarEdificioParaEditar(EdificioEntidadObjeto);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

        #endregion
    }
}
