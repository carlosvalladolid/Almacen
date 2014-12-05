﻿using System;
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
using Activos.Comun.Cadenas;
using Activos.Comun.Fecha;
using Activos.Entidad.Catalogo;
using Activos.Entidad.General;
using Activos.Entidad.Almacen;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Almacen;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Almacen.Aplicacion.Almacen
{
    public partial class Requisicion : System.Web.UI.Page
    {
        #region "Eventos"

        protected void NuevoRegistro_Click(Object sender, System.EventArgs e)
        {
            CambiarNuevoRegistro();
        }

        protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
        {
           GuardarRequisicion();
        }

        protected void ImagenProductoBusqueda_Click(object sender, ImageClickEventArgs e)
        {
            PanelBusquedaProducto.Visible = !PanelBusquedaProducto.Visible;
            pnlFondoBuscarProducto.Visible = !pnlFondoBuscarProducto.Visible;
        }


        protected void BotonProductoBusqueda_Click(object sender, ImageClickEventArgs e)
        {

            BuscarProducto();
        }
        protected void BotonAgregar_Click(object sender, ImageClickEventArgs e)
        {
            AgregarDetalleDocumento();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        //protected void LinkBuscarClave_SelectedTextChanged(object sender, EventArgs e)
        //{
        //    SeleccionarClave();
        //}

        protected void TablaRequisicion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaRequisicionEventoComando(e);
        }
        
        protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSubfamilia();
        }

        protected void TablaProducto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaProductoRowCommand(e);
        }
      
        #endregion

        #region "Métodos"

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
            }

        }

        //private void CambiarBusquedaAvanzada()
        //{
        //    PanelBusquedaAvanzada.Visible = !PanelBusquedaAvanzada.Visible;
        //    PanelNuevoRegistro.Visible = false;
        //}

        //protected void CambiarEditarRegistro()
        //{
        //    PanelBusquedaAvanzada.Visible = false;
        //    PanelNuevoRegistro.Visible = true;
        //}

        private void CambiarNuevoRegistro()
        {
            //PanelBusquedaAvanzada.Visible = false;
            PanelNuevoRegistroSolicitante.Visible = !PanelNuevoRegistroSolicitante.Visible;
            LimpiarNuevoRegistro();
        }

        protected void AgregarDetalleDocumento()
        {

            RequisicionEntidad RequisicionObjetoEntidad = new RequisicionEntidad();

            if (TemporalRequisicionIdHidden.Value == "")
            {
                RequisicionObjetoEntidad.RequisicionId = TemporalRequisicionIdHidden.Value;
                RequisicionObjetoEntidad.TemporalRequisicionId = TemporalRequisicionIdHidden.Value;
                RequisicionObjetoEntidad.ProductoId = ProductoIdHidden.Value;
                RequisicionObjetoEntidad.Cantidad = Int16.Parse(CantidadNuevo.Text.Trim());

                AgregarRequisicion(RequisicionObjetoEntidad);
            }
        }

        protected void AgregarRequisicion(RequisicionEntidad RequisicionObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

            Resultado = RequisicionProcesoNegocio.AgregarRequisicionDetalle(RequisicionObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Requisicion.RequisicionGuardadoCorrectamente)
            {
                TemporalRequisicionIdHidden.Value = RequisicionObjetoEntidad.RequisicionId;
                // LimpiarNuevoRegistro();
                LimpiarRequisicion();
                SeleccionarRequisicion();
            }
            else
            {

                EtiquetaMensaje.Text = Resultado.DescripcionError;
                LimpiarRequisicion();

            }
        }

        protected void GuardarRequisicion()
        {
            RequisicionEntidad RequisicionObjetoEntidad = new RequisicionEntidad();

            RequisicionObjetoEntidad.RequisicionId = TemporalRequisicionIdHidden.Value;
            RequisicionObjetoEntidad.EmpleadoId = Int16.Parse(EmpleadoIdHidden.Value);
            RequisicionObjetoEntidad.JefeId = Int16.Parse(JefeIdHidden.Value);

            GuardarRequisicion(RequisicionObjetoEntidad);

        }

        protected void GuardarRequisicion(RequisicionEntidad RequisicionObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

            Resultado = RequisicionProcesoNegocio.AgregarRequisicionEncabezado(RequisicionObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Requisicion.RequisicionGuardadoCorrectamente)
            {
                //  TemporalRecepcionIdHidden.Value = RecepcionObjetoEntidad.RecepcionId;
                LimpiarNuevoRegistro();
                LimpiarRequisicion();

            }
            else
            {
                EtiquetaMensaje.Text = Resultado.DescripcionError;
            }

        }

        protected void CargarInformacionUsuario()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();
            RequisicionEntidad RequisicionObjetoEntidad = new RequisicionEntidad();
            UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

            UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

            RequisicionObjetoEntidad.EmpleadoId = UsuarioSessionEntidad.UsuarioId;

            Resultado = RequisicionProcesoNegocio.SeleccionarEmpleado(RequisicionObjetoEntidad);


            if (Resultado.ErrorId == 0)
            {
                EmpleadoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                SolicitanteNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
                DependenciaNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Dependencia"].ToString();
                DireccionNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Direccion"].ToString();
                PuestoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Puesto"].ToString();
                //JefeInmediatoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Jefe"].ToString();
                JefeIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoIdJefe"].ToString();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }
        }

        //protected void BuscarEmpleado(RequisicionEntidad RequisicionObjetoEntidad)
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

        //    Resultado = RequisicionProcesoNegocio.SeleccionarEmpleado(RequisicionObjetoEntidad);

        //    if (Resultado.ErrorId == 0)
        //    {
        //        if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
        //            TablaEmpleado.CssClass = ConstantePrograma.ClaseTablaVacia;
        //        else
        //            TablaEmpleado.CssClass = ConstantePrograma.ClaseTabla;

        //        TablaEmpleado.DataSource = Resultado.ResultadoDatos;
        //        TablaEmpleado.DataBind();
        //    }
        //    else
        //    {
        //       // EtiquetaControlBuscarEmpleadoMensaje.Text = TextoError.ErrorGenerico;
        //    }
        //}
      
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
                    if (AsignacionPermitida == true)
                    {
                        FamiliaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString();
                        SeleccionarSubfamilia();
                        SubFamiliaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["SubFamiliaId"].ToString();
                        MarcaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["MarcaId"].ToString();
                        DescripcionNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreProducto"].ToString();
                        CantidadNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["MaximoPermitido"].ToString();
                        ProductoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ProductoId"].ToString();

                    }
                    else
                    {
                        ClaveNuevo.Focus();
                    }


                }
                else
                {
                    ClaveNuevo.Focus();
                }
            }
            else
            {
                // LimpiarProducto();
                //AgregarEtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

        }

        protected void SeleccionarFamilia()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
            FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

            //FamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusFamilia.Activo;

            Resultado = FamiliaProcesoObjeto.SeleccionarFamilia(FamiliaEntidadObjeto);

            FamiliaIdNuevo.DataValueField = "FamiliaId";
            FamiliaIdNuevo.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                FamiliaIdNuevo.DataSource = Resultado.ResultadoDatos;
                FamiliaIdNuevo.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            FamiliaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void SeleccionarMarca()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MarcaEntidad MarcaEntidadObjeto = new MarcaEntidad();
            MarcaProceso MarcaProcesoObjeto = new MarcaProceso();

            //MarcaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusMarca.Activo;

            Resultado = MarcaProcesoObjeto.SeleccionarMarca(MarcaEntidadObjeto);

            MarcaIdNuevo.DataValueField = "MarcaId";
            MarcaIdNuevo.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                MarcaIdNuevo.DataSource = Resultado.ResultadoDatos;
                MarcaIdNuevo.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            MarcaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void SeleccionarSubfamilia()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();
            SubFamiliaProceso SubFamiliaProcesoObjeto = new SubFamiliaProceso();

            //SubFamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusSubFamilia.Activo;
            SubFamiliaEntidadObjeto.FamiliaId = Int16.Parse(FamiliaIdNuevo.SelectedValue);

            if (SubFamiliaEntidadObjeto.FamiliaId == 0)
            {
                SubFamiliaIdNuevo.Items.Clear();
            }
            else
            {
                Resultado = SubFamiliaProcesoObjeto.SeleccionarSubFamilia(SubFamiliaEntidadObjeto);

                SubFamiliaIdNuevo.DataValueField = "SubFamiliaId";
                SubFamiliaIdNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    SubFamiliaIdNuevo.DataSource = Resultado.ResultadoDatos;
                    SubFamiliaIdNuevo.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            SubFamiliaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void SeleccionarRequisicion()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RequisicionEntidad RequisicionObjetoEntidad = new RequisicionEntidad();
            RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

            RequisicionObjetoEntidad.RequisicionId = TemporalRequisicionIdHidden.Value;

            Resultado = RequisicionProcesoNegocio.SeleccionaRequisicion(RequisicionObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaRequisicion.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaRequisicion.CssClass = ConstantePrograma.ClaseTabla;



                TablaRequisicion.DataSource = Resultado.ResultadoDatos;
                TablaRequisicion.DataBind();

            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }
        }

        private void Inicio()
        {
            //Master.NuevoRegistroMaster.Click += new EventHandler(NuevoRegistro_Click);
            //Master.BusquedaAvanzadaMaster.Click += new EventHandler(BusquedaAvanzadaLink_Click);
            //Master.EliminarRegistroMaster.Click += new EventHandler(EliminarRegistroLink_Click);

            if (Page.IsPostBack)
                return;

            CargarInformacionUsuario();
            SeleccionarMarca();
            SeleccionarFamilia();
            SeleccionarSubfamilia();

            TablaRequisicion.DataSource = null;
            TablaRequisicion.DataBind();

            TablaProducto.DataSource = null;
            TablaProducto.DataBind();
        }

        protected void TablaRequisicionEventoComando(GridViewCommandEventArgs e)
        {
            Int16 intFila = 0;
            int intTamañoPagina = 0;
            string ProductoId = string.Empty;
            string strCommand = string.Empty;

            intFila = Int16.Parse(e.CommandArgument.ToString());
            strCommand = e.CommandName.ToString();
            intTamañoPagina = TablaRequisicion.PageSize;

            if (intFila >= intTamañoPagina)
                intFila = (Int16)(intFila - (intTamañoPagina * TablaRequisicion.PageIndex));

            switch (strCommand)
            {
                case "EliminarRequisicion":
                    ProductoId = string.Format(TablaRequisicion.DataKeys[intFila]["ProductoId"].ToString());
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
            RequisicionEntidad RequisicionObjetoEntidad = new RequisicionEntidad();
            RequisicionProceso RequisicionProcesoNegocio = new RequisicionProceso();

            //if (ProductoIdHidden.Value == ProductoId.ToString())
            //{
            RequisicionObjetoEntidad.ProductoId = ProductoId;
            Resultado = RequisicionProcesoNegocio.CancelarNuevoRequisicion(RequisicionObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Requisicion.EliminadoExitosamente)
            {
                EtiquetaMensaje.Text = "";
                SeleccionarRequisicion();

            }
            else
            {
                EtiquetaMensaje.Text = Resultado.DescripcionError;
            }
            //}

        }

        protected void LimpiarRequisicion()
        {
            ClaveNuevo.Text = "";
            FamiliaIdNuevo.SelectedIndex = 0;
            SeleccionarSubfamilia();
            SubFamiliaIdNuevo.SelectedIndex = 0;
            MarcaIdNuevo.SelectedIndex = 0;
            DescripcionNuevo.Text = "";         
            CantidadNuevo.Text = "";           
            EtiquetaMensaje.Text = "";
            ProductoIdHidden.Value = "";


        }

        protected void LimpiarNuevoRegistro()
        {
            SolicitanteNuevo.Text = "";
            DependenciaNuevo.Text = "";
            DireccionNuevo.Text = "";
            PuestoNuevo.Text = "";
            JefeInmediatoNuevo.Text = "";
            TemporalRequisicionIdHidden.Value = "";
            EmpleadoIdHidden.Value = "";
            JefeIdHidden.Value = "";

            TablaRequisicion.DataSource = null;
            TablaRequisicion.DataBind();
            EtiquetaMensaje.Text = "";
        
        
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
                   // SeleccionarFamiliaParaEditar(FamiliaEntidadObjeto);
                    break;

                default:
                    // Do nothing
                    break;
            }
        }

        #endregion

    }
}
