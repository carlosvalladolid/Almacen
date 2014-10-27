<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="ReporteEstatusActivo.aspx.cs" Inherits="Activos.Web.Aplicacion.Reportes.ReporteEstatusActivo"  ViewStateEncryptionMode="Never"%>
<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
   <link href="/Incluir/Estilo/Privado/jquery-ui-1.8.16.custom.css" rel="Stylesheet" type="text/css" />
   <link href="/Incluir/Estilo/Privado/demos.css" rel="Stylesheet" type="text/css" />

   
   <script language="javascript" type="text/javascript">

        function Imprimir() {
           var Form = document.getElementById("aspnetForm");

           Form.action = "/Ventana/ImprimirReporteEstatusActivo.aspx";
           Form.target = "_blank";
           Form.submit();

           Form.action = "ReporteEstatusActivo.aspx";
           Form.target = "_self";
        }
       
   </script>

</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" SeccionMenu="Reportes" runat="server" />
    </div>
    
    <div class="DivContenido">
        <div class="DivContenidoTitulo">
            <div class="DivTitulo">Reporte de estatus de activos</div>
        </div>
        
        <div class="DivInformacionContenido">
            <asp:UpdatePanel ID="ActualizarTablaAsignacion" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <asp:Panel ID="PanelTablaAsignacionActivo" runat="server">
                        <br />
                        <div class="DivSubtituloPagina">
                           Datos generales
                        </div>
                        
                        <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                           <table class="TablaFormulario">
                                <tr>
                                    <td class="Nombre">Familia</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="FamiliaId" OnSelectedIndexChanged="ddlFamilia_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">SubFamilia</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="SubFamiliaId" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Estatus</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:CheckBox ID="Asignados" runat="server" />&nbsp; Asignados</td>
                                </tr>
                                <tr>
                                    <td class="Nombre"></td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:CheckBox ID="NoAsignados" runat="server" />&nbsp; Sin asignar</td>
                                </tr>
                                <tr>
                                    <td class="Nombre"></td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:CheckBox ID="Salida" runat="server" OnCheckedChanged="SalidaCheckBox_CheckedChanged" AutoPostBack="true" />&nbsp; Fuera de las instalaciones</td>
                                </tr>
                                <tr id="CampoTipoServicio" visible="false" runat="server">
                                    <td class="Nombre">Tipo de servicio</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="TipoServicio" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre"></td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:CheckBox ID="NoEtiquetado" runat="server" />&nbsp; Sin etiquetar</td>
                                </tr>
                             <tr>
                                 <td colspan="3">
                                    <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimir" OnClick="BotonImprimir_Click" ImageUrl="~/Imagen/Boton/BotonImprimir.png" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton AlternateText="Limpiar" ID="BotonLimpiar" OnClick="BotonLimpiar_Click" ImageUrl="~/Imagen/Boton/BotonLimpiar.png" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                 </td> 
                             </tr>
                           </table> 
                        </asp:Panel>
                        <br />
                         <table width="100%">
                              <tr>
                                 <td>
                                    <asp:Label CssClass="TextoError" ID="EtiquetaMensajeError" runat="server" Text=""></asp:Label>
                                    
                                    <br /><br /><br />
                                 </td>
                              </tr>
                           </table> 
                     </asp:Panel> 
                     
                     <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaAsignacion" ID="ProgresoTablaAsignacion" runat="server">
                        <ProgressTemplate>
                            <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                        </ProgressTemplate>
                     </asp:UpdateProgress>
                     
                     <asp:HiddenField ID="FamiliaIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="FamiliaNombreHidden" runat="server" Value="" />
                     <asp:HiddenField ID="SubFamiliaHidden" runat="server" Value="" />
                     <asp:HiddenField ID="SubFamiliaNombreHidden" runat="server" Value="" />
                     <asp:HiddenField ID="AsignadosHidden" runat="server" Value="" />
                     <asp:HiddenField ID="NoAsignadosHidden" runat="server" Value="" />
                     <asp:HiddenField ID="SalidaHidden" runat="server" Value="" />
                     <asp:HiddenField ID="TipoServicioHidden" runat="server" Value="" />
                     <asp:HiddenField ID="TipoServicioNombreHidden" runat="server" Value="" />
                     <asp:HiddenField ID="NoEtiquetadoHidden" runat="server" Value="" />
                     
                </ContentTemplate>

                <Triggers>
                    
                </Triggers>
            </asp:UpdatePanel>
        </div> 
    
    </div> 
</asp:Content>
