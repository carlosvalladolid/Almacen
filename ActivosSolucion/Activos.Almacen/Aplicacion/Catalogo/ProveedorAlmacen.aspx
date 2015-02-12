<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="ProveedorAlmacen.aspx.cs" Inherits="Activos.Almacen.Aplicacion.Catalogo.ProveedorAlmacen" %>

<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaPrivada.Master" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>
<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">


    <script language="javascript" src="/Incluir/Javascript/ValidarFormulario.js" type="text/javascript"></script>
    
    <script type="text/javascript">
       // Solo permite ingresar numeros.
        function soloNumeros(e){
	        var key = window.Event ? e.which : e.keyCode
	        return (key >= 48 && key <= 57)
        }
</script>
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
                                Catálogo de Proveedor
                            </td>
                            <td class="Search"><asp:TextBox CssClass="SearchBox" ID="TextoBusquedaRapida" MaxLength="50" runat="server"></asp:TextBox>&nbsp;</td>
                            <td class="Icon"><asp:ImageButton ID="BotonBusquedaRapida" ImageUrl="~/Imagen/Icono/ImagenBuscar.gif" OnClick="BotonBusquedaRapida_Click" runat="server" ToolTip="Buscar" /></td> 
                        </tr>
                    </table>
                </div>

                <asp:Panel CssClass="SearchDiv" ID="PanelBusquedaAvanzada" Visible="true" runat="server">
                   <table class="TablaFormulario">
                        <tr>
                            <td class="Nombre">Nombre</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreBusqueda" MaxLength="100" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                              <td colspan="3">
                                <br />
                                <asp:ImageButton AlternateText="Buscar" ID="BotonBusqueda" ImageUrl="/Imagen/Boton/BotonBuscar.png" OnClick="BotonBusqueda_Click" runat="server" />&nbsp;&nbsp;
                                <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarBusqueda" ImageUrl="/Imagen/Boton/BotonCancelar.png" OnClick="BotonCancelarBusqueda_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
               

                <asp:Panel CssClass="NewRowDiv" ID="PanelNuevoRegistro" Visible="true" runat="server">
                    <table class="TablaFormulario">
                            
                                <tr>
                                    <td class="Nombre">Dependencia</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="DependenciaNuevo" runat="server" ></asp:DropDownList></td>
                                    <td class="Espacio"></td>
                                </tr>                                
                                 <tr>
                                    <td class="Nombre">Estado</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="EstadoNuevo" OnSelectedIndexChanged = "EstadoNuevo_SelectedIndexChanged" AutoPostBack="true"  runat="server" ></asp:DropDownList></td>
                                    <td class="Espacio"></td>                                 
                                 </tr>
                                   <tr>                                  
                                    <td class="Nombre">Ciudad</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="CiudadNuevo" runat="server" ></asp:DropDownList></td>
                                    <td class="Espacio"></td>
                                  </tr>
                                 <tr>  
                                    <td class="Nombre">Banco</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="BancoNuevo" runat="server" ></asp:DropDownList></td>
                                    <td class="Espacio"></td>
                                 </tr>                                
                               
                                  <tr>  
                                     <td class="Nombre">Nombre</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreNuevo" MaxLength="100" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                 </tr>                                
                                 <tr>                                
                                    <td class="Nombre">Otra Ciudad</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="OtraCiudadNuevo" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                </tr>
                                                           
                                <tr>
                                    <td class="Nombre">RFC</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="RFCNuevo" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                </tr>                                
                                 <tr>
                                     <td class="Nombre">Calle</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CalleNuevo" MaxLength="100" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                </tr>    
                                
                                 <tr>
                                    <td class="Nombre">Numero</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="NumeroNuevo" MaxLength="30" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                 </tr>                                
                                 <tr>
                                    <td class="Nombre">Colonia</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="ColoniaNuevo" MaxLength="100" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                </tr> 
                                
                                 <tr>
                                    <td class="Nombre">Codigo Postal</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CodigoPostalNuevo" MaxLength="5" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                 </tr>                                
                                 <tr>                                    
                                    <td class="Nombre">Telefono</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="TelefonoNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                </tr> 
                                <tr>
                                    <td class="Nombre">Nombre Contacto</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="NombreContactoNuevo" MaxLength="100" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                </tr>                                
                                 <tr>
                                     <td class="Nombre">Email</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="EmailNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                </tr> 
                                
                                <tr>
                                    <td class="Nombre">Cuenta</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CuentaNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                </tr> 
                                                               
                                 <tr>
                                    <td class="Nombre">Clabe</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="ClabeNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                </tr> 
                                
                                 <tr>
                                    <td class="Nombre">Monto Maximo de Compra</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="MontoMaximoCompraNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                    <td class="Espacio"></td>
                                </tr> 
                                                           
                                <tr>
                                    <td colspan="3">
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="DependenciaNuevo" Display="Dynamic" ErrorMessage="" ID="DependenciaRequerido" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="EstadoNuevo" Display="Dynamic" ErrorMessage="" ID="EstadoRequerido" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="CiudadNuevo" Display="Dynamic" ErrorMessage="" ID="CiudadRequerido" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NombreNuevo" Display="Dynamic" ErrorMessage="" ID="NombreRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NombreContactoNuevo" Display="Dynamic" ErrorMessage="" ID="NombreContactoRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                     
                                       <br />
                                      <asp:ImageButton AlternateText="Guardar" ID="BotonGuardar" ImageUrl="/Imagen/Boton/BotonGuardar.png" OnClick="BotonGuardar_Click" runat="server" ValidationGroup="Guardar" />&nbsp;&nbsp;
                                        <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarNuevo" ImageUrl="/Imagen/Boton/BotonCancelar.png" OnClick="BotonCancelarNuevo_Click" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Los campos marcados con <span class="TextoError">*</span> son obligatorios
                                    </td>
                                </tr>
                            </table>
                </asp:Panel>

                <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>

			   <div class="DivTabla">
                            <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                                CssClass="TablaInformacion" DataKeyNames="ProveedorId" ID="TablaProveedor" OnPageIndexChanging="TablaProveedor_PageIndexChanging" OnRowCommand="TablaProveedor_RowCommand" runat="server" PageSize="10">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th style="width: 30px;"></th>
                                            <th>Nombre</th>
                                            <th style="width: 180px;">RFC</th>
                                            <th style="width: 180px;">Ciudad</th>
                                            <th style="width: 100px;">Telefono</th>
                                            <th style="width: 230px;">Contacto</th>
                                            <th style="width: 180px;">Email</th>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
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
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaNombre" runat="server" Text='<%#Eval("Nombre")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="RFC" HeaderText="RFC" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="NombreCiudad" HeaderText="Ciudad" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Telefono" HeaderText="Telefono" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundField>
                                    
                                     <asp:BoundField DataField="NombreContacto" HeaderText="Contacto" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="230px" />
                                    </asp:BoundField>
                                    
                                     <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>

                <br /><br />
                <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="/Image/Icon/LoadingIcon.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
                
                <asp:HiddenField ID="ProveedorIdHidden" runat="server" Value="0" />
              
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
