<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Marca.aspx.cs" Inherits="Almacen.Web.Aplicacion.Catalogo.Marca" Title="" %>

<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaPrivada.Master" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    <script language="javascript" src="/Incluir/Javascript/ValidarFormulario.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
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
                                Catálogo de marcas
                            </td>
                            <td class="Search"><asp:TextBox CssClass="SearchBox" ID="TextoBusquedaRapida" MaxLength="50" runat="server"></asp:TextBox>&nbsp;</td>
                            <td class="Icon"><asp:ImageButton ID="BotonBusquedaRapida" ImageUrl="~/Imagen/Icono/ImagenBuscar.gif" OnClick="BotonBusquedaRapida_Click" runat="server" ToolTip="Buscar" /></td> 
                        </tr>
                    </table>
                </div>

                <asp:Panel CssClass="SearchDiv" ID="PanelBusquedaAvanzada" Visible="false" runat="server">
                    <table class="TablaFormulario">
                        <tr>
                            <td class="Nombre">Nombre</td>
                            <td class="Espacio">&nbsp;</td>
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

                <asp:Panel CssClass="NewRowDiv" ID="PanelNuevoRegistro" Visible="false" runat="server">
                    <table class="TablaFormulario">
                        <tr>
                            <td class="Nombre">Dependencia</td>
                            <td class="Requerido">*</td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="DependenciaNuevo" MaxLength="30" runat="server" ></asp:DropDownList></td>
                            <td class="EspacioColumna"></td>
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
                                <asp:CompareValidator CssClass="TextoError" ControlToValidate="DependenciaNuevo" Display="Dynamic" ErrorMessage="" ID="DependenciaRequerido" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
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

                <div>
                    <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                        CssClass="TablaInformacion" DataKeyNames="MarcaId" ID="TablaMarca" OnPageIndexChanging="TablaMarca_PageIndexChanging"
                        OnRowCommand="TablaMarca_RowCommand" PageSize="10" runat="server">
                        <EmptyDataTemplate>
                            <table class="TablaVacia">
                                <tr class="Encabezado">
                                    <th style="width: 30px;"></th>
                                    <th>Nombre</th>
                                    <th style="width: 250px;">Dependencia</th>
                                    <th style="width: 100px;">Estatus</th>   
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
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaNombre" runat="server" Text='<%#Eval("Nombre")%>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="DependenciaNombre" HeaderText="Dependencia" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EstatusNombre" HeaderText="Estatus" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>

                <asp:HiddenField ID="MarcaIdHidden" runat="server" Value="0" />


                <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="../../Image/Icon/LoadingIcon.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
