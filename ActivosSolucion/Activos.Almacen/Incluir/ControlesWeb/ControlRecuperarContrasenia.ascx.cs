using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Seguridad;

namespace Activos.Web.Incluir.ControlesWeb
{
    public partial class ControlRecuperarContrasenia : System.Web.UI.UserControl
    {
        #region "Eventos"
            protected void BotonCancelar_Click(object sender, EventArgs e)
            {
                OcultarPanel();
            }

            protected void BotonEnviar_Click(object sender, EventArgs e)
            {
                RecuperarContrasenia();
            }

            protected void Page_Load(object sender, EventArgs e)
            {

            }
        #endregion

        #region "Métodos"
            protected void RecuperarContrasenia()
            {
                UsuarioEntidad UsuarioEntidadObjeto = new UsuarioEntidad();

                UsuarioEntidadObjeto.AplicacionId = (int)ConstantePrograma.AplicacionId.Activos;
                UsuarioEntidadObjeto.CuentaUsuario = CuentaUsuario.Text.Trim();

                RecuperarContrasenia(UsuarioEntidadObjeto);
            }

            protected void RecuperarContrasenia(UsuarioEntidad UsuarioEntidadObjeto)
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                UsuarioProceso UsuarioProcesoObjeto = new UsuarioProceso();

                ResultadoEntidadObjeto = UsuarioProcesoObjeto.RecuperarContrasenia(UsuarioEntidadObjeto);

                switch (ResultadoEntidadObjeto.ErrorId)
                {
                    case (int)ConstantePrograma.RecuperarContrasenia.CorreoEnviado:
                        LimpiarCampos();
                        OcultarPanel();
                        break;

                    case (int)ConstantePrograma.RecuperarContrasenia.CuentaNoValida:
                        EtiquetaMensaje.Text = TextoError.RecuperarCuentaNoValida;
                        break;

                    case (int)ConstantePrograma.RecuperarContrasenia.CuentaNoExiste:
                        EtiquetaMensaje.Text = TextoError.RecuperarCuentaNoExiste;
                        break;

                    default:
                        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                        break;
                }
            }

            public void Inicio()
            {
                LimpiarCampos();
                MostrarPanel();
                SeleccionarTextoError();
            }

            protected void LimpiarCampos()
            {
                CuentaUsuario.Text = "";
                EtiquetaMensaje.Text = "";
            }

            protected void MostrarPanel()
            {
                PanelFondo.Visible = true;
                PanelRecuperar.Visible = true;
            }

            protected void OcultarPanel()
            {
                PanelFondo.Visible = false;
                PanelRecuperar.Visible = false;
            }

            protected void SeleccionarTextoError()
            {
                CorreoRequerido.ErrorMessage = TextoError.RecuperarCorreo;
                CorreoExpresion.ErrorMessage = TextoError.RecuperarCuentaNoValida;
            }
        #endregion
    }
}