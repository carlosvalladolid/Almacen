<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="TransferenciaActivo.aspx.cs" Inherits="Activos.Web.Aplicacion.Activo.TransferenciaActivo" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarEmpleado" Src="~/Incluir/ControlesWeb/ControlBuscarEmpleado.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">

   <script language="javascript" type="text/javascript">

      function ImprimirDocumento(EmpleadoIdOrigen, EmpleadoIdDestino) {
         window.open("/Ventana/ImprimirTransferenciaActivo.aspx?EmpleadoIdOrigen=" + EmpleadoIdOrigen + "&EmpleadoIdDestino=" + EmpleadoIdDestino, "ImprimirDocumento", " resizable=yes,scrollbars=1");

       }
       
   </script>

</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" SeccionMenu="ActivoFijo" runat="server" />
   </div>
   
   <div class="DivContenido">
      <div class="DivContenidoTitulo">
         <div class="DivTitulo">Transferencia de activo</div>
      </div>
      
      <div class="DivInformacionContenido">
         <asp:UpdatePanel ID="ActualizarTablaTransferencia" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            
               <asp:Panel CssClass="Superposicion" ID="pnlFondo" runat="server" Visible="False"></asp:Panel>
               <wuc:BuscarEmpleado ID="ControlBuscarEmpleado" runat="server" />
            
               <asp:Panel ID="PanelTablaTransferenciaActivo" runat="server">
                  <br />
                  
                  <div class="DivSubtituloPagina">
                     Empleado origen
                  </div>
                  <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                     <table class="TablaFormulario">
                        <tr>
                           <td class="Nombre">Número de Empleado</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo">
                              <asp:Panel ID="Panel2" runat="server" DefaultButton="LinkBuscarEmpleadoOrigen">
                                 <asp:TextBox ID="NumeroEmpleadoOrigen" OnTextChanged="NumeroEmpleadoOrigen_TextChanged" AutoPostBack="true" CssClass="CajaTextoMediana" MaxLength="10" runat="server"></asp:TextBox>&nbsp;
                                 <asp:ImageButton ImageUrl="/Imagen/Icono/ImagenBuscar.gif" ID="BotonBuscarEmpleadoOrigen" OnClick="BotonBuscarEmpleadoOrigen_Click" runat="server" />
                                 <asp:LinkButton ID="LinkBuscarEmpleadoOrigen" OnClick="LinkBuscarEmpleadoOrigen_Click" ValidationGroup="BuscarCodigoBarras" runat="server" Text="" Width="0px"></asp:LinkButton>
                              </asp:Panel>
                           </td>
                        </tr>
                        <tr>
                           <td class="Nombre">Nombre</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreEmpleadoOrigen" Enabled="false" MaxLength="200" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Código de Barras</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo">
                              <asp:Panel ID="PanelBuscarCodigoBarras" runat="server" DefaultButton="CodigoBarrasImagen">
                                 <asp:TextBox ID="CodigoBarrasParticular" CssClass="CajaTextoMediana" MaxLength="15" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                 <asp:ImageButton ID="CodigoBarrasImagen" ValidationGroup="BuscarCodigoBarras" ImageUrl="/Imagen/Icono/IconoCodigoBarras.jpg" OnClick="CodigoBarrasImagen_Click" runat="server" />
                              </asp:Panel>
                           </td>
                        </tr>
                        <tr>
                           <td colspan="3">
                              <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CodigoBarrasParticular" Display="Dynamic" ErrorMessage="" ID="BuscarCodigoBarrasRequerido" SetFocusOnError="true" ValidationGroup="BuscarCodigoBarras" runat="server"></asp:RequiredFieldValidator>
                              <asp:Label CssClass="TextoError" ID="EtiquetaMensajeCBError" runat="server" Text=""></asp:Label>
                           </td>
                        </tr>
                     </table> 
                  </asp:Panel> 
                  
                  <div class="DivSubtituloPagina">
                     Activos asignados
                  </div>
                  <br />
                  <div class="DivTabla">
                      <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnRowDataBound="TablaActivo_RowDataBound"
                          CssClass="TablaInformacion" DataKeyNames="ActivoId, MovimientoId, CondicionId, UbicacionActivoId, TipoDocumentoId, CompraFolio, CompraId, CodigoBarrasParticular" ID="TablaActivo" runat="server">
                          <EmptyDataTemplate>
                              <table class="TablaVacia">
                                  <tr class="Encabezado">
                                      <th>Descripción</th>
                                      <th style="width: 150px;">Número de serie</th>
                                      <th style="width: 150px;">Modelo</th>
                                      <th style="width: 100px;">Color</th>
                                      <th style="width: 150px;">Código de barras</th>
                                      <th style="width: 110px;">Condición</th>
                                      <th style="width: 50px;">Bodega</th>
                                      <th style="width: 50px;">Transferir</th>
                                  </tr>
                                  <tr>
                                      <td colspan="8" style="text-align: center;">Favor de seleccionar el empleado origen</td>
                                  </tr>
                              </table>
                          </EmptyDataTemplate>
                          <HeaderStyle CssClass="Encabezado" />
                          <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                          <Columns>
                              <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
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
                              
                              <asp:TemplateField HeaderText="Condición">
                                  <ItemTemplate>
                                      <asp:DropDownList CssClass="ComboPequeño" ID="CondicionId" runat="server" ></asp:DropDownList>
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" Width="110px" />
                              </asp:TemplateField>
                              
                              <asp:TemplateField HeaderText="Bodega">
                                  <ItemTemplate>
                                      <asp:CheckBox ID="chkUbicacion" Text="" Checked="false" runat="server" />
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" Width="50px" />
                              </asp:TemplateField>
                              
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
                     Empleado destino
                  </div>
                  <asp:Panel CssClass="DivCampo" id="Panel1" runat="server">
                     <table class="TablaFormulario">
                        <tr>
                           <td class="Nombre">Número de Empleado</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo">
                              <asp:Panel ID="Panel3" runat="server" DefaultButton="LinkBuscarEmpleadoDestino">
                                 <asp:TextBox ID="NumeroEmpleadoDestino" OnTextChanged="NumeroEmpleadoDestino_TextChanged" AutoPostBack="true" CssClass="CajaTextoMediana" MaxLength="10" runat="server"></asp:TextBox>&nbsp;
                                 <asp:ImageButton ImageUrl="/Imagen/Icono/ImagenBuscar.gif" ID="BotonBuscarEmpleadoDestino" OnClick="BotonBuscarEmpleadoDestino_Click" runat="server" />
                                 <asp:LinkButton ID="LinkBuscarEmpleadoDestino" OnClick="LinkBuscarEmpleadoDestino_Click" ValidationGroup="BuscarCodigoBarras" runat="server" Text="" Width="0px"></asp:LinkButton>
                              </asp:Panel>
                           </td>
                        </tr>
                        <tr>
                           <td class="Nombre">Nombre</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreEmpleadoDestino" Enabled="false" MaxLength="200" runat="server" Text=""></asp:TextBox></td>
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
                              <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimir" Enabled="false" OnClick="BotonImprimir_Click" ImageUrl="/Imagen/Boton/BotonImprimir.png" runat="server" />&nbsp;&nbsp;
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
               
               <asp:HiddenField ID="EmpleadoOrigenIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="EmpleadoDestinoIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="TipoEmpleadoBusquedaHidden" runat="server" Value="" />
               <asp:HiddenField ID="EmpOrigIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="EmpDestIdHidden" runat="server" Value="0" />
            </ContentTemplate>
            <Triggers>
            </Triggers>
         </asp:UpdatePanel> 
      </div> 
        
   </div> 
   
</asp:Content>
