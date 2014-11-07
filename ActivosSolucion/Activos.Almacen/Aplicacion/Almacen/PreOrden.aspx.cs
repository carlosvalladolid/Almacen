﻿using System;
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
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.ProcesoNegocio.Seguridad;
using Activos.ProcesoNegocio.Catalogo;
using Activos.ProcesoNegocio.Almacen;



namespace Almacen.Web.Aplicacion.Almacen
{
    public partial class PreOrden : System.Web.UI.Page
    {
        #region "Eventos"

        protected void AdvancedSearchLink_Click(Object sender, System.EventArgs e)
        {

        }

        protected void BotonAgregar_Click(object sender, ImageClickEventArgs e)
        {
            AgregarProducto();

        }
        
        protected void BusquedaAvanzadaLink_Click(Object sender, System.EventArgs e)
        {
            CambiarBusquedaAvanzada();
        }

        protected void LinkBuscarClave_Click(object sender, EventArgs e)
        {
            SeleccionarClave();
        }

        protected void EliminarRegistroLink_Click(Object sender, System.EventArgs e)
        {
            EliminarPreOrden();
        }

        protected void NuevoRegistro_Click(Object sender, System.EventArgs e)
        {
            CambiarNuevoRegistro();
        }
        
        protected void ddlSolicitante_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscarJefe();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }


        protected void FechaPreOrden_Validate(object source, ServerValidateEventArgs args)
        {
            string strEndDate = string.Empty;
            DateTime dtEndDate;

            
            strEndDate = FechaPreOrdenNuevo.Text.Trim();

            if (strEndDate != "")
            {
                if (DateTime.TryParse(strEndDate, out dtEndDate))
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
            else
            {
                args.IsValid = false;
            }
        }


        #endregion


        #region "Métodos"

        protected void AgregarProducto()
        {
            TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad = new TemporalPreOrdenEntidad();    
          
          if (TemporalPreOrdenId.Value != "0")
            {
                TemporalPreOrdenObjetoEntidad.PreOrdenId =  TemporalPreOrdenId.Value;
                TemporalPreOrdenObjetoEntidad.EmpleadoId = Int16.Parse(SolicitanteIdNuevo.SelectedValue);
                TemporalPreOrdenObjetoEntidad.JefeId = Int16.Parse(JefeInmediatoIdNuevo.SelectedValue);
                TemporalPreOrdenObjetoEntidad.Clave = ClaveNuevo.Text.Trim();
               //PENDIENTE DE CHECAR SI VA LLEVAR EL CAMPO DE ESTATUS POQUE EN EL DIAGRAMA NO APARECE 06/11/2014
                TemporalPreOrdenObjetoEntidad.EstatusId = 0;
                TemporalPreOrdenObjetoEntidad.ProductoId = ProductoIdHidden.Value;
                TemporalPreOrdenObjetoEntidad.Cantidad = Int16.Parse(CantidadNuevo.Text.Trim());        

                AgregarProducto(TemporalPreOrdenObjetoEntidad);
            }
        }

        protected void AgregarProducto(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalPreOrdenProceso TemporalPreOrdenProcesoNegocio = new TemporalPreOrdenProceso();

            Resultado = TemporalPreOrdenProcesoNegocio.AgregarTemporalPreOrden(TemporalPreOrdenObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenGuardadoCorrectamente)
             {
                //LimpiarNuevo();
                //CambiarBotonesNuevo();
                //SeleccionarTemporalProducto();
            }
            else
            {
                EtiquetaMensaje.Text = Resultado.DescripcionError;
            }
        }
        
  
        private void Inicio()
        {
          

            if (!Page.IsPostBack)
            {                
                SeleccionarFamilia();
                SeleccionarSubfamilia();
                SeleccionarMarca();
                SeleccionarEmpleado();
                BuscarJefe();

                TablaPreOrden.DataSource = null;
                TablaPreOrden.DataBind();
            }

        }

        protected void BuscarJefe()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();
            Int16 EmpleadoIdJefe;

            EmpleadoEntidadObjeto.EmpleadoId = Int16.Parse(SolicitanteIdNuevo.SelectedValue);            

            if (EmpleadoEntidadObjeto.EmpleadoId != 0)
            {
                Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

                if (Resultado.ErrorId == 0)
                {
                    EmpleadoIdJefe = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoIdJefe"].ToString());
                    SeleccionarJefe(EmpleadoIdJefe);
                }
                else
                {
                    EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                }
            }

        }

        private void CambiarBusquedaAvanzada()
        {
            //PanelBusquedaAvanzada.Visible = !PanelBusquedaAvanzada.Visible;
            PanelNuevoRegistro.Visible = false;
        }

        protected void CambiarEditarRegistro()
        {
            //PanelBusquedaAvanzada.Visible = false;
            PanelNuevoRegistro.Visible = true;
        }

        private void CambiarNuevoRegistro()
        {
            //PanelBusquedaAvanzada.Visible = false;
            PanelNuevoRegistro.Visible = !PanelNuevoRegistro.Visible;
            LimpiarNuevoRegistro();
        }

        private void EliminarPreOrden()
        { 
        
        }

        protected void LimpiarNuevoRegistro()
        {
            PreOrdenNuevo.Text = "";
            FechaPreOrdenNuevo.Text = "";
            SolicitanteIdNuevo.SelectedIndex = 0;
            JefeInmediatoIdNuevo.Items.Clear();
            // ClaveNuevo.Text = "";
            // FamiliaIdNuevo.SelectedIndex = 0;
           // SubFamiliaIdNuevo.SelectedIndex = 0;
           // MarcaIdNuevo.SelectedIndex = 0;
           // DescripcionNuevo.Text = "";
           //  CantidadNuevo.Text = "";
            EtiquetaMensaje.Text = "";        
        
        }

        protected void LimpiarProducto()
            {
            
            ClaveNuevo.Text = "";
            FamiliaIdNuevo.SelectedIndex = 0;
            SubFamiliaIdNuevo.SelectedIndex = 0;
            MarcaIdNuevo.SelectedIndex = 0;
            DescripcionNuevo.Text = "";
            CantidadNuevo.Text = "";

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

                   // Se valida que se pueda asignar el producto
                    //AsignacionPermitida = AlmacenProcesoObjeto.ValidarAsignacionProducto(int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ProductoId"].ToString()));

                    if (AsignacionPermitida == true)
                    {
                        FamiliaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["FamiliaId"].ToString();
                        SeleccionarSubfamilia();
                        SubFamiliaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["SubFamiliaId"].ToString();
                        MarcaIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["MarcaId"].ToString();
                        DescripcionNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreProducto"].ToString();
                        CantidadNuevo.Text = Resultado.ResultadoDatos.Tables[0].Rows[0]["MaximoPermitido"].ToString();
                        ProductoIdHidden.Value = Resultado.ResultadoDatos.Tables[0].Rows[0]["ProductoId"].ToString();

                        AgregarEtiquetaMensaje.Text = "";
                    }
                    else
                    {
                        LimpiarProducto();
                        AgregarEtiquetaMensaje.Text = TextoError.EstatusActivoIncorrecto;
                        ClaveNuevo.Focus();

                    }


                }
                else
                {
                    LimpiarProducto();
                    AgregarEtiquetaMensaje.Text = TextoError.NoExisteActivo;
                    ClaveNuevo.Focus();
                }
            }
            else
            {
                LimpiarProducto();
                AgregarEtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

        }
       
        protected void SeleccionarEmpleado()
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

        //    EmpleadoEntidadObjeto.EstatusId = (Int16)ConstantePrograma.EstatusEmpleados.Activo;

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
                EtiquetaMensaje.Text = TextoError.ErrorGenerico;
            }

            SolicitanteIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));
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

        //public void SeleccionarJefe(Int16 EmpleadoIdJefe)
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
        //    EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

        //    if (EmpleadoIdJefe != 0)
        //    {
        //        EmpleadoEntidadObjeto.EmpleadoId = EmpleadoIdJefe;

        //        Resultado = EmpleadoProcesoNegocio.SeleccionarEmpleado(EmpleadoEntidadObjeto);

        //        if (Resultado.ErrorId == 0)
        //        {
        //            JefeInmediatoIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoIdJefe"].ToString();
        //            JefeInmediatoIdNuevo.SelectedValue = Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoId"].ToString();
                   
        //        }
        //        else
        //        {
        //            EtiquetaMensaje.Text = TextoError.ErrorGenerico;
        //        }
        //    }
        //    else
        //    {
        //      //  NombreJefe.Text = "";
        //       // JefeIdHidden.Value = "0";
        //       // ActualizarTablaEmpleado.Update();
        //    }

        //}


        public void SeleccionarJefe(Int16 EmpleadoIdJefe)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoNegocio = new EmpleadoProceso();

            if (EmpleadoIdJefe != 0)
            {
                EmpleadoEntidadObjeto.EmpleadoId = EmpleadoIdJefe;

                if (EmpleadoEntidadObjeto.EmpleadoId == 0)
                {
                    JefeInmediatoIdNuevo.Items.Clear();
                }
                else
                {

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
                        EtiquetaMensaje.Text = TextoError.ErrorGenerico;
                    }
                }

                JefeInmediatoIdNuevo.Items.Insert(0, new ListItem(ConstantePrograma.FiltroSeleccione, "0"));

            }

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

        #endregion
    }
}