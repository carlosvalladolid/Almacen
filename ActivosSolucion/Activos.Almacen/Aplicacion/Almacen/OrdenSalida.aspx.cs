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
    public partial class OrdenSalida : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonAgregar_Click(object sender, EventArgs e)
            {
                if (ValidarProducto()) GuardarProductoTemp();
            }

            protected void BotonCerrarProductoBusqueda_Click(object sender, ImageClickEventArgs e)
            {
                OcultarBusquedaProducto();
            }

            protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
            {
                GuardarOrdenSalida();
            }

            protected void BotonProductoBusqueda_Click(object sender, ImageClickEventArgs e)
            {
                SeleccionarProducto(ClaveProductoBusqueda.Text.Trim(), NombreProductoBusqueda.Text.Trim());
            }

            protected void BotonRequisicionBusqueda_Click(object sender, ImageClickEventArgs e)
            {
                SeleccionarRequisicionBusqueda();
            }

            protected void BotonRequisicionCerrar_Click(object sender, ImageClickEventArgs e)
            {
                OcultarBusquedaRequisicion();
            }

            protected void ImagenBuscarRequisicion_Click(object sender, ImageClickEventArgs e)
            {
                BuscarRequisicion();
            }

            protected void ImagenProductoBusqueda_Click(object sender, ImageClickEventArgs e)
            {
                BuscarProducto();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

            protected void TablaProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                SeleccionarProducto();
                TablaProducto.PageIndex = e.NewPageIndex;
                TablaProducto.DataBind();
            }    
        
            protected void TablaProducto_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaProductoRowCommand(e);
            }

            protected void TablaRequisicionBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                SeleccionarRequisicionBusqueda();
                TablaRequisicionBusqueda.PageIndex = e.NewPageIndex;
                TablaRequisicionBusqueda.DataBind();
            }

            protected void TablaRequisicionBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaRequisicionBusquedaRowCommand(e);
            }
            
            protected void TablaOrden_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                if (OrdenSalidaIdHidden.Value == "") return;
                OrdenSalidaProceso OrdenSalidaProceso = new OrdenSalidaProceso();
                OrdenSalidaProceso.OrdenSalidaDetalleEntidad.OrdenSalidaId = OrdenSalidaIdHidden.Value;
                SeleccionarOrdenSalidaDetalleTemp(OrdenSalidaProceso);
                TablaOrden.PageIndex = e.NewPageIndex;
                TablaOrden.DataBind();
            }

            protected void TablaOrden_RowCommand(object sender,GridViewCommandEventArgs e)
            {
                TablaOrdenRowCommmand(e);
            }
        
        #endregion

        #region "Métodos"
            private void BuscarProducto()
            {
                SeleccionarProducto();
                MostrarBusquedaProducto();
            }

            private void BuscarRequisicion()
            {
                SeleccionarEstatus();
                MostrarBusquedaRequisicion();
            }

            private void BorrarOrdenSalidaDetalleTemp(string ProductoId)
            {
                OrdenSalidaProceso OrdenSalidaProceso = new OrdenSalidaProceso();
                OrdenSalidaProceso.OrdenSalidaDetalleEntidad.OrdenSalidaId = OrdenSalidaIdHidden.Value;
                OrdenSalidaProceso.OrdenSalidaDetalleEntidad.ProductoId = ProductoId;
                OrdenSalidaProceso.BorrarOrdenSalidaDetalleTemp();

                if(OrdenSalidaProceso.ErrorId != 0)
                {
                    MostrarMensaje(OrdenSalidaProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                }
            }
        
            private void GuardarProductoTemp()
            {
                int Cantidad = 0;
                string ClaveProducto = string.Empty;
                UsuarioEntidad UsuarioEntidad = new UsuarioEntidad();
                OrdenSalidaProceso OrdenSalidaProceso = new OrdenSalidaProceso();

                if (ClaveProductoBox.Text.Trim() == "")
                {
                    MostrarMensaje(TextoError.SalidaClaveProducto, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                ClaveProducto = ClaveProductoBox.Text.Trim();
                UsuarioEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                OrdenSalidaProceso.OrdenSalidaDetalleEntidad.OrdenSalidaId = OrdenSalidaIdHidden.Value;
                OrdenSalidaProceso.OrdenSalidaDetalleEntidad.RequisicionId = RequisicionIdHidden.Value;
                OrdenSalidaProceso.OrdenSalidaDetalleEntidad.EstatusId = (short)ConstantePrograma.EstatusOrdenSalida.SalidaCompleta;
                OrdenSalidaProceso.OrdenSalidaDetalleEntidad.UsuarioIdInserto = UsuarioEntidad.UsuarioId;

                OrdenSalidaProceso.OrdenSalidaDetalleEntidad.ProductoId = ProductoIdHidden.Value;
                OrdenSalidaProceso.OrdenSalidaDetalleEntidad.Cantidad = Convert.ToInt16(CantidadBox.Text);

                OrdenSalidaProceso.GuardarOrdenSalidaTemp();

                if (OrdenSalidaProceso.ErrorId == 0)
                {
                    OrdenSalidaIdHidden.Value = OrdenSalidaProceso.OrdenSalidaDetalleEntidad.OrdenSalidaId;
                    LimpiarFormularioProducto();
                    SeleccionarOrdenSalidaDetalleTemp(OrdenSalidaProceso);
                }
                else
                {
                    MostrarMensaje(OrdenSalidaProceso.DescripcionError,ConstantePrograma.TipoErrorAlerta);
                }
            }

            private void GuardarOrdenSalida()
            {
                OrdenSalidaProceso OrdenSalidaProceso = new OrdenSalidaProceso();
                OrdenSalidaProceso.OrdenSalidaDetalleEntidad.OrdenSalidaId = OrdenSalidaIdHidden.Value;
                
                string OrdenSalidaClave = OrdenSalidaProceso.GuardarOrdenSalida();

                if (OrdenSalidaProceso.ErrorId != 0)
                {
                    MostrarMensaje(OrdenSalidaProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                MostrarMensaje(TextoInfo.MensajeNoOrdenSalida+OrdenSalidaClave,ConstantePrograma.TipoMensajeSimpleAlerta);
                LimpiarFormulario();
            }

            private void Inicio()
            {
                if (Page.IsPostBack)
                    return;

                RequisicionIdHidden.Value = "";
                ProductoIdHidden.Value = "";

                MensajeConfirmacion.Value = TextoInfo.MensajeConfirmOrdenSalida;
                MensajeLimpieza.Value = TextoInfo.MensajeLimpiarFormulario;

                SeleccionarRequisicion();
                
                TablaOrden.DataSource = null;
                TablaOrden.DataBind();
            }

            private void LimpiarFormulario()
            {
                RequisicionBox.Text = "";
                SolicitanteBox.Text = "";
                DependenciaBox.Text = "";
                DireccionBox.Text = "";
                PuestoBox.Text = "";
                JefeBox.Text = "";
                ClaveProductoBox.Text = "";
                FamiliaBox.Text = "";
                SubFamiliaBox.Text = "";
                MarcaBox.Text = "";                
                DescripcionBox.Text = "";
                CantidadBox.Text = "";

                ProductoIdHidden.Value = "";
                OrdenSalidaIdHidden.Value = "";
                RequisicionIdHidden.Value = "";

                TablaProducto.DataSource = null;
                TablaProducto.DataBind();

                TablaOrden.DataSource = null;
                TablaOrden.DataBind();
            }

            private void LimpiarFormularioProducto()
            {
                FamiliaBox.Text = "";
                SubFamiliaBox.Text = "";
                MarcaBox.Text = "";
                DescripcionBox.Text = "";
                CantidadBox.Text = "";
                ClaveProductoBox.Text = "";
            }

            private void MostrarBusquedaProducto()
            {
                FondoBuscarProducto.Visible = true;
                PanelBusquedaProducto.Visible = true;
            }

            private void MostrarBusquedaRequisicion()
            {
                FondoBusquedaRequisicion.Visible = true;
                PanelBusquedaRequisicion.Visible = true;
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

            private void OcultarBusquedaProducto()
            {
                FondoBuscarProducto.Visible = false;
                PanelBusquedaProducto.Visible = false;
            }

            private void OcultarBusquedaRequisicion()
            {
                FondoBusquedaRequisicion.Visible = false;
                PanelBusquedaRequisicion.Visible = false;
            }

            private void SeleccionarEstatus()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EstatusEntidad EstatusEntidad = new EstatusEntidad();
                EstatusProceso EstatusProceso = new EstatusProceso();

                EstatusEntidad.SeccionId = (int)ConstantePrograma.Seccion.Requerimiento;

                Resultado = EstatusProceso.SeleccionarEstatus(EstatusEntidad);

                EstatusBusquedaCombo.DataValueField = "EstatusId";
                EstatusBusquedaCombo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    EstatusBusquedaCombo.DataSource = Resultado.ResultadoDatos;
                    EstatusBusquedaCombo.DataBind();
                }
                else
                    MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);

                EstatusBusquedaCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            private void SeleccionarOrdenSalidaDetalleTemp(OrdenSalidaProceso OrdenSalidaProceso)
            {
                TablaOrden.DataSource = OrdenSalidaProceso.SeleccionarOrdenSalidaDetalleTemp();
                TablaOrden.DataBind();
            }

            private void SeleccionarProducto()
            {
                SeleccionarProducto("", "");
            }

            private void SeleccionarProducto(string ProductoId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenEntidad AlmacenObjetoEntidad = new AlmacenEntidad();
                AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

                AlmacenObjetoEntidad.ProductoId = ProductoId;

                Resultado = AlmacenProcesoNegocio.SeleccionarProducto(AlmacenObjetoEntidad);

                if (Resultado.ErrorId != 0)
                {
                    MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    MostrarMensaje(TextoError.ProductoNoEncontrado, ConstantePrograma.TipoErrorAlerta);
                else
                {
                    ProductoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ProductoId"].ToString();
                    ClaveProductoBox.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Clave"].ToString();
                    FamiliaBox.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Familia"].ToString();
                    SubFamiliaBox.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["SubFamilia"].ToString();
                    MarcaBox.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Marca"].ToString();
                    DescripcionBox.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreProducto"].ToString();
                }
            }

            private void SeleccionarProducto(string Clave, string Nombre)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AlmacenEntidad AlmacenObjetoEntidad = new AlmacenEntidad();
                AlmacenProceso AlmacenProcesoNegocio = new AlmacenProceso();

                AlmacenObjetoEntidad.Clave = Clave;
                AlmacenObjetoEntidad.Descripcion = Nombre;

                Resultado = AlmacenProcesoNegocio.SeleccionarProducto(AlmacenObjetoEntidad);

                if (Resultado.ErrorId != 0)
                {
                    MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaProducto.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaProducto.CssClass = ConstantePrograma.ClaseTabla;

                TablaProducto.DataSource = Resultado.ResultadoDatos;
                TablaProducto.DataBind();
            }

            private void SeleccionarRequisicion()
            {
                SeleccionarRequisicion("", "", "", "", 0,"");
            }

            private void SeleccionarRequisicionBusqueda()
            {
                string Mensaje = "";
                DateTime FechaInicio = new DateTime();
                DateTime FechaFinal = new DateTime();

                if(String.IsNullOrEmpty(FechaInicioBusquedaBox.Text.Trim()) && String.IsNullOrEmpty(FechaFinBusquedaBox.Text.Trim()))
                {
                    FechaInicio = DateTime.Parse(ConstantePrograma.SqlSmallDateTimeMinValue);
                    FechaFinal = DateTime.Parse(ConstantePrograma.SqlSmallDateTimeMaxValue);
                }
                else
                {
                    if (!DateTime.TryParse(FechaInicioBusquedaBox.Text.Trim(), out FechaInicio)) Mensaje = TextoInfo.MensajeRangoFechasInvalido;
                    if (!DateTime.TryParse(FechaFinBusquedaBox.Text.Trim(), out FechaFinal)) Mensaje = TextoInfo.MensajeRangoFechasInvalido;
                }
               
                if (Mensaje == "")
                {
                    SeleccionarRequisicion("", EmpleadoBusquedaBox.Text.Trim(), FechaInicio.ToString(ConstantePrograma.SqlServerFormatoFechaSoloFecha), FechaFinal.ToString(ConstantePrograma.SqlServerFormatoFechaSoloFecha), Int16.Parse(EstatusBusquedaCombo.SelectedValue), RequisicionBusquedaBox.Text.Trim());
                    return;
                }

                MostrarMensaje(Mensaje,ConstantePrograma.TipoErrorAlerta);
            }
        
            private void SeleccionarRequisicion(string RequisicionId)
            {
                RequisicionProceso RequisicionProceso = new RequisicionProceso();

                RequisicionProceso.RequisicionEntidad.RequisicionId = RequisicionId;

                RequisicionProceso.SeleccionarRequisicionSalida();

                if (RequisicionProceso.ErrorId != 0)
                {
                    MostrarMensaje(RequisicionProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                if (RequisicionProceso.ResultadoDatos.Tables[0].Rows.Count == 0)
                    MostrarMensaje(TextoError.SalidaOrdenNoEncontrada, ConstantePrograma.TipoErrorAlerta);
                else
                {
                    RequisicionBox.Text = RequisicionProceso.ResultadoDatos.Tables[0].Rows[0]["Clave"].ToString();
                    SolicitanteBox.Text = RequisicionProceso.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleado"].ToString();
                    DependenciaBox.Text = RequisicionProceso.ResultadoDatos.Tables[0].Rows[0]["NombreDependencia"].ToString();
                    DireccionBox.Text = RequisicionProceso.ResultadoDatos.Tables[0].Rows[0]["NombreDireccion"].ToString();
                    PuestoBox.Text = RequisicionProceso.ResultadoDatos.Tables[0].Rows[0]["NombrePuesto"].ToString();
                    JefeBox.Text = RequisicionProceso.ResultadoDatos.Tables[0].Rows[0]["NombreJefe"].ToString();
                }
            }

            private void SeleccionarRequisicion(string RequisicionId, string Empleado, string FechaInicial, string FechaFinal, Int16 EstatusId, string Clave)
            {
                RequisicionProceso RequisicionProceso = new RequisicionProceso();

                RequisicionProceso.RequisicionEntidad.RequisicionId = RequisicionId;
                RequisicionProceso.RequisicionEntidad.Clave = Clave;
                RequisicionProceso.RequisicionEntidad.Nombre = Empleado;
                RequisicionProceso.RequisicionEntidad.FechaInicial = FechaInicial;
                RequisicionProceso.RequisicionEntidad.FechaFinal = FechaFinal;
                RequisicionProceso.RequisicionEntidad.EstatusId = EstatusId;

                RequisicionProceso.SeleccionarRequisicionSalida();

                if (RequisicionProceso.ErrorId != 0)
                {
                    MostrarMensaje(RequisicionProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                // ToDo: Cambiar el estilo del grid si está vacío el dataset

                TablaRequisicionBusqueda.DataSource = RequisicionProceso.ResultadoDatos;
                TablaRequisicionBusqueda.DataBind();
            }

            private void TablaOrdenRowCommmand(GridViewCommandEventArgs e)
            {
                if (e.CommandName != "EliminarPreOrden") return;
                BorrarOrdenSalidaDetalleTemp(e.CommandArgument.ToString());
                OrdenSalidaProceso OrdenSalidaProceso = new OrdenSalidaProceso();
                OrdenSalidaProceso.OrdenSalidaDetalleEntidad.ProductoId = e.CommandArgument.ToString();
                OrdenSalidaProceso.OrdenSalidaDetalleEntidad.OrdenSalidaId = OrdenSalidaIdHidden.Value;
                SeleccionarOrdenSalidaDetalleTemp(OrdenSalidaProceso);
            }

            private void TablaProductoRowCommand(GridViewCommandEventArgs e)
            {
                string ProductoId = string.Empty;

                ProductoId = e.CommandArgument.ToString();

                if (e.CommandName != "Select") return;
                SeleccionarProducto(ProductoId);
                OcultarBusquedaProducto();
            }

            private void TablaRequisicionBusquedaRowCommand(GridViewCommandEventArgs e)
            {
                string RequisicionId = string.Empty;

                RequisicionId = e.CommandArgument.ToString();
               
                if (e.CommandName == "Select")
                {
                    RequisicionIdHidden.Value = RequisicionId;
                    SeleccionarRequisicion(RequisicionId);
                    OcultarBusquedaRequisicion();
                }
            }

            public bool ValidarProducto()
            {
                string Mensaje = "";
                int Cantidad = 0;
                if (RequisicionIdHidden.Value == "") Mensaje = TextoInfo.MensajeSeleccioneRequisicion;
                if (!Int32.TryParse(CantidadBox.Text, out Cantidad)) Mensaje = TextoInfo.MensajeCantidadGenerico;
                if (Cantidad < 1) Mensaje = TextoInfo.MensajeCantidadGenerico;

                if (Mensaje == "") return true;
                MostrarMensaje(Mensaje,ConstantePrograma.TipoErrorAlerta);
                return false;
            }
        #endregion
    }
}
