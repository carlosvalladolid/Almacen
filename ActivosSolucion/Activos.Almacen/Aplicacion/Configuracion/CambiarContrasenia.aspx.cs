using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Seguridad;

namespace Almacen.Web.Aplicacion.Configuracion
{
    public partial class CambiarContrasenia : System.Web.UI.Page
    {
        #region "Eventos"

        protected void BotonCancelar_Click(object sender, EventArgs e)
        {
            ResetFormControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        protected void BotonReset_Click(object sender, ImageClickEventArgs e)
        {
            ResetFormControl();
        }

        protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
        {
            GuardarCambioContrasenia();
        }


        #endregion

        #region "Métodos"

        protected void GuardarCambioContrasenia()
        {
            ResultadoEntidad ResultadoEntidad = new ResultadoEntidad();
            UsuarioEntidad UsuarioEntidadActual = new UsuarioEntidad();
            UsuarioEntidad UsuarioEntidadGuardar = new UsuarioEntidad();
            UsuarioProceso UsuarioProcesoObjeto = new UsuarioProceso();

            UsuarioEntidadActual = (UsuarioEntidad)Session["UsuarioEntidad"];

            UsuarioEntidadGuardar.UsuarioId = UsuarioEntidadActual.UsuarioId;
            UsuarioEntidadGuardar.UsuarioIdModifico = UsuarioEntidadActual.UsuarioId;
            UsuarioEntidadGuardar.ContraseniaAnterior = AnteriorContrasenia.Text.Trim();
            UsuarioEntidadGuardar.NuevaContrasenia = NuevaContrasenia.Text.Trim();
            UsuarioEntidadGuardar.Confirmacion = Confirmacion.Text.Trim();

            ResultadoEntidad = UsuarioProcesoObjeto.CambiarContrasenia(UsuarioEntidadGuardar);

            if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.Usuario.GuardadoExitoso)
                ResetFormControl();
            else
                MostrarMensaje(ResultadoEntidad.DescripcionError, ConstantePrograma.TipoErrorAlerta);
        }

        protected void Inicio()
        {
            if (!Page.IsPostBack)
            {
                ResetFormControl();
                SeleccionarTextoError();
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

        protected void ResetFormControl()
        {
            AnteriorContrasenia.Text = "";
            NuevaContrasenia.Text = "";
            Confirmacion.Text = "";
            
        }

        protected void SeleccionarTextoError()
        {
            AnteriorContraseniaRequerida.ErrorMessage = TextoError.AnteriorContrasenia + "<br />";
            NuevaContraseniaRequerida.ErrorMessage = TextoError.NuevaContrasenia + "<br />";
            ConfirmacionRequerida.ErrorMessage = TextoError.Confirmacion + "<br />";
        }

        #endregion
    }
}
