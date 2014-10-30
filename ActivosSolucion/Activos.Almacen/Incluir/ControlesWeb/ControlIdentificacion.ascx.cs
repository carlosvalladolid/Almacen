using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Seguridad;

namespace Almacen.Web.Incluir.ControlesWeb
{
    public partial class ControlIdentificacion : System.Web.UI.UserControl
    {
        #region "Eventos"
            protected void OlvidoContrasenia_Click(object sender, EventArgs e)
            {
                RecuperarContrasenia.Inicio();
            }

            protected void BotonAceptar_Click(object sender, EventArgs e)
            {
                IdentificarUsuario(CuentaUsuario.Text.Trim(), Contrasenia.Text.Trim(), RecordarContrasenia.Checked);
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }
        #endregion

        #region "Métodos"
            protected void IdentificarUsuario(string CuentaUsuario, string Contrasenia, bool RecordarContrasenia)
            {
                ResultadoEntidad ResultadoObjetoEntidad = new ResultadoEntidad();
                UsuarioEntidad UsuarioObjetoEntidad = new UsuarioEntidad();
                UsuarioProceso UsuarioObjetoProceso = new UsuarioProceso();

                UsuarioObjetoEntidad.CuentaUsuario = CuentaUsuario;
                UsuarioObjetoEntidad.Contrasenia = Contrasenia;
                UsuarioObjetoEntidad.RecordarContrasenia = RecordarContrasenia;

                ResultadoObjetoEntidad = UsuarioObjetoProceso.IdentificarUsuario(UsuarioObjetoEntidad);

                switch (ResultadoObjetoEntidad.ErrorId)
                {
                    case (int)ConstantePrograma.IdentificarUsuario.ValorPorDefecto:
                        Response.Redirect(ConfigurationManager.AppSettings["Activos.Web.UserIndexURL"].ToString(), true);
                        break;

                    case (int)ConstantePrograma.IdentificarUsuario.UsuarioContraseniaIncorrecta:
                        EtiquetaMensaje.Text = TextoError.IdentificarUsuarioContraseniaIncorrecta + "<br />";
                        break;

                    case (int)ConstantePrograma.IdentificarUsuario.UsuarioInactivo:
                        EtiquetaMensaje.Text = TextoError.IdentificarUsuarioInactivo + "<br />";
                        break;

                    default:
                        EtiquetaMensaje.Text = ResultadoObjetoEntidad.DescripcionError + "<br />";
                        break;
                }
            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    CuentaUsuario.Text = UsuarioProceso.ObtenerCookie(ConstantePrograma.CookieCuenta, HttpContext.Current);

                    ObtenerContrasenia();
                }
            }

            protected void ObtenerContrasenia()
            {
                string RememberPassword = string.Empty;

                RememberPassword = UsuarioProceso.ObtenerCookie(ConstantePrograma.CookieRecordar, HttpContext.Current);

                CookieOculto.Value = RememberPassword;
            }
        #endregion
    }
}