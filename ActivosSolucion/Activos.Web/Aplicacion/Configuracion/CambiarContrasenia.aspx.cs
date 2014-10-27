using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Seguridad;


namespace Activos.Web.Aplicacion.Configuracion
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

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    ResetFormControl();
                    SeleccionarTextoError();
                }
            }

            protected void ResetFormControl()
            {
                AnteriorContrasenia.Text = "";
                NuevaContrasenia.Text = "";
                Confirmacion.Text = "";
            }

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
                    MessageLabel.Text = ResultadoEntidad.DescripcionError;
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
