<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="EtiquetadoActivo.aspx.cs" Inherits="Activos.Web.Aplicacion.Activo.EtiquetadoActivo" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
</asp:Content>
<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" SeccionMenu="ActivoFijo" runat="server" />
    </div>
    
    <div class="DivContenido">
        <div class="DivContenidoTitulo">
            <div class="DivTitulo">Etiquetado de activos</div>
        </div>
        
        <div class="DivInformacionContenido">
            <asp:UpdatePanel ID="ActualizarTablaEtiquetado" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <asp:Panel ID="PanelTablaAsignacionActivo" runat="server">
                        <br />
                        <div class="DivSubtituloPagina">
                           Datos generales
                        </div>
                        
                        <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
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
                                       <asp:TextBox ID="CompraFolio" CssClass="CajaTextoMediana" MaxLength="20" runat="server"></asp:TextBox>
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
                           Activos (detalle del documento)
                        </div>
                        <br />
                        <div class="DivTabla">
                            <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                                CssClass="TablaInformacion" DataKeyNames="ActivoId" ID="TablaActivo" OnRowDataBound="TablaActivo_RowDataBound" runat="server">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th>Descripción</th>
                                            <th style="width: 150px;">Número de serie</th>
                                            <th style="width: 150px;">Modelo</th>
                                            <th style="width: 100px;">Color</th>
                                            <th style="width: 150px;">Cod. Bar. Particular</th>
                                            <th style="width: 150px;">Cod. Bar. General</th>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="text-align: center;">Ese documento no tiene activos</td>
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
                                    
                                    <asp:TemplateField HeaderText="Cod. Bar. Particular">
                                        <ItemTemplate>
                                            <asp:Label CssClass="TextoError" ID="BarrasParticularDuplicado" Width="10px" runat="server" Text=""></asp:Label>
                                            <asp:TextBox ID="CodigoBarrasParticular" runat="server" Text='<%#Eval("CodigoBarrasParticular")%>' MaxLength="15" Width="130px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Cod. Bar. General">
                                        <ItemTemplate>
                                            <asp:Label CssClass="TextoError" ID="BarrasGeneralDuplicado" Width="10px" runat="server" Text=""></asp:Label>
                                            <asp:TextBox ID="CodigoBarrasGeneral" runat="server" Text='<%#Eval("CodigoBarrasGeneral")%>' MaxLength="15" Width="130px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
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
                     
                     <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaEtiquetado" ID="ProgresoTablaEtiquetado" runat="server">
                        <ProgressTemplate>
                            <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                        </ProgressTemplate>
                     </asp:UpdateProgress>
                     
                     <asp:HiddenField ID="CompraIdHidden" runat="server" Value="0" />
                </ContentTemplate>

                <Triggers>
                    
                </Triggers>
            </asp:UpdatePanel>
        </div> 
    
    </div> 
</asp:Content>
