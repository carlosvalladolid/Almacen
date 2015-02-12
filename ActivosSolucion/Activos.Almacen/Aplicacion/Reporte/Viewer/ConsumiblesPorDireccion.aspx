<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="ConsumiblesPorDireccion.aspx.cs" Inherits="Activos.Almacen.Aplicacion.Reporte.Viewer.ConsumiblesPorDireccion" %>

<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaPrivada.Master" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="LeftBodyDiv">
        <wuc:ControlMenuIzquierdo ID="ControlMenuIzquierdo" runat="server" />
    </div>

    <div class="RightBodyDiv">
        <asp:UpdatePanel ID="PageUpdate" runat="server">
            <ContentTemplate>
                <div class="PageTitleDiv">
                    <table class="PageTitleTable">
                        <tr>
                            <td class="Title">
                               Consumo de Articulos por Dirección
                            </td>
                            <td class="Search"></td>
                            <td class="Icon"></td> 
                        </tr>
                    </table>
                </div>

                <table class="TablaFormulario">
                     <tr>
                        <td class="Nombre">Dirección</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="DireccionCombo" runat="server" ></asp:DropDownList></td>
                    </tr>
                    
                    <tr>
                        <td colspan="3">
                            <br />
                            <asp:ImageButton AlternateText="Buscar" ID="BotonBusqueda" ImageUrl="/Imagen/Boton/BotonBuscar.png" OnClick="BotonBusqueda_Click" runat="server" />&nbsp;&nbsp;
                            <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarBusqueda" ImageUrl="/Imagen/Boton/BotonCancelar.png" OnClick="BotonCancelarBusqueda_Click" runat="server" />
                        </td>
                    </tr>
                </table>

                <div class="DivTabla">
                  
                </div>

                <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="../../Image/Icon/LoadingIcon.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
            </ContentTemplate>
            <Triggers > 
                <asp:PostBackTrigger ControlID="BotonBusqueda" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>