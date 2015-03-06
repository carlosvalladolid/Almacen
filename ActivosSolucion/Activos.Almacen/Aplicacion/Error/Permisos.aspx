<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="Activos.Almacen.Aplicacion.Error.Permisos" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorCuerpo" runat="server">

<div class="MensajePermisos">
<br/><br/>
<img src="../../Imagen/Banner/Denegado.png"/><br/><br/>
No tiene los permisos para entrar a esta página, si necesita información acerca de su rol, por favor comuniquese con el administrador.
<br/>
Para regresar a la página de inico haga clic <a href="../Inicio.aspx">aquí</a>.
</div>

</asp:Content>
