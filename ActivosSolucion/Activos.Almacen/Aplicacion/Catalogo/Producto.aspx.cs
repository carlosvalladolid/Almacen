﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;


using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Almacen;




namespace Almacen.Web.Aplicacion.Catalogo
{
    public partial class Producto : System.Web.UI.Page
    {
        #region "Eventos"

            protected void BotonBusqueda_Click(object sender, EventArgs e)
            {
              
                BusquedaAvanzada();
            }


            protected void AdvancedSearchLink_Click(Object sender, System.EventArgs e)
            {

            }

            protected void BotonBusquedaRapida_Click(object sender, ImageClickEventArgs e)
            {

            }

            protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
            {
                GuardarProducto();

            }

            protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarSubfamilia();
            }


            protected void BotonLimpiar_Click(object sender, ImageClickEventArgs e)
            {

            }
            protected void BotonCancelar_Click(object sender, ImageClickEventArgs e)
            {

            }
        
            protected void DeleteRecordLink_Click(Object sender, System.EventArgs e)
            {

            }

            protected void NewRecordLink_Click(Object sender, System.EventArgs e)
            {

            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }
        

            protected void TablaProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaProducto.PageIndex = e.NewPageIndex;
                BusquedaAvanzada();
            }

            //protected void TablaProductoRowCommand(object sender, GridViewCommandEventArgs e)
            //{
            //    TablaProductoEventoComando(e);
            //}



        #endregion

        #region "Métodos"
         
        
        private void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    SeleccionarFamilia();
                    SeleccionarSubfamilia();
                    SeleccionarMarca();
                    SeleccionarUnidadMedida();
                    BusquedaAvanzada();
                }
                
            }


        protected void BusquedaAvanzada()
        {
            AlmacenEntidad AlmacenEntidadObjeto = new AlmacenEntidad();

            AlmacenEntidadObjeto.Clave = ClaveBusqueda.Text.Trim();
            AlmacenEntidadObjeto.Descripcion = DescripcionBusqueda.Text.Trim();
            //AlmacenEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

            SeleccionarProducto(AlmacenEntidadObjeto);
        }

        protected void LimpiarNuevoRegistro()
        {
            ClaveNuevo.Text = "";
            DescripcionNuevo.Text = "";
            FamiliaIdNuevo.SelectedIndex = 0;
            SeleccionarSubfamilia();
            MarcaIdNuevo.SelectedIndex = 0;
            MaximoNuevo.Text = "";
            MinimoNuevo.Text = "";
            UnidaddeMedidaIdNuevo.Text = "";
            EstatusProductoNuevo.Checked = true;
            MaximoPermitivoNuevo.Text = "";
            ProductoIdHidden.Value = "";
            EtiquetaMensaje.Text = "";
        
        }

        protected void LimpiarBusquedaRegistro()
        { 
            ClaveBusqueda.Text = "";
            DescripcionBusqueda.Text = "";
            EtiquetaMensaje.Text = "";        
        }        

        protected void GuardarProducto()
        {
            AlmacenEntidad AlmacenObjetoEntidad = new AlmacenEntidad();
            UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

            UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

            AlmacenObjetoEntidad.ProductoId = ProductoIdHidden.Value;
            AlmacenObjetoEntidad.Clave = ClaveNuevo.Text.Trim();
            AlmacenObjetoEntidad.FamiliaId = Int16.Parse(FamiliaIdNuevo.SelectedValue);
            AlmacenObjetoEntidad.SubFamiliaId = Int16.Parse(SubFamiliaIdNuevo.SelectedValue);
            AlmacenObjetoEntidad.MarcaId = Int16.Parse(MarcaIdNuevo.SelectedValue);
            AlmacenObjetoEntidad.Descripcion = DescripcionNuevo.Text.Trim();
            AlmacenObjetoEntidad.Minimo = Int16.Parse(MinimoNuevo.Text.Trim());
            AlmacenObjetoEntidad.Maximo = Int16.Parse(MaximoNuevo.Text.Trim());
            AlmacenObjetoEntidad.MaximoPermitido = Int16.Parse(MaximoPermitivoNuevo.Text.Trim());
            AlmacenObjetoEntidad.UnidadMedidaId = (UnidaddeMedidaIdNuevo.SelectedValue);
            AlmacenObjetoEntidad.EstatusId = EstatusProductoNuevo.Checked;

            GuardarProducto(AlmacenObjetoEntidad);
        }

        protected void GuardarProducto(AlmacenEntidad AlmacenObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

            Resultado = AlmacenProcesoNegocio.GuardarProducto(AlmacenObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Producto.ProductoGuardadoCorrectamente)
            {
               LimpiarNuevoRegistro();
               BusquedaAvanzada();
            }
            else
            {
                EtiquetaMensaje.Text = Resultado.DescripcionError;
            }
        }

        private void SeleccionarProducto(AlmacenEntidad AlmacenObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

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

        protected void SeleccionarUnidadMedida()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            UnidadMedidaEntidad UnidadMedidaEntidadObjeto = new UnidadMedidaEntidad();
            UnidadMedidaProceso UnidadMedidaProcesoObjeto = new UnidadMedidaProceso();

            //MarcaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusMarca.Activo;

            Resultado = UnidadMedidaProcesoObjeto.SeleccionarUnidadMedida(UnidadMedidaEntidadObjeto);


            UnidaddeMedidaIdNuevo.DataValueField = "UnidadMedidaId";
            UnidaddeMedidaIdNuevo.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                UnidaddeMedidaIdNuevo.DataSource = Resultado.ResultadoDatos;
                UnidaddeMedidaIdNuevo.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            UnidaddeMedidaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void TablaProductoEventoComando(GridViewCommandEventArgs e)
        {
            AlmacenEntidad AlmacenEntidadObjeto = new AlmacenEntidad();
            string intFila = "";
            int intTamañoPagina = 0;
            string ProductoId = "";
            string strCommand = string.Empty;

            intFila = (e.CommandArgument.ToString());
            strCommand = e.CommandName.ToString();
            intTamañoPagina = TablaProducto.PageSize;

            //if (intFila >= intTamañoPagina)
            //    intFila = (intFila - (intTamañoPagina * TablaProducto.PageIndex));


            switch (strCommand)
            {
                case "Select":
                    //ProductoId = string.Format(TablaProducto.DataKeys[intFila]["ProductoId"].ToString());
                    AlmacenEntidadObjeto.ProductoId = ProductoId;
                    ProductoIdHidden.Value = ProductoId.ToString();
                    //SeleccionarSubFamiliaParaEditar(SubFamiliaEntidadObjeto);
                    break;

                default:
                    // Do nothing
                    break;
            }
        }




        //{
        //    TablaProducto.DataSource = null;
        //    TablaProducto.DataBind();
        //}    
        #endregion
    }
}
