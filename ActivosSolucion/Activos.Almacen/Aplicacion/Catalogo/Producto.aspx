<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="Almacen.Web.Aplicacion.Catalogo.Producto" Title="" %>

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
            <br/>
            <br/>
                <div class="PageTitleDiv">
                    <table class="PageTitleTable">
                        <tr>
                            <td class="Title">
                                Catálogo de productos
                            </td>
                            <td class="Search"><asp:TextBox CssClass="SearchBox" ID="SearchText" MaxLength="50" runat="server"></asp:TextBox>&nbsp;</td>
                            <td class="Icon"><asp:ImageButton ID="BotonBusquedaRapida" ImageUrl="~/Image/Icon/SearchIcon.gif"  runat="server" ToolTip="Buscar" /></td> 
                        </tr>
                    </table>
                </div>


			
                <asp:Panel CssClass="SearchDiv" ID="PanelBusquedaAvanzada" Visible="false" runat="server">
						 <table class="FormTable">
							<tr>
								<td class="Name">Clave</td>
								<td class="Espacio"></td>
								<td class="Field"><asp:TextBox CssClass="CajaTextoMediana" ID="ClaveBusqueda"  runat="server" ></asp:TextBox></td>
							</tr>
							
							<tr>
								<td class="Name">Descripción</td>
								<td class="Espacio"></td>
								<td class="Field"><asp:TextBox CssClass="CajaTextoMediana" ID="DescripcionBusqueda"  runat="server" ></asp:TextBox></td>
							</tr>
							  <tr>
                            <td colspan="3">
                                <br />
                                <asp:ImageButton AlternateText="Buscar" ID="BotonBusqueda" ImageUrl="~/Imagen/Boton/BotonBuscar.png"  OnClick="BotonBusqueda_Click"  runat="server" />&nbsp;&nbsp;
                                <asp:ImageButton AlternateText="Limpiar" ID="BotonLimpiarBusqueda" ImageUrl="~/Imagen/Boton/BotonLimpiar.png" OnClick="BotonLimpiarBusqueda_Click" runat="server" />
                            </td>
                        </tr>							
							                        
                        </table>
                </asp:Panel>


		  <asp:Panel CssClass="NewRowDiv" ID="PanelNuevoRegistro" Visible="false" runat="server">
                    <table class="FormTable">
						<tr>
                            <td class="Name">Clave</td>
                            <td class="Required">*</td>
                            <td class="Field"><asp:TextBox CssClass="CajaTextoMediana" ID="ClaveNuevo"  runat="server" ></asp:TextBox></td>
                        </tr>
                    
						
                        <tr>
                            <td class="Name">Familia</td>
                            <td class="Required">*</td>
                            <td class="Field"><asp:DropDownList CssClass="ComboGrande" ID="FamiliaIdNuevo"  OnSelectedIndexChanged="ddlFamilia_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>                              
                            </td>
                        </tr>
                        <tr>
                            <td class="Name">SubFamilia</td>
                            <td class="Required">*</td>
                            <td class="Field"><asp:DropDownList CssClass="ComboGrande" ID="SubFamiliaIdNuevo" runat="server"></asp:DropDownList></td>
                        </tr>
                        
                        <tr>
                            <td class="Name">Marca</td>
                            <td class="Required"></td>
                            <td class="Field"><asp:DropDownList CssClass="ComboGrande" ID="MarcaIdNuevo"  runat="server"></asp:DropDownList></td>
                        </tr>
                        
                         <tr>
                            <td class="Name">Descripción</td>
                            <td class="Required">*</td>
                            <td class="Field"><asp:TextBox CssClass="CajaTextoGrande" ID="DescripcionNuevo"  runat="server" ></asp:TextBox></td>
                        </tr>
                    
						 <tr>
                            <td class="Name">Minimo</td>
                            <td class="Required">*</td>
                            <td class="Field"><asp:TextBox CssClass="CajaTextoChica" ID="MinimoNuevo"  runat="server" ></asp:TextBox></td>
                        </tr>
                        
                         <tr>
                            <td class="Name">Maximo</td>
                            <td class="Required">*</td>
                            <td class="Field"><asp:TextBox CssClass="CajaTextoChica" ID="MaximoNuevo"  runat="server" ></asp:TextBox></td>
                        </tr>
                        
                         <tr>
                            <td class="Name">Unidad de Medida</td>
                            <td class="Required"></td>
                            <td class="Field"><asp:DropDownList CssClass="Combopequenia" ID="UnidaddeMedidaIdNuevo"  runat="server"></asp:DropDownList></td>
                        </tr>
                        
                        <tr>
                            <td class="Name">Maximo Permitido</td>
                            <td class="Required">*</td>
                            <td class="Field"><asp:TextBox CssClass="CajaTextoChica" ID="MaximoPermitivoNuevo"  runat="server" ></asp:TextBox></td>
                        </tr>
                        
                         <tr>
                            <td class="Name">Estatus</td>
                            <td class="Required">*</td>
                            <td class="Field"><asp:CheckBox ID="EstatusProductoNuevo" Checked="false" runat="server" Text=" Activo" /> </td>
                        </tr>
                        
                        
                        
                        <tr>
                            <td colspan="3">
                                <br />
                                <asp:ImageButton AlternateText="Guardar" ID="BotonGuardar" ImageUrl="~/Imagen/Boton/BotonGuardar.png" OnClick="BotonGuardar_Click" runat="server" ValidationGroup="Save" />&nbsp;&nbsp;
                                <asp:ImageButton AlternateText="Limpiar" ID="LimpiarBoton" ImageUrl="~/Imagen/Boton/BotonLimpiar.png"  OnClick="BotonLimpiar_Click" runat="server" ValidationGroup="Save" />&nbsp;&nbsp;
                                <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelar" ImageUrl="~/Imagen/Boton/BotonCancelar.png" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>



           <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>

			<div>
                    <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                        CssClass="TablaInformacion" DataKeyNames="ProductoId" ID="TablaProducto" OnPageIndexChanging="TablaProducto_PageIndexChanging"  OnRowCommand="TablaProducto_RowCommand" runat="server" PageSize="10">
                        <EmptyDataTemplate>
                            <table class="TablaVacia">
                                <tr class="Encabezado">
                                    <th style="width: 30px;">Clave</th>
                                    <th style="width: 100px;">Nombre</th>                                    
                                    <th style="width: 60px;">SubFamilia</th>   
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                                </tr>
                            </table>
                      </EmptyDataTemplate>
                      
                        <HeaderStyle CssClass="Encabezado" />
                        <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:CheckBox ID="SeleccionarBorrar" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                            </asp:TemplateField>                            
                                                    
                              <asp:TemplateField HeaderText="Clave">
                                <ItemTemplate>
                                    <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaNombre" runat="server" Text='<%#Eval("Clave")%>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>                            
                            
                            <asp:BoundField DataField="NombreProducto" HeaderText="Producto" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>                      
                          
                            <asp:BoundField DataField="SubFamilia" HeaderText="SubFamilia" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>



                <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="../../Image/Icon/LoadingIcon.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
                
                
                     <asp:HiddenField ID="ProductoIdHidden" runat="server" Value="0" />
                     <asp:HiddenField ID="ClaveIdHidden" runat="server" Value="0" />
                
                
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
