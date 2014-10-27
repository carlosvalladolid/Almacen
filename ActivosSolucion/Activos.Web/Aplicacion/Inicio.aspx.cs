using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using Activos.Comun.Constante;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Seguridad;
using Activos.Entidad.General;

namespace Activos.Web.Aplicacion
{
    public partial class Inicio : System.Web.UI.Page
    {
        #region "Eventos"

            protected void Page_Load(object sender, EventArgs e)
            {
                InicioPagina();
            }

        #endregion

        #region "Métodos"

            protected void InicioPagina()
            {
                if (!Page.IsPostBack)
                {
                    SeleccionarPaginas();
                }
            }

            protected void SeleccionarPaginas()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                RolEntidad RolEntidadObjeto = new RolEntidad();
                RolProceso RolProcesoObjeto = new RolProceso();
                bool MostrarSeccionCatalogos = false;
                bool MostrarSeccionActivo = false;
                bool MostrarSeccionMantenimiento = false;
                bool MostrarSeccionReportes = false;

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                RolEntidadObjeto.RolId = UsuarioSessionEntidad.RolId;

                Resultado = RolProcesoObjeto.SeleccionarRolPagina(RolEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    foreach (DataRow dtRegistro in Resultado.ResultadoDatos.Tables[0].Rows)
                    {
                        switch (Int16.Parse(dtRegistro["PaginaId"].ToString()))
                        {
                            case (Int16)ConstantePrograma.Paginas.Edificios:
                                DivEdificios.Style["display"] = "block;";
                                MostrarSeccionCatalogos = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Empleados:
                                DivEmpleados.Style["display"] = "block;";
                                MostrarSeccionCatalogos = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Departamentos:
                                DivDepartamentos.Style["display"] = "block;";
                                MostrarSeccionCatalogos = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Direccion:
                                DivDireccion.Style["display"] = "block;";
                                MostrarSeccionCatalogos = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Familias:
                                DivFamilias.Style["display"] = "block;";
                                MostrarSeccionCatalogos = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Subfamilias:
                                DivSubfamilias.Style["display"] = "block;";
                                MostrarSeccionCatalogos = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Marcas:
                                DivMarcas.Style["display"] = "block;";
                                MostrarSeccionCatalogos = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Jefes:
                                DivJefes.Style["display"] = "block;";
                                MostrarSeccionCatalogos = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Proveedores:
                                DivProveedores.Style["display"] = "block;";
                                MostrarSeccionCatalogos = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Puestos:
                                DivPuestos.Style["display"] = "block;";
                                MostrarSeccionCatalogos = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Usuarios:
                                DivUsuarios.Style["display"] = "block;";
                                MostrarSeccionCatalogos = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.RecepcionActivos:
                                DivRecepcion.Style["display"] = "block;";
                                MostrarSeccionActivo = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.AsignacionActivos:
                                DivAsignacion.Style["display"] = "block;";
                                MostrarSeccionActivo = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.AsignacionGeneralActivos:
                                DivAsignacionGeneral.Style["display"] = "block;";
                                MostrarSeccionActivo = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.BajaActivos:
                                DivBaja.Style["display"] = "block;";
                                MostrarSeccionActivo = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.TransferenciaActivos:
                                DivTransferenciaActivos.Style["display"] = "block;";
                                MostrarSeccionActivo = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.EtiquetadoActivos:
                                DivEtiquetado.Style["display"] = "block;";
                                MostrarSeccionActivo = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.LevantamientoInventario:
                                DivLevantamiento.Style["display"] = "block;";
                                MostrarSeccionActivo = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.HistorialActivos:
                                DivHistorial.Style["display"] = "block;";
                                MostrarSeccionActivo = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.EntradasSalidas:
                                DivEntradasSalidas.Style["display"] = "block;";
                                MostrarSeccionActivo = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.TransferenciaAccesorios:
                                DivTransferenciaAccesorios.Style["display"] = "block;";
                                MostrarSeccionActivo = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.AtencionUsuarios:
                                DivAtencionUsuarios.Style["display"] = "block;";
                                MostrarSeccionMantenimiento = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteGeneralActivos:
                                DivReporteGeneralActivo.Style["display"] = "block;";
                                MostrarSeccionReportes = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteEstatusActivos:
                                DivReporteEstatusActivo.Style["display"] = "block;";
                                MostrarSeccionReportes = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteRastreoActivos:
                                DivReporteRastreoActivo.Style["display"] = "block;";
                                MostrarSeccionReportes = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteRastreoAccesorios:
                                DivReporteRastreoAccesorio.Style["display"] = "block;";
                                MostrarSeccionReportes = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteActivosPorEmpleado:
                                DivReporteActivosPorEmpleado.Style["display"] = "block;";
                                MostrarSeccionReportes = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteMantenimientosPorTecnico:
                                DivReporteMantenimientosPorTecnico.Style["display"] = "block;";
                                MostrarSeccionReportes = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteGeneralMantenimientos:
                                DivReporteGeneralMantenimientos.Style["display"] = "block;";
                                MostrarSeccionReportes = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteMantenimientosPorActivo:
                                DivReporteMantenimientosPorActivo.Style["display"] = "block;";
                                MostrarSeccionReportes = true;
                                break;
                        }
                    }

                    //Ahora se muestran las secciones
                    if (MostrarSeccionCatalogos == true)
                        DivSeccionCatalogos.Style["display"] = "block;";

                    if (MostrarSeccionActivo == true)
                        DivSeccionActivo.Style["display"] = "block;";

                    if (MostrarSeccionMantenimiento == true)
                        DivSeccionMantenimientos.Style["display"] = "block;";

                    if (MostrarSeccionReportes == true)
                        DivSeccionReportes.Style["display"] = "block;";

                    //Faltan las demas secciones
                    DivSeccionConfiguracion.Style["display"] = "block;";
                }

            }

        #endregion
    }
}
