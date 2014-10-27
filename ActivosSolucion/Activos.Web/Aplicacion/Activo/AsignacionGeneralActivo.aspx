<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="AsignacionGeneralActivo.aspx.cs" Inherits="Activos.Web.Aplicacion.Activo.AsignacionGeneralActivo" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarEmpleado" Src="~/Incluir/ControlesWeb/ControlBuscarEmpleado.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
   <script language="javascript" type="text/javascript">

      function ImprimirDocumento(EmpleadoId, CompraId) {
         window.open("/Ventana/ImprimirAsignacionGeneralActivo.aspx?EmpleadoId=" + EmpleadoId + "&CompraId=" + CompraId, "ImprimirDocumento", " resizable=yes,scrollbars=1");

      }

      function ImprimirDocumentoVehiculo(EmpleadoId, ActivoId) {
         window.open("/Ventana/ImprimirResguardoVehiculo.aspx?EmpleadoId=" + EmpleadoId + "&ActivoId=" + ActivoId, "ImprimirDocumento", " resizable=yes,scrollbars=1");

      }

      function ImprimirDocumentoVehiculoReverso() {
         window.open("/Ventana/ImprimirResguardoVehiculoReverso.aspx", "ImprimirDocumento", " resizable=yes,scrollbars=1");

      }
       
   </script>
   
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   <div class="DivMenuContenido">
       <wuc:Menu ID="ControlMenu" SeccionMenu="ActivoFijo" runat="server" />
   </div>
   
   <div class="DivContenido">
      <div class="DivContenidoTitulo">
         <div class="DivTitulo">Asignación general de activo</div>
      </div>
      
      <div class="DivInformacionContenido">
         <asp:UpdatePanel ID="ActualizarTablaAsignacion" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               <asp:Panel CssClass="Superposicion" ID="pnlFondo" runat="server" Visible="False"></asp:Panel>   
               <wuc:BuscarEmpleado ID="ControlBuscarEmpleado" runat="server" />
                
               <asp:Panel ID="PanelTablaAsignacionActivo" runat="server">  
               
                  <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                     <table class="TablaFormulario">
                         <tr>
                             <td class="Nombre">No. Empleado</td>
                             <td class="Requerido">*</td>
                             <td class="Campo">
                              <asp:Panel ID="Panel1" runat="server" DefaultButton="LinkBuscarEmpleado">
                                 <asp:TextBox ID="NumeroEmpleado" OnTextChanged="NumeroEmpleado_TextChanged" AutoPostBack="true" CssClass="CajaTextoMediana"  MaxLength="20" runat="server" Text=""></asp:TextBox>
                                 <asp:ImageButton ImageUrl="/Imagen/Icono/ImagenBuscar.gif" ID="BotonBuscarEmpleado" OnClick="BotonBuscarEmpleado_Click" runat="server" />
                                 <asp:LinkButton ID="LinkBuscarEmpleado" OnClick="LinkBuscarEmpleado_Click" ValidationGroup="BuscarCodigoBarras" runat="server" Text="" Width="0px"></asp:LinkButton>
                              </asp:Panel>
                             </td>
                         </tr>
                         <tr>
                             <td class="Nombre">Nombre de Empleado</td>
                             <td class="Espacio">&nbsp;</td>
                             <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreEmpleado" Enabled="false" MaxLength="200" runat="server" Text=""></asp:TextBox></td>
                         </tr>
                     </table>
                  </asp:Panel>
                  
                  <div class="DivSubtituloPagina">
                     Documento
                  </div>
                  
                  <asp:Panel CssClass="DivCampo" id="Panel2" runat="server">
                     <table class="TablaFormulario">
                       <tr>
                           <td class="Nombre">Proveedor</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="ProveedorId"  runat="server" ></asp:DropDownList></td>
                       </tr>
                       <tr>
                           <td class="Nombre">Tipo de Documento</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="TipoDocumentoId"  runat="server" ></asp:DropDownList></td>
                       </tr>
                       <tr>
                           <td class="Nombre">Folio</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo">
                              <asp:Panel ID="PanelBuscarDocumento" runat="server" DefaultButton="LinkBuscarDocumento">
                                 <asp:TextBox ID="CompraFolio" OnTextChanged="CompraFolio_TextChanged" AutoPostBack="true" CssClass="CajaTextoMediana" MaxLength="20" runat="server"></asp:TextBox>
                                 <asp:LinkButton ID="LinkBuscarDocumento" OnClick="LinkBuscarDocumento_Click" ValidationGroup="BuscarDocumento" runat="server" Text="" Width="0px"></asp:LinkButton>
                              </asp:Panel>
                           </td>
                       </tr>
                       <tr>
                           <td colspan="3">
                              <asp:CompareValidator CssClass="TextoError" ControlToValidate="ProveedorId" Display="Dynamic" ErrorMessage="" ID="BuscarProveedorRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="BuscarDocumento" ValueToCompare="0" runat="server"></asp:CompareValidator>
                              <asp:CompareValidator CssClass="TextoError" ControlToValidate="TipoDocumentoId" Display="Dynamic" ErrorMessage="" ID="BuscarTipoDocumentoRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="BuscarDocumento" ValueToCompare="0" runat="server"></asp:CompareValidator>
                              <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CompraFolio" Display="Dynamic" ErrorMessage="" ID="CompraFolioRequerido" SetFocusOnError="true" ValidationGroup="BuscarDocumento" runat="server"></asp:RequiredFieldValidator>
                           </td> 
                       </tr>
                     </table> 
                  </asp:Panel>
                  
                  <div class="DivSubtituloPagina">
                     Activos del documento
                  </div>
                  <br />
                  <div class="DivTabla">
                      <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnRowDataBound="TablaActivo_RowDataBound"
                          CssClass="TablaInformacion" DataKeyNames="ActivoId, CondicionId, UbicacionActivoId" ID="TablaActivo" runat="server">
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
                                  </tr>
                                  <tr>
                                      <td colspan="7" style="text-align: center;">Favor de seleccionar el documento</td>
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
                              
                          </Columns>
                      </asp:GridView>
                  </div>
                  
                  <div>
                     <table width="100%">
                        <tr>
                           <td>
                              <asp:CompareValidator CssClass="TextoError" ControlToValidate="ProveedorId" Display="Dynamic" ErrorMessage="" ID="ProveedorRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                              <asp:CompareValidator CssClass="TextoError" ControlToValidate="TipoDocumentoId" Display="Dynamic" ErrorMessage="" ID="TipoDocumentoRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                              <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CompraFolio" Display="Dynamic" ErrorMessage="" ID="GuardarCompraFolioRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                              <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NumeroEmpleado" Display="Dynamic" ErrorMessage="" ID="GuardarNumeroEmpleadoRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                              <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NombreEmpleado" Display="Dynamic" ErrorMessage="" ID="GuardarNombreEmpleadoRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                              <asp:Label CssClass="TextoError" ID="EtiquetaMensajeError" runat="server" Text=""></asp:Label>
                              <asp:Label CssClass="TextoInformacion" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
                              <br />
                              <asp:ImageButton AlternateText="Guardar" ID="BotonGuardar" OnClick="BotonGuardar_Click" ImageUrl="/Imagen/Boton/BotonGuardar.png" ValidationGroup="Guardar" runat="server" />&nbsp;&nbsp;
                              <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelar" OnClick="BotonCancelar_Click" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                              <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimir" Enabled="false" OnClick="BotonImprimir_Click" ImageUrl="/Imagen/Boton/BotonImprimir.png" runat="server" />&nbsp;&nbsp;
                              <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimirVehiculoReverso" Visible="false" OnClick="BotonImprimirVehiculoReverso_Click" ImageUrl="/Imagen/Boton/BotonImprimirReverso.png" runat="server" />&nbsp;&nbsp;
                              
                              <br /><br /><br />
                           </td>
                        </tr>
                     </table> 
                  </div>
                  
               </asp:Panel> 
               
               <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaAsignacion" ID="ProgresoTablaAsignacion" runat="server">
                  <ProgressTemplate>
                      <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                  </ProgressTemplate>
               </asp:UpdateProgress>
               
               <asp:HiddenField ID="EmpleadoIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="CompraIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="EmpIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="CompIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="TipoActivoIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="ActivoVehiculoIdHidden" runat="server" Value="0" />
            </ContentTemplate>
            <Triggers>
            </Triggers>
         </asp:UpdatePanel> 
      </div> 
      
   </div> 
</asp:Content>
