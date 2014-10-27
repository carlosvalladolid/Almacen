using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

namespace Activos.Web.Aplicacion.Reportes 
{
    public partial class ReporteGeneralActivo : System.Web.UI.Page
    {
        #region Eventos

        protected void BotonBuscar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SeleccionarActivos();
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
            LimpiarFormulario();
            
        }

        protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSubfamilia();
        }

        protected void ddlDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarDepartamento();
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

        protected void TablaActivo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TablaActivo.PageIndex = e.NewPageIndex;
            SeleccionarActivos();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        #endregion

        #region Métodos

        protected void Inicio() 
        {
            if (!Page.IsPostBack)
            {
                //Se validan los permisos
                Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.ReporteGeneralActivos);

                SeleccionarProveedor();
                SeleccionarFamilia();
                SeleccionarSubfamilia();
                SeleccionarMarca();
                SeleccionarDireccion();
                SeleccionarDepartamento();
                SeleccionarTextoError();
            }
        }

        protected void ImprimirReporte()
        {
            FamiliaIdHidden.Value = FamiliaId.SelectedValue;
            FamiliaNombreHidden.Value = FamiliaId.SelectedItem.Text;
            SubfamiliaIdHidden.Value  = SubFamiliaId.SelectedValue;
            SubfamiliaNombreHidden.Value = SubFamiliaId.SelectedItem.Text;
            MarcaIdHidden.Value = MarcaId.SelectedValue;
            MarcaNombreHidden.Value = MarcaId.SelectedItem.Text;
            ModeloHidden.Value = Modelo.Text.Trim();

            if (FechaDesde.Text.Trim() != "")
                FechaInicioHidden.Value = FechaDesde.Text.Trim();
            else
                FechaInicioHidden.Value = "";

            if (FechaHasta.Text.Trim() != "")
                FechaFinHidden.Value = FechaHasta.Text.Trim();
            else
                FechaFinHidden.Value = "";

            ProveedorIdHidden.Value = ProveedorId.SelectedValue;
            ProveedorNombreHidden.Value = ProveedorId.SelectedItem.Text;
            FolioDocumentoHidden.Value = CompraFolio.Text.Trim();
            DireccionIdHidden.Value = DireccionId.SelectedValue;
            DireccionNombreHidden.Value = DireccionId.SelectedItem.Text;
            DepartamentoIdHidden.Value = DepartamentoId.SelectedValue;
            DepartamentoNombreHidden.Value = DepartamentoId.SelectedItem.Text;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "Imprimir()", true);
        }

        protected void LimpiarFormulario()
        {
            ProveedorId.SelectedIndex = 0;
            FamiliaId.SelectedIndex = 0;
            SeleccionarSubfamilia();
            MarcaId.SelectedIndex = 0;
            DireccionId.SelectedIndex = 0;
            SeleccionarDepartamento();
            Modelo.Text = "";
            FechaDesde.Text = "";
            FechaHasta.Text = "";
            CompraFolio.Text = "";
        }

        protected void SeleccionarDepartamento()
          {
              ResultadoEntidad Resultado = new ResultadoEntidad();
              DepartamentoEntidad DepartamentoEntidadObjeto = new DepartamentoEntidad();
              DepartamentoProceso DepartamentoProcesoObjeto = new DepartamentoProceso();

              //DepartamentoEntidadObjeto.EstatusId = (int)ConstantePrograma.EstatusDepartamentos.Activo;
              DepartamentoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusDepartamentos.Activo;
              DepartamentoEntidadObjeto.DireccionId = Int16.Parse(DireccionId.SelectedValue);

              if (DepartamentoEntidadObjeto.DireccionId == 0)
              {
                  DepartamentoId.Items.Clear();
              }
              else
              {

                      Resultado = DepartamentoProcesoObjeto.SeleccionarDepartamento(DepartamentoEntidadObjeto);

                      DepartamentoId.DataValueField = "DepartamentoId";
                      DepartamentoId.DataTextField = "Nombre";

                      if (Resultado.ErrorId == 0)
                      {
                          DepartamentoId.DataSource = Resultado.ResultadoDatos;
                          DepartamentoId.DataBind();
                      }
                      else
                      {
                          EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
                      }
          }

                  DepartamentoId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
              
          }

        protected void SeleccionarDireccion()
          {
              ResultadoEntidad Resultado = new ResultadoEntidad();
              DireccionEntidad DireccionEntidadObjeto = new DireccionEntidad();
              DireccionProceso DireccionProcesoObjeto = new DireccionProceso();

              //DireccionEntidadObjeto.EstatusId = (int)ConstantePrograma.EstatusDireccion.Activo;

              Resultado = DireccionProcesoObjeto.SeleccionarDireccion(DireccionEntidadObjeto);

              DireccionId.DataValueField = "DireccionId";
              DireccionId.DataTextField = "Nombre";

            

              if (Resultado.ErrorId == 0)
              {
                  DireccionId.DataSource = Resultado.ResultadoDatos;
                  DireccionId.DataBind();

              }
              else
              {
                  EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
              }

              DireccionId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
          }

        protected void SeleccionarFamilia()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
            FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

            FamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusFamilia.Activo;

            Resultado = FamiliaProcesoObjeto.SeleccionarFamilia(FamiliaEntidadObjeto);

            FamiliaId.DataValueField = "FamiliaId";
            FamiliaId.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                FamiliaId.DataSource = Resultado.ResultadoDatos;
                FamiliaId.DataBind();
            }
            else
            {
                EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
            }

            FamiliaId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
        }

        protected void SeleccionarMarca()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MarcaEntidad MarcaEntidadObjeto = new MarcaEntidad();
            MarcaProceso MarcaProcesoObjeto = new MarcaProceso();

            MarcaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusMarca.Activo;

            Resultado = MarcaProcesoObjeto.SeleccionarMarca(MarcaEntidadObjeto);

            MarcaId.DataValueField = "MarcaId";
            MarcaId.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                MarcaId.DataSource = Resultado.ResultadoDatos;
                MarcaId.DataBind();
            }
            else
            {
                EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
            }

            MarcaId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
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
                EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
            }

            ProveedorId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
        }

        protected void SeleccionarSubfamilia()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();
            SubFamiliaProceso SubFamiliaProcesoObjeto = new SubFamiliaProceso();

            SubFamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusSubFamilia.Activo;
            SubFamiliaEntidadObjeto.FamiliaId = Int16.Parse(FamiliaId.SelectedValue);

            if (SubFamiliaEntidadObjeto.FamiliaId == 0)
            {
                SubFamiliaId.Items.Clear();
            }
            else
            {
                Resultado = SubFamiliaProcesoObjeto.SeleccionarSubFamilia(SubFamiliaEntidadObjeto);

                SubFamiliaId.DataValueField = "SubFamiliaId";
                SubFamiliaId.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    SubFamiliaId.DataSource = Resultado.ResultadoDatos;
                    SubFamiliaId.DataBind();
                }
                else
                {
                    EtiquetaMensajeError.Text = TextoError.ErrorGenerico;
                }
            }

            SubFamiliaId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroTodos, "0"));
        }

        protected void SeleccionarActivos()
        {
            ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

            ActivoEntidadObjeto.FamiliaId = Int16.Parse(FamiliaId.SelectedValue);
            ActivoEntidadObjeto.SubFamiliaId = Int16.Parse(SubFamiliaId.SelectedValue);
            ActivoEntidadObjeto.MarcaId = Int16.Parse(MarcaId.SelectedValue);
            ActivoEntidadObjeto.Modelo = Modelo.Text.Trim();

            if (FechaDesde.Text != "")
                ActivoEntidadObjeto.StrFechaInicio = FormatoFecha.AsignarFormato(FechaDesde.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);

            if (FechaHasta.Text != "")
                ActivoEntidadObjeto.StrFechaFin = FormatoFecha.AsignarFormato(FechaHasta.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);

            ActivoEntidadObjeto.ProveedorId = Int16.Parse(ProveedorId.SelectedValue);
            ActivoEntidadObjeto.CompraFolio = CompraFolio.Text.Trim();
            ActivoEntidadObjeto.DireccionId = Int16.Parse(DireccionId.SelectedValue);
            ActivoEntidadObjeto.DepartamentoId = Int16.Parse(DepartamentoId.SelectedValue);
            ActivoEntidadObjeto.TipoActivoId = ObtenerTipoActivoId();
            ActivoEntidadObjeto.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;

            SeleccionarActivos(ActivoEntidadObjeto);
        }

        protected void SeleccionarActivos(ActivoEntidad ActivoEntidadObjeto)
        {
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            Resultado = MovimientoProcesoObjeto.SeleccionarMovimientoReporteActivoGeneral(ActivoEntidadObjeto);

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
                EtiquetaMensajeError.Text = Resultado.DescripcionError;
            }

            
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

        protected void SeleccionarTextoError()
        {
            FechaDesdeValidado.ErrorMessage = TextoError.FechaDesdeInvalido + "<br />";
            FechaHastaValidado.ErrorMessage = TextoError.FechaHastaInvalido + "<br />";
        }

        #endregion
       
    }
}
