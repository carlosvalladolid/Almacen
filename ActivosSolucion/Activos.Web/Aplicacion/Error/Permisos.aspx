<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="Activos.Web.Aplicacion.Error.Permisos" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
   <script language="javascript" type="text/javascript">

      function goToMenu() {
         document.location.href = '../Inicio.aspx';
      }
      
   </script>
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" runat="server" />
   </div>
   
   <div class="DivContenido">
      <div class="DivContenidoTitulo">
         <div class="DivTitulo">Error</div>
      </div>
      
      <div class="DivInformacionContenido">
         <asp:UpdatePanel ID="ActualizarTablaError" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               <asp:Panel ID="PanelTablaError" runat="server">
                  <br />
                  <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                     <table class="TablaFormulario">
                        <tr>
                           <td>
                              <asp:Label CssClass="TitleTextoError" ID="EtiquetaMensajeTitulo" runat="server" Text="">
                                 No cuenta con los permisos necesarios para entrar a esa página.
                              </asp:Label>
                           </td>
                        </tr>
                        <tr>
                           <td>
                              <asp:Label CssClass="TextoError" ID="EtiquetaMensajeDetalle" runat="server" Text="">
                                 El rol al que pertenece su cuenta de usuario no posee los permisos necesarios para entrar a esa página,
                                 consulte a su administrador de sistemas.
                              </asp:Label>
                              <br /><br />
                           </td>
                        </tr>
                        <tr>
                           <td>
                              <asp:ImageButton AlternateText="Menu" ID="BotonMenu" OnClientClick="goToMenu();" ImageUrl="/Imagen/Boton/BotonMenu.png" runat="server" />
                           </td>
                        </tr>
                     </table> 
                  </asp:Panel> 
                  
               </asp:Panel>
               <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaError" ID="ProgresoTablaError" runat="server">
                  <ProgressTemplate>
                      <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                  </ProgressTemplate>
               </asp:UpdateProgress> 
            </ContentTemplate>
            
            <Triggers>
            </Triggers>
         </asp:UpdatePanel> 
      </div> 
        
   </div> 
</asp:Content>
