using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Activos;

namespace Activos.Web.Aplicacion.Catalogo
{
    public partial class Marca : System.Web.UI.Page
    {
        #region "Eventos"

        protected void BotonBusqueda_Click(object sender, EventArgs e)
        {
            TextoBusquedaRapida.Text = "";
            BusquedaAvanzada();
        }

        protected void BotonCancelarBusqueda_Click(object sender, EventArgs e)
        {
            CambiarBusquedaAvanzada();
        }

        protected void BotonCancelarNuevo_Click(object sender, EventArgs e)
        {
            CambiarNuevoRegistro();
        }

        protected void BusquedaAvanzadaLink_Click(object sender, EventArgs e)
        {
            CambiarBusquedaAvanzada();
        }

        protected void BotonGuardar_Click(object sender, EventArgs e)
        {
            GuardarMarca();
        }

        protected void EliminarRegistroLink_Click(object sender, EventArgs e)
        {
            EliminarMarca();
        }

        protected void ImagenBuscar_Click(object sender, ImageClickEventArgs e)
        {
            BusquedaAvanzada();
        }

        protected void NuevoRegistroLink_Click(object sender, EventArgs e)
        {
            CambiarNuevoRegistro();
        }
                
        protected void TablaMarca_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TablaMarca.PageIndex = e.NewPageIndex;
            BusquedaAvanzada();
        }

        protected void TablaMarca_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaMarcaEventoComando(e);
        }
                
        protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }
        #endregion

        #region "Métodos"

        protected void EliminarMarca()
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            MarcaEntidad MarcaEntidadObjeto = new MarcaEntidad();

            MarcaEntidadObjeto.CadenaMarcaId = ObtenerCadenaMarcaId();

            EliminarMarca(MarcaEntidadObjeto);
        }

        protected void EliminarMarca(MarcaEntidad MarcaEntidadObjeto)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            MarcaProceso MarcaProcesoObjeto = new MarcaProceso();

            ResultadoEntidadObjeto = MarcaProcesoObjeto.EliminarMarca(MarcaEntidadObjeto);

            if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Marca.EliminacionExitosa)
            {
                EtiquetaMensaje.Text = "";

                BusquedaAvanzada();
            }
            else
            {
                EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
            }
        }

        protected void GuardarMarca()
        {
            MarcaEntidad MarcaObjetoEntidad = new MarcaEntidad();
            UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

            UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

            MarcaObjetoEntidad.MarcaId = Int16.Parse(MarcaIdHidden.Value);
            MarcaObjetoEntidad.DependenciaId = Int16.Parse(DependenciaNuevo.SelectedValue);
            MarcaObjetoEntidad.Nombre = NombreNuevo.Text.Trim();
            MarcaObjetoEntidad.EstatusId = Int16.Parse(EstatusNuevo.SelectedValue);
            GuardarMarca(MarcaObjetoEntidad);
        }

        protected void GuardarMarca(MarcaEntidad MarcaObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MarcaProceso MarcaProcesoNegocio = new MarcaProceso();

            Resultado = MarcaProcesoNegocio.GuardarMarca(MarcaObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Marca.MarcaGuardadoCorrectamente)
            {
                LimpiarNuevoRegistro();
                PanelNuevoRegistro.Visible = false;
                PanelBusquedaAvanzada.Visible = false;
                BusquedaAvanzada();
            }
            else
            {
                EtiquetaMensaje.Text = Resultado.DescripcionError;
            }
        }

        protected void CambiarEditarRegistro()
        {
            PanelBusquedaAvanzada.Visible = false;
            PanelNuevoRegistro.Visible = true;
        }

        protected void SeleccionarMarcaParaEditar(MarcaEntidad MarcaObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MarcaProceso MarcaProcesoNegocio = new MarcaProceso();

            Resultado = MarcaProcesoNegocio.SeleccionarMarca(MarcaObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                DependenciaNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["DependenciaId"].ToString();
                EstatusNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString();
                NombreNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
                //SeleccionarDependenciaNuevo();
                CambiarEditarRegistro();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }
        }

        protected void SeleccionarDependenciaNuevo()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            DependenciaEntidad DependenciaEntidadObjeto = new DependenciaEntidad();
            DependenciaProceso DependenciaProcesoObjeto = new DependenciaProceso();

            Resultado = DependenciaProcesoObjeto.SeleccionarDependencia(DependenciaEntidadObjeto);

            DependenciaNuevo.DataValueField = "DependenciaId";
            DependenciaNuevo.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                DependenciaNuevo.DataSource = Resultado.ResultadoDatos;
                DependenciaNuevo.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            DependenciaNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void BusquedaAvanzada()
        {
            MarcaEntidad MarcaEntidadObjeto = new MarcaEntidad();

            MarcaEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
            MarcaEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

            SeleccionarMarca(MarcaEntidadObjeto);
        }

        protected void CambiarBusquedaAvanzada()
        {
            PanelBusquedaAvanzada.Visible = !PanelBusquedaAvanzada.Visible;
            PanelNuevoRegistro.Visible = false;
        }

        protected void CambiarNuevoRegistro()
        {
            PanelBusquedaAvanzada.Visible = false;
            PanelNuevoRegistro.Visible = !PanelNuevoRegistro.Visible;
            LimpiarNuevoRegistro();
        }

        protected void LimpiarNuevoRegistro()
        {
            DependenciaNuevo.SelectedValue = "0";
            EstatusNuevo.SelectedValue = "0";
            NombreNuevo.Text = "";
            EtiquetaMensaje.Text = "";
            MarcaIdHidden.Value = "0";
        }

        protected string ObtenerCadenaMarcaId()
        {
            StringBuilder CadenaMarcaId = new StringBuilder();
            CheckBox CasillaEliminar;

            CadenaMarcaId.Append(",");

            foreach (GridViewRow Registro in TablaMarca.Rows)
            {
                CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                if (CasillaEliminar.Checked)
                {
                    CadenaMarcaId.Append(TablaMarca.DataKeys[Registro.RowIndex]["MarcaId"].ToString());
                    CadenaMarcaId.Append(",");
                }
            }

            return CadenaMarcaId.ToString();
        }

        protected void SeleccionarDependencia()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            DependenciaEntidad DependenciaEntidadObjeto = new DependenciaEntidad();
            DependenciaProceso DependenciaProcesoObjeto = new DependenciaProceso();

            Resultado = DependenciaProcesoObjeto.SeleccionarDependencia(DependenciaEntidadObjeto);

            DependenciaNuevo.DataValueField = "DependenciaId";
            DependenciaNuevo.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                DependenciaNuevo.DataSource = Resultado.ResultadoDatos;
                DependenciaNuevo.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            DependenciaNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void TablaMarcaEventoComando(GridViewCommandEventArgs e)
        {
            MarcaEntidad MarcaEntidadObjeto = new MarcaEntidad();
            Int16 intFila = 0;
            int intTamañoPagina = 0;
            Int16 MarcaId = 0;
            string strCommand = string.Empty;

            intFila = Int16.Parse(e.CommandArgument.ToString());
            strCommand = e.CommandName.ToString();
            intTamañoPagina = TablaMarca.PageSize;

            if (intFila >= intTamañoPagina)
                intFila = (Int16)(intFila - (intTamañoPagina * TablaMarca.PageIndex));


            switch (strCommand)
            {
                case "Select":
                    MarcaId = Int16.Parse(TablaMarca.DataKeys[intFila]["MarcaId"].ToString());
                    MarcaEntidadObjeto.MarcaId = MarcaId;
                    MarcaIdHidden.Value = MarcaId.ToString();
                    SeleccionarMarcaParaEditar(MarcaEntidadObjeto);
                    break;

                default:
                    // Do nothing
                    break;
            }
        }

        protected void SeleccionarEstatusNuevo()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
            EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

            EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Marcas;

            Resultado = EstatusProcesoObjeto.SeleccionarEstatus(EstatusEntidadObjeto);

            EstatusNuevo.DataValueField = "EstatusId";
            EstatusNuevo.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                EstatusNuevo.DataSource = Resultado.ResultadoDatos;
                EstatusNuevo.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            EstatusNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void SeleccionarTextoError()
        {
            NombreRequerido.ErrorMessage = TextoError.MarcaNombre + "<br />";
            EstatusRequerido.ErrorMessage = TextoError.MarcaEstatus + "<br />";
            DependenciaRequerido.ErrorMessage = TextoError.MarcaDependencia + "<br />";
        }
        
        protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.Marcas);

                    SeleccionarEstatusNuevo();
                    SeleccionarDependenciaNuevo();
                    BusquedaAvanzada();
                    SeleccionarTextoError();

                    EliminarRegistroLink.Attributes.Add("onclick", "return ValidarCasillas()");
                }
            }

        protected void SeleccionarMarca(MarcaEntidad MarcaObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MarcaProceso MarcaProcesoNegocio = new MarcaProceso();

            Resultado = MarcaProcesoNegocio.SeleccionarMarca(MarcaObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaMarca.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaMarca.CssClass = ConstantePrograma.ClaseTabla;

                TablaMarca.DataSource = Resultado.ResultadoDatos;
                TablaMarca.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }
        }
        #endregion
    }
}
