<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Almacen.Web.Aplicacion.Inicio" Title="" %>

<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaPrivada.Master" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="LeftBodyDiv">
        <wuc:ControlMenuIzquierdo ID="ControlMenuIzquierdo" runat="server" />
    </div>

    <div class="RightBodyDiv">
        <asp:UpdatePanel ID="PageUpdate" runat="server">
            <ContentTemplate>
                <table class="RightMenuTable">
                    <tr>
                        <td class="Info">
                            <div class="MessageDiv">
                                <img alt="Mensajes" src="/Imagen/Icono/BubbleIcon.png" title="Mensajes" />
                                Mensajes

                                <br />
                                <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                                    CssClass="TablaInformacion" DataKeyNames="MessageId" ID="MessageGrid" runat="server" PageSize="10">
                                    <EmptyDataTemplate>
                                        <table class="TablaVacia">
                                            <tr class="Encabezado">
                                                <th style="width: 30px;"></th>
                                                <th>De</th>
                                                <th style="width: 300px;">Mensaje</th>
                                                <th style="width: 100px;">Fecha</th>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="text-align: center;">Está funcionalidad no se encuentra activada</td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="Encabezado" />
                                    <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="DeleteCheck" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="De">
                                            <ItemTemplate>
                                                <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="NameLink" runat="server" Text='<%#Eval("UserName")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Message" HeaderText="Mensaje" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InsertDate" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                        <td class="Banner"><img alt="Banner" src="/Imagen/Banner/PublicityBanner.jpg" /></td>
                    </tr>
                </table>

                <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="../../Image/Icon/LoadingIcon.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
