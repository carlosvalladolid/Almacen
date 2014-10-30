using System;
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
              
                //BusquedaAvanzada();
            }


            protected void AdvancedSearchLink_Click(Object sender, System.EventArgs e)
            {

            }

            protected void BotonBusquedaRapida_Click(object sender, ImageClickEventArgs e)
            {

            }

            protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
            {

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
        #endregion

        #region "Métodos"
         
        
        private void Inicio()
            {

                if (Page.IsPostBack)
                    return;

                SeleccionarProducto();

            }
        

        //protected void BusquedaAvanzada()
        //{
        //   ProductoEntidad ProductoEntidadObjeto = new ProductoEntidad();

        //   ProductoEntidadObjeto.Clave = ClaveBusqueda.Text.Trim();
        //   ProductoEntidadObjeto.Nombre = DescripcionBusqueda.Text.Trim();

        //   SeleccionarProducto(ProductoEntidadObjeto);
        //}


        //protected void GuardarProducto()
        //{
        //    AlmacenEntidad AlmacenObjetoEntidad = new AlmacenEntidad();
        //    UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

        //    UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

        //    AlmacenObjetoEntidad.ProductoId = ProductoIdHidden.Value;
        //    AlmacenObjetoEntidad.Clave = ClaveNuevo.Text.Trim();
        //    AlmacenObjetoEntidad.FamiliaId = Int16.Parse(FamiliaIdNuevo.SelectedValue);
        //    AlmacenObjetoEntidad.SubFamiliaId = Int16.Parse(SubFamiliaIdNuevo.SelectedValue);
        //    AlmacenObjetoEntidad.MarcaId = Int16.Parse(MarcaIdNuevo.SelectedValue);
        //    AlmacenObjetoEntidad.Descripcion = DescripcionNuevo.Text.Trim();
        //    AlmacenObjetoEntidad.Minimo = Int16.Parse(MinimoNuevo.Text.Trim());
        //    AlmacenObjetoEntidad.Maximo = Int16.Parse(MaximoNuevo.Text.Trim());
        //    AlmacenObjetoEntidad.MaximoPermitido = Int16.Parse(MaximoPermitivoNuevo.Text.Trim());
        //    AlmacenObjetoEntidad.EstatusId =EstatusProductoNuevo.Checked;
                       
        //    GuardarProducto(AlmacenObjetoEntidad);
        //}

        //protected void GuardarProducto(AlmacenEntidad AlmacenObjetoEntidad)
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

        //    Resultado = AlmacenProcesoNegocio.GuardarProducto(AlmacenObjetoEntidad);

        //    if (Resultado.ErrorId == (int)ConstantePrograma.Producto.ProductoGuardadoCorrectamente)
        //    {
        //        //LimpiarNuevoRegistro();
        //        //PanelNuevoRegistro.Visible = false;
        //        //PanelBusquedaAvanzada.Visible = false;
        //        //BusquedaAvanzada();
        //    }
        //    else
        //    {
        //        EtiquetaMensaje.Text = Resultado.DescripcionError;
        //    }
        //}




        private void SeleccionarProducto()
        {
            TablaProducto.DataSource = null;
            TablaProducto.DataBind();
        }    
        #endregion
    }
}
