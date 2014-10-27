<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaReporteImpresion.Master" AutoEventWireup="true" CodeBehind="ImprimirReporteEstatusActivo.aspx.cs" Inherits="Activos.Web.Ventana.ImprimirReporteEstatusActivo" EnableViewStateMac="False" %>
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
               <td class="CampoImpresionEmpleado"><asp:Label ID="SubFamiliaLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">ASIGNADOS:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="AsignadosLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">SIN ASIGNAR:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="NoAsignadosLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">FUERA DE INSTALACIONES:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="SalidaLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">SIN ETIQUETAR:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="NoEtiquetadosLabel" runat="server" Text=""></asp:Label></td>
            </tr>
         </table>
         
         <br /><br />
         
         <div ID="DivTablaAsignados" visible="false" runat="server">
         <div class="DivSubtituloImpresion">    
           ACTIVOS ASIGNADOS
         </div>
             <asp:GridView ID="TablaAsignados" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion"><EmptyDataTemplate><table class="TablaVacia">
                         <tr class="Encabezado">
                            <th style="width: 100px;">C.B.</th><th>DESCRIPCIÓN</th><th style="width: 100px;">FAMILIA</th><th style="width: 100px;">SUBFAMILIA</th><th style="width: 100px;">MARCA</th><th style="width: 100px;">MODELO</th><th style="width: 100PX;">NO. SERIE</th><th style="width: 150px;">UBICACION</th><th style="width: 100px;">CONDICIONES</th><th style="width: 100px;">FACTURA</th></tr><tr>
                             <td colspan="7" style="text-align: center;">
                                 No se encontró información con los parámetros seleccionados</td></tr></table></EmptyDataTemplate><HeaderStyle CssClass="EncabezadoImpresion" /><PagerStyle CssClass="PaginacionImpresion" HorizontalAlign="Right" /><Columns><asp:BoundField DataField="CodigoBarrasParticular" HeaderText="C.B." ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                      <asp:BoundField DataField="FamiliaNombre" HeaderText="FAMILIA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                     </asp:BoundField>
                     <asp:BoundField DataField="SubFamiliaNombre" HeaderText="SUBFAMILIA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                     </asp:BoundField>
                     <asp:BoundField DataField="MarcaNombre" HeaderText="MARCA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Modelo" HeaderText="MODELO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="NumeroSerie" HeaderText="NO. SERIE" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100PX" />
                     </asp:BoundField>
                     <asp:BoundField DataField="UbicacionActivo" HeaderText="UBICACION" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CondicionNombre" HeaderText="CONDICION" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CompraFolio" HeaderText="FACTURA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                 </Columns>
             </asp:GridView>
         </div>
         
         <br /><br />
         
         <div ID="DivTablaNoAsignados" visible="false" runat="server">
         <div class="DivSubtituloImpresion">    
            ACTIVOS NO ASIGNADOS
         </div>
             <asp:GridView ID="TablaNoAsignados" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion">
                 <EmptyDataTemplate>
                     <table class="TablaVacia">
                         <tr class="Encabezado">
                            <th style="width: 100px;">C.B.</th><th>DESCRIPCIÓN</th><th style="width: 100px;">FAMILIA</th><th style="width: 100px;">SUBFAMILIA</th><th style="width: 100px;">MARCA</th><th style="width: 100px;">MODELO</th><th style="width: 120px;">NO. SERIE</th><th style="width: 150px;">UBICACION</th><th style="width: 100px;">CONDICIONES</th><th style="width: 100px;">FACTURA</th></tr><tr>
                             <td colspan="7" style="text-align: center;">
                                 No se encontró información con los parámetros seleccionados</td></tr></table></EmptyDataTemplate><HeaderStyle CssClass="EncabezadoImpresion" />
                 <PagerStyle CssClass="PaginacionImpresion" HorizontalAlign="Right" />
                 <Columns>
                     <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="C.B." ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                      <asp:BoundField DataField="FamiliaNombre" HeaderText="FAMILIA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                     </asp:BoundField>
                     <asp:BoundField DataField="SubFamiliaNombre" HeaderText="SUBFAMILIA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                     </asp:BoundField>
                     <asp:BoundField DataField="MarcaNombre" HeaderText="MARCA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Modelo" HeaderText="MODELO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="NumeroSerie" HeaderText="NO. SERIE" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100PX" />
                     </asp:BoundField>
                     <asp:BoundField DataField="UbicacionActivo" HeaderText="UBICACION" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CondicionNombre" HeaderText="CONDICION" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CompraFolio" HeaderText="FACTURA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                 </Columns>
             </asp:GridView>
         </div>
         
         
         <br /><br />
         
         <div ID="DivTablaSalida" visible="false" runat="server">
         <div class="DivSubtituloImpresion">    
            ACTIVOS FUERA DE LAS INSTALACIONES
         </div>
             <asp:GridView ID="TablaSalida" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion">
                 <EmptyDataTemplate>
                     <table class="TablaVacia">
                         <tr class="Encabezado">
                            <th style="width: 120px;">C.B.</th><th>DESCRIPCIÓN</th><th style="width: 100px;">FAMILIA</th><th style="width: 100px;">SUBFAMILIA</th><th style="width: 100px;">MARCA</th><th style="width: 100px;">MODELO</th><th style="width: 120px;">NO. SERIE</th><th style="width: 150px;">UBICACION</th><th style="width: 100px;">CONDICIONES</th><th style="width: 100px;">FACTURA</th></tr><tr>
                             <td colspan="7" style="text-align: center;">
                                 No se encontró información con los parámetros seleccionados</td></tr></table></EmptyDataTemplate><HeaderStyle CssClass="EncabezadoImpresion" />
                 <PagerStyle CssClass="PaginacionImpresion" HorizontalAlign="Right" />
                 <Columns>
                     <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="C.B." ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                      <asp:BoundField DataField="FamiliaNombre" HeaderText="FAMILIA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                     </asp:BoundField>
                     <asp:BoundField DataField="SubFamiliaNombre" HeaderText="SUBFAMILIA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                     </asp:BoundField>
                     <asp:BoundField DataField="MarcaNombre" HeaderText="MARCA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Modelo" HeaderText="MODELO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="NumeroSerie" HeaderText="NO. SERIE" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100PX" />
                     </asp:BoundField>
                     <asp:BoundField DataField="UbicacionActivo" HeaderText="UBICACION" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CondicionNombre" HeaderText="CONDICION" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CompraFolio" HeaderText="FACTURA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                 </Columns>
             </asp:GridView>
         </div>
         
         <br /><br />
         
         <div ID="DivTablaNoEtiquetados" visible="false" runat="server">
         <div class="DivSubtituloImpresion">    
            ACTIVOS SIN ETIQUETAR
         </div>
             <asp:GridView ID="TablaNoEtiquetados" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion">
                 <EmptyDataTemplate>
                     <table class="TablaVacia">
                         <tr class="Encabezado">
                            <th>DESCRIPCIÓN</th><th style="width: 100px;">FAMILIA</th><th style="width: 100px;">SUBFAMILIA</th><th style="width: 100px;">MARCA</th><th style="width: 100px;">MODELO</th><th style="width: 120px;">NO. SERIE</th><th style="width: 150px;">UBICACION</th><th style="width: 100px;">CONDICIONES</th><th style="width: 100px;">FACTURA</th></tr><tr>
                             <td colspan="7" style="text-align: center;">
                                 No se encontró información con los parámetros seleccionados</td></tr></table></EmptyDataTemplate><HeaderStyle CssClass="EncabezadoImpresion" />
                 <PagerStyle CssClass="PaginacionImpresion" HorizontalAlign="Right" />
                 <Columns>
                     <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                      <asp:BoundField DataField="FamiliaNombre" HeaderText="FAMILIA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                     </asp:BoundField>
                     <asp:BoundField DataField="SubFamiliaNombre" HeaderText="SUBFAMILIA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                     </asp:BoundField>
                     <asp:BoundField DataField="MarcaNombre" HeaderText="MARCA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Modelo" HeaderText="MODELO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="NumeroSerie" HeaderText="NO. SERIE" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100PX" />
                     </asp:BoundField>
                     <asp:BoundField DataField="UbicacionActivo" HeaderText="UBICACION" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CondicionNombre" HeaderText="CONDICION" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CompraFolio" HeaderText="FACTURA" ItemStyle-HorizontalAlign="Left">
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

