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
    public partial class Empleado : System.Web.UI.Page
    {
        #region "Eventos"

            protected void BotonBuscarJefe_Click(object sender, EventArgs e)
            {
                ControlBuscarJefe.InicioControl();
            }

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
                GuardarEmpleado();
            }

            protected void BusquedaAvanzadaLink_Click(object sender, EventArgs e)
            {
                CambiarBusquedaAvanzada();
            }

            protected void EliminarRegistroLink_Click(object sender, EventArgs e)
            {
                EliminarEmpleado();
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

            protected void TablaEmpleado_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaEmpleado.PageIndex = e.NewPageIndex;
                BusquedaAvanzada();
            }

            protected void TablaEmpleado_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaEmpleadoEventoComando(e);
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }
        #endregion

        #region "Métodos"

            protected void BusquedaAvanzada()
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.DepartamentoId = Int16.Parse(DepartamentoBusqueda.SelectedValue);
                EmpleadoEntidadObjeto.EdificioId = Int16.Parse(EdificioBusqueda.SelectedValue);
                EmpleadoEntidadObjeto.PuestoId = Int16.Parse(PuestoBusqueda.SelectedValue);
                EmpleadoEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
                EmpleadoEntidadObjeto.TrabajoEmail = CorreoElectronicoBusqueda.Text.Trim();

                EmpleadoEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

                SeleccionarEmpleado(EmpleadoEntidadObjeto);
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

            protected void EliminarEmpleado()
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.CadenaEmpleadoId = ObtenerCadenaEmpleadoId();

                EliminarEmpleado(EmpleadoEntidadObjeto);
            }

            protected void EliminarEmpleado(EmpleadoEntidad EmpleadoObjetoEntidad)
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

                ResultadoEntidadObjeto = EmpleadoProcesoObjeto.EliminarEmpleado(EmpleadoObjetoEntidad);

                if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Empleado.EliminacionExitosa)
                {
                    EtiquetaMensaje.Text = "";

                    BusquedaAvanzada();
                }
                else
                {
                    EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
                }
            }

            protected void GuardarEmpleado()
            {
                EmpleadoEntidad EmpleadoObjetoEntidad = new EmpleadoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                EmpleadoObjetoEntidad.EmpleadoId = Int16.Parse(EmpleadoIdHidden.Value);
                EmpleadoObjetoEntidad.CiudadId = Int16.Parse(CiudadNuevo.SelectedValue);
                EmpleadoObjetoEntidad.DepartamentoId = Int16.Parse(DepartamentoNuevo.SelectedValue);
                EmpleadoObjetoEntidad.EdificioId = Int16.Parse(EdificioNuevo.SelectedValue);
                EmpleadoObjetoEntidad.EmpleadoIdJefe = Int16.Parse(EmpleadoIdJefeHidden.Value);
                EmpleadoObjetoEntidad.PuestoId = Int16.Parse(PuestoNuevo.SelectedValue);
                EmpleadoObjetoEntidad.EstatusId = Int16.Parse(EstatusNuevo.SelectedValue);
                EmpleadoObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
                EmpleadoObjetoEntidad.UsuarioIdModifico = UsuarioSessionEntidad.UsuarioId;
                EmpleadoObjetoEntidad.Nombre = NombreNuevo.Text.Trim();
                EmpleadoObjetoEntidad.ApellidoPaterno = ApellidoPaternoNuevo.Text.Trim();
                EmpleadoObjetoEntidad.ApellidoMaterno = ApellidoMaternoNuevo.Text.Trim();
                EmpleadoObjetoEntidad.RFC = RFCNuevo.Text.Trim();
                EmpleadoObjetoEntidad.Calle = CalleNuevo.Text.Trim();
                EmpleadoObjetoEntidad.Numero = NumeroNuevo.Text.Trim();
                EmpleadoObjetoEntidad.Colonia = ColoniaNuevo.Text.Trim();
                EmpleadoObjetoEntidad.CodigoPostal = CodigoPostalNuevo.Text.Trim();
                EmpleadoObjetoEntidad.TelefonoCasa = TelefonoCasaNuevo.Text.Trim();
                EmpleadoObjetoEntidad.Celular = CelularNuevo.Text.Trim();
                EmpleadoObjetoEntidad.Email = EmailNuevo.Text.Trim();
                EmpleadoObjetoEntidad.NumeroEmpleado = NumeroEmpleadoNuevo.Text.Trim();
                EmpleadoObjetoEntidad.TelefonoTrabajo = TelefonoTrabajoNuevo.Text.Trim();
                EmpleadoObjetoEntidad.TrabajoEmail = TrabajoEmailNuevo.Text.Trim();

                GuardarEmpleado(EmpleadoObjetoEntidad);
            }

            protected void GuardarEmpleado(EmpleadoEntidad EmpleadoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

                Resultado = EmpleadoProcesoNegocio.GuardarEmpleado(EmpleadoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Empleado.EmpleadoGuardadoCorrectamente)
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
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.Empleados);

                    SeleccionarDepartamento();
                    SeleccionarPuesto();
                    SeleccionarEstatusNuevo();
                    SeleccionarEstadoNuevo();
                    SeleccionarEdificio();
                    SeleccionarCiudadNuevo();
                    BusquedaAvanzada();
                    SeleccionarTextoError();

                    EliminarRegistroLink.Attributes.Add("onclick", "return ValidarCasillas()");
                }
            }

            protected void LimpiarNuevoRegistro()
            {
                NombreNuevo.Text = "";
                ApellidoPaternoNuevo.Text = "";
                ApellidoMaternoNuevo.Text = "";
                RFCNuevo.Text = "";
                CalleNuevo.Text = "";
                NumeroNuevo.Text = "";
                ColoniaNuevo.Text = "";
                CodigoPostalNuevo.Text = "";
                EstadoNuevo.SelectedValue = "0";
                SeleccionarCiudadNuevo();
                TelefonoCasaNuevo.Text = "";
                CelularNuevo.Text = "";
                EmailNuevo.Text = "";
                NumeroEmpleadoNuevo.Text = "";
                TelefonoTrabajoNuevo.Text = "";
                TrabajoEmailNuevo.Text = "";
                DepartamentoNuevo.SelectedIndex = 0;
                EdificioNuevo.SelectedIndex = 0;
                PuestoNuevo.SelectedIndex = 0;
                EstatusNuevo.SelectedValue = "0";
                NombreEmpleadoJefe.Text = "";
                EtiquetaMensaje.Text = "";

                EmpleadoIdHidden.Value = "0";
                EmpleadoIdJefeHidden.Value = "0";
            }

            protected string ObtenerCadenaEmpleadoId()
            {
                StringBuilder CadenaEmpleadoId = new StringBuilder();
                CheckBox CasillaEliminar;

                CadenaEmpleadoId.Append(",");

                foreach (GridViewRow Registro in TablaEmpleado.Rows)
                {
                    CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                    if (CasillaEliminar.Checked)
                    {
                        CadenaEmpleadoId.Append(TablaEmpleado.DataKeys[Registro.RowIndex]["EmpleadoId"].ToString());
                        CadenaEmpleadoId.Append(",");
                    }
                }

                return CadenaEmpleadoId.ToString();
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

            protected void SeleccionarDepartamento()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DepartamentoEntidad DepartamentoEntidadObjeto = new DepartamentoEntidad();
                DepartamentoProceso DepartamentoProcesoObjeto = new DepartamentoProceso();

                //DepartamentoEntidadObjeto.EstatusId = (int)ConstantePrograma.EstatusDepartamentos.Activo;

                Resultado = DepartamentoProcesoObjeto.SeleccionarDepartamento(DepartamentoEntidadObjeto);

                DepartamentoNuevo.DataValueField = "DepartamentoId";
                DepartamentoNuevo.DataTextField = "Nombre";

                DepartamentoBusqueda.DataValueField = "DepartamentoId";
                DepartamentoBusqueda.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    DepartamentoNuevo.DataSource = Resultado.ResultadoDatos;
                    DepartamentoNuevo.DataBind();

                    DepartamentoBusqueda.DataSource = Resultado.ResultadoDatos;
                    DepartamentoBusqueda.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                DepartamentoNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
                DepartamentoBusqueda.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void SeleccionarEdificio()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EdificioEntidad EdificioEntidadObjeto = new EdificioEntidad();
                EdificioProceso EdificioProcesoObjeto = new EdificioProceso();

                //EdificioEntidadObjeto.EstatusId = (int)ConstantePrograma.EstatusEdificio.Activo;

                Resultado = EdificioProcesoObjeto.SeleccionarEdificio(EdificioEntidadObjeto);

                EdificioNuevo.DataValueField = "EdificioId";
                EdificioNuevo.DataTextField = "Nombre";

                EdificioBusqueda.DataValueField = "EdificioId";
                EdificioBusqueda.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    EdificioNuevo.DataSource = Resultado.ResultadoDatos;
                    EdificioNuevo.DataBind();

                    EdificioBusqueda.DataSource = Resultado.ResultadoDatos;
                    EdificioBusqueda.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                EdificioNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
                EdificioBusqueda.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            protected void SeleccionarEmpleado(EmpleadoEntidad EmpleadoObjetoEntidad)
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
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarEmpleadoParaEditar(EmpleadoEntidad EmpleadoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

                Resultado = EmpleadoProcesoNegocio.SeleccionarEmpleado(EmpleadoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    EmpleadoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                    NombreNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
                    ApellidoPaternoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["ApellidoPaterno"].ToString();
                    ApellidoMaternoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["ApellidoMaterno"].ToString();
                    RFCNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["RFC"].ToString();
                    CalleNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Calle"].ToString();
                    NumeroNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Numero"].ToString();
                    ColoniaNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Colonia"].ToString();
                    CodigoPostalNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoPostal"].ToString();
                    EstadoNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstadoId"].ToString();
                    SeleccionarCiudadNuevo();
                    CiudadNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["CiudadId"].ToString();
                    TelefonoCasaNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["TelefonoCasa"].ToString();
                    CelularNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Celular"].ToString();
                    EmailNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Email"].ToString();
                    NumeroEmpleadoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                    TelefonoTrabajoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["TelefonoTrabajo"].ToString();
                    TrabajoEmailNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["TrabajoEmail"].ToString();
                    DepartamentoNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["DepartamentoId"].ToString();
                    EdificioNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EdificioId"].ToString();
                    PuestoNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["PuestoId"].ToString();
                    EstatusNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString();
                    SeleccionarJefe(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoIdJefe"].ToString()));
                    CambiarEditarRegistro();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarEstadoNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EstadoEntidad EstadoEntidadObjeto = new EstadoEntidad();
                EstadoProceso EstadoProcesoObjeto = new EstadoProceso();

                Resultado = EstadoProcesoObjeto.SeleccionarEstado(EstadoEntidadObjeto);

                EstadoNuevo.DataValueField = "EstadoId";
                EstadoNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    EstadoNuevo.DataSource = Resultado.ResultadoDatos;
                    EstadoNuevo.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                EstadoNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarEstatusNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
                EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

                EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Empleados;

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

            public void SeleccionarJefe(Int16 EmpleadoIdJefe)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

                if (EmpleadoIdJefe != 0)
                {
                    EmpleadoEntidadObjeto.EmpleadoId = EmpleadoIdJefe;

                    Resultado = EmpleadoProcesoNegocio.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                    if (Resultado.ErrorId == 0)
                    {
                        NombreEmpleadoJefe.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                        EmpleadoIdJefeHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                        ActualizarTablaEmpleado.Update();
                    }
                    else
                    {
                        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    }
                }
                else
                {
                    NombreEmpleadoJefe.Text = "";
                    EmpleadoIdJefeHidden.Value = "0";
                    ActualizarTablaEmpleado.Update();
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
                NombreRequerido.ErrorMessage = TextoError.EmpleadoNombre + "<br />";
                ApellidoPaternoRequerido.ErrorMessage = TextoError.EmpleadoApellidoPaterno + "<br />";
                EstadoRequerido.ErrorMessage = TextoError.EmpleadoEstado + "<br />";
                CiudadRequerido.ErrorMessage = TextoError.EmpleadoCiudad + "<br />";
                NumeroEmpleadoRequerido.ErrorMessage = TextoError.EmpleadoNumeroEmpleado + "<br />";
                DepartamentoRequerido.ErrorMessage = TextoError.EmpleadoDepartamento + "<br />";
                EdificioRequerido.ErrorMessage = TextoError.EmpleadoEdificio + "<br />";
                PuestoRequerido.ErrorMessage = TextoError.EmpleadoPuesto + "<br />";
                EstatusRequerido.ErrorMessage = TextoError.EmpleadoEstatus + "<br />";
                //NombreEmpleadoJefeRequerido.ErrorMessage = TextoError.EmpleadoJefe + "<br />";
            }

            protected void TablaEmpleadoEventoComando(GridViewCommandEventArgs e)
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
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
                        EmpleadoEntidadObjeto.EmpleadoId = EmpleadoId;
                        EmpleadoIdHidden.Value = EmpleadoId.ToString();
                        SeleccionarEmpleadoParaEditar(EmpleadoEntidadObjeto);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

        #endregion
    }
}
