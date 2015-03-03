using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Activos.Comun.Constante;

namespace Activos.Comun.Correo
{
    public class CuerpoCorreo
    {
        public static string SeleccionarCuerpoCorreo(string CorreoPara)
        {
            StringBuilder CuerpoCorreo = new StringBuilder();

            switch (CorreoPara)
            {
                case ConstantePrograma.CorreoNuevoUsuario:
                    CuerpoCorreo.Append("<div>");
                    CuerpoCorreo.Append("<p>Se ha creado un nuevo usuario para usted en la aplicación Web de Activos. Esta información es requerida para acceder al sistema.<br /><br />");
                    CuerpoCorreo.Append("<b>Usuario:</b> {0}<br />");
                    CuerpoCorreo.Append("<b>Contraseña:</b> {1}<br />");
                    CuerpoCorreo.Append("</p>");
                    CuerpoCorreo.Append("<p>No responda a este correo, ya que es generado automáticamente por la aplicación.</p>");
                    CuerpoCorreo.Append("<p>Para dudas o sugerencias, por favor contacte al administrador del sistema.</p>");
                    CuerpoCorreo.Append("</div>");
                    break;

                case ConstantePrograma.CorreoRecuperarContrasenia:
                    CuerpoCorreo.Append("<div>");
                    CuerpoCorreo.Append("<p>La siguiente información ha sido solicitada a través del sitio Web de Activos. Esta información es requerida para acceder al sistema.<br /><br />");
                    CuerpoCorreo.Append("<b>Usuario:</b> {0}<br />");
                    CuerpoCorreo.Append("<b>Contraseña:</b> {1}<br />");
                    CuerpoCorreo.Append("</p>");
                    CuerpoCorreo.Append("<p>No responda a este correo, ya que es generado automáticamente por la aplicación.</p>");
                    CuerpoCorreo.Append("<p>Para dudas o sugerencias, por favor contacte al administrador del sistema.</p>");
                    CuerpoCorreo.Append("</div>");
                    break;

                default:
                    CuerpoCorreo.Append("");
                    break;
            }

            return CuerpoCorreo.ToString();
        }

        public static string SeleccionarCuerpoCorreoAlmacen(string CorreoPara)
        {
            StringBuilder CuerpoCorreo = new StringBuilder();

            switch (CorreoPara)
            {
                case ConstantePrograma.CorreoNuevoUsuario:
                    CuerpoCorreo.Append("<div>");
                    CuerpoCorreo.Append("<p>Se ha generado una solicitud de Requisicion para usted en la aplicación Web de Almacen.<br /><br />");
                    CuerpoCorreo.Append("<b>Solicitante:</b> {0}<br />");
                    CuerpoCorreo.Append("<b>Dependencia:</b> {1}<br />");
                    CuerpoCorreo.Append("<b>Dirección:</b> {2}<br />");
                    CuerpoCorreo.Append("<b>Puesto:</b> {3}<br />");
                    CuerpoCorreo.Append("<b>Jefe Inmediato:</b> {4}<br />");
                    CuerpoCorreo.Append("<b>Fecha Requisición:</b> {5}<br />");
                    CuerpoCorreo.Append("</p>");
                    CuerpoCorreo.Append("<p>No responda a este correo, ya que es generado automáticamente por la aplicación.</p>");
                    CuerpoCorreo.Append("<p>Para dudas o sugerencias, por favor contacte al administrador del sistema.</p>");
                    CuerpoCorreo.Append("</div>");
                    break;

                default:
                    CuerpoCorreo.Append("");
                    break;
            }

            return CuerpoCorreo.ToString();
        }
    }
}
