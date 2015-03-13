<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FechaVencimientoFactura.aspx.cs" Inherits="Activos.Almacen.Aplicacion.Reporte.Viewer.FechaVencimientoFactura" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Vencimiento de Facturas</title>    
        <link href="/Incluir/Estilo/Privado/Alert.css" rel="Stylesheet" type="text/css" />
        <link href="/Incluir/Estilo/Privado/Control.css" rel="Stylesheet" type="text/css" />
        <link href="/Incluir/Estilo/Privado/Division.css" rel="Stylesheet" type="text/css" />
        <link href="/Incluir/Estilo/Privado/Principal.css" rel="Stylesheet" type="text/css" />
        <link href="/Incluir/Estilo/Privado/Tabla.css" rel="Stylesheet" type="text/css" />
        <link href="/Incluir/Estilo/Privado/Texto.css" rel="Stylesheet" type="text/css" />
        <link href="/Incluir/Estilo/Privado/Popup.css" rel="Stylesheet" type="text/css" />


        <script src="/Incluir/Javascript/jquery-1.6.4.min.js" type="text/javascript"></script>
        <script src="/Incluir/Javascript/jquery.blockUI.js" type="text/javascript"></script>
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
  <%--  <style type="text/css">
        .style2
        {
            width: 238px;
        }
        .style3
        {
            width: 625px;
        }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
    
             <div class="PageTitleDiv">
                    <table class="PageTitleTable">
                        <tr>
                            <td class="Title">
                               Listado de Fecha de Vencimientos de Facturas
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
                        <td class="style3">Desde&nbsp;<asp:TextBox CssClass="CajaTextoPequenia" ID="FechaDesde" MaxLength="20" runat="server" Text=""></asp:TextBox>
                                           Hasta&nbsp;<asp:TextBox CssClass="CajaTextoPequenia" ID="FechaHasta" MaxLength="20" runat="server" Text=""></asp:TextBox>                                          
                                           <span class="NotaCampo"> (dd/mm/aaaa)</span>
                                           </td>
                                           <td class="style2"></td>
                     </tr>  
                     <tr>
                        <td class="Nombre">Proveedor</td>
                        <td class="Espacio"></td>
                        <td class="Campo" colspan  ="2"><asp:DropDownList CssClass="ComboGrande" 
                                ID="ProveedorCombo" runat="server" Height="16px" Width="338px" ></asp:DropDownList></td>                        
                    </tr>                 
                  
                    <tr>
                        <td colspan="4">
                            <br />
                            <asp:ImageButton AlternateText="Buscar" ID="BotonBusqueda" ImageUrl="/Imagen/Boton/BotonBuscar.png" OnClick="BotonBusqueda_Click" runat="server" />&nbsp;&nbsp;
                            <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarBusqueda" ImageUrl="/Imagen/Boton/BotonCancelar.png" OnClick="BotonCancelarBusqueda_Click" runat="server" />
                        </td>
                    </tr>
                </table>
    
    <div>    
        <rsweb:ReportViewer ID="VencimientoFacturaReportViewer1" ShowPrintButton="true" runat="server" Font-Names="Verdana" 
            Font-Size="10pt" Height="400px" Width="963px">
            <LocalReport ReportPath="Aplicacion\Reporte\Diseño\VencimientoFacturaRPT.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                        Name="VencimientoFacturaDS_VencimientoFacturaDT" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetData" 
            TypeName="Almacen.Web.VencimientoFacturaDSTableAdapters.">
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
