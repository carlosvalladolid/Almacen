<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="CambiarContrasenia.aspx.cs" Inherits="Almacen.Web.Aplicacion.Configuracion.CambiarContrasenia" %>

<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaPrivada.Master" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">

    <script language="javascript" src="/Incluir/Javascript/ValidarFormulario.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="LeftBodyDiv">
        <wuc:ControlMenuIzquierdo ID="ControlMenuIzquierdo" runat="server" />
    </div>
    
    <div class="RightBodyDiv">
        <div class="PageTitleDiv">
            <table class="PageTitleTable">
                <tr>
                    <td class="Title">
                    Cambiar Contraseña
                    </td>
               </tr>
            </table>
        </div>

        <asp:UpdatePanel ID="PanelContrasenia" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="PanelContrasena" runat="server">
                    <table border="0" class="TablaFormulario">
                        <tr><br />
                            <td class="Nombre">Contraseña anterior</td>
                            <td class="Espacio">&nbsp;</td>
                            <td class="Campo"><asp:TextBox ID="AnteriorContrasenia" MaxLength="20" runat="server" TextMode="Password" Width="200px"></asp:TextBox></td>
                            <td class="Error"></td>
                        </tr>
                        <br />
                        <tr>
                            <td class="Nombre">Contraseña nueva</td>
                            <td class="Espacio">&nbsp;</td>
                            <td class="Campo"><asp:TextBox ID="NuevaContrasenia" MaxLength="20" runat="server" TextMode="Password" Width="200px"></asp:TextBox></td>
                            <td class="Error"></td>
                        </tr>
                        <br />
                        <tr>
                            <td class="Nombre">Confirmar contraseña</td>
                            <td class="Espacio">&nbsp;</td>
                            <td class="Campo"><asp:TextBox ID="Confirmacion" MaxLength="20" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                            <td class="Error"></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="AnteriorContrasenia" Display="Dynamic" ErrorMessage="" ID="AnteriorContraseniaRequerida" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NuevaContrasenia" Display="Dynamic" ErrorMessage="" ID="NuevaContraseniaRequerida" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="Confirmacion" Display="Dynamic" ErrorMessage="" ID="ConfirmacionRequerida" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                <asp:Label CssClass="TextoError" ID="MessageLabel" runat="server" Text=""></asp:Label>
                                <br />
                                    
                                <asp:ImageButton AlternateText="Guardar" ID="BotonGuardar" ImageUrl="/Imagen/Boton/BotonGuardar.png" OnClick="BotonGuardar_Click" runat="server" ValidationGroup="Guardar" />&nbsp;&nbsp;
                                <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelar" ImageUrl="/Imagen/Boton/BotonCancelar.png" OnClick="BotonCancelar_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:HiddenField ID="ControlComeFromIdHidden" runat="server" Value="1" />
            </ContentTemplate>
                
            <Triggers>
                    
            </Triggers>
        </asp:UpdatePanel>
    </div>

</asp:Content>
