using Microsoft.Reporting.WebForms;

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
using Activos.Entidad.Catalogo;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Almacen;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Seguridad;

namespace Almacen.Web.Aplicacion.Reporte.Viewer
{
    public partial class ExistenciaProducto : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonBusqueda_Click(object sender, EventArgs e)
            {
                seleccionarReporteExistenciaProducto();
            }

            protected void BotonCancelarBusqueda_Click(object sender, EventArgs e)
            {

            }

            protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarSubfamilia();
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

                //Validamos permisos
                Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.AlmacenExistenciaDeProducto);   

                SeleccionarFamilia();
                SeleccionarSubfamilia();
                SeleccionarMarca();
                ExistenciaProductoReporteViewer.Visible = false;
                //seleccionarReporteExistenciaProducto();
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

            protected void SeleccionarFamilia()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
                FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

                Resultado = FamiliaProcesoObjeto.SeleccionarFamilia(FamiliaEntidadObjeto);

                FamiliaCombo.DataValueField = "FamiliaId";
                FamiliaCombo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    FamiliaCombo.DataSource = Resultado.ResultadoDatos;
                    FamiliaCombo.DataBind();
                }
                else
                    MostrarMensaje(TextoError.ErrorGenerico, ConstantePrograma.TipoErrorAlerta);

                FamiliaCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarSubfamilia()
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
                      // EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    }
                }

                SubFamiliaCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void SeleccionarMarca()
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
                   // EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                MarcaCombo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            }

            private void seleccionarReporteExistenciaProducto()
            {
                AlmacenEntidad AlmacenEntidad = new AlmacenEntidad();
                AlmacenProceso AlmacenProceso = new AlmacenProceso();
                ResultadoEntidad ResultadoDatosEntidad = new ResultadoEntidad();

                AlmacenEntidad.Clave = ClaveBusqueda.Text.Trim();
                AlmacenEntidad.Descripcion = NombreBusqueda.Text.Trim();
                AlmacenEntidad.FamiliaId = Int16.Parse(FamiliaCombo.SelectedValue);
                AlmacenEntidad.SubFamiliaId = Int16.Parse(SubFamiliaCombo.SelectedValue);
                AlmacenEntidad.MarcaId = Int16.Parse(MarcaCombo.SelectedValue);

                ResultadoDatosEntidad = AlmacenProceso.SeleccionarExistenciaProducto(AlmacenEntidad);

                if (ResultadoDatosEntidad.ErrorId == 0)
                {
                    ExistenciaProductoReporteViewer.Visible = true;
                    ReportDataSource ParametroFuenteDatos = new ReportDataSource("ExistenciaPoductoDS_ExistenciaProductoDT", ResultadoDatosEntidad.ResultadoDatos.Tables[0]);

                    ExistenciaProductoReporteViewer.LocalReport.DataSources.Clear();
                    ExistenciaProductoReporteViewer.LocalReport.DataSources.Add(ParametroFuenteDatos);
                    ExistenciaProductoReporteViewer.LocalReport.Refresh();
                }

                //MostrarMensaje(AlmacenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
            }

        #endregion
    }
}
