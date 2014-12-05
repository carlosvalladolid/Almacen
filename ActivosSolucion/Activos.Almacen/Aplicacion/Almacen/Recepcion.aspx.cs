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
using Activos.Comun.Cadenas;
using Activos.Comun.Fecha;
using Activos.Entidad.Catalogo;
using Activos.Entidad.General;
using Activos.Entidad.Almacen;
//using Activos.Entidad.Activos;
using Activos.ProcesoNegocio.Almacen;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Almacen.Aplicacion.Almacen
{
    public partial class Recepcion : System.Web.UI.Page
    {

        #region "Eventos"

        protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
        {
            //if (Page.IsValid)
            //{
                GuardarRecepcion();
            //}
        }

        protected void BotonAgregar_Click(object sender, ImageClickEventArgs e)
        {
            AgregarDetalleDocumento();

        }

        protected void CalcularMonto_Click(object sender, EventArgs e)
        {
         
            int PrecioUnitario = 0;
            int Cantidad = 0;
            int Resultado = 0;

            if (PrecionUnitarioNuevo.Text != "0")
            {
                PrecioUnitario = int.Parse(PrecionUnitarioNuevo.Text.Trim());
                Cantidad = int.Parse(CantidadNuevo.Text.Trim());
                Resultado = PrecioUnitario * Cantidad;

                MontoDocumentoNuevo.Text = Resultado.ToString();
            }
            else
            {

                EtiquetaMensaje.Text = "Capture el Precio";
                PrecionUnitarioNuevo.Focus();

            }

        }

        protected void SolicitanteCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarJefe(Int16.Parse(SolicitanteIdNuevo.SelectedValue));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSubfamilia();
        }

        protected void LinkBuscarClave_SelectedTextChanged(object sender, EventArgs e)
        {
            SeleccionarClave();
        }

        protected void LinkBuscarOrdenCompra_SelectedTextChanged(object sender, EventArgs e)
        {
            SeleccionarOrdenCompra();
        }

        protected void TablaRecepcion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaRecepcionEventoComando(e);
        }

        #endregion

        #region "Métodos"

        protected void AgregarDetalleDocumento()
        {
            RecepcionEntidad RecepcionObjetoEntidad = new RecepcionEntidad();          
            if (TemporalRecepcionIdHidden.Value == "")
            {
                RecepcionObjetoEntidad.RecepcionId = TemporalRecepcionIdHidden.Value;
                RecepcionObjetoEntidad.TemporalRecepcionId = TemporalRecepcionIdHidden.Value;
                RecepcionObjetoEntidad.ProductoId = ProductoIdHidden.Value;
                RecepcionObjetoEntidad.PrecioUnitario = decimal.Parse(PrecionUnitarioNuevo.Text);
                RecepcionObjetoEntidad.Cantidad = MontoDocumentoNuevo.Text.Trim();

                AgregarRecepcion(RecepcionObjetoEntidad);
            }
        }

        protected void AgregarRecepcion(RecepcionEntidad RecepcionObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RecepcionProceso RecepcionProcesoNegocio = new RecepcionProceso();
           
            Resultado = RecepcionProcesoNegocio.AgregarRecepcionDetalle(RecepcionObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Recepcion.RecepcionGuardadoCorrectamente)
            {
                TemporalRecepcionIdHidden.Value = RecepcionObjetoEntidad.RecepcionId;              
                LimpiarRecepcion();          
                SeleccionarRecepcion();
            }
            else
            {
                //MostrarMensaje(RecepcionProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);  
                LimpiarRecepcion();                
            }
        }
     
        protected void GuardarRecepcion()
        {
            RecepcionEntidad RecepcionObjetoEntidad = new RecepcionEntidad();

            RecepcionObjetoEntidad.RecepcionId = TemporalRecepcionIdHidden.Value;
            RecepcionObjetoEntidad.ProveedorId = Int16.Parse(ProveedorIdNuevo.SelectedValue);
            RecepcionObjetoEntidad.TipoDocumentoId = Int16.Parse(TipoDocumentoIdNuevo.SelectedValue);
            RecepcionObjetoEntidad.EmpleadoId = Int16.Parse(SolicitanteIdNuevo.SelectedValue);
            RecepcionObjetoEntidad.JefeId = Int16.Parse(JefeInmediatoIdNuevo.SelectedValue);
            RecepcionObjetoEntidad.Clave = FolioNuevo.Text.Trim();
            RecepcionObjetoEntidad.Monto = decimal.Parse(MontoDatosNuevo.Text);
            RecepcionObjetoEntidad.OrdenId = OrdenIdHidden.Value;
            if (!(FechaDocumentoNuevo.Text.Trim() == ""))
                RecepcionObjetoEntidad.FechaDocumento = FormatoFecha.AsignarFormato(FechaDocumentoNuevo.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);

             GuardarRecepcion(RecepcionObjetoEntidad);
        }

        protected void GuardarRecepcion(RecepcionEntidad RecepcionObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RecepcionProceso RecepcionProcesoNegocio = new RecepcionProceso();
         
            Resultado = RecepcionProcesoNegocio.AgregarRecepcionEncabezado(RecepcionObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Recepcion.RecepcionGuardadoCorrectamente)
            {             
                LimpiarNuevoRegistro();
                LimpiarRecepcion();  
            }
            else
            {
              // MostrarMensaje(RecepcionProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                
            }
         }

        protected void SeleccionarRecepcion()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RecepcionEntidad RecepcionObjetoEntidad = new RecepcionEntidad();
            RecepcionProceso RecepcionProcesoNegocio = new RecepcionProceso();

            RecepcionObjetoEntidad.RecepcionId = TemporalRecepcionIdHidden.Value;

            Resultado = RecepcionProcesoNegocio.SeleccionaRecepcion(RecepcionObjetoEntidad);
            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaRecepcion.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaRecepcion.CssClass = ConstantePrograma.ClaseTabla;



                TablaRecepcion.DataSource = Resultado.ResultadoDatos;
                TablaRecepcion.DataBind();

            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }
        }

        protected void LimpiarRecepcion()
        {
            ClaveNuevo.Text = "";
            FamiliaIdNuevo.SelectedIndex = 0;
            SeleccionarSubfamilia();
            SubFamiliaIdNuevo.SelectedIndex = 0;
            MarcaIdNuevo.SelectedIndex = 0;           
            DescripcionNuevo.Text = "";
            PrecionUnitarioNuevo.Text = "";
            CantidadNuevo.Text = "";
            MontoDocumentoNuevo.Text = "";
            EtiquetaMensaje.Text = "";
            //ProductoIdHidden.Value = "";
        
        }

        protected void LimpiarNuevoRegistro()
        {
            ProveedorIdNuevo.SelectedIndex = 0;
            TipoDocumentoIdNuevo.SelectedIndex = 0;
            FolioNuevo.Text = "";
            FechaDocumentoNuevo.Text = "";
            MontoDatosNuevo.Text = "";
            OrderCompraNuevo.Text = "";
            FechaOrdenCompraNuevo.Text = "";
            SolicitanteIdNuevo.SelectedIndex = 0;
            JefeInmediatoIdNuevo.SelectedIndex = 0;
            OrdenIdHidden.Value = "";

            TablaRecepcion.DataSource = null;
            TablaRecepcion.DataBind();


        }             
       
        private void Inicio()
        {
            if (Page.IsPostBack)
                return;
           
            SeleccionarProveedor();
            SeleccionarEmpleado();
            BuscarJefe();
            SeleccionarMarca();
            SeleccionarFamilia();
            SeleccionarSubfamilia();
            SeleccionarTipoDocumento();

            TablaRecepcion.DataSource = null;
            TablaRecepcion.DataBind();

            JefeInmediatoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void SeleccionarClave()
        {           
            PreOrdenProceso PreOrdenProcesoObjeto = new PreOrdenProceso();          

            PreOrdenProcesoObjeto.PreOrdenEntidad.Clave = ClaveNuevo.Text.Trim(); 
  
            PreOrdenProcesoObjeto.SeleccionarClaveProductoPreOrden();

            if (PreOrdenProcesoObjeto.ErrorId != 0)
            {
                MostrarMensaje(PreOrdenProcesoObjeto.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                return;
            }
           
            if (PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
            {
                //  LimpiarProducto();
            }
            else
            {
                FamiliaIdNuevo.SelectedValue = PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString();
                SeleccionarSubfamilia();
                SubFamiliaIdNuevo.SelectedValue = PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows[0]["SubFamiliaId"].ToString();
                MarcaIdNuevo.SelectedValue = PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows[0]["MarcaId"].ToString();
                DescripcionNuevo.Text = PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows[0]["NombreProducto"].ToString();
                CantidadNuevo.Text = PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows[0]["MaximoPermitido"].ToString();
                ProductoIdHidden.Value = PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows[0]["ProductoId"].ToString();
            }
        }

        protected void SeleccionarOrdenCompra()
        {
            OrdenProceso OrdenProceso = new OrdenProceso();
          
            OrdenProceso.OrdenEncabezadoEntidad.Clave = OrderCompraNuevo.Text.Trim();

            OrdenProceso.SeleccionarBusquedaOrdenCompra();

            if (OrdenProceso.ErrorId != 0)
            {
            MostrarMensaje(OrdenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
            return;    
            } 

            if (OrdenProceso.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                FechaOrdenCompraNuevo.Text = OrdenProceso.ResultadoDatos.Tables[0].Rows[0]["FechaOrden"].ToString();
                SolicitanteIdNuevo.SelectedValue = OrdenProceso.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                BuscarJefe();
                //JefeInmediatoIdNuevo.SelectedValue = OrdenProceso.ResultadoDatos.Tables[0].Rows[0]["EmpleadoIdJefe"].ToString();
                OrdenIdHidden.Value = OrdenProceso.ResultadoDatos.Tables[0].Rows[0]["OrdenId"].ToString();

            }
            else
            {
               MostrarMensaje(OrdenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
               FechaOrdenCompraNuevo.Text = "";
               SolicitanteIdNuevo.SelectedIndex = 0;
               JefeInmediatoIdNuevo.SelectedIndex = 0;
               OrderCompraNuevo.Text = "";
               OrderCompraNuevo.Focus();
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

        private void SeleccionarProveedor()
        {
            Activos.ProcesoNegocio.Almacen.ProveedorProceso ProveedorProceso = new Activos.ProcesoNegocio.Almacen.ProveedorProceso();

            ProveedorProceso.SeleccionarProveedor();

            ProveedorIdNuevo.DataValueField = "ProveedorId";
            ProveedorIdNuevo.DataTextField = "Nombre";

            if (ProveedorProceso.ErrorId == 0)
            {
                ProveedorIdNuevo.DataSource = ProveedorProceso.ResultadoDatos;
                ProveedorIdNuevo.DataBind();
            }
            else
            {
                // ToDo: Manejar mensajes de error
                //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            ProveedorIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        private void SeleccionarEmpleado()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

            EmpleadoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusEmpleados.Activo;

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
                // ToDo: Manejar mensajes de error
                //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            SolicitanteIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        private void SeleccionarJefe(Int16 EmpleadoIdJefe)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

            if (EmpleadoIdJefe == 0)
            {
                JefeInmediatoIdNuevo.Items.Clear();
                JefeInmediatoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));

                return;
            }

            EmpleadoEntidadObjeto.EmpleadoId = EmpleadoIdJefe;

            Resultado = EmpleadoProcesoNegocio.SeleccionarEmpleado(EmpleadoEntidadObjeto);

            JefeInmediatoIdNuevo.DataValueField = "EmpleadoIdJefe";
            JefeInmediatoIdNuevo.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                JefeInmediatoIdNuevo.DataSource = Resultado.ResultadoDatos;
                JefeInmediatoIdNuevo.DataBind();
            }
            else
            {
                //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            JefeInmediatoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void BuscarJefe()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();
            Int16 EmpleadoIdJefe;

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

        }
      
        protected void SeleccionarTipoDocumento()
        {
            Activos.ProcesoNegocio.Almacen.TipoDocumentoProceso TipoDocumentoProceso = new Activos.ProcesoNegocio.Almacen.TipoDocumentoProceso();
            
            TipoDocumentoProceso.SeleccionarTipoDocumento();

            TipoDocumentoIdNuevo.DataValueField = "TipoDocumentoId";
            TipoDocumentoIdNuevo.DataTextField = "Nombre";

            if (TipoDocumentoProceso.ErrorId == 0)
            {
                TipoDocumentoIdNuevo.DataSource = TipoDocumentoProceso.ResultadoDatos;
                TipoDocumentoIdNuevo.DataBind();
            }
            else
            {
                // ToDo: Manejar mensajes de error
                //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            TipoDocumentoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }


        protected void TablaRecepcionEventoComando(GridViewCommandEventArgs e)
        {
            Int16 intFila = 0;
            int intTamañoPagina = 0;
            string ProductoId = string.Empty;
            string strCommand = string.Empty;

            intFila = Int16.Parse(e.CommandArgument.ToString());
            strCommand = e.CommandName.ToString();
            intTamañoPagina = TablaRecepcion.PageSize;

            if (intFila >= intTamañoPagina)
                intFila = (Int16)(intFila - (intTamañoPagina * TablaRecepcion.PageIndex));

            switch (strCommand)
            {
                   case "EliminarRecepcion":
                    ProductoId = string.Format(TablaRecepcion.DataKeys[intFila]["ProductoId"].ToString());
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
            RecepcionEntidad RecepcionObjetoEntidad = new RecepcionEntidad();
            RecepcionProceso RecepcionProcesoNegocio = new RecepcionProceso();

            //if (ProductoIdHidden.Value == ProductoId.ToString())
            //{
            RecepcionObjetoEntidad.ProductoId = ProductoId;
            Resultado = RecepcionProcesoNegocio.CancelarNuevoRecepcion(RecepcionObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Recepcion.RecepcionEliminadoCorrectamente)
            {
                EtiquetaMensaje.Text = "";
                SeleccionarRecepcion();

            }
            else
            {
                EtiquetaMensaje.Text = Resultado.DescripcionError;
            }
            //}

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
               

        #endregion


    }
}
