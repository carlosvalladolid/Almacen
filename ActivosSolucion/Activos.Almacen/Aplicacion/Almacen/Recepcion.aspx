<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Recepcion.aspx.cs" Inherits="Activos.Almacen.Aplicacion.Almacen.Recepcion" Title="" %>
<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaPrivada.Master" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
 <script language="javascript" src="/Incluir/Javascript/ValidarFormulario.js" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    
    <div class="LeftBodyDiv">
        <wuc:ControlMenuIzquierdo ID="ControlMenuIzquierdo" runat="server" />
    </div>


    <div class="RightBodyDiv">
         <asp:UpdatePanel ID="PageUpdate" runat="server">
             <ContentTemplate>
             
             <div class="PageTitleDiv">
                    <table class="PageTitleTable">
                        <tr>
                            <td class="Title">
                               Recepción de Articulos
                            </td>
                            <td class="Search"></td>
                            <td class="Icon"></td> 
                        </tr>
                    </table>
                </div>
             
              <div class="SubTituloDiv">Datos Generales</div>
             
             
              <asp:Panel CssClass="NewRowDiv" ID="PanelNuevoRegistroDatosGenerales" Visible="true" runat="server">
              <table class="FormTable">
                     <tr>
                        <td class="Nombre">Proveedor</td>
                        <td class="Espacio">*</td>
                        <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="ProveedorIdNuevo" runat="server"></asp:DropDownList>                              
                        </td>
                     </tr>
                        
                      <tr>
                        <td class="Nombre">Tipo de Documento</td>
                        <td class="Espacio">*</td>
                        <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="TipoDocumentoIdNuevo" runat="server"></asp:DropDownList>                              
                        </td>
                     </tr>
              
                    <tr>
                        <td class="Nombre">Folio</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="FolioNuevo"  runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    
                     <tr>
                        <td class="Nombre">Fecha Documento</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="FechaDocumentoNuevo" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    
                      <tr>
                        <td class="Nombre">Monto</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="MontoDatosNuevo" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    
                    <tr>
                        <td class="Nombre">Orden de Compra</td>
                        <td class="Espacio"></td>
                        <td class="Campo">                        
                            <asp:Panel ID="PanelBuscarOrdenCompra" runat="server" DefaultButton="LinkBuscarOrdenCompra">
                             <asp:TextBox ID="OrderCompraNuevo" CssClass="CajaTextoMediana" MaxLength="15" runat="server"></asp:TextBox>
                             <asp:LinkButton ID="LinkBuscarOrdenCompra" OnClick="LinkBuscarOrdenCompra_Click" Visible ="false"  ValidationGroup="BuscarOrdenCompra" runat="server" Text="" Width="0px"></asp:LinkButton>
                             </asp:Panel>
                       </td>
                    </tr>
                    
                      <tr>
                        <td class="Nombre">Fecha O.C</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="FechaOrdenCompraNuevo" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    
                    
                         <tr>
                            <td class="Nombre">Solicitante</td>
                            <td class="Required">*</td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="SolicitanteIdNuevo" onselectedindexchanged="SolicitanteCombo_SelectedIndexChanged" AutoPostBack="true"  runat="server"></asp:DropDownList>                              
                            </td>
                        </tr>
                        
                        
                         <tr>
                            <td class="Nombre">Jefe Inmediato</td>
                            <td class="Required">*</td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="JefeInmediatoIdNuevo"  runat="server"></asp:DropDownList>                              
                            </td>
                        </tr>
                </table>
             </asp:Panel>
             
              <div class="SubTituloDiv">Detalle del documento</div>
             
             
              <asp:Panel CssClass="NewRowDiv" ID="PanelNuevoRegistroDetalle" Visible="true" runat="server">
              <table class="FormTable">              
                     <tr>
                            <td class="Nombre">Clave</td>
                            <td class="Required">*</td>
                            <td class="Campo">  
                             <asp:TextBox ID="ClaveNuevo" CssClass="CajaTextoMediana" MaxLength="15" OnTextChanged ="LinkBuscarClave_SelectedTextChanged" AutoPostBack="true"  runat="server"></asp:TextBox>     
                             
                            </td>
                        </tr>
              
                         <tr>
                            <td class="Nombre">Familia</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="FamiliaIdNuevo" OnSelectedIndexChanged="ddlFamilia_SelectedIndexChanged" AutoPostBack="true"   runat="server"></asp:DropDownList>                              
                            </td>
                        </tr>
                        <tr>
                            <td class="Nombre">SubFamilia</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="SubFamiliaIdNuevo"  runat="server"></asp:DropDownList></td>
                        </tr>
                        
                        <tr>
                            <td class="Nombre">Marca</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="MarcaIdNuevo" runat="server"></asp:DropDownList></td>
                        </tr>
                        
                         <tr>
                            <td class="Nombre">Descripción</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="DescripcionNuevo"  runat="server" ></asp:TextBox></td>
                        </tr>
                        
                         <tr>
                            <td class="Nombre">Precio Unitario</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="PrecionUnitarioNuevo"  runat="server" Text=""></asp:TextBox></td>
                           
                        </tr>
                    
						 <tr>
                            <td class="Nombre">Cantidad</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CantidadNuevo"  runat="server" Text=""></asp:TextBox></td>
                           
                        </tr>
                        
                         <tr>
                            <td class="Nombre">Monto</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="MontoDocumentoNuevo"  runat="server" Text=""></asp:TextBox></td>
                           
                        </tr>
                </table>
             </asp:Panel>
             
            <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>

             	<div>
                    <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                        CssClass="TablaInformacion" DataKeyNames="RecepcionId" ID="TablaRecepcion" runat="server" PageSize="10">
                        <EmptyDataTemplate>
                            <table class="TablaVacia">
                                <tr class="Encabezado">
                                    <th style="width: 10px;"></th>
                                    <th style="width: 30px;">Clave</th>
                                    <th style="width: 100px;">Descripcion</th>  
                                    <th style="width: 80px;">Precio Unitario</th>                                    
                                    <th style="width: 60px;">Cantidad</th>   
                                    <th style="width: 60px;">Monto</th>  
                                </tr>
                                <tr>
                                    <td colspan="6" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                                </tr>
                            </table>
                      </EmptyDataTemplate>
                      
                        <HeaderStyle CssClass="Encabezado" />
                        <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                        <Columns>
                           <asp:BoundField DataField="Clave" HeaderText="Clave" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>                                                      
                            
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>    
                            
                              <asp:BoundField DataField="PrecioUnitario" HeaderText="PrecioUnitario" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>                  
                          
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Monto" HeaderText="Monto" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="">
                                     <ItemTemplate>
                                         <asp:ImageButton ID="BotonEliminarActivo" CommandArgument="<%#Container.DataItemIndex%>" CommandName="EliminarPreOrden" runat="server" ImageUrl="/Imagen/Icono/IconoEliminarRegistro.gif" />
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" Width="25px" />
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
                </div>
             
             
                <div>
                        <table width="100%">
                           <tr>
                              <td style="width:70%;">
                                 
                              <br />
                              <asp:ImageButton AlternateText="Guardar" ID="BotonGuardarPreOrden"  ImageUrl="/Imagen/Boton/BotonGuardar.png" runat="server" />&nbsp;&nbsp;
                              <asp:ImageButton AlternateText="Limpiar" ID="BotonLimpiarRegistro"  ImageUrl="/Imagen/Boton/BotonLimpiar.png" runat="server" />&nbsp;&nbsp;
                              <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarPreOrden" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                              
                              </td>                            
                           </tr>
                        </table> 
                     </div>
                     <br /><br /><br /> 

             
               <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="../../Image/Icon/LoadingIcon.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
                
                
                     <asp:HiddenField ID="TemporalRecepcionIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="ProductoIdHidden" runat="server" Value="" />
             
             
             
             
             </ContentTemplate>
         </asp:UpdatePanel>


    </div>

</asp:Content>
