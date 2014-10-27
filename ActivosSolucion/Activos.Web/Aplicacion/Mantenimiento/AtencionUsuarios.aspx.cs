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

namespace Activos.Web.Aplicacion.Mantenimiento
{
    public partial class AtencionUsuarios : System.Web.UI.Page
    {
        #region "Eventos"

            protected void BotonAgregarActivo_Click(object sender, EventArgs e)
            {
                AgregarActivo();
            }

            protected void BotonAgregarEmpleado_Click(object sender, EventArgs e)
            {
                AgregarEmpleado();
            }

            protected void BotonBuscarMantenimiento_Click(object sender, EventArgs e)
            {
                ControlBuscarMantenimiento.InicioControl();
            }

            protected void BotonImprimir_Click(object sender, EventArgs e) 
            {
                ImprimirDocumento();
            }

            protected void BotonLimpiar_Click(object sender, EventArgs e)
            {
                LimpiarPantalla();
                DesbloquearDatosGenerales();
                MantenimientoFolio.Text = "";
                ComboAsignacion.SelectedIndex = 0;
                BotonImprimir.Enabled = false;
            }

            protected void BotonGuardar_Click(object sender, EventArgs e)
            {
                GuardarMantenimiento();
            }

            protected void CodigoBarrasImagen_Click(object sender, EventArgs e)
            {
                SeleccionarCodigoBarras();
            }

            protected void ComboAsignacion_SelectedIndexChanged(object sender, EventArgs e)
            {
                ComboAsignacionSelectedIndexChanged();
            }

            protected void LinkBuscarFolio_Click(object sender, EventArgs e)
            {
                SeleccionarFolioMantenimiento();
            }

            protected void MantenimientoFolio_TextChanged(object sender, EventArgs e)
            {
                SeleccionarFolioMantenimiento();
            }

            protected void RadioArea_CheckedChanged(object sender, EventArgs e)
            {
                RadiosCambioSeleccion();
            }

            protected void RadioEmpleado_CheckedChanged(object sender, EventArgs e)
            {
                RadiosCambioSeleccion();
            }

            protected void TablaActivosAgregados_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaActivosAgregadosEventoComando(e);
            }

            protected void TablaEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaEmpleadosEventoComando(e);
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

        #endregion

        #region "Métodos"

            protected void AgregarActivo()
            {
                TemporalMantenimientoActivoEntidad TemporalMantenimientoActivoObjetoEntidad = new TemporalMantenimientoActivoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                TemporalMantenimientoActivoObjetoEntidad.ActivosXML = ObtenerCadenaActivosXML();

                if (TemporalMantenimientoActivoObjetoEntidad.ActivosXML != "<row></row>")
                {
                    UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                    TemporalMantenimientoActivoObjetoEntidad.SesionId = UsuarioSessionEntidad.SesionId;
                    TemporalMantenimientoActivoObjetoEntidad.Descripcion = Descripcion.Text.Trim();
                    TemporalMantenimientoActivoObjetoEntidad.EstatusId = Int16.Parse(EstatusId.SelectedValue);
                    TemporalMantenimientoActivoObjetoEntidad.TipoAsistenciaId = Int16.Parse(TipoAsistenciaId.SelectedValue);
                    TemporalMantenimientoActivoObjetoEntidad.TipoMantenimientoId = Int16.Parse(TipoMantenimientoId.SelectedValue);

                    AgregarActivo(TemporalMantenimientoActivoObjetoEntidad);
                }
                else
                {
                    EtiquetaAgregarActivoError.Text = TextoError.MantenimientoActivosNoSeleccionados;
                }
            }

            protected void AgregarActivo(TemporalMantenimientoActivoEntidad TemporalMantenimientoActivoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalMantenimientoActivoProceso TemporalMantenimientoActivoProcesoNegocio = new TemporalMantenimientoActivoProceso();

                Resultado = TemporalMantenimientoActivoProcesoNegocio.InsertarTemporalMantenimientoActivo(TemporalMantenimientoActivoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalMantenimientoActivo.GuardadoCorrectamente)
                {
                    LimpiarAgregarActivoSeccion();
                    BloquearDatosGenerales();
                    LlenarTablaActivosAsignados();
                    SeleccionarActivosAgregados();
                }
                else
                {
                    EtiquetaAgregarActivoError.Text = Resultado.DescripcionError;
                }
            }

            protected void AgregarEmpleado()
            {
                TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoObjetoEntidad = new TemporalMantenimientoEmpleadoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                TemporalMantenimientoEmpleadoObjetoEntidad.SesionId = UsuarioSessionEntidad.SesionId;
                TemporalMantenimientoEmpleadoObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
                TemporalMantenimientoEmpleadoObjetoEntidad.EmpleadoId = Int16.Parse(ComboEmpleadoAtiende.SelectedValue);

                AgregarEmpleado(TemporalMantenimientoEmpleadoObjetoEntidad);
            }

            protected void AgregarEmpleado(TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalMantenimientoEmpleadoProceso TemporalMantenimientoEmpleadoProcesoNegocio = new TemporalMantenimientoEmpleadoProceso();

                Resultado = TemporalMantenimientoEmpleadoProcesoNegocio.AgregarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalMantenimientoEmpleado.GuardadoCorrectamente)
                {
                    ComboEmpleadoAtiende.SelectedIndex = 0;
                    SeleccionarEmpleadosAgregados();
                    EtiquetaMensajeError.Text = "";
                }
                else
                {
                    EtiquetaMensajeError.Text = Resultado.DescripcionError;
                }
            }

            protected void BloquearDatosGenerales()
            {
                RadioEmpleado.Enabled = false;
                RadioArea.Enabled = false;
                ComboAsignacion.Enabled = false;
                MantenimientoFolio.Enabled = false;
                BotonBuscarMantenimiento.Enabled = false;
            }

            public void BuscarMantenimientoPorId(int MantenimientoId)
            {
                MantenimientoFolio.Text = MantenimientoId.ToString();
                SeleccionarFolioMantenimiento();
                ActualizarTablaAtencionUsuario.Update();
            }

            protected void ComboAsignacionSelectedIndexChanged()
            {
                LimpiarPantalla();
                BotonImprimir.Enabled = false;
                MantenimientoFolio.Text = "";

                if (Int16.Parse(ComboAsignacion.SelectedValue) != 0)
                {
                    SeleccionarActivosAsignacion();
                }
            }

            protected void DesbloquearDatosGenerales()
            {
                RadioEmpleado.Enabled = true;
                RadioArea.Enabled = true;
                ComboAsignacion.Enabled = true;
                MantenimientoFolio.Enabled = true;
                BotonBuscarMantenimiento.Enabled = true;
            }

            protected void EliminarTemporalMantenimientoActivo(int TemporalMantenimientoActivoId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalMantenimientoActivoEntidad TemporalMantenimientoActivoObjetoEntidad = new TemporalMantenimientoActivoEntidad();
                TemporalMantenimientoActivoProceso TemporalMantenimientoActivoProcesoNegocio = new TemporalMantenimientoActivoProceso();

                TemporalMantenimientoActivoObjetoEntidad.TemporalMantenimientoActivoId = TemporalMantenimientoActivoId;

                Resultado = TemporalMantenimientoActivoProcesoNegocio.EliminarTemporalMantenimientoActivo(TemporalMantenimientoActivoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalMantenimientoActivo.EliminadoCorrectamente)
                {
                    LlenarTablaActivosAsignados();
                    SeleccionarActivosAgregados();
                }
                else
                {
                    EtiquetaAgregarActivoError.Text = Resultado.DescripcionError;
                }
            }

            protected void EliminarTemporalMantenimientoEmpleado(Int16 EmpleadoId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoObjetoEntidad = new TemporalMantenimientoEmpleadoEntidad();
                TemporalMantenimientoEmpleadoProceso TemporalMantenimientoEmpleadoProcesoNegocio = new TemporalMantenimientoEmpleadoProceso();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                TemporalMantenimientoEmpleadoObjetoEntidad.EmpleadoId = EmpleadoId;
                TemporalMantenimientoEmpleadoObjetoEntidad.SesionId = UsuarioSessionEntidad.SesionId;

                Resultado = TemporalMantenimientoEmpleadoProcesoNegocio.EliminarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalMantenimientoEmpleado.EliminadoCorrectamente)
                {
                    SeleccionarEmpleadosAgregados();
                    EtiquetaMensajeError.Text = "";
                }
                else
                {
                    EtiquetaMensajeError.Text = Resultado.DescripcionError;
                }
            }

            protected void GuardarMantenimiento()
            {
                MantenimientoEntidad MantenimientoEntidadObjeto = new MantenimientoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                if (TablaActivosAgregados.Rows.Count > 0 & TablaEmpleados.Rows.Count > 0)
                {
                    UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                    MantenimientoEntidadObjeto.SesionId = UsuarioSessionEntidad.SesionId;
                    MantenimientoEntidadObjeto.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;

                    if (RadioArea.Checked == true)
                        MantenimientoEntidadObjeto.DepartamentoId = Int16.Parse(ComboAsignacion.SelectedValue);
                    else
                        MantenimientoEntidadObjeto.EmpleadoId = Int16.Parse(ComboAsignacion.SelectedValue);

                    if (MantenimientoFolio.Text.Trim() != "")
                        MantenimientoEntidadObjeto.MantenimientoId = int.Parse(MantenimientoFolio.Text.Trim());

                    GuardarMantenimiento(MantenimientoEntidadObjeto);
                }
                else
                {
                    EtiquetaMensajeError.Text = "Es necesario agregar por lo menos un activo y un empleado.";
                }
                
            }

            protected void GuardarMantenimiento(MantenimientoEntidad MantenimientoEntidadObjeto)
            {
                MantenimientoProceso MantenimientoProcesoObjeto = new MantenimientoProceso();
                ResultadoEntidad Resultado = new ResultadoEntidad();

                Resultado = MantenimientoProcesoObjeto.GuardarMantenimiento(MantenimientoEntidadObjeto);

                if (Resultado.ErrorId == (int)ConstantePrograma.MantenimientoEmpleado.GuardadoCorrectamente)
                {
                    EtiquetaMensaje.Text = "Atención guardado correctamente.";
                    BotonImprimir.Enabled = true;
                    DesbloquearDatosGenerales();
                    LimpiarAgregarActivoSeccion();
                    MantenimientoIdHidden.Value = Resultado.NuevoRegistroId.ToString();

                    LimpiarGrids();

                    MantenimientoFolio.Text = "";
                    ComboAsignacion.SelectedIndex = 0;

                    //Se limpia la tabla temporal de empleados
                    LimpiarTemporalMantenimientoEmpleado();
                    ImprimirDocumento();
                }
                else
                {
                    EtiquetaMensajeError.Text = Resultado.DescripcionError;
                }

            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.AtencionUsuarios);

                    SeleccionarTipoAsistencia();
                    SeleccionarTipoMantenimiento();
                    SeleccionarEstatus();
                    SeleccionarEmpleadoAsignacion();
                    SeleccionarEmpleadoAtiende();

                    //Se limpian las tablas temporales
                    LimpiarTemporalMantenimientoActivo();
                    LimpiarTemporalMantenimientoEmpleado();

                    LimpiarGrids();

                    SeleccionarTextoError();
                }
            }

            protected void ImprimirDocumento()
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirDocumento('" + MantenimientoIdHidden.Value +"')", true);
            }

            protected void LimpiarAgregarActivoSeccion()
            {
                Descripcion.Text = "";
                TipoAsistenciaId.SelectedIndex = 0;
                EstatusId.SelectedIndex = 0;
                EtiquetaAgregarActivoError.Text = "";
            }

            protected void LimpiarGrids()
            {
                TablaActivosAsignados.DataSource = null;
                TablaActivosAsignados.DataBind();

                TablaActivosAgregados.DataSource = null;
                TablaActivosAgregados.DataBind();

                TablaEmpleados.DataSource = null;
                TablaEmpleados.DataBind();
            }

            protected void LimpiarPantalla()
            {
                LimpiarAgregarActivoSeccion();

                //Se limpian las tablas temporales
                LimpiarTemporalMantenimientoActivo();
                LimpiarTemporalMantenimientoEmpleado();

                LimpiarGrids();

                EtiquetaMensajeError.Text = "";
                EtiquetaMensaje.Text = "";
                EtiquetaAgregarActivoError.Text = "";
                EtiquetaMensajeCBError.Text = "";
                EtiquetaErrorDatosGenerales.Text = "";
            }

            protected void LimpiarTemporalMantenimientoActivo()
            {
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalMantenimientoActivoEntidad TemporalMantenimientoActivoObjetoEntidad = new TemporalMantenimientoActivoEntidad();
                TemporalMantenimientoActivoProceso TemporalMantenimientoActivoProcesoNegocio = new TemporalMantenimientoActivoProceso();
                
                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                TemporalMantenimientoActivoObjetoEntidad.SesionId = UsuarioSessionEntidad.SesionId;

                Resultado = TemporalMantenimientoActivoProcesoNegocio.EliminarTemporalMantenimientoActivo(TemporalMantenimientoActivoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalMantenimientoActivo.EliminadoCorrectamente)
                {
                    //Do nothing
                }
            }

            protected void LimpiarTemporalMantenimientoEmpleado()
            {
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoObjetoEntidad = new TemporalMantenimientoEmpleadoEntidad();
                TemporalMantenimientoEmpleadoProceso TemporalMantenimientoEmpleadoProcesoNegocio = new TemporalMantenimientoEmpleadoProceso();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                TemporalMantenimientoEmpleadoObjetoEntidad.SesionId = UsuarioSessionEntidad.SesionId;

                Resultado = TemporalMantenimientoEmpleadoProcesoNegocio.EliminarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalMantenimientoEmpleado.EliminadoCorrectamente)
                {
                    //Do nothing
                }
            }

            protected void LlenarTablaActivosAsignados()
            {
                if (MantenimientoFolio.Text.Trim() != "")
                {
                    SeleccionarMantenimientoActivo(false);
                }
                else
                {
                    SeleccionarActivosAsignacion();
                }
            }

            protected string ObtenerCadenaActivosXML()
            {
                StringBuilder CadenaActivosXML = new StringBuilder();
                CheckBox CheckBoxSeleccionarAtender;

                CadenaActivosXML.Append("<row>");

                foreach (GridViewRow Registro in TablaActivosAsignados.Rows)
                {
                    CheckBoxSeleccionarAtender = (CheckBox)Registro.FindControl("SeleccionarAtender");

                    if (CheckBoxSeleccionarAtender.Checked == true)
                    {
                        CadenaActivosXML.Append("<Activo ActivoId='");
                        CadenaActivosXML.Append(TablaActivosAsignados.DataKeys[Registro.RowIndex]["ActivoId"].ToString());
                        CadenaActivosXML.Append("'/>");
                    }
                }

                CadenaActivosXML.Append("</row>");

                return CadenaActivosXML.ToString();
            }

            protected void RadiosCambioSeleccion()
            {
                LimpiarPantalla();
                BotonImprimir.Enabled = false;
                MantenimientoFolio.Text = "";

                if (RadioEmpleado.Checked == true)
                {
                    SeleccionarEmpleadoAsignacion();
                }
                else
                {
                    SeleccionarDepartamentoAsignacion();
                }
            }

            protected void SeleccionarActivosAgregados()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalMantenimientoActivoEntidad TemporalMantenimientoActivoObjetoEntidad = new TemporalMantenimientoActivoEntidad();
                TemporalMantenimientoActivoProceso TemporalMantenimientoActivoProcesoNegocio = new TemporalMantenimientoActivoProceso();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                TemporalMantenimientoActivoObjetoEntidad.SesionId = UsuarioSessionEntidad.SesionId;

                Resultado = TemporalMantenimientoActivoProcesoNegocio.SeleccionarTemporalMantenimientoActivo(TemporalMantenimientoActivoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaActivosAgregados.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaActivosAgregados.CssClass = ConstantePrograma.ClaseTabla;

                    TablaActivosAgregados.DataSource = Resultado.ResultadoDatos;
                    TablaActivosAgregados.DataBind();
                }
                else
                {
                    EtiquetaAgregarActivoError.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarActivosAsignacion()
            {
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                ActivoObjetoEntidad.SesionId = UsuarioSessionEntidad.SesionId;
                ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;
                ActivoObjetoEntidad.TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.EquipoComputo;

                if (RadioEmpleado.Checked == true)
                    ActivoObjetoEntidad.EmpleadoId = Int16.Parse(ComboAsignacion.SelectedValue);
                else
                    ActivoObjetoEntidad.DepartamentoId = Int16.Parse(ComboAsignacion.SelectedValue);

                SeleccionarActivosAsignacion(ActivoObjetoEntidad);
            }

            protected void SeleccionarActivosAsignacion(ActivoEntidad ActivoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MovimientoProceso MovimientoProcesoNegocio = new MovimientoProceso();

                Resultado = MovimientoProcesoNegocio.SeleccionarMovimientoAsignacionMantenimientos(ActivoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaActivosAsignados.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaActivosAsignados.CssClass = ConstantePrograma.ClaseTabla;

                    TablaActivosAsignados.DataSource = Resultado.ResultadoDatos;
                    TablaActivosAsignados.DataBind();
                }
                else
                {
                    EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarCodigoBarras()
            {
                CheckBox chkAtender;
                bool ExisteActivo = false;

                foreach (GridViewRow FilaGrid in TablaActivosAsignados.Rows)
                {
                    if (TablaActivosAsignados.DataKeys[FilaGrid.RowIndex]["CodigoBarrasParticular"].ToString() == CodigoBarrasParticular.Text.Trim())
                    {
                        chkAtender = (CheckBox)FilaGrid.FindControl("SeleccionarAtender");

                        chkAtender.Checked = true;
                        ExisteActivo = true;
                        break;
                    }
                }

                if (ExisteActivo == false)
                    EtiquetaMensajeCBError.Text = "Activo no encontrado.";
                else
                    EtiquetaMensajeCBError.Text = "";

                CodigoBarrasParticular.Text = "";
                CodigoBarrasParticular.Focus();
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
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                ComboAsignacion.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
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
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                ComboAsignacion.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarEmpleadoAtiende()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

                EmpleadoEntidadObjeto.DepartamentoId = Int16.Parse(ConfigurationManager.AppSettings["Activos.Web.DepartamentoSistemaId"].ToString());
                EmpleadoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusEmpleados.Activo;

                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                ComboEmpleadoAtiende.DataValueField = "EmpleadoId";
                ComboEmpleadoAtiende.DataTextField = "NombreEmpleadoCompleto";

                if (Resultado.ErrorId == 0)
                {
                    ComboEmpleadoAtiende.DataSource = Resultado.ResultadoDatos;
                    ComboEmpleadoAtiende.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                ComboEmpleadoAtiende.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarEmpleadosAgregados()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoObjetoEntidad = new TemporalMantenimientoEmpleadoEntidad();
                TemporalMantenimientoEmpleadoProceso TemporalMantenimientoEmpleadoProcesoNegocio = new TemporalMantenimientoEmpleadoProceso();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                TemporalMantenimientoEmpleadoObjetoEntidad.SesionId = UsuarioSessionEntidad.SesionId;

                Resultado = TemporalMantenimientoEmpleadoProcesoNegocio.SeleccionarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaEmpleados.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaEmpleados.CssClass = ConstantePrograma.ClaseTabla;

                    TablaEmpleados.DataSource = Resultado.ResultadoDatos;
                    TablaEmpleados.DataBind();
                }
                else
                {
                    EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarEstatus()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
                EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

                EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Mantenimientos;

                Resultado = EstatusProcesoObjeto.SeleccionarEstatus(EstatusEntidadObjeto);

                EstatusId.DataValueField = "EstatusId";
                EstatusId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    EstatusId.DataSource = Resultado.ResultadoDatos;
                    EstatusId.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarFolioMantenimiento()
            {
                MantenimientoEntidad MantenimientoEntidadObjeto = new MantenimientoEntidad();

                LimpiarPantalla();
                BotonImprimir.Enabled = false;
                ComboAsignacion.SelectedIndex = 0;

                if (MantenimientoFolio.Text.Trim() != "")
                {
                    //Se valida que el folio sea un numero
                    if (Regex.Match(MantenimientoFolio.Text.Trim(), @"^[0-9]+$").Success == true)
                    {
                        EtiquetaErrorDatosGenerales.Text = "";
                        //Se busca si existe el folio del mantenimiento

                        MantenimientoEntidadObjeto.MantenimientoId = int.Parse(MantenimientoFolio.Text.Trim());
                        SeleccionarFolioMantenimiento(MantenimientoEntidadObjeto);
                    }
                    else
                    {
                        EtiquetaErrorDatosGenerales.Text = "Folio incorreto.";
                    }
                    
                }
            }

            protected void SeleccionarFolioMantenimiento(MantenimientoEntidad MantenimientoEntidadObjeto)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MantenimientoProceso MantenimientoProcesoNegocio = new MantenimientoProceso();

                Resultado = MantenimientoProcesoNegocio.SeleccionarMantenimiento(MantenimientoEntidadObjeto);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    //Si existe ese folio de mantenimiento, ahora se buscan los activos
                    SeleccionarMantenimientoActivo(true);
                }
                else
                {
                    EtiquetaErrorDatosGenerales.Text = "No existe ese folio.";
                }
            }

            protected void SeleccionarMantenimientoActivo(bool ValidarAtencionCompleta)
            {
                MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto = new MantenimientoActivoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                MantenimientoActivoEntidadObjeto.SesionId = UsuarioSessionEntidad.SesionId;
                MantenimientoActivoEntidadObjeto.MantenimientoId = int.Parse(MantenimientoFolio.Text.Trim());
                MantenimientoActivoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusMantenimientos.Cerrado;


                SeleccionarMantenimientoActivo(ValidarAtencionCompleta, MantenimientoActivoEntidadObjeto);
            }

            protected void SeleccionarMantenimientoActivo(bool ValidarAtencionCompleta, MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MantenimientoActivoProceso MantenimientoActivoProcesoNegocio = new MantenimientoActivoProceso();

                Resultado = MantenimientoActivoProcesoNegocio.SeleccionarMantenimientoActivo(MantenimientoActivoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0 & ValidarAtencionCompleta == true)
                    {
                        EtiquetaErrorDatosGenerales.Text = "No hay nada pendiente con ese folio de atención.";
                    }
                    else
                    {
                        if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                            TablaActivosAsignados.CssClass = ConstantePrograma.ClaseTablaVacia;
                        else
                            TablaActivosAsignados.CssClass = ConstantePrograma.ClaseTabla;

                        TablaActivosAsignados.DataSource = Resultado.ResultadoDatos;
                        TablaActivosAsignados.DataBind();

                        //Si es cuando se selecciona por primera ves el folio entonces se llenan los empleados
                        if (ValidarAtencionCompleta == true)
                        {
                            ObtenerEmpleadosDelMantenimiento();
                        }
                    } 
                    
                }
                else
                {
                    EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarTextoError()
            {
                DescripcionRequerido.ErrorMessage = TextoError.MantenimientoDescripcionObligatorio + "<br />";
                BuscarCodigoBarrasRequerido.ErrorMessage = TextoError.RecepcionCodigoBarrasParticular + "<br />";
                EmpleadoAtiendeRequerido.ErrorMessage = TextoError.MantenimientoEmpleadoAtiendeObligatorio + "<br />";
            }

            protected void ObtenerEmpleadosDelMantenimiento()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoEntidadObjeto = new TemporalMantenimientoEmpleadoEntidad();
                TemporalMantenimientoEmpleadoProceso TemporalMantenimientoEmpleadoProcesoNegocio = new TemporalMantenimientoEmpleadoProceso();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                TemporalMantenimientoEmpleadoEntidadObjeto.MantenimientoId = int.Parse(MantenimientoFolio.Text.Trim());
                TemporalMantenimientoEmpleadoEntidadObjeto.SesionId = UsuarioSessionEntidad.SesionId;

                Resultado = TemporalMantenimientoEmpleadoProcesoNegocio.InsertarTemporalMantenimientoEmpleadoAnteriores(TemporalMantenimientoEmpleadoEntidadObjeto);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalMantenimientoEmpleado.GuardadoCorrectamente)
                {
                    SeleccionarEmpleadosAgregados();
                }
                else
                {
                    EtiquetaMensajeError.Text = Resultado.DescripcionError;
                }
            }

            protected void SeleccionarTipoAsistencia()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TipoAsistenciaEntidad TipoAsistenciaEntidadObjeto = new TipoAsistenciaEntidad();
                TipoAsistenciaProceso TipoAsistenciaProcesoObjeto = new TipoAsistenciaProceso();

                Resultado = TipoAsistenciaProcesoObjeto.SeleccionarTipoAsistencia(TipoAsistenciaEntidadObjeto);

                TipoAsistenciaId.DataValueField = "TipoAsistenciaId";
                TipoAsistenciaId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    TipoAsistenciaId.DataSource = Resultado.ResultadoDatos;
                    TipoAsistenciaId.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarTipoMantenimiento()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TipoMantenimientoEntidad TipoMantenimientoEntidadObjeto = new TipoMantenimientoEntidad();
                TipoMantenimientoProceso TipoMantenimientoProcesoObjeto = new TipoMantenimientoProceso();

                Resultado = TipoMantenimientoProcesoObjeto.SeleccionarTipoMantenimiento(TipoMantenimientoEntidadObjeto);

                TipoMantenimientoId.DataValueField = "TipoMantenimientoId";
                TipoMantenimientoId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    TipoMantenimientoId.DataSource = Resultado.ResultadoDatos;
                    TipoMantenimientoId.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void TablaActivosAgregadosEventoComando(GridViewCommandEventArgs e)
            {
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                int TemporalMantenimientoActivoId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaActivosAgregados.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaActivosAgregados.PageIndex));

                switch (strCommand)
                {
                    case "EliminarActivo":
                        TemporalMantenimientoActivoId = int.Parse(TablaActivosAgregados.DataKeys[intFila]["TemporalMantenimientoActivoId"].ToString());
                        EliminarTemporalMantenimientoActivo(TemporalMantenimientoActivoId);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

            protected void TablaEmpleadosEventoComando(GridViewCommandEventArgs e)
            {
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 EmpleadoId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaEmpleados.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaEmpleados.PageIndex));

                switch (strCommand)
                {
                    case "EliminarEmpleado":
                        EmpleadoId = Int16.Parse(TablaEmpleados.DataKeys[intFila]["EmpleadoId"].ToString());
                        EliminarTemporalMantenimientoEmpleado(EmpleadoId);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

        #endregion
    }
}
