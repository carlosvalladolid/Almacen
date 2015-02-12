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
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Almacen;

namespace Activos.Almacen.Aplicacion.Catalogo
{
    public partial class ProveedorAlmacen : System.Web.UI.Page
    {
        #region "Eventos"

        protected void BotonBusquedaRapida_Click(object sender, ImageClickEventArgs e)
        {
            NombreBusqueda.Text = "";
            BusquedaAvanzada();
        }

        protected void BotonCancelarNuevo_Click(object sender, EventArgs e)
        {
            CambiarNuevoRegistro();
        }

        protected void BotonGuardar_Click(object sender, EventArgs e)
        {
            GuardarProveedor();
        }

        protected void NuevoRegistroLink_Click(object sender, EventArgs e)
        {
            CambiarNuevoRegistro();
        }

        protected void BusquedaAvanzadaLink_Click(object sender, EventArgs e)
        {
            CambiarBusquedaAvanzada();
        }

        protected void BotonCancelarBusqueda_Click(object sender, EventArgs e)
        {
            CambiarBusquedaAvanzada();
        }

        protected void BotonBusqueda_Click(object sender, EventArgs e)
        {
            BusquedaAvanzada();
        }

        protected void EstadoNuevo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarCiudadNuevo();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        protected void TablaProveedor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TablaProveedor.PageIndex = e.NewPageIndex;
            BusquedaAvanzada();
        }

        protected void TablaProveedor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TablaProveedorEventoComando(e);
        }

        #endregion

        #region "Métodos"

        //protected void EliminarProveedor()
        //{
        //    ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
        //    ProveedorAlmacenEntidad ProveedorAlmacenEntidadObjeto = new ProveedorAlmacenEntidad();

        //    ProveedorAlmacenEntidadObjeto.CadenaProveedorId = ObtenerCadenaProveedorId();

        //    EliminarProveedor(ProveedorAlmacenEntidadObjeto);
        //}

        //protected void EliminarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenEntidadObjeto)
        //{
        //    ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
        //    ProveedorAlmacenProceso ProveedorAlmacenProcesoObjeto = new ProveedorAlmacenProceso();

        //    ResultadoEntidadObjeto = ProveedorAlmacenProcesoObjeto.EliminarProveedor(ProveedorAlmacenProcesoObjeto);

        //    if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Proveedor.EliminacionExitosa)
        //    {
        //        EtiquetaMensaje.Text = "";

        //        BusquedaAvanzada();
        //    }
        //    else
        //    {
        //        EtiquetaMensaje.Text = ResultadoEntidadObjeto.DescripcionError;
        //    }
        //}

        protected void BusquedaAvanzada()
        {
            ProveedorAlmacenEntidad ProveedorAlmacenEntidadObjeto = new ProveedorAlmacenEntidad();

            ProveedorAlmacenEntidadObjeto.Nombre = NombreBusqueda.Text.Trim();
           
            ProveedorAlmacenEntidadObjeto.BusquedaRapida = TextoBusquedaRapida.Text.Trim();

            SeleccionarProveedor(ProveedorAlmacenEntidadObjeto);
        }

        protected string ObtenerCadenaProveedorId()
        {
            StringBuilder CadenaProveedorId = new StringBuilder();
            CheckBox CasillaEliminar;

            CadenaProveedorId.Append(",");

            foreach (GridViewRow Registro in TablaProveedor.Rows)
            {
                CasillaEliminar = (CheckBox)Registro.FindControl("SeleccionarBorrar");

                if (CasillaEliminar.Checked)
                {
                    CadenaProveedorId.Append(TablaProveedor.DataKeys[Registro.RowIndex]["ProveedorId"].ToString());
                    CadenaProveedorId.Append(",");
                }
            }

            return CadenaProveedorId.ToString();
        }

        protected void GuardarProveedor()
        {
            ProveedorAlmacenEntidad ProveedorAlmacenObjetoEntidad = new ProveedorAlmacenEntidad();
            UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

            UsuarioSessionEntidad = (UsuarioEntidad)Session["UsuarioEntidad"];

            ProveedorAlmacenObjetoEntidad.ProveedorId = Int16.Parse(ProveedorIdHidden.Value);
            ProveedorAlmacenObjetoEntidad.DependenciaId = Int16.Parse(DependenciaNuevo.SelectedValue);
            ProveedorAlmacenObjetoEntidad.BancoId = Int16.Parse(BancoNuevo.SelectedValue);
            ProveedorAlmacenObjetoEntidad.CiudadId = Int16.Parse(CiudadNuevo.SelectedValue);
            ProveedorAlmacenObjetoEntidad.UsuarioIdInserto = UsuarioSessionEntidad.UsuarioId;
            ProveedorAlmacenObjetoEntidad.UsuarioIdModifico = UsuarioSessionEntidad.UsuarioId;
            ProveedorAlmacenObjetoEntidad.Nombre = NombreNuevo.Text.Trim();
            ProveedorAlmacenObjetoEntidad.RFC = RFCNuevo.Text.Trim();
            ProveedorAlmacenObjetoEntidad.Calle = CalleNuevo.Text.Trim();
            ProveedorAlmacenObjetoEntidad.Numero = NumeroNuevo.Text.Trim();
            ProveedorAlmacenObjetoEntidad.Colonia = ColoniaNuevo.Text.Trim();
            ProveedorAlmacenObjetoEntidad.CodigoPostal = CodigoPostalNuevo.Text.Trim();
            ProveedorAlmacenObjetoEntidad.Telefono = TelefonoNuevo.Text.Trim();
            ProveedorAlmacenObjetoEntidad.NombreContacto = NombreContactoNuevo.Text.Trim();
            ProveedorAlmacenObjetoEntidad.Email = EmailNuevo.Text.Trim();
            ProveedorAlmacenObjetoEntidad.CiudadOtro = OtraCiudadNuevo.Text.Trim();
            ProveedorAlmacenObjetoEntidad.MontoMaximoCompra = decimal.Parse(MontoMaximoCompraNuevo.Text);
            ProveedorAlmacenObjetoEntidad.Cuenta = CuentaNuevo.Text.Trim();

            ProveedorAlmacenObjetoEntidad.Clabe = ClabeNuevo.Text.Trim();

            GuardarProveedor(ProveedorAlmacenObjetoEntidad);
        }

        protected void GuardarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ProveedorAlmacenProceso ProveedorAlmacenProcesoNegocio = new ProveedorAlmacenProceso();

            Resultado = ProveedorAlmacenProcesoNegocio.GuardarProveedor(ProveedorAlmacenObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Proveedor.ProveedorGuardadoCorrectamente)
            {
                LimpiarNuevoRegistro();
                PanelNuevoRegistro.Visible = false;
                PanelBusquedaAvanzada.Visible = false;
                BusquedaAvanzada();
            }
            else
            {
                EtiquetaMensaje.Text = Resultado.DescripcionError;
            }
        }

        private void Inicio()
        {
           
            //Master.NuevoRegistroMaster.Click += new EventHandler(NuevoRegistro_Click);
            //Master.BusquedaAvanzadaMaster.Click += new EventHandler(BusquedaAvanzadaLink_Click);
            //Master.EliminarRegistroMaster.Click += new EventHandler(EliminarRegistroLink_Click);

            if (!Page.IsPostBack)
            {
                SeleccionarDependencia();
                SeleccionarEstado();
                SeleccionarCiudadNuevo();               
                SeleccionarBanco();
                BusquedaAvanzada();
                SeleccionarTextoError();
            }
        }

        protected void CambiarBusquedaAvanzada()
        {
            PanelBusquedaAvanzada.Visible = !PanelBusquedaAvanzada.Visible;
            PanelNuevoRegistro.Visible = false;
        }

        protected void CambiarNuevoRegistro()
        {
            PanelBusquedaAvanzada.Visible = false;
            PanelNuevoRegistro.Visible = !PanelNuevoRegistro.Visible;
            LimpiarNuevoRegistro();
        }

        protected void CambiarEditarRegistro()
        {
            PanelBusquedaAvanzada.Visible = false;
            PanelNuevoRegistro.Visible = true;
        }

        protected void LimpiarNuevoRegistro()
        {
            NombreNuevo.Text = "";
            CalleNuevo.Text = "";
            NumeroNuevo.Text = "";
            ColoniaNuevo.Text = "";
            NumeroNuevo.Text = "";
            CodigoPostalNuevo.Text = "";
            NombreContactoNuevo.Text = "";
            TelefonoNuevo.Text = "";
            EmailNuevo.Text = "";
            ColoniaNuevo.Text = "";
            CuentaNuevo.Text = "";
            ClabeNuevo.Text = "";
            RFCNuevo.Text = "";
            BancoNuevo.SelectedValue = "0";
            CiudadNuevo.SelectedValue = "0";
            OtraCiudadNuevo.Text = "";
            MontoMaximoCompraNuevo.Text = "";
            //EstadoNuevo.SelectedValue = "0";
            DependenciaNuevo.SelectedValue = "0";
            EtiquetaMensaje.Text = "";

            ProveedorIdHidden.Value = "0";
        }

        protected void SeleccionarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ProveedorAlmacenProceso ProveedorAlmacenProcesoNegocio = new ProveedorAlmacenProceso();

            Resultado = ProveedorAlmacenProcesoNegocio.SeleccionarProveedor(ProveedorAlmacenObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                    TablaProveedor.CssClass = ConstantePrograma.ClaseTablaVacia;
                else
                    TablaProveedor.CssClass = ConstantePrograma.ClaseTabla;

                TablaProveedor.DataSource = Resultado.ResultadoDatos;
                TablaProveedor.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }
        }

        protected void SeleccionarProveedorParaEditar(ProveedorAlmacenEntidad ProveedorAlmacenObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ProveedorAlmacenProceso ProveedorAlmacenProcesoNegocio = new ProveedorAlmacenProceso();

            Resultado = ProveedorAlmacenProcesoNegocio.SeleccionarProveedor(ProveedorAlmacenObjetoEntidad);

            if (Resultado.ErrorId == 0)
            {
                NombreNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
                CalleNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Calle"].ToString();
                NumeroNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Numero"].ToString();
                ColoniaNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Colonia"].ToString();
                CodigoPostalNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CodigoPostal"].ToString();
                NombreContactoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreContacto"].ToString();
                TelefonoNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Telefono"].ToString();
                EmailNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Email"].ToString();
                CuentaNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Cuenta"].ToString();
                ClabeNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["Clabe"].ToString();
                DependenciaNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["DependenciaId"].ToString();
                BancoNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["BancoId"].ToString();
                RFCNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["RFC"].ToString();
                EstadoNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EstadoId"].ToString();
                SeleccionarCiudadNuevo();
                CiudadNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["CiudadId"].ToString();
                MontoMaximoCompraNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["MontoMaximoCompra"].ToString();
                OtraCiudadNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["CiudadOtro"].ToString();
                CambiarEditarRegistro();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }
        }

        protected void SeleccionarDependencia()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            DependenciaEntidad DependenciaEntidadObjeto = new DependenciaEntidad();
            DependenciaProceso DependenciaProcesoObjeto = new DependenciaProceso();

            Resultado = DependenciaProcesoObjeto.SeleccionarDependencia(DependenciaEntidadObjeto);

            DependenciaNuevo.DataValueField = "DependenciaId";
            DependenciaNuevo.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                DependenciaNuevo.DataSource = Resultado.ResultadoDatos;
                DependenciaNuevo.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            DependenciaNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void SeleccionarBanco()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            BancoEntidad BancoEntidadObjeto = new BancoEntidad();
            BancoProceso BancoProcesoObjeto = new BancoProceso();

            Resultado = BancoProcesoObjeto.SeleccionarBanco(BancoEntidadObjeto);

            BancoNuevo.DataValueField = "BancoId";
            BancoNuevo.DataTextField = "Nombre";

            if (Resultado.ErrorId == 0)
            {
                BancoNuevo.DataSource = Resultado.ResultadoDatos;
                BancoNuevo.DataBind();
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            BancoNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        protected void SeleccionarEstado()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EstadoEntidad EstadoEntidadObjeto = new EstadoEntidad();
            EstadoProceso EstadoProcesoObjeto = new EstadoProceso();

            Resultado = EstadoProcesoObjeto.SeleccionarEstado(EstadoEntidadObjeto);

            EstadoNuevo.DataValueField = "EstadoId";
            EstadoNuevo.DataTextField = "Nombre";

           

            if (Resultado.ErrorId == 0)
            {
                EstadoNuevo.DataSource = Resultado.ResultadoDatos;
                EstadoNuevo.DataBind();

              
            }
            else
            {
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            EstadoNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
            
        }

        protected void SeleccionarCiudadNuevo()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            CiudadEntidad CiudadEntidadObjeto = new CiudadEntidad();
            CiudadProceso CiudadProcesoObjeto = new CiudadProceso();

            CiudadEntidadObjeto.EstadoId = Int16.Parse(EstadoNuevo.SelectedValue);

            if (CiudadEntidadObjeto.EstadoId == 0)
            {
                CiudadNuevo.Items.Clear();
            }
            else
            {
                Resultado = CiudadProcesoObjeto.SeleccionarCiudad(CiudadEntidadObjeto);

                CiudadNuevo.DataValueField = "CiudadId";
                CiudadNuevo.DataTextField = "Nombre";

                if (Resultado.ErrorId == 0)
                {
                    CiudadNuevo.DataSource = Resultado.ResultadoDatos;
                    CiudadNuevo.DataBind();
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

            CiudadNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

        private void MostrarMensaje(string Mensaje, string TipoMensaje)
        {
            StringBuilder FormatoMensaje = new StringBuilder();

            FormatoMensaje.Append("MostrarMensaje(\"");
            FormatoMensaje.Append(Mensaje);
            FormatoMensaje.Append("\", \"");
            FormatoMensaje.Append(TipoMensaje);
            FormatoMensaje.Append("\");");

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Mensaje", Comparar.ReemplazarCadenaJavascript(FormatoMensaje.ToString()), true);
        }
       
        protected void SeleccionarTextoError()
        {
            NombreRequerido.ErrorMessage = TextoError.ProveedorNombre + "<br />";
            DependenciaRequerido.ErrorMessage = TextoError.ProveedorDependencia + "<br />";
            NombreContactoRequerido.ErrorMessage = TextoError.ProveedorNombreContacto + "<br />";
            EstadoRequerido.ErrorMessage = TextoError.ProveedorEstado + "<br />";
            CiudadRequerido.ErrorMessage = TextoError.ProveedorCiudad + "<br />";
        }

        protected void TablaProveedorEventoComando(GridViewCommandEventArgs e)
        {
            ProveedorAlmacenEntidad ProveedorAlmacenEntidadObjeto = new ProveedorAlmacenEntidad();
            Int16 intFila = 0;
            int intTamañoPagina = 0;
            Int16 ProveedorId = 0;
            string strCommand = string.Empty;

            intFila = Int16.Parse(e.CommandArgument.ToString());
            strCommand = e.CommandName.ToString();
            intTamañoPagina = TablaProveedor.PageSize;

            if (intFila >= intTamañoPagina)
                intFila = (Int16)(intFila - (intTamañoPagina * TablaProveedor.PageIndex));


            switch (strCommand)
            {
                case "Select":
                    ProveedorId = Int16.Parse(TablaProveedor.DataKeys[intFila]["ProveedorId"].ToString());
                    ProveedorAlmacenEntidadObjeto.ProveedorId = ProveedorId;
                    ProveedorIdHidden.Value = ProveedorId.ToString();
                    SeleccionarProveedorParaEditar(ProveedorAlmacenEntidadObjeto);
                    break;

                default:
                    // Do nothing
                    break;
            }
        }

        #endregion
    }
}
