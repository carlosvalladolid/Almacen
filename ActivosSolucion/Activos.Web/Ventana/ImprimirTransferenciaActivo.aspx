<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaImpresion.Master" AutoEventWireup="true" CodeBehind="ImprimirTransferenciaActivo.aspx.cs" Inherits="Activos.Web.Ventana.ImprimirTransferenciaActivo" %>
<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaImpresion.Master" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
</asp:Content>
<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   <div class="DivContenido">
      <div class="DivInformacionContenido">
         
         <div>
            <div class="DivImpresionFecha">
               <table class="TablaImpresionFecha">
                  <tr>
                     <td class="NombreFecha">Fecha</td>
                     <td class="CampoFecha"><asp:Label ID="FechaLabel" runat="server" Text=""></asp:Label></td>
                  </tr>
               </table>
            </div>
         </div> 
         <br /><br /><br />
         <div class="DivSubtituloImpresion">    
            DESCRIPCIÓN DEL ACTIVO FIJO A TRANSFERIR
         </div>
         <br /><br />
         
         <div ID="DivTabla">
             <asp:GridView ID="TablaActivos" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion" DataKeyNames="ActivoId">
                 <EmptyDataTemplate>
                     <table class="TablaVacia">
                         <tr class="Encabezado">
                            <th style="width: 90px;">FOLIO</th>
                            <th style="width: 80px;">DOCUMENTO</th>
                            <th style="width: 120px;">C.B.</th>
                            <th style="width: 120px;">NO. SERIE</th>
                            <th>DESCRIPCIÓN</th>
                            <th style="width: 100px;">MODELO</th>
                            <th style="width: 80px;">CONDICIÓN</th>
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
                     <asp:BoundField DataField="CompraFolio" HeaderText="FOLIO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="90px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="TipoDocumentoNombre" HeaderText="DOCUMENTO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="80px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="C.B." ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="NumeroSerie" HeaderText="NO. SERIE" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Modelo" HeaderText="MODELO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CondicionNombre" HeaderText="CONDICIÓN" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="80px" />
                     </asp:BoundField>
                 </Columns>
             </asp:GridView>
         </div>
         
         <br /><br />
         <div class="DivSubtituloImpresion">    
            NUEVA UBICACIÓN DEL ACTIVO FIJO
         </div>
         <br /><br />
         
         <table class="TablaImpresionEmpleado">
            <tr>
               <td class="NombreImpresionEmpleado">DIRECCIÓN:</td>
               <td class="CampoImpresionEmpleado" colspan="3"><asp:Label ID="DireccionLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">DEPARTAMENTO:</td>
               <td class="CampoImpresionEmpleado" colspan="3"><asp:Label ID="DepartamentoLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">NO. EMPLEADO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="NumeroEmpleadoLabel" runat="server" Text=""></asp:Label></td>
               <td class="NombrePequeñoImpresionEmpleado">RFC:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="RFCLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">NOMBRE:</td>
               <td class="CampoImpresionEmpleado" colspan="3"><asp:Label ID="NombreLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">DOMICILIO:</td>
               <td class="CampoImpresionEmpleado" colspan="3"><asp:Label ID="DomicilioLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">TELÉFONO:</td>
               <td class="CampoImpresionEmpleado" colspan="3"><asp:Label ID="TelefonoLabel" runat="server" Text=""></asp:Label></td>
            </tr>
         </table>
         
         <br /><br /><br />
         <asp:Panel ID="Panel1" runat="server" Visible="true">
             <div class="DivImpresionFirmas">
               
               <table class="TablaImpresionFirmas">
                  <tr>
                     <td class="ImpresionFirmasCampo">RESPONSABLE</td>
                  </tr>
                  <tr><td>&nbsp;</td></tr>
                  <tr>
                     <td class="ImpresionFirmasCampoSubrrayado">&nbsp;</td>
                  </tr>
                  <tr>
                     <td class="ImpresionFirmasCampo"><asp:Label ID="ResponsableNombreLabel" Width="400px" runat="server" ></asp:Label></td>
                  </tr>
                  <tr>
                     <td class="ImpresionFirmasCampo">ADQUISICIONES Y SERVICIOS</td>
                  </tr>
               </table>
               
               <br /><br />
               <table class="TablaImpresionFirmas">
                  <tr>
                     <td class="ImpresionFirmasCampo">ENTREGA</td>
                     <td class="ImpresionFirmasEspacio"></td>
                     <td class="ImpresionFirmasCampo">RECIBE</td>
                  </tr>
                  <tr><td colspan="3">&nbsp;</td></tr>
                  <tr>
                     <td class="ImpresionFirmasCampoSubrrayado">&nbsp;</td>
                     <td class="ImpresionFirmasEspacio">&nbsp;</td>
                     <td class="ImpresionFirmasCampoSubrrayado">&nbsp;</td>
                  </tr>
                  <tr>
                     <td class="ImpresionFirmasCampo"><asp:Label ID="EntregaNombreLabel" Width="300px" runat="server" ></asp:Label></td>
                     <td class="ImpresionFirmasEspacio"></td>
                     <td class="ImpresionFirmasCampo"><asp:Label ID="RecibeNombreLabel" Width="300px" runat="server" ></asp:Label></td>
                  </tr>
                  <tr>
                     <td class="ImpresionFirmasCampo">(nombre y firma)</td>
                     <td class="ImpresionFirmasEspacio">&nbsp;</td>
                     <td class="ImpresionFirmasCampo">(nombre y firma)</td>
                  </tr>
               </table>
               <br /><br /><br /><br />
             </div>
         </asp:Panel>
         
      </div> 
   </div>
   <script language="javascript" type="text/javascript">

     function ImprimirPantalla() {
         window.print();
     }

     ImprimirPantalla();
     
   </script>
</asp:Content>
