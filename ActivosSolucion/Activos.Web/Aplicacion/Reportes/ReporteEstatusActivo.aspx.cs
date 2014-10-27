using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Comun.Fecha;
using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Aplicacion.Reportes
{
    public partial class ReporteEstatusActivo : System.Web.UI.Page
    {
        #region Eventos

        protected void BotonImprimir_Click(object sender, EventArgs e)
        {
            RecogerValores();

        }

        protected void BotonLimpiar_Click(object sender, EventArgs e)
        {
            //LimpiarFormulario();

        }

        protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSubfamilia();
        }

        protected void SalidaCheckBox_CheckedChanged(object sender, EventArgs e) 
        {
            HabilitarTipoServicio();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Inicio();
            }
        }

        #endregion

        #region Métodos

        protected void Inicio() 
        {
            if (!Page.IsPostBack)
            {
                //Se validan los permisos
                Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.ReporteEstatusActivos);

                SeleccionarFamilia();
                SeleccionarSubfamilia();
                SeleccionarTipoServicio();
            }
        }

        protected void ImprimirReporte() 
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "Imprimir()", true);
        }

        protected void HabilitarTipoServicio() 
        {
            if (Salida.Checked == true)
                CampoTipoServicio.Visible = true;
            else
                CampoTipoServicio.Visible = false;
        }

        protected void RecogerValores()
        {
            FamiliaIdHidden.Value = FamiliaId.SelectedValue.ToString();
            FamiliaNombreHidden.Value = FamiliaId.SelectedItem.Text;
            SubFamiliaHidden.Value = SubFamiliaId.SelectedValue.ToString();
            SubFamiliaNombreHidden.Value = SubFamiliaId.SelectedItem.Text;

            if (Asignados.Checked == true || NoAsignados.Checked == true || Salida.Checked == true || NoEtiquetado.Checked == true)
            {

                if (Asignados.Checked == true)
                    AsignadosHidden.Value = "1";
                else AsignadosHidden.Value = "";
                if (NoAsignados.Checked == true)
                    NoAsignadosHidden.Value = "1";
                else NoAsignadosHidden.Value = "";
                if (Salida.Checked == true)
                {
                    SalidaHidden.Value = "1";
                    TipoServicioHidden.Value = TipoServicio.SelectedValue.ToString();
                    TipoServicioNombreHidden.Value = TipoServicio.SelectedItem.Text;
                }
                else
                {
                    SalidaHidden.Value = "";
                    TipoServicioHidden.Value = TipoServicio.SelectedValue.ToString();
                    TipoServicioNombreHidden.Value = TipoServicio.SelectedItem.Text;
                }
                if (NoEtiquetado.Checked == true)
                    NoEtiquetadoHidden.Value = "1";
                else NoEtiquetadoHidden.Value = "";

                ImprimirReporte();
            }
            else EtiquetaMensajeError.Text = "Seleccione al menos un estatus";

        }

        protected void SeleccionarFamilia()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
            FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

            FamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusFamilia.Activo;

            Resultado = FamiliaProcesoObjeto.SeleccionarFamilia(FamiliaEntidadObjeto);

            FamiliaId.DataValueField = "FamiliaId";
            FamiliaId.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                FamiliaId.DataSource = Resultado.ResultadoDatos;
                FamiliaId.DataBind();
            }
            else
            {
                EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
            }

            FamiliaId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
        }

        protected void SeleccionarSubfamilia()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();
            SubFamiliaProceso SubFamiliaProcesoObjeto = new SubFamiliaProceso();

            SubFamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusSubFamilia.Activo;
            SubFamiliaEntidadObjeto.FamiliaId = Int16.Parse(FamiliaId.SelectedValue);

            if (SubFamiliaEntidadObjeto.FamiliaId == 0)
            {
                SubFamiliaId.Items.Clear();
            }
            else
            {
                Resultado = SubFamiliaProcesoObjeto.SeleccionarSubFamilia(SubFamiliaEntidadObjeto);

                SubFamiliaId.DataValueField = "SubFamiliaId";
                SubFamiliaId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    SubFamiliaId.DataSource = Resultado.ResultadoDatos;
                    SubFamiliaId.DataBind();
                }
                else
                {
                    EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
                }
            }

            SubFamiliaId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
        }

        protected void SeleccionarTipoServicio()
        {

            ResultadoEntidad Resultado = new ResultadoEntidad();
            TipoServicioEntidad TipoServicioEntidadObjeto = new TipoServicioEntidad();
            TipoServicioProceso TipoServicioProcesoObjeto = new TipoServicioProceso();


            Resultado = TipoServicioProcesoObjeto.SeleccionarTipoServicio(TipoServicioEntidadObjeto);
            TipoServicio.DataValueField = "TipoServicioId";
            TipoServicio.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                TipoServicio.DataSource = Resultado.ResultadoDatos;
                TipoServicio.DataBind();
            }
            else
            {
                EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
            }

            TipoServicio.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
        }


        #endregion

       
    }
}
