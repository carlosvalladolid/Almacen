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
    public partial class LevantamientoActivo : System.Web.UI.Page
    {

        #region "Eventos"
        protected void BotonBuscarCodigoBarra_Click(object sender, EventArgs e)
        {
            BuscarActivoEnTabla();
        }

        protected void BotonBuscarEmpleado_Click(object sender, EventArgs e)
        {
            ControlBuscarEmpleado.InicioControl((Int16)ConstantePrograma.TipoBusquedaEmpleado.Empleado);
        }

        //protected void BotonBusqueda_Click(object sender, EventArgs e)
        //{
        //    BusquedaAvanzada();
        //}

        protected void LinkBuscarEmpleado_Click(object sender, EventArgs e)
        {
            BuscarEmpleadoPorNumero();
        }

        protected void BotonLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        protected void BotonTerminarLevantamiento_Click(object sender, EventArgs e)
        {
            GuardarLeventamiento();
        }

        protected void BotonImprimir_Click(object sender, EventArgs e)
        {
            if (LevantamientoCorrectoHidden.Value == "SI")
                ImprimirLevantamientoCorrecto();
            else
                ImprimirLevantamiento();
            
        }

        protected void NumeroEmpleado_TextChanged(object sender, EventArgs e)
        {
            NumeroEmpleadoTextoModificado();
        }

        //protected void NumeroEmpleado_TextChanged(object sender, EventArgs e)
        //{
        //    SeleccionarEmpleado();
        //}

        //protected void TablaLevantamientoActivo_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    TablaLevantamientoActivoEventoComando(e);
        //}

        protected void TablaLevantamiento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton Imagen = (ImageButton)e.Row.FindControl("IconoRevisado");

                switch (Int16.Parse(TablaLevantamiento.DataKeys[e.Row.RowIndex]["EstatusID"].ToString()))
                {
                    case (Int16)ConstantePrograma.EstatusLevantamiento.Localizado:
                        Imagen.ImageUrl = ("/Imagen/Icono/IconoRevisado.jpg");
                        break;

                    case (Int16)ConstantePrograma.EstatusLevantamiento.LocalizadoYNoAsignado:
                        Imagen.ImageUrl = ("/Imagen/Icono/IconoDuda.jpg");
                        break;

                    case (Int16)ConstantePrograma.EstatusLevantamiento.SinLocalizar:
                        Imagen.Visible = false;
                        break;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }
        #endregion


        #region "Métodos"

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.LevantamientoInventario);

                    TablaLevantamiento.DataSource = null;
                    TablaLevantamiento.DataBind();
                    SeleccionarTextoError();
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

                EtiquetaMensajeExito.Text = "";
                BotonImprimir.Enabled = false;
                LevantamientoCorrectoHidden.Value = "SI";
                EmpIdHidden.Value = "0";

                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString()) != (Int16)ConstantePrograma.EstatusEmpleados.Activo)
                        {
                            NumeroEmpleado.Enabled = true;
                            CodigoBarraParticular.Enabled = false;
                            EmpleadoIdHidden.Value = "0";
                            NombreEmpleado.Text = "";
                            EtiquetaMensaje.Text = TextoError.EmpleadoInactivo;
                            TablaLevantamiento.DataSource = null;
                            TablaLevantamiento.DataBind();
                            NumeroEmpleado.Focus();
                        }
                        else
                        {
                            NumeroEmpleado.Enabled = false;
                            CodigoBarraParticular.Enabled = true;
                            NombreEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                            EmpleadoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                            NumeroEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                            SeleccionarAsignacion(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString()));
                            EtiquetaMensaje.Text = "";
                            CodigoBarraParticular.Focus();
                        }
                    }
                    else
                    {
                        NumeroEmpleado.Enabled = true;
                        CodigoBarraParticular.Enabled = false;
                        EmpleadoIdHidden.Value = "0";
                        NombreEmpleado.Text = "";
                        EtiquetaMensaje.Text = TextoError.EmpleadoNoEncontrado;
                        TablaLevantamiento.DataSource = null;
                        TablaLevantamiento.DataBind();
                        NumeroEmpleado.Focus();
                    }
                }
                else
                {
                    NumeroEmpleado.Enabled = true;
                    CodigoBarraParticular.Enabled = false;
                    EmpleadoIdHidden.Value = "0";
                    NombreEmpleado.Text = "";
                    TablaLevantamiento.DataSource = null;
                    TablaLevantamiento.DataBind();
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarAsignacion(Int16 EmpleadoId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();

                ActivoObjetoEntidad.EmpleadoId = EmpleadoId;
                ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;
                ActivoObjetoEntidad.TipoAccesorioId = (Int16)ConstantePrograma.TipoAccesorio.ActivoFijo;

                Resultado = MovimientoProcesoObjeto.SeleccionarMovimientoAsignacionAccesorios(ActivoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaLevantamiento.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaLevantamiento.CssClass = ConstantePrograma.ClaseTabla;

                    LlenarGridConAsignacion(Resultado.ResultadoDatos.Tables[0]);
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void LlenarGridConAsignacion(DataTable ActivosAsignados)
            {
                DataTable Activos = new DataTable();
                DataRow dtRow;

                Activos = ObtenerEstructuraTabla();

                foreach (DataRow dtRegistro in ActivosAsignados.Rows)
                {
                    dtRow = Activos.NewRow();
                    dtRow["ActivoId"] = dtRegistro["ActivoId"].ToString();
                    dtRow["DescripcionPadre"] = dtRegistro["DescripcionPadre"].ToString();
                    dtRow["Descripcion"] = dtRegistro["Descripcion"].ToString();
                    dtRow["NumeroSerie"] = dtRegistro["NumeroSerie"].ToString();
                    dtRow["Modelo"] = dtRegistro["Modelo"].ToString();
                    dtRow["Color"] = dtRegistro["Color"].ToString();
                    dtRow["CodigoBarrasParticular"] = dtRegistro["CodigoBarrasParticular"].ToString();
                    dtRow["NombreEstatus"] = "Sin localizar";
                    dtRow["EstatusID"] = (Int16)ConstantePrograma.EstatusLevantamiento.SinLocalizar;
                    Activos.Rows.Add(dtRow);
                }
                Activos.AcceptChanges();

                TablaLevantamiento.DataSource = Activos;
                TablaLevantamiento.DataBind();
            }

            protected DataTable ObtenerEstructuraTabla()
            {
                DataTable Activos = new DataTable();

                Activos.Columns.Add("ActivoId");
                Activos.Columns.Add("DescripcionPadre");
                Activos.Columns.Add("Descripcion");
                Activos.Columns.Add("NumeroSerie");
                Activos.Columns.Add("Modelo");
                Activos.Columns.Add("Color");
                Activos.Columns.Add("CodigoBarrasParticular");
                Activos.Columns.Add("NombreEstatus");
                Activos.Columns.Add("EstatusID");

                return Activos;
            }

            protected void BuscarActivoEnTabla()
            {
                DataTable Activos = new DataTable();
                DataRow dtRow;
                bool SeEncontroActivo = false;

                EtiquetaMensaje.Text = "";

                if (CodigoBarraParticular.Text.Trim() != "")
                {
                    Activos = ObtenerEstructuraTabla();

                    //Se barre el grid y se pasan los activos al datatable
                    foreach (GridViewRow Renglon in TablaLevantamiento.Rows)
                    {
                        dtRow = Activos.NewRow();
                        dtRow["ActivoId"] = TablaLevantamiento.DataKeys[Renglon.RowIndex]["ActivoId"].ToString();
                        dtRow["DescripcionPadre"] = TablaLevantamiento.DataKeys[Renglon.RowIndex]["DescripcionPadre"].ToString();
                        dtRow["Descripcion"] = TablaLevantamiento.DataKeys[Renglon.RowIndex]["Descripcion"].ToString();
                        dtRow["NumeroSerie"] = TablaLevantamiento.DataKeys[Renglon.RowIndex]["NumeroSerie"].ToString();
                        dtRow["Modelo"] = TablaLevantamiento.DataKeys[Renglon.RowIndex]["Modelo"].ToString();
                        dtRow["Color"] = TablaLevantamiento.DataKeys[Renglon.RowIndex]["Color"].ToString();
                        dtRow["CodigoBarrasParticular"] = TablaLevantamiento.DataKeys[Renglon.RowIndex]["CodigoBarrasParticular"].ToString();
                        
                        //Si el código de barras es igual, se marca como Localizado
                        if (Renglon.Cells[5].Text == CodigoBarraParticular.Text.Trim())
                        {
                            if (Int16.Parse(TablaLevantamiento.DataKeys[Renglon.RowIndex]["EstatusID"].ToString()) == (Int16)ConstantePrograma.EstatusLevantamiento.SinLocalizar)
                            {
                                dtRow["NombreEstatus"] = "Localizado";
                                dtRow["EstatusID"] = (Int16)ConstantePrograma.EstatusLevantamiento.Localizado;
                            }
                            else
                            {
                                dtRow["NombreEstatus"] = TablaLevantamiento.DataKeys[Renglon.RowIndex]["NombreEstatus"].ToString();
                                dtRow["EstatusID"] = TablaLevantamiento.DataKeys[Renglon.RowIndex]["EstatusID"].ToString();
                            }

                            SeEncontroActivo = true;
                        }
                        else
                        {
                            dtRow["NombreEstatus"] = TablaLevantamiento.DataKeys[Renglon.RowIndex]["NombreEstatus"].ToString();
                            dtRow["EstatusID"] = TablaLevantamiento.DataKeys[Renglon.RowIndex]["EstatusID"].ToString();
                        }

                        Activos.Rows.Add(dtRow);
                    }
                    Activos.AcceptChanges();

                    if (SeEncontroActivo == false)
                        Activos = AgregarActivo(Activos);

                    TablaLevantamiento.DataSource = Activos;
                    TablaLevantamiento.DataBind();

                    CodigoBarraParticular.Text = "";
                    CodigoBarraParticular.Focus();
                }
            }

            protected DataTable AgregarActivo(DataTable Activos)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ResultadoEntidad ResultadoBaja = new ResultadoEntidad();
                ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
                MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();
                DataRow dtRow;

                ActivoEntidadObjeto.CodigoBarrasParticular = CodigoBarraParticular.Text.Trim();

                Resultado = ActivoProcesoObjeto.SeleccionarActivo(ActivoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        ActivoEntidadObjeto = new ActivoEntidad();
                        ActivoEntidadObjeto.ActivoId = int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString());

                        //Se valida que el Activo no este dado de baja
                        ResultadoBaja = MovimientoProcesoObjeto.SeleccionarMovimientoBaja(ActivoEntidadObjeto);

                        if (ResultadoBaja.ResultadoDatos.Tables[0].Rows.Count == 0)
                        {
                            dtRow = Activos.NewRow();
                            dtRow["ActivoId"] = Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();
                            dtRow["Descripcion"] = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                            dtRow["NumeroSerie"] = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroSerie"].ToString();
                            dtRow["Modelo"] = Resultado.ResultadoDatos.Tables[0].Rows[0]["Modelo"].ToString();
                            dtRow["Color"] = Resultado.ResultadoDatos.Tables[0].Rows[0]["Color"].ToString();
                            dtRow["CodigoBarrasParticular"] = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoBarrasParticular"].ToString();
                            dtRow["NombreEstatus"] = "Localizado y No Asignado";
                            dtRow["EstatusID"] = (Int16)ConstantePrograma.EstatusLevantamiento.LocalizadoYNoAsignado;
                            Activos.Rows.Add(dtRow);
                            Activos.AcceptChanges();
                            EtiquetaMensaje.Text = "";
                        }
                        else
                        {
                            EtiquetaMensaje.Text = TextoError.ActivoDadoBaja;
                        }
                    }
                    else
                    {
                        EtiquetaMensaje.Text = TextoError.NoExisteActivo;
                    }
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                return Activos;
            }

            protected string ObtenerCadenaCodigosXML()
            {
                StringBuilder CadenaActivosXML = new StringBuilder();

                CadenaActivosXML.Append("<row>");

                foreach (GridViewRow Registro in TablaLevantamiento.Rows)
                {
                    if (Int16.Parse(TablaLevantamiento.DataKeys[Registro.RowIndex]["EstatusID"].ToString()) == (Int16)ConstantePrograma.EstatusLevantamiento.SinLocalizar
                        || Int16.Parse(TablaLevantamiento.DataKeys[Registro.RowIndex]["EstatusID"].ToString()) == (Int16)ConstantePrograma.EstatusLevantamiento.LocalizadoYNoAsignado)
                    {
                        CadenaActivosXML.Append("<Activo ActivoId='");
                        CadenaActivosXML.Append(TablaLevantamiento.DataKeys[Registro.RowIndex]["ActivoId"].ToString());
                        CadenaActivosXML.Append("' EstatusID='");
                        CadenaActivosXML.Append(TablaLevantamiento.DataKeys[Registro.RowIndex]["EstatusID"].ToString());
                        CadenaActivosXML.Append("'/>");
                    }
                }

                CadenaActivosXML.Append("</row>");

                return CadenaActivosXML.ToString();
            }

            protected void ImprimirLevantamiento()
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirLevantamiento('" + LevantamientoIdHidden.Value + "')", true);
            }

            protected void ImprimirLevantamientoCorrecto()
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirLevantamientoCorrecto('" + EmpIdHidden.Value + "')", true);
            }

            protected void GuardarLeventamiento()
            {
                LevantamientoActivoEntidad LevantamientoObjetoEntidad = new LevantamientoActivoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                if (EmpleadoIdHidden.Value != "0")
                {
                    LevantamientoObjetoEntidad.CadenaActivosXML = ObtenerCadenaCodigosXML();

                    //Si en el levantamiento hubo problemas se guarda en la BD
                    if (LevantamientoObjetoEntidad.CadenaActivosXML != "<row></row>")
                    {
                        LevantamientoCorrectoHidden.Value = "NO";

                        UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                        LevantamientoObjetoEntidad.EmpleadoId = int.Parse(EmpleadoIdHidden.Value);
                        LevantamientoObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;

                        GuardarLeventamiento(LevantamientoObjetoEntidad);
                    }
                    else
                    {
                        //Si todos los activos fueron encontrados en el levantamiento y tampoco hubo de mas solo
                        //se manda imprimir el documento
                        LevantamientoCorrectoHidden.Value = "SI";
                        EmpIdHidden.Value = EmpleadoIdHidden.Value;
                        ImprimirLevantamientoCorrecto();
                        LimpiarFormulario();
                        BotonImprimir.Enabled = true;
                    }
                }
                else
                {
                    EtiquetaMensaje.Text = "Favor de seleccionar el empleado.";
                }

            }

            protected void GuardarLeventamiento(LevantamientoActivoEntidad LevantamientoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                LevantamientoActivoProceso LevantamientoActivoProcesoNegocio = new LevantamientoActivoProceso();

                Resultado = LevantamientoActivoProcesoNegocio.GuardarLevantamiento(LevantamientoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.LevantamientoActivo.LevantamientoActivoGuardadoCorrectamente)
                {
                    LimpiarFormulario();
                    EtiquetaMensajeExito.Text = TextoError.LevantamientoGuardadoCorrectamente;
                    BotonImprimir.Enabled = true;
                    LevantamientoIdHidden.Value = Resultado.NuevoRegistroId.ToString();
                    ImprimirLevantamiento();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico + ". " + Resultado.DescripcionError;
                }
            }

            protected void LimpiarFormulario()
            {
                NumeroEmpleado.Enabled = true;
                NumeroEmpleado.Text = "";
                EmpleadoIdHidden.Value = "0";
                NombreEmpleado.Text = "";
                CodigoBarraParticular.Text = "";
                CodigoBarraParticular.Enabled = false;
                EtiquetaMensaje.Text = "";
                EtiquetaMensajeExito.Text = "";
                BotonImprimir.Enabled = false;
                LevantamientoIdHidden.Value = "0";

                TablaLevantamiento.DataSource = null;
                TablaLevantamiento.DataBind();
            }

            protected void SeleccionarTextoError()
            {
                NumeroEmpleadoRequerido.ErrorMessage = TextoError.LevantamientoActivoEmpleadoId + "<br />";
                BuscarCodigoBarraParticularRequerido.ErrorMessage = TextoError.RecepcionCodigoBarrasParticular + "<br />";
            }


            //OLIVIA:.........................................................................

        //protected void TablaLevantamientoActivoEventoComando(GridViewCommandEventArgs e)
        //{
        //    ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

        //    Int16 intFila = 0;
        //    int intTamañoPagina = 0;
        //    Int16 ActivoId = 0;
        //    string strCommand = string.Empty;

        //    intFila = Int16.Parse(e.CommandArgument.ToString());
        //    strCommand = e.CommandName.ToString();
        //    intTamañoPagina = TablaLevantamiento.PageSize;

        //    if (intFila >= intTamañoPagina)
        //        intFila = (Int16)(intFila - (intTamañoPagina * TablaLevantamiento.PageIndex));


        //    switch (strCommand)
        //    {
        //        case "Select":
        //            ActivoId = Int16.Parse(TablaLevantamiento.DataKeys[intFila]["ActivoId"].ToString());
        //            ActivoEntidadObjeto.ActivoId = ActivoId;
        //            //ActivoIdHidden.Value = ActivoId.ToString();
        //            //SeleccionarActivoParaEditar(ActivoEntidadObjeto);
        //            break;

        //        default:
        //            // Do nothing
        //            break;
        //    }
        //}

        //protected void BusquedaAvanzada()
        //{
        //    LevantamientoActivoEntidad LevantamientoActivoEntidadObjeto = new LevantamientoActivoEntidad();

        //    // LevantamientoActivoEntidadObjeto.EmpleadoId = NumeroEmpleado.Text.Trim();
        //    LevantamientoActivoEntidadObjeto.EmpleadoId = Int16.Parse(EmpleadoIdHidden.Value);
        //    LevantamientoActivoEntidadObjeto.CodigoBarrasParticular = CodigoBarraParticular.Text.Trim();

        //    SeleccionarLevantamientoActivo(LevantamientoActivoEntidadObjeto);
        //}

        //protected void BuscarEmpleado(EmpleadoEntidad EmpleadoEntidadObjeto)
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

        //    Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

        //    if (Resultado.ErrorId == 0)
        //    {
        //        if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
        //        {
        //            if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString()) != (Int16)ConstantePrograma.EstatusEmpleados.Activo)
        //            {
        //                EmpleadoIdHidden.Value = "0";
        //                NombreEmpleado.Text = "";
        //                EtiquetaMensaje.Text = TextoError.EmpleadoInactivo;
        //                NumeroEmpleado.Focus();
        //            }
        //            else
        //            {
        //                NombreEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
        //                EmpleadoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
        //                NumeroEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
        //                EtiquetaMensaje.Text = "";
        //                //LO agregue aqui haber si lo hace desde aqui haber que pasa
        //                NumeroEmpleado.Enabled = false;
        //                CodigoBarraParticular.Enabled = true;

        //            }
        //        }
        //        else
        //        {
        //            EmpleadoIdHidden.Value = "0";
        //            NombreEmpleado.Text = "";
        //            EtiquetaMensaje.Text = TextoError.EmpleadoNoEncontrado;
        //            NumeroEmpleado.Focus();
        //        }
        //    }
        //    else
        //    {
        //        EmpleadoIdHidden.Value = "0";
        //        NombreEmpleado.Text = "";
        //        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
        //    }
        //}

        //protected void BuscarEmpleadoPorNumero()
        //{
        //    EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

        //    EmpleadoEntidadObjeto.NumeroEmpleado = NumeroEmpleado.Text.Trim();

        //    BuscarEmpleado(EmpleadoEntidadObjeto);

        //    BusquedaAvanzada();
        //}

        //protected void BusquedaLevantamientoActivo()
        //{
        //    LevantamientoActivoEntidad LevantamientoActivoEntidadObjeto = new LevantamientoActivoEntidad();

        //    if (CodigoBarraParticular.Text.Trim() == "")
        //        LevantamientoActivoEntidadObjeto.CodigoBarrasParticular = "";
        //    else
        //        LevantamientoActivoEntidadObjeto.CodigoBarrasParticular = CodigoBarraParticular.Text.Trim();

        //    SeleccionarLevantamientoActivoGrid(CodigoBarraParticular.Text.Trim());
        //}

        //protected void SeleccionarLevantamientoActivo(LevantamientoActivoEntidad LevantamientoActivoObjetoEntidad)
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    LevantamientoActivoProceso LevantamientoActivoProcesoNegocio = new LevantamientoActivoProceso();

        //    Resultado = LevantamientoActivoProcesoNegocio.SeleccionarLevantamientoActivo(LevantamientoActivoObjetoEntidad);

        //    if (Resultado.ErrorId == 0)
        //    {
        //        if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
        //            TablaLevantamiento.CssClass = ConstantePrograma.ClaseTablaVacia;
        //        else
        //            TablaLevantamiento.CssClass = ConstantePrograma.ClaseTabla;

        //        TablaLevantamiento.DataSource = Resultado.ResultadoDatos;
        //        TablaLevantamiento.DataBind();
        //    }
        //    else
        //    {
        //        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
        //    }
        //}

        //protected DataRow SeleccionarActivoNuevo(string CodigoBarras)
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    ActivoProceso ActivoProcesoNegocio = new ActivoProceso();
        //    ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();

        //    ActivoObjetoEntidad.CodigoBarrasParticular = CodigoBarras;
        //    Resultado = ActivoProcesoNegocio.SeleccionarActivo(ActivoObjetoEntidad);

        //    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
        //        return null;
        //    else
        //        return Resultado.ResultadoDatos.Tables[0].Rows[0];
        //}

        //protected void SeleccionarLevantamientoActivoGrid(string CodigoBarras)
        //{
        //    int ActivoId = Int32.MaxValue;
        //    string NombreActivo = string.Empty;
        //    string NumeroSerie = string.Empty;
        //    string Modelo = string.Empty;
        //    string Color = string.Empty;
        //    string NombreEstatus = string.Empty;
        //    int Levantamiento = 0;
        //    Image imagen;
        //    bool RenglonLocalizado = false;

        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    LevantamientoActivoProceso LevantamientoActivoProcesoNegocio = new LevantamientoActivoProceso();
        //    LevantamientoActivoEntidad LevantamientoActivoObjetoEntidad = new LevantamientoActivoEntidad();

        //    if (CodigoBarraParticular.Text.Trim() != "")
        //    {
        //        foreach (GridViewRow Renglon in TablaLevantamiento.Rows)
        //        {
        //            if (TablaLevantamiento.DataKeys[Renglon.RowIndex]["CodigoBarrasParticular"].ToString() == CodigoBarras)
        //            {
        //                imagen = (Image)Renglon.FindControl("IconoRevisado");
        //                imagen.ImageUrl = ("/Imagen/Icono/IconoRevisado.jpg");
        //                TablaLevantamiento.Rows[Renglon.RowIndex].Cells[7].Text = "24";
        //                RenglonLocalizado = true;
        //                break;
        //            }
        //        }

        //        if (!RenglonLocalizado)
        //            FomatoDatatable(CodigoBarras);
        //    }
        //    else
        //    {
        //        EtiquetaMensaje.Text = "";  // ToDo: poner un mensaje de error
        //    }
        //}

        //protected void FomatoDatatable(string CodigoBarras)
        //{
        //    Image imagen;
        //    //string EstatusLevantamiento;
        //    LinkButton LigaNombre;
        //    DataTable dt = new DataTable();
        //    DataRow dr;
        //    DataRow drNew;

        //    dt.Columns.Add("ActivoId", typeof(int));
        //    dt.Columns.Add("Descripcion", typeof(string));
        //    dt.Columns.Add("NumeroSerie", typeof(string));
        //    dt.Columns.Add("Modelo", typeof(string));
        //    dt.Columns.Add("Color", typeof(string));
        //    dt.Columns.Add("CodigoBarrasParticular", typeof(string));
        //    dt.Columns.Add("NombreEstatus", typeof(string));
        //    dt.Columns.Add("Levantamiento", typeof(string));


        //    foreach (GridViewRow Renglon in TablaLevantamiento.Rows)
        //    {
        //        dr = dt.NewRow();
        //        dr[0] = int.Parse(TablaLevantamiento.DataKeys[Renglon.RowIndex]["ActivoId"].ToString());
        //        LigaNombre = (LinkButton)Renglon.FindControl("LigaNombre");
        //        dr[1] = LigaNombre.Text;        //Descripcion
        //        dr[2] = Renglon.Cells[1].Text;  // Número de serie
        //        dr[3] = Renglon.Cells[2].Text;  // Modelo
        //        dr[4] = Renglon.Cells[3].Text;  // Color
        //        dr[5] = TablaLevantamiento.DataKeys[Renglon.RowIndex]["CodigoBarrasParticular"].ToString();
        //        dr[6] = Renglon.Cells[5].Text;  // Nombre estatus
        //        dr[7] = Renglon.Cells[7].Text;  //IdEstatus                
        //        dt.Rows.Add(dr);
        //    }

        //    drNew = SeleccionarActivoNuevo(CodigoBarras);
        //    //aqui es cuando solo encuentra el activo pero no corresponde al empleado
        //    if (drNew != null)
        //    {
        //        dr = dt.NewRow();
        //        dr[0] = int.Parse(drNew["ActivoId"].ToString());
        //        dr[1] = drNew["Descripcion"].ToString();
        //        dr[2] = drNew["NumeroSerie"].ToString();
        //        dr[3] = drNew["Modelo"].ToString();
        //        dr[4] = drNew["Color"].ToString();
        //        dr[5] = drNew["CodigoBarrasParticular"].ToString();
        //        //dr[6] = drNew["NombreEstatus"].ToString();
        //        dr[7] = "26";
        //        dt.Rows.Add(dr);
        //        //dt.ImportRow(dr);
        //    }

        //    TablaLevantamiento.DataSource = dt;
        //    TablaLevantamiento.DataBind();

        //    foreach (GridViewRow Renglon in TablaLevantamiento.Rows)
        //    {
        //        //if (Renglon.Cells[7].Text == "24" || Renglon.Cells[7].Text == "26")
        //        if (Renglon.Cells[7].Text == "24")
        //        {
        //            imagen = (Image)Renglon.FindControl("IconoRevisado");
        //            imagen.ImageUrl = ("/Imagen/Icono/IconoRevisado.jpg");
        //        }
        //        if (Renglon.Cells[7].Text == "26")
        //        {
        //            imagen = (Image)Renglon.FindControl("IconoRevisado");
        //            imagen.ImageUrl = ("/Imagen/Icono/IconoDuda.jpg");
        //        }
        //    }
        //}

        //protected void SeleccionarEmpleado()
        //{
        //    if (NumeroEmpleado.Text.Trim() == "")
        //        EmpleadoIdHidden.Value = "0";
        //    else
        //        SeleccionarEmpleado(NumeroEmpleado.Text.Trim());
        //}

        //protected void SeleccionarEmpleado(string NumeroEmpleado)
        //{

        //}

        //protected void LimpiaCampo()
        //{
        //    NumeroEmpleado.Text = null;
        //    CodigoBarraParticular.Text = "";
        //    NombreEmpleado.Text = "";
        //    NumeroEmpleado.Enabled = true;
        //    CodigoBarraParticular.Enabled = false;
        //    TablaLevantamiento.DataSource = null;
        //    TablaLevantamiento.DataBind();
        //    //BotonImprimir.Visible = false;
        //}

        
        //protected void AgregaLevantamiento()
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    LevantamientoActivoProceso LevantamientoActivoProcesoNegocio = new LevantamientoActivoProceso();
        //    LevantamientoActivoEntidad LevantamientoActivoObjetoEntidad = new LevantamientoActivoEntidad();


        //    foreach (GridViewRow Renglon in TablaLevantamiento.Rows)
        //    {
        //        if (TablaLevantamiento.Rows.Count > 0)
        //        {
        //            if (Renglon.Cells[7].Text == "26" || Renglon.Cells[7].Text == "0")
        //            {
        //                LevantamientoActivoObjetoEntidad.ActivoId = Int16.Parse(TablaLevantamiento.DataKeys[Renglon.RowIndex]["ActivoId"].ToString());
        //                //LevantamientoActivoObjetoEntidad.EmpleadoId = int.Parse(NumeroEmpleado.Text.Trim());
        //                LevantamientoActivoObjetoEntidad.EmpleadoId =  int.Parse(EmpleadoIdHidden.Value);
        //                LevantamientoActivoObjetoEntidad.EstatusId = Int16.Parse(Renglon.Cells[7].Text);

        //                //LevantamientoActivoProcesoNegocio.SeleccionarLevantamientoActivoEstatusId(LevantamientoActivoObjetoEntidad);
        //            }
        //        }
            
        //    }
        //    //LimpiaCampo();
        //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "OpenRequest(" + no_requisicion.ToString() + ")", true);
        //  }   
        #endregion
       
    }
}

