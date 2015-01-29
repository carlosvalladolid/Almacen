<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="ExistenciaProducto.aspx.cs" Inherits="Almacen.Web.Aplicacion.Reporte.Viewer.ExistenciaProducto" Title="" %>

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
                                Existencia producto
                            </td>
                            <td class="Search"></td>
                            <td class="Icon"></td> 
                        </tr>
                    </table>
                </div>

                <table class="TablaFormulario">
                    <tr>
                        <td class="Nombre">Familia</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="FamiliaCombo" runat="server" ></asp:DropDownList></td>
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
                    <rsweb:ReportViewer ID="ExistenciaProductoReporteViewer" runat="server" Font-Names="Verdana" 
                        Font-Size="10pt" Width="100%" Height="100%">
                        <LocalReport ReportPath="/Aplicacion/Reporte/Diseño/ExistenciProductoRPT.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                                    Name="ExistenciaPoductoDS_ExistenciaProductoDT" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        SelectMethod="GetData" TypeName="Almacen.Web.ExistenciaPoductoDSTableAdapters.">
                    </asp:ObjectDataSource>
                </div>

                <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="../../Image/Icon/LoadingIcon.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
