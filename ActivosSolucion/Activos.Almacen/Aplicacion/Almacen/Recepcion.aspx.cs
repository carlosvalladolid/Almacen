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

        protected void ImagenProductoBusqueda_Click(object sender, ImageClickEventArgs e)
        {

            CargaPanelVisibleProducto();

        }

        protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
        {
            //if (Page.IsValid)
            //{
            if (ValidarFormulario()) GuardarRecepcion();
            //}
        }

        protected void BotonCerrarProductoBusqueda_Click(object sender, ImageClickEventArgs e)
        {
            CargaPanelInVisibleProducto();
        }

        protected void BotonProductoBusqueda_Click(object sender, ImageClickEventArgs e)
        {
            BuscarProducto();
        }

        protected void BotonAgregar_Click(object sender, ImageClickEventArgs e)
        {
            if (ValidarAgregarProducto()) AgregarDetalleDocumento();

        }

        protected void CantidadNuevo_SelectedTextChanged(object sender, EventArgs e)
        {
            ObtenerMonto();
        }

        protected void PrecionUnitarioNuevo_SelectedTextChanged(object sender, EventArgs e)
        {
            ObtenerMonto();
        }

        private bool ObtenerMonto()
        {
            int PrecioUnitario = 0;
            int Cantidad = 0;
            //decimal Resultado = 0;
            string Mensaje = "";
            if (!Int32.TryParse(PrecionUnitarioNuevo.Text.Trim(), out PrecioUnitario) && String.IsNullOrEmpty(CantidadNuevo.Text.Trim())) Mensaje = TextoInfo.MensajePrecioInvalido;
            if (!Int32.TryParse(CantidadNuevo.Text.Trim(), out Cantidad) && String.IsNullOrEmpty(PrecionUnitarioNuevo.Text.Trim())) Mensaje = TextoInfo.MensajeCantidadGenerico;
            

            if (Mensaje == "")
            {
                MontoDocumentoNuevo.Text = ((decimal)(PrecioUnitario * Cantidad)).ToString();
                return true;
            }
                
            MostrarMensaje(Mensaje,ConstantePrograma.TipoErrorAlerta);
            return false;


            //if (PrecionUnitarioNuevo.Text != "0")
            //{

            //    PrecioUnitario = int.Parse(PrecionUnitarioNuevo.Text.Trim());

            //    if (CantidadNuevo.Text == "")
            //    {
            //        Cantidad = 0;
            //    }
            //    else
            //    {
            //        Cantidad = int.Parse(CantidadNuevo.Text.Trim());
            //    }
            //    Resultado = (PrecioUnitario * Cantidad);

            //    MontoDocumentoNuevo.Text = Resultado.ToString();
            //}
            //else
            //{

            //    EtiquetaMensaje.Text = "Capture el Precio";
            //    PrecionUnitarioNuevo.Focus();

            //}
        }

        protected void SolicitanteCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarJefe(Int16.Parse(SolicitanteIdNuevo.SelectedValue));
        }

        protected void BotonCerrarOrdenBusqueda_Click(object sender, ImageClickEventArgs e)
        {
            CargaPanelInVisibleOrden();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        protected void OrdenCompraNuevo_Click(object sender, ImageClickEventArgs e)
        {
            CargaPanelVisibleOrden();
        }


        protected void TablaOrdenBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaOrdenBusquedaRowCommand(e);
        }

        //protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SeleccionarSubfamilia();
        //}

        //protected void LinkBuscarClave_SelectedTextChanged(object sender, EventArgs e)
        //{
        //    SeleccionarClave();
        //}

        protected void LinkBuscarOrdenCompra_SelectedTextChanged(object sender, EventArgs e)
        {
            //SeleccionarOrdenCompra(Clave);
        }

        protected void TablaProducto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaProductoRowCommand(e);
        }


        protected void BotonOrdenBusqueda_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarFechas(FechaFiltroInicioOrdenBox.Text,FechaFiltroFinOrdenBox.Text)) BuscarOrden();
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
            if (TemporalRecepcionIdHidden.Value != "")
            {
                RecepcionObjetoEntidad.RecepcionId = TemporalRecepcionIdHidden.Value;
                RecepcionObjetoEntidad.TemporalRecepcionId = TemporalRecepcionIdHidden.Value;
                RecepcionObjetoEntidad.ProductoId = ProductoIdHidden.Value;
                RecepcionObjetoEntidad.PrecioUnitario = decimal.Parse(PrecionUnitarioNuevo.Text);
                RecepcionObjetoEntidad.Cantidad = Int16.Parse(CantidadNuevo.Text);

                AgregarRecepcion(RecepcionObjetoEntidad);
            }
        }

        protected void AgregarRecepcion(RecepcionEntidad RecepcionObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RecepcionProceso RecepcionProcesoNegocio = new RecepcionProceso();
           
           
           
            Resultado = RecepcionProcesoNegocio.AgregarRecepcionDetalleTemp(RecepcionObjetoEntidad);

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

        private void BuscarOrden()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            OrdenProceso OrdenProcesoNegocio = new OrdenProceso();

            DateTime FechaInicio = DateTime.Parse(FechaFiltroInicioOrdenBox.Text);
            DateTime FechaFin = DateTime.Parse(FechaFiltroFinOrdenBox.Text);

            Resultado = OrdenProcesoNegocio.SeleccionarOrdenEncabezadoPorRangoFechas(OrdenBusquedaBox.Text, FechaInicio, FechaFin);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaOrdenBusqueda.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaOrdenBusqueda.CssClass = ConstantePrograma.ClaseTabla;

                TablaOrdenBusqueda.DataSource = Resultado.ResultadoDatos;
                TablaOrdenBusqueda.DataBind();
            }
            else
            {
                //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                // MostrarMensaje(AlmacenProcesoNegocio.DescripcionError, ConstantePrograma.TipoErrorAlerta);
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
                RecepcionObjetoEntidad.RecepcionId = TemporalRecepcionIdHidden.Value;
                Resultado = RecepcionProcesoNegocio.AgregarRecepcionDetalle(RecepcionObjetoEntidad);
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

            Resultado = RecepcionProcesoNegocio.SeleccionaRecepcionTemp(RecepcionObjetoEntidad);
            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                {

                    TablaRecepcion.CssClass = ConstantePrograma.ClaseTablaVacia;
                    LabelMontoTotal.Text = "0";
                }
                else
                {
                    TablaRecepcion.CssClass = ConstantePrograma.ClaseTabla;
                    int Suma = 0;
                    foreach (DataRow Fila in Resultado.ResultadoDatos.Tables[0].Rows)
                    {
                        Suma += Convert.ToInt32(Fila["Cantidad"]);
                    }
                    LabelMontoTotal.Text = Suma.ToString();
                }



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
            FamiliaIdNuevo.Text = "";
            SubFamiliaIdNuevo.Text = "";
            MarcaIdNuevo.Text = "";
            //FamiliaIdNuevo.SelectedIndex = 0;
            //SeleccionarSubfamilia();
            //SubFamiliaIdNuevo.SelectedIndex = 0;
            //MarcaIdNuevo.SelectedIndex = 0;           
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
            //#
            TemporalRecepcionIdHidden.Value = Guid.NewGuid().ToString();
            
            
            MensajeRangoDeFechasInvalido.Value = TextoInfo.MensajeRangoFechasInvalido;
            SeleccionarProveedor();
            SeleccionarEmpleado();
            BuscarJefe();
            BuscarProducto();
            //SeleccionarMarca();
            //SeleccionarFamilia();
            //SeleccionarSubfamilia();
            SeleccionarTipoDocumento();

            TablaRecepcion.DataSource = null;
            TablaRecepcion.DataBind();

            JefeInmediatoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        //protected void SeleccionarClave()
        //{           
        //    PreOrdenProceso PreOrdenProcesoObjeto = new PreOrdenProceso();          

        //    PreOrdenProcesoObjeto.PreOrdenEntidad.Clave = ClaveNuevo.Text.Trim(); 
  
        //    PreOrdenProcesoObjeto.SeleccionarClaveProductoPreOrden();

        //    if (PreOrdenProcesoObjeto.ErrorId != 0)
        //    {
        //        MostrarMensaje(PreOrdenProcesoObjeto.DescripcionError, ConstantePrograma.TipoErrorAlerta);
        //        return;
        //    }
           
        //    if (PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
        //    {
        //        //  LimpiarProducto();
        //    }
        //    else
        //    {
        //        FamiliaIdNuevo.SelectedValue = PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString();
        //        SeleccionarSubfamilia();
        //        SubFamiliaIdNuevo.SelectedValue = PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows[0]["SubFamiliaId"].ToString();
        //        MarcaIdNuevo.SelectedValue = PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows[0]["MarcaId"].ToString();
        //        DescripcionNuevo.Text = PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows[0]["NombreProducto"].ToString();
        //        CantidadNuevo.Text = PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows[0]["MaximoPermitido"].ToString();
        //        ProductoIdHidden.Value = PreOrdenProcesoObjeto.ResultadoDatos.Tables[0].Rows[0]["ProductoId"].ToString();
        //    }
        //}

        protected void SeleccionarOrdenCompra(string Clave)
        {
            OrdenProceso OrdenProceso = new OrdenProceso();

            OrdenProceso.OrdenEncabezadoEntidad.Clave = Clave;

            OrdenProceso.SeleccionarBusquedaOrdenCompra();

            if (OrdenProceso.ErrorId != 0)
            {
            MostrarMensaje(OrdenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
            return;    
            } 

            if (OrdenProceso.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                FechaOrdenCompraNuevo.Text = String.Format("{0:" + ConstantePrograma.EspañolFormatoFecha2 + "}", OrdenProceso.ResultadoDatos.Tables[0].Rows[0]["FechaOrden"]);
                SolicitanteIdNuevo.SelectedValue = OrdenProceso.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                BuscarJefe();
                ProveedorIdNuevo.SelectedValue = OrdenProceso.ResultadoDatos.Tables[0].Rows[0]["ProveedorId"].ToString();
               // JefeInmediatoIdNuevo.SelectedValue = OrdenProceso.ResultadoDatos.Tables[0].Rows[0]["EmpleadoIdJefe"].ToString();
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

        private void CargaPanelVisibleProducto()
        {
            PanelBusquedaProducto.Visible = !PanelBusquedaProducto.Visible;
            pnlFondoBuscarProducto.Visible = !pnlFondoBuscarProducto.Visible;
        }

        private void CargaPanelInVisibleProducto()
        {
            PanelBusquedaProducto.Visible = false;
            pnlFondoBuscarProducto.Visible = false;

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
            RecepcionObjetoEntidad.RecepcionId = TemporalRecepcionIdHidden.Value;
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

        private Boolean ValidarFormulario()
        {
            //ConstantePrograma.EstatusPreOrden
            String Mensaje = "";
            Single PrecioTemp = new Single();
            Single MontoTemp = new Single();
            Int16 CantidadTemp = new Int16();
            DateTime FechaTemp = new DateTime();
            //if (!Single.TryParse(MontoDatosNuevo.Text, out MontoTemp)) Mensaje = TextoInfo.MensajeMontoInvalido;
            //if (!Int16.TryParse(CantidadNuevo.Text, out CantidadTemp)) Mensaje = TextoInfo.MensajeCantidadGenerico;
            //if (!Single.TryParse(PrecionUnitarioNuevo.Text, out PrecioTemp)) Mensaje = TextoInfo.MensajePrecioInvalido;
            if (String.IsNullOrEmpty(FolioNuevo.Text)) Mensaje = TextoInfo.MensajeFolioVacio;
            
            if (SolicitanteIdNuevo.SelectedIndex == 0) Mensaje = TextoInfo.MensajeSolicitanteGenerico;

            if (ProveedorIdNuevo.Items.Count > 0)
            {
                if (ProveedorIdNuevo.SelectedIndex == 0) Mensaje = TextoInfo.MensajeSeleccioneProveedor;
            }
            else
            {   
                Mensaje = TextoInfo.MensajeProveedoresVacio;
            }

            if (TipoDocumentoIdNuevo.Items.Count > 0)
            {
                if (TipoDocumentoIdNuevo.SelectedIndex == 0) Mensaje = TextoInfo.MensajeSeleccioneTipoDocumento;
            }
            else
            {
                Mensaje = TextoInfo.MensajeTipoDocumentoVacio;
            }

            if (TablaRecepcion.Rows.Count <= 0) Mensaje = TextoInfo.MensajeRecepcionVacia;
            if (!DateTime.TryParse(FechaDocumentoNuevo.Text, out FechaTemp)) Mensaje = TextoInfo.MensajeFechaGenerico;
            if (!Single.TryParse(MontoDatosNuevo.Text, out MontoTemp)) Mensaje = TextoInfo.MensajeMontoInvalido;

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
            
            if (SolicitanteIdNuevo.SelectedIndex == 0) Mensaje = TextoInfo.MensajeSolicitanteGenerico;

            if (Mensaje == "") return true;
            else MostrarMensaje(Mensaje, "Error");
            return false;
        }


        private void CargaPanelInVisibleOrden()
        {
            PanelBusquedaOrden.Visible = false;
            pnlFondoBuscarOrden.Visible = false;
        }

        private void CargaPanelVisibleOrden()
        {
            PanelBusquedaOrden.Visible = !PanelBusquedaOrden.Visible;
            pnlFondoBuscarOrden.Visible = !pnlFondoBuscarOrden.Visible;
        }




            private void TablaOrdenBusquedaRowCommand(GridViewCommandEventArgs e)
            {
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                string Clave = "";
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaOrdenBusqueda.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaOrdenBusqueda.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        Clave = string.Format(TablaOrdenBusqueda.DataKeys[intFila]["Clave"].ToString());
                        //AlmacenEntidadObjeto.Clave = ProductoId;
                        //ProductoIdHidden.Value = Clave.ToString();
                        //SeleccionarProductoMostrar(AlmacenEntidadObjeto);
                        //ValidarPreOrden(Clave);
                        SeleccionarOrdenCompra(Clave);
                        OrderCompraNuevo.Text = Clave;
                        CargaPanelInVisibleOrden();
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

            private Boolean VerificarFechas(string FechaInicio,string FechaFin)
            {
                DateTime Temporal = new DateTime();
                DateTime Temporal2 = new DateTime();
                String Mensaje = "";
                if (!DateTime.TryParse(FechaInicio, out Temporal)) Mensaje = TextoInfo.MensajeFechaGenerico;
                if (!DateTime.TryParse(FechaFin, out Temporal2)) Mensaje = TextoInfo.MensajeFechaGenerico;
                if (Mensaje == "") if (Temporal > Temporal2) Mensaje = TextoInfo.MensajeRangoFechasInvalido;

                if (Mensaje == "") return true;
                else MostrarMensaje(Mensaje, ConstantePrograma.TipoErrorAlerta);
                return false;
            }


        #endregion


    }
}
