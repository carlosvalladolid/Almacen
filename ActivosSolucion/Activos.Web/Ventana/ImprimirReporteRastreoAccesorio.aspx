<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaReporteImpresion.Master" AutoEventWireup="true" CodeBehind="ImprimirReporteRastreoAccesorio.aspx.cs" Inherits="Activos.Web.Ventana.ImprimirReporteRastreoAccesorio" EnableViewStateMac="False" %>
<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaReporteImpresion.Master" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
</asp:Content>
<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   <div class="DivContenido">
      <div class="DivInformacionContenido">
         <div>
            <div class="DivImpresionFecha">
               <table class="TablaImpresionFecha">
                  <tr>
                     <td class="NombreFecha">Fecha:</td>
                     <td class="CampoFecha"><asp:Label ID="FechaLabel" runat="server" Text=""></asp:Label></td>
                  </tr>
               </table>
            </div>
         </div>
         <br /><br /><br />
         
         <table class="TablaImpresionEmpleado">
            <tr>
               <td class="NombreImpresionEmpleado">CÓDIGO DE BARRAS:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="CodigoBarrasLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">DESCRIPCIÓN:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="DescripcionLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">NÚMERO DE SERIE:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="NumeroSerieLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">MODELO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="ModeloLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">MARCA:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="MarcaLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">DIRECCIÓN:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="DireccionLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">DEPARTAMENTO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="DepartamentoLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">EDIFICIO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="EdificioLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">FOLIO DEL DOCUMENTO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="FolioDocumentoLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">PROVEEDOR:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="ProveedorLabel" runat="server" Text=""></asp:Label></td>
            </tr>
         </table>
         
         <br />
         <div class="DivSubtituloImpresion">    
            MOVIMIENTOS
         </div>
         <br /><br />
         
         <div ID="DivTabla">
             <asp:GridView ID="TablaMovimientos" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion">
                 <EmptyDataTemplate>
                     <table class="TablaVacia">
                         <tr class="Encabezado">
                             <th style="width: 100px;">FECHA</th>
                             <th style="width: 150px;">TIPO DE MOVIMIENTO</th>
                             <th>ACTIVO QUE LO TIENE ASIGNADO</th>
                         </tr>
                         <tr>
                             <td colspan="3" style="text-align: center;">
                                 No se encontró información con los parámetros seleccionados</td>
                         </tr>
                     </table>
                 </EmptyDataTemplate>
                 <HeaderStyle CssClass="EncabezadoImpresion" />
                 <PagerStyle CssClass="PaginacionImpresion" HorizontalAlign="Right" />
                 <Columns>
                     <asp:BoundField DataField="Fecha" HeaderText="FECHA" ItemStyle-HorizontalAlign="Left">
                         <HeaderStyle HorizontalAlign="Center" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="TipoMovimientoNombre" HeaderText="TIPO DE MOVIMIENTO" ItemStyle-HorizontalAlign="Left">
                         <HeaderStyle HorizontalAlign="Center" Width="150px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="ActivoPadre" HeaderText="ACTIVO QUE LO TIENE ASIGNADO" ItemStyle-HorizontalAlign="Left">
                         <HeaderStyle HorizontalAlign="Center" />
                     </asp:BoundField>
                 </Columns>
             </asp:GridView>
         </div>
         
      </div> 
   </div>
   <script language="javascript" type="text/javascript">

     function ImprimirPantalla() {
         window.print();
     }

     ImprimirPantalla();
     
   </script>
</asp:Content>
