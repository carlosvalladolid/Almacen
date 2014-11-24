<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="PreOrden.aspx.cs" Inherits="Almacen.Web.Aplicacion.Almacen.PreOrden" %>
<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaPrivada.Master" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">

   <script src="/Incluir/Javascript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
   <script src="/Incluir/Javascript/jquery.ui.datepicker-es.js" type="text/javascript"></script>
   <script src="/Incluir/Javascript/Calendar.js" type="text/javascript"></script>
      
   
   
  <script language="javascript" type="text/javascript">

      function pageLoad(sender, args) {
         SetNewCalendar("#<%= FechaPreOrdenNuevo.ClientID %>");
      }
      
   </script>


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
                            <td class="SubTituloDiv">
                                Pre Orden de Compra
                            </td>
                            <td class="Search"><asp:TextBox CssClass="SearchBox" ID="SearchText" MaxLength="50" runat="server"></asp:TextBox>&nbsp;</td>
                            <td class="Icon"><asp:ImageButton ID="BotonBusquedaRapida" ImageUrl="~/Image/Icon/SearchIcon.gif"  runat="server" ToolTip="Buscar" /></td> 
                        </tr>
                    </table>
                </div>
                <br />

		  <asp:Panel CssClass="NewRowDiv" ID="PanelNuevoRegistro" Visible="true" runat="server">
                    <table class="FormTable">
                    
							 <tr>
								 <td colspan="3" class="SubTituloDiv">
									Datos Generales
								</td>
							 </tr>
						<%--<tr>
                            <td class="Nombre">Pre Orden</td>
                            <td class="Es">*</td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="PreOrdenNuevo" Visible ="false"   runat="server" ></asp:TextBox></td>
                        </tr>--%>
                    
						 <tr>
                            <td class="Nombre">Fecha de PreOrden</td>
                            <td class="Required">*</td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="FechaPreOrdenNuevo"  runat="server" ></asp:TextBox></td>
                        </tr>
                        
                         <tr>
                            <td class="Nombre">Solicitante</td>
                            <td class="Required">*</td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="SolicitanteIdNuevo" OnSelectedIndexChanged="ddlSolicitante_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>                              
                            </td>
                        </tr>
                        
                        
                         <tr>
                            <td class="Nombre">Jefe Inmediato</td>
                            <td class="Required">*</td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="JefeInmediatoIdNuevo"  runat="server"></asp:DropDownList>                              
                            </td>
                        </tr>
                        
                        
                        	 <tr>
								 <td colspan="3" class="SubTituloDiv">
									Detalle de Articulos
								</td>
							 </tr>
                        
                         <tr>
                            <td class="Nombre">Clave</td>
                            <td class="Required">*</td>
                            <td class="Campo"> <asp:TextBox ID="ClaveNuevo" CssClass="CajaTextoMediana" MaxLength="15" OnTextChanged ="LinkBuscarClave_SelectedTextChanged" AutoPostBack="true"  runat="server"></asp:TextBox>     
                            
                           <%--  <asp:Panel ID="PanelBuscarClave" runat="server" DefaultButton="LinkBuscarClave">
                             <asp:TextBox ID="ClaveNuevo" CssClass="CajaTextoMediana" MaxLength="15" runat="server"></asp:TextBox>
                             <asp:LinkButton ID="LinkBuscarClave" OnClick="LinkBuscarClave_Click" Visible ="false"  ValidationGroup="BuscarClave" runat="server" Text="" Width="0px"></asp:LinkButton>
                             </asp:Panel>
--%>                            </td>
                        </tr>
						
                        <tr>
                            <td class="Nombre">Familia</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="FamiliaIdNuevo" Enabled ="false"   runat="server"></asp:DropDownList>                              
                            </td>
                        </tr>
                        <tr>
                            <td class="Nombre">SubFamilia</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="SubFamiliaIdNuevo"  Enabled ="false"  runat="server"></asp:DropDownList></td>
                        </tr>
                        
                        <tr>
                            <td class="Nombre">Marca</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="MarcaIdNuevo" Enabled ="false" runat="server"></asp:DropDownList></td>
                        </tr>
                        
                         <tr>
                            <td class="Nombre">Descripción</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="DescripcionNuevo" Enabled ="false"   runat="server" ></asp:TextBox></td>
                        </tr>
                    
						 <tr>
                            <td class="Nombre">Cantidad</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CantidadNuevo" MaxLength="10" runat="server" Text=""></asp:TextBox></td>
                           
                        </tr>
                        
                        <tr>
                            <td colspan="3">
                            
                            
                                <asp:Label CssClass="TextoError" ID="AgregarEtiquetaMensaje" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:ImageButton AlternateText="Guardar" ID="BotonAgregar" ImageUrl="~/Imagen/Boton/BotonAgregar.png" OnClick="BotonAgregar_Click" runat="server" ValidationGroup="Save" />&nbsp;&nbsp;
                              </td>
                        </tr>
                    </table>
                </asp:Panel>



           <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>

			<div>
                    <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                        CssClass="TablaInformacion" DataKeyNames="PreOrdenId, ProductoId" ID="TablaPreOrden" OnRowCommand="TablaPreOrden_RowCommand" runat="server" PageSize="10">
                        <EmptyDataTemplate>
                            <table class="TablaVacia">
                                <tr class="Encabezado">
                                    <th style="width: 10px;"></th>
                                    <th style="width: 30px;">Clave</th>
                                    <th style="width: 100px;">Nombre</th>  
                                    <th style="width: 80px;">Familia</th>                                    
                                    <th style="width: 60px;">SubFamilia</th>   
                                    <th style="width: 60px;">Marca</th>  
                                    <th style="width: 20px;">Cantidad</th>  
                                </tr>
                                <tr>
                                    <td colspan="7" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                                </tr>
                            </table>
                      </EmptyDataTemplate>
                      
                        <HeaderStyle CssClass="Encabezado" />
                        <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                        <Columns>
                           <%-- <asp:TemplateField HeaderText="">
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
                            </asp:TemplateField> --%>
                             
                           <asp:BoundField DataField="Clave" HeaderText="Clave" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>                                                      
                            
                            <asp:BoundField DataField="Descripcion" HeaderText="Producto" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>    
                            
                              <asp:BoundField DataField="FamiliaNombre" HeaderText="Familia" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>                  
                          
                            <asp:BoundField DataField="SubFamiliaNombre" HeaderText="SubFamilia" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="MarcaNombre" HeaderText="Marca" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
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
                               <asp:CustomValidator CssClass="TextoError" ControlToValidate="FechaPreOrdenNuevo" EnableClientScript="false" ErrorMessage="" ID="FechaPreOrden" OnServerValidate="FechaPreOrden_Validate"  runat="server" SetFocusOnError="true" ValidationGroup="Guardar" ValidateEmptyText="True"></asp:CustomValidator>
                                 
                              <br />
                              <asp:ImageButton AlternateText="Guardar" ID="BotonGuardarPreOrden"  ImageUrl="/Imagen/Boton/BotonGuardar.png" OnClick="BotonGuardar_Click" runat="server" />&nbsp;&nbsp;
                                <asp:ImageButton AlternateText="Limpiar" ID="BotonLimpiarRegistro"  ImageUrl="/Imagen/Boton/BotonLimpiar.png" runat="server" />&nbsp;&nbsp;
                                <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarPreOrden" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                                <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimir"  ImageUrl="/Imagen/Boton/BotonImprimir.png"  runat="server" />&nbsp;&nbsp;
                               
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
                
                
                     <asp:HiddenField ID="ProductoIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="ClaveIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="SolicitanteIdHidden" runat="server" Value="0" />
                     <asp:HiddenField ID="TemporalPreOrdenIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="TemporalProducto" runat="server" Value="" />
                
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>



</asp:Content>
