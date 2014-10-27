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
    public partial class SubFamilia : System.Web.UI.Page
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
            GuardarSubFamilia();
        }

        protected void EliminarRegistroLink_Click(object sender, EventArgs e)
        {
            EliminarSubFamilia();
        }

        protected void ImagenBuscar_Click(object sender, ImageClickEventArgs e)
        {
            BusquedaAvanzada();
        }

        protected void NuevoRegistroLink_Click(object sender, EventArgs e)
        {
            CambiarNuevoRegistro();
        }

       protected void SubFamiliaNuevo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarFamiliaNuevo();
        }

        protected void TablaSubFamilia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TablaSubFamilia.PageIndex = e.NewPageIndex;
            BusquedaAvanzada();
        }

        protected void TablaSubFamiliaRowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaSubFamiliaEventoComando(e);
        }

        protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

        #endregion

        #region "Métodos"

            protected void EliminarSubFamilia()
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();

                SubFamiliaEntidadObjeto.CadenaSubFamiliaId = ObtenerCadenaSubFamiliaId();

                EliminarSubFamilia(SubFamiliaEntidadObjeto);
            }

            protected void EliminarSubFamilia(SubFamiliaEntidad SubFamiliaEntidadObjeto)
            {
                ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
                SubFamiliaProceso SubFamiliaProcesoObjeto = new SubFamiliaProceso();

                ResultadoEntidadObjeto = SubFamiliaProcesoObjeto.EliminarSubFamilia(SubFamiliaEntidadObjeto);

                if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.SubFamilia.EliminacionExitosa)
                {
                    EtiquetaMensaje.Text = "";

                    BusquedaAvanzada();
                }
                else
                {
                    EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
                }
            }

            protected void GuardarSubFamilia()
            {
                SubFamiliaEntidad SubFamiliaObjetoEntidad = new SubFamiliaEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                SubFamiliaObjetoEntidad.SubFamiliaId = Int16.Parse(SubFamiliaIdHidden.Value);
                SubFamiliaObjetoEntidad.FamiliaId = Int16.Parse(FamiliaNuevo.SelectedValue);
                SubFamiliaObjetoEntidad.EstatusId = Int16.Parse(EstatusNuevo.SelectedValue);
                SubFamiliaObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
                SubFamiliaObjetoEntidad.UsuarioIdModifico = UsuarioSessionEntidad.UsuarioId;
                SubFamiliaObjetoEntidad.Nombre = NombreNuevo.Text.Trim();

                GuardarSubFamilia(SubFamiliaObjetoEntidad);
            }

            protected void GuardarSubFamilia(SubFamiliaEntidad SubFamiliaObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                SubFamiliaProceso SubFamiliaProcesoNegocio = new SubFamiliaProceso();

                Resultado = SubFamiliaProcesoNegocio.GuardarSubFamilia(SubFamiliaObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.SubFamilia.SubFamiliaGuardadoCorrectamente)
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

            protected void SeleccionarSubFamiliaParaEditar(SubFamiliaEntidad SubFamiliaObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                SubFamiliaProceso SubFamiliaProcesoNegocio = new SubFamiliaProceso();

                Resultado = SubFamiliaProcesoNegocio.SeleccionarSubFamilia(SubFamiliaObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    FamiliaNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString();
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

            protected void BusquedaAvanzada()
            {
                SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();

                SubFamiliaEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
                SubFamiliaEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

                SeleccionarSubFamilia(SubFamiliaEntidadObjeto);
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

                FamiliaNuevo.SelectedValue = "0";
                EstatusNuevo.SelectedValue = "0";
                NombreNuevo.Text = "";
                EtiquetaMensaje.Text = "";
                SubFamiliaIdHidden.Value = "0";
            }

            protected void TablaSubFamiliaEventoComando(GridViewCommandEventArgs e)
            {
                SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 SubFamiliaId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaSubFamilia.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaSubFamilia.PageIndex));


                switch (strCommand)
                {
                    case "Select":
                        SubFamiliaId = Int16.Parse(TablaSubFamilia.DataKeys[intFila]["SubFamiliaId"].ToString());
                        SubFamiliaEntidadObjeto.SubFamiliaId = SubFamiliaId;
                        SubFamiliaIdHidden.Value = SubFamiliaId.ToString();
                        SeleccionarSubFamiliaParaEditar(SubFamiliaEntidadObjeto);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

            protected string ObtenerCadenaSubFamiliaId()
            {
                StringBuilder CadenaSubFamiliaId = new StringBuilder();
                CheckBox CasillaEliminar;

                CadenaSubFamiliaId.Append(",");

                foreach (GridViewRow Registro in TablaSubFamilia.Rows)
                {
                    CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                    if (CasillaEliminar.Checked)
                    {
                        CadenaSubFamiliaId.Append(TablaSubFamilia.DataKeys[Registro.RowIndex]["SubFamiliaId"].ToString());
                        CadenaSubFamiliaId.Append(",");
                    }
                }

                return CadenaSubFamiliaId.ToString();
            }

            protected void SeleccionarEstatusNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
                EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

                EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.SubFamilia;

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

            protected void SeleccionarFamiliaNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
                FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

                //FamiliaEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Familia;

                Resultado = FamiliaProcesoObjeto.SeleccionarFamilia(FamiliaEntidadObjeto);

                FamiliaNuevo.DataValueField = "FamiliaId";
                FamiliaNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    FamiliaNuevo.DataSource = Resultado.ResultadoDatos;
                    FamiliaNuevo.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                FamiliaNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarSubFamilia(SubFamiliaEntidad SubFamiliaObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                SubFamiliaProceso SubFamiliaProcesoNegocio = new SubFamiliaProceso();

                Resultado = SubFamiliaProcesoNegocio.SeleccionarSubFamilia(SubFamiliaObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaSubFamilia.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaSubFamilia.CssClass = ConstantePrograma.ClaseTabla;

                    TablaSubFamilia.DataSource = Resultado.ResultadoDatos;
                    TablaSubFamilia.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void SeleccionarTextoError()
            {
                NombreRequerido.ErrorMessage = TextoError.SubFamiliaNombre + "<br />";
                EstatusRequerido.ErrorMessage = TextoError.SubFamiliaEstatus + "<br />";
                FamiliaRequerido.ErrorMessage = TextoError.SubFamiliaFamilia + "<br />";
            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.Subfamilias);

                    SeleccionarEstatusNuevo();
                    SeleccionarFamiliaNuevo();        
                    BusquedaAvanzada();
                    SeleccionarTextoError();

                    EliminarRegistroLink.Attributes.Add("onclick", "return ValidarCasillas()");
                }
            }

                   
        #endregion
    }
}
