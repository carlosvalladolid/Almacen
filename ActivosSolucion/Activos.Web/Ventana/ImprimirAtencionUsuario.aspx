<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaImpresion.Master" AutoEventWireup="true" CodeBehind="ImprimirAtencionUsuario.aspx.cs" Inherits="Activos.Web.Ventana.ImprimirAtencionUsuario" %>
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
                     <td class="NombreFecha">Fecha:</td>
                     <td class="CampoFecha"><asp:Label ID="FechaLabel" runat="server" Text=""></asp:Label></td>
                  </tr>
               </table>
            </div>
         </div>
         <br /><br /><br />
         <div>
         <table class="TablaImpresionEmpleado">
            <tr>
                <td class="NombreImpresionEmpleado">FOLIO:  </td>
                <td ><asp:Label ID="FolioId" runat="server" Text=""></asp:Label></td>
            </tr>
         </table>
         
         </div>
          <div class="LineasNaranjas">
        &nbsp;
        </div>
         <br /><br />
         <table class="TablaImpresionEmpleado">
            <tr>
               <td class="NombreImpresionEmpleado">DIRECCIÓN:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="DireccionLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">DEPARTAMENTO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="DepartamentoLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">NO. EMPLEADO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="NumeroEmpleadoLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">NOMBRE:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="NombreLabel" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
               <td class="NombreImpresionEmpleado">TELÉFONO:</td>
               <td class="CampoImpresionEmpleado"><asp:Label ID="TelefonoLabel" runat="server" Text=""></asp:Label></td>
            </tr>
         </table>
         
         <br /><br /><br />
         <div class="DivSubtituloImpresion">    
            DATOS DEL ACTIVO
         </div>
         <br /><br />
         
         <div class="DivTabla">
             <asp:GridView ID="TablaActivos" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion">
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
                     <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="C.B." ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="SolucionDescripcion" HeaderText="SERVICIO REALIZADO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="50%" />
                     </asp:BoundField>
                 </Columns>
             </asp:GridView>
         </div>
         
         
          <br /><br /><br />
         
         <div class="DivSubtituloImpresion">    
            ATIENDEN
         </div>
         <br /><br />
         <div class="DivTabla"><asp:GridView ID="TablaEmpleados" runat="server" AllowPaging="false" 
                 AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                 CssClass="TablaInformacionImpresion" DataKeyNames="EmpleadoId">
                 <EmptyDataTemplate>
                     <table class="TablaVacia">
                         <tr class="Encabezado">
                            <th style="width: 30%;">NÚMERO DE EMPLEADO</th><th style="width: 80%;">NOMBRE</th></tr><tr>
                             <td colspan="7" style="text-align: center;">
                                 No se encontró información con los parámetros seleccionados</td></tr></table></EmptyDataTemplate><HeaderStyle CssClass="EncabezadoImpresion" />
                 <PagerStyle CssClass="PaginacionImpresion" HorizontalAlign="Right" />
                 <Columns>
                     <asp:BoundField DataField="NumeroEmpleado" HeaderText="NÚMERO DE EMPLEADO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                     </asp:BoundField>
                     <asp:BoundField DataField="EmpleadoNombreCompleto" HeaderText="NOMBRE" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                 </Columns>
             </asp:GridView>
         </div>  
         <asp:Panel ID="Panel1" runat="server" Visible="true">
            <div class="DivImpresionFirmas">
               <table class="TablaImpresionFirmas">
                  <tr>
                     <td class="ImpresionFirmasCampo">USUARIO</td>
                  </tr>
                  <tr><td colspan="3">&nbsp;</td></tr>
                  <tr>
                     <td class="ImpresionFirmasCampoSubrrayado">&nbsp;</td>
                  </tr>
                  <tr>
                     <td class="ImpresionFirmasCampo"><asp:Label ID="UsuarioNombreLabel" Width="300px" runat="server" ></asp:Label></td>
                  </tr>
                  <tr>
                     <td class="ImpresionFirmasCampo">(nombre y firma)</td>
                  </tr>
               </table>
            </div> 
         </asp:Panel>         
      </div>
 </div>
                    
                    
                    
                     <script language="javascript" type="text/javascript">

     function ImprimirPantalla() {
         window.print();
     }

     ImprimirPantalla();
     
   </script></asp:Content>