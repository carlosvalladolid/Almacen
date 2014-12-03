<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="OrdenSalida.aspx.cs" Inherits="Almacen.Web.Aplicacion.Almacen.OrdenSalida" Title="" %>

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
                <div class="PageTitleDiv">
                    <table class="PageTitleTable">
                        <tr>
                            <td class="Title">
                                Orden de salida
                            </td>
                            <td class="Search"></td>
                            <td class="Icon"></td> 
                        </tr>
                    </table>
                </div>

                <div class="SubTituloDiv">Solicitante</div>

                <table class="TablaFormulario">
                    <tr>
                        <td class="Nombre">Requisición</td>
                        <td class="Espacio"></td>
                        <td class="Campo">
                            <asp:TextBox CssClass="CajaTextoChica" ID="RequisicionBox" MaxLength="10" runat="server" Text="">
                            </asp:TextBox><asp:ImageButton ID="ImagenBuscarPreOrden" ImageUrl="/Imagen/Icono/ImagenBuscar.gif" runat="server" onclick="ImagenBuscarPreOrden_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Nombre">Solicitante</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="SolicitanteBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Dependencia</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="DependenciaBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Dirección</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="DireccionBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Puesto</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="PuestoBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Jefe inmediato</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="JefeBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                </table>

                <br />
                <div class="SubTituloDiv">Artículos</div>

                <table class="TablaFormulario">
                    <tr>
                        <td class="Nombre">Clave</td>
                        <td class="Espacio"></td>
                        <td class="Campo">
                            <asp:TextBox CssClass="CajaTextoChica" ID="ClaveRequisicionBox" MaxLength="10" runat="server" Text="">
                            </asp:TextBox><asp:ImageButton ID="ImageButton1" ImageUrl="/Imagen/Icono/ImagenBuscar.gif" runat="server" onclick="ImagenBuscarPreOrden_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Nombre">Familia</td>
                        <td class="Espacio"></td>
                          <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="FamiliaBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Sub familia</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="SubFamiliaBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>                 </tr>
                    <tr>
                        <td class="Nombre">Marca</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="MarcaBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                   </tr>
                    <tr>
                        <td class="Nombre">Descripción</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="DescripcionBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Cantidad</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoChica" ID="CantidadBox" MaxLength="10" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td coslpan="3">
                            <br />
                            <asp:Button ID="BotonAgregar" runat="server" Text="Agregar" />
                        </td>
                    </tr>
                </table>

                <div class="DivTabla">
                    <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                        CssClass="TablaInformacion" DataKeyNames="OrdenId" ID="TablaOrden"
                        runat="server" PageSize="10">
                        <EmptyDataTemplate>
                            <table class="TablaVacia">
                                <tr class="Encabezado">
                                    <th style="width: 35px;"></th>
                                    <th style="width: 100px;">Clave</th>
                                    <th>Descripción</th>
                                    <th style="width: 125px;">Familia</th>
                                    <th style="width: 125px;">Marca</th>
                                    <th style="width: 125px;">Cantidad</th> 
                                 </tr>
                                <tr>
                                    <td colspan="6" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="Encabezado" />
                        <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton AlternateText="Quitar" CommandArgument='<%#Container.DataItemIndex%>' CommandName="Agregar" ID="BotonQuitar" ImageUrl="/Imagen/Icono/IconoQuitar.png" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="35px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ClaveProducto" HeaderText="Clave" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DescripcionProducto" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NombreFamilia" HeaderText="Familia" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="125px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NombreMarca" HeaderText="Marca" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="125px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="125px" />
                            </asp:BoundField>                                 
                        </Columns>
                    </asp:GridView>
                </div>

                <table class="TablaFormulario">
                    <tr>
                        <td colspan="3">
                            <br />
                            <asp:ImageButton AlternateText="Guardar" ID="BotonGuardar" ImageUrl="/Imagen/Boton/BotonGuardar.png" runat="server" onclick="BotonGuardar_Click" />&nbsp;&nbsp;
                            <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelar" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                        </td>
                    </tr>
                </table>

                <asp:HiddenField ID="OrdenIdHidden" runat="server" Value="" />

                <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="../../Image/Icon/LoadingIcon.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
