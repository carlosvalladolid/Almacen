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
    public partial class TransferenciaActivo : System.Web.UI.Page
    {
        #region "Eventos"

        public DataTable DTCondiciones;

            protected void BotonBuscarEmpleadoDestino_Click(object sender, EventArgs e)
            {
                TipoEmpleadoBusquedaHidden.Value = "Destino";
                ControlBuscarEmpleado.InicioControl((Int16)ConstantePrograma.TipoBusquedaEmpleado.Empleado);
            }

            protected void BotonBuscarEmpleadoOrigen_Click(object sender, EventArgs e)
            {
                TipoEmpleadoBusquedaHidden.Value = "Origen";
                ControlBuscarEmpleado.InicioControl((Int16)ConstantePrograma.TipoBusquedaEmpleado.Empleado);
            }

            protected void BotonCancelar_Click(object sender, EventArgs e)
            {
                LimpiarFormulario();
            }

            protected void BotonGuardar_Click(object sender, EventArgs e)
            {
                GuardarTransferencia();
            }

            protected void BotonImprimir_Click(object sender, EventArgs e)
            {
                ImprimirDocumento();
            }

            protected void CodigoBarrasImagen_Click(object sender, EventArgs e)
            {
                SeleccionarCodigoBarras();
            }

            protected void LinkBuscarEmpleadoDestino_Click(object sender, EventArgs e)
            {
                BuscarEmpleadoDestinoPorNumero();
            }

            protected void LinkBuscarEmpleadoOrigen_Click(object sender, EventArgs e)
            {
                BuscarEmpleadoOrigenPorNumero();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

            protected void TablaActivo_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList CondicionId = (DropDownList)e.Row.FindControl("CondicionId");
                    CheckBox chkUbicacion = (CheckBox)e.Row.FindControl("chkUbicacion");

                    CondicionId.DataValueField = "CondicionId";
                    CondicionId.DataTextField = "Nombre";

                    CondicionId.DataSource = DTCondiciones;
                    CondicionId.DataBind();

                    CondicionId.SelectedValue = TablaActivo.DataKeys[e.Row.RowIndex]["CondicionId"].ToString();

                    switch (Int16.Parse(TablaActivo.DataKeys[e.Row.RowIndex]["UbicacionActivoId"].ToString()))
                    {
                        case (Int16)ConstantePrograma.UbicacionActivo.Bodega:
                            chkUbicacion.Checked = true;
                            break;

                        default:
                            chkUbicacion.Checked = false;
                            break;
                    }

                }
            }

            protected void NumeroEmpleadoDestino_TextChanged(object sender, EventArgs e)
            {
                NumeroEmpleadoDestinoTextoModificado();
            }

            protected void NumeroEmpleadoOrigen_TextChanged(object sender, EventArgs e)
            {
                NumeroEmpleadoOrigenTextoModificado();
            }

        #endregion

        #region "Métodos"

            public void BuscarEmpleadoPorId(Int16 EmpleadoId)
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoId;

                switch (TipoEmpleadoBusquedaHidden.Value)
                {
                    case "Origen":
                        BuscarEmpleadoOrigen(EmpleadoEntidadObjeto);
                        break;

                    case "Destino":
                        BuscarEmpleadoDestino(EmpleadoEntidadObjeto);
                        break;

                    default:
                        // Do nothing
                        break;
                }

                BotonImprimir.Enabled = false;
                ActualizarTablaTransferencia.Update();
            }

            protected void BuscarEmpleadoDestinoPorNumero()
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.NumeroEmpleado = NumeroEmpleadoDestino.Text.Trim();

                BuscarEmpleadoDestino(EmpleadoEntidadObjeto);

                BotonImprimir.Enabled = false;
            }

            protected void BuscarEmpleadoOrigenPorNumero()
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.NumeroEmpleado = NumeroEmpleadoOrigen.Text.Trim();

                BuscarEmpleadoOrigen(EmpleadoEntidadObjeto);

                BotonImprimir.Enabled = false;
            }

            protected void BuscarEmpleadoDestino(EmpleadoEntidad EmpleadoEntidadObjeto)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString()) != (Int16)ConstantePrograma.EstatusEmpleados.Activo)
                        {
                            EmpleadoDestinoIdHidden.Value = "0";
                            NombreEmpleadoDestino.Text = "";
                            EtiquetaMensaje.Text = TextoError.EmpleadoInactivo;
                            NumeroEmpleadoDestino.Focus();
                        }
                        else
                        {
                            NombreEmpleadoDestino.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                            EmpleadoDestinoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                            NumeroEmpleadoDestino.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                            EtiquetaMensaje.Text = "";
                        }
                    }
                    else
                    {
                        EmpleadoDestinoIdHidden.Value = "0";
                        NombreEmpleadoDestino.Text = "";
                        EtiquetaMensaje.Text = TextoError.EmpleadoNoEncontrado;
                        NumeroEmpleadoDestino.Focus();
                    }
                }
                else
                {
                    EmpleadoDestinoIdHidden.Value = "0";
                    NombreEmpleadoDestino.Text = "";
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void BuscarEmpleadoOrigen(EmpleadoEntidad EmpleadoEntidadObjeto)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString()) != (Int16)ConstantePrograma.EstatusEmpleados.Activo)
                        {
                            EmpleadoOrigenIdHidden.Value = "0";
                            NombreEmpleadoOrigen.Text = "";
                            EtiquetaMensaje.Text = TextoError.EmpleadoInactivo;
                            TablaActivo.DataSource = null;
                            TablaActivo.DataBind();
                            NumeroEmpleadoOrigen.Focus();
                        }
                        else
                        {
                            NombreEmpleadoOrigen.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                            EmpleadoOrigenIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                            NumeroEmpleadoOrigen.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                            SeleccionarAsignacion(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString()));
                            EtiquetaMensaje.Text = "";
                        }
                    }
                    else
                    {
                        EmpleadoOrigenIdHidden.Value = "0";
                        NombreEmpleadoOrigen.Text = "";
                        EtiquetaMensaje.Text = TextoError.EmpleadoNoEncontrado;
                        TablaActivo.DataSource = null;
                        TablaActivo.DataBind();
                        NumeroEmpleadoOrigen.Focus();
                    }
                }
                else
                {
                    EmpleadoOrigenIdHidden.Value = "0";
                    NombreEmpleadoOrigen.Text = "";
                    TablaActivo.DataSource = null;
                    TablaActivo.DataBind();
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void GuardarTransferencia()
            {
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                DataTable ActivosSeleccionados = new DataTable();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                ActivosSeleccionados = ObtenerActivosSeleccionados();

                if (ActivosSeleccionados.Rows.Count > 0)
                {
                    if (EmpleadoOrigenIdHidden.Value != "0")
                    {
                        if (EmpleadoDestinoIdHidden.Value != "0")
                        {
                            if (EmpleadoOrigenIdHidden.Value != EmpleadoDestinoIdHidden.Value)
                            {
                                GuardarTransferencia(ActivosSeleccionados, Int16.Parse(EmpleadoOrigenIdHidden.Value), Int16.Parse(EmpleadoDestinoIdHidden.Value), UsuarioSessionEntidad.UsuarioId, UsuarioSessionEntidad.SesionId);
                            }
                            else
                            {
                                EtiquetaMensajeError.Text = "No puede ser el mismo empleado origen y destino.";
                            }
                        }
                        else
                        {
                            EtiquetaMensajeError.Text = "Favor de seleccionar el empleado destino.";
                        }
                    }
                    else
                    {
                        EtiquetaMensajeError.Text = "Favor de seleccionar el empleado de origen.";
                    }
                }
                else
                {
                    EtiquetaMensajeError.Text = "Favor de seleccionar los activos a transferir.";
                }

            }

            protected void GuardarTransferencia(DataTable ActivosSeleccionados, Int16 EmpleadoOrigenID, Int16 EmpleadoDestinoID, Int16 UsuarioID, string SesionId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MovimientoProceso MovimientoProcesoNegocio = new MovimientoProceso();

                Resultado = MovimientoProcesoNegocio.TransferirActivos(ActivosSeleccionados, EmpleadoOrigenID, EmpleadoDestinoID, UsuarioID, SesionId);

                if (Resultado.ErrorId == (int)ConstantePrograma.Movimiento.MovimientoAsignacionGuardadoCorrectamente)
                {
                    EmpOrigIdHidden.Value = EmpleadoOrigenIdHidden.Value;
                    EmpDestIdHidden.Value = EmpleadoDestinoIdHidden.Value;
                    LimpiarFormulario();
                    ImprimirDocumento();
                    BotonImprimir.Enabled = true;
                    EtiquetaMensaje.Text = "Transferencia de activos guardada correctamente";
                }
                else
                {
                    EtiquetaMensajeError.Text = Resultado.DescripcionError;
                }
            }

            protected DataTable ObtenerActivosSeleccionados()
            {
                DataTable ActivosSeleccionados = new DataTable();
                DataRow Registro;
                DropDownList ddlCondicion;
                CheckBox chkUbicacion;
                CheckBox chkTransferir;
                int ActivoID = 0;

                ActivosSeleccionados.Columns.Add("ActivoId");
                ActivosSeleccionados.Columns.Add("CondicionId");
                ActivosSeleccionados.Columns.Add("UbicacionActivoId");

                foreach (GridViewRow FilaGrid in TablaActivo.Rows)
                {
                    ddlCondicion = (DropDownList)FilaGrid.FindControl("CondicionId");
                    chkUbicacion = (CheckBox)FilaGrid.FindControl("chkUbicacion");
                    chkTransferir = (CheckBox)FilaGrid.FindControl("SeleccionarTransferir");
                    ActivoID = int.Parse(TablaActivo.DataKeys[FilaGrid.RowIndex]["ActivoId"].ToString());

                    if (chkTransferir.Checked == true)
                    {
                        Registro = ActivosSeleccionados.NewRow();
                        Registro["ActivoId"] = ActivoID;
                        Registro["CondicionId"] = ddlCondicion.SelectedValue;

                        if (chkUbicacion.Checked == true)
                            Registro["UbicacionActivoId"] = (Int16)ConstantePrograma.UbicacionActivo.Bodega;
                        else
                            Registro["UbicacionActivoId"] = (Int16)ConstantePrograma.UbicacionActivo.Piso;

                        ActivosSeleccionados.Rows.Add(Registro);
                    }
                }

                ActivosSeleccionados.AcceptChanges();

                return ActivosSeleccionados;

            }

            protected void SeleccionarCodigoBarras()
            {
                CheckBox chkTransferir;
                bool ExisteActivo = false;

                foreach (GridViewRow FilaGrid in TablaActivo.Rows)
                {
                    if (TablaActivo.DataKeys[FilaGrid.RowIndex]["CodigoBarrasParticular"].ToString() == CodigoBarrasParticular.Text.Trim())
                    {
                        chkTransferir = (CheckBox)FilaGrid.FindControl("SeleccionarTransferir");

                        chkTransferir.Checked = true;
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

            protected void LimpiarFormulario()
            {
                TablaActivo.DataSource = null;
                TablaActivo.DataBind();

                NumeroEmpleadoOrigen.Text = "";
                NombreEmpleadoOrigen.Text = "";
                NumeroEmpleadoDestino.Text = "";
                NombreEmpleadoDestino.Text = "";
                EtiquetaMensajeError.Text = "";
                EtiquetaMensaje.Text = "";
                EmpleadoOrigenIdHidden.Value = "0";
                EmpleadoDestinoIdHidden.Value = "0";
                BotonImprimir.Enabled = false;
                ActualizarTablaTransferencia.Update();
            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.TransferenciaActivos);

                    TablaActivo.DataSource = null;
                    TablaActivo.DataBind();

                    SeleccionarTextoError();
                }
            }

            protected void ImprimirDocumento()
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirDocumento('" + EmpOrigIdHidden.Value + "','" + EmpDestIdHidden.Value + "')", true);
            }

            protected void NumeroEmpleadoDestinoTextoModificado()
            {
                if (NumeroEmpleadoDestino.Text.Trim() != "")
                {
                    BuscarEmpleadoDestinoPorNumero();
                }
                else
                {
                    EmpleadoDestinoIdHidden.Value = "0";
                    NombreEmpleadoDestino.Text = "";
                }
            }

            protected void NumeroEmpleadoOrigenTextoModificado()
            {
                if (NumeroEmpleadoOrigen.Text.Trim() != "")
                {
                    BuscarEmpleadoOrigenPorNumero();
                }
                else
                {
                    EmpleadoOrigenIdHidden.Value = "0";
                    NombreEmpleadoOrigen.Text = "";
                }
            }

            protected void SeleccionarAsignacion(Int16 EmpleadoId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();

                ActivoObjetoEntidad.EmpleadoId = EmpleadoId;
                ActivoObjetoEntidad.TipoActivoId = ObtenerTipoActivoId();
                ActivoObjetoEntidad.MostrarAsignadosSalida = 0;

                Resultado = MovimientoProcesoObjeto.SeleccionarAsignacionPorEmpleado(ActivoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaActivo.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaActivo.CssClass = ConstantePrograma.ClaseTabla;

                    SeleccionarCondicion();

                    TablaActivo.DataSource = Resultado.ResultadoDatos;
                    TablaActivo.DataBind();
                }
                else
                {
                    EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
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

            protected void SeleccionarCondicion()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                CondicionEntidad CondicionEntidadObjeto = new CondicionEntidad();
                CondicionProceso CondicionProcesoObjeto = new CondicionProceso();
                DTCondiciones = new DataTable();

                Resultado = CondicionProcesoObjeto.SeleccionarCondicion(CondicionEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    DTCondiciones = Resultado.ResultadoDatos.Tables[0];
                    int filas = DTCondiciones.Rows.Count;
                }
            }

            protected void SeleccionarTextoError()
            {
                BuscarCodigoBarrasRequerido.ErrorMessage = TextoError.AsignacionCodigoBarrasParticular + "<br />";
            }

        #endregion
    }
}
