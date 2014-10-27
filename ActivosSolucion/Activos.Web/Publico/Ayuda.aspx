<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPublica.Master" AutoEventWireup="true" CodeBehind="Ayuda.aspx.cs" Inherits="Activos.Web.Publico.Ayuda" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="DivContenido">
        <div class="DivContenidoAyuda">
            <div class="DivTitulo">Ayuda</div>

            <br />
            <b>Problemas para acceder a la aplicación</b>
            <br />
            Si no puede acceder a la aplicación, verifique que está escribiendo correctamente su cuenta de usuario y contraseña. Si
            está seguro que se encuentran bien escritas, verifique que la aplicación no esté desplegando un mensaje de error.
            <br /><br />
            Para obtener más ayuda puede enviar un comentario al administrador del sistema desde <a href="/Publico/Contacto.aspx">aquí</a>.

            <br /><br />
            <b>No cuenta con un usuario</b>
            <br />
            Para poder acceder a la aplicación debe poseer una cuenta de usuario y una contraseña. Si no posee una, y cree que
            debería poseer una, envíe un comentario al administrador del sistema desde <a href="/Publico/Contacto.aspx">aquí</a>.

            <br /><br />
            <b>Cuenta inactiva</b>
            <br />
            Si al ingresar su usuario y contraseña le aparece un mensaje de cuenta inactiva, es porque el administrador
            del sistema ha bloqueado su cuenta. Si cree que su cuenta no debería estar inactiva, envíe un comentario al
            administrador del sistema desde <a href="/Publico/Contacto.aspx">aquí</a>.

            <br /><br /><br />
            <a href="/Inicio.aspx"><< Regresar al inicio</a>
        </div>
    </div>
</asp:Content>
