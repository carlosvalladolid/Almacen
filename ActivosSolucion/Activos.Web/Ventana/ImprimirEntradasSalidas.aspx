<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaImpresion.Master" AutoEventWireup="true" CodeBehind="ImprimirEntradasSalidas.aspx.cs" Inherits="Activos.Web.Ventana.ImprimirEntradasSalidas" %>
<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>
<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaImpresion.Master" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">

    <script src="/Incluir/Javascript/Buscar.js" type="text/javascript"></script>
    
    
    
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
        <div>
        <asp:Label runat="server" Text="TIPO DE SERVICIO:" Font-Bold="true"></asp:Label>
        </div>
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
        <div>
        <asp:Label ID="TipoServicioEtiqueta" runat="server" Text=""></asp:Label>
        </div>
        <asp:Panel ID="PanelAutomovil" runat="server" Visible="false">
        <div class="DivEmpleado">
        <table>
        <tr>
        <td class="Empleado">VEHICULO
        </td>
        <td class="CampoEmpleado"><asp:Label ID="Vehiculo" runat="server"></asp:Label>
        </td>
        </tr>
        <tr>
        <td class="Empleado">NO. ECONÓMICO
        </td>
        <td class="CampoEmpleado"><asp:Label ID="NoEconomico" runat="server"></asp:Label>
        </td>
        </tr>
        <tr>
        <td class="Empleado">PLACAS
        </td>
        <td class="CampoEmpleado"><asp:Label ID="Placas" runat="server"></asp:Label>
        </td>
        </tr>
        </table>
        </div>
        </asp:Panel>
        <div class="DivInformacionContenido">
        
                    <div class="DivEmpleado">
                                                                                   
                     <asp:Panel id="PanelDatosDireccion" runat="server" > 
                          <table>
                          <tr>
                          <td class="Empleado">PROVEEDOR
                          </td>
                          <td class="CampoEmpleado"><asp:Label ID="Proveedor" runat="server"></asp:Label>
                          </td>
                          </tr>
                          <tr>
                          <td class="Empleado">TELÉFONO
                          </td>
                          <td class="CampoEmpleado"><asp:Label ID="TelefonoProveedor" runat="server"></asp:Label>
                          </td>
                          </tr>
                          </table>
                          &nbsp;&nbsp;&nbsp;&nbsp;
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
                           <asp:GridView ID="TablaActivo" runat="server" AllowPaging="false" 
                               AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                               CssClass="TablaInformacionImpresion" DataKeyNames="MovimientoId" >   

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
                               </Columns>
                           </asp:GridView>
                       </div>
                       <br />
                       <br />
                       <br />
                       <asp:Panel ID="PanelFirmas" runat="server" Visible="true">
             <div class="DivImpresionFirmas">
               
               <table class="TablaImpresionFirmas">
                  <tr>
                     <td class="ImpresionFirmasCampo">RESPONSABLE DE ÁREA</td>
                  </tr>
                  <tr><td>&nbsp;</td></tr>
                  <tr>
                     <td class="ImpresionFirmasCampoSubrrayado">&nbsp;</td>
                  </tr>
                  <tr>
                     <td class="ImpresionFirmasCampo"><asp:Label ID="TitularDelArea" runat="server" CssClass="TextoInformacion" Text=""></asp:Label></td>
                  </tr>
                  <tr>
                     <td class="ImpresionFirmasCampo">(NOMBRE Y FIRMA)</td>
                  </tr>
               </table>
               
               <br /><br />
               <table class="TablaImpresionFirmas">
                  <tr>
                     <td class="ImpresionFirmasCampo">TITULAR DEL ÁREA</td>
                     <td class="ImpresionFirmasEspacio"></td>
                     <td class="ImpresionFirmasCampo">RESPONSABLE</td>
                  </tr>
                  <tr><td colspan="3">&nbsp;</td></tr>
                  <tr>
                     <td class="ImpresionFirmasCampoSubrrayado">&nbsp;</td>
                     <td class="ImpresionFirmasEspacio">&nbsp;</td>
                     <td class="ImpresionFirmasCampoSubrrayado">&nbsp;</td>
                  </tr>
                  <tr>
                     <td class="ImpresionFirmasCampo"><asp:Label ID="DirectorAdministrativo" runat="server" CssClass="TextoInformacion" Text=""></asp:Label></td>
                     <td class="ImpresionFirmasEspacio"></td>
                     <td class="ImpresionFirmasCampo"><asp:Label ID="Adquisiciones" runat="server" CssClass="TextoInformacion" Text=""></asp:Label></td>
                  </tr>
                  <tr>
                     <td class="ImpresionFirmasCampo">DIRECTOR ADMINISTRATIVO</td>
                     <td class="ImpresionFirmasEspacio">&nbsp;</td>
                     <td class="ImpresionFirmasCampo">ADQUISICIONES Y SERVICIOS</td>
                  </tr>
               </table>
               <br /><br /><br /><br />
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



