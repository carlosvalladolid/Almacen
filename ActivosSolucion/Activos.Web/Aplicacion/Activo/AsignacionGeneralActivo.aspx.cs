using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

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
    public partial class AsignacionGeneralActivo : System.Web.UI.Page
    {
        #region "Eventos"

        public DataTable DTCondiciones;

            protected void BotonBuscarEmpleado_Click(object sender, EventArgs e)
            {
                ControlBuscarEmpleado.InicioControl((Int16)ConstantePrograma.TipoBusquedaEmpleado.Empleado);
            }

            protected void BotonCancelar_Click(object sender, EventArgs e)
            {
                LimpiarFormulario();
            }

            protected void BotonGuardar_Click(object sender, EventArgs e)
            {
                GuardarAsignacion();
            }

            protected void BotonImprimir_Click(object sender, EventArgs e)
            {
                ImprimirDocumento();
            }

            protected void BotonImprimirVehiculoReverso_Click(object sender, EventArgs e)
            {
                ImprimirDocumentoVehiculoReverso();
            }

            protected void LinkBuscarDocumento_Click(object sender, EventArgs e)
            {
                BuscarDocumento();
            }

            protected void LinkBuscarEmpleado_Click(object sender, EventArgs e)
            {
                BuscarEmpleadoPorNumero();
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

            protected void CompraFolio_TextChanged(object sender, EventArgs e)
            {
                CompraFolioTextoModificado();
            }

            protected void NumeroEmpleado_TextChanged(object sender, EventArgs e)
            {
                NumeroEmpleadoTextoModificado();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

        #endregion

        #region "Métodos"

            protected void BloquearDocumento()
            {
                ProveedorId.Enabled = false;
                TipoDocumentoId.Enabled = false;
                CompraFolio.Enabled = false;
            }

            protected void DesbloquearDocumento()
            {
                ProveedorId.Enabled = true;
                TipoDocumentoId.Enabled = true;
                CompraFolio.Enabled = true;
            }

            protected void BuscarDocumento()
            {
                CompraEntidad CompraObjetoEntidad = new CompraEntidad();

                CompraObjetoEntidad.ProveedorId = Int16.Parse(ProveedorId.SelectedValue);
                CompraObjetoEntidad.TipoDocumentoId = Int16.Parse(TipoDocumentoId.SelectedValue);
                CompraObjetoEntidad.CompraFolio = CompraFolio.Text.Trim();

                BuscarDocumento(CompraObjetoEntidad);
                BotonImprimir.Enabled = false;
            }

            protected void BuscarDocumento(CompraEntidad CompraObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                CompraProceso CompraProcesoNegocio = new CompraProceso();

                Resultado = CompraProcesoNegocio.SeleccionarCompra(CompraObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        CompraIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["CompraId"].ToString();
                        ValidarDocumento();
                    }
                    else
                    {
                        CompraIdHidden.Value = "0";
                        EtiquetaMensajeError.Text = "No existe ese documento";
                        TablaActivo.DataSource = null;
                        TablaActivo.DataBind();
                        DesbloquearDocumento();
                    }
                }
                else
                {
                    EtiquetaMensajeError.Text = Resultado.DescripcionError;
                }
            }

            protected void ValidarDocumento()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MovimientoProceso MovimientoProcesoNegocio = new MovimientoProceso();
                AccesorioProceso AccesorioProcesoNegocio = new AccesorioProceso();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                AccesorioEntidad AccesorioObjetoEntidad = new AccesorioEntidad();

                if (CompraIdHidden.Value != "0")
                {
                    ActivoObjetoEntidad.CompraId = int.Parse(CompraIdHidden.Value);
                    ActivoObjetoEntidad.TipoDeMovimiento = (int)ConstantePrograma.TipoMovimiento.Asignacion;

                    Resultado = MovimientoProcesoNegocio.SeleccionarMovimientoPorDocumento(ActivoObjetoEntidad);

                    //Validamos que ninguno de los activos del documento este asignado a un empleado
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    {
                        //Ahora se valida que ninguno de los activos del documento este asignado a otro activo
                        AccesorioObjetoEntidad.CompraId = int.Parse(CompraIdHidden.Value);
                        Resultado = AccesorioProcesoNegocio.SeleccionarAccesorioPorDocumento(AccesorioObjetoEntidad);

                        if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        {
                            SeleccionarActivos();
                        }
                        else
                        {
                            EtiquetaMensajeError.Text = TextoError.DocumentoProcesado;
                            CompraIdHidden.Value = "0";
                            TipoActivoIdHidden.Value = "0";
                            TablaActivo.DataSource = null;
                            TablaActivo.DataBind();
                            DesbloquearDocumento();
                        }
                    }
                    else
                    {
                        EtiquetaMensajeError.Text = TextoError.DocumentoProcesado;
                        CompraIdHidden.Value = "0";
                        TipoActivoIdHidden.Value = "0";
                        TablaActivo.DataSource = null;
                        TablaActivo.DataBind();
                        DesbloquearDocumento();
                    }
                }

            }

            protected void SeleccionarActivos()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoProceso ActivoProcesoNegocio = new ActivoProceso();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();

                if (CompraIdHidden.Value != "0")
                {
                    ActivoObjetoEntidad.CompraId = int.Parse(CompraIdHidden.Value);
                    ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Alta;

                    Resultado = ActivoProcesoNegocio.SeleccionarActivoPorDocumento(ActivoObjetoEntidad);

                    TablaActivo.CssClass = ConstantePrograma.ClaseTablaVacia;

                    if (Resultado.ErrorId == 0)
                    {
                        if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        {
                            TablaActivo.DataSource = null;
                            TablaActivo.DataBind();
                        }
                        else
                        {
                            //Ahora se valida que todos los activos tengan codigo de barras particular
                            if (Resultado.ResultadoDatos.Tables[0].Select("CodigoBarrasParticular = ''").Count() > 0)
                            {
                                EtiquetaMensajeError.Text = TextoError.DocumentoNoEtiquetado;
                                CompraIdHidden.Value = "0";
                                TipoActivoIdHidden.Value = "0";
                                TablaActivo.DataSource = null;
                                TablaActivo.DataBind();
                                DesbloquearDocumento();
                            }
                            else
                            {
                                //Se valida que todos los activos del documento sean del mismo tipo y que si es de vehiculo, solo haya uno
                                if (ValidarActivosDocumento(Resultado.ResultadoDatos.Tables[0]) == true)
                                {
                                    BloquearDocumento();
                                    TablaActivo.CssClass = ConstantePrograma.ClaseTabla;
                                    SeleccionarCondicion();
                                    TablaActivo.DataSource = Resultado.ResultadoDatos;
                                    TablaActivo.DataBind();
                                    EtiquetaMensajeError.Text = "";
                                }
                                else
                                {
                                    CompraIdHidden.Value = "0";
                                    TipoActivoIdHidden.Value = "0";
                                    TablaActivo.DataSource = null;
                                    TablaActivo.DataBind();
                                    DesbloquearDocumento();
                                }
                                
                            }
                            
                        }
                        
                    }
                    else
                    {
                        EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
                    }
                }

            }

            public bool ValidarActivosDocumento(DataTable Activos)
            {
                bool DocumentoValidado = true;
                Int16 TipoActivoId = 0;

                if (Activos.Rows.Count > 1)
                {
                    //Se valida que todos los activos del Documento sean del mismo tipo
                    foreach (DataRow dtRegistro in Activos.Rows)
                    {
                        if (TipoActivoId == 0)
                            TipoActivoId = Int16.Parse(dtRegistro["TipoActivoId"].ToString());

                        if (TipoActivoId != Int16.Parse(dtRegistro["TipoActivoId"].ToString()))
                        {
                            DocumentoValidado = false;
                            EtiquetaMensajeError.Text = TextoError.DocumentoConTiposActivosDiferentes;
                            break;
                        }
                    }

                    if (TipoActivoId == (Int16)ConstantePrograma.TipoAtivo.Vehiculo)
                    {
                        DocumentoValidado = false;
                        EtiquetaMensajeError.Text = TextoError.DocumentoConVariosVehiculos;
                    }
                }

                if (DocumentoValidado == true)
                    TipoActivoIdHidden.Value = Activos.Rows[0]["TipoActivoId"].ToString();

                return DocumentoValidado;
            }

            public void BuscarEmpleadoPorId(Int16 EmpleadoId)
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoId;

                BuscarEmpleado(EmpleadoEntidadObjeto);

                ActualizarTablaAsignacion.Update();
            }

            protected void BuscarEmpleadoPorNumero()
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.NumeroEmpleado = NumeroEmpleado.Text.Trim();

                BuscarEmpleado(EmpleadoEntidadObjeto);
            }

            protected void BuscarEmpleado(EmpleadoEntidad EmpleadoEntidadObjeto)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

                BotonImprimir.Enabled = false;
                BotonImprimirVehiculoReverso.Visible = false;

                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString()) != (Int16)ConstantePrograma.EstatusEmpleados.Activo)
                        {
                            EmpleadoIdHidden.Value = "0";
                            NombreEmpleado.Text = "";
                            EtiquetaMensaje.Text = TextoError.EmpleadoInactivo;
                            NumeroEmpleado.Focus();
                        }
                        else
                        {
                            NombreEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                            EmpleadoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                            NumeroEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                            EtiquetaMensaje.Text = "";
                        }
                    }
                    else
                    {
                        EmpleadoIdHidden.Value = "0";
                        NombreEmpleado.Text = "";
                        EtiquetaMensaje.Text = TextoError.EmpleadoNoEncontrado;
                        NumeroEmpleado.Focus();
                    }
                }
                else
                {
                    EmpleadoIdHidden.Value = "0";
                    NombreEmpleado.Text = "";
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void GuardarAsignacion()
            {
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                if (EmpleadoIdHidden.Value != "0")
                {
                    if (CompraIdHidden.Value != "0")
                    {
                        UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                        ActivoObjetoEntidad.UsuarioId = UsuarioSessionEntidad.UsuarioId;
                        ActivoObjetoEntidad.EmpleadoId = Int16.Parse(EmpleadoIdHidden.Value);
                        ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;
                        ActivoObjetoEntidad.ActivosXML = ObtenerCadenaXML();
                        ActivoObjetoEntidad.CompraId = int.Parse(CompraIdHidden.Value);

                        GuardarAsignacion(ActivoObjetoEntidad);
                    }
                    else
                    {
                        EtiquetaMensajeError.Text = "Favor de seleccionar el documento.";
                    }
                }
                else
                {
                    EtiquetaMensajeError.Text = TextoError.AsignacionEmpleado + "<br />";
                }
            }

            protected void GuardarAsignacion(ActivoEntidad ActivoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MovimientoProceso MovimientoProcesoNegocio = new MovimientoProceso();

                Resultado = MovimientoProcesoNegocio.GuardarAsignacionGeneral(ActivoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Movimiento.MovimientoAsignacionGuardadoCorrectamente)
                {
                    CompIdHidden.Value = CompraIdHidden.Value;
                    EmpIdHidden.Value = EmpleadoIdHidden.Value;
                    ActivoVehiculoIdHidden.Value = TablaActivo.DataKeys[0]["ActivoId"].ToString();
                    LimpiarFormulario();
                    ImprimirDocumento();
                    BotonImprimir.Enabled = true;
                    EtiquetaMensaje.Text = "Asignación guardada correctamente";
                }
                else
                {
                    EtiquetaMensajeError.Text = Resultado.DescripcionError;
                }
            }

            protected void LimpiarFormulario()
            {
                CompraIdHidden.Value = "0";
                EmpleadoIdHidden.Value = "0";
                NumeroEmpleado.Text = "";
                NombreEmpleado.Text = "";
                ProveedorId.SelectedIndex = 0;
                TipoDocumentoId.SelectedIndex = 0;
                CompraFolio.Text = "";
                BotonImprimir.Enabled = false;
                BotonImprimirVehiculoReverso.Visible = false;

                TablaActivo.DataSource = null;
                TablaActivo.DataBind();
                EtiquetaMensaje.Text = "";
                EtiquetaMensajeError.Text = "";
                DesbloquearDocumento();
            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.LevantamientoInventario);

                    SeleccionarProveedor();
                    SeleccionarTipoDocumento();

                    TablaActivo.DataSource = null;
                    TablaActivo.DataBind();
                    SeleccionarTextoError();
                }
            }

            protected void ImprimirDocumento()
            {
                if (TipoActivoIdHidden.Value == ((Int16)ConstantePrograma.TipoAtivo.Vehiculo).ToString())
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirDocumentoVehiculo('" + EmpIdHidden.Value + "','" + ActivoVehiculoIdHidden.Value + "')", true);
                    BotonImprimirVehiculoReverso.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirDocumento('" + EmpIdHidden.Value + "','" + CompIdHidden.Value + "')", true);
                    BotonImprimirVehiculoReverso.Visible = false;
                }
                
            }

            protected void ImprimirDocumentoVehiculoReverso()
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirDocumentoVehiculoReverso()", true);
            }

            protected void NumeroEmpleadoTextoModificado()
            {
                if (NumeroEmpleado.Text.Trim() != "")
                {
                    BuscarEmpleadoPorNumero();
                }
                else
                {
                    EmpleadoIdHidden.Value = "0";
                    NombreEmpleado.Text = "";
                }
            }

            protected void CompraFolioTextoModificado()
            {
                if (CompraFolio.Text.Trim() != "" & ProveedorId.SelectedValue != "0" & TipoDocumentoId.SelectedValue != "0")
                {
                    BuscarDocumento();
                }
                else
                {
                    CompraIdHidden.Value = "0";
                    TablaActivo.DataSource = null;
                    TablaActivo.DataBind();
                    TablaActivo.CssClass = ConstantePrograma.ClaseTablaVacia;
                }
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

            protected void SeleccionarProveedor()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ProveedorEntidad ProveedorEntidadObjeto = new ProveedorEntidad();
                ProveedorProceso ProveedorProcesoObjeto = new ProveedorProceso();

                Resultado = ProveedorProcesoObjeto.SeleccionarProveedor(ProveedorEntidadObjeto);

                ProveedorId.DataValueField = "ProveedorId";
                ProveedorId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    ProveedorId.DataSource = Resultado.ResultadoDatos;
                    ProveedorId.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                ProveedorId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarTipoDocumento()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TipoDocumentoEntidad TipoDocumentoEntidadObjeto = new TipoDocumentoEntidad();
                TipoDocumentoProceso TipoDocumentoProcesoObjeto = new TipoDocumentoProceso();

                Resultado = TipoDocumentoProcesoObjeto.SeleccionarTipoDocumento(TipoDocumentoEntidadObjeto);

                TipoDocumentoId.DataValueField = "TipoDocumentoId";
                TipoDocumentoId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    TipoDocumentoId.DataSource = Resultado.ResultadoDatos;
                    TipoDocumentoId.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                TipoDocumentoId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarTextoError()
            {
                CompraFolioRequerido.ErrorMessage = TextoError.EtiquetadoCompraFolio + "<br />";
                ProveedorRequerido.ErrorMessage = TextoError.EtiquetadoProveedor + "<br />";
                TipoDocumentoRequerido.ErrorMessage = TextoError.EtiquetadoTipoDocumento + "<br />";

                GuardarCompraFolioRequerido.ErrorMessage = TextoError.EtiquetadoCompraFolio + "<br />";
                BuscarProveedorRequerido.ErrorMessage = TextoError.EtiquetadoProveedor + "<br />";
                BuscarTipoDocumentoRequerido.ErrorMessage = TextoError.EtiquetadoTipoDocumento + "<br />";

                GuardarNumeroEmpleadoRequerido.ErrorMessage = TextoError.AsignacionEmpleado + "<br />";
                GuardarNombreEmpleadoRequerido.ErrorMessage = TextoError.AsignacionEmpleado + "<br />";
            }

            protected string ObtenerCadenaXML()
            {
                StringBuilder ActivosXML = new StringBuilder();
                DropDownList ddlCondicion;
                CheckBox chkUbicacion;
                string CondicionId = "0";
                string UbicacionActivoId = "0";
                ActivosXML = new StringBuilder();

                ActivosXML.Append("<row>");

                foreach (GridViewRow Registro in TablaActivo.Rows)
                {
                    ddlCondicion = (DropDownList)Registro.FindControl("CondicionId");
                    chkUbicacion = (CheckBox)Registro.FindControl("chkUbicacion");

                    CondicionId = ddlCondicion.SelectedValue;
                    if (chkUbicacion.Checked == true)
                        UbicacionActivoId = ((Int16)ConstantePrograma.UbicacionActivo.Bodega).ToString();
                    else
                        UbicacionActivoId = ((Int16)ConstantePrograma.UbicacionActivo.Piso).ToString();

                    ActivosXML.Append("<Activo ActivoId='");
                    ActivosXML.Append(TablaActivo.DataKeys[Registro.RowIndex]["ActivoId"].ToString());
                    ActivosXML.Append("' CondicionId='");
                    ActivosXML.Append(CondicionId);
                    ActivosXML.Append("' UbicacionActivoId='");
                    ActivosXML.Append(UbicacionActivoId);
                    ActivosXML.Append("'/>");
                }

                ActivosXML.Append("</row>");

                return ActivosXML.ToString();
            }
            
        #endregion
    }
}
