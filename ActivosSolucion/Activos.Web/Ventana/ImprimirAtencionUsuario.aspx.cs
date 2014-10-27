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
using Activos.Entidad.Mantenimiento;
using Activos.ProcesoNegocio.Mantenimiento;


namespace Activos.Web.Ventana
{
    public partial class ImprimirAtencionUsuario : System.Web.UI.Page
    {
        #region Eventos

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
                    int MantenimientoId = 0;
                    string FechaActual = string.Empty;

                    MantenimientoId = int.Parse(Request.QueryString["MantenimientoId"]);
                    MostrarTitulo();
                    FolioId.Text = MantenimientoId.ToString();

                    SeleccionarFolioMantenimiento(MantenimientoId);
                    SeleccionarActivos();
                    SeleccionarEmpleadosMantenimiento(MantenimientoId);

                    FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
                    FechaLabel.Text = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);

                }
            }

            protected void MostrarTitulo()
            {
                Master.MostrarTitulo("INFORMÁTICA", "HOJA DE SERVICIO");
            }

            protected void SeleccionarActivos() 
            {
               
                TemporalMantenimientoActivoEntidad TemporalMantenimientoActivoEntidadObjeto = new TemporalMantenimientoActivoEntidad();
                TemporalMantenimientoActivoProceso TemporalMantenimientoActivoProcesoObjeto = new TemporalMantenimientoActivoProceso();
                UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();
                ResultadoEntidad Resultado = new ResultadoEntidad();

                UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];
                TemporalMantenimientoActivoEntidadObjeto.SesionId = UsuarioSessionEntidad.SesionId;

                Resultado = TemporalMantenimientoActivoProcesoObjeto.SeleccionarTemporalMantenimientoActivo(TemporalMantenimientoActivoEntidadObjeto);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaActivos.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaActivos.CssClass = ConstantePrograma.ClaseTablaImpresion;


                TablaActivos.DataSource = Resultado.ResultadoDatos;
                TablaActivos.DataBind();
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
                    NombreLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                    TelefonoLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["TelefonoCasa"].ToString();
                }
            }

            protected void SeleccionarFolioMantenimiento(int MantenimientoId)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                MantenimientoEntidad MantenimientoEntidadObjeto = new MantenimientoEntidad();
                MantenimientoProceso MantenimientoProcesoNegocio = new MantenimientoProceso();

                MantenimientoEntidadObjeto.MantenimientoId = MantenimientoId;

                Resultado = MantenimientoProcesoNegocio.SeleccionarMantenimiento(MantenimientoEntidadObjeto);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString()) != 0)
                    {
                        SeleccionarEmpleado(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString()));
                    }
                    else 
                    {
                        SeleccionaDepartamento(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["DepartamentoId"].ToString()));
                    }
                }
                
            }

            protected void SeleccionaDepartamento(Int16 DepartamentoId) 
            {
                DepartamentoEntidad DepartamentoEntidadObjeto = new DepartamentoEntidad();
                DepartamentoProceso DepartamentoProcesoObjeto = new DepartamentoProceso();
                ResultadoEntidad Resultado = new ResultadoEntidad();

                DepartamentoEntidadObjeto.DepartamentoId = DepartamentoId;

                Resultado = DepartamentoProcesoObjeto.SeleccionarDepartamento(DepartamentoEntidadObjeto);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0) 
                {
                    DireccionLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreDireccion"].ToString();
                    DepartamentoLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();

                    UsuarioNombreLabel.Text = ConfigurationManager.AppSettings["Activos.Web.TitularAtencionUsuario"].ToString();


                }

            }

            protected void SeleccionarEmpleadosMantenimiento(int MantenimientoId) 
            {

                MantenimientoEmpleadoEntidad MantenimientoEmpleadoEntidadObjeto = new MantenimientoEmpleadoEntidad();
                MantenimientoEmpleadoProceso MantenimientoEmpleadoProcesoObjeto = new MantenimientoEmpleadoProceso();
                ResultadoEntidad Resultado = new ResultadoEntidad();


                MantenimientoEmpleadoEntidadObjeto.MantenimientoId = MantenimientoId;
                Resultado = MantenimientoEmpleadoProcesoObjeto.SeleccionarMantenimientoEmpleado(MantenimientoEmpleadoEntidadObjeto);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaActivos.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaActivos.CssClass = ConstantePrograma.ClaseTablaImpresion;

                TablaEmpleados.DataSource = Resultado.ResultadoDatos;
                TablaEmpleados.DataBind();
            
            }


        #endregion
    }
}
