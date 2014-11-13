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


        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSubfamilia();
        }

        #endregion

        #region "Métodos"

        private void Inicio()
        {
            if (Page.IsPostBack)
                return;

           
            SeleccionarProveedor();
            SeleccionarEmpleado();
            SeleccionarMarca();
            SeleccionarFamilia();
            SeleccionarSubfamilia();
            //SeleccionarTipoDocumento();

            JefeInmediatoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
        }

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

        //protected void SeleccionarTipoDocumento()
        //{

        //    Activos.ProcesoNegocio.Almacen.ProveedorProceso ProveedorProceso = new Activos.ProcesoNegocio.Almacen.ProveedorProceso();

        //    ProveedorProceso.SeleccionarProveedor();

        //    ProveedorIdNuevo.DataValueField = "ProveedorId";
        //    ProveedorIdNuevo.DataTextField = "Nombre";

        //    if (ProveedorProceso.ErrorId == 0)
        //    {
        //        ProveedorIdNuevo.DataSource = ProveedorProceso.ResultadoDatos;
        //        ProveedorIdNuevo.DataBind();
        //    }
        //    else
        //    {
        //        // ToDo: Manejar mensajes de error
        //        //EtiquetaMensaje.Text = TextoError.ErrorGenerico;
        //    }

        //    ProveedorIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));





        //}

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
