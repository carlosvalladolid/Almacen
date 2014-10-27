<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaReporteImpresion.Master" AutoEventWireup="true" CodeBehind="ImprimirReporteMantenimientosPorActivo.aspx.cs" Inherits="Activos.Web.Ventana.ImprimirReporteMantenimientosPorActivo" EnableViewStateMac="False"%>
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
               <td class="NombreImpresionEmpleado">FECHAS:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="FechasLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">ESTATUS:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="EstatusLabel" runat="server" Text=""></asp:Label></td>
            </tr>
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
               <td class="NombreImpresionEmpleado">MARCA:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="MarcaLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">MODELO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="ModeloLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">EMPLEADO ASIGNADO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="EmpleadoAsignadoLabel" runat="server" Text=""></asp:Label></td>
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
            MANTENIMIENTOS
         </div>
         <br /><br />
         
         <div ID="DivTabla">
             <asp:GridView ID="TablaMantenimientos" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion">
                 <EmptyDataTemplate>
                     <table class="TablaVacia">
                         <tr class="Encabezado">
                             <th style="width: 100px;">FECHA</th>
                             <th style="width: 300px;">TÉCNICO QUE ATENDIÓ</th>
                             <th style="width: 150px;">TIPO DE ASISTENCIA</th>
                             <th style="width: 300px;">DESCRIPCIÓN</th>
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
                     <asp:BoundField DataField="FechaInserto" HeaderText="FECHA" ItemStyle-HorizontalAlign="Left">
                         <HeaderStyle HorizontalAlign="Center" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="EmpleadosNombre" HeaderText="TÉCNICO QUE ATENDIÓ" ItemStyle-HorizontalAlign="Left">
                         <HeaderStyle HorizontalAlign="Center" Width="300px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="TipoAsistenciaNombre" HeaderText="TIPO DE ASISTENCIA" ItemStyle-HorizontalAlign="Left">
                         <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                     </asp:BoundField>
                     <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-HorizontalAlign="Left">
                         <HeaderStyle HorizontalAlign="Center" Width="300px" />
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
