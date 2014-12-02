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
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Almacen;

namespace Almacen.Web.Aplicacion.Almacen
{
    public partial class OrdenSalida : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
            {

            }

            protected void ImagenBuscarPreOrden_Click(object sender, ImageClickEventArgs e)
            {
                ValidarRequisicion(RequisicionBox.Text.Trim());
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

                TablaOrden.DataSource = null;
                TablaOrden.DataBind();
            }

            private void LimpiarFormulario()
            {

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

            private void SeleccionarRequisicion(string ClaveRequisicion, string SesionId)
            {
                //RequisicionProceso RequisicionProceso = new RequisicionProceso();

                //RequisicionProceso.RequisicionEntidad.Clave = ClaveRequisicion;
                //RequisicionProceso.RequisicionEntidad.SesionId = SesionId;

                //RequisicionProceso.SeleccionarRequisicionOrdenSalida();

                //if (RequisicionProceso.ErrorId != 0)
                //{
                //    MostrarMensaje(RequisicionProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                //    return;
                //}

                // ToDo: Cambiar el estilo del grid si está vacío el dataset

                //if (RequisicionProceso.ResultadoDatos.Tables[0].Rows == 0)
                //    LimpiarFormulario();
                //else
                //{


                //}
            }

            private void ValidarRequisicion(string ClaveRequisicion)
            {
                UsuarioEntidad UsuarioEntidad = new UsuarioEntidad();

                UsuarioEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                // Validar que la requisición tenga un estatus válido


                SeleccionarRequisicion(ClaveRequisicion, UsuarioEntidad.SesionId);
            }
        #endregion
    }
}
