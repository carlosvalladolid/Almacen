using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activos.Comun.Constante;
using Activos.Comun.Fecha;
using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Mantenimiento;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Mantenimiento;



namespace Activos.Web.Aplicacion.Reportes
{
    public partial class ReporteMantenimientosPorActivo : System.Web.UI.Page
    {
        #region Eventos

        protected void BotonBuscarActivo_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BuscarActivo();
            }
        }

        protected void BotonImprimir_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ImprimirReporte();
            }
        }

        protected void BotonLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
            CodigoBarrasBusqueda.Text = "";
            NumeroSerieBusqueda.Text = "";
        }

        protected void FechaDesde_Validate(object source, ServerValidateEventArgs args)
        {
            string strFechaDesde = string.Empty;
            DateTime dtEndDate;

            strFechaDesde = FechaDesde.Text.Trim();

            if (strFechaDesde != "")
            {
                if (DateTime.TryParse(strFechaDesde, out dtEndDate))
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void FechaHasta_Validate(object source, ServerValidateEventArgs args)
        {
            string strFechaHasta = string.Empty;
            DateTime dtEndDate;

            strFechaHasta = FechaHasta.Text.Trim();

            if (strFechaHasta != "")
            {
                if (DateTime.TryParse(strFechaHasta, out dtEndDate))
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        protected void TablaMantenimientos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TablaMantenimientos.PageIndex = e.NewPageIndex;
            SeleccionarMantenimientos();
        }

        #endregion



        #region Métodos

        protected void BuscarActivo()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
            

            if (CodigoBarrasBusqueda.Text.Trim() == "" & NumeroSerieBusqueda.Text.Trim() == "")
            {
                LimpiarPantalla();
                EtiquetaMensaje.Text = "Favor de ingresar el código de barras y/o el número de serie.";
            }
            else
            {
                ActivoEntidadObjeto.CodigoBarrasParticular = CodigoBarrasBusqueda.Text.Trim();
                ActivoEntidadObjeto.NumeroSerie = NumeroSerieBusqueda.Text.Trim();
                //ActivoEntidadObjeto.TipoActivoId = ObtenerTipoActivoId();

                Resultado = ActivoProcesoObjeto.SeleccionarActivo(ActivoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        CodigoBarras.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoBarrasParticular"].ToString();
                        Descripcion.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                        NumeroSerie.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroSerie"].ToString();
                        Modelo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Modelo"].ToString();
                        Marca.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreMarca"].ToString();
                        FolioDocumento.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CompraFolio"].ToString();
                        Proveedor.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["ProveedorNombre"].ToString();
                        ActivoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();
                        EtiquetaMensaje.Text = "";
                        BotonImprimir.Enabled = true;

                        VerificarAsignacion(int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString()));
                        SeleccionarMantenimientos();

                       
                    }
                    else
                    {
                        LimpiarPantalla();
                        EtiquetaMensaje.Text = "Activo no encontrado.";
                    }
                }
                else
                {
                    LimpiarPantalla();
                    EtiquetaMensaje.Text = Resultado.DescripcionError;
                }
            }
        }

        protected void ConsultarEmpleado(Int16 EmpleadoAsignadoId) 
        {
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            EmpleadoEntidadObjeto.EmpleadoId = EmpleadoAsignadoId;
            Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables.Count == 0)
                {
                    EmpleadoAsignado.Text = "";
                }
                else
                {
                    EmpleadoAsignado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                }
            }
            else EtiquetaMensaje.Text = TextoError.ErrorGenerico;

        }

        protected void ImprimirReporte()
        {
            //Se pasan los valores a los campos ocultos
            CodigoBarrasHidden.Value = CodigoBarras.Text;
            DescripcionHidden.Value = Descripcion.Text;
            NumeroSerieHidden.Value = NumeroSerie.Text;
            ModeloHidden.Value = Modelo.Text;
            MarcaHidden.Value = Marca.Text;
            FolioDocumentoHidden.Value = FolioDocumento.Text;
            ProveedorHidden.Value = Proveedor.Text;
            FechaDesdeHidden.Value = FechaDesde.Text;
            FechaHastaHidden.Value = FechaHasta.Text;
            EmpleadoAsignadoHidden.Value = EmpleadoAsignado.Text;
            EstatusIdHidden.Value = EstatusId.SelectedValue;
            EstatusNombreHidden.Value = EstatusId.SelectedItem.Text;


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "Imprimir()", true);
        }

        protected void Inicio()
        {
            if (!Page.IsPostBack)
            {
                //Se validan los permisos
                Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.ReporteMantenimientosPorActivo);

                TablaMantenimientos.DataSource = null;
                TablaMantenimientos.DataBind();
                SeleccionarEstatus();
                SeleccionarTextoError();
            }
        }

        protected void LimpiarPantalla()
        {
            CodigoBarras.Text = "";
            Descripcion.Text = "";
            NumeroSerie.Text = "";
            Modelo.Text = "";
            Marca.Text = "";
            Modelo.Text = "";
            FolioDocumento.Text = "";
            Proveedor.Text = "";
            FechaDesde.Text = "";
            FechaHasta.Text = "";
            EmpleadoAsignado.Text = "";

            BotonImprimir.Enabled = false;
            ActivoIdHidden.Value = "0";

            TablaMantenimientos.DataSource = null;
            TablaMantenimientos.DataBind();

            EtiquetaMensaje.Text = "";
        }

        protected Int16 ObtenerTipoActivoId()
        {
            Int16 TipoActivoId = 0;
            UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

            UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

            switch (UsuarioSessionEntidad.RolId)
            {
                case (Int16)ConstantePrograma.RolUsuario.Administrador:
                    TipoActivoId = 0;
                    break;

                case (Int16)ConstantePrograma.RolUsuario.Almacenista:
                    TipoActivoId = 0;
                    break;

                case (Int16)ConstantePrograma.RolUsuario.Mantenimientos:
                    TipoActivoId = 0;
                    break;

                case (Int16)ConstantePrograma.RolUsuario.ActivosMobiliario:
                    TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.Mobiliario;
                    break;

                case (Int16)ConstantePrograma.RolUsuario.ActivosEquipoComputo:
                    TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.EquipoComputo;
                    break;

                case (Int16)ConstantePrograma.RolUsuario.ActivosVehiculo:
                    TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.Vehiculo;
                    break;

                case (Int16)ConstantePrograma.RolUsuario.ActivosOperacionYMantenimiento:
                    TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.OperaciónYMantenimiento;
                    break;
            }

            return TipoActivoId;
        }

        protected void SeleccionarEstatus()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EstatusEntidad EstatusEntidadObjeto = new EstatusEntidad();
            EstatusProceso EstatusProcesoObjeto = new EstatusProceso();

            EstatusEntidadObjeto.SeccionId = (int)ConstantePrograma.Seccion.Mantenimientos;

            Resultado = EstatusProcesoObjeto.SeleccionarEstatus(EstatusEntidadObjeto);

            EstatusId.DataValueField = "EstatusId";
            EstatusId.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                EstatusId.DataSource = Resultado.ResultadoDatos;
                EstatusId.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }
            EstatusId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
        }

        protected void SeleccionarMantenimientos()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto = new MantenimientoActivoEntidad();
            MantenimientoActivoProceso MantenimientoActivoProcesoObjeto = new MantenimientoActivoProceso();

            MantenimientoActivoEntidadObjeto.ActivoId = int.Parse(ActivoIdHidden.Value);
            MantenimientoActivoEntidadObjeto.EstatusId = Int16.Parse(EstatusId.SelectedValue);
            if (FechaDesde.Text != "")
                MantenimientoActivoEntidadObjeto.StrFechaInicio = FormatoFecha.AsignarFormato(FechaDesde.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);

            if (FechaHasta.Text != "")
                MantenimientoActivoEntidadObjeto.StrFechaFin = FormatoFecha.AsignarFormato(FechaHasta.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);

           


            Resultado = MantenimientoActivoProcesoObjeto.SeleccionarMantenimientoReportePorActivo(MantenimientoActivoEntidadObjeto);
            
            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaMantenimientos.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaMantenimientos.CssClass = ConstantePrograma.ClaseTabla;

                TablaMantenimientos.DataSource = Resultado.ResultadoDatos;
                TablaMantenimientos.DataBind();

                BotonImprimir.Enabled = true;
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

        }

        protected void SeleccionarTextoError()
        {
            FechaDesdeValidado.ErrorMessage = TextoError.FechaDesdeInvalido + "<br />";
            FechaHastaValidado.ErrorMessage = TextoError.FechaHastaInvalido + "<br />";
        }

        protected void VerificarAsignacion(int ActivoId) 
        {
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();
            ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            ActivoEntidadObjeto.ActivoId = ActivoId;

            Resultado = MovimientoProcesoObjeto.SeleccionarAsignacionPorEmpleado(ActivoEntidadObjeto);
            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    EmpleadoAsignado.Text = "";
                }
                else 
                {
                    ConsultarEmpleado(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoResguardoId"].ToString()));
                }
            }
            else EtiquetaMensaje.Text = TextoError.ErrorGenerico;
  
        }

        #endregion

        
    }
}
