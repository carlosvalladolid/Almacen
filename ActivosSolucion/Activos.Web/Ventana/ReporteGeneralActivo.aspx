<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaReporteImpresion.Master" AutoEventWireup="true" CodeBehind="ReporteGeneralActivo.aspx.cs" Inherits="Activos.Web.Ventana.ReporteGeneralActivo" EnableViewStateMac="False" %>
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
               <td class="NombreImpresionEmpleado">FAMILIA:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="FamiliaLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">SUBFAMILIA:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="SubfamiliaLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">MARCA:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="MarcaLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">MODELO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="ModeloLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">FECHAS:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="FechasLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">PROVEEDOR:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="ProveedorLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">FOLIO DOCUMENTO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="FolioDocumentoLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">DIRECCIÓN:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="DireccionLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">DEPARTAMENTO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="DepartamentoLabel" runat="server" Text=""></asp:Label></td>
            </tr>
         </table>
         
         <br /><br />
         
         <div ID="DivTabla">
             <asp:GridView ID="TablaActivos" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion" DataKeyNames="ActivoId">
                 <EmptyDataTemplate>
                     <table class="TablaVacia">
                         <tr class="Encabezado">
                            <th style="width: 120px;">C.B.</th>
                            <th>DESCRIPCIÓN</th>
                            <th style="width: 100px;">MODELO</th>
                            <th style="width: 120px;">NO. SERIE</th>
                            <th style="width: 150px;">QUIEN LO TIENE ASIGNADO</th>
                            <th style="width: 100px;">MARCA</th>
                            <th style="width: 100px;">EDIFICIO</th>
                         </tr>
                         <tr>
                             <td colspan="7" style="text-align: center;">
                                 No se encontró información con los parámetros seleccionados</td>
                         </tr>
                     </table>
                 </EmptyDataTemplate>
                 <HeaderStyle CssClass="EncabezadoImpresion" />
                 <PagerStyle CssClass="PaginacionImpresion" HorizontalAlign="Right" />
                 <Columns>
                     <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="C.B." ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Modelo" HeaderText="MODELO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="NumeroSerie" HeaderText="NO. SERIE" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="EmpleadoNombreCompleto" HeaderText="QUIEN LO TIENE ASIGNADO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="MarcaNombre" HeaderText="MARCA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="EdificioNombre" HeaderText="EDIFICIO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
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
