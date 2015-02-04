using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using Activos.Comun.Constante;
using Activos.Comun.Fecha;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Almacen;

namespace Almacen.Web.Aplicacion.Almacen
{
    public partial class PreOrden : System.Web.UI.Page
    {
        #region "Eventos"
        protected void AdvancedSearchLink_Click(Object sender, System.EventArgs e)
        {

        }

        protected void BotonAgregar_Click(object sender, ImageClickEventArgs e)
        {
            AgregarProducto();
        }

        protected void BotonGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ValidarFormulario()) GuardarPreOrden();
            }
        }

        protected void BotonLimpiarRegistro_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                LimpiarFormulario();
            }
        }
        
        //protected void LinkBuscarClave_SelectedTextChanged(object sender, EventArgs e)
        //{
        //    SeleccionarClave();
        //}

        protected void NuevoRegistro_Click(Object sender, System.EventArgs e)
        {
            CambiarNuevoRegistro();
        }

        protected void ddlSolicitante_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscarJefe();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
            BuscarProducto();
        }

        protected void FechaPreOrden_Validate(object source, ServerValidateEventArgs args)
        {
            string strEndDate = string.Empty;
            DateTime dtEndDate;


            strEndDate = FechaPreOrdenNuevo.Text.Trim();

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

        protected void TablaPreOrden_PageIndexChanging(object source, GridViewPageEventArgs e)
        {
            SeleccionarTemporalPreOrden();
            TablaPreOrden.PageIndex = e.NewPageIndex;
            TablaPreOrden.DataBind();
        }

        protected void TablaPreOrden_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaPreOrdenEventoComando(e);
        }

        protected void ImagenBuscarClaveProducto_Click(object sender, ImageClickEventArgs e)
        {
            CargaPanelVisibleProducto();
        }

        protected void TablaProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BuscarProducto();
            TablaProducto.PageIndex = e.NewPageIndex;
            TablaProducto.DataBind();
        }

        protected void TablaProducto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaProductoRowCommand(e);
        }

        protected void BotonProductoBusqueda_Click(object sender, ImageClickEventArgs e)
        {
            BuscarProducto();
        }

        protected void BotonCerrarProductoBusqueda_Click(object sender, ImageClickEventArgs e)
        {
            CargaPanelInVisibleProducto();
        }

        #endregion

        #region "Métodos"
        protected void AgregarProducto()
        {
            if (!ValidarAgregarProducto()) return;
            TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad = new TemporalPreOrdenEntidad();

            TemporalPreOrdenObjetoEntidad.PreOrdenId = TemporalPreOrdenIdHidden.Value;
            TemporalPreOrdenObjetoEntidad.TemporalPreOrdenId = TemporalPreOrdenIdHidden.Value;
            TemporalPreOrdenObjetoEntidad.EmpleadoId = Int16.Parse(SolicitanteIdNuevo.SelectedValue);
            //TemporalPreOrdenObjetoEntidad.JefeId = Int16.Parse(JefeInmediatoIdNuevo.Text);
            TemporalPreOrdenObjetoEntidad.ClaveProducto = ClaveNuevo.Text.Trim();

            
            TemporalPreOrdenObjetoEntidad.EstatusId = Convert.ToInt16(ConstantePrograma.EstatusPreOrden.SinOC);
            TemporalPreOrdenObjetoEntidad.ProductoId = ProductoIdHidden.Value;
            TemporalPreOrdenObjetoEntidad.Cantidad = Int16.Parse(CantidadNuevo.Text.Trim());

            if (!(FechaPreOrdenNuevo.Text.Trim() == ""))
                TemporalPreOrdenObjetoEntidad.FechaPreOrden = FormatoFecha.AsignarFormato(FechaPreOrdenNuevo.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);

            AgregarProducto(TemporalPreOrdenObjetoEntidad);
        }

        protected void AgregarProducto(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalPreOrdenProceso TemporalPreOrdenProcesoNegocio = new TemporalPreOrdenProceso();

            InsertarTemporalPreOrdenEncabezadoTemp(TemporalPreOrdenObjetoEntidad);

            Resultado = TemporalPreOrdenProcesoNegocio.AgregarTemporalPreOrden(TemporalPreOrdenObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenGuardadoCorrectamente)
            {
                TemporalPreOrdenIdHidden.Value = TemporalPreOrdenObjetoEntidad.PreOrdenId;
                LimpiarProducto();

                SeleccionarTemporalPreOrden();

                //NO DEJAR QUE LA FECHA SEA MODIFICADA
                FechaPreOrdenNuevo.Enabled = false;
                SolicitanteIdNuevo.Enabled = false;
            }
            else
            {
                EtiquetaMensaje.Text = Resultado.DescripcionError;
            }
        }

        protected void InsertarTemporalPreOrdenEncabezadoTemp(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalPreOrdenProceso TemporalPreOrdenProcesoNegocio = new TemporalPreOrdenProceso();
            //UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

            if (TemporalPreOrdenIdHidden.Value == "")
            {
                //   UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];
                // TemporalCompraObjetoEntidad.UsuarioId = UsuarioSessionEntidad.UsuarioId;

                Resultado = TemporalPreOrdenProcesoNegocio.InsertarTemporalPreOrdenEncabezado(TemporalPreOrdenObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenGuardadoCorrectamente)
                {

                    // TemporalPreOrdenObjetoEntidad = TemporalPreOrdenObjetoEntidad.PreOrdenId;
                    // LimpiarProducto();
                }
                else
                {
                    // EtiquetaMensaje.Text = Resultado.DescripcionError;
                }
            }
        }

        private void Inicio()
        {
            if (!Page.IsPostBack)
            {
                //SeleccionarFamilia();
                //SeleccionarSubfamilia();
                //SeleccionarMarca();
                LabelTotalArticulo.Text = "0";
                SeleccionarEmpleado();
                BuscarJefe();
                MensajeConfirmacion.Value = Comparar.ReemplazarCadenaJavascript(TextoInfo.MensajeConfirmPreOrden);
                MensajeLimpieza.Value = Comparar.ReemplazarCadenaJavascript(TextoInfo.MensajeLimpiarFormulario);
                //JefeInmediatoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));

                TablaPreOrden.DataSource = null;
                TablaPreOrden.DataBind();
            }

        }

        protected Int16 BuscarJefe()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();
            Int16 EmpleadoIdJefe = 0;

            EmpleadoEntidadObjeto.EmpleadoId = Int16.Parse(SolicitanteIdNuevo.SelectedValue);

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
            return EmpleadoIdJefe;
        }

        private void CambiarNuevoRegistro()
        {
            LimpiarNuevoRegistro();
        }

        protected void GuardarPreOrden()
        {
            PreOrdenEntidad PreOrdenObjetoEntidad = new PreOrdenEntidad();
            UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

            if (TemporalPreOrdenIdHidden.Value != "0")
            {
                if (TablaPreOrden.Rows.Count > 0)
                {
                    PreOrdenObjetoEntidad.JefeId = BuscarJefe();
                    PreOrdenObjetoEntidad.PreOrdenId = TemporalPreOrdenIdHidden.Value;
                    GuardarPreOrden(PreOrdenObjetoEntidad);
                }

            }
            else
            {
                EtiquetaMensaje.Text = "Favor de agregar los Productos";
            }
        }

        protected void GuardarPreOrden(PreOrdenEntidad PreOrdenObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            PreOrdenProceso PreOrdenProcesoNegocio = new PreOrdenProceso();

            Resultado = PreOrdenProcesoNegocio.GuardarPreOrdenCompra(PreOrdenObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.PreOrden.PreOrdenGuardadoCorrectamente)
            {
                LimpiarNuevoRegistro();
                LimpiarProducto();
                //12/enero/2015 oly...agregue esta linea creo que esto muestra el msg
                MostrarMensaje(TextoInfo.MensajeNoPreOrden + ObtenerClavePreOrden(PreOrdenObjetoEntidad), ConstantePrograma.TipoMensajeSimpleAlerta);
                //MostrarMensaje(TextoInfo.MensajeGuardadoGenerico, ConstantePrograma.TipoMensajeAlerta);

                //********************************************************************************************
            }
            else
            {
                EtiquetaMensaje.Text = Resultado.DescripcionError;
            }
        }

        protected void SeleccionarTemporalPreOrden()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad = new TemporalPreOrdenEntidad();
            TemporalPreOrdenProceso TemporalPreOrdenProcesoNegocio = new TemporalPreOrdenProceso();

            TemporalPreOrdenObjetoEntidad.PreOrdenId = TemporalPreOrdenIdHidden.Value;

            Resultado = TemporalPreOrdenProcesoNegocio.SeleccionarPreOrdenDetalleTemp(TemporalPreOrdenObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaPreOrden.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaPreOrden.CssClass = ConstantePrograma.ClaseTabla;

                int CantidadTotal = 0;
                foreach (DataRow Fila in Resultado.ResultadoDatos.Tables[0].Rows)
                {
                    CantidadTotal += Convert.ToInt32(Fila["Cantidad"]);
                }
                LabelTotalArticulo.Text = CantidadTotal.ToString();
                
                TablaPreOrden.DataSource = Resultado.ResultadoDatos;
                TablaPreOrden.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }
        }

        protected void LimpiarNuevoRegistro()
        {
            //PreOrdenNuevo.Text = "";
            FechaPreOrdenNuevo.Text = "";
            SolicitanteIdNuevo.SelectedIndex = 0;
            //JefeInmediatoIdNuevo.Items.Clear();
            JefeInmediatoNombreNuevo.Text = "";
            //JefeInmediatoIdNuevo.Text = "";
            EtiquetaMensaje.Text = "";
            LabelTotalArticulo.Text = "0";
            TablaPreOrden.DataSource = null;
            TablaPreOrden.DataBind();
            TemporalPreOrdenIdHidden.Value = "";
            ProductoIdHidden.Value = "";

        }

        protected void LimpiarProducto()
        {
            ClaveNuevo.Text = "";
            FamiliaIdNuevo.Text = "";
            SubFamiliaIdNuevo.Text = "";
            MarcaIdNuevo.Text = "";
            DescripcionNuevo.Text = "";
            CantidadNuevo.Text = "";
            ProductoIdHidden.Value = "";
            

            //TablaPreOrden.DataSource = null;
            //TablaPreOrden.DataBind();
        }

        protected void LimpiarFormulario()
        {
            LimpiarNuevoRegistro();
            LimpiarProducto();

            ProductoIdHidden.Value = "";
            ClaveIdHidden.Value = "";
            SolicitanteIdHidden.Value = "";
            TemporalPreOrdenIdHidden.Value = "";
            TemporalProducto.Value = "";
            MensajeConfirmacion.Value = "";
            LabelTotalArticulo.Text = "";
            
            TablaPreOrden.DataSource = null;
            TablaPreOrden.DataBind();
        }


        private void MostrarMensaje(string Mensaje, string TipoMensaje)
        {
            StringBuilder FormatoMensaje = new StringBuilder();

            FormatoMensaje.Append("MostrarMensaje(\"");
            FormatoMensaje.Append(Mensaje);
            FormatoMensaje.Append("\", \"");
            FormatoMensaje.Append(TipoMensaje);
            FormatoMensaje.Append("\");");

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Mensaje", Comparar.ReemplazarCadenaJavascript(FormatoMensaje.ToString()), true);
        }


        protected void SeleccionarClave()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AlmacenEntidad AlmacenEntidadObjeto = new AlmacenEntidad();
            AlmacenProceso AlmacenProcesoObjeto = new AlmacenProceso();
            bool AsignacionPermitida = true;

            AlmacenEntidadObjeto.Clave = ClaveNuevo.Text.Trim();

            Resultado = AlmacenProcesoObjeto.SeleccionarProducto(AlmacenEntidadObjeto);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                {

                    // Se valida que se pueda asignar el producto
                    //AsignacionPermitida = AlmacenProcesoObjeto.ValidarAsignacionProducto(int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ProductoId"].ToString()));

                    if (AsignacionPermitida == true)
                    {
                        FamiliaIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString();
                        //SeleccionarSubfamilia();
                        SubFamiliaIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["SubFamiliaId"].ToString();
                        MarcaIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["MarcaId"].ToString();
                        DescripcionNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreProducto"].ToString();
                        //CantidadNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["MaximoPermitido"].ToString();
                        ProductoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ProductoId"].ToString();

                        AgregarEtiquetaMensaje.Text = "";
                    }
                    else
                    {
                        LimpiarProducto();
                        AgregarEtiquetaMensaje.Text = TextoError.EstatusActivoIncorrecto;
                        ClaveNuevo.Focus();
                    }


                }
                else
                {
                    LimpiarProducto();
                    AgregarEtiquetaMensaje.Text = TextoError.NoExisteActivo;
                    ClaveNuevo.Focus();
                }
            }
            else
            {
                LimpiarProducto();
                AgregarEtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

        }

        protected void SeleccionarEmpleado()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

            //    EmpleadoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusEmpleados.Activo;

            Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

            SolicitanteIdNuevo.DataValueField = "EmpleadoId";
            SolicitanteIdNuevo.DataTextField = "NombreEmpleadoCompleto";

            if (Resultado.ErrorId == 0)
            {
                SolicitanteIdNuevo.DataSource = Resultado.ResultadoDatos;
                SolicitanteIdNuevo.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            SolicitanteIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        //protected void SeleccionarFamilia()
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
        //    FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

        //    //FamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusFamilia.Activo;

        //    Resultado = FamiliaProcesoObjeto.SeleccionarFamilia(FamiliaEntidadObjeto);

        //    FamiliaIdNuevo.DataValueField = "FamiliaId";
        //    FamiliaIdNuevo.DataTextField = "Nombre";

        //    if (Resultado.ErrorId == 0)
        //    {
        //        FamiliaIdNuevo.DataSource = Resultado.ResultadoDatos;
        //        FamiliaIdNuevo.DataBind();
        //    }
        //    else
        //    {
        //        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
        //    }

        //    FamiliaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        //}

        public void SeleccionarJefe(Int16 EmpleadoIdJefe)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

            if (EmpleadoIdJefe != 0)
            {
                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoIdJefe;

                if (EmpleadoEntidadObjeto.EmpleadoId == 0)
                {
                    //JefeInmediatoIdNuevo.Text = "";
                    JefeInmediatoNombreNuevo.Text = "";
                    //JefeInmediatoIdNuevo.Items.Clear();
                }
                else
                {

                    Resultado = EmpleadoProcesoNegocio.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                    //JefeInmediatoIdNuevo.DataValueField = "EmpleadoIdJefe";
                    //JefeInmediatoIdNuevo.DataTextField = "Nombre";

                    if (Resultado.ErrorId == 0)
                    {
                        //JefeInmediatoIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoIdJefe"].ToString();



                        JefeInmediatoNombreNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                        //JefeInmediatoIdNuevo.DataSource = Resultado.ResultadoDatos;
                        //JefeInmediatoIdNuevo.DataBind();
                    }
                    else
                    {
                        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    }


                    //JefeInmediatoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
                }
            }
            else
            {
                JefeInmediatoNombreNuevo.Text = "";
            }
        }

        //protected void SeleccionarMarca()
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    MarcaEntidad MarcaEntidadObjeto = new MarcaEntidad();
        //    MarcaProceso MarcaProcesoObjeto = new MarcaProceso();

        //    //MarcaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusMarca.Activo;

        //    Resultado = MarcaProcesoObjeto.SeleccionarMarca(MarcaEntidadObjeto);

        //    MarcaIdNuevo.DataValueField = "MarcaId";
        //    MarcaIdNuevo.DataTextField = "Nombre";

        //    if (Resultado.ErrorId == 0)
        //    {
        //        MarcaIdNuevo.DataSource = Resultado.ResultadoDatos;
        //        MarcaIdNuevo.DataBind();
        //    }
        //    else
        //    {
        //        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
        //    }

        //    MarcaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        //}

        //protected void SeleccionarSubfamilia()
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();
        //    SubFamiliaProceso SubFamiliaProcesoObjeto = new SubFamiliaProceso();

        //    //SubFamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusSubFamilia.Activo;
        //    SubFamiliaEntidadObjeto.FamiliaId = Int16.Parse(FamiliaIdNuevo.SelectedValue);

        //    if (SubFamiliaEntidadObjeto.FamiliaId == 0)
        //    {
        //        SubFamiliaIdNuevo.Items.Clear();
        //    }
        //    else
        //    {
        //        Resultado = SubFamiliaProcesoObjeto.SeleccionarSubFamilia(SubFamiliaEntidadObjeto);

        //        SubFamiliaIdNuevo.DataValueField = "SubFamiliaId";
        //        SubFamiliaIdNuevo.DataTextField = "Nombre";

        //        if (Resultado.ErrorId == 0)
        //        {
        //            SubFamiliaIdNuevo.DataSource = Resultado.ResultadoDatos;
        //            SubFamiliaIdNuevo.DataBind();
        //        }
        //        else
        //        {
        //            EtiquetaMensaje.Text = TextoError.ErrorGenerico;
        //        }
        //    }

        //    SubFamiliaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        //}

        protected void TablaPreOrdenEventoComando(GridViewCommandEventArgs e)
        {
            Int16 intFila = 0;
            int intTamañoPagina = 0;
            string ProductoId = string.Empty;
            string strCommand = string.Empty;

            intFila = Int16.Parse(e.CommandArgument.ToString());
            strCommand = e.CommandName.ToString();
            intTamañoPagina = TablaPreOrden.PageSize;

            if (intFila >= intTamañoPagina)
                intFila = (Int16)(intFila - (intTamañoPagina * TablaPreOrden.PageIndex));

            switch (strCommand)
            {
                case "Select":
                    ProductoId = string.Format(TablaPreOrden.DataKeys[intFila]["ProductoId"].ToString());

                    if (ProductoIdHidden.Value == "0")
                    {
                        // SeleccionarPreOrdenParaEditar(ProductoId);
                        //CambiarBotonesActualizar();
                    }
                    else
                    {
                        EtiquetaMensaje.Text = "Favor de finalizar.";
                    }
                    break;

                case "EliminarPreOrden":
                    ProductoId = string.Format(TablaPreOrden.DataKeys[intFila]["ProductoId"].ToString());
                    EliminarProducto(ProductoId);
                    break;

                default:
                    // Do nothing
                    break;
            }
        }

        protected void EliminarProducto(string ProductoId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad = new TemporalPreOrdenEntidad();
            TemporalPreOrdenProceso TemporalPreOrdenProcesoNegocio = new TemporalPreOrdenProceso();

            //if (ProductoIdHidden.Value == ProductoId.ToString())
            //{
            TemporalPreOrdenObjetoEntidad.ProductoId = ProductoId;
            Resultado = TemporalPreOrdenProcesoNegocio.CancelarNuevoPreOrden(TemporalPreOrdenObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenEliminadoCorrectamente)
            {
                EtiquetaMensaje.Text = "";
                SeleccionarTemporalPreOrden();

            }
            else
            {
                EtiquetaMensaje.Text = Resultado.DescripcionError;
            }
            //}

        }

        private void ShowMessage(string Mensaje, string TipoMensaje)
        {
            StringBuilder FormatoMensaje = new StringBuilder();

            FormatoMensaje.Append("MostrarMensaje(\"");
            FormatoMensaje.Append(Mensaje);
            FormatoMensaje.Append("\", \"");
            FormatoMensaje.Append(TipoMensaje);
            FormatoMensaje.Append("\");");

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Mensaje", Comparar.ReemplazarCadenaJavascript(FormatoMensaje.ToString()), true);
        }

        private Boolean ValidarFormulario()
        {
            String Mensaje = "";
            DateTime Temporal = new DateTime();
            if (!DateTime.TryParse(FechaPreOrdenNuevo.Text, out Temporal)) Mensaje = TextoInfo.MensajeFechaGenerico;
            if (SolicitanteIdNuevo.SelectedIndex == 0) Mensaje = TextoInfo.MensajeSolicitanteGenerico;
            if (TablaPreOrden.Rows.Count <= 0) Mensaje = TextoInfo.MensajeProductoGenerico;

            if (Mensaje == "") return true;
            else MostrarMensaje(Mensaje, "Error");
            return false;
        }

        private Boolean ValidarAgregarProducto()
        {
            String Mensaje = "";
            Int16 NumeroTemporal = 0;
            if (!Int16.TryParse(CantidadNuevo.Text, out NumeroTemporal)) Mensaje = TextoInfo.MensajeCantidadGenerico;
            if (NumeroTemporal == 0) Mensaje = TextoInfo.MensajeCantidadGenerico;
            if (String.IsNullOrEmpty(ClaveNuevo.Text)) Mensaje = TextoInfo.MensajeClaveGenerico;
            DateTime Temporal = new DateTime();
            if (!DateTime.TryParse(FechaPreOrdenNuevo.Text, out Temporal)) Mensaje = TextoInfo.MensajeFechaGenerico;
            if (SolicitanteIdNuevo.SelectedIndex == 0) Mensaje = TextoInfo.MensajeSolicitanteGenerico;

            if (Mensaje == "") return true;
            else MostrarMensaje(Mensaje, "Error");
            return false;
        }

        private void CargaPanelVisibleProducto()
        {
            PanelBusquedaProducto.Visible = !PanelBusquedaProducto.Visible;
            pnlFondoBuscarProducto.Visible = !pnlFondoBuscarProducto.Visible;
        }

        private void SeleccionarProductoMostrar(AlmacenEntidad AlmacenEntidadObjeto)
        {

            ResultadoEntidad Resultado = new ResultadoEntidad();
            AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

            Resultado = AlmacenProcesoNegocio.SeleccionarProductoparaEditar(AlmacenEntidadObjeto);

            if (Resultado.ErrorId == 0)
            {
                ClaveNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Clave"].ToString();
                FamiliaIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Familia"].ToString();
                SubFamiliaIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["SubFamilia"].ToString();
                MarcaIdNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Marca"].ToString();
                DescripcionNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreProducto"].ToString();
                CargaPanelInVisibleProducto();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

        }

        private void CargaPanelInVisibleProducto()
        {
            PanelBusquedaProducto.Visible = false;
            pnlFondoBuscarProducto.Visible = false;

        }

        private void BuscarProducto()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AlmacenEntidad AlmacenObjetoEntidad = new AlmacenEntidad();
            AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

            AlmacenObjetoEntidad.Clave = ClaveProductoBusqueda.Text.Trim();
            AlmacenObjetoEntidad.Descripcion = NombreProductoBusqueda.Text.Trim();

            Resultado = AlmacenProcesoNegocio.SeleccionarProducto(AlmacenObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaProducto.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaProducto.CssClass = ConstantePrograma.ClaseTabla;

                TablaProducto.DataSource = Resultado.ResultadoDatos;
                TablaProducto.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                // MostrarMensaje(AlmacenProcesoNegocio.DescripcionError, ConstantePrograma.TipoErrorAlerta);
            }
        }

        private void TablaProductoRowCommand(GridViewCommandEventArgs e)
        {
            AlmacenEntidad AlmacenEntidadObjeto = new AlmacenEntidad();
            Int16 intFila = 0;
            int intTamañoPagina = 0;
            string ProductoId = "";
            string strCommand = string.Empty;

            intFila = Int16.Parse(e.CommandArgument.ToString());
            strCommand = e.CommandName.ToString();
            intTamañoPagina = TablaProducto.PageSize;

            if (intFila >= intTamañoPagina)
                intFila = (Int16)(intFila - (intTamañoPagina * TablaProducto.PageIndex));


            switch (strCommand)
            {
                case "Select":
                    ProductoId = string.Format(TablaProducto.DataKeys[intFila]["ProductoId"].ToString());
                    AlmacenEntidadObjeto.ProductoId = ProductoId;
                    ProductoIdHidden.Value = ProductoId.ToString();
                    SeleccionarProductoMostrar(AlmacenEntidadObjeto);
                    break;

                default:
                    // Do nothing
                    break;
            }
        }

        private string ObtenerClavePreOrden(PreOrdenEntidad PreOrdenObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            PreOrdenProceso PreOrdenProcesoNegocio = new PreOrdenProceso();
            PreOrdenProcesoNegocio.PreOrdenEntidad = PreOrdenObjetoEntidad;
            Resultado = PreOrdenProcesoNegocio.SeleccionarPreOrdenEncabezado();

            if (Resultado.ResultadoDatos.Tables.Count > 0)
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0) return Resultado.ResultadoDatos.Tables[0].Rows[0]["Clave"].ToString();

            return String.Empty;
        }

        #endregion




    }
}
