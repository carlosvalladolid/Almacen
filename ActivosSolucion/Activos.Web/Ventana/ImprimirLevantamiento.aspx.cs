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
    public partial class ImprimirLevantamiento : System.Web.UI.Page
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
                    TablaLevantamiento.DataSource = null;
                    TablaLevantamiento.DataBind();

                    int LevantamientoID = 0;
                    LevantamientoID = int.Parse(Request.QueryString["LevantamientoID"]);

                    SeleccionarLevantamiento(LevantamientoID);
                    MostrarTitulo();
                }
            }

            protected void MostrarTitulo()
            {
                Master.MostrarTitulo("RESGUARDO DE ACTIVOS", "DIRECCIÓN ADMINISTRATIVA");
            }

            protected void SeleccionarLevantamiento(int LevantamientoID)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                LevantamientoActivoEntidad LevantamientoObjetoEntidad = new LevantamientoActivoEntidad();
                LevantamientoActivoProceso LevantamientoProcesoObjeto = new LevantamientoActivoProceso();

                LevantamientoObjetoEntidad.LevantamientoID = LevantamientoID;

                Resultado = LevantamientoProcesoObjeto.SeleccionarLevantamientoReporte(LevantamientoObjetoEntidad);

                if (Resultado.ErrorId == 0)
                {
                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    {
                        TablaLevantamiento.CssClass = ConstantePrograma.ClaseTablaVacia;
                    }
                    else
                    {
                        TablaLevantamiento.CssClass = ConstantePrograma.ClaseTablaImpresion;
                        LlenarFormulario(Resultado);
                    }

                }
            }

            protected void LlenarFormulario(ResultadoEntidad Resultado)
            {
                Int16 EmpleadoId = 0;
                string FechaActual = string.Empty;

                FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
                FechaLabel.Text = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);

                //Se carga el grid con los activos
                TablaLevantamiento.DataSource = Resultado.ResultadoDatos.Tables[0];
                TablaLevantamiento.DataBind();

                //Se obtiene el ID del Empleado y se consulta
                EmpleadoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString());

                SeleccionarEmpleado(EmpleadoId);
                SeleccionarJefe(EmpleadoId);
            }

            protected void SeleccionarEmpleado(Int16 EmpleadoId)
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                ResultadoEntidad Resultado = new ResultadoEntidad();
                EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoId;

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

            protected void SeleccionarJefe(Int16 EmpleadoId)
            {
                EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
                JefeProceso JefeProcesoNegocio = new JefeProceso();

                EmpleadoEntidadObjeto = JefeProcesoNegocio.SeleccionarTitular(EmpleadoId);

                TitularAreaNombreLabel.Text = EmpleadoEntidadObjeto.Nombre;
            }

        #endregion
    }
}
