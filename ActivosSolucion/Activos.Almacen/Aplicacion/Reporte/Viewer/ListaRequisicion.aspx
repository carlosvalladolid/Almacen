<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaRequisicion.aspx.cs" Inherits="Activos.Almacen.Aplicacion.Reporte.Viewer.ListaRequisicion" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reporte de Listado Requisicion</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <rsweb:ReportViewer ID="ListaRequisicionReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="400px" Width="931px">
            <LocalReport ReportPath="Aplicacion\Reporte\Diseño\ListaRequisicionRPT.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                        Name="ListaRequisicionDS_ListaRequisicionDT" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetData" TypeName="Almacen.Web.ListaRequisicionDSTableAdapters.">
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
