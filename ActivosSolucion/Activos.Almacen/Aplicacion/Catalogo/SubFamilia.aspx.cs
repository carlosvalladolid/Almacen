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

using Activos.Comun.Cadenas;
using Activos.Comun.Constante;
using Activos.Entidad.Almacen;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Almacen;
using Activos.ProcesoNegocio.Seguridad;

namespace Almacen.Web.Aplicacion.Catalogo
{
    public partial class SubFamilia : System.Web.UI.Page
    {
         #region "Eventos"

            protected void BotonBusqueda_Click(object sender, EventArgs e)
            {
                TextoBusquedaRapida.Text = "";
                BusquedaAvanzada();
            }

            protected void BotonBusquedaRapida_Click(object sender, ImageClickEventArgs e)
            {
                NombreBusqueda.Text = "";
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

            protected void BotonGuardar_Click(object sender, EventArgs e)
            {
                GuardarSubFamilia();
               
            }

            protected void BusquedaAvanzadaLink_Click(Object sender, System.EventArgs e)
            {
                CambiarBusquedaAvanzada();
            }

            protected void EliminarRegistroLink_Click(Object sender, System.EventArgs e)
            {
                EliminarSubFamilia();
            }

            protected void NuevoRegistro_Click(Object sender, System.EventArgs e)
            {
                CambiarNuevoRegistro();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

            protected void TablaSubFamilia_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TablaSubFamilia.PageIndex = e.NewPageIndex;
                BusquedaAvanzada();
            }

            protected void TablaSubFamilia_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaSubFamiliaEventoComando(e);
            }

            protected void TablaSubFamiliaPuesto_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaSubFamiliaPuestoEventoComando(e);
            }
           
            #endregion

            #region "Métodos"

            protected void BusquedaAvanzada()
            {
                SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();

                SubFamiliaEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
                SubFamiliaEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

                SeleccionarSubFamilia(SubFamiliaEntidadObjeto);
            }

            private void CambiarBusquedaAvanzada()
            {
                PanelBusquedaAvanzada.Visible = !PanelBusquedaAvanzada.Visible;
                PanelNuevoRegistro.Visible = false;
            }

            protected void CambiarEditarRegistro()
            {
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = true;
            }

            private void CambiarNuevoRegistro()
            {
                PanelBusquedaAvanzada.Visible = false;
                PanelNuevoRegistro.Visible = !PanelNuevoRegistro.Visible;
                
                LimpiarNuevoRegistro();
            }

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
                    // ToDo: Se muestra vacío el mensaje
                    MostrarMensaje(TextoInfo.MensajeBorradoGenerico, ConstantePrograma.TipoMensajeAlerta);
                    BusquedaAvanzada();
                }
                else
                    MostrarMensaje(ResultadoEntidadObjeto.DescripcionError, ConstantePrograma.TipoErrorAlerta);
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
                SubFamiliaObjetoEntidad.CadenaXMLPuestoId = ObtenerCadenaPuestoXML();
                SubFamiliaObjetoEntidad.Nombre = NombreNuevo.Text.Trim();

                GuardarSubFamilia(SubFamiliaObjetoEntidad);
                //GuardarSubFamiliaPuesto();
            }

            protected void GuardarSubFamilia(SubFamiliaEntidad SubFamiliaObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                SubFamiliaProceso SubFamiliaProcesoNegocio = new SubFamiliaProceso();

                Resultado = SubFamiliaProcesoNegocio.GuardarSubFamilia(SubFamiliaObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    LimpiarNuevoRegistro();
                    SubFamiliaPuestoIdHidden.Value = Resultado.NuevoRegistroId.ToString();
                    AgregarSubFamiliaPuesto();
                    PanelNuevoRegistro.Visible = false;
                    PanelBusquedaAvanzada.Visible = false;
                    BusquedaAvanzada();
                }
                else
                    MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);
            }

            protected void AgregarSubFamiliaPuesto()
            {
                SubFamiliaPuestoEntidad SubFamiliaPuestoObjetoEntidad = new SubFamiliaPuestoEntidad();            
               
                if (SubFamiliaPuestoIdHidden.Value != "0")
                {
                    SubFamiliaPuestoObjetoEntidad.CadenaPuestoXML = ObtenerCadenaPuestoXML();
                   
                    if (SubFamiliaPuestoObjetoEntidad.CadenaPuestoXML != "<row></row>")
                    {                        
                        SubFamiliaPuestoObjetoEntidad.SubFamiliaId = Int16.Parse(SubFamiliaPuestoIdHidden.Value);                       

                        AgregarSubFamiliaPuesto(SubFamiliaPuestoObjetoEntidad);
                    }
                    else
                    {
                      //LimpiarFormulario();
                      
                    }
                }
            }
          
            protected void AgregarSubFamiliaPuesto(SubFamiliaPuestoEntidad SubFamiliaPuestoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
               SubFamiliaPuestoProceso SubFamiliaPuestoProcesoNegocio = new SubFamiliaPuestoProceso();

               Resultado = SubFamiliaPuestoProcesoNegocio.GuardarSubFamiliaPuesto(SubFamiliaPuestoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.SubFamilia.SubFamiliaGuardadoCorrectamente)
                {
                    //LimpiarFormulario();
                    //EtiquetaMensajeExito.Text = TextoError.SubFamiliaTieneRegistrosRelacionados;                  
                }
                else
                {
                    //EtiquetaMensaje.Text = TextoError.ErrorGenerico + ". " + Resultado.DescripcionError;
                }
            }

            protected string ObtenerCadenaPuestoXML()
            {
                StringBuilder CadenaPuestoXML = new StringBuilder();
                CheckBox ValorVerdadero = new CheckBox();

                CadenaPuestoXML.Append("<root>");
                //CadenaPuestoXML.Append("<row>");

                foreach (GridViewRow Registro in TablaSubFamiliaPuesto.Rows)
                {
                    ValorVerdadero = (CheckBox)Registro.FindControl("AgregarPuesto");

                    if (ValorVerdadero.Checked)
                    {
                        CadenaPuestoXML.Append("<puesto puestoId=\"");
                        CadenaPuestoXML.Append(TablaSubFamiliaPuesto.DataKeys[Registro.RowIndex]["PuestoId"].ToString());
                        CadenaPuestoXML.Append("\"/>");
                    }
                 }
               //CadenaPuestoXML.Append("</row>");
               CadenaPuestoXML.Append("</root>");
                return CadenaPuestoXML.ToString();          

             }

            private void Inicio()
            {
                Master.NuevoRegistroMaster.Click += new EventHandler(NuevoRegistro_Click);
                Master.BusquedaAvanzadaMaster.Click += new EventHandler(BusquedaAvanzadaLink_Click);
                Master.EliminarRegistroMaster.Click += new EventHandler(EliminarRegistroLink_Click);

                if (!Page.IsPostBack)
                {
                    SeleccionarFamiliaNuevo();
                    SeleccionarEstatusNuevo();
                    SeleccionarSubFamiliaPuesto();
                    BusquedaAvanzada();
                }
            }

            private void LimpiarNuevoRegistro()
            {
                FamiliaNuevo.SelectedIndex = 0;
                EstatusNuevo.SelectedIndex = 0;
                NombreNuevo.Text = "";
                SubFamiliaIdHidden.Value = "0";
                SeleccionarSubFamiliaPuesto();
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
                    MostrarMensaje(TextoError.ErrorGenerico, ConstantePrograma.TipoErrorAlerta);

                EstatusNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarFamiliaNuevo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
                FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

                Resultado = FamiliaProcesoObjeto.SeleccionarFamilia(FamiliaEntidadObjeto);

                FamiliaNuevo.DataValueField = "FamiliaId";
                FamiliaNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    FamiliaNuevo.DataSource = Resultado.ResultadoDatos;
                    FamiliaNuevo.DataBind();
                }
                else
                    MostrarMensaje(TextoError.ErrorGenerico, ConstantePrograma.TipoErrorAlerta);

                FamiliaNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarSubFamilia(SubFamiliaEntidad SubFamiliaObjetoEntidad)
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
                    MostrarMensaje(TextoError.ErrorGenerico, ConstantePrograma.TipoErrorAlerta);
            }

            protected void SeleccionarSubFamiliaParaEditar(SubFamiliaEntidad SubFamiliaObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                SubFamiliaProceso SubFamiliaProcesoNegocio = new SubFamiliaProceso();

                Resultado = SubFamiliaProcesoNegocio.SeleccionarSubFamilia(SubFamiliaObjetoEntidad);

                if (Resultado.ErrorId != 0)
                {
                    MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                    return;
                }

                try
                {
                    FamiliaNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString();
                    EstatusNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString();
                    NombreNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();                     
                    CambiarEditarRegistro();
                }
                catch
                {
                    MostrarMensaje(TextoError.ErrorGenerico, ConstantePrograma.TipoErrorAlerta);
                }
            }

            protected void SeleccionarSubFamiliaPuestoparaEditar(SubFamiliaPuestoEntidad SubFamiliaPuestoEntidadObjeto)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                CheckBox chkSeleccionado;
               
                SubFamiliaPuestoProceso SubFamiliaPuestoProcesoNegocio = new SubFamiliaPuestoProceso();               
            
                Resultado = SubFamiliaPuestoProcesoNegocio.SeleccionarSubFamiliaPuestoEditar(SubFamiliaPuestoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    {   
                         MostrarMensaje(Resultado.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                       
                    }
                    else
                    {
                        foreach (DataRow Puesto in Resultado.ResultadoDatos.Tables[0].Rows)
                        {
                            foreach (GridViewRow Registro in TablaSubFamiliaPuesto.Rows)
                            {
                                if (TablaSubFamiliaPuesto.DataKeys[Registro.RowIndex]["PuestoId"].ToString() == Puesto["PuestoId"].ToString())
                                {
                                    chkSeleccionado = (CheckBox)Registro.FindControl("AgregarPuesto");
                                    chkSeleccionado.Checked = true;
                                    break;
                                }
                            }
                        
                        
                        
                        }

                    }
                  
                   // EtiquetaMensaje.Text = TextoError.ErrorGenerico;
               
                }
              
            }

            protected void SeleccionarSubFamiliaPuesto()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                SubFamiliaPuestoEntidad SubFamiliaPuestoObjetoEntidad = new SubFamiliaPuestoEntidad();
                SubFamiliaPuestoProceso SubFamiliaPuestoProcesoNegocio = new SubFamiliaPuestoProceso();

                Resultado = SubFamiliaPuestoProcesoNegocio.SeleccionarSubFamiliaPuesto(SubFamiliaPuestoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaSubFamiliaPuesto.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaSubFamiliaPuesto.CssClass = ConstantePrograma.ClaseTabla;

                    TablaSubFamiliaPuesto.DataSource = Resultado.ResultadoDatos;
                    TablaSubFamiliaPuesto.DataBind();
                }
                else
                {
                    //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void TablaSubFamiliaEventoComando(GridViewCommandEventArgs e)
            {
                SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();
                SubFamiliaPuestoEntidad SubFamiliaPuestoEntidadObjeto = new SubFamiliaPuestoEntidad();
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
                        SubFamiliaPuestoEntidadObjeto.SubFamiliaId = SubFamiliaId;
                        SeleccionarSubFamiliaPuestoparaEditar(SubFamiliaPuestoEntidadObjeto);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

            protected void TablaSubFamiliaPuestoEventoComando(GridViewCommandEventArgs e)
            {
                SubFamiliaPuestoEntidad SubFamiliaPuestoEntidadObjeto = new SubFamiliaPuestoEntidad();
                Int16 intFila = 0;
                int intTamañoPagina = 0;
                Int16 PuestoId = 0;
                string strCommand = string.Empty;

                intFila = Int16.Parse(e.CommandArgument.ToString());
                strCommand = e.CommandName.ToString();
                intTamañoPagina = TablaSubFamilia.PageSize;

                if (intFila >= intTamañoPagina)
                    intFila = (Int16)(intFila - (intTamañoPagina * TablaSubFamilia.PageIndex));

                switch (strCommand)
                {
                    case "Select":
                        PuestoId = Int16.Parse(TablaSubFamilia.DataKeys[intFila]["PuestoId"].ToString());
                        SubFamiliaPuestoEntidadObjeto.PuestoId = PuestoId;
                        //SubFamiliaIdHidden.Value = PuestoId.ToString();
                        //SeleccionarSubFamiliaParaEditar(SubFamiliaEntidadObjeto);
                        break;

                    default:
                        // Do nothing
                        break;
                }
            }

        #endregion
    }
}
