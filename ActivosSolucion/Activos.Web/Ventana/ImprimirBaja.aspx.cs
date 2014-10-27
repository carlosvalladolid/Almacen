using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

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
    public partial class ImprimirBaja : System.Web.UI.Page
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
            string EmpleadoAsignado = string.Empty;
            EmpleadoAsignado = Request.QueryString["Emp"];
            LlenarFormularioBaja(EmpleadoAsignado);
            MostrarTitulo();
            
            
            
        }

        protected void MostrarTitulo() 
        {

            Master.MostrarTitulo("RESGUARDO DE BAJA","DIRECCIÓN ADMINISTRATIVA");
        }

        protected void TablaBaja_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TablaBajaEliminarSeleccion(e);
        }

        protected void TablaBaja_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        protected void LlenarFormularioBaja(string EmpleadoAsignado) 
        {
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            UsuarioEntidad UsuarioEntidadActual = new UsuarioEntidad();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
            EmpleadoEntidad EmpleadoObjetoEntidad = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();
            string FechaActual = string.Empty;
            UsuarioEntidadActual = (UsuarioEntidad)Session["UsuarioEntidad"];
           
            LlenarTabla();
            
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
            CampoRFC.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["RFC"].ToString();
            CampoDomicilio.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["DomicilioCompleto"].ToString();
            if (Resultado.ResultadoDatos.Tables[0].Rows[0]["TelefonoCasa"].ToString()=="")
            CampoTelefono.Text = "Cel.  "+ Resultado.ResultadoDatos.Tables[0].Rows[0]["Celular"].ToString();
            else if (Resultado.ResultadoDatos.Tables[0].Rows[0]["Celular"].ToString()=="")
                CampoTelefono.Text = "Tel.  " + Resultado.ResultadoDatos.Tables[0].Rows[0]["TelefonoCasa"].ToString();
            else
            CampoTelefono.Text = "Tel. " + Resultado.ResultadoDatos.Tables[0].Rows[0]["TelefonoCasa"].ToString() + "     Cel. " + Resultado.ResultadoDatos.Tables[0].Rows[0]["Celular"].ToString();

            //con esta información se llenarán los nombres de las firmas de autorización
            
            
            Adquisiciones.Text = ConfigurationManager.AppSettings["Activos.Web.AdquisicionesYServicios"].ToString();
            Usuario.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
            DirectorAdministrativo.Text = ConfigurationManager.AppSettings["Activos.Web.DirectorAdministrativo"].ToString();

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
               // EtiquetaMensaje.CssClass = ConstantePrograma.ClaseInformacion;
                //EtiquetaMensaje.Text = TextoError.ListaVacia;
                //NumeroEmpleadoHiddden.Value = "";
                //EmpleadoNombreBaja.Text = "";
                //TablaBaja.DataBind();
            }
            else
            {
                TablaBaja.DataSource = Resultado.ResultadoDatos;
                TablaBaja.DataBind();
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
