using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Incluir.ControlesWeb
{
    public partial class ControlBuscarActivo : System.Web.UI.UserControl
    {

        public enum Behavior
        {
            EquipoComputo = (Int16)ConstantePrograma.TipoAtivo.EquipoComputo,
            Mobiliario = (Int16)ConstantePrograma.TipoAtivo.Mobiliario,
            Vehiculo = (Int16)ConstantePrograma.TipoAtivo.Vehiculo
        }

        public Behavior TipoActivo
        {
            get { return (Behavior)ViewState[this.ID + "_TipoActivo"]; }
            set { ViewState[this.ID + "_TipoActivo"] = value; }
        }

        #region "Eventos"

            protected void BotonCancelarBusquedaActivo_Click(object sender, EventArgs e)
            {
                OcultarControlPanel();
            }

            protected void BotonBusquedaActivo_Click(object sender, EventArgs e)
            {
                BuscarActivo();
            }

            protected void imgCancelarBuscarActivo_Click(object sender, ImageClickEventArgs e)
            {
                OcultarControlPanel();
            }

            protected void TablaActivosEncontrados_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaActivosEncontradosEventoComando(e);
            }

            protected void TablaActivosEncontrados_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaActivosEncontrados.PageIndex = e.NewPageIndex;
                BuscarActivo();
            }

            protected void Page_Load(object sender, EventArgs e)
            {

            }

        #endregion

        #region "Métodos"

            protected void BuscarActivo()
            {
                ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

                ActivoEntidadObjeto.NumeroSerie = NumeroSerieBusqueda.Text.Trim();
                ActivoEntidadObjeto.CodigoBarrasParticular = CodigoBarrasParticularBusqueda.Text.Trim();
                ActivoEntidadObjeto.Descripcion = DescripcionBusqueda.Text.Trim();
                ActivoEntidadObjeto.Modelo = ModeloBusqueda.Text.Trim();
                ActivoEntidadObjeto.Color = ColorBusqueda.Text.Trim();

                switch (TipoActivo)
                {
                    case Behavior.Vehiculo:
                        ActivoEntidadObjeto.GrupoTipoActivoId = "," + (int)ConstantePrograma.TipoAtivo.Vehiculo + ",";
                        break;
                }


                BuscarActivo(ActivoEntidadObjeto);
            }

            protected void BuscarActivo(ActivoEntidad ActivoEntidadObjeto)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoProceso ActivoProcesoNegocio = new ActivoProceso();

                Resultado = ActivoProcesoNegocio.SeleccionarActivoAvanzado(ActivoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaActivosEncontrados.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaActivosEncontrados.CssClass = ConstantePrograma.ClaseTabla;

                    TablaActivosEncontrados.DataSource = Resultado.ResultadoDatos;
                    TablaActivosEncontrados.DataBind();
                }
                else
                {
                    EtiquetaControlBuscarActivoMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            public void InicioControl()
            {
                MostrarControlPanel();
                LimpiarControl();
                BuscarActivo();
            }

            protected void LimpiarControl()
            {
                NumeroSerieBusqueda.Text = "";
                CodigoBarrasParticularBusqueda.Text = "";
                DescripcionBusqueda.Text = "";
                ModeloBusqueda.Text = "";
                ColorBusqueda.Text = "";
                EtiquetaControlBuscarActivoMensaje.Text = "";
            }

            public void MostrarControlPanel()
            {
                pnlFondoBuscarActivo.Visible = true;
                pnlControlBuscarActivo.Visible = true;
            }

            protected void OcultarControlPanel()
            {
                pnlFondoBuscarActivo.Visible = false;
                pnlControlBuscarActivo.Visible = false;
            }

            protected void TablaActivosEncontradosEventoComando(GridViewCommandEventArgs e)
            {
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 ActivoId = 0;
                string Descripcion = string.Empty;
                string NumeroSerie = string.Empty;
                string Modelo = string.Empty;
                string Color = string.Empty;

                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaActivosEncontrados.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaActivosEncontrados.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        ActivoId = Int16.Parse(TablaActivosEncontrados.DataKeys[intFila]["ActivoId"].ToString());
                        Descripcion = TablaActivosEncontrados.DataKeys[intFila]["Descripcion"].ToString();
                        NumeroSerie = TablaActivosEncontrados.DataKeys[intFila]["NumeroSerie"].ToString();
                        Modelo = TablaActivosEncontrados.DataKeys[intFila]["Modelo"].ToString();
                        Color = TablaActivosEncontrados.DataKeys[intFila]["Color"].ToString();

                        this.Page.GetType().InvokeMember("BuscarActivoPorId", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { ActivoId, Descripcion, NumeroSerie, Modelo, Color });

                        OcultarControlPanel();
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

        #endregion
    }
}