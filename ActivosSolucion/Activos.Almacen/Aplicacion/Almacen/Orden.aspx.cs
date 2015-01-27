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

using Activos.Comun.Cadenas;
using Activos.Comun.Constante;
using Activos.Comun.Fecha;
using Activos.Entidad.Catalogo;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Almacen;
using Activos.ProcesoNegocio.Catalogo;

using Activos.Entidad.Almacen;

namespace Almacen.Web.Aplicacion.Almacen
{
    public partial class Orden : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
            {
                if (ValidarFormulario()) GuardarOrden();
            }

            protected void EmpleadoCombo_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarJefe(Int16.Parse(EmpleadoCombo.SelectedValue));
            }

            protected void ImagenBuscarPreOrden_Click(object sender, ImageClickEventArgs e)
            {
                CargaPanelVisibleProducto();
                //ValidarPreOrden(PreOrdenBusqueda.Text.Trim());
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

            protected void ProveedorCombo_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarProveedor(Int16.Parse(ProveedorCombo.SelectedValue));
            }

            protected void TablaPreOrden_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaPreOrdenRowCommand(e);
            }


            protected void BotonPreOrdenBusqueda_Click(object sender, ImageClickEventArgs e)
            {
                if(VerificarFechas()) BuscarPreOrden();
            }

            protected void BotonCerrarPreOrdenBusqueda_Click(object sender, ImageClickEventArgs e)
            {
                CargaPanelInVisibleProducto();
            }


            protected void TablaPreOrdenBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaPreOrdenBusquedaRowCommand(e);
            }

            protected void TablaOrden_RowCommand(object sender,GridViewCommandEventArgs e)
            {
                TablaOrdenRowCommand(e);
            }


        #endregion

        #region "Métodos"
            private bool ExistePreOrdenConOrden(string Clave)
            {
                PreOrdenProceso PreOrdenProceso = new PreOrdenProceso();

                PreOrdenProceso.PreOrdenEntidad.Clave = Clave;

                PreOrdenProceso.SeleccionarPreOrdenSinOrden();

                if (PreOrdenProceso.ErrorId != 0)
                {
                    MostrarMensaje(PreOrdenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return false;
                }

                if (PreOrdenProceso.ResultadoDatos.Tables[0].Rows.Count == 0)
                    return false;
                else
                    return true;
            }

            private void GuardarOrden()
            {
                
                OrdenProceso OrdenProceso = new OrdenProceso();

                OrdenProceso.OrdenEncabezadoEntidad.OrdenId = OrdenIdHidden.Value;
                OrdenProceso.OrdenEncabezadoEntidad.EmpleadoId = EmpleadoCombo.SelectedValue;
                OrdenProceso.OrdenEncabezadoEntidad.JefeId = JefeIdHidden.Value;
                OrdenProceso.OrdenEncabezadoEntidad.ProveedorId = Int16.Parse(ProveedorCombo.SelectedValue);
                OrdenProceso.OrdenEncabezadoEntidad.EstatusId = (int)ConstantePrograma.EstatusOrden.SinSurtir;
                OrdenProceso.OrdenEncabezadoEntidad.FechaOrden = FormatoFecha.AsignarFormato(FechaOrdenBox.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);
                OrdenProceso.OrdenDetalleEntidad.ProductoIdArray = ObtenerProductoId();

                ObtenerEstatusOrden();


                OrdenProceso.GuardarOrden();

                if (OrdenProceso.ErrorId == 0)
                {
                    LimpiarFormulario();

                    MostrarMensaje(TextoInfo.MensajeNoOrden + OrdenProceso.SeleccionarOrdenEncabezadoPorOrdenId().ResultadoDatos.Tables[0].Rows[0]["Clave"], ConstantePrograma.TipoMensajeSimpleAlerta);
                    //MostrarMensaje(TextoInfo.MensajeGuardadoGenerico, ConstantePrograma.TipoMensajeAlerta);
                }
                else
                    MostrarMensaje(OrdenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
            }

            private short ObtenerEstatusPreOrden(OrdenEntidad OrdenEntidad)
            {
                short Estatus = ConstantePrograma.EstatusPreOrden.SinOC;





                return Estatus;
            }


            
            private void GuardarProductoOrdenTemp(string OrdenId, string PreOrdenId, string ProductoId, string SesionId)
            {
                OrdenProceso OrdenProceso = new OrdenProceso();

                OrdenProceso.OrdenDetalleEntidad.OrdenId = OrdenId;
                OrdenProceso.OrdenDetalleEntidad.PreOrdenId = PreOrdenId;
                OrdenProceso.OrdenDetalleEntidad.ProductoId = ProductoId;
                OrdenProceso.OrdenDetalleEntidad.SesionId = SesionId;

                OrdenProceso.GuardarProductoOrdenTemp();

                if (OrdenProceso.ErrorId == 0)
                {
                    OrdenIdHidden.Value = OrdenProceso.OrdenDetalleEntidad.OrdenId;

                    SeleccionarOrdenDetalleTemp(OrdenIdHidden.Value);
                }
                else
                    MostrarMensaje(OrdenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
            }

            private void Inicio()
            {
                if (Page.IsPostBack)
                    return;

                MensajeRangoDeFechasInvalido.Value = TextoInfo.MensajeRangoFechasInvalido;
                MensajeConfirmacion.Value = TextoInfo.MensajeConfirmacionOrden;
                FechaOrdenBox.Text = DateTime.Now.Date.ToShortDateString();

                TablaPreOrden.DataSource = null;
                TablaPreOrden.DataBind();

                TablaOrden.DataSource = null;
                TablaOrden.DataBind();

                SeleccionarProveedor();
                SeleccionarEmpleado();
            }

            private void LimpiarFormulario()
            {
                ImagenBuscarPreOrden.Enabled = true;
                PreOrdenBusqueda.Text = "";
                FechaOrdenBox.Text = "";
                ProveedorCombo.SelectedIndex = 0;
                TelefonoBox.Text = "";
                ContactoBox.Text = "";
                CorreoBox.Text = "";
                EmpleadoCombo.SelectedIndex = 0;
                JefeBox.Text = "";

                OrdenIdHidden.Value = "";
                JefeIdHidden.Value = "0";

                TablaPreOrden.DataSource = null;
                TablaPreOrden.DataBind();

                TablaOrden.DataSource = null;
                TablaOrden.DataBind();
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

            private string[,] ObtenerProductoId()
            {
                int Contador = 0;
                string[,] ProductoIdArray;
                TextBox CantidadBox = new TextBox();
                StringBuilder OrderIdString = new StringBuilder();

                ProductoIdArray = new string[TablaOrden.Rows.Count, 2];

                foreach (GridViewRow Registro in TablaOrden.Rows)
                {
                    CantidadBox = (TextBox)Registro.FindControl("CantidadBox");

                    if (CantidadBox == null)
                        continue;

                    ProductoIdArray[Contador, 0] = TablaOrden.DataKeys[Contador]["ProductoId"].ToString();
                    ProductoIdArray[Contador, 1] = CantidadBox.Text.Trim();

                    Contador++;
                }

                return ProductoIdArray;
            }

            private void SeleccionarEmpleado()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

                EmpleadoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusEmpleados.Activo;

                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                EmpleadoCombo.DataValueField = "EmpleadoId";
                EmpleadoCombo.DataTextField = "NombreEmpleadoCompleto";

                if (Resultado.ErrorId == 0)
                {
                    EmpleadoCombo.DataSource = Resultado.ResultadoDatos;
                    EmpleadoCombo.DataBind();
                }
                else
                    MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);

                EmpleadoCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarJefe(Int16 EmpleadoIdJefe)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoEntidad EmpleadoEntidad = new EmpleadoEntidad();
                EmpleadoProceso EmpleadoProceso = new EmpleadoProceso();

                if (EmpleadoIdJefe == 0)
                    return;

                EmpleadoEntidad.EmpleadoId = EmpleadoIdJefe;

                Resultado = EmpleadoProceso.SeleccionarEmpleadoJefe(EmpleadoEntidad);

                if (Resultado.ErrorId != 0)
                {
                    MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    JefeBox.Text = "";
                    JefeIdHidden.Value = "0";
                }
                else
                {
                    JefeBox.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreJefe"].ToString();
                    JefeIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                }
            }

            private void SeleccionarOrdenDetalleTemp(string OrdenId)
            {
                OrdenProceso OrdenProceso = new OrdenProceso();

                OrdenProceso.OrdenEncabezadoEntidad.OrdenId = OrdenId;

                OrdenProceso.SeleccionarOrdenDetalleTemp();

                if (OrdenProceso.ErrorId != 0)
                {
                    MostrarMensaje(OrdenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                TablaOrden.DataSource = OrdenProceso.ResultadoDatos;
                TablaOrden.DataBind();
            }

            private void SeleccionarPreOrdenDetalleSinOrden(string PreOrdenId, string SesionId)
            {
                PreOrdenProceso PreOrdenProceso = new PreOrdenProceso();

                PreOrdenProceso.PreOrdenEntidad.Clave = PreOrdenId;
                PreOrdenProceso.PreOrdenEntidad.SesionId = SesionId;

                PreOrdenProceso.SeleccionarPreOrdenDetalleSinOrden();

                if (PreOrdenProceso.ErrorId != 0)
                {
                    MostrarMensaje(PreOrdenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                // ToDo: Cambiar el estilo del grid si está vacío el dataset

                TablaPreOrden.DataSource = PreOrdenProceso.ResultadoDatos;
                TablaPreOrden.DataBind();
            }

            private void SeleccionarProveedor()
            {
                Activos.ProcesoNegocio.Almacen.ProveedorProceso ProveedorProceso = new Activos.ProcesoNegocio.Almacen.ProveedorProceso();

                ProveedorProceso.SeleccionarProveedor();

                ProveedorCombo.DataValueField = "ProveedorId";
                ProveedorCombo.DataTextField = "Nombre";

                if (ProveedorProceso.ErrorId == 0)
                {
                    ProveedorCombo.DataSource = ProveedorProceso.ResultadoDatos;
                    ProveedorCombo.DataBind();
                }
                else
                    MostrarMensaje(ProveedorProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);

                ProveedorCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarProveedor(Int16 ProveedorId)
            {
                Activos.ProcesoNegocio.Almacen.ProveedorProceso ProveedorProceso = new Activos.ProcesoNegocio.Almacen.ProveedorProceso();

                ProveedorProceso.ProveedorEntidad.ProveedorId = ProveedorId;

                ProveedorProceso.SeleccionarProveedor();

                if (ProveedorProceso.ErrorId != 0)
                {
                    MostrarMensaje(ProveedorProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                if (ProveedorProceso.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    TelefonoBox.Text = ProveedorProceso.ResultadoDatos.Tables[0].Rows[0]["Telefono"].ToString();
                    ContactoBox.Text = ProveedorProceso.ResultadoDatos.Tables[0].Rows[0]["NombreContacto"].ToString();
                    CorreoBox.Text = ProveedorProceso.ResultadoDatos.Tables[0].Rows[0]["Email"].ToString();
                }   
            }

            private void TablaPreOrdenRowCommand(GridViewCommandEventArgs e)
            {
                int Indice = 0;
                string PreOrdenId = string.Empty;
                string Clave = string.Empty;
                string ProductoId = string.Empty;
                string SesionId = string.Empty;
                string CommandName = string.Empty;
                UsuarioEntidad UsuarioEntidad = new UsuarioEntidad();

                UsuarioEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                Indice = int.Parse(e.CommandArgument.ToString());
                PreOrdenId = TablaPreOrden.DataKeys[Indice]["PreOrdenId"].ToString();
                Clave = TablaPreOrden.DataKeys[Indice]["ClavePreOrden"].ToString();
                ProductoId = TablaPreOrden.DataKeys[Indice]["ProductoId"].ToString();
                SesionId = UsuarioEntidad.SesionId;
                CommandName = e.CommandName.ToString();

                switch(CommandName)
                {
                    case ConstantePrograma.ComandoAgregar:
                        GuardarProductoOrdenTemp(OrdenIdHidden.Value, PreOrdenId, ProductoId, SesionId);
                        SeleccionarPreOrdenDetalleSinOrden(Clave, SesionId);

                        //Se inabilita el boton para cambiar de PreOrden
                        ImagenBuscarPreOrden.Enabled = false;
                        
                        break;
                }
            }

            private void TablaOrdenRowCommand(GridViewCommandEventArgs e)
            {
                int Indice = 0;
                string OrdenId = string.Empty;
                string PreOrdenId = string.Empty;
                string ProductoId = string.Empty;
                string SesionId = string.Empty;
                string CommandName = string.Empty;
                UsuarioEntidad UsuarioEntidad = new UsuarioEntidad();

                UsuarioEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                Indice = int.Parse(e.CommandArgument.ToString());
                OrdenId = TablaOrden.DataKeys[Indice]["OrdenId"].ToString();
                PreOrdenId = TablaOrden.DataKeys[Indice]["PreOrdenId"].ToString();
                ProductoId = TablaOrden.DataKeys[Indice]["ProductoId"].ToString();
                SesionId = UsuarioEntidad.SesionId;
                CommandName = e.CommandName.ToString();

                switch(CommandName)
                {
                    case ConstantePrograma.ComandoEliminar:
                        EliminarProductoDetalleTemp(OrdenId, PreOrdenId, ProductoId, SesionId);
                        PreOrdenEntidad PreOrdenObjetoEntidad = new PreOrdenEntidad();
                        PreOrdenObjetoEntidad.PreOrdenId = PreOrdenId;
                        string ClavePreOrden = ObtenerClavePreOrden(PreOrdenObjetoEntidad);
                        if (ClavePreOrden != "") SeleccionarPreOrdenDetalleSinOrden(ClavePreOrden, SesionId);
                        else MostrarMensaje("No se encuentra la clave de la PreOrden",ConstantePrograma.TipoErrorAlerta);

                        
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

            private void EliminarProductoDetalleTemp(string OrdenId, string PreOrdenId, string ProductoId, string SesionId)
            {
                OrdenProceso OrdenProceso = new OrdenProceso();

                OrdenProceso.OrdenDetalleEntidad.OrdenId = OrdenId;
                OrdenProceso.OrdenDetalleEntidad.PreOrdenId = PreOrdenId;
                OrdenProceso.OrdenDetalleEntidad.ProductoId = ProductoId;
                OrdenProceso.OrdenDetalleEntidad.SesionId = SesionId;

                OrdenProceso.EliminarProductoOrdenDetalleTemp();

                if (OrdenProceso.ErrorId == 0)
                {
                    //OrdenIdHidden.Value = OrdenProceso.OrdenDetalleEntidad.OrdenId;

                    SeleccionarOrdenDetalleTemp(OrdenId);
                }
                else
                    MostrarMensaje(OrdenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
            }

        

            private void ValidarPreOrden(string Clave)
            {
                UsuarioEntidad UsuarioEntidad = new UsuarioEntidad();

                UsuarioEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                if (ExistePreOrdenConOrden(Clave))
                {
                    MostrarMensaje(TextoError.OrdenConPreOrdenId, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                SeleccionarPreOrdenDetalleSinOrden(Clave, UsuarioEntidad.SesionId);
            }




            private Boolean ValidarFormulario()
            {
                String Mensaje = "";
                DateTime Temporal = new DateTime();

                if (EmpleadoCombo.Items.Count <= 0)
                    Mensaje = TextoInfo.MensajeEmpleadosVacio;
                    else if (EmpleadoCombo.SelectedIndex == 0) Mensaje = TextoInfo.MensajeSeleccioneEmpleado;

                if (ProveedorCombo.Items.Count <= 0)
                    Mensaje = TextoInfo.MensajeProveedoresVacio;
                    else if (ProveedorCombo.SelectedIndex == 0) Mensaje = TextoInfo.MensajeSeleccioneProveedor;
                
                
                if (!DateTime.TryParse(FechaOrdenBox.Text, out Temporal)) Mensaje = TextoInfo.MensajeFechaGenerico;
                
                if(TablaOrden.Rows.Count <= 0) Mensaje = TextoInfo.MensajeOrdenVacia;

                if (Mensaje == "") return true;
                else MostrarMensaje(Mensaje, "Error");
                return false;
            }

            //private Boolean ValidarAgregarProducto()
            //{
            //    String Mensaje = "";
            //    Int16 NumeroTemporal = 0;
            //    if (!Int16.TryParse(CantidadNuevo.Text, out NumeroTemporal)) Mensaje = TextoInfo.MensajeCantidadGenerico;
            //    if (NumeroTemporal == 0) Mensaje = TextoInfo.MensajeCantidadGenerico;
            //    if (String.IsNullOrEmpty(ClaveNuevo.Text)) Mensaje = TextoInfo.MensajeClaveGenerico;
            //    DateTime Temporal = new DateTime();
            //    if (!DateTime.TryParse(FechaPreOrdenNuevo.Text, out Temporal)) Mensaje = TextoInfo.MensajeFechaGenerico;
            //    if (SolicitanteIdNuevo.SelectedIndex == 0) Mensaje = TextoInfo.MensajeSolicitanteGenerico;

            //    if (Mensaje == "") return true;
            //    else MostrarMensaje(Mensaje, "Error");
            //    return false;
            //}
            private string ObtenerClaveOrden(OrdenEntidad OrdenObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                OrdenProceso OrdenProcesoNegocio = new OrdenProceso();
                OrdenProcesoNegocio.OrdenEncabezadoEntidad = OrdenObjetoEntidad;
                Resultado = OrdenProcesoNegocio.SeleccionarOrdenEncabezadoPorOrdenId();

                if (Resultado.ResultadoDatos.Tables.Count > 0)
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0) return Resultado.ResultadoDatos.Tables[0].Rows[0]["Clave"].ToString();

                return String.Empty;
            }

            private void CargaPanelVisibleProducto()
            {
                PanelBusquedaProducto.Visible = !PanelBusquedaProducto.Visible;
                pnlFondoBuscarProducto.Visible = !pnlFondoBuscarProducto.Visible;
            }


            private void BuscarPreOrden()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                PreOrdenProceso PreOrdenProcesoNegocio = new PreOrdenProceso();

                DateTime FechaInicio = DateTime.Parse(FechaFiltroInicioBox.Text);
                DateTime FechaFin = DateTime.Parse(FechaFiltroFinBox.Text);

                //AlmacenObjetoEntidad.Clave = ClaveProductoBusqueda.Text.Trim();
                //AlmacenObjetoEntidad.Descripcion = NombreProductoBusqueda.Text.Trim();

                Resultado = PreOrdenProcesoNegocio.SeleccionarPreOrdenEncabezadoPorBusqueda(ClaveProductoBusqueda.Text, FechaInicio, FechaFin);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaPreOrdenBusqueda.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaPreOrdenBusqueda.CssClass = ConstantePrograma.ClaseTabla;

                    TablaPreOrdenBusqueda.DataSource = Resultado.ResultadoDatos;
                    TablaPreOrdenBusqueda.DataBind();
                }
                else
                {
                    //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    // MostrarMensaje(AlmacenProcesoNegocio.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                }
            }

            private void CargaPanelInVisibleProducto()
            {
                PanelBusquedaProducto.Visible = false;
                pnlFondoBuscarProducto.Visible = false;

            }

            private Boolean VerificarFechas()
            {
                DateTime Temporal = new DateTime();
                DateTime Temporal2 = new DateTime();
                String Mensaje = "";
                if (!DateTime.TryParse(FechaFiltroInicioBox.Text, out Temporal)) Mensaje = TextoInfo.MensajeFechaGenerico;
                if (!DateTime.TryParse(FechaFiltroFinBox.Text, out Temporal2)) Mensaje = TextoInfo.MensajeFechaGenerico;
                if (Mensaje == "") if (Temporal > Temporal2) Mensaje = TextoInfo.MensajeRangoFechasInvalido;

                if (Mensaje == "") return true;
                else MostrarMensaje(Mensaje,ConstantePrograma.TipoErrorAlerta);
                return false;                
            }




            private void TablaPreOrdenBusquedaRowCommand(GridViewCommandEventArgs e)
            {
                //AlmacenEntidad AlmacenEntidadObjeto = new AlmacenEntidad();
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                string Clave = "";
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaPreOrdenBusqueda.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaPreOrdenBusqueda.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        Clave = string.Format(TablaPreOrdenBusqueda.DataKeys[intFila]["Clave"].ToString());
                        //AlmacenEntidadObjeto.Clave = ProductoId;
                        //ProductoIdHidden.Value = Clave.ToString();
                        //SeleccionarProductoMostrar(AlmacenEntidadObjeto);
                        ValidarPreOrden(Clave);
                        PreOrdenBusqueda.Text = Clave;
                        CargaPanelInVisibleProducto();
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }




        #endregion
    }
}
