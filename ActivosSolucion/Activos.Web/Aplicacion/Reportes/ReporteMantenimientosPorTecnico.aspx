<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="ReporteMantenimientosPorTecnico.aspx.cs" Inherits="Activos.Web.Aplicacion.Reportes.ReporteMantenimientosPorTecnico" ViewStateEncryptionMode="Never" %>
<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
   <link href="/Incluir/Estilo/Privado/jquery-ui-1.8.16.custom.css" rel="Stylesheet" type="text/css" />
   <link href="/Incluir/Estilo/Privado/demos.css" rel="Stylesheet" type="text/css" />
   <script src="/Incluir/Javascript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
   <script src="/Incluir/Javascript/jquery.ui.datepicker-es.js" type="text/javascript"></script>
   <script src="/Incluir/Javascript/Calendar.js" type="text/javascript"></script>
   
   <script language="javascript" type="text/javascript">
       function pageLoad(sender, args) {
           SetNewCalendar("#<%= FechaDesde.ClientID %>", "#<%= FechaHasta.ClientID %>");
        }

        function Imprimir() {
           var Form = document.getElementById("aspnetForm");

           Form.action = "/Ventana/ImprimirReporteMantenimientosPorTecnico.aspx";
           Form.target = "_blank";
           Form.submit();

           Form.action = "ReporteMantenimientosPorTecnico.aspx";
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
           <div class="DivTitulo">Reporte mantenimientos por técnico</div>
      </div>
      
      <div class="DivInformacionContenido">
         <asp:UpdatePanel ID="ActualizarTablaReporte" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               
               <asp:Panel ID="PanelTablaReporte" runat="server">
                  <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                     <table class="TablaFormulario">
                        <tr>
                           <td class="Nombre">Fechas<span class="NotaCampo"> (dd/mm/aaaa)</span></td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo">
                              Desde&nbsp;&nbsp;<asp:TextBox CssClass="CajaTextoPequenia" ID="FechaDesde" MaxLength="50" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                              Hasta&nbsp;&nbsp;<asp:TextBox CssClass="CajaTextoPequenia" ID="FechaHasta" MaxLength="50" runat="server" Text=""></asp:TextBox>
                           </td>
                        </tr>
                        <tr>
                           <td class="Nombre">Estatus</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:DropDownList CssClass="ComboPequeño" ID="EstatusId" runat="server" ></asp:DropDownList></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Tipo de mantenimiento</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:DropDownList CssClass="ComboPequeño" ID="TipoMantenimientoId" runat="server" ></asp:DropDownList></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Técnico que atendió</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="ComboEmpleadoAtiende" runat="server" ></asp:DropDownList></td>
                        </tr>
                        <tr>
                           <td class="Nombre"></td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo">
                              <asp:RadioButton ID="rbDesglosado" Text=" Desglosado" GroupName="TipoReporte" Checked="true" runat="server" />&nbsp;&nbsp;
                              <asp:RadioButton ID="rbConcentrado" Text=" Concentrado" GroupName="TipoReporte" runat="server" />
                           </td>
                        </tr>
                        <tr>
                           <td colspan="3">
                              <asp:CustomValidator CssClass="TextoError" ControlToValidate="FechaDesde" EnableClientScript="false" ErrorMessage="" ID="FechaDesdeValidado" OnServerValidate="FechaDesde_Validate" runat="server" SetFocusOnError="true" ValidationGroup="Imprimir" ValidateEmptyText="false"></asp:CustomValidator>
                              <asp:CustomValidator CssClass="TextoError" ControlToValidate="FechaHasta" EnableClientScript="false" ErrorMessage="" ID="FechaHastaValidado" OnServerValidate="FechaHasta_Validate" runat="server" SetFocusOnError="true" ValidationGroup="Imprimir" ValidateEmptyText="false"></asp:CustomValidator>
                              <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
                              <br />
                              <asp:ImageButton ID="BotonImprimir" runat="server" AlternateText="Imprimir" ValidationGroup="Imprimir"
                                  ImageUrl="/Imagen/Boton/BotonImprimir.png" OnClick="BotonImprimir_Click" />&nbsp;&nbsp;
                              <asp:ImageButton ID="BotonLimpiar" runat="server" AlternateText="Limpiar" 
                                  ImageUrl="/Imagen/Boton/BotonLimpiar.png" OnClick="BotonLimpiar_Click" />&nbsp;&nbsp;
                           </td>
                        </tr>
                     </table> 
                  </asp:Panel> 
               </asp:Panel>
               
               <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaReporte" ID="ProgresoTablaReporte" runat="server">
                  <ProgressTemplate>
                      <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                  </ProgressTemplate>
               </asp:UpdateProgress>
               
               <asp:HiddenField ID="FechaDesdeHidden" runat="server" Value="0" />
               <asp:HiddenField ID="FechaHastaHidden" runat="server" Value="0" />
               <asp:HiddenField ID="EstatusIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="EstatusNombreHidden" runat="server" Value="0" />
               <asp:HiddenField ID="TipoMantenimientoIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="TipoMantenimientoNombreHidden" runat="server" Value="0" />
               <asp:HiddenField ID="EmpleadoIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="EmpleadoNombreHidden" runat="server" Value="0" />
               <asp:HiddenField ID="TipoReporteHidden" runat="server" Value="0" />
               
            </ContentTemplate>
            <Triggers>
            </Triggers>
         </asp:UpdatePanel> 
      </div> 
   </div> 
</asp:Content>
