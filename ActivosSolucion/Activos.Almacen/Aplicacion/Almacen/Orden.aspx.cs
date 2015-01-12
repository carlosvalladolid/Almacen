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

namespace Almacen.Web.Aplicacion.Almacen
{
    public partial class Orden : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
            {
                GuardarOrden();
            }

            protected void EmpleadoCombo_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarJefe(Int16.Parse(EmpleadoCombo.SelectedValue));
            }

            protected void ImagenBuscarPreOrden_Click(object sender, ImageClickEventArgs e)
            {
                ValidarPreOrden(PreOrdenBusqueda.Text.Trim());
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

                OrdenProceso.GuardarOrden();

                if (OrdenProceso.ErrorId == 0)
                {
                    LimpiarFormulario();
                    MostrarMensaje(TextoInfo.MensajeGuardadoGenerico, ConstantePrograma.TipoMensajeAlerta);
                }
                else
                    MostrarMensaje(OrdenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
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

                TablaPreOrden.DataSource = null;
                TablaPreOrden.DataBind();

                TablaOrden.DataSource = null;
                TablaOrden.DataBind();

                SeleccionarProveedor();
                SeleccionarEmpleado();
            }

            private void LimpiarFormulario()
            {
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
                        break;
                }
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
        #endregion
    }
}
