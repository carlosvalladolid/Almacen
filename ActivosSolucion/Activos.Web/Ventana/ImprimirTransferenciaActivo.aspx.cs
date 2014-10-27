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
    public partial class ImprimirTransferenciaActivo : System.Web.UI.Page
    {
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
                    Int16 EmpleadoIdOrigen = 0;
                    Int16 EmpleadoIdDestino = 0;
                    string FechaActual = string.Empty;

                    EmpleadoIdOrigen = Int16.Parse(Request.QueryString["EmpleadoIdOrigen"]);
                    EmpleadoIdDestino = Int16.Parse(Request.QueryString["EmpleadoIdDestino"]);

                    SeleccionarEmpleadoOrigen(EmpleadoIdOrigen);
                    SeleccionarEmpleadoDestino(EmpleadoIdDestino);
                    BuscarActivos();
                    MostrarTitulo();

                    FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
                    FechaLabel.Text = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);

                    ResponsableNombreLabel.Text = ConfigurationManager.AppSettings["Activos.Web.AdquisicionesYServicios"].ToString();
                }
            }

            protected void MostrarTitulo()
            {
                Master.MostrarTitulo("TRANSFERENCIA DE ACTIVO FIJO", "DIRECCIÓN ADMINISTRATIVA");
            }

            protected void SeleccionarEmpleadoOrigen(Int16 EmpleadoIdOrigen)
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoIdOrigen;

                Resultado = EmpleadoProcesoNegocio.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    EntregaNombreLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                }
            }

            protected void SeleccionarEmpleadoDestino(Int16 EmpleadoIdDestino)
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoIdDestino;

                Resultado = EmpleadoProcesoNegocio.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    RecibeNombreLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                    DireccionLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreDireccion"].ToString();
                    DepartamentoLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreDepartamento"].ToString();
                    NumeroEmpleadoLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                    RFCLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["RFC"].ToString();
                    NombreLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                    DomicilioLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Calle"].ToString()
                        + " No." + Resultado.ResultadoDatos.Tables[0].Rows[0]["Numero"].ToString()
                        + ", " + Resultado.ResultadoDatos.Tables[0].Rows[0]["Colonia"].ToString()
                        + ", " + Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreCiudad"].ToString();
                    TelefonoLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["TelefonoCasa"].ToString();
                }
            }

            protected void BuscarActivos()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoEntidadObjeto = new TemporalTransferenciaActivoEntidad();
                TemporalTransferenciaActivoProceso TemporalTransferenciaActivoProcesoNegocio = new TemporalTransferenciaActivoProceso();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

                TemporalTransferenciaActivoEntidadObjeto.SesionId = UsuarioSessionEntidad.SesionId;

                Resultado = TemporalTransferenciaActivoProcesoNegocio.SeleccionarTemporalTransferenciaActivo(TemporalTransferenciaActivoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaActivos.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaActivos.CssClass = ConstantePrograma.ClaseTablaImpresion;

                    TablaActivos.DataSource = Resultado.ResultadoDatos;
                    TablaActivos.DataBind();
                }
            }

        #endregion
    }
}
