<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaImpresion.Master" AutoEventWireup="true" CodeBehind="ImprimirBaja.aspx.cs" Inherits="Activos.Web.Ventana.ImprimirBaja"  Title=""%>
<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaImpresion.Master" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">

    <script src="/Incluir/Javascript/Buscar.js" type="text/javascript">
    </script>
    
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   
    
    <div class="DivContenido">
       
        <div class="DivTablaFecha">
         
                            <table class="TablaFechaFactura">
                                <tr>
                                    <td class="TablaFechaFactura">FECHA:
                                    </td>
                                    <td class="TablaFechaFactura"><asp:Label class="FechaFactura" ID="FechaMovimiento" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
        </div>
               &nbsp;
               &nbsp;
        <div class="LineasNaranjas">
        &nbsp;
        &nbsp;
        &nbsp;
        </div>
        &nbsp;
        &nbsp;
        &nbsp;&nbsp;
        &nbsp;
        &nbsp;&nbsp;
        &nbsp;
        &nbsp;&nbsp;
        &nbsp;
        &nbsp;
        <div style="height:30px;">
        
        </div>
        <div class="DivInformacionContenido">
        
                    <div class="DivEmpleado">
                                                                                   
                     <asp:Panel id="PanelDatosDireccion" runat="server"> 
                          
                          
                            <table>
                             <tr>
                            <td class="Empleado">DIRECCION
                            </td>
                            <td class="CampoEmpleado"><asp:Label ID="CampoDireccion" runat="server"></asp:Label>
                            </td>
                            </tr>
                            <tr>
                            <td class="Empleado">DEPARTAMENTO
                            </td>
                            <td class="CampoEmpleado"><asp:Label ID="CampoDepartamento" runat="server"></asp:Label>
                            </td>
                            </tr>
                            
                             <tr>
                            <td class="Empleado">NO. EMPLEADO
                            </td>
                            <td class="CampoEmpleado"><asp:Label ID="CampoNoEmpleado" runat="server"></asp:Label>
                            </td>
                            </tr>
                            
                             <tr>
                            <td class="Empleado">NOMBRE
                            </td>
                            <td class="CampoEmpleado"><asp:Label ID="CampoNombre" runat="server"></asp:Label>
                            </td>
                            </tr>
                            
                             <tr>
                            <td class="Empleado">R.F.C
                            </td>
                            <td class="CampoEmpleado"><asp:Label ID="CampoRFC" runat="server"></asp:Label>
                            </td>
                            </tr>
                            
                             <tr>
                            <td class="Empleado">DOMICILIO
                            </td>
                            <td class="CampoEmpleado"><asp:Label ID="CampoDomicilio" runat="server"></asp:Label>
                            </td>
                            </tr>
                            
                             <tr>
                            <td class="Empleado">TELÉFONO
                            </td>
                            <td class="CampoEmpleado"><asp:Label ID="CampoTelefono" runat="server"></asp:Label>
                            </td>
                            </tr>
                            
                            </table>
                          
        &nbsp;&nbsp;
        &nbsp;
        &nbsp;&nbsp;
        &nbsp;
        &nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;
        &nbsp;
        &nbsp;&nbsp;
        &nbsp;
        &nbsp;&nbsp;
                     </asp:Panel>
                           </div>     
            
                       <br />
                       <div class="DivTabla">
                           <asp:GridView ID="TablaBaja" runat="server" AllowPaging="false" 
                               AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                               CssClass="TablaInformacionImpresion" DataKeyNames="MovimientoId" 
                               OnRowCommand="TablaBaja_RowCommand" 
                               OnRowDataBound="TablaBaja_RowDataBound">
                               <EmptyDataTemplate>
                                   <table class="TablaVacia">
                                       <tr class="Encabezado">
                                           <th>
                                               Descripción</th>
                                           <th style="width: 150px;">
                                               Número de serie</th>
                                           <th style="width: 150px;">
                                               Modelo</th>
                                           <th style="width: 100px;">
                                               Condiciones</th> 
                                           <th style="width: 150px;">
                                               Código de barras</th>
                                           <th style="width: 150px;">
                                               Tipo de baja</th>
                                           <th style="width: 25px;">
                                           </th>
                                       </tr>
                                       <tr>
                                           <td colspan="6" style="text-align: center;">
                                               Favor de agregar los activos</td>
                                       </tr>
                                   </table>
                               </EmptyDataTemplate>
                               <HeaderStyle CssClass="EncabezadoImpresion" />
                               <PagerStyle CssClass="PaginacionImpresion" HorizontalAlign="Right" />
                               <Columns>
                               <asp:BoundField DataField="CompraFolio" 
                                       HeaderText="Factura" ItemStyle-HorizontalAlign="Left">
                                       <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="CodigoBarrasParticular" 
                                       HeaderText="C/B" ItemStyle-HorizontalAlign="Left">
                                       <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="NumeroSerie" HeaderText="Número de serie" 
                                       ItemStyle-HorizontalAlign="Left">
                                       <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Descripcion" HeaderText="Descripción" 
                                       ItemStyle-HorizontalAlign="Left">
                                       <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Modelo" HeaderText="Modelo" 
                                       ItemStyle-HorizontalAlign="Left">
                                       <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="CondiocionNombre" HeaderText="Condiciones" 
                                       ItemStyle-HorizontalAlign="Left">
                                       <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="TipoBajaNombre" HeaderText="Tipo de baja" 
                                       ItemStyle-HorizontalAlign="Left">
                                       <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                   </asp:BoundField>
                               </Columns>
                           </asp:GridView>
                       </div>
                       <br />
                       <br />
                       <br />
                       <asp:Panel ID="PanelFirmas" runat="server" Visible="false">
                           <div>
                               <table class="TablaControlWeb">
                                   <tr>
                                       <td align="center" >
                                           USUARIO</td>
                                       <td class="Espacio">
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                       <td align="center">
                                           TITULAR DEL ÁREA</td>
                                   </tr>
                                    <tr>
                                       <td align="center">
                                           __________________________________________________</td>
                                       <td class="Espacio">
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                       <td align="center">
                                           __________________________________________________</td>
                                   </tr>
                                   <tr>
                                       <td align="center">
                                           <asp:Label ID="Usuario" runat="server" CssClass="TextoInformacion" Text=""></asp:Label>
                                       </td>
                                       <td class="Espacio">
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                       <td align="center">
                                           <asp:Label ID="TitularDelArea" runat="server" CssClass="TextoInformacion" Text=""></asp:Label>
                                       </td>
                                   </tr>
                                  
                                   <tr>
                                       <td align="center" >
                                           Nombre y Firma</td>
                                       <td class="Espacio">
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                       <td align="center">
                                           Nombre y Firma</td>
                                   </tr>
                                   <tr>
                                   <td colspan="3" style="height:40px;">
                                    &nbsp;&nbsp; &nbsp;&nbsp;
                                   </td>
                                   </tr>
                                   <tr>
                                       <td align="center" >
                                           TITULAR DEL ÁREA</td>
                                       <td class="Espacio">
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                       <td align="center">
                                           RESPONSABLE</td>
                                   </tr>
                                    <tr>
                                       <td align="center">
                                           __________________________________________________</td>
                                       <td class="Espacio">
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                       <td align="center">
                                           __________________________________________________</td>
                                   </tr>
                                   <tr>
                                       <td align="center">
                                           <asp:Label ID="DirectorAdministrativo" runat="server" CssClass="TextoInformacion" Text=""></asp:Label>
                                       </td>
                                       <td class="Espacio">
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                       <td align="center">
                                           <asp:Label ID="Adquisiciones" runat="server" CssClass="TextoInformacion" Text=""></asp:Label>
                                       </td>
                                   </tr>
                                  
                                   <tr>
                                       <td align="center" >
                                           DIRECTOR ADMINISTRATIVO</td>
                                       <td class="Espacio">
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                       <td align="center">
                                           ADQISICIONES Y SERVICIOS</td>
                                   </tr>
                               </table>
                           </div>
                       </asp:Panel>
                       <br />
                       <br />
                       <br />
             
          
          
        </div>
        
          <script language="javascript" type="text/javascript">

              function ImprimirPantalla() {
                  window.print();
              }

              ImprimirPantalla();
   </script>
   </div>
</asp:Content>

