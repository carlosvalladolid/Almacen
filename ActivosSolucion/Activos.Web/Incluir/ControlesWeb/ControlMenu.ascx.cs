using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Activos.Comun.Constante;
using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Incluir.ControlesWeb
{
    public partial class ControlMenu : System.Web.UI.UserControl
    {
        public enum Behavior
        {
            MenuInicio = 0,
            Catalogos = (Int16)ConstantePrograma.SeccionMenu.Catalogos,
            ActivoFijo = (Int16)ConstantePrograma.SeccionMenu.ActivoFijo,
            Mantenimiento = (Int16)ConstantePrograma.SeccionMenu.Mantenimiento,
            Reportes = (Int16)ConstantePrograma.SeccionMenu.Reportes,
            Configuracion = (Int16)ConstantePrograma.SeccionMenu.Configuracion
        }

        public Behavior SeccionMenu
        {
            get { return (Behavior)ViewState[this.ID + "_SeccionMenu"]; }
            set { ViewState[this.ID + "_SeccionMenu"] = value; }
        }

        #region "Eventos"

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio();
            }

        #endregion

        #region "Métodos"

            protected void Inicio()
            {
                if (!Page.IsPostBack)
                {

                    switch (SeccionMenu)
                    {
                        case Behavior.Catalogos:
                            SeleccionarOpcionesCatalogos();
                            break;

                        case Behavior.ActivoFijo:
                            SeleccionarOpcionesActivos();
                            break;

                        case Behavior.Mantenimiento:
                            SeleccionarOpcionesMantenimiento();
                            break;

                        case Behavior.Reportes:
                            SeleccionarOpcionesReportes();
                            break;

                        case Behavior.Configuracion:
                            OpcionCambiarContrasenia.Visible = true;
                            break;

                        case Behavior.MenuInicio:
                            OpcionCambiarContrasenia.Visible = true;
                            break;
                    }

                }
            }

            protected void SeleccionarOpcionesCatalogos()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                RolEntidad RolEntidadObjeto = new RolEntidad();
                RolProceso RolProcesoObjeto = new RolProceso();

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
                                OpcionEdificio.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Empleados:
                                OpcionEmpleado.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Departamentos:
                                OpcionDepartamento.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Direccion:
                                OpcionDireccion.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Familias:
                                OpcionFamilia.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Subfamilias:
                                OpcionSubfamilia.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Marcas:
                                OpcionMarca.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Jefes:
                                OpcionJefe.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Proveedores:
                                OpcionProveedor.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Puestos:
                                OpcionPuesto.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.Usuarios:
                                OpcionUsuario.Visible = true;
                                break;
                        }
                    }

                }

            }

            protected void SeleccionarOpcionesActivos()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                RolEntidad RolEntidadObjeto = new RolEntidad();
                RolProceso RolProcesoObjeto = new RolProceso();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                RolEntidadObjeto.RolId = UsuarioSessionEntidad.RolId;

                Resultado = RolProcesoObjeto.SeleccionarRolPagina(RolEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    foreach (DataRow dtRegistro in Resultado.ResultadoDatos.Tables[0].Rows)
                    {
                        switch (Int16.Parse(dtRegistro["PaginaId"].ToString()))
                        {
                            case (Int16)ConstantePrograma.Paginas.RecepcionActivos:
                                OpcionRecepcion.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.AsignacionActivos:
                                OpcionAsignacion.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.AsignacionGeneralActivos:
                                OpcionAsignacionGeneral.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.BajaActivos:
                                OpcionBaja.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.TransferenciaActivos:
                                OpcionTransferenciaActivo.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.EtiquetadoActivos:
                                OpcionEtiquetado.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.LevantamientoInventario:
                                OpcionLevantamiento.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.HistorialActivos:
                                OpcionHistorial.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.EntradasSalidas:
                                OpcionEntradaSalida.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.TransferenciaAccesorios:
                                OpcionTransferenciaAccesorio.Visible = true;
                                break;
                        }
                    }

                }

            }

            protected void SeleccionarOpcionesMantenimiento()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                RolEntidad RolEntidadObjeto = new RolEntidad();
                RolProceso RolProcesoObjeto = new RolProceso();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                RolEntidadObjeto.RolId = UsuarioSessionEntidad.RolId;

                Resultado = RolProcesoObjeto.SeleccionarRolPagina(RolEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    foreach (DataRow dtRegistro in Resultado.ResultadoDatos.Tables[0].Rows)
                    {
                        switch (Int16.Parse(dtRegistro["PaginaId"].ToString()))
                        {
                            case (Int16)ConstantePrograma.Paginas.AtencionUsuarios:
                                OpcionAtencionUsuarios.Visible = true;
                                break;
                        }
                    }

                }

            }

            protected void SeleccionarOpcionesReportes()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                RolEntidad RolEntidadObjeto = new RolEntidad();
                RolProceso RolProcesoObjeto = new RolProceso();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                RolEntidadObjeto.RolId = UsuarioSessionEntidad.RolId;

                Resultado = RolProcesoObjeto.SeleccionarRolPagina(RolEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    foreach (DataRow dtRegistro in Resultado.ResultadoDatos.Tables[0].Rows)
                    {
                        switch (Int16.Parse(dtRegistro["PaginaId"].ToString()))
                        {
                            case (Int16)ConstantePrograma.Paginas.ReporteGeneralActivos:
                                OpcionReporteGeneralActivo.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteEstatusActivos:
                                OpcionReporteEstatusActivo.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteRastreoActivos:
                                OpcionReporteRastreoActivo.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteRastreoAccesorios:
                                OpcionReporteRastreoAccesorio.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteActivosPorEmpleado:
                                OpcionReporteActivosPorEmpleado.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteGeneralMantenimientos:
                                OpcionReporteGeneralMantenimientos.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteMantenimientosPorTecnico:
                                OpcionReporteMantenimientosPorTecnico.Visible = true;
                                break;

                            case (Int16)ConstantePrograma.Paginas.ReporteMantenimientosPorActivo:
                                OpcionReporteMantenimientosPorActivo.Visible = true;
                                break;
                        }
                    }

                }

            }

        #endregion
    }
}