﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExistenciaProductoViewer.aspx.cs" Inherits="Activos.Almacen.Aplicacion.Reporte.Viewer.ExistenciaProductoViewer" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

    <style type="text/css">
        html, body, form
        {
	        height: 100%;
	        width: 100%;
        }
    </style>    
</head>

<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID = "ReporteExistencia" EnablePageMethods ="true" runat="server">
    
    
    </asp:ScriptManager>
    <div> 
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
    </form>
</body>
</html>
