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
    public partial class ImprimirLevantamientoCorrecto : System.Web.UI.Page
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
                string FechaActual = string.Empty;

                EmpleadoId = Int16.Parse(Request.QueryString["EmpleadoId"]);

                SeleccionarEmpleado(EmpleadoId);
                SeleccionarAsignacion(EmpleadoId);
                SeleccionarJefe(EmpleadoId);
                MostrarTitulo();

                FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
                FechaLabel.Text = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);

            }
        }

        protected void MostrarTitulo()
        {
            Master.MostrarTitulo("RESGUARDO DE ACTIVOS", "DIRECCIÓN ADMINISTRATIVA");
        }

        protected void SeleccionarAsignacion(Int16 EmpleadoId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();

            ActivoObjetoEntidad.EmpleadoId = EmpleadoId;
            ActivoObjetoEntidad.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;
            ActivoObjetoEntidad.TipoAccesorioId = (Int16)ConstantePrograma.TipoAccesorio.ActivoFijo;

            Resultado = MovimientoProcesoObjeto.SeleccionarMovimientoAsignacionAccesorios(ActivoObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaActivos.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaActivos.CssClass = ConstantePrograma.ClaseTablaImpresion;

                TablaActivos.DataSource = Resultado.ResultadoDatos;
                TablaActivos.DataBind();

                CantidadActivosLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows.Count.ToString();
            }
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
