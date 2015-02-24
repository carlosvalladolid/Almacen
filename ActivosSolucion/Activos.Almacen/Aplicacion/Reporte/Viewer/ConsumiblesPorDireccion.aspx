<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="ConsumiblesPorDireccion.aspx.cs" Inherits="Almacen.Web.Aplicacion.Reporte.Viewer.ConsumiblesPorDireccion" %>

<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaPrivada.Master" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    
    <link href="/Incluir/Estilo/Privado/jquery-ui-1.8.16.custom.css" rel="Stylesheet" type="text/css" />
    <link href="/Incluir/Estilo/Privado/demos.css" rel="Stylesheet" type="text/css" />

    <script src="/Incluir/Javascript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="/Incluir/Javascript/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <script src="/Incluir/Javascript/Calendar.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad(sender, args)
        {
            SetNewCalendar("#<%= FechaDesde.ClientID %>");
            SetNewCalendar("#<%= FechaHasta.ClientID %>");
                }
    </script>
    <style type="text/css">
        .style1
        {
            width: 415px;
        }
    </style>
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
                        <td class="Nombre">Fechas</td>
                        <td class="Espacio">&nbsp;</td>
                        <td class="style1">Desde&nbsp;<asp:TextBox CssClass="CajaTextoPequenia" ID="FechaDesde" MaxLength="20" runat="server" Text=""></asp:TextBox>
                                           Hasta&nbsp;<asp:TextBox CssClass="CajaTextoPequenia" ID="FechaHasta" MaxLength="20" runat="server" Text=""></asp:TextBox>                                          
                                           <span class="NotaCampo"> (dd/mm/aaaa)</span>
                                           </td>
                                           <td></td>
                     </tr>  
                     <tr>
                        <td class="Nombre">Dirección</td>
                        <td class="Espacio"></td>
                        <td class="Campo" colspan  ="2"><asp:DropDownList CssClass="ComboGrande" ID="DireccionCombo" runat="server" ></asp:DropDownList></td>                        
                    </tr>
                    
                     <tr>
                        <td class="Nombre">Estatus</td>
                        <td class="Espacio"></td>
                        <td class="Campo" colspan  ="2"><asp:DropDownList CssClass="ComboGrande" ID="EstatusCombo" runat="server" ></asp:DropDownList></td>                        
                    </tr>
                    
                    
                    <tr>
                        <td colspan="4">
                            <br />
                            <asp:ImageButton AlternateText="Buscar" ID="BotonBusqueda" ImageUrl="/Imagen/Boton/BotonBuscar.png" OnClick="BotonBusqueda_Click" runat="server" />&nbsp;&nbsp;
                            <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarBusqueda" ImageUrl="/Imagen/Boton/BotonCancelar.png" OnClick="BotonCancelarBusqueda_Click" runat="server" />
                        </td>
                    </tr>
                </table>

                <div class="DivTabla">
                    <rsweb:ReportViewer ID="ConsumiblePorDireccionReporteViewer" runat="server" Font-Names="Verdana"
                     Font-Size="10pt" Width="100%" Height="100%">
                        
                        <LocalReport ReportPath="Aplicacion/Reporte/Diseño/ConsumoPorDireccionRPT.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                                    Name="ConsumoPorDireccionDS_ConsumoPorDireccionDT" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        SelectMethod="GetData" TypeName="Almacen.Web.ConsumoPorDireccionDSTableAdapters.">                    
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