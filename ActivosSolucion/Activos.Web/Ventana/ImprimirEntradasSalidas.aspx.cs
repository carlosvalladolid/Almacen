using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

using Activos.Comun.Constante;
using Activos.Comun.Fecha;
using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Web.Ventana
{
    public partial class ImprimirEntradasSalidas : System.Web.UI.Page
    {
        #region "Eventos"
        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        #endregion

        #region "Metodos"

        protected void Inicio()
        {
            string EmpleadoAutorizo = string.Empty;
            string TipoActivo = string.Empty;
            string VehiculoPadre = string.Empty;
            string ProveedorId = string.Empty;
            string TipoServicioId = string.Empty;
            EmpleadoAutorizo = Request.QueryString["Emp"];
            TipoActivo = Request.QueryString["TipoActivo"];
            VehiculoPadre = Request.QueryString["VehiculoPadre"];
            ProveedorId = Request.QueryString["ProveedorId"];
            TipoServicioId = Request.QueryString["TipoServicioId"];

            LlenarFormularioImpresion(EmpleadoAutorizo, TipoActivo, VehiculoPadre, ProveedorId, TipoServicioId);
            MostrarTitulo();
            
            
            
        }

        protected void TablaActivo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TablaBajaEliminarSeleccion(e);
        }

        protected void TablaActivo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        protected void LlenarFormularioImpresion(string EmpleadoAsignado, string TipoActivo, string VehiculoPadre, string ProveedorId, string TipoServicioId)
        {
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            ActivoEntidad ActivoVehiculoObjeto = new ActivoEntidad();
            UsuarioEntidad UsuarioEntidadActual = new UsuarioEntidad();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
            EmpleadoEntidad EmpleadoObjetoEntidad = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();
            string FechaActual = string.Empty;
            UsuarioEntidadActual = (UsuarioEntidad)Session["UsuarioEntidad"];

            LlenarTabla();


            if (TipoActivo == ((Int16)ConstantePrograma.TipoAtivo.Vehiculo).ToString()) 
            {
                ActivoVehiculoObjeto.ActivoId = int.Parse(VehiculoPadre);
                PanelAutomovil.Visible = true;
                Resultado = ActivoProcesoObjeto.SeleccionarActivo(ActivoVehiculoObjeto);
                Vehiculo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Descripcion"].ToString();
                NoEconomico.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoBarrasParticular"].ToString();
                SeleccionarPlacas(int.Parse(VehiculoPadre));
            }



            SeleccionarTipoServicio(Int16.Parse(TipoServicioId));
            SeleccionarProveedor(Int16.Parse(ProveedorId));

            FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
            ActivoObjetoEntidad.FechaMovimiento = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);
            FechaMovimiento.Text = ActivoObjetoEntidad.FechaMovimiento.ToString();
            //Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoObjetoEntidad);
            //Departamento.Text = ConfigurationManager.AppSettings["Activos.Web.Secretaria"].ToString();
            //Direccion.Text = ConfigurationManager.AppSettings["Activos.Web.Direccion"].ToString();
            //NumeroEmpleado.Text = ConfigurationManager.AppSettings["Activos.Web.Domicilio"].ToString();
            EmpleadoObjetoEntidad.EmpleadoId = Int16.Parse(EmpleadoAsignado.ToString());
            Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoObjetoEntidad);
            //Entrega.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString() + " " + Resultado.ResultadoDatos.Tables[0].Rows[0]["ApellidoPaterno"].ToString();
            CampoDireccion.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreDireccion"].ToString();
            CampoDepartamento.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreDepartamento"].ToString();
            CampoNoEmpleado.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
            CampoNombre.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
            if (Resultado.ResultadoDatos.Tables[0].Rows[0]["TelefonoCasa"].ToString() == "")
                CampoTelefono.Text = "Cel.  " + Resultado.ResultadoDatos.Tables[0].Rows[0]["Celular"].ToString();
            else if (Resultado.ResultadoDatos.Tables[0].Rows[0]["Celular"].ToString() == "")
                CampoTelefono.Text = "Tel.  " + Resultado.ResultadoDatos.Tables[0].Rows[0]["TelefonoCasa"].ToString();
            else
                CampoTelefono.Text = "Tel. " + Resultado.ResultadoDatos.Tables[0].Rows[0]["TelefonoCasa"].ToString() + "     Cel. " + Resultado.ResultadoDatos.Tables[0].Rows[0]["Celular"].ToString();

            //con esta información se llenarán los nombres de las firmas de autorización


            Adquisiciones.Text = ConfigurationManager.AppSettings["Activos.Web.AdquisicionesYServicios"].ToString();
            DirectorAdministrativo.Text = ConfigurationManager.AppSettings["Activos.Web.DirectorAdministrativo"].ToString();

        }

        protected void SeleccionarTipoServicio(Int16 TipoServicioId)
        {

            ResultadoEntidad Resultado = new ResultadoEntidad();
            TipoServicioEntidad TipoServicioEntidadObjeto = new TipoServicioEntidad();
            TipoServicioProceso TipoServicioProcesoObjeto = new TipoServicioProceso();

            TipoServicioEntidadObjeto.TipoServicioId = TipoServicioId;

            Resultado = TipoServicioProcesoObjeto.SeleccionarTipoServicio(TipoServicioEntidadObjeto);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                TipoServicioEtiqueta.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
            }

        }

        protected void SeleccionarProveedor(Int16 ProveedorId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ProveedorEntidad ProveedorEntidadObjeto = new ProveedorEntidad();
            ProveedorProceso ProveedorProcesoObjeto = new ProveedorProceso();

            ProveedorEntidadObjeto.ProveedorId = ProveedorId;

            Resultado = ProveedorProcesoObjeto.SeleccionarProveedor(ProveedorEntidadObjeto);



            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                Proveedor.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
                TelefonoProveedor.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Telefono"].ToString();
            }
            
        }


        protected void SeleccionarPlacas(int ActivoId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioEntidad AccesorioObjetoEntidad = new AccesorioEntidad();
            AccesorioProceso AccesorioProcesoNegocio = new AccesorioProceso();

            AccesorioObjetoEntidad.ActivoId = ActivoId;

            Resultado = AccesorioProcesoNegocio.SeleccionarAccesorio(AccesorioObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                foreach (DataRow dtRegistro in Resultado.ResultadoDatos.Tables[0].Rows)
                {
                    switch (Int16.Parse(dtRegistro["TipoAccesorioId"].ToString()))
                    {
                        case (Int16)ConstantePrograma.TipoAccesorio.Placas:
                            Placas.Text = dtRegistro["Descripcion"].ToString();
                            break;

                       
                    }
                }



            }
        }

        protected void MostrarTitulo()
        {

            Master.MostrarTitulo("ORDEN DE SALIDA", "DIRECCIÓN ADMINISTRATIVA");
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
               // EtiquetaMensaje.CssClass = ConstantePrograma.ClaseInformacion;
                //EtiquetaMensaje.Text = TextoError.ListaVacia;
                //NumeroEmpleadoHiddden.Value = "";
                //EmpleadoNombreBaja.Text = "";
                //TablaBaja.DataBind();
            }
            else
            {
                TablaActivo.DataSource = Resultado.ResultadoDatos;
                TablaActivo.DataBind();
                PanelFirmas.Visible = true;


                switch (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString()))
                {
                    case (Int16)ConstantePrograma.TipoAtivo.Mobiliario:
                        TitularDelArea.Text = ConfigurationManager.AppSettings["Activos.Web.TitularActivoMobiliario"].ToString();
                        break;

                    case (Int16)ConstantePrograma.TipoAtivo.EquipoComputo:
                        TitularDelArea.Text = ConfigurationManager.AppSettings["Activos.Web.TitularActivoEquipoDeComputo"].ToString();
                        break;

                    case (Int16)ConstantePrograma.TipoAtivo.Vehiculo:
                        TitularDelArea.Text = ConfigurationManager.AppSettings["Activos.Web.TitularActivoVehiculo"].ToString();
                        break;

                    case (Int16)ConstantePrograma.TipoAtivo.OperaciónYMantenimiento:
                        TitularDelArea.Text = ConfigurationManager.AppSettings["Activos.Web.TitularActivoOperacionMantenimiento"].ToString();
                        break;
                }

            }
        }

        #endregion
    }
}

