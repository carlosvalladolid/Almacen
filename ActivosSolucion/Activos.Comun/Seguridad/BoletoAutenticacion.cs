using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text;

namespace Activos.Comun.Seguridad
{
    public class BoletoAutenticacion : Base
    {
        public static void IdentificarUsuario(string Usuario, int TiempoSesion)
        {
            FormsAuthenticationTicket Boleto;
            string CookieBoleto;
            HttpContext httpContext = HttpContext.Current;
            HttpCookie Cookie;

            Boleto = new FormsAuthenticationTicket(1, Usuario, DateTime.Now, DateTime.Now.AddMinutes(TiempoSesion), true, "");
            CookieBoleto = FormsAuthentication.Encrypt(Boleto);
            Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, CookieBoleto);
            Cookie.Expires = Boleto.Expiration;
            Cookie.Path = FormsAuthentication.FormsCookiePath;

            httpContext.Response.Cookies.Add(Cookie);
        }
    }
}
