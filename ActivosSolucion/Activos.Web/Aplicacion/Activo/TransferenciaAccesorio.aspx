<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="TransferenciaAccesorio.aspx.cs" Inherits="Activos.Web.Aplicacion.Activo.TransferenciaAccesorio" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarActivo" Src="~/Incluir/ControlesWeb/ControlBuscarActivo.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" SeccionMenu="ActivoFijo" runat="server" />
   </div>
   
   <div class="DivContenido">
      <div class="DivContenidoTitulo">
         <div class="DivTitulo">Transferencia de accesorios</div>
      </div>
      
      <div class="DivInformacionContenido">
         <asp:UpdatePanel ID="ActualizarTablaTransferencia" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               <asp:Panel CssClass="Superposicion" ID="pnlFondo" runat="server" Visible="False"></asp:Panel>
               <wuc:BuscarActivo ID="ControlBuscarActivo" TipoActivo="Vehiculo" runat="server" />
            
               <asp:Panel ID="PanelTablaTransferenciaAccesorio" runat="server">
                  <br />
                  
                  <div class="DivSubtituloPagina">
                     Equipo de transporte origen
                  </div>
                  <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                     <table class="TablaFormulario">
                        <tr>
                           <td class="Nombre">Número económico</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo">
                              <asp:Panel ID="Panel4" runat="server" DefaultButton="LinkBuscarActivoOrigenEconomico">
                                 <asp:TextBox ID="NumeroEconomicoOrigen" OnTextChanged="NumeroEconomicoOrigen_TextChanged" AutoPostBack="true" CssClass="CajaTextoMediana" MaxLength="25" runat="server"></asp:TextBox>&nbsp;
                                 <asp:LinkButton ID="LinkBuscarActivoOrigenEconomico" OnClick="LinkBuscarActivoOrigenEconomico_Click" ValidationGroup="BuscarNumeroEconomicoOrigen" runat="server" Text="" Width="0px"></asp:LinkButton>
                              </asp:Panel> 
                           </td> 
                        </tr>
                        <tr>
                           <td class="Nombre">Número de serie</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo">
                              <asp:Panel ID="Panel2" runat="server" DefaultButton="LinkBuscarActivoOrigen">
                                 <asp:TextBox ID="NumeroSerieOrigen" OnTextChanged="NumeroSerieOrigen_TextChanged" AutoPostBack="true" CssClass="CajaTextoMediana" MaxLength="25" runat="server"></asp:TextBox>&nbsp;
                                 <asp:ImageButton ImageUrl="/Imagen/Icono/ImagenBuscar.gif" ID="BotonBuscarActivoOrigen" OnClick="BotonBuscarActivoOrigen_Click" runat="server" />
                                 <asp:LinkButton ID="LinkBuscarActivoOrigen" OnClick="LinkBuscarActivoOrigen_Click" ValidationGroup="BuscarNumeroSerieOrigen" runat="server" Text="" Width="0px"></asp:LinkButton>
                              </asp:Panel>
                           </td>
                        </tr>
                        <tr>
                           <td class="Nombre">Descripción</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="DescripcionActivoOrigen" Enabled="false" MaxLength="200" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Modelo</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="ModeloActivoOrigen" Enabled="false" MaxLength="200" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Color</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="ColorActivoOrigen" Enabled="false" MaxLength="200" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td colspan="3">
                              <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NumeroEconomicoOrigen" Display="Dynamic" ErrorMessage="" ID="BuscarEconomicoOrigenRequerido" SetFocusOnError="true" ValidationGroup="BuscarNumeroEconomicoOrigen" runat="server"></asp:RequiredFieldValidator>
                              <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NumeroSerieOrigen" Display="Dynamic" ErrorMessage="" ID="BuscarSerieOrigenRequerido" SetFocusOnError="true" ValidationGroup="BuscarNumeroSerieOrigen" runat="server"></asp:RequiredFieldValidator>
                           </td> 
                        </tr>
                     </table> 
                  </asp:Panel> 
                  
                  <div class="DivSubtituloPagina">
                     Accesorios asignados
                  </div>
                  <br />
                  <div class="DivTabla">
                      <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnRowDataBound="TablaActivo_RowDataBound"
                          CssClass="TablaInformacion" DataKeyNames="ActivoAccesorioId" ID="TablaActivo" runat="server">
                          <EmptyDataTemplate>
                              <table class="TablaVacia">
                                  <tr class="Encabezado">
                                      <th>Descripción</th>
                                      <th style="width: 150px;">Número de serie</th>
                                      <th style="width: 150px;">Modelo</th>
                                      <th style="width: 100px;">Color</th>
                                      <th style="width: 150px;">Código de barras</th>
                                      <th style="width: 50px;">Transferir</th>
                                  </tr>
                                  <tr>
                                      <td colspan="6" style="text-align: center;">Favor de seleccionar el equipo de transporte origen</td>
                                  </tr>
                              </table>
                          </EmptyDataTemplate>
                          <HeaderStyle CssClass="Encabezado" />
                          <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                          <Columns>
                              <asp:BoundField DataField="DescripcionActivo" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" />
                              </asp:BoundField>
                              <asp:BoundField DataField="NumeroSerie" HeaderText="Número de serie" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="150px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="150px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Color" HeaderText="Color" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="100px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="Código de barras" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="150px" />
                              </asp:BoundField>
                              
                              <asp:TemplateField HeaderText="Transferir">
                                  <ItemTemplate>
                                      <asp:CheckBox ID="SeleccionarTransferir" runat="server" />
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" Width="50px" />
                              </asp:TemplateField>
                          </Columns>
                      </asp:GridView>
                  </div>
                  <div class="DivSubtituloPagina">
                     Equipo de transporte destino
                  </div>
                  <asp:Panel CssClass="DivCampo" id="Panel1" runat="server">
                     <table class="TablaFormulario">
                        <tr>
                           <td class="Nombre">Número económico</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo">
                              <asp:Panel ID="Panel5" runat="server" DefaultButton="LinkBuscarActivoDestinoEconomico">
                                 <asp:TextBox ID="NumeroEconomicoDestino" OnTextChanged="NumeroEconomicoDestino_TextChanged" AutoPostBack="true" CssClass="CajaTextoMediana" MaxLength="25" runat="server"></asp:TextBox>&nbsp;
                                 <asp:LinkButton ID="LinkBuscarActivoDestinoEconomico" OnClick="LinkBuscarActivoDestinoEconomico_Click" ValidationGroup="BuscarNumeroEconomicoDestino" runat="server" Text="" Width="0px"></asp:LinkButton>
                              </asp:Panel> 
                           </td> 
                        </tr>
                        <tr>
                           <td class="Nombre">Número de serie</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo">
                              <asp:Panel ID="Panel3" runat="server" DefaultButton="LinkBuscarActivoDestino">
                                 <asp:TextBox ID="NumeroSerieDestino" OnTextChanged="NumeroSerieDestino_TextChanged" AutoPostBack="true" CssClass="CajaTextoMediana" MaxLength="10" runat="server"></asp:TextBox>&nbsp;
                                 <asp:ImageButton ImageUrl="/Imagen/Icono/ImagenBuscar.gif" ID="BotonBuscarActivoDestino" OnClick="BotonBuscarActivoDestino_Click" runat="server" />
                                 <asp:LinkButton ID="LinkBuscarActivoDestino" OnClick="LinkBuscarActivoDestino_Click" ValidationGroup="BuscarNumeroSerieDestino" runat="server" Text="" Width="0px"></asp:LinkButton>
                              </asp:Panel>
                           </td>
                        </tr>
                        <tr>
                           <td class="Nombre">Descripción</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="DescripcionActivoDestino" Enabled="false" MaxLength="200" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Modelo</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="ModeloActivoDestino" Enabled="false" MaxLength="200" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Color</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="ColorActivoDestino" Enabled="false" MaxLength="200" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td colspan="3">
                              <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NumeroEconomicoDestino" Display="Dynamic" ErrorMessage="" ID="BuscarEconomicoDestinoRequerido" SetFocusOnError="true" ValidationGroup="BuscarNumeroEconomicoDestino" runat="server"></asp:RequiredFieldValidator>
                              <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NumeroSerieDestino" Display="Dynamic" ErrorMessage="" ID="BuscarSerieDestinoRequerido" SetFocusOnError="true" ValidationGroup="BuscarNumeroSerieDestino" runat="server"></asp:RequiredFieldValidator>
                           </td> 
                        </tr>
                     </table> 
                  </asp:Panel>
                  
                  <div>
                     <table width="100%">
                        <tr>
                           <td>
                              <asp:Label CssClass="TextoError" ID="EtiquetaMensajeError" runat="server" Text=""></asp:Label>
                              <asp:Label CssClass="TextoInformacion" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
                              <br />
                              <asp:ImageButton AlternateText="Guardar" ID="BotonGuardar" OnClick="BotonGuardar_Click" ImageUrl="/Imagen/Boton/BotonGuardar.png" ValidationGroup="Guardar" runat="server" />&nbsp;&nbsp;
                              <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelar" OnClick="BotonCancelar_Click" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                              <br /><br /><br />
                           </td>
                        </tr>
                     </table> 
                  </div>
                  
               </asp:Panel>
               <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaTransferencia" ID="ProgresoTablaTransferencia" runat="server">
                  <ProgressTemplate>
                      <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                  </ProgressTemplate>
               </asp:UpdateProgress>
               
               <asp:HiddenField ID="ActivoOrigenIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="ActivoDestinoIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="TipoActivoBusquedaHidden" runat="server" Value="" />
               
            </ContentTemplate>
            <Triggers>
            </Triggers>
         </asp:UpdatePanel> 
      </div> 
        
   </div> 
</asp:Content>
