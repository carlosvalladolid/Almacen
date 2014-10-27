<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaReporteImpresion.Master" AutoEventWireup="true" CodeBehind="ImprimirReporteMantenimientosPorTecnico.aspx.cs" Inherits="Activos.Web.Ventana.ImprimirReporteMantenimientosPorTecnico" EnableViewStateMac="False" %>
<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaReporteImpresion.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
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
               <td class="NombreImpresionEmpleado">TIPO DE MANTENIMIENTO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="TipoMantenimientoLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">TÉCNICO QUE ATENDIÓ:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="TecnicoAtendioLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">TIPO REPORTE:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="TipoReporteLabel" runat="server" Text=""></asp:Label></td>
            </tr>
         </table>
         <br /><br />
         
         <div id="DivTablaConcentrado" runat="server" visible="false">
             <asp:GridView ID="TablaConcentrado" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion">
                 <EmptyDataTemplate>
                     <table class="TablaVacia">
                         <tr class="Encabezado">
                              <th>Técnico que atendió</th>
                              <th style="width: 100px;">Asistencia</th>
                              <th style="width: 50px;">Cantidad</th>
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
                    <asp:BoundField DataField="NombreEmpleadoAtendio" HeaderText="Técnico que atendió" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TipoAsistenciaNombre" HeaderText="Asistencia" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>
                 </Columns>
             </asp:GridView>
         </div>
         
         <div id="DivTablaDesglosado" runat="server" visible="false">
             <asp:GridView ID="TablaDesglosado" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion">
                 <EmptyDataTemplate>
                     <table class="TablaVacia">
                         <tr class="Encabezado">
                              <th style="width: 80px;">Folio</th>
                              <th style="width: 100px;">Fecha</th>
                              <th style="width: 80px;">Asistencia</th>
                              <th style="width: 80px;">Mantenimiento</th>
                              <th style="width: 100px;">C.B.</th>
                              <th style="width: 150px;">Empleado que lo tiene asignado</th>
                              <th>Descripción mantenimiento</th>
                              <th style="width: 200px;">Técnico que atendió</th>
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
                    <asp:BoundField DataField="MantenimientoId" HeaderText="Folio" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TipoAsistenciaNombre" HeaderText="Asistencia" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TipoMantenimientoNombre" HeaderText="Mantenimiento" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="C.B." ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NombreEmpleadoResguardo" HeaderText="Empleado que lo tiene asignado" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción mantenimiento" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NombreEmpleadoAtendio" HeaderText="Quien atendió" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
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
