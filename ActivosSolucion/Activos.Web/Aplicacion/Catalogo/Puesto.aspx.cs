using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Aplicacion.Catalogo
{
    public partial class Puesto : System.Web.UI.Page
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
            GuardarPuesto();
        }

        protected void EliminarRegistroLink_Click(object sender, EventArgs e)
        {
            EliminarPuesto();
        }

        protected void ImagenBuscar_Click(object sender, ImageClickEventArgs e)
        {
            BusquedaAvanzada();
        }

        protected void NuevoRegistroLink_Click(object sender, EventArgs e)
        {
            CambiarNuevoRegistro();
        }

        protected void TablaPuesto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TablaPuesto.PageIndex = e.NewPageIndex;
            BusquedaAvanzada();
        }

        protected void TablaPuesto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaPuestoEventoComando(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }
       

        #endregion

        #region "Métodos"

        protected void GuardarPuesto()
        {
            PuestoEntidad PuestoObjetoEntidad = new PuestoEntidad();
            UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

            UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

            PuestoObjetoEntidad.PuestoId = Int16.Parse(PuestoIdHidden.Value);
            PuestoObjetoEntidad.DependenciaId = Int16.Parse(DependenciaNuevo.SelectedValue);
            PuestoObjetoEntidad.EstatusId = Int16.Parse(EstatusNuevo.SelectedValue);
            PuestoObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
            PuestoObjetoEntidad.UsuarioIdModifico = UsuarioSessionEntidad.UsuarioId;
            PuestoObjetoEntidad.Nombre = NombreNuevo.Text.Trim();

            GuardarPuesto(PuestoObjetoEntidad);
        }

        protected void GuardarPuesto(PuestoEntidad PuestoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            PuestoProceso PuestoProcesoNegocio = new PuestoProceso();

            Resultado = PuestoProcesoNegocio.GuardarPuesto(PuestoObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Puesto.PuestoGuardadoCorrectamente)
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

        protected void EliminarPuesto()
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            PuestoEntidad PuestoEntidadObjeto = new PuestoEntidad();

            PuestoEntidadObjeto.CadenaPuestoId = ObtenerCadenaPuestoId();

            EliminarPuesto(PuestoEntidadObjeto);
        }

        protected void EliminarPuesto(PuestoEntidad PuestoObjetoEntidad)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            PuestoProceso PuestoProcesoObjeto = new PuestoProceso();

            ResultadoEntidadObjeto = PuestoProcesoObjeto.EliminarPuesto(PuestoObjetoEntidad);

            if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Puesto.EliminacionExitosa)
            {
                EtiquetaMensaje.Text = "";

                BusquedaAvanzada();
            }
            else
            {
                EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
            }
        }

        protected string ObtenerCadenaPuestoId()
        {
            StringBuilder CadenaPuestoId = new StringBuilder();
            CheckBox CasillaEliminar;

            CadenaPuestoId.Append(",");

            foreach (GridViewRow Registro in TablaPuesto.Rows)
            {
                CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                if (CasillaEliminar.Checked)
                {
                    CadenaPuestoId.Append(TablaPuesto.DataKeys[Registro.RowIndex]["PuestoId"].ToString());
                    CadenaPuestoId.Append(",");
                }
            }

            return CadenaPuestoId.ToString();
        }

        protected void SeleccionarPuestoParaEditar(PuestoEntidad PuestoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            PuestoProceso PuestoProcesoNegocio = new PuestoProceso();

            Resultado = PuestoProcesoNegocio.SeleccionarPuesto(PuestoObjetoEntidad);

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
            PuestoEntidad PuestoEntidadObjeto = new PuestoEntidad();

            PuestoEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
            PuestoEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

            SeleccionarPuesto(PuestoEntidadObjeto);
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
            //SeleccionarDependenciaNuevo();
            EtiquetaMensaje.Text = "";
            PuestoIdHidden.Value = "0";
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

        protected void TablaPuestoEventoComando(GridViewCommandEventArgs e)
        {
            PuestoEntidad PuestoEntidadObjeto = new PuestoEntidad();
            Int16 intFila = 0;
            int intTamañoPagina = 0;
            Int16 PuestoId = 0;
            string strCommand = string.Empty;

            intFila = Int16.Parse(e.CommandArgument.ToString());
            strCommand = e.CommandName.ToString();
            intTamañoPagina = TablaPuesto.PageSize;

            if (intFila >= intTamañoPagina)
                intFila = (Int16)(intFila - (intTamañoPagina * TablaPuesto.PageIndex));


            switch (strCommand)
            {
                case "Select":
                    PuestoId = Int16.Parse(TablaPuesto.DataKeys[intFila]["PuestoId"].ToString());
                    PuestoEntidadObjeto.PuestoId = PuestoId;
                    PuestoIdHidden.Value = PuestoId.ToString();
                    SeleccionarPuestoParaEditar(PuestoEntidadObjeto);
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

            EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Puestos;

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

        protected void SeleccionarPuesto(PuestoEntidad PuestoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            PuestoProceso PuestoProcesoNegocio = new PuestoProceso();

            Resultado = PuestoProcesoNegocio.SeleccionarPuesto(PuestoObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaPuesto.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaPuesto.CssClass = ConstantePrograma.ClaseTabla;

                TablaPuesto.DataSource = Resultado.ResultadoDatos;
                TablaPuesto.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }
        }

        protected void SeleccionarTextoError()
        {
            NombreRequerido.ErrorMessage = TextoError.DireccionNombre + "<br />";
            EstatusRequerido.ErrorMessage = TextoError.DireccionEstatus + "<br />";
            DependenciaRequerido.ErrorMessage = TextoError.DependenciaNuevo + "<br />";
        }

        protected void Inicio()
        {
            if (!Page.IsPostBack)
            {
                //Validamos permisos
                Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.Puestos);

                SeleccionarEstatusNuevo();
                SeleccionarDependencia();
                BusquedaAvanzada();
                SeleccionarTextoError();

                EliminarRegistroLink.Attributes.Add("onclick", "return ValidarCasillas()");
            }
        }

          
        #endregion
    }
}
