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
    public partial class Proveedor : System.Web.UI.Page
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
                GuardarProveedor();
            }

            protected void BusquedaAvanzadaLink_Click(object sender, EventArgs e)
            {
                CambiarBusquedaAvanzada();
            }

            protected void EstadoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarCiudad();
            }

            protected void EstadoNuevo_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarCiudadNuevo();
            }

            protected void EliminarRegistroLink_Click(object sender, EventArgs e)
            {
                EliminarProveedor();
            }

            protected void ImagenBuscar_Click(object sender, ImageClickEventArgs e)
            {
                BusquedaAvanzada();
            }

            protected void NuevoRegistroLink_Click(object sender, EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void TablaProveedor_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaProveedor.PageIndex = e.NewPageIndex;
                BusquedaAvanzada();
            }

            protected void TablaProveedor_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaProveedorEventoComando(e);
            }
       
            protected void Page_Load(object sender, EventArgs e)
                {
                    Inicio();
                }
        #endregion

        #region "Métodos"

            protected void BusquedaAvanzada()
            {
                ProveedorEntidad ProveedorEntidadObjeto = new ProveedorEntidad();

                ProveedorEntidadObjeto.EstadoId = Int16.Parse(EstadoBusqueda.SelectedValue);
                ProveedorEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
                ProveedorEntidadObjeto.RFC = RFCBusqueda.Text.Trim();
                ProveedorEntidadObjeto.CiudadId = Int16.Parse(CiudadBusqueda.SelectedValue);
                ProveedorEntidadObjeto.NombreContacto = NombreContactoBusqueda.Text.Trim();
                ProveedorEntidadObjeto.EstadoId = Int16.Parse(EstadoBusqueda.SelectedValue);
                ProveedorEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

                SeleccionarProveedor(ProveedorEntidadObjeto);
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

            protected void EliminarProveedor()
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                ProveedorEntidad ProveedorEntidadObjeto = new ProveedorEntidad();

                ProveedorEntidadObjeto.CadenaProveedorId = ObtenerCadenaProveedorId();

                EliminarProveedor(ProveedorEntidadObjeto);
            }

            protected void EliminarProveedor(ProveedorEntidad ProveedorEntidadObjeto)
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                ProveedorProceso ProveedorProcesoObjeto = new ProveedorProceso();

                ResultadoEntidadObjeto = ProveedorProcesoObjeto.EliminarProveedor(ProveedorEntidadObjeto);

                if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Proveedor.EliminacionExitosa)
                {
                    EtiquetaMensaje.Text = "";

                    BusquedaAvanzada();
                }
                else
                {
                    EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
                }
            }

            protected string ObtenerCadenaProveedorId()
            {
                StringBuilder CadenaProveedorId = new StringBuilder();
                CheckBox CasillaEliminar;

                CadenaProveedorId.Append(",");

                foreach (GridViewRow Registro in TablaProveedor.Rows)
                {
                    CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                    if (CasillaEliminar.Checked)
                    {
                        CadenaProveedorId.Append(TablaProveedor.DataKeys[Registro.RowIndex]["ProveedorId"].ToString());
                        CadenaProveedorId.Append(",");
                    }
                }

                return CadenaProveedorId.ToString();
            }

            protected void GuardarProveedor()
            {
                ProveedorEntidad ProveedorObjetoEntidad = new ProveedorEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                ProveedorObjetoEntidad.ProveedorId = Int16.Parse(ProveedorIdHidden.Value);
                ProveedorObjetoEntidad.DependenciaId = Int16.Parse(DependenciaNuevo.SelectedValue);
                ProveedorObjetoEntidad.BancoId = Int16.Parse(BancoNuevo.SelectedValue);
                ProveedorObjetoEntidad.CiudadId = Int16.Parse(CiudadNuevo.SelectedValue);
                ProveedorObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
                ProveedorObjetoEntidad.UsuarioIdModifico = UsuarioSessionEntidad.UsuarioId;               
                ProveedorObjetoEntidad.Nombre = NombreNuevo.Text.Trim();
                ProveedorObjetoEntidad.RFC = RFCNuevo.Text.Trim();               
                ProveedorObjetoEntidad.Calle = CalleNuevo.Text.Trim();
                ProveedorObjetoEntidad.Numero = NumeroNuevo.Text.Trim();
                ProveedorObjetoEntidad.Colonia = ColoniaNuevo.Text.Trim();
                ProveedorObjetoEntidad.CodigoPostal = CodigoPostalNuevo.Text.Trim();
                ProveedorObjetoEntidad.Telefono = TelefonoNuevo.Text.Trim();
                ProveedorObjetoEntidad.NombreContacto = NombreContactoNuevo.Text.Trim();
                ProveedorObjetoEntidad.Email = EmailNuevo.Text.Trim();
                ProveedorObjetoEntidad.CiudadOtro = OtraCiudadNuevo.Text.Trim();

                ProveedorObjetoEntidad.Cuenta = CuentaNuevo.Text.Trim();

                ProveedorObjetoEntidad.Clabe = ClabeNuevo.Text.Trim();   
            
                GuardarProveedor(ProveedorObjetoEntidad);
            }

            protected void GuardarProveedor(ProveedorEntidad ProveedorObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ProveedorProceso ProveedorProcesoNegocio = new ProveedorProceso();

                Resultado = ProveedorProcesoNegocio.GuardarProveedor(ProveedorObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Proveedor.ProveedorGuardadoCorrectamente)
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

            protected void LimpiarNuevoRegistro()
            {
                NombreNuevo.Text = "";
                CalleNuevo.Text = "";
                NumeroNuevo.Text = "";
                ColoniaNuevo.Text = "";
                NumeroNuevo.Text = "";
                CodigoPostalNuevo.Text = "";
                NombreContactoNuevo.Text = "";
                TelefonoNuevo.Text = "";
                EmailNuevo.Text = "";
                ColoniaNuevo.Text = "";
                CuentaNuevo.Text = "";
                ClabeNuevo.Text = "";
                RFCNuevo.Text = "";
                BancoNuevo.SelectedValue = "0";
                CiudadNuevo.SelectedValue = "0";
                OtraCiudadNuevo.Text = "";
                //EstadoNuevo.SelectedValue = "0";
                DependenciaNuevo.SelectedValue = "0";
                EtiquetaMensaje.Text = "";

                ProveedorIdHidden.Value = "0";
            }

            protected void SeleccionarProveedor(ProveedorEntidad ProveedorObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ProveedorProceso ProveedorProcesoNegocio = new ProveedorProceso();

                Resultado = ProveedorProcesoNegocio.SeleccionarProveedor(ProveedorObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaProveedor.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaProveedor.CssClass = ConstantePrograma.ClaseTabla;

                    TablaProveedor.DataSource = Resultado.ResultadoDatos;
                    TablaProveedor.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarProveedorParaEditar(ProveedorEntidad ProveedorObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ProveedorProceso ProveedorProcesoNegocio = new ProveedorProceso();

                Resultado = ProveedorProcesoNegocio.SeleccionarProveedor(ProveedorObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    NombreNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
                    CalleNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Calle"].ToString();
                    NumeroNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Numero"].ToString();
                    ColoniaNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Colonia"].ToString();
                    CodigoPostalNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoPostal"].ToString();
                    NombreContactoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreContacto"].ToString();
                    TelefonoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Telefono"].ToString();
                    EmailNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Email"].ToString();
                    CuentaNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Cuenta"].ToString();
                    ClabeNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Clabe"].ToString();
                    DependenciaNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["DependenciaId"].ToString();
                    BancoNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["BancoId"].ToString();
                    RFCNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["RFC"].ToString();
                    EstadoNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstadoId"].ToString();
                    SeleccionarCiudadNuevo();
                    CiudadNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["CiudadId"].ToString();
                    OtraCiudadNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CiudadOtro"].ToString();
                    CambiarEditarRegistro();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }
            
            protected void SeleccionarDependencia()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DependenciaEntidad DependenciaEntidadObjeto = new DependenciaEntidad();
                DependenciaProceso DependenciaProcesoObjeto = new DependenciaProceso();

                Resultado = DependenciaProcesoObjeto.SeleccionarDependencia(DependenciaEntidadObjeto);

                DependenciaNuevo.DataValueField = "DependenciaId";
                DependenciaNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    DependenciaNuevo.DataSource = Resultado.ResultadoDatos;
                    DependenciaNuevo.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                DependenciaNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarBanco()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                BancoEntidad BancoEntidadObjeto = new BancoEntidad();
                BancoProceso BancoProcesoObjeto = new BancoProceso();

                Resultado = BancoProcesoObjeto.SeleccionarBanco(BancoEntidadObjeto);

                BancoNuevo.DataValueField = "BancoId";
                BancoNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    BancoNuevo.DataSource = Resultado.ResultadoDatos;
                    BancoNuevo.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                BancoNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
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
                NombreRequerido.ErrorMessage = TextoError.ProveedorNombre + "<br />";
                DependenciaRequerido.ErrorMessage = TextoError.ProveedorDependencia + "<br />";
                NombreContactoRequerido.ErrorMessage = TextoError.ProveedorNombreContacto + "<br />";
                EstadoRequerido.ErrorMessage = TextoError.ProveedorEstado + "<br />";
                CiudadRequerido.ErrorMessage = TextoError.ProveedorCiudad + "<br />";
            }

            protected void TablaProveedorEventoComando(GridViewCommandEventArgs e)
            {
                ProveedorEntidad ProveedorEntidadObjeto = new ProveedorEntidad();
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 ProveedorId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaProveedor.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaProveedor.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        ProveedorId = Int16.Parse(TablaProveedor.DataKeys[intFila]["ProveedorId"].ToString());
                        ProveedorEntidadObjeto.ProveedorId = ProveedorId;
                        ProveedorIdHidden.Value = ProveedorId.ToString();
                        SeleccionarProveedorParaEditar(ProveedorEntidadObjeto);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.Proveedores);

                    SeleccionarDependencia();
                    SeleccionarEstado();
                    SeleccionarCiudadNuevo();
                    SeleccionarCiudad();
                    SeleccionarBanco();
                    BusquedaAvanzada();
                    SeleccionarTextoError();

                    EliminarRegistroLink.Attributes.Add("onclick", "return ValidarCasillas()");
                }
            }

            
        #endregion
    }
}
