using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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

namespace Activos.Web.Aplicacion.Activo
{
    public partial class EtiquetadoActivo : System.Web.UI.Page
    {
        #region "Eventos"

        public StringBuilder CadenaParticularXML = new StringBuilder();
        public StringBuilder CadenaGeneralXML = new StringBuilder();

            protected void BotonCancelar_Click(object sender, EventArgs e)
            {
                LimpiarFormulario();
            }

            protected void BotonGuardar_Click(object sender, EventArgs e)
            {
                GuardarCodigosBarra();
            }

            protected void LinkBuscarDocumento_Click(object sender, EventArgs e)
            {
                BuscarDocumento();
            }

            protected void TablaActivo_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                string BarrasParticular = string.Empty;
                string BarrasGeneral = string.Empty;
                DataRowView drFila;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    drFila = (DataRowView)e.Row.DataItem;
                    BarrasParticular = drFila["CodigoBarrasParticular"].ToString();
                    BarrasGeneral = drFila["CodigoBarrasGeneral"].ToString();

                    TextBox TextBoxBarrasParticular = (TextBox)e.Row.FindControl("CodigoBarrasParticular");
                    TextBox TextBoxBarrasGeneral = (TextBox)e.Row.FindControl("CodigoBarrasGeneral");

                    if (BarrasParticular != "")
                        TextBoxBarrasParticular.Enabled = false;

                    if (BarrasGeneral != "")
                        TextBoxBarrasGeneral.Enabled = false;

                }
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

        #endregion

        #region "Métodos"

            protected void BloquearDocumento()
            {
                ProveedorId.Enabled = false;
                TipoDocumentoId.Enabled = false;
                CompraFolio.Enabled = false;
            }

            protected void DesbloquearDocumento()
            {
                ProveedorId.Enabled = true;
                TipoDocumentoId.Enabled = true;
                CompraFolio.Enabled = true;
            }

            protected void BuscarDocumento()
            {
                CompraEntidad CompraObjetoEntidad = new CompraEntidad();

                CompraObjetoEntidad.ProveedorId = Int16.Parse(ProveedorId.SelectedValue);
                CompraObjetoEntidad.TipoDocumentoId = Int16.Parse(TipoDocumentoId.SelectedValue);
                CompraObjetoEntidad.CompraFolio = CompraFolio.Text.Trim();

                BuscarDocumento(CompraObjetoEntidad);
            }

            protected void BuscarDocumento(CompraEntidad CompraObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                CompraProceso CompraProcesoNegocio = new CompraProceso();

                Resultado = CompraProcesoNegocio.SeleccionarCompra(CompraObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        CompraIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["CompraId"].ToString();
                        SeleccionarActivos();
                        BloquearDocumento();
                        EtiquetaMensaje.Text = "";
                    }
                    else
                    {
                        CompraIdHidden.Value = "0";
                        EtiquetaMensajeError.Text = "No existe ese documento";
                        TablaActivo.DataSource = null;
                        TablaActivo.DataBind();
                        DesbloquearDocumento();
                    }
                }
                else
                {
                    EtiquetaMensajeError.Text = Resultado.DescripcionError;
                }
            }

            protected void GuardarCodigosBarra()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();

                if (CompraIdHidden.Value != "0")
                {
                    Resultado = ObtenerCadenaCodigosXML();

                    switch (Resultado.ErrorId)
                    {
                        case (int)ConstantePrograma.EtiquetadoActivo.NoHayCodigosBarra:
                            EtiquetaMensajeError.Text = TextoError.EtiquetadoNoHayCodigosBarra;
                            break;

                        case (int)ConstantePrograma.EtiquetadoActivo.CadigoBarrasDuplicado:
                            EtiquetaMensajeError.Text = TextoError.EtiquetadoCadigoBarrasDuplicado;
                            break;

                        default:
                            ActivoObjetoEntidad.CadenaGeneralXML = CadenaGeneralXML.ToString();
                            ActivoObjetoEntidad.CadenaParticularXML = CadenaParticularXML.ToString();
                            GuardarCodigosBarra(ActivoObjetoEntidad);
                            break;
                    }
                }
                else
                {
                    EtiquetaMensajeError.Text = TextoError.EtiquetadoNoHayDocumentoSeleccionado;
                }
                
            }

            protected void GuardarCodigosBarra(ActivoEntidad ActivoObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoProceso ActivoProcesoNegocio = new ActivoProceso();

                Resultado = ActivoProcesoNegocio.EtiquetadoActivo(ActivoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.Activo.ActivoEtiquetadoCorrectamente)
                {
                    LimpiarFormulario();
                    EtiquetaMensaje.Text = TextoError.EtiquetadoExitoso;
                }
                else
                {
                    EtiquetaMensajeError.Text = TextoError.ErrorGenerico + ". " + Resultado.DescripcionError;
                }
            }

            protected void LimpiarFormulario()
            {
                ProveedorId.SelectedIndex = 0;
                TipoDocumentoId.SelectedIndex = 0;
                CompraFolio.Text = "";
                CompraIdHidden.Value = "0";
                DesbloquearDocumento();
                EtiquetaMensajeError.Text = "";
                EtiquetaMensaje.Text = "";

                TablaActivo.DataSource = null;
                TablaActivo.DataBind();
            }

            protected ResultadoEntidad ObtenerCadenaCodigosXML()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TextBox CajaTextoBarrasParticular;
                TextBox CajaTextoBarrasGeneral;
                Label EtiquetaParticularDuplicado;
                Label EtiquetaGeneralDuplicado;

                bool CodigoBarrasDuplicado = false;
                bool CodigoBarrasNuevo = false;

                CadenaParticularXML = new StringBuilder();
                CadenaGeneralXML = new StringBuilder();

                CadenaParticularXML.Append("<row>");
                CadenaGeneralXML.Append("<row>");

                foreach (GridViewRow Registro in TablaActivo.Rows)
                {
                    CajaTextoBarrasParticular = (TextBox)Registro.FindControl("CodigoBarrasParticular");
                    CajaTextoBarrasGeneral = (TextBox)Registro.FindControl("CodigoBarrasGeneral");
                    EtiquetaParticularDuplicado = (Label)Registro.FindControl("BarrasParticularDuplicado");
                    EtiquetaGeneralDuplicado = (Label)Registro.FindControl("BarrasGeneralDuplicado");

                    if (CajaTextoBarrasParticular.Enabled == true & CajaTextoBarrasParticular.Text.Trim() != "")
                    {
                        CodigoBarrasNuevo = true;

                        if (ActivoDuplicadoPorCodigoParticular(CajaTextoBarrasParticular.Text.Trim()) == false
                            & CodigoParticularDuplicado(CajaTextoBarrasParticular.Text.Trim(), Registro.RowIndex) == false)
                        {
                            CadenaParticularXML.Append("<Activo ActivoId='");
                            CadenaParticularXML.Append(TablaActivo.DataKeys[Registro.RowIndex]["ActivoId"].ToString());
                            CadenaParticularXML.Append("' CodigoBarrasParticular='");
                            CadenaParticularXML.Append(CajaTextoBarrasParticular.Text.Trim());
                            CadenaParticularXML.Append("'/>");

                            EtiquetaParticularDuplicado.Text = "";
                        }
                        else
                        {
                            CodigoBarrasDuplicado = true;
                            EtiquetaParticularDuplicado.Text = "*";
                        }
                       
                    }

                    if (CajaTextoBarrasGeneral.Enabled == true & CajaTextoBarrasGeneral.Text.Trim() != "")
                    {
                        CodigoBarrasNuevo = true;

                        if (ActivoDuplicadoPorCodigoGeneral(CajaTextoBarrasGeneral.Text.Trim()) == false
                            & CodigoGeneralDuplicado(CajaTextoBarrasGeneral.Text.Trim(), Registro.RowIndex) == false)
                        {
                            CadenaGeneralXML.Append("<Activo ActivoId='");
                            CadenaGeneralXML.Append(TablaActivo.DataKeys[Registro.RowIndex]["ActivoId"].ToString());
                            CadenaGeneralXML.Append("' CodigoBarrasGeneral='");
                            CadenaGeneralXML.Append(CajaTextoBarrasGeneral.Text.Trim());
                            CadenaGeneralXML.Append("'/>");

                            EtiquetaGeneralDuplicado.Text = "";
                        }
                        else
                        {
                            CodigoBarrasDuplicado = true;
                            EtiquetaGeneralDuplicado.Text = "*";
                        }
                        
                    }
                }

                CadenaParticularXML.Append("</row>");
                CadenaGeneralXML.Append("</row>");
                // falta devolver los errores si se repitio un codigo o no ingresaron ningun codigo
                if (CodigoBarrasNuevo == false)
                    Resultado.ErrorId = (int)ConstantePrograma.EtiquetadoActivo.NoHayCodigosBarra;

                if (CodigoBarrasDuplicado == true)
                    Resultado.ErrorId = (int)ConstantePrograma.EtiquetadoActivo.CadigoBarrasDuplicado;

                return Resultado;
            }

            protected bool CodigoParticularDuplicado(string CodigoBarrasParticular, int IndiceFila)
            {
                bool CodigoParticularDuplicado = false;
                TextBox CajaTextoBarrasParticular;

                foreach (GridViewRow Registro in TablaActivo.Rows)
                {
                    CajaTextoBarrasParticular = (TextBox)Registro.FindControl("CodigoBarrasParticular");

                    if (CajaTextoBarrasParticular.Text.Trim() == CodigoBarrasParticular & Registro.RowIndex != IndiceFila)
                    {
                        CodigoParticularDuplicado = true;
                        break;
                    }   

                }

                return CodigoParticularDuplicado;
            }

            protected bool CodigoGeneralDuplicado(string CodigoBarrasGeneral, int IndiceFila)
            {
                bool CodigoGeneralDuplicado = false;
                TextBox CajaTextoBarrasGeneral;

                foreach (GridViewRow Registro in TablaActivo.Rows)
                {
                    CajaTextoBarrasGeneral = (TextBox)Registro.FindControl("CodigoBarrasGeneral");

                    if (CajaTextoBarrasGeneral.Text.Trim() == CodigoBarrasGeneral & Registro.RowIndex != IndiceFila)
                    {
                        CodigoGeneralDuplicado = true;
                        break;
                    }

                }

                return CodigoGeneralDuplicado;
            }

            protected bool ActivoDuplicadoPorCodigoParticular(string CodigoBarrasParticular)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoProceso ActivoProcesoNegocio = new ActivoProceso();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();

                ActivoObjetoEntidad.CodigoBarrasParticular = CodigoBarrasParticular;
                //ActivoObjetoEntidad.GrupoEstatus = "," + (int)ConstantePrograma.EstatusActivos.Asignado + "," + (int)ConstantePrograma.EstatusActivos.SinAsignar + ",";
                ActivoObjetoEntidad.TipoDeMovimiento = (int)ConstantePrograma.TipoMovimiento.Baja;

                Resultado = ActivoProcesoNegocio.SeleccionarActivoPorCompra(ActivoObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    return false;
                else
                    return true;
            }

            protected bool ActivoDuplicadoPorCodigoGeneral(string CodigoBarrasGeneral)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoProceso ActivoProcesoNegocio = new ActivoProceso();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();

                ActivoObjetoEntidad.CodigoBarrasGeneral = CodigoBarrasGeneral;
                //ActivoObjetoEntidad.GrupoEstatus = "," + (int)ConstantePrograma.EstatusActivos.Asignado + "," + (int)ConstantePrograma.EstatusActivos.SinAsignar + ",";
                ActivoObjetoEntidad.TipoDeMovimiento = (int)ConstantePrograma.TipoMovimiento.Baja;

                Resultado = ActivoProcesoNegocio.SeleccionarActivoPorCompra(ActivoObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    return false;
                else
                    return true;
            }

            protected void SeleccionarActivos()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoProceso ActivoProcesoNegocio = new ActivoProceso();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();

                if (CompraIdHidden.Value != "0")
                {
                    ActivoObjetoEntidad.CompraId = Int16.Parse(CompraIdHidden.Value);
                    //ActivoObjetoEntidad.GrupoEstatus = "," + (int)ConstantePrograma.EstatusActivos.Asignado + "," + (int)ConstantePrograma.EstatusActivos.SinAsignar + ",";
                    ActivoObjetoEntidad.TipoDeMovimiento = (int)ConstantePrograma.TipoMovimiento.Baja;

                    Resultado = ActivoProcesoNegocio.SeleccionarActivoPorCompra(ActivoObjetoEntidad);

                    if (Resultado.ErrorId == 0)
                    {
                        if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                            TablaActivo.CssClass = ConstantePrograma.ClaseTablaVacia;
                        else
                            TablaActivo.CssClass = ConstantePrograma.ClaseTabla;

                        TablaActivo.DataSource = Resultado.ResultadoDatos;
                        TablaActivo.DataBind();
                    }
                    else
                    {
                        EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
                    }
                }

            }

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {
                    //Validamos permisos
                    Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.EtiquetadoActivos);

                    SeleccionarProveedor();
                    SeleccionarTipoDocumento();
                    SeleccionarTextoError();

                    TablaActivo.DataSource = null;
                    TablaActivo.DataBind();
                }
            }

            protected void SeleccionarProveedor()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ProveedorEntidad ProveedorEntidadObjeto = new ProveedorEntidad();
                ProveedorProceso ProveedorProcesoObjeto = new ProveedorProceso();

                Resultado = ProveedorProcesoObjeto.SeleccionarProveedor(ProveedorEntidadObjeto);

                ProveedorId.DataValueField = "ProveedorId";
                ProveedorId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    ProveedorId.DataSource = Resultado.ResultadoDatos;
                    ProveedorId.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                ProveedorId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarTextoError()
            {
                CompraFolioRequerido.ErrorMessage = TextoError.EtiquetadoCompraFolio + "<br />";
                ProveedorRequerido.ErrorMessage = TextoError.EtiquetadoProveedor + "<br />";
                TipoDocumentoRequerido.ErrorMessage = TextoError.EtiquetadoTipoDocumento + "<br />";

                GuardarCompraFolioRequerido.ErrorMessage = TextoError.EtiquetadoCompraFolio + "<br />";
                BuscarProveedorRequerido.ErrorMessage = TextoError.EtiquetadoProveedor + "<br />";
                BuscarTipoDocumentoRequerido.ErrorMessage = TextoError.EtiquetadoTipoDocumento + "<br />";
            }

            protected void SeleccionarTipoDocumento()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TipoDocumentoEntidad TipoDocumentoEntidadObjeto = new TipoDocumentoEntidad();
                TipoDocumentoProceso TipoDocumentoProcesoObjeto = new TipoDocumentoProceso();

                Resultado = TipoDocumentoProcesoObjeto.SeleccionarTipoDocumento(TipoDocumentoEntidadObjeto);

                TipoDocumentoId.DataValueField = "TipoDocumentoId";
                TipoDocumentoId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    TipoDocumentoId.DataSource = Resultado.ResultadoDatos;
                    TipoDocumentoId.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                TipoDocumentoId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

        #endregion
    }
}
