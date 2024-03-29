﻿<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPublica.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Almacen.Web.Inicio" Title="" %>

<%@ Register TagPrefix="wuc" TagName="Identificacion" Src="~/Incluir/ControlesWeb/ControlIdentificacion.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">

</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div id="ContentDiv">
        <div id="ContentLoginLeftDiv">
            <img alt="Instituto de Defensoría Pública de Nuevo León" src="/Imagen/Logotipo/EncabezadoIDP3_flama.png" />
            <br />
            <br />
            Panel de Control es una aplicación interna de administración para el manejo de Almacén.
            <br />
            <div class="SoftwarePackageIcon">
                <img alt="" src="/Imagen/Icono/SoftwarePackageIcon.jpg" />
            </div>
            <br />
            Para sugerencias y comentarios contacte al administrador del sistema.
        </div>

        <div id="ContentSeparatorDiv">
            <img alt="" src="/Imagen/Decoracion/LoginSeparatorDecoration.jpg" />
        </div>

        <div id="LoginDiv">
            <wuc:Identificacion ID="ControlIdentificacion" runat="server" />
        </div>
    </div>
</asp:Content>
