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
    public partial class ImprimirAsignacionActivo : System.Web.UI.Page
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
                    Int16 EmpleadoId = 0;
                    int TemporalAsignacionId = 0;
                    string FechaActual = string.Empty;

                    EmpleadoId = Int16.Parse(Request.QueryString["EmpleadoId"]);
                    TemporalAsignacionId = int.Parse(Request.QueryString["TemporalAsignacionId"]);

                    SeleccionarEmpleado(EmpleadoId);
                    SeleccionarAsignacion(TemporalAsignacionId);
                    MostrarTitulo();

                    FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
                    FechaLabel.Text = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);

                }
            }

            protected void MostrarTitulo()
            {
                Master.MostrarTitulo("RESGUARDO DE ACTIVOS", "DIRECCIÓN ADMINISTRATIVA");
            }

            protected void SeleccionarEmpleado(Int16 EmpleadoIdDestino)
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoIdDestino;

                Resultado = EmpleadoProcesoNegocio.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    UsuarioNombreLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
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

            protected void SeleccionarAsignacion(int TemporalAsignacionId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad = new TemporalAsignacionEntidad();
                TemporalAsignacionDetalleProceso TemporalAsignacionDetalleProcesoNegocio = new TemporalAsignacionDetalleProceso();

                TemporalAsignacionObjetoEntidad.TemporalAsignacionId = TemporalAsignacionId;

                Resultado = TemporalAsignacionDetalleProcesoNegocio.SeleccionarTemporalAsignacionDetalle(TemporalAsignacionObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    SeleccionarTitularArea(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString()));

                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        TablaActivos.CssClass = ConstantePrograma.ClaseTablaVacia;
                    else
                        TablaActivos.CssClass = ConstantePrograma.ClaseTablaImpresion;

                    TablaActivos.DataSource = Resultado.ResultadoDatos;
                    TablaActivos.DataBind();

                    CantidadActivosLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows.Count.ToString();
                }
            }

            protected void SeleccionarTitularArea(Int16 TipoActivoId)
            {
                switch (TipoActivoId)
                {
                    case (Int16)ConstantePrograma.TipoAtivo.Mobiliario:
                        TitularAreaNombreLabel.Text = ConfigurationManager.AppSettings["Activos.Web.TitularActivoMobiliario"].ToString();
                        break;

                    case (Int16)ConstantePrograma.TipoAtivo.EquipoComputo:
                        TitularAreaNombreLabel.Text = ConfigurationManager.AppSettings["Activos.Web.TitularActivoEquipoDeComputo"].ToString();
                        break;

                    case (Int16)ConstantePrograma.TipoAtivo.Vehiculo:
                        TitularAreaNombreLabel.Text = ConfigurationManager.AppSettings["Activos.Web.TitularActivoVehiculo"].ToString();
                        break;

                    case (Int16)ConstantePrograma.TipoAtivo.OperaciónYMantenimiento:
                        TitularAreaNombreLabel.Text = ConfigurationManager.AppSettings["Activos.Web.TitularActivoOperacionMantenimiento"].ToString();
                        break;
                }
            }

        #endregion
    }
}
