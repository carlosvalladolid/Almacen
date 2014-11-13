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

using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.Catalogo;
using Activos.Entidad.General;
using Activos.ProcesoNegocio.Almacen;
using Activos.ProcesoNegocio.Catalogo;

namespace Almacen.Web.Aplicacion.Almacen
{
    public partial class Orden : System.Web.UI.Page
    {
        #region "Eventos"
            protected void EmpleadoCombo_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarJefe(Int16.Parse(EmpleadoCombo.SelectedValue));
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

                SeleccionarPreOrden();
                SeleccionarOrden();
                SeleccionarProveedor();
                SeleccionarEmpleado();

                JefeCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarEmpleado()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

                EmpleadoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusEmpleados.Activo;

                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                EmpleadoCombo.DataValueField = "EmpleadoId";
                EmpleadoCombo.DataTextField = "NombreEmpleadoCompleto";

                if (Resultado.ErrorId == 0)
                {
                    EmpleadoCombo.DataSource = Resultado.ResultadoDatos;
                    EmpleadoCombo.DataBind();
                }
                else
                {
                    // ToDo: Manejar mensajes de error
                    //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                EmpleadoCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarJefe(Int16 EmpleadoIdJefe)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

                if (EmpleadoIdJefe == 0)
                {
                    JefeCombo.Items.Clear();
                    JefeCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));

                    return;
                }

                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoIdJefe;

                Resultado = EmpleadoProcesoNegocio.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                JefeCombo.DataValueField = "EmpleadoIdJefe";
                JefeCombo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    JefeCombo.DataSource = Resultado.ResultadoDatos;
                    JefeCombo.DataBind();
                }
                else
                {
                    //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                JefeCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarOrden()
            {
                TablaOrden.DataSource = null;
                TablaOrden.DataBind();
            }

            private void SeleccionarPreOrden()
            {
                TablaPreOrden.DataSource = null;
                TablaPreOrden.DataBind();
            }

            private void SeleccionarProveedor()
            {
                Activos.ProcesoNegocio.Almacen.ProveedorProceso ProveedorProceso = new Activos.ProcesoNegocio.Almacen.ProveedorProceso();

                ProveedorProceso.SeleccionarProveedor();

                ProveedorCombo.DataValueField = "DependenciaId";
                ProveedorCombo.DataTextField = "Nombre";

                if (ProveedorProceso.ErrorId == 0)
                {
                    ProveedorCombo.DataSource = ProveedorProceso.ResultadoDatos;
                    ProveedorCombo.DataBind();
                }
                else
                {
                    // ToDo: Manejar mensajes de error
                    //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                ProveedorCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void ShowMessage(string Mensaje, string TipoMensaje)
            {
                StringBuilder FormatoMensaje = new StringBuilder();

                FormatoMensaje.Append("MostrarMensaje(\"");
                FormatoMensaje.Append(Mensaje);
                FormatoMensaje.Append("\", \"");
                FormatoMensaje.Append(TipoMensaje);
                FormatoMensaje.Append("\");");

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Mensaje", Comparar.ReemplazarCadenaJavascript(FormatoMensaje.ToString()), true);
            }
        #endregion
    }
}
