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
    public partial class HistorialRecepcionActivo : System.Web.UI.Page
    {

        #region "Eventos"

            public decimal SumaMonto = 0;

            protected void BotonActualizarActivo_Click(object sender, EventArgs e)
            {
                AgregarActivo();
            }

            protected void BotonAgregarAccesorio_Click(object sender, EventArgs e)
            {
                AgregarAccesorio();
            }

            protected void BotonAgregarActivo_Click(object sender, EventArgs e)
            {
                AgregarActivo();
            }

            protected void BotonBuscarEmpleado_Click(object sender, EventArgs e)
            {
                ControlBuscarEmpleado.InicioControl((Int16)ConstantePrograma.TipoBusquedaEmpleado.Empleado);
            }

            protected void BotonBuscarSolicitante_Click(object sender, EventArgs e)
            {
                ControlBuscarEmpleado.InicioControl((Int16)ConstantePrograma.TipoBusquedaEmpleado.Solicitante);
            }

            protected void BotonBuscarJefe_Click(object sender, EventArgs e)
            {
                ControlBuscarJefe.InicioControl();
            }

            protected void BotonCancelar_Click(object sender, EventArgs e)
            {
                LimpiarTemporalTabla();
                LimpiarFormulario();
            }

            protected void BotonCancelarActualizar_Click(object sender, EventArgs e)
            {
                CancelarActualizarActivo();
            }

            protected void BotonCancelarNuevo_Click(object sender, EventArgs e)
            {
                CancelarNuevoActivo();
            }

            protected void BotonGuardar_Click(object sender, EventArgs e)
            {
                if (Page.IsValid)
                {
                    GuardarRecepcion();
                }
            }

            protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarSubfamilia();
            }

            //protected void ddlSolicitante_SelectedIndexChanged(object sender, EventArgs e)
            //{
            //    BuscarJefe();
            //}

            protected void ddlTipoActivo_SelectedIndexChanged(object sender, EventArgs e)
            {
                OcultarMostrarAccesorios();
            }

            protected void LinkBuscarActivoAccesorio_Click(object sender, EventArgs e)
            {
                SeleccionarActivoAccesorio();
            }

            protected void LinkBuscarEmpleado_Click(object sender, EventArgs e)
            {
                BuscarEmpleadoPorNumero();
            }

            protected void LinkBuscarSolicitante_Click(object sender, EventArgs e)
            {
                BuscarSolicitantePorNumero();
            }

            protected void TablaAccesorio_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaAccesorioEventoComando(e);
            }

            protected void TablaActivo_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaActivoEventoComando(e);
            }

            protected void TablaActivo_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                decimal Monto = 0;
                DataRowView drFila;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    drFila = (DataRowView)e.Row.DataItem;
                    Monto = decimal.Parse(drFila["Monto"].ToString());
                    e.Row.Cells[4].Text = string.Format("{0:C}", Monto);

                    SumaMonto = (SumaMonto + Monto);
                }
            }

            protected void TipoAccesorioId_SelectedIndexChanged(object sender, EventArgs e)
            {
                CambiarTipoAccesorioID();
            }

            protected void txtFechaDocumento_Validate(object source, ServerValidateEventArgs args)
            {
                string strStartDate = string.Empty;
                DateTime dtStartDate;

                strStartDate = FechaCompra.Text.Trim();

                if (strStartDate != "")
                {
                    if (DateTime.TryParse(strStartDate, out dtStartDate))
                        args.IsValid = true;
                    else
                        args.IsValid = false;
                }
                else
                {
                    args.IsValid = false;
                }
            }

            protected void txtFechaOC_Validate(object source, ServerValidateEventArgs args)
            {
                string strEndDate = string.Empty;
                DateTime dtEndDate;

                strEndDate = FechaOC.Text.Trim();

                if (strEndDate != "")
                {
                    if (DateTime.TryParse(strEndDate, out dtEndDate))
                        args.IsValid = true;
                    else
                        args.IsValid = false;
                }
                else
                {
                    args.IsValid = false;
                }
            }

            protected void NumeroEmpleado_TextChanged(object sender, EventArgs e)
            {
                NumeroEmpleadoTextoModificado();
            }

            protected void NumeroSolicitante_TextChanged(object sender, EventArgs e)
            {
                NumeroSolicitanteTextoModificado();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

        #endregion

        #region "Metodos"

            protected void AgregarAccesorio()
            {
                TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad = new TemporalAccesorioEntidad();
                TemporalActivoEntidad TemporalActivoObjetoEntidad = new TemporalActivoEntidad();

                InsertarTemporalCompra();

                if (TemporalCompraIdHidden.Value != "0")
                {
                    TemporalAccesorioObjetoEntidad.TemporalActivoId = int.Parse(TemporalActivoIdHidden.Value);
                    TemporalAccesorioObjetoEntidad.TipoAccesorioId = Int16.Parse(TipoAccesorioId.SelectedValue);
                    //TemporalAccesorioObjetoEntidad.Descripcion = DescripcionAccesorio.Text.Trim();
                    //TemporalAccesorioObjetoEntidad.ActivoAccesorioId = Int16.Parse(ActivoAccesorioIdHidden.Value);

                    if (AccesorioOperacionHidden.Value == "Nuevo")
                        TemporalAccesorioObjetoEntidad.Estatus = (Int16)ConstantePrograma.EstatusTemporalAccesorio.Activo;
                    else
                        TemporalAccesorioObjetoEntidad.Estatus = (Int16)ConstantePrograma.EstatusTemporalAccesorio.Nuevo;

                    TemporalActivoObjetoEntidad.TemporalActivoId = int.Parse(TemporalActivoIdHidden.Value);
                    TemporalActivoObjetoEntidad.TemporalCompraId = int.Parse(TemporalCompraIdHidden.Value);

                    switch (Int16.Parse(TipoAccesorioId.SelectedValue))
                    {
                        case (Int16)ConstantePrograma.TipoAccesorio.ActivoFijo:

                            if (CodigoBarrasParticular.Text != "")
                            {
                                if (ActivoAccesorioIdHidden.Value != "0" & ActivoAccesorioIdHidden.Value != "")
                                {
                                    TemporalAccesorioObjetoEntidad.ActivoAccesorioId = Int16.Parse(ActivoAccesorioIdHidden.Value);
                                    AgregarAccesorio(TemporalAccesorioObjetoEntidad, TemporalActivoObjetoEntidad);

                                }
                                else
                                {
                                    EtiquetaMensajeAccesorio.Text = "Favor de ingresar el Código de Barras y dar Enter para seleccionarlo.";
                                }
                            }
                            else
                            {
                                EtiquetaMensajeAccesorio.Text = "El campo Código de Barras es obligatorio.";
                            }

                            break;

                        default:
                            if (DescripcionAccesorio.Text != "")
                            {
                                TemporalAccesorioObjetoEntidad.Descripcion = DescripcionAccesorio.Text.Trim();
                                AgregarAccesorio(TemporalAccesorioObjetoEntidad, TemporalActivoObjetoEntidad);
                            }
                            else
                            {
                                EtiquetaMensajeAccesorio.Text = "El campo Descripción es obligatorio.";
                            }
                            break;
                    }

                }
            }

            protected void AgregarAccesorio(TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad, TemporalActivoEntidad TemporalActivoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalAccesorioProceso TemporalAccesorioProcesoNegocio = new TemporalAccesorioProceso();

                Resultado = TemporalAccesorioProcesoNegocio.AgregarTemporalAccesorio(TemporalAccesorioObjetoEntidad, TemporalActivoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioGuardadoCorrectamente)
                {
                    TipoAccesorioId.SelectedIndex = 0;
                    DescripcionAccesorio.Text = "";
                    ActivoAccesorioIdHidden.Value = "0";
                    CodigoBarrasParticular.Text = "";
                    DecripcionActivoAccesorio.Text = "";
                    TemporalActivoIdHidden.Value = Resultado.NuevoRegistroId.ToString();
                    TipoActivoId.Enabled = false;
                    EtiquetaMensajeAccesorio.Text = "";
                    CambiarAccesorioTipoNinguno();
                    SeleccionarTemporalAccesorio();
                }
                else
                {
                    EtiquetaMensajeAccesorio.Text = Resultado.DescripcionError;
                }
            }

            protected void AgregarActivo()
            {
                TemporalActivoEntidad TemporalActivoObjetoEntidad = new TemporalActivoEntidad();

                InsertarTemporalCompra();

                if (TemporalCompraIdHidden.Value != "0")
                {
                    BuscarEmpleadoPorNumero();

                    if (EmpleadoIdHidden.Value != "0")
                    {
                        TemporalActivoObjetoEntidad.EmpleadoId = Int16.Parse(EmpleadoIdHidden.Value);
                        TemporalActivoObjetoEntidad.TemporalActivoId = int.Parse(TemporalActivoIdHidden.Value);
                        TemporalActivoObjetoEntidad.TemporalCompraId = int.Parse(TemporalCompraIdHidden.Value);
                        TemporalActivoObjetoEntidad.TipoActivoId = Int16.Parse(TipoActivoId.SelectedValue);
                        TemporalActivoObjetoEntidad.SubFamiliaId = Int16.Parse(SubFamiliaId.SelectedValue);
                        TemporalActivoObjetoEntidad.MarcaId = Int16.Parse(MarcaId.SelectedValue);
                        TemporalActivoObjetoEntidad.CondicionId = Int16.Parse(CondicionId.SelectedValue);
                        TemporalActivoObjetoEntidad.CodigoBarrasParticular = ActivoCodigoBarrasParticular.Text.Trim();
                        TemporalActivoObjetoEntidad.CodigoBarrasGeneral = ActivoCodigoBarrasGeneral.Text.Trim();
                        TemporalActivoObjetoEntidad.Descripcion = DescripcionActivo.Text.Trim();
                        TemporalActivoObjetoEntidad.NumeroSerie = NumeroSerie.Text.Trim();
                        TemporalActivoObjetoEntidad.Modelo = Modelo.Text.Trim();
                        TemporalActivoObjetoEntidad.Color = Color.Text.Trim();
                        TemporalActivoObjetoEntidad.Monto = decimal.Parse(MontoActivo.Text.Trim());

                        if (UbicacionActivoBodega.Checked == true)
                            TemporalActivoObjetoEntidad.UbicacionActivoId = (Int16)ConstantePrograma.UbicacionActivo.Bodega;
                        else
                            TemporalActivoObjetoEntidad.UbicacionActivoId = (Int16)ConstantePrograma.UbicacionActivo.Piso;

                        AgregarActivo(TemporalActivoObjetoEntidad);
                    }
                }
            }

            protected void AgregarActivo(TemporalActivoEntidad TemporalActivoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalActivoProceso TemporalActivoProcesoNegocio = new TemporalActivoProceso();

                Resultado = TemporalActivoProcesoNegocio.AgregarTemporalActivo(TemporalActivoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalActivo.TemporalActivoGuardadoCorrectamente
                    || Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEditadoCorrectamente)
                {
                    LimpiarActivo();
                    CambiarBotonesNuevo();
                    SeleccionarTemporalActivo();
                    NumeroEmpleado.Focus();
                }
                else
                {
                    EtiquetaMensaje.Text = Resultado.DescripcionError;
                }
            }

            protected void BuscarJefe()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();
                Int16 EmpleadoIdJefe;

                EmpleadoEntidadObjeto.EmpleadoId = Int16.Parse(SolicitanteIdHidden.Value);

                if (EmpleadoEntidadObjeto.EmpleadoId != 0)
                {
                    Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                    if (Resultado.ErrorId == 0)
                    {
                        EmpleadoIdJefe = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoIdJefe"].ToString());
                        SeleccionarJefe(EmpleadoIdJefe);
                    }
                    else
                    {
                        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    }
                }

            }

            public void BuscarEmpleadoPorId(Int16 EmpleadoId)
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoId;

                BuscarEmpleado(EmpleadoEntidadObjeto);

                ActualizarTablaEmpleado.Update();
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
                            TipoActivoId.Focus();
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

            public void BuscarSolicitantePorId(Int16 EmpleadoId)
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoId;

                BuscarSolicitante(EmpleadoEntidadObjeto);

                ActualizarTablaEmpleado.Update();
            }

            protected void BuscarSolicitantePorNumero()
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.NumeroEmpleado = NumeroSolicitante.Text.Trim();

                BuscarSolicitante(EmpleadoEntidadObjeto);
            }

            protected void BuscarSolicitante(EmpleadoEntidad EmpleadoEntidadObjeto)
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
                            SolicitanteIdHidden.Value = "0";
                            NombreSolicitante.Text = "";
                            EtiquetaMensaje.Text = TextoError.EmpleadoInactivo;
                            NumeroSolicitante.Focus();
                        }
                        else
                        {
                            NombreSolicitante.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                            SolicitanteIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                            NumeroSolicitante.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                            BuscarJefe();
                            NumeroEmpleado.Focus();
                            EtiquetaMensaje.Text = "";
                        }
                    }
                    else
                    {
                        SolicitanteIdHidden.Value = "0";
                        NombreSolicitante.Text = "";
                        EtiquetaMensaje.Text = TextoError.EmpleadoNoEncontrado;
                        NumeroSolicitante.Focus();
                    }
                }
                else
                {
                    SolicitanteIdHidden.Value = "0";
                    NombreSolicitante.Text = "";
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void CambiarBotonesActualizar()
            {
                BotonAgregarActivo.Visible = false;
                BotonCancelarNuevo.Visible = false;
                BotonActualizarActivo.Visible = true;
                BotonCancelarActualizar.Visible = true;
                AccesorioOperacionHidden.Value = "Edicion";
            }

            protected void CambiarBotonesNuevo()
            {
                BotonAgregarActivo.Visible = true;
                BotonCancelarNuevo.Visible = true;
                AccesorioOperacionHidden.Value = "Nuevo";
                BotonActualizarActivo.Visible = false;
                BotonCancelarActualizar.Visible = false;
            }

            protected void CancelarActualizarActivo()
            {
                TemporalActivoEntidad TemporalActivoObjetoEntidad = new TemporalActivoEntidad();

                TemporalActivoObjetoEntidad.TemporalActivoId = int.Parse(TemporalActivoIdHidden.Value);

                switch (Int16.Parse(TipoActivoId.SelectedValue))
                {
                    case (Int16)ConstantePrograma.TipoAtivoConAccesorio.TipoActivoVehículoId:
                        CancelarActualizarActivo(TemporalActivoObjetoEntidad);
                        break;

                    default:
                        LimpiarActivo();
                        CambiarBotonesNuevo();
                        break;
                }

            }

            protected void CancelarActualizarActivo(TemporalActivoEntidad TemporalActivoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalActivoProceso TemporalActivoProcesoNegocio = new TemporalActivoProceso();

                Resultado = TemporalActivoProcesoNegocio.CancelarActualizarActivo(TemporalActivoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEditadoCorrectamente)
                {
                    LimpiarActivo();
                    CambiarBotonesNuevo();
                    EtiquetaMensaje.Text = "";
                }
                else
                {
                    EtiquetaMensaje.Text = Resultado.DescripcionError;
                }
            }

            protected void CancelarNuevoActivo()
            {
                TemporalActivoEntidad TemporalActivoObjetoEntidad = new TemporalActivoEntidad();
                TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad = new TemporalAccesorioEntidad();

                if (TemporalActivoIdHidden.Value != "0")
                {
                    TemporalActivoObjetoEntidad.TemporalActivoId = int.Parse(TemporalActivoIdHidden.Value);

                    TemporalAccesorioObjetoEntidad.TemporalActivoId = int.Parse(TemporalActivoIdHidden.Value);
                    TemporalAccesorioObjetoEntidad.GrupoEstatus = "," + (int)ConstantePrograma.EstatusTemporalAccesorio.Eliminado
                        + "," + (int)ConstantePrograma.EstatusTemporalAccesorio.Activo + "," + (int)ConstantePrograma.EstatusTemporalAccesorio.Nuevo + ",";
                    CancelarNuevoActivo(TemporalAccesorioObjetoEntidad, TemporalActivoObjetoEntidad, true);
                }
                else
                {
                    LimpiarActivo();
                }
            }

            protected void EliminarActivo(int TemporalActivoId)
            {
                TemporalActivoEntidad TemporalActivoObjetoEntidad = new TemporalActivoEntidad();
                TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad = new TemporalAccesorioEntidad();

                if (TemporalActivoIdHidden.Value != TemporalActivoId.ToString())
                {
                    TemporalActivoObjetoEntidad.TemporalActivoId = TemporalActivoId;

                    TemporalAccesorioObjetoEntidad.TemporalActivoId = TemporalActivoId;
                    TemporalAccesorioObjetoEntidad.GrupoEstatus = "," + (int)ConstantePrograma.EstatusTemporalAccesorio.Eliminado
                        + "," + (int)ConstantePrograma.EstatusTemporalAccesorio.Activo + "," + (int)ConstantePrograma.EstatusTemporalAccesorio.Nuevo + ",";
                    // Se usa la misma función que la de Cancelar un Nuevo Activo ya que hacen lo mismo
                    CancelarNuevoActivo(TemporalAccesorioObjetoEntidad, TemporalActivoObjetoEntidad, false);
                }
                else
                {
                    EtiquetaMensaje.Text = "No es posible eliminar ese Activo ya que se encuentra en Edición actualmente";
                }
            }

            protected void GuardarRecepcion()
            {
                CompraEntidad CompraObjetoEntidad = new CompraEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                if (TemporalCompraIdHidden.Value != "0")
                {
                    if (decimal.Parse(MontoDocumento.Text.Trim()) == decimal.Parse(MontoTotalHidden.Value))
                    {
                        if (TablaActivo.Rows.Count > 0)
                        {
                            BuscarSolicitantePorNumero();

                            if (SolicitanteIdHidden.Value != "0")
                            {
                                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                                CompraObjetoEntidad.TemporalCompraId = int.Parse(TemporalCompraIdHidden.Value);
                                CompraObjetoEntidad.ProveedorId = Int16.Parse(ProveedorId.SelectedValue);
                                CompraObjetoEntidad.TipoDocumentoId = Int16.Parse(TipoDocumentoId.SelectedValue);
                                CompraObjetoEntidad.CompraFolio = CompraFolio.Text.Trim();
                                CompraObjetoEntidad.Monto = decimal.Parse(MontoDocumento.Text.Trim());
                                CompraObjetoEntidad.OrdenCompra = OrdenCompra.Text.Trim();
                                CompraObjetoEntidad.EmpleadoId = Int16.Parse(SolicitanteIdHidden.Value);
                                CompraObjetoEntidad.JefeId = Int16.Parse(JefeIdHidden.Value);
                                CompraObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;

                                if (!(FechaCompra.Text.Trim() == ""))
                                    CompraObjetoEntidad.FechaCompra = FormatoFecha.AsignarFormato(FechaCompra.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);

                                if (!(FechaOC.Text.Trim() == ""))
                                    CompraObjetoEntidad.FechaOc = FormatoFecha.AsignarFormato(FechaOC.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);

                                GuardarRecepcion(CompraObjetoEntidad, (Int16)ConstantePrograma.TipoMovimiento.Asignacion);
                            }
                            
                        }
                        else
                        {
                            EtiquetaMensaje.Text = "Favor de agregar los activos";
                        }
                    }
                    else
                    {
                        EtiquetaMensaje.Text = "El monto total de los activos no es igual al monto del documento.";
                    }
                }
                else
                {
                    EtiquetaMensaje.Text = "Favor de agregar los activos";
                }
            }

            protected void GuardarRecepcion(CompraEntidad CompraObjetoEntidad, Int16 TipoMovimientoId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                CompraProceso CompraProcesoNegocio = new CompraProceso();

                Resultado = CompraProcesoNegocio.GuardarRecepcion(CompraObjetoEntidad, TipoMovimientoId);

                if (Resultado.ErrorId == (int)ConstantePrograma.Compra.RecepcionGuardadoCorrectamente)
                {
                    LimpiarTemporalTabla();
                    LimpiarFormulario();
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
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.HistorialActivos);

                    SeleccionarProveedor();
                    SeleccionarFamilia();
                    SeleccionarSubfamilia();
                    SeleccionarMarca();
                    SeleccionarCondicion();
                    SeleccionarTipoAccesorio();
                    SeleccionarTipoActivo();
                    SeleccionarTipoDocumento();
                    SeleccionarTextoError();

                    TablaActivo.DataSource = null;
                    TablaActivo.DataBind();

                    TablaAccesorio.DataSource = null;
                    TablaAccesorio.DataBind();
                }
            }

            protected void InsertarTemporalCompra()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalCompraEntidad TemporalCompraObjetoEntidad = new TemporalCompraEntidad();
                TemporalCompraProceso TemporalCompraProcesoNegocio = new TemporalCompraProceso();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                if (TemporalCompraIdHidden.Value == "0")
                {
                    UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                    TemporalCompraObjetoEntidad.UsuarioId = UsuarioSessionEntidad.UsuarioId;

                    Resultado = TemporalCompraProcesoNegocio.InsertarTemporalCompra(TemporalCompraObjetoEntidad);

                    if (Resultado.ErrorId == (int)ConstantePrograma.TemporalCompra.TemporalCompraGuardadoCorrectamente)
                    {
                        TemporalCompraIdHidden.Value = Resultado.NuevoRegistroId.ToString();
                    }
                    else
                    {
                        TemporalCompraIdHidden.Value = "0";
                        EtiquetaMensaje.Text = Resultado.DescripcionError;
                    }
                }
            }

            protected void LimpiarAccesorios()
            {
                TipoAccesorioId.SelectedIndex = 0;
                DescripcionAccesorio.Text = "";
                CodigoBarrasParticular.Text = "";
                DecripcionActivoAccesorio.Text = "";
                ActivoAccesorioIdHidden.Value = "0";
                TablaAccesorio.DataSource = null;
                TablaAccesorio.DataBind();
                EtiquetaMensajeAccesorio.Text = "";
            }

            protected void LimpiarActivo()
            {
                LimpiarAccesorios();
                TipoActivoId.Enabled = true;
                TipoActivoId.SelectedIndex = 0;
                OcultarMostrarAccesorios();
                FamiliaId.SelectedIndex = 0;
                SeleccionarSubfamilia();
                MarcaId.SelectedIndex = 0;
                CondicionId.SelectedIndex = 0;
                ActivoCodigoBarrasParticular.Text = "";
                ActivoCodigoBarrasGeneral.Text = "";
                DescripcionActivo.Text = "";
                NumeroSerie.Text = "";
                Modelo.Text = "";
                Color.Text = "";
                MontoActivo.Text = "";
                UbicacionActivoBodega.Checked = false;
                EtiquetaMensaje.Text = "";
                NumeroEmpleado.Text = "";
                EmpleadoIdHidden.Value = "0";
                NombreEmpleado.Text = "";

                TemporalActivoIdHidden.Value = "0";
                AccesorioOperacionHidden.Value = "Nuevo";
            }

            protected void LimpiarFormulario()
            {
                ProveedorId.SelectedIndex = 0;
                TipoDocumentoId.SelectedIndex = 0;
                CompraFolio.Text = "";
                FechaCompra.Text = "";
                MontoDocumento.Text = "";
                OrdenCompra.Text = "";
                FechaOC.Text = "";
                SolicitanteIdHidden.Value = "0";
                NumeroSolicitante.Text = "";
                NombreSolicitante.Text = "";
                NombreJefe.Text = "";
                LabelMontoTotal.Text = "";
                MontoTotalHidden.Value = "0";
                TemporalCompraIdHidden.Value = "0";
                JefeIdHidden.Value = "0";

                TablaActivo.DataSource = null;
                TablaActivo.DataBind();

                LimpiarActivo();
            }

            protected void LimpiarTemporalTabla()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalCompraEntidad TemporalCompraObjetoEntidad = new TemporalCompraEntidad();
                TemporalCompraProceso TemporalCompraProcesoNegocio = new TemporalCompraProceso();

                if (TemporalCompraIdHidden.Value != "0")
                {
                    TemporalCompraObjetoEntidad.TemporalCompraId = int.Parse(TemporalCompraIdHidden.Value);

                    Resultado = TemporalCompraProcesoNegocio.LimpiarTemporalTabla(TemporalCompraObjetoEntidad);

                    if (Resultado.ErrorId == (int)ConstantePrograma.TemporalCompra.TemporalCompraEliminadoCorrectamente)
                    {
                        //Do nothing
                    }
                }
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

            protected void NumeroSolicitanteTextoModificado()
            {
                if (NumeroSolicitante.Text.Trim() != "")
                {
                    BuscarSolicitantePorNumero();
                }
                else
                {
                    SolicitanteIdHidden.Value = "0";
                    NombreSolicitante.Text = "";
                    JefeIdHidden.Value = "0";
                    NombreJefe.Text = "";
                }
            }

            protected void OcultarMostrarAccesorios()
            {
                switch (Int16.Parse(TipoActivoId.SelectedValue))
                {
                    case (Int16)ConstantePrograma.TipoAtivoConAccesorio.TipoActivoVehículoId:
                        pnlAccesorio.Visible = true;
                        break;

                    default:
                        pnlAccesorio.Visible = false;
                        break;
                }
            }

            protected void SeleccionarActivoAccesorio()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
                bool AsignacionPermitida = true;

                ActivoEntidadObjeto.CodigoBarrasParticular = CodigoBarrasParticular.Text.Trim();

                Resultado = ActivoProcesoObjeto.SeleccionarActivo(ActivoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                    {
                        if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString()) != (Int16)ConstantePrograma.TipoAtivoConAccesorio.TipoActivoVehículoId)
                        {
                            DecripcionActivoAccesorio.Text = "";
                            ActivoAccesorioIdHidden.Value = "0";
                            EtiquetaMensajeAccesorio.Text = TextoError.TipoActivoIncorrecto;
                            CodigoBarrasParticular.Focus();
                        }
                        else
                        {
                            //Se valida que se pueda asignar el Activo
                            AsignacionPermitida = ActivoProcesoObjeto.ValidarAsignacionActivo(int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString()));

                            if (AsignacionPermitida == true)
                            {
                                DecripcionActivoAccesorio.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                                ActivoAccesorioIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();
                                EtiquetaMensajeAccesorio.Text = "";
                            }
                            else
                            {
                                DecripcionActivoAccesorio.Text = "";
                                ActivoAccesorioIdHidden.Value = "0";
                                EtiquetaMensajeAccesorio.Text = TextoError.EstatusActivoIncorrecto;
                                CodigoBarrasParticular.Focus();
                            }

                        }

                        //if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString()) != (Int16)ConstantePrograma.EstatusActivos.SinAsignar)
                        //{
                        //    DecripcionActivoAccesorio.Text = "";
                        //    ActivoAccesorioIdHidden.Value = "0";
                        //    EtiquetaMensajeAccesorio.Text = TextoError.EstatusActivoIncorrecto;
                        //    CodigoBarrasParticular.Focus();
                        //}
                        //else
                        //{
                        //    if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString()) != (Int16)ConstantePrograma.TipoAtivoConAccesorio.TipoActivoVehículoId)
                        //    {
                        //        DecripcionActivoAccesorio.Text = "";
                        //        ActivoAccesorioIdHidden.Value = "0";
                        //        EtiquetaMensajeAccesorio.Text = TextoError.TipoActivoIncorrecto;
                        //        CodigoBarrasParticular.Focus();
                        //    }
                        //    else
                        //    {
                        //        DecripcionActivoAccesorio.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                        //        ActivoAccesorioIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();
                        //        EtiquetaMensajeAccesorio.Text = "";
                        //    }
                        //}
                    }
                    else
                    {
                        DecripcionActivoAccesorio.Text = "";
                        ActivoAccesorioIdHidden.Value = "0";
                        EtiquetaMensajeAccesorio.Text = TextoError.NoExisteActivo;
                        CodigoBarrasParticular.Focus();
                    }
                }
                else
                {
                    EtiquetaMensajeAccesorio.Text = TextoError.ErrorGenerico;
                }
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
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                CondicionId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            //protected void SeleccionarEmpleado()
            //{
            //    ResultadoEntidad Resultado = new ResultadoEntidad();
            //    EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            //    EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

            //    EmpleadoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusEmpleados.Activo;

            //    Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

            //    EmpleadoId.DataValueField = "EmpleadoId";
            //    EmpleadoId.DataTextField = "NombreEmpleadoCompleto";

            //    if (Resultado.ErrorId == 0)
            //    {
            //        EmpleadoId.DataSource = Resultado.ResultadoDatos;
            //        EmpleadoId.DataBind();
            //    }
            //    else
            //    {
            //        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            //    }

            //    EmpleadoId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            //}

            protected void SeleccionarFamilia()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
                FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

                FamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusFamilia.Activo;

                Resultado = FamiliaProcesoObjeto.SeleccionarFamilia(FamiliaEntidadObjeto);

                FamiliaId.DataValueField = "FamiliaId";
                FamiliaId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    FamiliaId.DataSource = Resultado.ResultadoDatos;
                    FamiliaId.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                FamiliaId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            public void SeleccionarJefe(Int16 EmpleadoIdJefe)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoIdJefe;

                Resultado = EmpleadoProcesoNegocio.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    NombreJefe.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                    JefeIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                    ActualizarTablaEmpleado.Update();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarMarca()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MarcaEntidad MarcaEntidadObjeto = new MarcaEntidad();
                MarcaProceso MarcaProcesoObjeto = new MarcaProceso();

                MarcaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusMarca.Activo;

                Resultado = MarcaProcesoObjeto.SeleccionarMarca(MarcaEntidadObjeto);

                MarcaId.DataValueField = "MarcaId";
                MarcaId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    MarcaId.DataSource = Resultado.ResultadoDatos;
                    MarcaId.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                MarcaId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
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

            protected void SeleccionarSubfamilia()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();
                SubFamiliaProceso SubFamiliaProcesoObjeto = new SubFamiliaProceso();

                SubFamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusSubFamilia.Activo;
                SubFamiliaEntidadObjeto.FamiliaId = Int16.Parse(FamiliaId.SelectedValue);

                if (SubFamiliaEntidadObjeto.FamiliaId == 0)
                {
                    SubFamiliaId.Items.Clear();
                }
                else
                {
                    Resultado = SubFamiliaProcesoObjeto.SeleccionarSubFamilia(SubFamiliaEntidadObjeto);

                    SubFamiliaId.DataValueField = "SubFamiliaId";
                    SubFamiliaId.DataTextField = "Nombre";

                    if (Resultado.ErrorId == 0)
                    {
                        SubFamiliaId.DataSource = Resultado.ResultadoDatos;
                        SubFamiliaId.DataBind();
                    }
                    else
                    {
                        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    }
                }

                SubFamiliaId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarTemporalAccesorio()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad = new TemporalAccesorioEntidad();
                TemporalAccesorioProceso TemporalAccesorioProcesoNegocio = new TemporalAccesorioProceso();

                TemporalAccesorioObjetoEntidad.TemporalActivoId = int.Parse(TemporalActivoIdHidden.Value);
                TemporalAccesorioObjetoEntidad.GrupoEstatus = "," + (int)ConstantePrograma.EstatusTemporalAccesorio.Activo + "," + (int)ConstantePrograma.EstatusTemporalAccesorio.Nuevo + ",";

                Resultado = TemporalAccesorioProcesoNegocio.SeleccionarTemporalAccesorio(TemporalAccesorioObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaAccesorio.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaAccesorio.CssClass = ConstantePrograma.ClaseTabla;

                    TablaAccesorio.DataSource = Resultado.ResultadoDatos;
                    TablaAccesorio.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarTemporalActivo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalActivoEntidad TemporalActivoObjetoEntidad = new TemporalActivoEntidad();
                TemporalActivoProceso TemporalActivoProcesoNegocio = new TemporalActivoProceso();

                TemporalActivoObjetoEntidad.TemporalCompraId = int.Parse(TemporalCompraIdHidden.Value);

                Resultado = TemporalActivoProcesoNegocio.SeleccionarTemporalActivo(TemporalActivoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaActivo.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaActivo.CssClass = ConstantePrograma.ClaseTabla;

                    SumaMonto = 0;

                    TablaActivo.DataSource = Resultado.ResultadoDatos;
                    TablaActivo.DataBind();

                    MontoTotalHidden.Value = SumaMonto.ToString();
                    LabelMontoTotal.Text = string.Format("{0:C}", SumaMonto);
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarTemporalActivoParaEditar(int TemporalActivoId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalActivoEntidad TemporalActivoObjetoEntidad = new TemporalActivoEntidad();
                TemporalActivoProceso TemporalActivoProcesoNegocio = new TemporalActivoProceso();

                TemporalActivoObjetoEntidad.TemporalActivoId = TemporalActivoId;

                Resultado = TemporalActivoProcesoNegocio.SeleccionarTemporalActivo(TemporalActivoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                    {
                        TipoActivoId.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString();

                        if (Int16.Parse(TipoActivoId.SelectedValue) == (Int16)ConstantePrograma.TipoAtivoConAccesorio.TipoActivoVehículoId)
                            TipoActivoId.Enabled = false;
                        else
                            TipoActivoId.Enabled = true;

                        NumeroEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                        NombreEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleado"].ToString() +
                            " " + Resultado.ResultadoDatos.Tables[0].Rows[0]["ApellidoPaterno"].ToString() +
                            " " + Resultado.ResultadoDatos.Tables[0].Rows[0]["ApellidoMaterno"].ToString();
                        EmpleadoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                        FamiliaId.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString();
                        SeleccionarSubfamilia();
                        SubFamiliaId.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["SubFamiliaId"].ToString();
                        MarcaId.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["MarcaId"].ToString();
                        CondicionId.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["CondicionId"].ToString();
                        ActivoCodigoBarrasParticular.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoBarrasParticular"].ToString();
                        ActivoCodigoBarrasGeneral.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoBarrasGeneral"].ToString();
                        DescripcionActivo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                        NumeroSerie.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroSerie"].ToString();
                        Modelo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Modelo"].ToString();
                        Color.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Color"].ToString();
                        MontoActivo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Monto"].ToString();

                        switch (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["UbicacionActivoId"].ToString()))
                        {
                            case (Int16)ConstantePrograma.UbicacionActivo.Bodega:
                                UbicacionActivoBodega.Checked = true;
                                break;

                            default:
                                UbicacionActivoBodega.Checked = false;
                                break;
                        }

                        TemporalActivoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["TemporalActivoId"].ToString();
                        AccesorioOperacionHidden.Value = "Edicion";
                        OcultarMostrarAccesorios();

                        if (Int16.Parse(TipoActivoId.SelectedValue) == (Int16)ConstantePrograma.TipoAtivoConAccesorio.TipoActivoVehículoId)
                            SeleccionarTemporalAccesorio();
                    }
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarTextoError()
            {
                // Datos Generales
                ProveedorRequerido.ErrorMessage = TextoError.RecepcionProveedor + "<br />";
                TipoDocumentoRequerido.ErrorMessage = TextoError.RecepcionTipoDocumento + "<br />";
                FolioRequerido.ErrorMessage = TextoError.RecepcionFolio + "<br />";
                MontoDocumentoRequerido.ErrorMessage = TextoError.RecepcionMonto + "<br />";
                MontoDocumentoValidado.ErrorMessage = TextoError.RecepcionMontoValido + "<br />";
                OrdenCompraRequerido.ErrorMessage = TextoError.RecepcionOrdenCompra + "<br />";
                NumeroSolicitanteGuardarRequerido.ErrorMessage = TextoError.RecepcionSolicitante + "<br />";
                NumeroSolicitanteBuscarRequerido.ErrorMessage = TextoError.RecepcionSolicitante + "<br />";
                JefeRequerido.ErrorMessage = TextoError.RecepcionJefe + "<br />";
                FechaDocumentoValidado.ErrorMessage = TextoError.RecepcionFechaDocumento + "<br />";
                FechaOCValidado.ErrorMessage = TextoError.RecepcionFechaOC + "<br />";

                //Activo
                NumeroEmpleadoRequerido.ErrorMessage = TextoError.RecepcionAsignacionEmpleado + "<br />";
                TipoActivoRequerido.ErrorMessage = TextoError.RecepcionTipoActivo + "<br />";
                FamiliaRequerido.ErrorMessage = TextoError.RecepcionFamilia + "<br />";
                SubFamiliaRequerido.ErrorMessage = TextoError.RecepcionSubFamilia + "<br />";
                MarcaRequerido.ErrorMessage = TextoError.RecepcionMarca + "<br />";
                CondicionRequerido.ErrorMessage = TextoError.RecepcionCondicion + "<br />";
                ActivoCodigoBarrasParticularRequerido.ErrorMessage = TextoError.RecepcionCodigoBarrasParticular + "<br />";
                MontoActivoRequerido.ErrorMessage = TextoError.RecepcionMontoActivo + "<br />";
                MontoActivoValidado.ErrorMessage = TextoError.RecepcionMontoActivoValido + "<br />";

                //Accesorios
                TipoAccesorioRequerido.ErrorMessage = TextoError.RecepcionTipoAccesorio + "<br />";
                //DescripcionAccesorioRequerido.ErrorMessage = TextoError.RecepcionDescripcionAccesorio + "<br />";
                //CodigoBarrasRequerido.ErrorMessage = TextoError.RecepcionCodigoBarrasParticular + "<br />";
                BuscarCodigoBarrasRequerido.ErrorMessage = TextoError.RecepcionCodigoBarrasParticular + "<br />";
            }

            protected void SeleccionarTipoAccesorio()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TipoAccesorioEntidad TipoAccesorioEntidadObjeto = new TipoAccesorioEntidad();
                TipoAccesorioProceso TipoAccesorioProcesoObjeto = new TipoAccesorioProceso();

                Resultado = TipoAccesorioProcesoObjeto.SeleccionarTipoAccesorio(TipoAccesorioEntidadObjeto);

                TipoAccesorioId.DataValueField = "TipoAccesorioId";
                TipoAccesorioId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    TipoAccesorioId.DataSource = Resultado.ResultadoDatos;
                    TipoAccesorioId.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                TipoAccesorioId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarTipoActivo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TipoActivoEntidad TipoActivoEntidadObjeto = new TipoActivoEntidad();
                TipoActivoProceso TipoActivoProcesoObjeto = new TipoActivoProceso();

                Resultado = TipoActivoProcesoObjeto.SeleccionarTipoActivo(TipoActivoEntidadObjeto);

                TipoActivoId.DataValueField = "TipoActivoId";
                TipoActivoId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    TipoActivoId.DataSource = Resultado.ResultadoDatos;
                    TipoActivoId.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                TipoActivoId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
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

            protected void TablaAccesorioEventoComando(GridViewCommandEventArgs e)
            {
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                int TemporalAccesorioId = 0;
                Int16 Estatus = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaAccesorio.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaAccesorio.PageIndex));

                switch (strCommand)
                {
                    case "EliminarAccesorio":
                        TemporalAccesorioId = int.Parse(TablaAccesorio.DataKeys[intFila]["TemporalAccesorioId"].ToString());
                        Estatus = Int16.Parse(TablaAccesorio.DataKeys[intFila]["Estatus"].ToString());

                        EliminarTemporalAccesorioIndividual(TemporalAccesorioId, Estatus);

                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

            protected void TablaActivoEventoComando(GridViewCommandEventArgs e)
            {
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                int TemporalActivoId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaActivo.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaActivo.PageIndex));

                switch (strCommand)
                {
                    case "Select":
                        TemporalActivoId = int.Parse(TablaActivo.DataKeys[intFila]["TemporalActivoId"].ToString());

                        if (TemporalActivoIdHidden.Value == "0")
                        {
                            SeleccionarTemporalActivoParaEditar(TemporalActivoId);
                            CambiarBotonesActualizar();
                        }
                        else
                        {
                            EtiquetaMensaje.Text = "Favor de finalizar la transacción del activo en curso.";
                        }
                        break;

                    case "EliminarActivo":
                        TemporalActivoId = int.Parse(TablaActivo.DataKeys[intFila]["TemporalActivoId"].ToString());
                        EliminarActivo(TemporalActivoId);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

            protected void CancelarNuevoActivo(TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad, TemporalActivoEntidad TemporalActivoObjetoEntidad, bool LimpiarAct)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalActivoProceso TemporalActivoProcesoNegocio = new TemporalActivoProceso();

                Resultado = TemporalActivoProcesoNegocio.CancelarNuevoActivo(TemporalAccesorioObjetoEntidad, TemporalActivoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalActivo.TemporalActivoEliminadoCorrectamente)
                {
                    if (LimpiarAct == true)
                    {
                        LimpiarActivo();
                        EtiquetaMensaje.Text = "";
                    }
                    else
                    {
                        SeleccionarTemporalActivo();
                    }
                }
                else
                {
                    EtiquetaMensaje.Text = Resultado.DescripcionError;
                }
            }

            protected void EliminarTemporalAccesorioIndividual(int TemporalAccesorioId, Int16 Estatus)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad = new TemporalAccesorioEntidad();
                TemporalAccesorioProceso TemporalAccesorioProcesoNegocio = new TemporalAccesorioProceso();

                TemporalAccesorioObjetoEntidad.TemporalAccesorioId = TemporalAccesorioId;

                if (AccesorioOperacionHidden.Value == "Nuevo")
                {
                    TemporalAccesorioObjetoEntidad.GrupoEstatus = "," + ((int)ConstantePrograma.EstatusTemporalAccesorio.Activo).ToString() + ",";
                    Resultado = TemporalAccesorioProcesoNegocio.EliminarTemporalAccesorioIndividual(TemporalAccesorioObjetoEntidad);
                }
                else
                {
                    if (Estatus == (Int16)ConstantePrograma.EstatusTemporalAccesorio.Activo)
                    {
                        TemporalAccesorioObjetoEntidad.Estatus = (Int16)ConstantePrograma.EstatusTemporalAccesorio.Eliminado;
                        TemporalAccesorioObjetoEntidad.GrupoEstatus = "," + ((int)ConstantePrograma.EstatusTemporalAccesorio.Activo).ToString() + ",";
                        Resultado = TemporalAccesorioProcesoNegocio.ActualizarTemporalAccesorioIndividual(TemporalAccesorioObjetoEntidad);
                    }
                    else
                    {
                        TemporalAccesorioObjetoEntidad.GrupoEstatus = "," + ((int)ConstantePrograma.EstatusTemporalAccesorio.Nuevo).ToString() + ",";
                        Resultado = TemporalAccesorioProcesoNegocio.EliminarTemporalAccesorioIndividual(TemporalAccesorioObjetoEntidad);
                    }
                }

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEliminadoCorrectamente
                    || Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEditadoCorrectamente)
                {
                    SeleccionarTemporalAccesorio();
                }
                else
                {
                    EtiquetaMensajeAccesorio.Text = Resultado.DescripcionError;
                }
            }



            protected void CambiarTipoAccesorioID()
            {
                switch (Int16.Parse(TipoAccesorioId.SelectedValue))
                {
                    case (Int16)ConstantePrograma.TipoAccesorio.ActivoFijo:
                        CambiarAccesorioTipoActivoFijo();
                        break;

                    case 0:
                        CambiarAccesorioTipoNinguno();
                        break;

                    default:
                        CambiarAccesorioTipoNOActivoFijo();
                        break;
                }
            }

            protected void CambiarAccesorioTipoActivoFijo()
            {
                trCodigoActivoAccesorio.Style["display"] = "block;";
                trDescripcionActivoAccesorio.Style["display"] = "block;";

                trDescripcionAccesorio.Style["display"] = "none;";

                DescripcionAccesorio.Text = "";
                CodigoBarrasParticular.Text = "";
                DecripcionActivoAccesorio.Text = "";
            }

            protected void CambiarAccesorioTipoNOActivoFijo()
            {
                trDescripcionAccesorio.Style["display"] = "block;";

                trCodigoActivoAccesorio.Style["display"] = "none;";
                trDescripcionActivoAccesorio.Style["display"] = "none;";

                DescripcionAccesorio.Text = "";
                CodigoBarrasParticular.Text = "";
                DecripcionActivoAccesorio.Text = "";
            }

            protected void CambiarAccesorioTipoNinguno()
            {
                trCodigoActivoAccesorio.Style["display"] = "none;";
                trDescripcionActivoAccesorio.Style["display"] = "none;";
                trDescripcionAccesorio.Style["display"] = "none;";
                DescripcionAccesorio.Text = "";
                CodigoBarrasParticular.Text = "";
                DecripcionActivoAccesorio.Text = "";
            }


        #endregion
    }
}
