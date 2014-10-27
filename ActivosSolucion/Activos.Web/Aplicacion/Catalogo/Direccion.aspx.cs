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
    public partial class Direccion : System.Web.UI.Page
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
            GuardarDireccion();
        }

        protected void EliminarRegistroLink_Click(object sender, EventArgs e)
        {
            EliminarDireccion();
        }

        protected void ImagenBuscar_Click(object sender, ImageClickEventArgs e)
        {
            BusquedaAvanzada();
        }

        protected void NuevoRegistroLink_Click(object sender, EventArgs e)
        {
            CambiarNuevoRegistro();
        }

        protected void TablaDireccion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TablaDireccion.PageIndex = e.NewPageIndex;
            BusquedaAvanzada();
        }

        protected void TablaDireccion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaDireccionEventoComando(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }
        #endregion


        #region "Métodos"

        protected void EliminarDireccion()
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            DireccionEntidad DireccionEntidadObjeto = new DireccionEntidad();

            DireccionEntidadObjeto.CadenaDireccionId = ObtenerCadenaDireccionId();

            EliminarDireccion(DireccionEntidadObjeto);
        }

        protected void EliminarDireccion(DireccionEntidad DireccionEntidadObjeto)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            DireccionProceso DireccionProcesoObjeto = new DireccionProceso();

            ResultadoEntidadObjeto = DireccionProcesoObjeto.EliminarDireccion(DireccionEntidadObjeto);

            if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Direccion.EliminacionExitosa)
            {
                EtiquetaMensaje.Text = "";

                BusquedaAvanzada();
            }
            else
            {
                EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
            }
        }

        protected void GuardarDireccion()
        {
           DireccionEntidad DireccionObjetoEntidad = new DireccionEntidad();
           UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

            UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

            DireccionObjetoEntidad.DireccionId = Int16.Parse(DireccionIdHidden.Value);
            DireccionObjetoEntidad.DependenciaId = Int16.Parse(DependenciaNuevo.SelectedValue);
            DireccionObjetoEntidad.EstatusId = Int16.Parse(EstatusNuevo.SelectedValue);
            DireccionObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
            DireccionObjetoEntidad.UsuarioIdModifico = UsuarioSessionEntidad.UsuarioId;
            DireccionObjetoEntidad.Nombre = NombreNuevo.Text.Trim();
            
            GuardarDireccion(DireccionObjetoEntidad);
        }

        protected void GuardarDireccion(DireccionEntidad DireccionObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            DireccionProceso DireccionProcesoNegocio = new DireccionProceso();

            Resultado = DireccionProcesoNegocio.GuardarDireccion(DireccionObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Direccion.DireccionGuardadoCorrectamente)
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

        protected void SeleccionarDireccionParaEditar(DireccionEntidad DireccionObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DireccionProceso DireccionProcesoNegocio = new DireccionProceso();

                Resultado = DireccionProcesoNegocio.SeleccionarDireccion(DireccionObjetoEntidad);

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
            DireccionEntidad DireccionEntidadObjeto = new DireccionEntidad();

           DireccionEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
           DireccionEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

            SeleccionarDireccion(DireccionEntidadObjeto);
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

        protected string ObtenerCadenaDireccionId()
        {
            StringBuilder CadenaDireccionId = new StringBuilder();
            CheckBox CasillaEliminar;

            CadenaDireccionId.Append(",");

            foreach (GridViewRow Registro in TablaDireccion.Rows)
            {
                CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                if (CasillaEliminar.Checked)
                {
                    CadenaDireccionId.Append(TablaDireccion.DataKeys[Registro.RowIndex]["DireccionId"].ToString());
                    CadenaDireccionId.Append(",");
                }
            }

            return CadenaDireccionId.ToString();
        }

        protected void Inicio()
        {
            if (!Page.IsPostBack)
            {
                //Validamos permisos
                Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.Direccion);

                SeleccionarEstatusNuevo();
                SeleccionarDependencia();
                BusquedaAvanzada();
                SeleccionarTextoError();

                EliminarRegistroLink.Attributes.Add("onclick", "return ValidarCasillas()");

            }
        }

        protected void LimpiarNuevoRegistro()
            {
                DependenciaNuevo.SelectedValue = "0";
                EstatusNuevo.SelectedValue = "0";
                NombreNuevo.Text = "";            
                //SeleccionarDependenciaNuevo();
                EtiquetaMensaje.Text = "";
                DireccionIdHidden.Value = "0";
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

        protected void TablaDireccionEventoComando(GridViewCommandEventArgs e)
            {
                DireccionEntidad DireccionEntidadObjeto = new DireccionEntidad();
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 DireccionId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaDireccion.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaDireccion.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        DireccionId = Int16.Parse(TablaDireccion.DataKeys[intFila]["DireccionId"].ToString());
                        DireccionEntidadObjeto.DireccionId = DireccionId;
                       DireccionIdHidden.Value = DireccionId.ToString();
                        SeleccionarDireccionParaEditar(DireccionEntidadObjeto);
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

            EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Direccion;

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

       protected void SeleccionarDireccion(DireccionEntidad DireccionObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                DireccionProceso DireccionProcesoNegocio = new DireccionProceso();

                Resultado = DireccionProcesoNegocio.SeleccionarDireccion(DireccionObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaDireccion.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaDireccion.CssClass = ConstantePrograma.ClaseTabla;

                    TablaDireccion.DataSource = Resultado.ResultadoDatos;
                    TablaDireccion.DataBind();
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
        #endregion
       
    }

}
