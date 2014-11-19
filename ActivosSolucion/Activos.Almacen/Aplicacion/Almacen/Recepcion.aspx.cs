using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;


using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Comun.Fecha;
using Activos.Entidad.Catalogo;
using Activos.Entidad.General;
using Activos.Entidad.Almacen;
//using Activos.Entidad.Activos;
using Activos.ProcesoNegocio.Almacen;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.Almacen.Aplicacion.Almacen
{
    public partial class Recepcion : System.Web.UI.Page
    {

        #region "Eventos"

        //protected void BotonGuardar_Click(object sender, EventArgs e)
        //{
        //    if (Page.IsValid)
        //    {
        //        GuardarRecepcion();
        //    }
        //}

        protected void BotonAgregar_Click(object sender, ImageClickEventArgs e)
        {
            AgregarDetalleDocumento();

        }

        protected void SolicitanteCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarJefe(Int16.Parse(SolicitanteIdNuevo.SelectedValue));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSubfamilia();
        }


        protected void LinkBuscarClave_SelectedTextChanged(object sender, EventArgs e)
        {
            SeleccionarClave();
        }


        protected void LinkBuscarOrdenCompra_SelectedTextChanged(object sender, EventArgs e)
        {
            SeleccionarOrdenCompra();
        }
        #endregion

        #region "Métodos"

        protected void AgregarDetalleDocumento()
        {

            RecepcionEntidad RecepcionObjetoEntidad = new RecepcionEntidad();

            RecepcionObjetoEntidad.TemporalRecepcionId = TemporalRecepcionIdHidden.Value;           
            RecepcionObjetoEntidad.ProveedorId = Int16.Parse(ProveedorIdNuevo.SelectedValue);
            RecepcionObjetoEntidad.TipoDocumentoId = Int16.Parse(TipoDocumentoIdNuevo.SelectedValue);           
            RecepcionObjetoEntidad.EmpleadoId = Int16.Parse(SolicitanteIdNuevo.SelectedValue);
            RecepcionObjetoEntidad.JefeId = Int16.Parse(JefeInmediatoIdNuevo.SelectedValue);
            RecepcionObjetoEntidad.Clave = FolioNuevo.Text.Trim();
            RecepcionObjetoEntidad.Monto = decimal.Parse(MontoDatosNuevo.Text);
            if (!(FechaDocumentoNuevo.Text.Trim() == ""))
                RecepcionObjetoEntidad.FechaDocumento = FormatoFecha.AsignarFormato(FechaDocumentoNuevo.Text.Trim(), ConstantePrograma.UniversalFormatoFecha);

            RecepcionObjetoEntidad.ProductoId = ProductoIdHidden.Value;
            RecepcionObjetoEntidad.Precio = decimal.Parse(PrecionUnitarioNuevo.Text);
            RecepcionObjetoEntidad.Cantidad = MontoDocumentoNuevo.Text.Trim();

            AgregarRecepcion(RecepcionObjetoEntidad);
        }

        protected void AgregarRecepcion(RecepcionEntidad RecepcionObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RecepcionProceso RecepcionProcesoNegocio = new RecepcionProceso();

            Resultado = RecepcionProcesoNegocio.AgregarRecepcion(RecepcionObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Recepcion.RecepcionGuardadoCorrectamente)
            {
                TemporalRecepcionIdHidden.Value = RecepcionObjetoEntidad.RecepcionId;
                // LimpiarNuevo();
                LimpiarRecepcion();          
                SeleccionarRecepcion();
            }
            else
            {
                EtiquetaMensaje.Text = Resultado.DescripcionError;
            }
        }

        protected void SeleccionarRecepcion()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RecepcionEntidad RecepcionObjetoEntidad = new RecepcionEntidad();
            RecepcionProceso RecepcionProcesoNegocio = new RecepcionProceso();

            RecepcionObjetoEntidad.RecepcionId = TemporalRecepcionIdHidden.Value;

            Resultado = RecepcionProcesoNegocio.SeleccionaRecepcion(RecepcionObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaRecepcion.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaRecepcion.CssClass = ConstantePrograma.ClaseTabla;



                TablaRecepcion.DataSource = Resultado.ResultadoDatos;
                TablaRecepcion.DataBind();

            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }
        }

        protected void LimpiarRecepcion()
        {
            ClaveNuevo.Text = "";
            FamiliaIdNuevo.SelectedIndex = 0;
            SeleccionarSubfamilia();
            SubFamiliaIdNuevo.SelectedIndex = 0;
            MarcaIdNuevo.Text = "";
            DescripcionNuevo.Text = "";
            PrecionUnitarioNuevo.Text = "";
            CantidadNuevo.Text = "";
            MontoDocumentoNuevo.Text = "";
        
        
        }


        //protected void GuardaRecepcion()
        //{
        //    RecepcionEntidad RecepcionObjetoEntidad = new RecepcionEntidad();
        //    UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

        //    if (TemporalRecepcionIdHidden.Value != "0")
        //    {
        //        if (TablaRecepcion.Rows.Count > 0)
        //        {
        //            RecepcionObjetoEntidad.RecepcionId= TemporalRecepcionIdHidden.Value;

        //            GuardarRecepcion(RecepcionObjetoEntidad);
        //        }

        //    }
        //    else
        //    {
        //        EtiquetaMensaje.Text = "Favor de agregar los Productos";
        //    }
        //}

        //protected void GuardarRecepcion(RecepcionEntidad RecepcionObjetoEntidad)
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    RecepcionProceso RecepcionProcesoNegocio = new RecepcionProceso();

        //    Resultado = RecepcionProcesoNegocio.GuardarRecepcion(RecepcionObjetoEntidad);

        //    if (Resultado.ErrorId == (int)ConstantePrograma.Recepcion.RecepcionGuardadoCorrectamente)
        //    {
        //        LimpiarNuevoRegistro();
        //        LimpiarDetalleDocumento();

        //    }
        //    else
        //    {
        //        EtiquetaMensaje.Text = Resultado.DescripcionError;
        //    }
        //}



        private void Inicio()
        {
            if (Page.IsPostBack)
                return;
           
            SeleccionarProveedor();
            SeleccionarEmpleado();
            SeleccionarMarca();
            SeleccionarFamilia();
            SeleccionarSubfamilia();
            SeleccionarTipoDocumento();

            JefeInmediatoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void SeleccionarClave()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AlmacenEntidad AlmacenEntidadObjeto = new AlmacenEntidad();
            AlmacenProceso AlmacenProcesoObjeto = new AlmacenProceso();
            bool AsignacionPermitida = true;

            AlmacenEntidadObjeto.Clave = ClaveNuevo.Text.Trim();

            Resultado = AlmacenProcesoObjeto.SeleccionarProducto(AlmacenEntidadObjeto);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                {
                    if (AsignacionPermitida == true)
                    {
                        FamiliaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString();
                        SeleccionarSubfamilia();
                        SubFamiliaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["SubFamiliaId"].ToString();
                        MarcaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["MarcaId"].ToString();
                        DescripcionNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreProducto"].ToString();
                        CantidadNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["MaximoPermitido"].ToString();
                        ProductoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ProductoId"].ToString();

                       // AgregarEtiquetaMensaje.Text = "";
                    }
                    else
                    {
                       // LimpiarProducto();
                        //AgregarEtiquetaMensaje.Text = TextoError.EstatusActivoIncorrecto;
                        ClaveNuevo.Focus();

                    }


                }
                else
                {
                   // LimpiarProducto();
                  //  AgregarEtiquetaMensaje.Text = TextoError.NoExisteActivo;
                    ClaveNuevo.Focus();
                }
            }
            else
            {
               // LimpiarProducto();
                //AgregarEtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

        }

        protected void SeleccionarOrdenCompra()
        {
            //ResultadoEntidad Resultado = new ResultadoEntidad();
            //OrdenEntidad OrdenEntidadObjeto = new OrdenEntidad();
          //  OrdenProceso OrdenProcesoObjeto = new OrdenProceso();

            OrdenProceso OrdenProceso = new OrdenProceso();
            //bool AsignacionPermitida = true;

            OrdenProceso.OrdenEncabezadoEntidad.Clave = OrderCompraNuevo.Text.Trim();
            OrdenProceso.SeleccionarBusquedaOrdenCompra();
            
            if (OrdenProceso.ErrorId == 0)
                {
                    // OrdenIdHidden.Value = OrdenProceso.OrdenDetalleEntidad.OrdenId;
                     FechaOrdenCompraNuevo.Text = OrdenProceso.OrdenDetalleEntidad.FechaOrden;
                    SolicitanteIdNuevo.SelectedValue =OrdenProceso.OrdenDetalleEntidad.EmpleadoId;
                    
                }
                else
                {
                 //   MostrarMensaje(OrdenProceso.DescripcionError, ConstantePrograma.TipoErrorAlerta);
                 }

             }








           // Resultado = OrdenProcesoObjeto.SeleccionarBusquedaOrdenCompra(OrdenEntidadObjeto);

            //if (Resultado.ErrorId == 0)
            //{
            //    if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
            //    {
            //        if (AsignacionPermitida == true)
            //        {
            //            FechaOrdenCompraNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["FechaOrden"].ToString();
            //            SolicitanteIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
            //          //  SeleccionarJefe();                        
            //          //  AgregarEtiquetaMensaje.Text = "";
            //        }
            //        else
            //        {
            //            LimpiarRecepcion();
            //          //  AgregarEtiquetaMensaje.Text = TextoError.EstatusActivoIncorrecto;
            //            FolioNuevo.Focus();

            //        }


            //    }
            //    else
            //    {
            //        LimpiarRecepcion();
            //     //   AgregarEtiquetaMensaje.Text = TextoError.NoExisteActivo;
            //        FolioNuevo.Focus();
            //    }
            //}
            //else
            //{
            //    LimpiarRecepcion();
            //   // AgregarEtiquetaMensaje.Text = TextoError.ErrorGenerico;
            //}

        //}

        protected void SeleccionarFamilia()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            FamiliaEntidad FamiliaEntidadObjeto = new FamiliaEntidad();
            FamiliaProceso FamiliaProcesoObjeto = new FamiliaProceso();

            //FamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusFamilia.Activo;

            Resultado = FamiliaProcesoObjeto.SeleccionarFamilia(FamiliaEntidadObjeto);

            FamiliaIdNuevo.DataValueField = "FamiliaId";
            FamiliaIdNuevo.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                FamiliaIdNuevo.DataSource = Resultado.ResultadoDatos;
                FamiliaIdNuevo.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            FamiliaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void SeleccionarMarca()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MarcaEntidad MarcaEntidadObjeto = new MarcaEntidad();
            MarcaProceso MarcaProcesoObjeto = new MarcaProceso();

            //MarcaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusMarca.Activo;

            Resultado = MarcaProcesoObjeto.SeleccionarMarca(MarcaEntidadObjeto);

            MarcaIdNuevo.DataValueField = "MarcaId";
            MarcaIdNuevo.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                MarcaIdNuevo.DataSource = Resultado.ResultadoDatos;
                MarcaIdNuevo.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            MarcaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void SeleccionarSubfamilia()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SubFamiliaEntidad SubFamiliaEntidadObjeto = new SubFamiliaEntidad();
            SubFamiliaProceso SubFamiliaProcesoObjeto = new SubFamiliaProceso();

            //SubFamiliaEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusSubFamilia.Activo;
            SubFamiliaEntidadObjeto.FamiliaId = Int16.Parse(FamiliaIdNuevo.SelectedValue);

            if (SubFamiliaEntidadObjeto.FamiliaId == 0)
            {
                SubFamiliaIdNuevo.Items.Clear();
            }
            else
            {
                Resultado = SubFamiliaProcesoObjeto.SeleccionarSubFamilia(SubFamiliaEntidadObjeto);

                SubFamiliaIdNuevo.DataValueField = "SubFamiliaId";
                SubFamiliaIdNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    SubFamiliaIdNuevo.DataSource = Resultado.ResultadoDatos;
                    SubFamiliaIdNuevo.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            SubFamiliaIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        private void SeleccionarProveedor()
        {
            Activos.ProcesoNegocio.Almacen.ProveedorProceso ProveedorProceso = new Activos.ProcesoNegocio.Almacen.ProveedorProceso();

            ProveedorProceso.SeleccionarProveedor();

            ProveedorIdNuevo.DataValueField = "ProveedorId";
            ProveedorIdNuevo.DataTextField = "Nombre";

            if (ProveedorProceso.ErrorId == 0)
            {
                ProveedorIdNuevo.DataSource = ProveedorProceso.ResultadoDatos;
                ProveedorIdNuevo.DataBind();
            }
            else
            {
                // ToDo: Manejar mensajes de error
                //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            ProveedorIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        private void SeleccionarEmpleado()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

            EmpleadoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusEmpleados.Activo;

            Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

            SolicitanteIdNuevo.DataValueField = "EmpleadoId";
            SolicitanteIdNuevo.DataTextField = "NombreEmpleadoCompleto";

            if (Resultado.ErrorId == 0)
            {
                SolicitanteIdNuevo.DataSource = Resultado.ResultadoDatos;
                SolicitanteIdNuevo.DataBind();
            }
            else
            {
                // ToDo: Manejar mensajes de error
                //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            SolicitanteIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        private void SeleccionarJefe(Int16 EmpleadoIdJefe)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

            if (EmpleadoIdJefe == 0)
            {
                JefeInmediatoIdNuevo.Items.Clear();
                JefeInmediatoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));

                return;
            }

            EmpleadoEntidadObjeto.EmpleadoId = EmpleadoIdJefe;

            Resultado = EmpleadoProcesoNegocio.SeleccionarEmpleado(EmpleadoEntidadObjeto);

            JefeInmediatoIdNuevo.DataValueField = "EmpleadoIdJefe";
            JefeInmediatoIdNuevo.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                JefeInmediatoIdNuevo.DataSource = Resultado.ResultadoDatos;
                JefeInmediatoIdNuevo.DataBind();
            }
            else
            {
                //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            JefeInmediatoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void SeleccionarTipoDocumento()
        {
            Activos.ProcesoNegocio.Almacen.TipoDocumentoProceso TipoDocumentoProceso = new Activos.ProcesoNegocio.Almacen.TipoDocumentoProceso();
            
            TipoDocumentoProceso.SeleccionarTipoDocumento();

            TipoDocumentoIdNuevo.DataValueField = "TipoDocumentoId";
            TipoDocumentoIdNuevo.DataTextField = "Nombre";

            if (TipoDocumentoProceso.ErrorId == 0)
            {
                TipoDocumentoIdNuevo.DataSource = TipoDocumentoProceso.ResultadoDatos;
                TipoDocumentoIdNuevo.DataBind();
            }
            else
            {
                // ToDo: Manejar mensajes de error
                //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            TipoDocumentoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));





        }

        private void ShowMessage(string Mensaje, string TipoMensaje)
        {
            StringBuilder FormatoMensaje = new StringBuilder();

            FormatoMensaje.Append("MostrarMensaje(\"");
            FormatoMensaje.Append(Mensaje);
            FormatoMensaje.Append("\", \"");
            FormatoMensaje.Append(TipoMensaje);
            FormatoMensaje.Append("\");");

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Mensaje", Comparar.ReemplazarCadenaJavascript(FormatoMensaje.ToString()), true);
        }



        #endregion


    }
}
