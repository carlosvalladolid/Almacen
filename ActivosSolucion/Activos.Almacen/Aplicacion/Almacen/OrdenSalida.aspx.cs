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
using Activos.Comun.Fecha;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Almacen;

namespace Almacen.Web.Aplicacion.Almacen
{
    public partial class OrdenSalida : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonRequisicionBusqueda(object sender, ImageClickEventArgs e)
            {
                SeleccionarRequisicion();
            }

            protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
            {

            }

            protected void BotonRequisicionCerrar_Click(object sender, ImageClickEventArgs e)
            {
                OcultarBusquedaRequisicion();
            }

            protected void ImagenBuscarRequisicion_Click(object sender, ImageClickEventArgs e)
            {
                BuscarRequisicion();
            }

            protected void ImagenBuscarArticulo_Click(object sender, ImageClickEventArgs e)
            {
               
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

            protected void TablaRequisicionBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
            {

            }
        #endregion

        #region "Métodos"
            private void BuscarRequisicion()
            {
                SeleccionarEstatus();

                MostrarBusquedaRequisicion();
            }

            private void Inicio()
            {
                if (Page.IsPostBack)
                    return;
                
                TablaOrden.DataSource = null;
                TablaOrden.DataBind();

                TablaRequisicionBusqueda.DataSource = null;
                TablaRequisicionBusqueda.DataBind();
            }

            private void LimpiarFormulario()
            {
                RequisicionBox.Text = "";
                SolicitanteBox.Text = "";
                DependenciaBox.Text = "";
                DireccionBox.Text = "";
                PuestoBox.Text = "";
                JefeBox.Text = "";
                //***********************
                ClaveRequisicionBox.Text = "";
                FamiliaBox.Text = "";
                SubFamiliaBox.Text = "";
                MarcaBox.Text = "";                
                DescripcionBox.Text = "";
                CantidadBox.Text = "";

            }

            private void MostrarBusquedaRequisicion()
            {
                FondoBusquedaRequisicion.Visible = true;
                PanelBusquedaRequisicion.Visible = true;
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

            private void OcultarBusquedaRequisicion()
            {
                FondoBusquedaRequisicion.Visible = false;
                PanelBusquedaRequisicion.Visible = false;
            }

            protected void SeleccionarEstatus()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EstatusEntidad EstatusEntidad = new EstatusEntidad();
                EstatusProceso EstatusProceso = new EstatusProceso();

                EstatusEntidad.SeccionId = (int)ConstantePrograma.Seccion.Familia;

                Resultado = EstatusProceso.SeleccionarEstatus(EstatusEntidad);

                EstatusBusquedaCombo.DataValueField = "EstatusId";
                EstatusBusquedaCombo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    EstatusBusquedaCombo.DataSource = Resultado.ResultadoDatos;
                    EstatusBusquedaCombo.DataBind();
                }
                else
                    MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);

                EstatusBusquedaCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
            }

            private void SeleccionarRequisicion()
            {
                RequisicionProceso RequisicionProceso = new RequisicionProceso();

                //RequisicionProceso.RequisicionEntidad.Clave = ClaveRequisicion;
                // RequisicionProceso.RequisicionEntidad.SesionId = SesionId;

                //RequisicionProceso.SeleccionarRequisicion();

                if (RequisicionProceso.ErrorId != 0)
                {
                    MostrarMensaje(RequisicionProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                // ToDo: Cambiar el estilo del grid si está vacío el dataset

                if (RequisicionProceso.ResultadoDatos.Tables[0].Rows.Count == 0)
                    LimpiarFormulario();
                else
                {

                    SolicitanteBox.Text = RequisicionProceso.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleado"].ToString();
                    DependenciaBox.Text = RequisicionProceso.ResultadoDatos.Tables[0].Rows[0]["Dependencia"].ToString();
                    DireccionBox.Text = RequisicionProceso.ResultadoDatos.Tables[0].Rows[0]["Direccion"].ToString();
                    PuestoBox.Text = RequisicionProceso.ResultadoDatos.Tables[0].Rows[0]["Puesto"].ToString();
                    JefeBox.Text = RequisicionProceso.ResultadoDatos.Tables[0].Rows[0]["Jefe"].ToString();
                }
            }
        #endregion
    }
}
