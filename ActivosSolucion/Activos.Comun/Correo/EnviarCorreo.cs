using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Activos.Comun.Correo
{
    public class EnviarCorreo
    {
        public static int SendEmail()
        {
            int intPort = 25;
            string strTo = string.Empty;
            string strFrom = string.Empty;
            string strSubject = string.Empty;
            string strBody = string.Empty;
            string strSmtpHost = string.Empty;
            string strAccount = string.Empty;
            string strPassword = string.Empty;
            bool bolIsBodyHtml = true;
            MailMessage mmEmail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            try
            {
                mmEmail.From = new MailAddress(strFrom);
                mmEmail.To.Add(strTo);
                mmEmail.Subject = strSubject;
                mmEmail.Body = strBody;
                mmEmail.IsBodyHtml = bolIsBodyHtml;
                mmEmail.Priority = MailPriority.Normal;

                smtpClient.Host = strSmtpHost;
                smtpClient.Port = intPort;
                smtpClient.Credentials = new NetworkCredential(strAccount, strPassword);
                smtpClient.Send(mmEmail);

                return (int)Constante.ConstantePrograma.EnviarCorreo.ValorPorDefecto;
            }
            catch (Exception ex)
            {
                return (int)Constante.ConstantePrograma.EnviarCorreo.ErrorInesperado;
            }
        }
    }
}
