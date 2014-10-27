using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

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
    public partial class BajaActivo : System.Web.UI.Page
    {
        #region "Eventos"

        protected void BotonActualizarActivo_Click(object sender, EventArgs e)
        {
            // AgregarActivo();
        }

        protected void BotonAgregar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                DarBajaTemporal();
            }
        }

        protected void BotonCancelar_Click(object sender, EventArgs e)
        {
            VaciarTemporalMovimientos();
            LlenarTabla();
            LimpiarCampos();

        }

        protected void BotonDarDeBaja_Click(object sender, EventArgs e)
        {
            DarBaja();
        }

        protected void BotonImprimir_Click(object sender, EventArgs e) 
        {
            ImprimirFormato();
        }

        protected void BotonLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void ImagenCodigoBarras_Click(object sender, EventArgs e)
        {
            LocalizarActivo();
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        protected void RadioOtrosActivo_Select(object sender, EventArgs e)
        {
            ActivarOtrosTipoBaja();
        }                             

        protected void TablaBaja_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaBajaEliminarSeleccion(e);
        }

        protected void TablaBaja_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void FechaBaja_Validate(object source, ServerValidateEventArgs args)
        {
            string strStartDate = string.Empty;
            DateTime dtStartDate;

            strStartDate = FechaBaja.Text.Trim();

            if (strStartDate != "")
            {
                if (DateTime.TryParse(strStartDate, out dtStartDate))
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = false;
            }
        }

        #endregion

        #region "Métodos"

        protected void Inicio()
        {
            if (!Page.IsPostBack)
            {
                //Validamos permisos
                Activos.ProcesoNegocio.Base BaseProcesoNegocio = new Activos.ProcesoNegocio.Base();
                BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.BajaActivos);
              
                SeleccionarCondicion();
                SeleccionarTexto();
                VaciarTemporalMovimientos();
                TablaBaja.DataSource = null;
                TablaBaja.DataBind();
            }
        }

        protected void ActivarOtrosTipoBaja()
        {
            OtrosTipoBaja.Enabled = true;
            if (Int16.Parse(TipoBaja.SelectedValue) == 1)
            {
                OtrosTipoBaja.Text = TipoBaja.SelectedItem.Text + ", ";
            }
            else if (Int16.Parse(TipoBaja.SelectedValue) == 2)
            {
                OtrosTipoBaja.Text = TipoBaja.SelectedItem.Text + ", ";
            }
            else
                OtrosTipoBaja.Text = "";

        }

        protected ResultadoEntidad BuscarActivo()
        {
            
            ResultadoEntidad ResultadoEntidad = new ResultadoEntidad();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();
            EmpleadoEntidad EmpleadoObjetoEntidad = new EmpleadoEntidad();
            AccesorioEntidad AccesorioEntidadObjeto = new AccesorioEntidad();
            CompraEntidad CompraEntidadObjeto = new CompraEntidad();
            UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
            bool EsAccesorio = false;
            bool EsActivoPadre = false;
            
            
            ActivoObjetoEntidad.CodigoBarrasParticular = CodigoBarrasBaja.Text.Trim();
            ActivoObjetoEntidad.TipoActivoId = ObtenerTipoActivoId();
            UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

            ResultadoEntidad = ActivoProcesoObjeto.ValidarExistenciaActivoSalida(ActivoObjetoEntidad, UsuarioSessionEntidad.RolId);

            

            if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoDadoDeBaja || ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoNoEncontrado
                || ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoConEstatusSalida || ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.PermisoDenegado)
            {
                return ResultadoEntidad;
            }
            else
                if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoNoAsignado)
                {
                    NumeroEmpleado.Text = "XXXXXXXXX";
                    NombreEmpleado.Text = "Activo en almacen";
                    return ResultadoEntidad;

                }

                else if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.EntradasSalidas.ActivoValidoParaSalida||ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoPadre || ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoEsAccresorioAsignado)
                {

                       if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoPadre) 
                        {
                            EsActivoPadre = true;
                        }if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoEsAccresorioAsignado)
                        {
                            EsAccesorio = true;
                            ActivoIdHidden.Value = ResultadoEntidad.NuevoRegistroId.ToString();
                        }else ActivoIdHidden.Value = ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();

                        EmpleadoObjetoEntidad.EmpleadoId = Int16.Parse(ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["EmpleadoResguardoId"].ToString());
                        EmpleadoIdHidden.Value = ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["EmpleadoResguardoId"].ToString();
                        ResultadoEntidad = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoObjetoEntidad);


                        NumeroEmpleado.Text = ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                        NombreEmpleado.Text = ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString() + " " + ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["ApellidoPaterno"].ToString();

                        ResultadoEntidad = ActivoProcesoObjeto.SeleccionarActivoPorCompra(ActivoObjetoEntidad);
                      //CompraEntidadObjeto.CompraId=Int16.Parse(ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["CompraId"].ToString());

                    
                        CodigoBarrasParticularHidden.Value = ActivoObjetoEntidad.CodigoBarrasParticular;
                        if (EsAccesorio == true)
                        {
                            //mostrar mensaje cuando sea una accesorio asignado

                            ResultadoEntidad.DescripcionError = TextoError.ActivoEsAccresorioAsignado;
                            ResultadoEntidad.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoEsAccresorioAsignado;
                            
                            return ResultadoEntidad;
                        }

                        if (EsActivoPadre == true)
                        {
                            ResultadoEntidad.DescripcionError = TextoError.ActivoPadre;
                            ResultadoEntidad.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoPadre;
                            
                            AccesorioEntidadObjeto.ActivoId = Int16.Parse(ActivoIdHidden.Value.ToString());

                            ControlBuscarAccesorio.InicioControl(AccesorioEntidadObjeto);
                            ActualizarTablaAsignacion.Update();

                            return ResultadoEntidad;
                        }

                        return ResultadoEntidad;
                    
                } return ResultadoEntidad;


        }

        protected void DarBaja()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            UsuarioEntidad UsuarioEntidadActual = new UsuarioEntidad();
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
            AccesorioEntidad AccesorioObjetoEntidad = new AccesorioEntidad();
            AccesorioProceso AccesorioProcesoObjero = new AccesorioProceso();
            string EmpleadoAsignado = string.Empty;
            

            UsuarioEntidadActual = (UsuarioEntidad)Session["UsuarioEntidad"];
            ActivoObjetoEntidad.SesionId = UsuarioEntidadActual.SesionId;
            ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Baja;

            //CON INNER JOIN    
            Resultado = ActivoProcesoObjeto.SeleccionarActivoTemporal(ActivoObjetoEntidad);
            if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
            {
                EtiquetaMensaje.Text = TextoError.ActivoNoEncontrado;
                
            }else

                Resultado = ActivoProcesoObjeto.InsertarMovimientoBaja(ActivoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.BajaActivo.BajaActivoCorrecta)
                {
                    EtiquetaMensaje.CssClass = ConstantePrograma.ClaseInformacion;
                    EtiquetaMensaje.Text = TextoError.BajaActivoCorrecta;


                   
                    LlenarTabla();
                    EmpleadoAsignado = ActivoObjetoEntidad.EmpleadoId.ToString();
                    EmpleadoIdHidden.Value = ActivoObjetoEntidad.EmpleadoId.ToString();
                    BotonImprimir.Enabled = true;
                    BotonDarDeBaja.Enabled = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirBaja('" + EmpleadoAsignado + "')", true);
                }
                
          

        }

        protected void DarBajaTemporal()
        {
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            UsuarioEntidad UsuarioEntidadActual = new UsuarioEntidad();
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
            TemporalActivoEntidad TemporalActivoObjetoEntidad = new TemporalActivoEntidad();
            TemporalActivoProceso TemporalActivoProcesoObjeto = new TemporalActivoProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            
            bool EsPadre = false;
           

                if (NumeroEmpleadoHiddden.Value == NumeroEmpleado.Text || NumeroEmpleadoHiddden.Value == "")
                {
                    NumeroEmpleadoHiddden.Value = NumeroEmpleado.Text;

                    ActivoObjetoEntidad.CodigoBarrasParticular = CodigoBarrasParticularHidden.Value;
                    ActivoObjetoEntidad.TipoBaja = TipoBaja.SelectedValue;
                    ActivoObjetoEntidad.DescripcionMovimiento = OtrosTipoBaja.Text.Trim();
                    UsuarioEntidadActual = (UsuarioEntidad)Session["UsuarioEntidad"];
                    ActivoObjetoEntidad.SesionId = UsuarioEntidadActual.SesionId;

                    ActivoObjetoEntidad.UsuarioId = UsuarioEntidadActual.UsuarioId;
                    ActivoObjetoEntidad.UsuarioIdModifico = UsuarioEntidadActual.UsuarioId;
                    ActivoObjetoEntidad.FechaMovimiento = FormatoFecha.AsignarFormato(FechaBaja.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);
                    ActivoObjetoEntidad.CondicionId = Int16.Parse(CondicionId.SelectedValue.ToString());
                    ActivoObjetoEntidad.ActivoId = Int16.Parse(ActivoIdHidden.Value);
                    ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Baja;
                    ActivoObjetoEntidad.EstatusId = (Int16)ConstantePrograma.EstatusActivos.Baja;
                    ActivoObjetoEntidad.EmpleadoId = Int16.Parse(EmpleadoIdHidden.Value.ToString());
                    

                    MostrarNombreADarBaja();
                    if (ActivoPadreHidden.Value != "0")
                    {
                        EsPadre= true;
                        ActivoPadreHidden.Value = "0";
                    }
                    Resultado = ActivoProcesoObjeto.DarBajaTemporal(ActivoObjetoEntidad, EsPadre);
                    if (Resultado.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoYaSeleccionado)
                    {
                        LlenarTabla();
                        LimpiarCampos();
                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                        EtiquetaMensaje.Text = TextoError.ActivoYaSeleccionado;
                    }
                    else
                    {

                        ActivoObjetoEntidad.SesionId = UsuarioEntidadActual.SesionId;
                        ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Baja;

                        //CON INNER JOIN    
                        Resultado = ActivoProcesoObjeto.SeleccionarActivoTemporal(ActivoObjetoEntidad);
                        if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        {
                            EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                            EtiquetaMensaje.Text = TextoError.ActivoNoEncontrado;
                        }
                        else
                        {
                            TablaBaja.DataSource = Resultado.ResultadoDatos;
                            TablaBaja.DataBind();
                            LimpiarCampos();
                            BotonDarDeBaja.Enabled = true;
                        }
                    }
                }
                else 
                {
                    LimpiarCampos();
                    EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                    EtiquetaMensaje.Text = TextoError.EmpleadoDiferente;
                }
            
                
            
        }

        protected void EliminarActivo(int MovimientoId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
            ActivoEntidadObjeto.MovimientoId = Int16.Parse(MovimientoId.ToString());

            Resultado = ActivoProcesoObjeto.EliminarActivoTemporalSeleccionado(ActivoEntidadObjeto);

            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoEliminadoCorrectamente)
            {
                EtiquetaMensaje.CssClass = ConstantePrograma.ClaseInformacion;
                EtiquetaMensaje.Text = TextoError.ElementoEliminado;
            }
            else

                EtiquetaMensaje.Text = Resultado.DescripcionError;
        }

        public void EsActivoPadre() 
        {
            ActivoPadreHidden.Value = "1";
            ActualizarTablaAsignacion.Update();
        }

        protected void ImprimirFormato() 
        {
            string EmpleadoAsignado = string.Empty;   
            EmpleadoAsignado = EmpleadoIdHidden.Value;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirBaja('" + EmpleadoAsignado + "')", true);
        }
        
        public void LimpiarFormulario()
        {
            LimpiarCampos();
        }

        protected void LimpiarCampos()
        {
            FechaBaja.Text = "";
            TipoBaja.ClearSelection();
            OtrosTipoBaja.Enabled = false;
            OtrosTipoBaja.Text = "";
            CondicionId.SelectedIndex = 0;
            CodigoBarrasBaja.Text = "";
            NumeroEmpleado.Text = "";
            NombreEmpleado.Text = "";
            EtiquetaMensaje.Text = "";
            ActivoPadreHidden.Value = "0";
            ActualizarTablaAsignacion.Update();

        }

        protected void LlenarTabla()
        {
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
            UsuarioEntidad UsuarioEntidadActual = new UsuarioEntidad();

            UsuarioEntidadActual = (UsuarioEntidad)Session["UsuarioEntidad"];
            ActivoObjetoEntidad.SesionId = UsuarioEntidadActual.SesionId;
            ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Baja;

            //CON INNER JOIN    
            Resultado = ActivoProcesoObjeto.SeleccionarActivoTemporal(ActivoObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
            {
                EtiquetaMensaje.CssClass = ConstantePrograma.ClaseInformacion;
                EtiquetaMensaje.Text = TextoError.ListaVacia;
                NumeroEmpleadoHiddden.Value = "";
                EmpleadoNombreBaja.Text = "";
                TablaBaja.DataBind();
                BotonDarDeBaja.Enabled = false;
                BotonImprimir.Enabled = false;
            }
            else
            {
                TablaBaja.DataSource = Resultado.ResultadoDatos;
                TablaBaja.DataBind();
            }
        }

        protected void LocalizarActivo()
        {

            switch (BuscarActivo().ErrorId)
            {

                case (int)ConstantePrograma.BajaActivo.ActivoNoEncontrado:
                    LimpiarCampos();
                    EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                    EtiquetaMensaje.Text = TextoError.ActivoNoEncontrado;
                    break;

                case (int)ConstantePrograma.BajaActivo.ActivoDadoDeBaja:
                    LimpiarCampos();
                    EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                    EtiquetaMensaje.Text = TextoError.ActivoDadoDeBaja;
                    break;

                case (int)ConstantePrograma.BajaActivo.ActivoNoAsignado:
                    LimpiarCampos();
                    EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                    EtiquetaMensaje.Text = TextoError.ActivoNoAsignado;
                    break;

                case (int)ConstantePrograma.BajaActivo.ActivoConEstatusSalida:
                    LlenarTabla();
                    LimpiarCampos();
                    EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                    EtiquetaMensaje.Text = TextoError.ActivoConEstatusSalida;
                    
                    break;
                case (int)ConstantePrograma.BajaActivo.PermisoDenegado:
                    LlenarTabla();
                    LimpiarCampos();
                    EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                    EtiquetaMensaje.Text = TextoError.PermisoDenegado;

                    break;

                case (int)ConstantePrograma.BajaActivo.ActivoEsAccresorioAsignado:
                    LlenarTabla();
                    EtiquetaMensaje.CssClass = ConstantePrograma.ClaseInformacion;
                    EtiquetaMensaje.Text = TextoError.ActivoEsAccresorioAsignado;
                    break;

             
                default:
                    EtiquetaMensaje.Text = "";
                   
                    break;
            }

        }

        protected void MostrarNombreADarBaja()
        {
            if (EmpleadoNombreBaja.Text == "")
            {
                EmpleadoNombreBaja.Text = NombreEmpleado.Text;
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

        protected void SeleccionarCondicion()
        {

            ResultadoEntidad Resultado = new ResultadoEntidad();
            CondicionEntidad CondicionEntidadObjeto = new CondicionEntidad();
            CondicionProceso CondicionProcesoObjeto = new CondicionProceso();


            Resultado = CondicionProcesoObjeto.SeleccionarCondicion(CondicionEntidadObjeto);

            CondicionId.DataValueField = "CondicionId";
            CondicionId.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                CondicionId.DataSource = Resultado.ResultadoDatos;
                CondicionId.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            CondicionId.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }       
                
        protected void SeleccionarTexto()
        {
            FechaBajaRequerido.ErrorMessage = TextoError.CampoFechaVacio + "<br />";
            TipoBajaRequerido.ErrorMessage = TextoError.TipoBajaNoSeleccionado + "<br />";
            OtrosTipoBajaRequerido.ErrorMessage = TextoError.OtrosTipoBajaRequerido + "<br />";
            CondicionRequerido.ErrorMessage = TextoError.AsignacionCondicion + "<br />";
            CodigoBarrasRequerido.ErrorMessage = TextoError.AsignacionCodigoBarrasParticular + "<br />";
            CodigoBarras.ErrorMessage = TextoError.AsignacionCodigoBarrasParticular + "<br />";

        }
                
        protected void TablaBajaEliminarSeleccion(GridViewCommandEventArgs e)
        {
            Int16 intFila = 0;
            int MovimientoId = 0;
            string strCommand = string.Empty;

            intFila = Int16.Parse(e.CommandArgument.ToString());
            strCommand = e.CommandName.ToString();

            if (strCommand == "EliminarActivo")
            {
                MovimientoId = int.Parse(TablaBaja.DataKeys[intFila]["MovimientoId"].ToString());
                EliminarActivo(MovimientoId);
                LlenarTabla();

            }

        }

        protected void VaciarTemporalMovimientos() 
        {
            //ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            UsuarioEntidad UsuarioEntidadActual = new UsuarioEntidad();
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();


            UsuarioEntidadActual = (UsuarioEntidad)Session["UsuarioEntidad"];
            ActivoObjetoEntidad.SesionId = UsuarioEntidadActual.SesionId;
            ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Baja;

           ActivoProcesoObjeto.EliminarRegistrosTemporales(ActivoObjetoEntidad);

        }

        #endregion
            }
        }
    



