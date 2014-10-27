<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Empleado.aspx.cs" Inherits="Activos.Web.Aplicacion.Catalogo.Empleado" Title="" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarJefe" Src="~/Incluir/ControlesWeb/ControlBuscarJefe.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
   <script language="javascript" src="/Incluir/Javascript/ValidarFormulario.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" SeccionMenu="Catalogos" runat="server" />
    </div>
    
    <div class="DivContenido">
        <div class="DivContenidoTitulo">
            <div class="DivTitulo">Catálogo de empleados</div>
        </div>
        
        <div class="DivInformacionContenido">
            <asp:UpdatePanel ID="ActualizarTablaEmpleado" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                
                    <asp:Panel CssClass="Superposicion" ID="pnlFondo" runat="server" Visible="False"></asp:Panel>
                
                    <wuc:BuscarJefe ID="ControlBuscarJefe" runat="server" />
                
                    <asp:Panel ID="PanelTablaEmpleado" runat="server">
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
                                    <td class="Nombre">Departamento</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="DepartamentoBusqueda" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Edificio</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="EdificioBusqueda" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Puesto</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="PuestoBusqueda" runat="server"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Nombre</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="NombreBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Email del trabajo</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CorreoElectronicoBusqueda" MaxLength="65" runat="server" Text=""></asp:TextBox></td>
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
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="NombreNuevo" MaxLength="30" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Apellido Paterno</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="ApellidoPaternoNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Apellido Materno</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="ApellidoMaternoNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">RFC</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="RFCNuevo" MaxLength="15" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Calle</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CalleNuevo" MaxLength="30" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Número</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="NumeroNuevo" MaxLength="10" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Colonia</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="ColoniaNuevo" MaxLength="30" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">CódigoPostal</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="CodigoPostalNuevo" MaxLength="5" runat="server" Text=""></asp:TextBox></td>
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
                                    <td class="Nombre">Teléfono de Casa</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="TelefonoCasaNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Celular</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CelularNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Email</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="EmailNuevo" MaxLength="30" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Número de Empleado</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="NumeroEmpleadoNuevo" MaxLength="10" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Teléfono del Trabajo</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="TelefonoTrabajoNuevo" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Email del Trabajo</td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="TrabajoEmailNuevo" MaxLength="30" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Departamento</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="DepartamentoNuevo" runat="server" ></asp:DropDownList></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Edificio</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="EdificioNuevo" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Puesto</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="PuestoNuevo" runat="server" ></asp:DropDownList></td>
                                    <td class="EspacioColumna"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Jefe</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreEmpleadoJefe" Enabled="false" MaxLength="150" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"><asp:ImageButton ImageUrl="/Imagen/Icono/ImagenBuscar.gif" ID="BotonBuscarJefe" OnClick="BotonBuscarJefe_Click" runat="server" /></td>
                                    <td class="Nombre"></td>
                                    <td class="Requerido"></td>
                                    <td class="Campo"></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Estatus</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="EstatusNuevo" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NombreNuevo" Display="Dynamic" ErrorMessage="" ID="NombreRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="ApellidoPaternoNuevo" Display="Dynamic" ErrorMessage="" ID="ApellidoPaternoRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="EstadoNuevo" Display="Dynamic" ErrorMessage="" ID="EstadoRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="CiudadNuevo" Display="Dynamic" ErrorMessage="" ID="CiudadRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NumeroEmpleadoNuevo" Display="Dynamic" ErrorMessage="" ID="NumeroEmpleadoRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="DepartamentoNuevo" Display="Dynamic" ErrorMessage="" ID="DepartamentoRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="EdificioNuevo" Display="Dynamic" ErrorMessage="" ID="EdificioRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="PuestoNuevo" Display="Dynamic" ErrorMessage="" ID="PuestoRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="EstatusNuevo" Display="Dynamic" ErrorMessage="" ID="EstatusRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        
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
                                CssClass="TablaInformacion" DataKeyNames="EmpleadoId" ID="TablaEmpleado" runat="server" 
                                OnPageIndexChanging="TablaEmpleado_PageIndexChanging" OnRowCommand="TablaEmpleado_RowCommand" PageSize="10">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th style="width: 30px;"></th>
                                            <th>Nombre</th>
                                            <th style="width: 180px;">Email del trabajo</th>
                                            <th style="width: 180px;">Dirección</th>
                                            <th style="width: 180px;">Puesto</th>
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
                                            <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaNombre" runat="server" Text='<%#Eval("NombreEmpleadoCompleto")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TrabajoEmail" HeaderText="Email del trabajo" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NombreDireccion" HeaderText="Dirección" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="280px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NombrePuesto" HeaderText="Puesto" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="280px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:Panel>

                    <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaEmpleado" ID="ProgresoTablaEmpleado" runat="server">
                        <ProgressTemplate>
                            <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <asp:HiddenField ID="EmpleadoIdHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="EmpleadoIdJefeHidden" runat="server" Value="0" />
                </ContentTemplate>

                <Triggers>
                    
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
