<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExistenciaProductoViewer.aspx.cs" Inherits="Activos.Almacen.Aplicacion.Reporte.Viewer.ExistenciaProductoViewer" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID = "ReporteExistencia" EnablePageMethods ="true" runat="server">
    
    
    </asp:ScriptManager>
    <div>
    
        <rsweb:ReportViewer ID="ExistenciaProductoReporteViewer" runat="server" Font-Names="Verdana" SizeToReportContent="true" 
            Font-Size="8pt" Width="100%">
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
    </form>
</body>
</html>
