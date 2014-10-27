using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Activos.Comun.Correo
{
    public class Servidor
    {
        public int Puerto = 25;
        public string Para = string.Empty;
        public string DeParte = string.Empty;
        public string Asunto = string.Empty;
        public string Cuerpo = string.Empty;
        public string SmtpHost = string.Empty;
        public string CuentaUsuario = string.Empty;
        public string Contrasenia = string.Empty;

        public int EnviarCorreo()
        {
            bool EsCodigoHTML = true;
            MailMessage Correo = new MailMessage();
            SmtpClient ClienteSmtp = new SmtpClient();

            try
            {
                Correo.From = new MailAddress(DeParte);
                Correo.To.Add(Para);
                Correo.Subject = Asunto;
                Correo.Body = Cuerpo;
                Correo.IsBodyHtml = EsCodigoHTML;
                Correo.Priority = MailPriority.Normal;

                ClienteSmtp.Host = SmtpHost;
                ClienteSmtp.Port = Puerto;
                ClienteSmtp.Credentials = new NetworkCredential(CuentaUsuario, Contrasenia);
                ClienteSmtp.Send(Correo);

                return (int)Constante.ConstantePrograma.EnviarCorreo.ValorPorDefecto;
            }
            catch
            {
                return (int)Constante.ConstantePrograma.EnviarCorreo.ErrorInesperado;
            }
        }
    }
}
