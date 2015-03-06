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

using Activos.Entidad.General;
using System.Text;

namespace Almacen.Web.Incluir.ControlesWeb
{
    public partial class ControlMenuIzquierdo : System.Web.UI.UserControl
    {
        #region "Eventos"
            protected void Page_Load(object sender, EventArgs e)
            {
                PageLoad();
                CargarMenu();
                
            }
        #endregion

        #region "Métodos"
            private void PageLoad()
            {

            }

            private void CargarMenu()
            {
                ResultadoEntidad ResultadoEntidad = new ResultadoEntidad();
                StringBuilder Controles = new StringBuilder();

                Controles.Append("<table class='LeftMenuTable' runat='server' id='LeftMenuTable'>");

                string Proyecto = "Almacen";
                //Validamos permisos
                Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                ResultadoEntidad = BaseProcesoNegocio.SeleccionarRolPagina(0, HttpContext.Current,Proyecto);
                
                int Cantidad = 0;
                foreach(DataRow FilaCategoria in ResultadoEntidad.ResultadoDatos.Tables[1].Rows)
                {
                    Cantidad = 0;
                    foreach(DataRow FilaEnlace in ResultadoEntidad.ResultadoDatos.Tables[0].Rows)
                        if (FilaEnlace["ModuloId"].ToString() == FilaCategoria["ModuloId"].ToString())
                            Cantidad++;

                    if (Cantidad > 0)
                    {
                        Controles.Append("    <tr>");
                        Controles.Append("        <td class='Space'></td>");
                        Controles.Append("        <td><img alt='Inicio' border='0' src='"+FilaCategoria["RutaIcono"]+"' title='Pantalla de inicio' /></td>");
                        Controles.Append("        <td class='Section'>"+FilaCategoria["Nombre"]+"</td>");
                        Controles.Append("    </tr>");
                        foreach (DataRow FilaEnlace in ResultadoEntidad.ResultadoDatos.Tables[0].Rows)
                        {
                            if (FilaEnlace["ModuloId"].ToString() == FilaCategoria["ModuloId"].ToString())
                            {
                                Controles.Append("<tr>");
                                Controles.Append("    <td class='Space'></td>");
                                Controles.Append("    <td></td>");
                                Controles.Append("    <td><a href='" + FilaEnlace["URL"] + "'>" + FilaEnlace["PaginaNombre"] + "</a></td>");
                                Controles.Append("</tr>");
                            }
                        }
                    }
                }

                Controles.Append("</table>");

                LeftMenuTableDiv.InnerHtml = Controles.ToString();
            }
        #endregion
    }
}