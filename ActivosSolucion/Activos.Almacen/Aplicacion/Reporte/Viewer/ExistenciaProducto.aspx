<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="ExistenciaProducto.aspx.cs" Inherits="Activos.Almacen.Aplicacion.Reporte.Viewer.ExistenciaProducto" Title="" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="LeftBodyDiv">
        <wuc:ControlMenuIzquierdo ID="ControlMenuIzquierdo" runat="server" />
    </div>

    <div class="RightBodyDiv">
        <rsweb:ReportViewer ID="ExistenciaProductoReporteViewer" runat="server" Font-Names="Verdana" 
            Font-Size="10pt" Width="100%" Height="100%">
            <LocalReport ReportPath="Aplicacion\Reporte\Diseño\ExistenciProductoRPT.rdlc">
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
</asp:Content>
