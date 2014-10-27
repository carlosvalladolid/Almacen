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
using Activos.Entidad.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Activos;

namespace Activos.Web.Aplicacion.Catalogo
{
    public partial class Familia : System.Web.UI.Page
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
            GuardarFamilia();
        }

        protected void EliminarRegistroLink_Click(object sender, EventArgs e)
        {
            EliminarFamilia();
        }

        protected void ImagenBuscar_Click(object sender, ImageClickEventArgs e)
        {
            BusquedaAvanzada();
        }

        protected void NuevoRegistroLink_Click(object sender, EventArgs e)
        {
            CambiarNuevoRegistro();
        }

        //protected void Familia_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SeleccionarSubFamilia();
        //}

        //protected void FamiliaNuevo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SeleccionarSubFamiliaNuevo();
        //}

        protected void TablaFamilia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TablaFamilia.PageIndex = e.NewPageIndex;
            BusquedaAvanzada();
        }

        protected void TablaFamiliaRowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaFamiliaEventoComando(e);
        }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }
        #endregion

        #region "Métodos"

            protected void EliminarFamilia()
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();

                FamiliaEntidadObjeto.CadenaFamiliaId = ObtenerCadenaFamiliaId();

                EliminarFamilia(FamiliaEntidadObjeto);
            }

            protected void EliminarFamilia(FamiliaEntidad FamiliaEntidadObjeto)
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

                ResultadoEntidadObjeto = FamiliaProcesoObjeto.EliminarFamilia(FamiliaEntidadObjeto);

                if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Familia.EliminacionExitosa)
                {
                    EtiquetaMensaje.Text = "";

                    BusquedaAvanzada();
                }
                else
                {
                    EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
                }
            }

            protected void GuardarFamilia()
            {
                FamiliaEntidad FamiliaObjetoEntidad = new FamiliaEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                FamiliaObjetoEntidad.FamiliaId = Int16.Parse(FamiliaIdHidden.Value);
                FamiliaObjetoEntidad.DependenciaId = Int16.Parse(DependenciaNuevo.SelectedValue);
                FamiliaObjetoEntidad.EstatusId = Int16.Parse(EstatusNuevo.SelectedValue);
                FamiliaObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
                FamiliaObjetoEntidad.UsuarioIdModifico = UsuarioSessionEntidad.UsuarioId;
                FamiliaObjetoEntidad.Nombre = NombreNuevo.Text.Trim();

                GuardarFamilia(FamiliaObjetoEntidad);
            }

            protected void GuardarFamilia(FamiliaEntidad FamiliaObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                FamiliaProceso FamiliaProcesoNegocio = new FamiliaProceso();

                Resultado = FamiliaProcesoNegocio.GuardarFamilia(FamiliaObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Familia.FamiliaGuardadoCorrectamente)
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

            protected void SeleccionarFamiliaParaEditar(FamiliaEntidad FamiliaObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                FamiliaProceso FamiliaProcesoNegocio = new FamiliaProceso();

                Resultado = FamiliaProcesoNegocio.SeleccionarFamilia(FamiliaObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    DependenciaNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["DependenciaId"].ToString();
                    EstatusNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString();
                    NombreNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
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
                FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();

                FamiliaEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
                FamiliaEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

                SeleccionarFamilia(FamiliaEntidadObjeto);
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
                FamiliaIdHidden.Value = "0";
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

            protected string ObtenerCadenaFamiliaId()
            {
                StringBuilder CadenaFamiliaId = new StringBuilder();
                CheckBox CasillaEliminar;

                CadenaFamiliaId.Append(",");

                foreach (GridViewRow Registro in TablaFamilia.Rows)
                {
                    CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                    if (CasillaEliminar.Checked)
                    {
                        CadenaFamiliaId.Append(TablaFamilia.DataKeys[Registro.RowIndex]["FamiliaId"].ToString());
                        CadenaFamiliaId.Append(",");
                    }
                }

                return CadenaFamiliaId.ToString();
            }

            protected void TablaFamiliaEventoComando(GridViewCommandEventArgs e)
            {
                FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 FamiliaId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaFamilia.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaFamilia.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        FamiliaId = Int16.Parse(TablaFamilia.DataKeys[intFila]["FamiliaId"].ToString());
                        FamiliaEntidadObjeto.FamiliaId = FamiliaId;
                        FamiliaIdHidden.Value = FamiliaId.ToString();
                        SeleccionarFamiliaParaEditar(FamiliaEntidadObjeto);
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

                EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Familia;

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

           protected void SeleccionarFamilia(FamiliaEntidad FamiliaObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                FamiliaProceso FamiliaProcesoNegocio = new FamiliaProceso();

                Resultado = FamiliaProcesoNegocio.SeleccionarFamilia(FamiliaObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaFamilia.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaFamilia.CssClass = ConstantePrograma.ClaseTabla;

                    TablaFamilia.DataSource = Resultado.ResultadoDatos;
                    TablaFamilia.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarTextoError()
            {
                NombreRequerido.ErrorMessage = TextoError.FamiliaNombre + "<br />";
                EstatusRequerido.ErrorMessage = TextoError.FamiliaEstatus + "<br />";
                DependenciaRequerido.ErrorMessage = TextoError.FamiliaDependencia + "<br />";
            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.Familias);

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
