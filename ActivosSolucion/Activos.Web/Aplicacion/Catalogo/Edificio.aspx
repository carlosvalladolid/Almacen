<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Edificio.aspx.cs" Inherits="Activos.Web.Aplicacion.Catalogo.Edificio" Title="" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    <script language="javascript" src="/Incluir/Javascript/ValidarFormulario.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" SeccionMenu="Catalogos" runat="server" />
    </div>

    <div class="DivContenido">
        <div class="DivContenidoTitulo">
            <div class="DivTitulo">Catálogo de edificios</div>
        </div>

        <div class="DivInformacionContenido">
            <asp:UpdatePanel ID="ActualizarTablaEdificio" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PanelTablaEdificio" runat="server">
                        <div class="DivTituloPagina">
                            <table class="TablaTituloPagina">
                                <tr>
                                    <td class="Titulo">
                                        <asp:LinkButton ID="BusquedaAvanzadaLink" runat="server" onclick="BusquedaAvanzadaLink_Click">Búsqueda avanzada</asp:LinkButton>
                                        &nbsp;&nbsp;|&nbsp;&nbsp;
                                        <asp:LinkButton ID="NuevoRegistroLink" runat="server" onclick="NuevoRegistroLink_Click">Nuevo</asp:LinkButton>
                                        &nbsp;&nbsp;|&nbsp;&nbsp;
                                        <asp:LinkButton ID="EliminarRegistroLink" runat="server" onclick="EliminarRegistroLink_Click">Eliminar</asp:LinkButton>
                                    </td>
                                    <td class="Buscar"><asp:TextBox CssClass="BusquedaRapida" ID="TextoBusquedaRapida" MaxLength="50" runat="server"></asp:TextBox>&nbsp;</td>
                                    <td class="Icono"><asp:ImageButton ID="ImagenBuscar" ImageUrl="/Imagen/Icono/ImagenBuscar.gif" OnClick="ImagenBuscar_Click" runat="server" ToolTip="Buscar" /></td>
                                </tr>
                            </table>
                        </div>

                        <asp:Panel CssClass="DivBusquedaAvanzada" ID="PanelBusquedaAvanzada" Visible="false" runat="server">
                            <table class="TablaFormulario">
                                <tr>
                                    <td class="Nombre">Nombre</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreBusqueda" MaxLength="100" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Estado</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="EstadoBusqueda" OnSelectedIndexChanged="Estado_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Ciudad</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="CiudadBusqueda" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Calle</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CalleBusqueda" MaxLength="30" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td class="Nombre">Código postal</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="CodigoPostalBusqueda" MaxLength="5" runat="server" Text=""></asp:TextBox></td>
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
                        
                        <asp:Panel CssClass="DivNuevoRegistro" id="PanelNuevoRegistro" Visible="false" runat="server">
                            <table class="TablaFormulario">
                                <tr>
                                    <td class="Nombre">Nombre</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreNuevo" MaxLength="100" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Calle</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CalleNuevo" MaxLength="30" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Número</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="NumeroNuevo" MaxLength="10" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Colonia</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="ColoniaNuevo" MaxLength="30" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Número interior</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="NumeroInteriorNuevo" MaxLength="10" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Codigo Postal</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="CodigoPostalNuevo" MaxLength="5" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Nombre del arrendado</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreArrendadoNuevo" MaxLength="100" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Teléfono del arrendado</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="TelefonoArrendadoNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">E-mail del arrendado</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="EmailArrendadoNuevo" MaxLength="30" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Estatus</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="EstatusNuevo" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Estado</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="EstadoNuevo" OnSelectedIndexChanged="EstadoNuevo_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:DropDownList></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Ciudad</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="CiudadNuevo" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NombreNuevo" Display="Dynamic" ErrorMessage="" ID="NombreRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="EstatusNuevo" Display="Dynamic" ErrorMessage="" ID="EstatusRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="EstadoNuevo" Display="Dynamic" ErrorMessage="" ID="EstadoRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="CiudadNuevo" Display="Dynamic" ErrorMessage="" ID="CiudadRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
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
                                CssClass="TablaInformacion" DataKeyNames="EdificioId" ID="TablaEdificio" runat="server" 
                                OnPageIndexChanging="TablaEdificio_PageIndexChanging" OnRowCommand="TablaEdificio_RowCommand" PageSize="10">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th style="width: 30px;"></th>
                                            <th>Nombre</th>
                                            <th style="width: 180px;">Ciudad</th>
                                            <th style="width: 70px;">Numero</th>
                                            <th style="width: 200px;">Calle</th>
                                            <th style="width: 120px;">Código Postal</th>
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
                                    <asp:BoundField DataField="CiudadNombre" HeaderText="Ciudad" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Numero" HeaderText="Número" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Calle" HeaderText="Calle" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoPostal" HeaderText="Código Postal" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:Panel>

                    <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaEdificio" ID="ProgresoTablaEdificio" runat="server">
                        <ProgressTemplate>
                            <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <asp:HiddenField ID="EdificioIdHidden" runat="server" Value="0" />
                </ContentTemplate>

                <Triggers>
                    
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="ActualizarFormularioEdificio" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PanelFormularioEdificio" runat="server">
                        
                    </asp:Panel>
                </ContentTemplate>

                <Triggers>
                    
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

