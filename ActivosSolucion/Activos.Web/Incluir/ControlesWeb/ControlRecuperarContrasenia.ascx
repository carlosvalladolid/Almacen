<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlRecuperarContrasenia.ascx.cs" Inherits="Activos.Web.Incluir.ControlesWeb.ControlRecuperarContrasenia" %>

<asp:UpdatePanel ID="ActualizarRecuperar" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel CssClass="SuperposicionDiv" ID="PanelFondo" runat="server" Visible="False"></asp:Panel>

        <asp:Panel CssClass="PopupChicoDiv" ID="PanelRecuperar" runat="server" Visible="False">
            <div class="EncabezadoDiv"><asp:Label ID="EtiquetaTitulo" Text="Recuperar contraseña" runat="server"></asp:Label></div>

            <div class="ContenidoDiv">
                <table class="TablaContenido">
                    <tr>
                        <td colspan="3"><asp:Label CssClass="EtiquetaSubtitulo" ID="EtiquetaSubtitulo" runat="server" Text="Proporcione el correo electrónico de su cuenta para recuperar la contraseña"></asp:Label></td>
                    </tr>
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td class="Nombre"><asp:Label ID="EtiquetaCorreo" Text="Correo electrónico" runat="server"></asp:Label></td>
                        <td class="Espacio">&nbsp;</td>
                        <td class="Campo"><asp:TextBox ID="CuentaUsuario" MaxLength="65" runat="server" Width="200px"></asp:TextBox></td>
                    </tr>
                </table>

                <br /><br />
                <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CuentaUsuario" Display="Dynamic" ErrorMessage="" ID="CorreoRequerido" runat="server" SetFocusOnError="true" ValidationGroup="EnviarContrasenia"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="TextoError" ControlToValidate="CuentaUsuario" Display="Dynamic" ErrorMessage="" ID="CorreoExpresion" runat="server" SetFocusOnError="true" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$" ValidationGroup="EnviarContrasenia"></asp:RegularExpressionValidator>
                <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
            </div>

            <div class="PieDiv">
                <asp:Button ID="BotonEnviar" onclick="BotonEnviar_Click" runat="server" Text="Aceptar" ValidationGroup="EnviarContrasenia" />&nbsp;&nbsp;
                <asp:Button ID="BotonCancelar" onclick="BotonCancelar_Click" runat="server" Text="Cancelar" />
            </div>
        </asp:Panel>
    </ContentTemplate>

    <Triggers>
        
    </Triggers>
</asp:UpdatePanel>
