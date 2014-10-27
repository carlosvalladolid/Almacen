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
    public partial class ImprimirResguardoVehiculo : System.Web.UI.Page
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
                int ActivoId = 0;
                string FechaActual = string.Empty;

                EmpleadoId = Int16.Parse(Request.QueryString["EmpleadoId"]);
                ActivoId = int.Parse(Request.QueryString["ActivoId"]);

                SeleccionarEmpleado(EmpleadoId);
                SeleccionarActivo(ActivoId);
                MostrarTitulo();

                FechaActual = DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString("0#") + "/" + DateTime.Today.Day.ToString("0#") + "/" + DateTime.Today.Hour.ToString("0#") + "/" + DateTime.Today.Minute.ToString("0#");
                FechaLabel.Text = FormatoFecha.AsignarFormato(FechaActual, ConstantePrograma.UniversalFormatoFecha);

                DependenciaLabel.Text = ConfigurationManager.AppSettings["Activos.Web.Dependencia"].ToString();
                TitularAreaLabel.Text = ConfigurationManager.AppSettings["Activos.Web.DirectorGeneralNombre"].ToString();
                RFCTitularAreaLabel.Text = ConfigurationManager.AppSettings["Activos.Web.DirectorGeneralRFC"].ToString();
                NumeroEmpleadoTitularAreaLabel.Text = ConfigurationManager.AppSettings["Activos.Web.DirectorGeneralNumero"].ToString();

                //Firmas:
                FirmaTitularAreaLabel.Text = ConfigurationManager.AppSettings["Activos.Web.DirectorAdministrativo"].ToString();
                FirmaEnlaceVehiculosLabel.Text = ConfigurationManager.AppSettings["Activos.Web.EnlaceVehiculo"].ToString();
            }
        }

        protected void MostrarTitulo()
        {
            Master.MostrarTitulo("INSTITUTO DE DEFENSORIA PUBLICA DE NUEVO LEÓN", "GOBIERNO DEL ESTADO DE NUEVO LEÓN");
        }

        protected void SeleccionarActivo(int ActivoId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            ActivoProceso ActivoProcesoNegocio = new ActivoProceso();

            ActivoObjetoEntidad.ActivoId = ActivoId;

            Resultado = ActivoProcesoNegocio.SeleccionarActivoCompleto(ActivoObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                TipoDocumentoLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreTipoDocumento"].ToString();
                FolioLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CompraFolio"].ToString();

                MarcaLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreMarca"].ToString();
                TipoLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreSubFamilia"].ToString();
                ModeloLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Modelo"].ToString();
                ColorLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Color"].ToString();

                CodigoBarrasLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoBarrasParticular"].ToString();

                SeleccionarAccesorios(ActivoId);
            }
        }

        protected void SeleccionarAccesorios(int ActivoId)
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
                            PlacasLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.Motor:
                            MotorLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.Serie:
                            SerieLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.Kilometraje:
                            KilometrajeLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.PlacaAnterior:
                            PlacaAnteriorLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.Clima:
                            ClimaLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.Radio:
                            RadioLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.Antena:
                            AntenaLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.Extinguidor:
                            ExtinguidorLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.Refaccion:
                            RefaccionLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.Gato:
                            GatoLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.CrucetaL:
                            CrucetaLLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.Herramienta:
                            HerramientaLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.JgoLucesPreventivas:
                            LucesPreventivasLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.JgoCablesPasacorriente:
                            CablesPasaCorrienteLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;

                        case (Int16)ConstantePrograma.TipoAccesorio.TarjetaEstacionamiento:
                            TarjetaEstacionamientoLabel.Text = dtRegistro["Descripcion"].ToString();
                            break;
                    }
                }

               
                
            }
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
                ResponsableVehiculoLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                RFCResponsableVehiculoLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["RFC"].ToString();
                NumeroEmpleadoResponsableVehiculoLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NumeroEmpleado"].ToString();
                FirmaResponsableVehiculoLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleadoCompleto"].ToString();
                DireccionLabel.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreDireccion"].ToString();
            }
        }

        #endregion
    }
}
