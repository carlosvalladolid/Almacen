<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="ImprimirRequisicion.aspx.cs" Inherits="Activos.Almacen.Aplicacion.Reporte.Viewer.ImprimirRequisicion" %>

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
               
                <table>
                    <tr>
                        <td>
                        
                        </td>
                    </tr>
                </table>

                <div class="DivTabla">
                    <rsweb:ReportViewer ID="ImprimirRequisicionReporteViewer" runat="server" Font-Names="Verdana" 
                        Font-Size="10pt" Width="100%" Height="100%">
                        <LocalReport ReportPath="Aplicacion/Reporte/Diseño/ImprimirRequisicionRPT.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                                    Name="ImprimirRequisicionDS_ImprimirRequisicionDT" />
                            </DataSources>
                        </LocalReport>
                        
                    </rsweb:ReportViewer>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        SelectMethod="GetData" TypeName="Almacen.Web.ImprimirRequisicionDSTableAdapters.">
                    </asp:ObjectDataSource>
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

