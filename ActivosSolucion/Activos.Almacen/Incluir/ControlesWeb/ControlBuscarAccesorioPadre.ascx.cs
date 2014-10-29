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
    public partial class ControlBuscarAccesorioPadre : System.Web.UI.UserControl

    {
       #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imgCancelarBuscarAccesorio_Click(object sender, ImageClickEventArgs e)
        {
            OcultarControlPanel();
        }

        protected void TablaActivosEncontrados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //TablaActivosEncontrados.PageIndex = e.NewPageIndex;
            //BuscarActivo();
        }

        protected void imgAceptarAccesorio_Click(object sender, EventArgs e)
        {
            
            AceptarDarBajaConAccesorios();
            OcultarControlPanel();


        }

        protected void imgCancelar_Click(object sender, EventArgs e) 
        {
            CancelarOperacion();
            OcultarControlPanel();
             
        }

        protected void TablaActivosEncontrados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TablaActivosEncontradosEventoComando(e);
        }
        #endregion 

        #region Métodos 

        public void InicioControl(AccesorioEntidad AccesorioEntidadObjeto)
        {

            MostrarControlPanel();
            BuscarActivo(AccesorioEntidadObjeto);
            LlenarControl(AccesorioEntidadObjeto);


        }

        protected void AceptarDarBajaConAccesorios() 
        {
            this.Page.GetType().InvokeMember("EsActivoPadre", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
        }

        protected void BuscarActivo(AccesorioEntidad AccesorioEntidadObjeto)
        {
            ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
            AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            AccesorioEntidadObjeto.TipoAccesorioId = (int)ConstantePrograma.TipoAccesorio.ActivoFijo;

            Resultado = AccesorioProcesoObjeto.SeleccionarAccesorio(AccesorioEntidadObjeto);

            TablaAccesoriosHijos.DataSource = Resultado.ResultadoDatos;
            TablaAccesoriosHijos.DataBind();

        }

        protected void CancelarOperacion()
        {

            //this.Page.GetType().InvokeMember("LimpiarFormulario", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
            this.Page.GetType().InvokeMember("CancelarOperacion", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
            
        }

        protected void LlenarControl(AccesorioEntidad AccesorioObjetoEntidad)
        {
            ActivoProceso ActivoPocesoObjeto = new ActivoProceso();
            ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            ActivoEntidadObjeto.ActivoId = AccesorioObjetoEntidad.ActivoId;
            Resultado = ActivoPocesoObjeto.SeleccionarActivo(ActivoEntidadObjeto);

            NumeroSerie.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroSerie"].ToString();
            CodigoBarrasParticular.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoBarrasParticular"].ToString();
            Descripcion.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
            Modelo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Modelo"].ToString();
            AceptarMensajeDarSalida.Text = "Este activo cuenta con accesorios asignados, \n ¿Desea darle salida al Activo junto con sus accesorios?";

        }

        public void MostrarControlPanel()
        {
            pnlFondoBuscarAccesorio.Visible = true;
            pnlControlBuscarAccesorio.Visible = true;
        }

        protected void OcultarControlPanel()
        {
            pnlFondoBuscarAccesorio.Visible = false;
            pnlControlBuscarAccesorio.Visible = false;
        }
        
        }
        #endregion

    }
