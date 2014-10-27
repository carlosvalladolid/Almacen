<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="FormatoLevantamiento.aspx.cs" Inherits="Activos.Web.Ventana.FormatoLevantamiento" Title="" %>
<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    <script src="/Incluir/Javascript/Buscar.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   
    
    <div class="DivContenido">
        <div class="DivContenidoTitulo">    
            <div class="DivTitulo">Formato de Levantamiento</div>
        </div> 
        
        <div class="DivInformacionContenido">
             <asp:UpdatePanel ID="ActualizarTablaEmpleado" runat="server" UpdateMode="Conditional">
                <ContentTemplate> 
                     
                     <asp:Panel CssClass="DivCampo" id="PanelEncabezado" runat="server" 
                         Width="1374px">
                        <div><img alt="" src="/Imagen/Logotipo/EncabezadoIDP3.png" /></div>  
                     </asp:Panel>
                     
                       <div>
                          <table class="TablaControlWeb">
                               <tr>
                                  <td class="Nombre">Fecha del Movimiento</td>
                                  <td class="Espacio">&nbsp;</td>
                                  <td class="Campo"><asp:TextBox CssClass="CajaTextochico" ID="FechaMovimiento" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                               </tr>
                            </table>
                        </div>
                                                                                   
                     <asp:Panel id="PanelDatosDireccion" runat="server">
                        <div>
                          <table class="TablaControlWeb">
                               <tr>
                                  <td class="Nombre">EL SIGUIENTE EQUIPO SE ENCUENTRA EN CALIDAD DE:</td>
                                  <td class="Espacio">&nbsp;</td>
                                  <td class="Nombre">ASIGNACION:</td>
                                  <td class="Nombre">------</td>
                                   <td class="Nombre">PRESTAMO:</td>
                                  <td class="Nombre">------</td>                                 
                               </tr> 
                              </table>
                           </div>
                              
                          <div>
                              <table>
                              <tr>
                                 <td class="Nombre">DIRECCIÓN :</td>
                                 <td class="Espacio">&nbsp;&nbsp;</td>
                                 <td class="Campo" ><asp:TextBox CssClass="CajaTextoGrande" ID="Direccion" 
                                  MaxLength="100" runat="server" Text="" Width="445px"></asp:TextBox></td>
                                  <td class="Nombre">&nbsp;DEPARTAMENTO :</td>
                                 <td class="Espacio">&nbsp;&nbsp;</td>
                                 <td class="Campo" ><asp:TextBox CssClass="CajaTextoGrande" ID="Departamento" 
                                  MaxLength="100" runat="server" Text="" Width="439px"></asp:TextBox></td>
                               </tr>                                                            
                            </table>
                        </div>
                     </asp:Panel>
                                
            <div >LA FIRMAR ESTA FORMA USTED ES RESPONSABLE DE:</div>
                <br />
                  <h3>Dar un buen manejo al equipo descrito.</h3> 
                <br />
                   <h3> Notificar por medio de oficio la terminación de la responsabilidad sobre el equipo,
                    ya sea por reubicación del usuario o terminación de sus funciones.</h3>                    
                    <h3>Solicitar por medio de oficio la reubicación del equipo.</h3>                   
                    <h3>Ademas del estricto cumplimiento del Reglamento de Informática.Ver al reverso.</h3>
                     <br>
                     <br></br>
                     <h4>
                         NOTA.- EL EQUIPO NO PUEDE SALIR DEL AREA ASIGNADA SIN AUTORIZACION FIRMADA POR 
                         EL DIRECTOR GENERAL DE DEFENSORIA PUBLICA</h4>
                     <h4>
                         NO SE PERMITE INSTALAR SOFTWARE EN EL EQUIPO SIN PREVIA AUTORIZACIÓN</h4>
                     <br />
                     <asp:Panel ID="PanelFirmas" runat="server" Visible="false">
                         <div>
                             <table class="TablaControlWeb">
                                 <tr>
                                     <td align="center" class="Nombre">
                                         USUARIO</td>
                                     <td class="Espacio">
                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td align="center" class="Nombre">
                                         AUTORIZA</td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <asp:Label ID="NombreUsuario" runat="server" CssClass="TextoInformacion" 
                                             Text=""></asp:Label>
                                     </td>
                                     <td class="Espacio">
                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td>
                                         <asp:Label ID="NombreAutorizo" runat="server" CssClass="TextoInformacion" 
                                             Text=""></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="center" class="Nombre">
                                         __________________________________________________</td>
                                     <td class="Espacio">
                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td align="center" class="Nombre">
                                         __________________________________________________</td>
                                 </tr>
                                 <tr>
                                     <td align="center" class="Nombre">
                                         Nombre y Firma</td>
                                     <td class="Espacio">
                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td align="center" class="Nombre">
                                         Nombre y Firma</td>
                                 </tr>
                             </table>
                         </div>
                     </asp:Panel>
                     <div ID="DivTabla">
                         <asp:GridView ID="TablaLevantamiento" runat="server" AllowPaging="true" 
                             AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                             CssClass="TablaInformacion" DataKeyNames="ActivoId, CodigoBarrasParticular" 
                             PageSize="10">
                             <EmptyDataTemplate>
                                 <table class="TablaVacia">
                                     <tr class="Encabezado">
                                         <th>
                                             Descripcion</th>
                                         <th style="width: 180px;">
                                             No.Serie</th>
                                         <th style="width: 180px;">
                                             Modelo</th>
                                         <th style="width: 180px;">
                                             Color</th>
                                         <th style="width: 180px;">
                                             Codigo de Barra</th>
                                         <th style="width: 180px;">
                                             Estatus</th>
                                         <th style="width: 30px;">
                                             Validacion</th>
                                         <th style="width: 30px;">
                                         </th>
                                     </tr>
                                     <tr>
                                         <td colspan="8" style="text-align: center;">
                                             No se encontró información con los parámetros seleccionados</td>
                                     </tr>
                                 </table>
                             </EmptyDataTemplate>
                             <HeaderStyle CssClass="Encabezado" />
                             <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                             <Columns>
                                 <asp:TemplateField HeaderText="Descripción">
                                     <ItemTemplate>
                                         <asp:LinkButton ID="LigaNombre" runat="server" 
                                             CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" 
                                             Text='<%#Eval("Descripcion")%>'></asp:LinkButton>
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left" />
                                 </asp:TemplateField>
                                 <asp:BoundField DataField="NumeroSerie" HeaderText="Numero de Serie" 
                                     ItemStyle-HorizontalAlign="Left">
                                     <HeaderStyle HorizontalAlign="Left" Width="180px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Modelo" HeaderText="Modelo" 
                                     ItemStyle-HorizontalAlign="Left">
                                     <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Color" HeaderText="Color" 
                                     ItemStyle-HorizontalAlign="Left">
                                     <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="Codigo Barras" 
                                     ItemStyle-HorizontalAlign="Left">
                                     <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="NombreEstatus" HeaderText="Estatus" 
                                     ItemStyle-HorizontalAlign="Left">
                                     <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                 </asp:BoundField>
                                 <asp:TemplateField HeaderText="Validado">
                                     <ItemTemplate>
                                         <asp:ImageButton ID="IconoRevisado" runat="server" 
                                             ImageUrl="/Imagen/Decoracion/DecoracionEspacio.png" />
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" Width="30px" />
                                 </asp:TemplateField>
                                 <asp:BoundField DataField="Levantamiento" HeaderText="Levantamiento" 
                                     ItemStyle-HorizontalAlign="Left" Visible="true">
                                     <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                 </asp:BoundField>
                             </Columns>
                         </asp:GridView>
                     </div>
                     <asp:Panel ID="Panel1" runat="server" Visible="false">
                         <div>
                             <table class="TablaControlWeb">
                                 <tr>
                                     <td align="center" class="Nombre">
                                         RESPONSABLE DEL AREA</td>
                                     <td class="Espacio">
                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td align="center" class="Nombre">
                                         AUTORIZA DIRECTOR ADMINISTRATIVO</td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <asp:Label ID="ResponsableArea" runat="server" CssClass="TextoInformacion" 
                                             Text=""></asp:Label>
                                     </td>
                                     <td class="Espacio">
                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td>
                                         <asp:Label ID="Autoriza" runat="server" CssClass="TextoInformacion" Text=""></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="center" class="Nombre">
                                         __________________________________________________</td>
                                     <td class="Espacio">
                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td align="center" class="Nombre">
                                         __________________________________________________</td>
                                 </tr>
                                 <tr>
                                     <td align="center" class="Nombre">
                                         Nombre y Firma</td>
                                     <td class="Espacio">
                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td align="center" class="Nombre">
                                         Nombre y Firma</td>
                                 </tr>
                             </table>
                         </div>
                     </asp:Panel>
                    </br>
            </div>   
            
                                     
                </ContentTemplate>             
                <Triggers> 
                                   
                </Triggers>
           </asp:UpdatePanel>
          
        </div>
       </div>   
</asp:Content>
