using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Comun.Fecha;
using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Aplicacion.Activo
{
    public partial class AsignacionActivo : System.Web.UI.Page
    {
        #region "Eventos"

            protected void BotonActualizarActivo_Click(object sender, EventArgs e)
            {
                AgregarActivo();
            }

            protected void BotonAgregarActivo_Click(object sender, EventArgs e)
            {
                AgregarActivo();
            }

            protected void BotonCancelar_Click(object sender, EventArgs e)
            {
                LimpiarTemporalTablaAsignacion();
                LimpiarFormulario();
                OcultarBotonesImprimir();
            }

            protected void BotonCancelarActualizar_Click(object sender, EventArgs e)
            {
                LimpiarActivo();
                CambiarBotonesNuevo();
            }

            protected void BotonGuardar_Click(object sender, EventArgs e)
            {
                GuardarAsignacion();
            }

            protected void BotonImprimir_Click(object sender, EventArgs e)
            {
                ImprimirDocumento();
            }

            protected void BotonImprimirVehiculo_Click(object sender, EventArgs e)
            {
                ImprimirDocumentoVehiculo();
            }

            protected void BotonImprimirVehiculoReverso_Click(object sender, EventArgs e)
            {
                ImprimirDocumentoVehiculoReverso();
            }

            protected void EmpleadoId_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarAsignacion();
            }

            protected void LinkBuscarActivo_Click(object sender, EventArgs e)
            {
                SeleccionarActivo();
            }

            protected void TablaActivo_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaActivoEventoComando(e);
            }

            protected void TablaActivo_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton BotonEliminarActivo = (ImageButton)e.Row.FindControl("BotonEliminarActivo");
                    LinkButton LigaDescripcion = (LinkButton)e.Row.FindControl("LigaDescripcion");

                    if (TablaActivo.DataKeys[e.Row.RowIndex]["TemporalAsignacionDetalleId"].ToString() == "0")
                    {
                        BotonEliminarActivo.Visible = false;
                        LigaDescripcion.Enabled = false;
                    }
                }
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

        #endregion

        #region "Métodos"

            protected void AgregarActivo()
            {
                TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad = new TemporalAsignacionEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                SeleccionarActivo();

                if (ActivoIdHidden.Value != "0")
                {
                    UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                    TemporalAsignacionObjetoEntidad.UsuarioId = UsuarioSessionEntidad.UsuarioId;
                    TemporalAsignacionObjetoEntidad.TemporalAsignacionId = Int16.Parse(TemporalAsignacionIdHidden.Value);
                    TemporalAsignacionObjetoEntidad.TemporalAsignacionDetalleId = Int16.Parse(TemporalAsignacionDetalleIdHidden.Value);
                    TemporalAsignacionObjetoEntidad.ActivoId = Int16.Parse(ActivoIdHidden.Value);
                    TemporalAsignacionObjetoEntidad.CondicionId = Int16.Parse(CondicionId.SelectedValue);

                    if (UbicacionActivoBodega.Checked == true)
                        TemporalAsignacionObjetoEntidad.UbicacionActivoId = (Int16)ConstantePrograma.UbicacionActivo.Bodega;
                    else
                        TemporalAsignacionObjetoEntidad.UbicacionActivoId = (Int16)ConstantePrograma.UbicacionActivo.Piso;

                    AgregarActivo(TemporalAsignacionObjetoEntidad);
                }

            }

            protected void AgregarActivo(TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalAsignacionDetalleProceso TemporalAsignacionDetalleProcesoNegocio = new TemporalAsignacionDetalleProceso();

                Resultado = TemporalAsignacionDetalleProcesoNegocio.AgregarActivo(TemporalAsignacionObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAsignacion.TemporalAsignacionDetalleGuardadoCorrectamente)
                {
                    LimpiarActivo();
                    CambiarBotonesNuevo();
                    TemporalAsignacionIdHidden.Value = Resultado.NuevoRegistroId.ToString();
                    SeleccionarAsignacion();
                    ActivoVehiculoIdHidden.Value = TemporalAsignacionObjetoEntidad.ActivoId.ToString();

                    if (TipoActivoIdHidden.Value == "0")
                        TipoActivoIdHidden.Value = TipoActivoIdSeleccionadoHidden.Value;
                }
                else
                {
                    AgregarEtiquetaMensaje.Text = Resultado.DescripcionError;
                }
            }

            protected void CambiarBotonesActualizar()
            {
                BotonAgregarActivo.Visible = false;
                BotonActualizarActivo.Visible = true;
                BotonCancelarActualizar.Visible = true;
            }

            protected void CambiarBotonesNuevo()
            {
                BotonAgregarActivo.Visible = true;
                BotonActualizarActivo.Visible = false;
                BotonCancelarActualizar.Visible = false;
            }

            protected void EliminarActivo(int TemporalAsignacionDetalleId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad = new TemporalAsignacionEntidad();
                TemporalAsignacionDetalleProceso TemporalAsignacionDetalleProcesoNegocio = new TemporalAsignacionDetalleProceso();

                TemporalAsignacionObjetoEntidad.TemporalAsignacionDetalleId = TemporalAsignacionDetalleId;

                Resultado = TemporalAsignacionDetalleProcesoNegocio.EliminarTemporalAsignacionDetalle(TemporalAsignacionObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAsignacion.TemporalAsignacionDetalleEliminadoCorrectamente)
                {
                    SeleccionarAsignacion();

                    if (TemporalAsignacionDetalleIdHidden.Value == TemporalAsignacionDetalleId.ToString())
                    {
                        LimpiarActivo();
                        CambiarBotonesNuevo();
                    }
                }
                else
                {
                    AgregarEtiquetaMensaje.Text = Resultado.DescripcionError;
                }
            }

            protected void GuardarAsignacion()
            {
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                if (TemporalAsignacionIdHidden.Value != "0")
                {
                    UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                    ActivoObjetoEntidad.UsuarioId = UsuarioSessionEntidad.UsuarioId;
                    ActivoObjetoEntidad.EmpleadoId = Int16.Parse(EmpleadoId.SelectedValue);
                    ActivoObjetoEntidad.TemporalAsignacionId = int.Parse(TemporalAsignacionIdHidden.Value);
                    ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;

                    GuardarAsignacion(ActivoObjetoEntidad);
                }
                else
                {
                    EtiquetaMensajeError.Text = "Favor de agregar los activos";
                }

            }

            protected void GuardarAsignacion(ActivoEntidad ActivoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MovimientoProceso MovimientoProcesoNegocio = new MovimientoProceso();

                Resultado = MovimientoProcesoNegocio.InsertarMovimientoAsignacionDesdeTemporal(ActivoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Movimiento.MovimientoAsignacionGuardadoCorrectamente)
                {
                    MostrarBotonesImprimir();
                    LimpiarFormulario();
                    EtiquetaMensaje.Text = "Asignación guardada correctamente";
                }
                else
                {
                    EtiquetaMensajeError.Text = Resultado.DescripcionError;
                }
            }

            protected void MostrarBotonesImprimir()
            {
                EmpIdHidden.Value = EmpleadoId.SelectedValue;

                //Dependiendo del tipo de activo de la asignación son los botones que se van a mostrar

                if (TipoActivoIdHidden.Value == ((Int16)ConstantePrograma.TipoAtivo.Vehiculo).ToString())
                {
                    BotonImprimirVehiculo.Visible = true;
                    BotonImprimirVehiculoReverso.Visible = true;
                    BotonImprimir.Visible = false;
                   
                    ImprimirDocumentoVehiculo();
                }
                else
                {
                    BotonImprimir.Visible = true;
                    BotonImprimirVehiculo.Visible = false;
                    BotonImprimirVehiculoReverso.Visible = false;

                    TempAsigIdHidden.Value = TemporalAsignacionIdHidden.Value;
                    ImprimirDocumento();
                }

            }

            protected void OcultarBotonesImprimir()
            {
                BotonImprimir.Visible = false;
                BotonImprimirVehiculo.Visible = false;
                BotonImprimirVehiculoReverso.Visible = false;
            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.AsignacionActivos);

                    SeleccionarEmpleado();
                    SeleccionarCondicion();
                    SeleccionarTextoError();

                    TablaActivo.DataSource = null;
                    TablaActivo.DataBind();

                }
            }

            protected void ImprimirDocumento()
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirDocumento('" + EmpIdHidden.Value + "','" + TempAsigIdHidden.Value + "')", true);
            }

            protected void ImprimirDocumentoVehiculo()
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirDocumentoVehiculo('" + EmpIdHidden.Value + "','" + ActivoVehiculoIdHidden.Value + "')", true);
            }

            protected void ImprimirDocumentoVehiculoReverso()
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirDocumentoVehiculoReverso()", true);
            }

            protected void LimpiarActivo()
            {
                CodigoBarrasParticular.Text = "";
                CodigoBarrasParticular.Enabled = true;
                DescripcionActivo.Text = "";
                NumeroSerie.Text = "";
                Modelo.Text = "";
                Color.Text = "";
                Monto.Text = "";
                CondicionId.SelectedIndex = 0;
                UbicacionActivoBodega.Checked = false;
                ActivoIdHidden.Value = "0";
                TemporalAsignacionDetalleIdHidden.Value = "0";
                AgregarEtiquetaMensaje.Text = "";

                EtiquetaMensajeError.Text = "";
                EtiquetaMensaje.Text = "";
            }

            protected void LimpiarFormulario()
            {
                EmpleadoId.SelectedIndex = 0;
                TemporalAsignacionIdHidden.Value = "0";
                TipoActivoIdHidden.Value = "0";
                CantActivosAgregadosHidden.Value = "0";

                TablaActivo.DataSource = null;
                TablaActivo.DataBind();
                LimpiarActivo();
            }

            protected void LimpiarTemporalTablaAsignacion()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad = new TemporalAsignacionEntidad();
                TemporalAsignacionProceso TemporalAsignacionProcesoNegocio = new TemporalAsignacionProceso();

                if (TemporalAsignacionIdHidden.Value != "0")
                {
                    TemporalAsignacionObjetoEntidad.TemporalAsignacionId = int.Parse(TemporalAsignacionIdHidden.Value);

                    Resultado = TemporalAsignacionProcesoNegocio.LimpiarTemporalTablaAsignacion(TemporalAsignacionObjetoEntidad);

                    if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAsignacion.TemporalAsignacionEliminadoCorrectamente)
                    {
                        //Do nothing
                    }
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

            protected void SeleccionarActivo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
                bool AsignacionPermitida = true;

                ActivoEntidadObjeto.CodigoBarrasParticular = CodigoBarrasParticular.Text.Trim();

                Resultado = ActivoProcesoObjeto.SeleccionarActivo(ActivoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        //Ahora se valida que el usuario tenga permisos para asignar ese activo
                        if (ValidarPermisos(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString())) == true)
                        {
                            //A continuación se valida que el nuevo activo a agregar sea del mismo tipo que los demas
                            if (ValidarTipoActivo(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString())) == true)
                            {
                                //Ahora se valida que si el tipo de activo es de vehículo, que sea el unico que pueden agregar
                                if (ValidarActivoVehiculo(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString())) == true)
                                {
                                    //Se valida que se pueda asignar el Activo
                                    AsignacionPermitida = ActivoProcesoObjeto.ValidarAsignacionActivo(int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString()));

                                    if (AsignacionPermitida == true)
                                    {
                                        DescripcionActivo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                                        NumeroSerie.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroSerie"].ToString();
                                        Modelo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Modelo"].ToString();
                                        Color.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Color"].ToString();
                                        Monto.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Monto"].ToString();
                                        ActivoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();

                                        TipoActivoIdSeleccionadoHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString();

                                        AgregarEtiquetaMensaje.Text = "";
                                    }
                                    else
                                    {
                                        LimpiarActivo();
                                        AgregarEtiquetaMensaje.Text = TextoError.EstatusActivoIncorrecto;
                                        CodigoBarrasParticular.Focus();
                                    }
                                }
                                else
                                {
                                    LimpiarActivo();
                                    AgregarEtiquetaMensaje.Text = TextoError.AsignacionActivoVehiculo;
                                    CodigoBarrasParticular.Focus();
                                }
                                
                            }
                            else
                            {
                                LimpiarActivo();
                                AgregarEtiquetaMensaje.Text = TextoError.AsignacionTipoActivo;
                                CodigoBarrasParticular.Focus();
                            }
                            
                        }
                        else
                        {
                            LimpiarActivo();
                            AgregarEtiquetaMensaje.Text = TextoError.PermisoDenegado;
                            CodigoBarrasParticular.Focus();
                        }
                        
                    }
                    else
                    {
                        LimpiarActivo();
                        AgregarEtiquetaMensaje.Text = TextoError.NoExisteActivo;
                        CodigoBarrasParticular.Focus();
                    }
                }
                else
                {
                    LimpiarActivo();
                    AgregarEtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected bool ValidarPermisos(Int16 TipoActivoId)
            {
                bool TienePermiso = false;
                Int16 UsuarioTipoActivoId = 0;

                UsuarioTipoActivoId = ObtenerTipoActivoId();

                if (UsuarioTipoActivoId != 0)
                {
                    if (UsuarioTipoActivoId == TipoActivoId)
                        TienePermiso = true;
                }
                else
                {
                    TienePermiso = true;
                }

                return TienePermiso;
            }

            protected bool ValidarTipoActivo(Int16 TipoActivoId)
            {
                bool PermitirAgregar = false;

                if (TipoActivoIdHidden.Value != "0")
                {
                    if (TipoActivoId == Int16.Parse(TipoActivoIdHidden.Value))
                        PermitirAgregar = true;
                }
                else
                {
                    PermitirAgregar = true;
                }

                return PermitirAgregar;
            }

            protected bool ValidarActivoVehiculo(Int16 TipoActivoId)
            {
                bool PermitirAgregar = false;

                if (TipoActivoId == (Int16)ConstantePrograma.TipoAtivo.Vehiculo)
                {
                    if (CantActivosAgregadosHidden.Value == "0")
                        PermitirAgregar = true;
                }
                else
                {
                    PermitirAgregar = true;
                }

                return PermitirAgregar;
            }

            protected void SeleccionarActivoParaEditar(int TemporalAsignacionDetalleId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalAsignacionEntidad TemporalAsignacionEntidadObjeto = new TemporalAsignacionEntidad();
                TemporalAsignacionDetalleProceso TemporalAsignacionDetalleProcesoObjeto = new TemporalAsignacionDetalleProceso();

                TemporalAsignacionEntidadObjeto.TemporalAsignacionDetalleId = TemporalAsignacionDetalleId;

                Resultado = TemporalAsignacionDetalleProcesoObjeto.SeleccionarTemporalAsignacionDetalle(TemporalAsignacionEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                    {
                        CodigoBarrasParticular.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoBarrasParticular"].ToString();
                        DescripcionActivo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                        NumeroSerie.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroSerie"].ToString();
                        Modelo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Modelo"].ToString();
                        Color.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Color"].ToString();
                        Monto.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Monto"].ToString();
                        ActivoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();
                        TemporalAsignacionDetalleIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["TemporalAsignacionDetalleId"].ToString();
                        CondicionId.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["CondicionId"].ToString();

                        switch (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TemporalUbicacionActivoId"].ToString()))
                        {
                            case (Int16)ConstantePrograma.UbicacionActivo.Bodega:
                                UbicacionActivoBodega.Checked = true;
                                break;

                            default:
                                UbicacionActivoBodega.Checked = false;
                                break;
                        }

                        CodigoBarrasParticular.Enabled = false;
                        AgregarEtiquetaMensaje.Text = "";
                        CambiarBotonesActualizar();
                    }
                }
                else
                {
                    LimpiarActivo();
                    AgregarEtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarAsignacion()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad = new TemporalAsignacionEntidad();
                TemporalAsignacionDetalleProceso TemporalAsignacionDetalleProcesoNegocio = new TemporalAsignacionDetalleProceso();

                ActivoObjetoEntidad.EmpleadoId = Int16.Parse(EmpleadoId.SelectedValue);
                ActivoObjetoEntidad.TipoActivoId = ObtenerTipoActivoId();

                TemporalAsignacionObjetoEntidad.TemporalAsignacionId = int.Parse(TemporalAsignacionIdHidden.Value);

                Resultado = TemporalAsignacionDetalleProcesoNegocio.SeleccionarAsignacion(ActivoObjetoEntidad, TemporalAsignacionObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    CantActivosAgregadosHidden.Value = Resultado.NuevoRegistroId.ToString();

                    if (Resultado.NuevoRegistroId == 0)
                        TipoActivoIdHidden.Value = "0";

                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaActivo.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaActivo.CssClass = ConstantePrograma.ClaseTabla;

                    TablaActivo.DataSource = Resultado.ResultadoDatos;
                    TablaActivo.DataBind();
                }
                else
                {
                    AgregarEtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
                OcultarBotonesImprimir();
            }

            protected void SeleccionarCondicion()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                CondicionEntidad CondicionEntidadObjeto = new CondicionEntidad();
                CondicionProceso CondicionProcesoObjeto = new CondicionProceso();

                Resultado = CondicionProcesoObjeto.SeleccionarCondicion(CondicionEntidadObjeto);

                CondicionId.DataValueField = "CondicionId";
                CondicionId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    CondicionId.DataSource = Resultado.ResultadoDatos;
                    CondicionId.DataBind();
                }
                else
                {
                    AgregarEtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                CondicionId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarEmpleado()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

                EmpleadoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusEmpleados.Activo;

                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                EmpleadoId.DataValueField = "EmpleadoId";
                EmpleadoId.DataTextField = "NombreEmpleadoCompleto";

                if (Resultado.ErrorId == 0)
                {
                    EmpleadoId.DataSource = Resultado.ResultadoDatos;
                    EmpleadoId.DataBind();
                }
                else
                {
                    AgregarEtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                EmpleadoId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarTextoError()
            {
                CodigoBarrasRequerido.ErrorMessage = TextoError.AsignacionCodigoBarrasParticular + "<br />";
                BuscarCodigoBarrasRequerido.ErrorMessage = TextoError.AsignacionCodigoBarrasParticular + "<br />";
                CondicionRequerido.ErrorMessage = TextoError.AsignacionCondicion + "<br />";
                EmpleadoRequerido.ErrorMessage = TextoError.AsignacionEmpleado + "<br />";
            }

            protected void TablaActivoEventoComando(GridViewCommandEventArgs e)
            {
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                int TemporalAsignacionDetalleId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaActivo.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaActivo.PageIndex));

                switch (strCommand)
                {
                    case "Select":
                        TemporalAsignacionDetalleId = int.Parse(TablaActivo.DataKeys[intFila]["TemporalAsignacionDetalleId"].ToString());

                        if (TemporalAsignacionDetalleId != 0)
                        {
                            SeleccionarActivoParaEditar(TemporalAsignacionDetalleId);
                            CambiarBotonesActualizar();
                        }
                        break;

                    case "EliminarActivo":
                        TemporalAsignacionDetalleId = int.Parse(TablaActivo.DataKeys[intFila]["TemporalAsignacionDetalleId"].ToString());
                        EliminarActivo(TemporalAsignacionDetalleId);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

        #endregion
    }
}
