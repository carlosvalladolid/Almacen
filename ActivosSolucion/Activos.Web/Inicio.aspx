<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPublica.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Activos.Web.Inicio" Title="" %>

<%@ Register TagPrefix="wuc" TagName="Identificacion" Src="~/Incluir/ControlesWeb/ControlIdentificacion.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="DivContenido">
        <div class="DivContenidoIzquierdoIdentificacion">
            <div class="DivTitulo">Activos</div>

            La apliación de Activos es una aplicación de administración para el Instituto de Defensoría Pública de Nuevo León.

            <br />
            <div class="DivLogotipoInstituto">
                <img alt="" src="/Imagen/Logotipo/LogotipoInstituto.png" />
            </div>

            <br />
            Si tiene alguna duda, puede revisar la sección de <a href="/Publico/Ayuda.aspx">ayuda</a>.

            <br />
            Para sugerencias y comentarios <a href="/Publico/Contacto.aspx">dé clic aquí</a>.
        </div>

        <div class="DivContenidoSeparadorIdentificacion">
            <img alt="" src="/Imagen/Decoracion/DecoracionSeparadorIdentificacion.jpg" />
        </div>

        <div class="DivContenidoControlIdentificacion">
            <wuc:Identificacion ID="ControlIdentificacion" runat="server" />
        </div>
    </div>
</asp:Content>
