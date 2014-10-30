<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Marca.aspx.cs" Inherits="Almacen.Web.Aplicacion.Catalogo.Marca" Title="" %>

<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaPrivada.Master" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorEncabezado" runat="server">

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
                            <td class="Search"><asp:TextBox CssClass="SearchBox" ID="SearchText" MaxLength="50" runat="server"></asp:TextBox>&nbsp;</td>
                            <td class="Icon"><asp:ImageButton ID="BotonBusquedaRapida" ImageUrl="~/Image/Icon/SearchIcon.gif" OnClick="BotonBusquedaRapida_Click" runat="server" ToolTip="Buscar" /></td> 
                        </tr>
                    </table>
                </div>

                <asp:Panel CssClass="SearchDiv" ID="SearchPanel" Visible="false" runat="server">
                    
                </asp:Panel>

                <asp:Panel CssClass="NewRowDiv" ID="RowPanel" Visible="false" runat="server">
                    <table class="FormTable">
                        <tr>
                            <td class="Name">Idioma</td>
                            <td class="Required">*</td>
                            <td class="Field">
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="Name">Nombre</td>
                            <td class="Required">*</td>
                            <td class="Field"></td>
                        </tr>
                        <tr>
                            <td class="Name">Descripción</td>
                            <td class="Required"></td>
                            <td class="Field"></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br />
                                <asp:ImageButton AlternateText="Guardar" ID="SaveButton" ImageUrl="~/Image/Button/SaveButton.png" runat="server" ValidationGroup="Save" />&nbsp;&nbsp;
                                <asp:ImageButton AlternateText="Cancelar" ID="CancelButton" ImageUrl="~/Image/Button/CancelButton.png" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <div>
                    <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                        CssClass="TablaInformacion" DataKeyNames="MarcaId" ID="TablaMarca" runat="server" PageSize="10">
                        <EmptyDataTemplate>
                            <table class="TablaVacia">
                                <tr class="Encabezado">
                                    <th style="width: 30px;"></th>
                                    <th>Nombre</th>
                                    <th style="width: 200px;">Dependencia</th>
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
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EstatusNombre" HeaderText="Estatus" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>

                


                <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="../../Image/Icon/LoadingIcon.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
