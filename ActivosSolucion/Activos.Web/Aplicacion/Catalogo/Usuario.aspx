<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="Activos.Web.Aplicacion.Catalogo.Usuario" %>

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
            <div class="DivTitulo">Catálogo de usuarios</div>
        </div>

        <div class="DivInformacionContenido">
            <asp:UpdatePanel ID="ActualizarTablaUsuario" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PanelTablaUsuario" runat="server">
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
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="NombreBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Cuenta de usuario</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CuentaUsuarioBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Rol de usuario</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="RolBusqueda" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Estatus</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="EstatusBusqueda" runat="server" ></asp:DropDownList></td>
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

                        <asp:Panel CssClass="DivEditarRegistro" id="PanelNuevoRegistro" Visible="false" runat="server">
                            <table class="TablaFormulario">
                                <tr>
                                    <td class="Nombre">Nombre</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="NombreNuevo" MaxLength="30" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                    <td class="Nombre">Apellido paterno</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="ApellidoPaternoNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Apellido materno</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="ApellidoMaternoNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                    <td class="Nombre">Cuenta de usuario</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CuentaUsuarioNuevo" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Rol de usuario</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="RolNuevo" runat="server" ></asp:DropDownList></td>
                                    <td class="EspacioColumna"></td>
                                    <td class="Nombre">Estatus</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="EstatusNuevo" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NombreNuevo" Display="Dynamic" ErrorMessage="" ID="NombreRequerido" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="ApellidoPaternoNuevo" Display="Dynamic" ErrorMessage="" ID="ApellidoPaternoRequerido" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CuentaUsuarioNuevo" Display="Dynamic" ErrorMessage="" ID="CuentaUsuarioRequerido" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="TextoError" ControlToValidate="CuentaUsuarioNuevo" Display="Dynamic" ErrorMessage="" ID="CuentaUsuarioExpresion" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Guardar"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="RolNuevo" Display="Dynamic" ErrorMessage="" ID="RolRequerido" Operator="GreaterThan" runat="server" ValidationGroup="Guardar" ValueToCompare="0"></asp:CompareValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="EstatusNuevo" Display="Dynamic" ErrorMessage="" ID="EstatusRequerido" Operator="GreaterThan" runat="server" ValidationGroup="Guardar" ValueToCompare="0"></asp:CompareValidator>
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

                        <div id="DivTabla">
                            <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                                CssClass="TablaInformacion" DataKeyNames="UsuarioId" ID="TablaUsuario" OnRowCommand="TablaUsuario_RowCommand"
                                runat="server" PageSize="10">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th style="width: 30px;"></th>
                                            <th>Nombre</th>
                                            <th style="width: 180px;">Cuenta usuario</th>
                                            <th style="width: 180px;">Rol</th>
                                            <th style="width: 125px;">Estatus</th>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
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
                                            <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaNombre" runat="server" Text='<%#Eval("NombreUsuario")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CuentaUsuario" HeaderText="Correo electrónico" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NombreRol" HeaderText="Rol de usuario" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NombreEstatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="125px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:Panel>

                    <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaUsuario" ID="ProgresoTablaUsuario" runat="server">
                        <ProgressTemplate>
                            <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                    <asp:HiddenField ID="UsuarioIdHidden" runat="server" Value="0" />
                </ContentTemplate>

                <Triggers>
                    
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="ActualizarFormularioUsuario" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PanelFormularioUsuario" runat="server">
                        
                    </asp:Panel>
                </ContentTemplate>

                <Triggers>
                    
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
