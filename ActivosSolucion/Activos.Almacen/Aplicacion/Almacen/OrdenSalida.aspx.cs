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




using Activos.Comun.Constante;
using Activos.Comun.Fecha;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Almacen;

namespace Almacen.Web.Aplicacion.Almacen
{
    public partial class OrdenSalida : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonGuardar_Click(object sender, ImageClickEventArgs e)
            {

            }


            protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarSubfamilia();
            }

            protected void ImagenBuscarPreOrden_Click(object sender, ImageClickEventArgs e)
            {
                ValidarRequisicion(RequisicionBox.Text.Trim());
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }
        #endregion

        #region "Métodos"
            private void Inicio()
            {
                if (Page.IsPostBack)
                    return;

                SeleccionarFamilia();
                SeleccionarSubfamilia();
                SeleccionarMarca();

                TablaOrden.DataSource = null;
                TablaOrden.DataBind();
            }

            private void LimpiarFormulario()
            {
                RequisicionBox.Text = "";
                SolicitanteBox.Text = "";
                DependenciaBox.Text = "";
                DireccionBox.Text = "";
                PuestoBox.Text = "";
                JefeBox.Text = "";
                //***********************
                ClaveRequisicionBox.Text = "";
                FamiliaCombo.SelectedIndex = 0;
                SubFamiliaCombo.SelectedIndex = 0;
                MarcaCombo.SelectedIndex = 0;
                DescripcionBox.Text = "";
                CantidadBox.Text = "";

            }

            protected void SeleccionarFamilia()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
                FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

                //FamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusFamilia.Activo;

                Resultado = FamiliaProcesoObjeto.SeleccionarFamilia(FamiliaEntidadObjeto);

                FamiliaCombo.DataValueField = "FamiliaId";
                FamiliaCombo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    FamiliaCombo.DataSource = Resultado.ResultadoDatos;
                    FamiliaCombo.DataBind();
                }
                else
                {
                 //   EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                FamiliaCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarMarca()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MarcaEntidad MarcaEntidadObjeto = new MarcaEntidad();
                MarcaProceso MarcaProcesoObjeto = new MarcaProceso();

                //MarcaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusMarca.Activo;

                Resultado = MarcaProcesoObjeto.SeleccionarMarca(MarcaEntidadObjeto);

                MarcaCombo.DataValueField = "MarcaId";
                MarcaCombo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    MarcaCombo.DataSource = Resultado.ResultadoDatos;
                    MarcaCombo.DataBind();
                }
                else
                {
                //    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                MarcaCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            protected void SeleccionarSubfamilia()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();
                SubFamiliaProceso SubFamiliaProcesoObjeto = new SubFamiliaProceso();

                //SubFamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusSubFamilia.Activo;
                SubFamiliaEntidadObjeto.FamiliaId = Int16.Parse(FamiliaCombo.SelectedValue);

                if (SubFamiliaEntidadObjeto.FamiliaId == 0)
                {
                    SubFamiliaCombo.Items.Clear();
                }
                else
                {
                    Resultado = SubFamiliaProcesoObjeto.SeleccionarSubFamilia(SubFamiliaEntidadObjeto);

                    SubFamiliaCombo.DataValueField = "SubFamiliaId";
                    SubFamiliaCombo.DataTextField = "Nombre";

                    if (Resultado.ErrorId == 0)
                    {
                        SubFamiliaCombo.DataSource = Resultado.ResultadoDatos;
                        SubFamiliaCombo.DataBind();
                    }
                    else
                    {
                      //  EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    }
                }

                SubFamiliaCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
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

            private void SeleccionarRequisicion(string ClaveRequisicion, string SesionId)
            {
                //RequisicionProceso RequisicionProceso = new RequisicionProceso();

                //RequisicionProceso.RequisicionEntidad.Clave = ClaveRequisicion;
                //RequisicionProceso.RequisicionEntidad.SesionId = SesionId;

                //RequisicionProceso.SeleccionarRequisicionOrdenSalida();

                //if (RequisicionProceso.ErrorId != 0)
                //{
                //    MostrarMensaje(RequisicionProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                //    return;
                //}

                // ToDo: Cambiar el estilo del grid si está vacío el dataset

                //if (RequisicionProceso.ResultadoDatos.Tables[0].Rows == 0)
                //    LimpiarFormulario();
                //else
                //{


                //}
            }

            private void ValidarRequisicion(string ClaveRequisicion)
            {
                UsuarioEntidad UsuarioEntidad = new UsuarioEntidad();

                UsuarioEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                // Validar que la requisición tenga un estatus válido


                SeleccionarRequisicion(ClaveRequisicion, UsuarioEntidad.SesionId);
            }


        #endregion
    }
}
