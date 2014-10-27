<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPublica.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="Activos.Web.Publico.Contacto" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="DivContenido">
        <div class="DivContenidoComentarios">
            <div class="DivTitulo">Comentarios</div>

            <table class="Tabla">
                <tr><td colspan="3">Por favor llene todos los campos del siguiente formulario para enviarnos su comentario</td></tr>
                <tr>
                    <td class="Name">Nombre</td>
                    <td></td>
                    <td><asp:TextBox CssClass="CajaTextoMediana" ID="TextBox1" MaxLength="50" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="Name">Correo electrónico</td>
                    <td></td>
                    <td><asp:TextBox CssClass="CajaTextoMediana" ID="TextBox2" MaxLength="65" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="Name">Comentario</td>
                    <td></td>
                    <td><asp:TextBox CssClass="CajaTextoMediana" ID="TextBox3" Rows="8" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="BotonAceptar" runat="server" Text="Aceptar" />
                    </td>
                </tr>
            </table>

            <a href="/Inicio.aspx"><< Regresar al inicio</a>
        </div>
    </div>
</asp:Content>
