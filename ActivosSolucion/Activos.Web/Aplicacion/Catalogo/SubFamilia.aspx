<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="SubFamilia.aspx.cs" Inherits="Activos.Web.Aplicacion.Catalogo.SubFamilia" %>

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
            <div class="DivTitulo">Catálogo de SubFamilias</div>
        </div>
        
        <div class="DivInformacionContenido">
            <asp:UpdatePanel ID="ActualizarTablaSubFamilia" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PanelTablaSubFamilia" runat="server">
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
                                    <td class="Nombre">Familia</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="FamiliaNuevo" MaxLength="30" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Nombre</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreNuevo" MaxLength="100" runat="server" Text=""></asp:TextBox></td>
                                    <td class="EspacioColumna"></td>                                   
                                </tr>       
                                <tr>
                                     <td class="Nombre">Estatus</td>
                                    <td class="Requerido">*</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="EstatusNuevo" MaxLength="30" runat="server" ></asp:DropDownList></td>
                             </tr>                        
                                <tr>
                                    <td colspan="3">
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="FamiliaNuevo" Display="Dynamic" ErrorMessage="" ID="FamiliaRequerido" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        <asp:CompareValidator CssClass="TextoError" ControlToValidate="EstatusNuevo" Display="Dynamic" ErrorMessage="" ID="EstatusRequerido" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                        <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NombreNuevo" Display="Dynamic" ErrorMessage="" ID="NombreRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
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
                                CssClass="TablaInformacion" DataKeyNames="SubFamiliaId" ID="TablaSubFamilia"
                                 OnPageIndexChanging="TablaSubFamilia_PageIndexChanging" OnRowCommand="TablaSubFamiliaRowCommand" runat="server" PageSize="10">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th style="width: 30px;"></th>
                                            <th>Nombre</th>
                                            <th style="width: 200px;">Familia</th>
                                            <th style="width: 200px;">Estatus</th>                                           
                                         </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
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
                                    <asp:BoundField DataField="FamiliaNombre" HeaderText="Familia" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EstatusNombre" HeaderText="Estatus" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                    </asp:BoundField>                                    
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:Panel>

                    <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaSubFamilia" ID="ProgresoTablaSubFamilia" runat="server">
                        <ProgressTemplate>
                            <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <asp:HiddenField ID="SubFamiliaIdHidden" runat="server" Value="0" />
                </ContentTemplate>

                <Triggers>
                    
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>