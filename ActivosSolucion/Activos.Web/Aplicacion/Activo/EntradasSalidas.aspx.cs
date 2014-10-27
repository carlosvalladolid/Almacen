using System;
using System.Collections.Generic;
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

namespace Activos.Web.Aplicacion.Activo
{
    public partial class EntradasSalidas : System.Web.UI.Page
    {
        #region "Eventos"
            protected void BotonBuscarCodigoBarra_Click(object sender, EventArgs e)
            {
                LocalizarActivo();
            }

            protected void BotonAgregarActivo_Click(object sender, EventArgs e)
            {
                if (Page.IsValid)
                {
                    AgregarActivo();
                }
            }

            protected void LinkBuscarActivo_Click(object sender, EventArgs e)
            {
                
            }

            protected void MostrarCamposProveedor(object sender, EventArgs e)
            {
                MostrarCamposProveedor();    
            }

            protected void NumeroEmpleado_TextChanged(object sender, EventArgs e)
            {
                NumeroEmpleadoTextoModificado();
            }

            protected void BotonCancelar_Click(object sender, EventArgs e)
            {
                VaciarTemporalMovimientos();
                LimpiarCampos();
                LlenarTabla();

                BotonGuardarActivo.Enabled = true;
                ActivoVehiculoHidden.Value = "0";
                MostrarCamposProveedor();

            }

            protected void BotonDarEntradaSalida_Click(object sender, EventArgs e) 
            {
                GuardarEntradasSalidas();
            }

            protected void BotonImprimir_Click(object sender, EventArgs e)
            {
                ImprimirFormato();

            }

            protected void BotonLimpiar_Click(object sender, EventArgs e)
            {
                LimpiarCampos();
            }

            protected void Page_Load(object sender, EventArgs e)
            {
                Inicio(); 
            }

            protected void TablaActivo_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TablaBajaEliminarSeleccion(e);
            }

            protected void TablaActivo_RowDataBound(object sender, GridViewRowEventArgs e)
            {

            }

            protected void LinkBuscarSolicitante_Click(object sender, EventArgs e)
            {
                BuscarEmpleadoPorNumero();
            }

            protected void BotonBuscarSolicitante_Click(object sender, EventArgs e)
            {
                ControlBuscarEmpleado.InicioControl((Int16)ConstantePrograma.TipoBusquedaEmpleado.Solicitante);
            }

            protected void FechaMovimiento_Validate(object source, ServerValidateEventArgs args)
            {
                string strStartDate = string.Empty;
                DateTime dtStartDate;

                strStartDate = FechaMovimiento.Text.Trim();

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
                    BaseProcesoNegocio.ValidarPermiso((Int16)ConstantePrograma.Paginas.EntradasSalidas);

                    
                    SeleccionarTipoServicio();
                    SeleccionarCondicion();
                    SeleccionarProveedor();
                    SeleccionarTextoError();
                    VaciarTemporalMovimientos();
                    TablaActivo.DataSource = null;
                    TablaActivo.DataBind();
                }
            }

            protected void AgregarActivo() 
            {
                
                
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
                UsuarioEntidad UsuarioEntidadActual = new UsuarioEntidad();



                bool EsPadre = false;
                int TipoDeMovimiento = Int16.Parse(TipoMovimiento.SelectedValue.ToString());
                if (NumeroEmpleadoHiddden.Value == NumeroEmpleado.Text || NumeroEmpleadoHiddden.Value == "")
                {
                    NumeroEmpleadoHiddden.Value = NumeroEmpleado.Text;

                    UsuarioEntidadActual = (UsuarioEntidad)Session["UsuarioEntidad"];
                    ActivoObjetoEntidad.UsuarioId = UsuarioEntidadActual.UsuarioId;
                    ActivoObjetoEntidad.UsuarioIdModifico = UsuarioEntidadActual.UsuarioId;
                    ActivoObjetoEntidad.SesionId = UsuarioEntidadActual.SesionId;
                    ActivoObjetoEntidad.FechaMovimiento = FormatoFecha.AsignarFormato(FechaMovimiento.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);


                    if (Int16.Parse(TipoMovimiento.SelectedValue.ToString()) == 1)
                    {
                        ActivoObjetoEntidad.TipoDeMovimiento = (int)ConstantePrograma.TipoMovimiento.Entrada;

                    }
                    else if (Int16.Parse(TipoMovimiento.SelectedValue.ToString()) == 2)
                    {
                        ActivoObjetoEntidad.TipoDeMovimiento = (int)ConstantePrograma.TipoMovimiento.Salida;
                    }

                    ActivoObjetoEntidad.DescripcionMovimiento = Observaciones.Text.Trim();
                    ActivoObjetoEntidad.CondicionId = Int16.Parse(CondicionId.SelectedValue.ToString());
                    ActivoObjetoEntidad.EmpleadoId = Int16.Parse(EmpleadoIdHidden.Value.ToString());
                    ActivoObjetoEntidad.CodigoBarrasParticular = CodigoBarrasParticular.Text.Trim();
                    ActivoObjetoEntidad.ActivoId = Int16.Parse(ActivoIdHidden.Value.ToString());
                    ActivoObjetoEntidad.EstatusId = (int)ConstantePrograma.EstatusActivos.Asignado;
                    ActivoObjetoEntidad.EmpleadoAutorizoId = Int16.Parse(EmpleadoAutorizoIdHidden.Value.ToString());
                    // se asigna tipo baja=1, para que pase la validación posterior
                    ActivoObjetoEntidad.TipoBaja = "1";


                    if (ActivoPadreHidden.Value != "0")
                    {
                        ActivoPadreHidden.Value = "0";
                        EsPadre = true;
                    }

                    MostrarNombreADarBaja();

                    Resultado = ActivoProcesoObjeto.InsertarMovimientoTemporal(ActivoObjetoEntidad, EsPadre);

                    if (Resultado.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoYaSeleccionado)
                    {

                        EtiquetaMensaje.Text = TextoError.ActivoYaSeleccionado;

                    }
                    else




                        //CON INNER JOIN
                        Resultado = ActivoProcesoObjeto.SeleccionarActivoTemporal(ActivoObjetoEntidad);
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    {
                        EtiquetaMensaje.Text = TextoError.ActivoNoEncontrado;
                    }
                    else
                    {
                        TablaActivo.DataSource = Resultado.ResultadoDatos;
                        TablaActivo.DataBind();
                        BotonBuscarSolicitante.Enabled = false;
                        LimpiarCampos();
                        EtiquetaMensaje.Text = "";
                        TipoMovimiento.SelectedValue = TipoDeMovimiento.ToString();
                        TipoMovimiento.Enabled = false;
                        MostrarCamposProveedor();

                    }

                }
                else 
                {
                    LimpiarCampos();
                    EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                    EtiquetaMensaje.Text = TextoError.EmpleadoDiferente;
                }
            }

            protected ResultadoEntidad BuscarActivo()
            {

                ResultadoEntidad ResultadoEntidad = new ResultadoEntidad();
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();
                EmpleadoEntidad EmpleadoObjetoEntidad = new EmpleadoEntidad();
                AccesorioEntidad AccesorioEntidadObjeto = new AccesorioEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];
                bool EsAccesorio = false;
                bool EsActivoPadre = false;
                int Salida = 0;

                ActivoObjetoEntidad.CodigoBarrasParticular = CodigoBarrasParticular.Text.Trim();
                ActivoObjetoEntidad.TipoActivoId = ObtenerTipoActivoId();

                if (ActivoVehiculoHidden.Value == ((Int16)ConstantePrograma.TipoAtivo.Vehiculo).ToString())
                {
                    ResultadoEntidad.ErrorId = (int)ConstantePrograma.EntradasSalidas.ActivoEsVehiculo;
                    return ResultadoEntidad;
                }
                TipoMovimiento.Enabled = false;
                if (Int16.Parse(TipoMovimiento.SelectedValue.ToString()) == 1)
                {
                    ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Entrada;
                    ResultadoEntidad = ActivoProcesoObjeto.ValidarExistenciaActivoEntrada(ActivoObjetoEntidad, UsuarioSessionEntidad.RolId);
                    TipoMovimiento.Enabled = false;

                }
                else if (Int16.Parse(TipoMovimiento.SelectedValue.ToString()) == 2)
                {
                    ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Salida;
                    
                    ResultadoEntidad = ActivoProcesoObjeto.ValidarExistenciaActivoSalida(ActivoObjetoEntidad, UsuarioSessionEntidad.RolId);
                }



                if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoDadoDeBaja || ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoNoEncontrado
                    || ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoConEstatusSalida || ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.PermisoDenegado)
                {
                    return ResultadoEntidad;
                }
                else
                    if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoNoAsignado)
                    {
                        return ResultadoEntidad;
                    }

                    else if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.EntradasSalidas.ActivoValidoParaSalida || ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoPadre || ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoEsAccresorioAsignado)
                    {
                        VehiculoPadreHidden.Value = ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();

                        if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoPadre)
                        {
                            
                            EsActivoPadre = true;
                        }

                        if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.BajaActivo.ActivoEsAccresorioAsignado)
                        {
                            ActivoIdHidden.Value = ResultadoEntidad.NuevoRegistroId.ToString();
                            ActivoVehiculoHidden.Value = ((Int16)ConstantePrograma.TipoAtivo.Vehiculo).ToString(); 
                            EsAccesorio = true;
                        }
                        else ActivoIdHidden.Value = ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();
                        //if(ResultadoEntidad.NuevoRegistroId==(Int16)ConstantePrograma.TipoAtivo.Vehiculo)
                        //{
                        //    ActivoVehiculoHidden.Value = ((Int16)ConstantePrograma.TipoAtivo.Vehiculo).ToString(); 
                       // }

                        
                        
                        EmpleadoObjetoEntidad.EmpleadoId = Int16.Parse(ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["EmpleadoResguardoId"].ToString());
                        EmpleadoIdHidden.Value = ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["EmpleadoResguardoId"].ToString();
                        Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoObjetoEntidad);


                        NumeroEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                        NombreEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString() + " " + Resultado.ResultadoDatos.Tables[0].Rows[0]["ApellidoPaterno"].ToString();
                        ResultadoEntidad.ErrorId = (int)ConstantePrograma.EntradasSalidas.ActivoValidoParaSalida;
                        if (EsActivoPadre == true)
                        {
                            ResultadoEntidad.DescripcionError = TextoError.ActivoPadre;
                            ResultadoEntidad.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoPadre;

                            AccesorioEntidadObjeto.ActivoId = Int16.Parse(ActivoIdHidden.Value.ToString());
                            //esto abre el control para aceptar darle salida a los activos hijos
                            ControlBuscarAccesorioPadre.InicioControl(AccesorioEntidadObjeto);
                            ActualizarTablaEntradaSalida.Update();
                            return ResultadoEntidad;
                        } if (EsAccesorio == true)
                        {
                            ResultadoEntidad.DescripcionError = TextoError.ActivoEsAccresorioAsignado;
                            ResultadoEntidad.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoEsAccresorioAsignado;

                            AccesorioEntidadObjeto.ActivoId = Int16.Parse(ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString());

                            //esto abre el control para aceptar darle salida a los activos hijos
                            if(Int16.Parse(TipoMovimiento.SelectedValue)==2)
                                Salida=3;
                            if (Salida == (Int16)ConstantePrograma.TipoMovimiento.Salida)
                            {
                                ControlBuscarAccesorioHijo.InicioControl(AccesorioEntidadObjeto);
                                ActualizarTablaEntradaSalida.Update();
                            }
                            else UsarActivoPadre();
                            return ResultadoEntidad;
                        }


                        return ResultadoEntidad;
                    }
                    else if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.EntradasSalidas.ActivoValidoParaEntrada || ResultadoEntidad.ErrorId == (int)ConstantePrograma.EntradasSalidas.ActivoPadreValidoParaEntrada)
                    {

                        EmpleadoObjetoEntidad.EmpleadoId = Int16.Parse(ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["EmpleadoResguardoId"].ToString());
                        EmpleadoIdHidden.Value = ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["EmpleadoResguardoId"].ToString();
                        Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoObjetoEntidad);


                        NumeroEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                        NombreEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString() + " " + Resultado.ResultadoDatos.Tables[0].Rows[0]["ApellidoPaterno"].ToString();

                        if (ResultadoEntidad.ErrorId == (int)ConstantePrograma.EntradasSalidas.ActivoPadreValidoParaEntrada)
                        {
                            EsActivoPadre = true;
                            ActivoPadreHidden.Value = "1";
                            ResultadoEntidad.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoPadre;
                        }
                        ActivoIdHidden.Value = ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();

                        EmpleadoIdHidden.Value = ResultadoEntidad.ResultadoDatos.Tables[0].Rows[0]["EmpleadoResguardoId"].ToString();

                        return ResultadoEntidad;
                    }
                    else return ResultadoEntidad;


            }

            protected void BuscarEmpleado(EmpleadoEntidad EmpleadoEntidadObjeto)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                    {
                        if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString()) != (Int16)ConstantePrograma.EstatusEmpleados.Activo)
                        {
                            EmpleadoAutorizoIdHidden.Value = "0";
                            EtiquetaMensaje.Text = TextoError.EmpleadoInactivo;
                            EmpleadoAutorizo.Focus();
                        }
                        else
                        {

                            EmpleadoAutorizoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                            EmpleadoAutorizo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                            //EmpleadoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();

                            EtiquetaMensaje.Text = "";
                        }
                    }
                    else
                    {
                        EmpleadoAutorizoIdHidden.Value = "0";
                        EtiquetaMensaje.Text = TextoError.EmpleadoNoEncontrado;
                        EmpleadoAutorizo.Focus();
                    }
                }
                else
                {
                    EmpleadoAutorizoIdHidden.Value = "0";
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            protected void BuscarEmpleadoPorNumero()
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.NumeroEmpleado = EmpleadoAutorizo.Text.Trim();

                BuscarEmpleado(EmpleadoEntidadObjeto);


            }

            public void BuscarSolicitantePorId(Int16 EmpleadoId)
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();

                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoId;

                BuscarEmpleado(EmpleadoEntidadObjeto);

                ActualizarTablaEntradaSalida.Update();
            }

            public void CancelarOperacion() 
            {
                ActivoVehiculoHidden.Value = "0";
                LimpiarCampos();
                LlenarTabla();
                ActualizarTablaEntradaSalida.Update();
            }

            protected ResultadoEntidad ComprobarTablaVacia() 
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
                UsuarioEntidad UsuarioEntidadActual = new UsuarioEntidad();

                UsuarioEntidadActual = (UsuarioEntidad)Session["UsuarioEntidad"];
                ActivoEntidadObjeto.SesionId = UsuarioEntidadActual.SesionId;
                if (Int16.Parse(TipoMovimiento.SelectedValue.ToString()) == 1)
                {
                    ActivoEntidadObjeto.TipoDeMovimiento = (int)ConstantePrograma.TipoMovimiento.Entrada;

                }
                else if (Int16.Parse(TipoMovimiento.SelectedValue.ToString()) == 2)
                {
                    ActivoEntidadObjeto.TipoDeMovimiento = (int)ConstantePrograma.TipoMovimiento.Salida;
                }

                Resultado = ActivoProcesoObjeto.SeleccionarActivoTemporal(ActivoEntidadObjeto);
                return Resultado;

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
                    EtiquetaMensaje.Text = TextoError.ElementoEliminado;
                }
                else

                    EtiquetaMensaje.Text = Resultado.DescripcionError;
            }

            public void EsActivoPadre()
            {
                ActivoPadreHidden.Value = "1";
                ActualizarTablaEntradaSalida.Update();
            }

            protected void GuardarEntradasSalidas()
            {
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
                UsuarioEntidad UsuarioEntidadActual = new UsuarioEntidad();
                string EmpleadoAutorizo = string.Empty;
                Int16 TipoActivo = 0;

                UsuarioEntidadActual = (UsuarioEntidad)Session["UsuarioEntidad"];
                ActivoObjetoEntidad.SesionId = UsuarioEntidadActual.SesionId;
                ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Salida;
                


                //CON INNER JOIN    
                Resultado = ActivoProcesoObjeto.SeleccionarActivoTemporal(ActivoObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                {

                    EtiquetaMensaje.Text = TextoError.ActivoNoEncontrado;


                }
                else
                {

                    ActivoObjetoEntidad.TipoServicioId = Int16.Parse(TipoServicio.SelectedValue);
                    ActivoObjetoEntidad.ProveedorId = int.Parse(ProveedorId.SelectedValue);
                    Resultado = ActivoProcesoObjeto.InsertarMovimientoEntradaSalida(ActivoObjetoEntidad);
                    if (Resultado.ErrorId == (int)ConstantePrograma.EntradasSalidas.MovimientoCorrecto)
                    {
                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseInformacion;
                        EtiquetaMensaje.Text = TextoError.MovimientoRegistradoCorrectamente;
                        LlenarTabla();
                        EmpleadoAutorizo = EmpleadoAutorizoIdHidden.Value.ToString();
                        BotonAgregarActivo.Enabled = false;
                        BotonGuardarActivo.Enabled = false;
                        if (ActivoObjetoEntidad.TipoDeMovimiento == (Int16)ConstantePrograma.TipoMovimiento.Salida)
                        BotonImprimir.Enabled = true;
                        ProveedorHidden.Value = ProveedorId.SelectedValue;
                        TipoServicioIdHidden.Value = TipoServicio.SelectedValue;

                        if(ActivoObjetoEntidad.TipoDeMovimiento==(Int16)ConstantePrograma.TipoMovimiento.Salida)
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirEntradasSalidas('" + EmpleadoAutorizo + "','" + ActivoVehiculoHidden.Value + "','" + VehiculoPadreHidden.Value + "','" + ProveedorHidden.Value + "','" + TipoServicioIdHidden.Value + "')", true);

                    }
                    else
                    {
                        EtiquetaMensaje.Text = TextoError.ErrorAlGuardadMovimiento;
                    }



                }
            }

            protected void ImprimirFormato()
            {
                string EmpleadoAutorizo = string.Empty;
                EmpleadoAutorizo = EmpleadoAutorizoIdHidden.Value.ToString();
                ProveedorHidden.Value = ProveedorId.SelectedValue;
                TipoServicioIdHidden.Value = TipoServicio.SelectedValue;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Guid.NewGuid().ToString()", "ImprimirEntradasSalidas('" + EmpleadoAutorizo + "','" + ActivoVehiculoHidden.Value + "','" + VehiculoPadreHidden.Value + "','" + ProveedorHidden.Value + "','" + TipoServicioIdHidden.Value + "')", true);
            }

            protected void LimpiarCampos()
            {
                if (ComprobarTablaVacia().ResultadoDatos.Tables[0].Rows.Count == 0)
                {

                    TipoMovimiento.Enabled = true;
                    TipoMovimiento.SelectedValue = "2";
                    CondicionId.SelectedIndex = 0;
                    NumeroEmpleado.Text = "";
                    NombreEmpleado.Text = "";
                    Observaciones.Text = "";
                    BotonImprimir.Enabled = false;
                    CodigoBarrasParticular.Text = "";
                    EmpleadoIdHidden.Value = "";
                    ActivoIdHidden.Value = "";
                    MostrarCamposProveedor();
                }
                else 
                {
                    FechaMovimiento.Text = "";
                    Observaciones.Text = "";
                    NumeroEmpleado.Text = "";
                    NombreEmpleado.Text = "";
                    CodigoBarrasParticular.Text = "";
                    ActivoIdHidden.Value = "";
                    MostrarCamposProveedor();
                }
            }

            public void LimpiarFormulario()
            {
                LimpiarCampos();
            }

            protected void NumeroEmpleadoTextoModificado()
            {
                if (EmpleadoAutorizo.Text.Trim() != "")
                {
                    BuscarEmpleadoPorNumero();
                }
                else
                {
                    EmpleadoIdHidden.Value = "0";
                    EmpleadoAutorizoIdHidden.Value = "0";

                }
            }

            protected void LlenarTabla()
            {
                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
                UsuarioEntidad UsuarioEntidadActual = new UsuarioEntidad();

                UsuarioEntidadActual = (UsuarioEntidad)Session["UsuarioEntidad"];
                ActivoObjetoEntidad.SesionId = UsuarioEntidadActual.SesionId;
                ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Salida;

                //CON INNER JOIN    
                Resultado = ActivoProcesoObjeto.SeleccionarActivoTemporal(ActivoObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    EtiquetaMensaje.CssClass = ConstantePrograma.ClaseInformacion;
                    EtiquetaMensaje.Text = TextoError.ListaVacia;
                    BotonAgregarActivo.Enabled = true;
                    BotonBuscarSolicitante.Enabled = true;
                    EmpleadoAutorizo.Text = "";
                    TablaActivo.DataBind();
                    EmpleadoNombreBaja.Text = "";
                    TipoMovimiento.Enabled = true;
                    TipoMovimiento.SelectedValue = "2";
                    ActivoVehiculoHidden.Value = "0";
                    NumeroEmpleadoHiddden.Value = "";

                }
                else
                {
                    TablaActivo.DataSource = Resultado.ResultadoDatos;
                    TablaActivo.DataBind();
                    if (ActivoVehiculoHidden.Value == ConstantePrograma.TipoAtivo.Vehiculo.ToString()) 
                    {
                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                        EtiquetaMensaje.Text = TextoError.ActivoEsVehiculo;
                    }

                }
            }

            protected void MostrarCamposProveedor() 
            {
                if (TipoMovimiento.SelectedValue == "2")
                {
                    RenglonTipoServicio.Visible = true;
                    RenglonProveedor.Visible = true;
                    SeleccionarServicioEtiqueta.Visible = true;
                    SeleccionarProveedorEtiqueta.Visible = true;
                    TipoServicio.Enabled = true;
                    ProveedorId.Enabled = true;
                    TipoServicio.Visible = true;
                    ProveedorId.Visible = true;
                    BotonGuardarActivo.ValidationGroup = "GuardarMovimientos";
                }
                else if (TipoMovimiento.SelectedValue == "1")
                {
                    
                    RenglonTipoServicio.Visible = false;
                    RenglonProveedor.Visible = false;
                    SeleccionarServicioEtiqueta.Visible = false;
                    SeleccionarProveedorEtiqueta.Visible = false;
                    TipoServicio.Enabled = false;
                    ProveedorId.Enabled = false;
                    TipoServicio.Visible = false;
                    ProveedorId.Visible = false;
                    BotonGuardarActivo.ValidationGroup = "";
                }
            }

            protected void MostrarNombreADarBaja()
            {
                if (EmpleadoNombreBaja.Text == "")
                {
                    EmpleadoNombreBaja.Text = NombreEmpleado.Text;
                }
            }

            protected void LocalizarActivo()
            {

                switch (BuscarActivo().ErrorId)
                {

                    case (int)ConstantePrograma.BajaActivo.ActivoNoEncontrado:
                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                        EtiquetaMensaje.Text = TextoError.ActivoNoEncontrado;
                        LimpiarCampos();
                        break;

                    case (int)ConstantePrograma.BajaActivo.ActivoDadoDeBaja:
                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                        EtiquetaMensaje.Text = TextoError.ActivoDadoDeBaja;
                        LimpiarCampos();
                        break;

                    case (int)ConstantePrograma.BajaActivo.ActivoNoAsignado:
                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                        EtiquetaMensaje.Text = TextoError.ActivoNoAsignado;
                        LimpiarCampos();
                        break;

                    case (int)ConstantePrograma.BajaActivo.ActivoConEstatusSalida:
                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                        EtiquetaMensaje.Text = TextoError.ActivoConEstatusSalida;
                        LimpiarCampos();
                        break;
                    case (int)ConstantePrograma.EntradasSalidas.ActivoValidoParaSalida:

                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseInformacion;
                        EtiquetaMensaje.Text = TextoError.ActivoValidoParaSalida;

                        break;
                    case (int)ConstantePrograma.EntradasSalidas.ActivoValidoParaEntrada:

                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseInformacion;
                        EtiquetaMensaje.Text = TextoError.ActivoValidoParaEntrada;

                        break;
                    case (int)ConstantePrograma.EntradasSalidas.ActivoNoValidoParaEntrada:
                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                        EtiquetaMensaje.Text = TextoError.ActivoNoValidoParaEntrada;
                        LimpiarCampos();
                        break;
                    case (int)ConstantePrograma.BajaActivo.ActivoPadre:
                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseInformacion;
                        EtiquetaMensaje.Text = TextoError.ActivoEsPadre;
                        break;
                    case (int)ConstantePrograma.BajaActivo.ActivoEsAccresorioAsignado:
                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseInformacion;
                        EtiquetaMensaje.Text = TextoError.ActivoAccesorio;
                        break;
                    case (int)ConstantePrograma.BajaActivo.PermisoDenegado:
                        LlenarTabla();
                        LimpiarCampos();
                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                        EtiquetaMensaje.Text = TextoError.PermisoDenegado;
                        break;
                    case (int)ConstantePrograma.EntradasSalidas.ActivoEsVehiculo:
                        EtiquetaMensaje.CssClass = ConstantePrograma.ClaseError;
                        EtiquetaMensaje.Text = TextoError.ActivoEsVehiculo;
                        break;
                    default:
                        EtiquetaMensaje.Text = "";

                        break;
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

            public void UsarActivoPadre() 
            {
                AccesorioEntidad AccesorioEntidadObjeto = new AccesorioEntidad();
                AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
                
                ResultadoEntidad Resultado = new ResultadoEntidad();
                AccesorioEntidadObjeto.ActivoAccesorioId = Int16.Parse(ActivoIdHidden.Value.ToString());

                Resultado = AccesorioProcesoObjeto.SeleccionarAccesorio(AccesorioEntidadObjeto);

                ActivoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString();
                ActivoPadreHidden.Value = "1";
                ActualizarTablaEntradaSalida.Update();
               
            }          
          
            protected void SeleccionarTextoError()
            {
                FechaMovimientoRequerido.ErrorMessage = TextoError.CampoFechaVacio + "<br />";
                TipoMovimientoRequerido.ErrorMessage = TextoError.TipoMovimientoNoSeleccionado + "<br />";
                ObservacionesRequerido.ErrorMessage = TextoError.ObservacionesRequerido + "<br />";
                CondicionIdRequerido.ErrorMessage = TextoError.CondicionIdRequerida + "<br />";
                CodigoBarrasRequerido.ErrorMessage = TextoError.AsignacionCodigoBarrasParticular + "<br />";
                TipoServicioRequerido.ErrorMessage = TextoError.TipoServicioRequerido + "<br/>";
                ProveedorRequerido.ErrorMessage = TextoError.ProveedorRequerido + "<br/>";
                CodigoBarrasObligatorio.ErrorMessage = TextoError.AsignacionCodigoBarrasParticular + "<br />";
                //EmpleadoRequerido.ErrorMessage = TextoError.EmpleadoRequerido + "<br />";

                
                

            }

            protected void SeleccionarTemporalActivo()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalActivoEntidad TemporalActivoObjetoEntidad = new TemporalActivoEntidad();
                TemporalActivoProceso TemporalActivoProcesoNegocio = new TemporalActivoProceso();

                TemporalActivoObjetoEntidad.TemporalCompraId = int.Parse(TemporalMovimientoIdHidden.Value);

                Resultado = TemporalActivoProcesoNegocio.SeleccionarTemporalActivo(TemporalActivoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaActivo.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaActivo.CssClass = ConstantePrograma.ClaseTabla;

                    //SumaMonto = 0;

                    TablaActivo.DataSource = Resultado.ResultadoDatos;
                    TablaActivo.DataBind();

                    //MontoTotalHidden.Value = SumaMonto.ToString();
                    //LabelMontoTotal.Text = string.Format("{0:C}", SumaMonto);
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
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

            protected void SeleccionarTipoServicio()
            {

                ResultadoEntidad Resultado = new ResultadoEntidad();
                TipoServicioEntidad TipoServicioEntidadObjeto = new TipoServicioEntidad();
                TipoServicioProceso TipoServicioProcesoObjeto = new TipoServicioProceso();


                Resultado = TipoServicioProcesoObjeto.SeleccionarTipoServicio(TipoServicioEntidadObjeto);
                TipoServicio.DataValueField = "TipoServicioId";
                TipoServicio.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    TipoServicio.DataSource = Resultado.ResultadoDatos;
                    TipoServicio.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }

                TipoServicio.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
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
                    MovimientoId = int.Parse(TablaActivo.DataKeys[intFila]["MovimientoId"].ToString());
                    EliminarActivo(MovimientoId);
                    LlenarTabla();

                }

            }

            protected void VaciarTemporalMovimientos()
            {

                ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
                UsuarioEntidad UsuarioEntidadActual = new UsuarioEntidad();
                ActivoProceso ActivoProcesoObjeto = new ActivoProceso();


                UsuarioEntidadActual = (UsuarioEntidad)Session["UsuarioEntidad"];
                ActivoObjetoEntidad.SesionId = UsuarioEntidadActual.SesionId;
                ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Salida;

                ActivoProcesoObjeto.EliminarRegistrosTemporales(ActivoObjetoEntidad);

            }

        #endregion
    }
}
