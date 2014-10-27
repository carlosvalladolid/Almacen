<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaReporteImpresion.Master" AutoEventWireup="true" CodeBehind="ImprimirReporteGeneralMantenimientos.aspx.cs" Inherits="Activos.Web.Ventana.ImprimirReporteGeneralMantenimientos"  EnableViewStateMac="False"%>
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
               <td class="NombreImpresionEmpleado">TIPO DE MANTENIMIENTO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="TipoMantenimientoLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">TIPO DE REPORTE:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="TipoReporteLabel" runat="server" Text=""></asp:Label></td>
            </tr>
         </table>
         
         <br /><br />
         
         <div ID="DivTablaReporteConcentrado" visible="false" runat="server">
         <div class="DivSubtituloImpresion">    
           REPORTE CONCENTRADO
         </div>
             <asp:GridView ID="TablaConcentrados" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion">
                 <EmptyDataTemplate>
                    <table class="TablaVacia">
                         <tr class="Encabezado">
                            <th style="width: 300px;">TIPO DE ASISTENCIA</th>
                            <th style="width: 100px;">CANTIDAD</th>
                         </tr>
                         <tr>
                             <td colspan="7" style="text-align: center;">
                                 No se encontró información con los parámetros seleccionados
                             </td>
                         </tr>
                    </table>
                  </EmptyDataTemplate>
                  <HeaderStyle CssClass="EncabezadoImpresion" />
                  <PagerStyle CssClass="PaginacionImpresion" HorizontalAlign="Right" />
                  <Columns>
                     <asp:BoundField DataField="TipoAsistenciaNombre" HeaderText="TIPO DE ASISTENCIA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="300px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Cantidad" HeaderText="CANTIDAD" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                     </asp:BoundField>
                 </Columns>
             </asp:GridView>
         </div>
         
         <br /><br />
         
         <div ID="DivTablaReporteDesglosado" visible="false" runat="server">
         <div class="DivSubtituloImpresion">    
            REPORTE DESGLOSADO
         </div>
             <asp:GridView ID="TablaDesglosados" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion">
                 <EmptyDataTemplate>
                     <table class="TablaVacia">
                         <tr class="Encabezado">
                            <th style="width: 100px;">FECHA</th>
                            <th style="width: 100px;">TIPO DE ASISTENCIA</th>
                            <th style="width: 100px;">C.B</th>
                            <th style="width: 150px;">EMPLEADO ASIGNADO</th>
                            <th>DESCRIPCIÓN</th>
                            <th style="width: 200px;">EMPLEADOS QUE ATENDIERON</th>
                            <th style="width: 100px;">TIPO DE MANTENIMIENTO</th>
                        </tr>
                        <tr>
                            <td colspan="7" style="text-align: center;">
                                 No se encontró información con los parámetros seleccionados
                            </td>
                        </tr>
                    </table>
                 </EmptyDataTemplate>
                 <HeaderStyle CssClass="EncabezadoImpresion" />
                 <PagerStyle CssClass="PaginacionImpresion" HorizontalAlign="Right" />
                 <Columns>
                     <asp:BoundField DataField="FechaInserto" HeaderText="FECHA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="TipoAsistenciaNombre" HeaderText="TIPO DE ASISTENCIA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                     </asp:BoundField>
                      <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="C.B." ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                     </asp:BoundField>
                     <asp:BoundField DataField="NombreEmpleadoAsignado" HeaderText="EMPLEADO ASIGNADO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="150px"/>
                     </asp:BoundField>
                     <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="EmpleadosAtendieron" HeaderText="EMPLEADOS QUE ATENDIERON" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="TipoMantenimientoNombre" HeaderText="TIPO DE MANTENIMIENTO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100PX" />
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

