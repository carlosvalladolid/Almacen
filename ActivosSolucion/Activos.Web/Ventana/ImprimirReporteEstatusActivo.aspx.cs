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
    public partial class ImprimirReporteEstatusActivo : System.Web.UI.Page
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
                MostrarTitulo();

                string FechaActual = string.Empty;
                string FamiliaId = string.Empty;
                string FamiliaNombre = string.Empty;
                string SubFamiliaId = string.Empty;
                string SubFamiliaNombre = string.Empty;
                string Asignados = string.Empty;
                string NoAsignados= string.Empty;
                string Salida = string.Empty;
                string TipoServicio = string.Empty;
                string TipoServicioNombre = string.Empty;
                string NoEtiquetado = string.Empty;

                //Se obtienen los datos de los filtros de la pantalla padre

                FamiliaId = Request.Form["ctl00$ContenedorCuerpo$FamiliaIdHidden"].ToString();
                FamiliaNombre = Request.Form["ctl00$ContenedorCuerpo$FamiliaNombreHidden"].ToString();
                SubFamiliaId = Request.Form["ctl00$ContenedorCuerpo$SubFamiliaHidden"].ToString();
                SubFamiliaNombre = Request.Form["ctl00$ContenedorCuerpo$SubFamiliaNombreHidden"].ToString();
                Asignados = Request.Form["ctl00$ContenedorCuerpo$AsignadosHidden"].ToString();
                NoAsignados = Request.Form["ctl00$ContenedorCuerpo$NoAsignadosHidden"].ToString();
                Salida = Request.Form["ctl00$ContenedorCuerpo$SalidaHidden"].ToString();
                TipoServicio = Request.Form["ctl00$ContenedorCuerpo$TipoServicioHidden"].ToString();
                TipoServicioNombre = Request.Form["ctl00$ContenedorCuerpo$TipoServicioNombreHidden"].ToString();
                NoEtiquetado = Request.Form["ctl00$ContenedorCuerpo$NoEtiquetadoHidden"].ToString();


                //Se llenan los datos del encabezado con los filtros
                if (FamiliaNombre == "-- Todos --")
                    FamiliaLabel.Text = "TODAS LAS FAMILIAS";
                else
                FamiliaLabel.Text = FamiliaNombre;
                if (SubFamiliaNombre == "-- Todos --")
                    SubFamiliaLabel.Text = "TODAS LAS SUBFAMILIAS";
                else
                SubFamiliaLabel.Text = SubFamiliaNombre;

                if (Asignados == "1")
                {
                    AsignadosLabel.Text = "SI";
                    LlenarTablaAsignados(FamiliaId, SubFamiliaId);
                }
                if (NoAsignados == "1")
                {
                    NoAsignadosLabel.Text = "SI";
                    LlenarTablaNoAsignados(FamiliaId, SubFamiliaId);
                }
                if (Salida == "1")
                {
                    SalidaLabel.Text = "SI";
                    if (TipoServicioNombre == "-- Todos --")
                        SalidaLabel.Text = SalidaLabel.Text + ", TODOS";
                    else SalidaLabel.Text = SalidaLabel.Text +", " +TipoServicioNombre;
                    LlenarTablaSalida(FamiliaId, SubFamiliaId, TipoServicio);

                }

                if (NoEtiquetado == "1")
                {
                    NoEtiquetadosLabel.Text = "SI";
                    LlenarTablaNoEtiquetado(FamiliaId, SubFamiliaId);
                }


                FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
                FechaLabel.Text = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);
            }
        }

        protected void MostrarTitulo()
        {

            Master.MostrarTitulo("ESTATUS DE ACTIVOS", "REPORTE");
        }

        protected void LlenarTablaAsignados(string FamiliaId, string SubFamiliaId) 
        {
            ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

            ActivoEntidadObjeto.FamiliaId = Int16.Parse(FamiliaId);
            ActivoEntidadObjeto.SubFamiliaId= Int16.Parse(SubFamiliaId);
            ActivoEntidadObjeto.TipoActivoId = ObtenerTipoActivoId();
            ActivoEntidadObjeto.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;
            ActivoEntidadObjeto.AlmacenistaEmpleadoId = Int16.Parse(ConfigurationManager.AppSettings["Activos.Web.AlmacenistaEmpleadoId"].ToString());


            LlenarTablaAsignados(ActivoEntidadObjeto);

                
        }

        protected void LlenarTablaAsignados(ActivoEntidad ActivoEntidadObjeto)
        {
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            

            Resultado = MovimientoProcesoObjeto.SeleccionarMovimientoReporteActivoEstatusAsignado(ActivoEntidadObjeto);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaAsignados.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaAsignados.CssClass = ConstantePrograma.ClaseTablaImpresion;

                DivTablaAsignados.Visible = true;
                TablaAsignados.DataSource = Resultado.ResultadoDatos;
                TablaAsignados.DataBind();
            }


        }

        protected void LlenarTablaNoAsignados(string FamiliaId, string SubFamiliaId)
        {
            ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

            ActivoEntidadObjeto.FamiliaId = Int16.Parse(FamiliaId);
            ActivoEntidadObjeto.SubFamiliaId = Int16.Parse(SubFamiliaId);
            ActivoEntidadObjeto.TipoActivoId = ObtenerTipoActivoId();
            ActivoEntidadObjeto.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Asignacion;
            ActivoEntidadObjeto.AlmacenistaEmpleadoId = Int16.Parse(ConfigurationManager.AppSettings["Activos.Web.AlmacenistaEmpleadoId"].ToString());

            LlenarTablaNoAsignados(ActivoEntidadObjeto);
        }

        protected void LlenarTablaNoAsignados(ActivoEntidad ActivoEntidadObjeto)
        {
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            Resultado = ActivoProcesoObjeto.SeleccionarActivoReporteEstatusSinAsignar(ActivoEntidadObjeto);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaNoAsignados.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaNoAsignados.CssClass = ConstantePrograma.ClaseTablaImpresion;

                DivTablaNoAsignados.Visible = true;
                TablaNoAsignados.DataSource = Resultado.ResultadoDatos;
                TablaNoAsignados.DataBind();
            }


        }

        protected void LlenarTablaNoEtiquetado(string FamiliaId, string SubFamiliaId) 
        {
            ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

            ActivoEntidadObjeto.FamiliaId = Int16.Parse(FamiliaId);
            ActivoEntidadObjeto.SubFamiliaId = Int16.Parse(SubFamiliaId);
            ActivoEntidadObjeto.TipoActivoId = ObtenerTipoActivoId();
            ActivoEntidadObjeto.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Alta;
            ActivoEntidadObjeto.AlmacenistaEmpleadoId = Int16.Parse(ConfigurationManager.AppSettings["Activos.Web.AlmacenistaEmpleadoId"].ToString());

            LlenarTablaNoEtiquetado(ActivoEntidadObjeto);
        }

        protected void LlenarTablaNoEtiquetado(ActivoEntidad ActivoEntidadObjeto)
        {
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            Resultado = ActivoProcesoObjeto.SeleccionarActivoReporteEstatusSinEtiquetar(ActivoEntidadObjeto);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaNoEtiquetados.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaNoEtiquetados.CssClass = ConstantePrograma.ClaseTablaImpresion;

                DivTablaNoEtiquetados.Visible = true;
                TablaNoEtiquetados.DataSource = Resultado.ResultadoDatos;
                TablaNoEtiquetados.DataBind();
            }


        }

        protected void LlenarTablaSalida(string FamiliaId, string SubFamiliaId, string TipoServicio) 
        {
            ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();

            ActivoEntidadObjeto.FamiliaId = Int16.Parse(FamiliaId);
            ActivoEntidadObjeto.SubFamiliaId = Int16.Parse(SubFamiliaId);
            ActivoEntidadObjeto.TipoActivoId = ObtenerTipoActivoId();
            ActivoEntidadObjeto.TipoDeMovimiento = (Int16)ConstantePrograma.TipoMovimiento.Salida;
            ActivoEntidadObjeto.TipoServicioId = Int16.Parse(TipoServicio);


            LlenarTablaSalida(ActivoEntidadObjeto);
        }

        protected void LlenarTablaSalida(ActivoEntidad ActivoEntidadObjeto)
        {
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            Resultado = MovimientoProcesoObjeto.SeleccionarMovimientoReporteActivoEstatusSalida(ActivoEntidadObjeto);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaSalida.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaSalida.CssClass = ConstantePrograma.ClaseTablaImpresion;

                DivTablaSalida.Visible = true;
                TablaSalida.DataSource = Resultado.ResultadoDatos;
                TablaSalida.DataBind();
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

        #endregion

       
    }
}
