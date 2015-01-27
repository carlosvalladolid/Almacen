<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Orden.aspx.cs" Inherits="Almacen.Web.Aplicacion.Almacen.Orden" Title="" %>

<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    <link href="/Incluir/Estilo/Privado/jquery-ui-1.8.16.custom.css" rel="Stylesheet" type="text/css" />
    <link href="/Incluir/Estilo/Privado/demos.css" rel="Stylesheet" type="text/css" />

    <script src="/Incluir/Javascript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="/Incluir/Javascript/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <script language="javascript" src="/Incluir/Javascript/ValidarFormulario.js" type="text/javascript"></script>
    <script src="/Incluir/Javascript/Calendar.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad(sender, args)
        {
            SetNewCalendar("#<%= FechaOrdenBox.ClientID %>");
            SetNewCalendar("#<%= FechaFiltroInicioBox.ClientID %>");
            SetNewCalendar("#<%= FechaFiltroFinBox.ClientID %>");
            $("#<%= BotonGuardar.ClientID %>").Confirmar("<%= MensajeConfirmacion.Value%>");
            $("#<%= FechaFiltroInicioBox.ClientID %>").VerificarFechas("#<%= FechaFiltroInicioBox.ClientID %>","#<%= FechaFiltroFinBox.ClientID %>","<%= MensajeRangoDeFechasInvalido.Value %>");
            $("#<%= FechaFiltroFinBox.ClientID %>").VerificarFechas("#<%= FechaFiltroInicioBox.ClientID %>","#<%= FechaFiltroFinBox.ClientID %>","<%= MensajeRangoDeFechasInvalido.Value %>");        
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
                                Órdenes de compra
                            </td>
                            <td class="Search"></td>
                            <td class="Icon"></td> 
                        </tr>
                    </table>
                </div>

                <div class="SubTituloDiv">Pre Orden de compra</div>

                <table class="TablaFormulario">
                    <tr>
                        <td class="Nombre">Pre Orden</td>
                        <td class="Espacio"></td>
                        <td class="CampoPequenio"><asp:TextBox CssClass="CajaTextoPequenia" Enabled="false" ID="PreOrdenBusqueda" MaxLength="10" runat="server" Text=""></asp:TextBox></td>
                        <td><asp:ImageButton ID="ImagenBuscarPreOrden" ImageUrl="/Imagen/Icono/ImagenBuscar.gif" runat="server" onclick="ImagenBuscarPreOrden_Click" /></td>
                    </tr>
                </table>

                <div class="DivTabla">
                    <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                        CssClass="TablaInformacion" DataKeyNames="PreOrdenId, ClavePreOrden, ProductoId" ID="TablaPreOrden" OnRowCommand="TablaPreOrden_RowCommand"
                        runat="server" PageSize="10">
                        <EmptyDataTemplate>
                            <table class="TablaVacia">
                                <tr class="Encabezado">
                                    <th style="width: 35px;"></th>
                                    <th style="width: 100px;">Clave</th>
                                    <th>Descripción</th>
                                    <th style="width: 125px;">Familia</th>
                                    <th style="width: 125px;">Marca</th>
                                    <th style="width: 90px;">Cantidad</th> 
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
                                    <asp:ImageButton AlternateText="Agregar" CommandArgument='<%#Container.DataItemIndex%>' CommandName="Agregar" ID="BotonAgregar" ImageUrl="/Imagen/Icono/IconoAgregar.png" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="35px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ClaveProducto" HeaderText="Clave" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NombreProducto" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NombreFamilia" HeaderText="Familia" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" Width="125px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NombreMarca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" Width="125px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField> 
                        </Columns>
                    </asp:GridView>
                </div>

                <br /><br>
                <div class="SubTituloDiv">Orden de compra</div>

                <div class="DivTabla">
                    <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                        CssClass="TablaInformacion" DataKeyNames="OrdenId, ProductoId, PreOrdenId" ID="TablaOrden" OnRowCommand ="TablaOrden_RowCommand"
                        runat="server" PageSize="10">
                        <EmptyDataTemplate>
                            <table class="TablaVacia">
                                <tr class="Encabezado">
                                    <th style="width: 35px;"></th>
                                    <th style="width: 100px;">Clave</th>
                                    <th>Descripción</th>
                                    <th style="width: 125px;">Familia</th>
                                    <th style="width: 125px;">Marca</th>
                                    <th style="width: 90px;">Cantidad</th> 
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
                                    <asp:ImageButton AlternateText="Quitar" CommandArgument='<%#Container.DataItemIndex%>' CommandName="Eliminar" ID="BotonQuitar" ImageUrl="/Imagen/Icono/IconoQuitar.png" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="35px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ClaveProducto" HeaderText="Clave" ItemStyle-HorizontalAlign="Center">
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
                            <asp:TemplateField HeaderText="Cantidad">
                                <ItemTemplate>
                                    <asp:TextBox CssClass="CajaTextoPequenia" ID="CantidadBox" MaxLength="3" runat="server" Text='<%#Eval("Cantidad")%>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:TemplateField>                                
                        </Columns>
                    </asp:GridView>
                </div>

                <br />
                <table class="TablaFormulario">
                    <tr>
                        <td class="Nombre">Fecha Orden</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="FechaOrdenBox" MaxLength="10" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                </table>

                <br />
                <div class="SubTituloDiv">Datos del proveedor</div>

                <br />
                <table class="TablaFormulario">
                    <tr>
                        <td class="Nombre">Proveedor</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList AutoPostBack="true" CssClass="ComboGrande" ID="ProveedorCombo" MaxLength="30" OnSelectedIndexChanged="ProveedorCombo_SelectedIndexChanged" runat="server" ></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Teléfono</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediano" Enabled="false" ID="TelefonoBox" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Contacto</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" Enabled="false" ID="ContactoBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Correo electrónico</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" Enabled="false" ID="CorreoBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td class="Nombre">Solicitante</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList AutoPostBack="true" CssClass="ComboGrande" ID="EmpleadoCombo" MaxLength="30" runat="server" onselectedindexchanged="EmpleadoCombo_SelectedIndexChanged" ></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Jefe inmediato</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" Enabled="false" ID="JefeBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                            <asp:ImageButton AlternateText="Guardar" ID="BotonGuardar" ImageUrl="/Imagen/Boton/BotonGuardar.png" runat="server" onclick="BotonGuardar_Click" />&nbsp;&nbsp;
                            <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelar" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                            <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimir"  ImageUrl="/Imagen/Boton/BotonImprimir.png"  runat="server" />
                        </td>
                    </tr>
                </table>

                <%--POPUP Busqueda por PreOrden--%>
                <asp:Panel CssClass="Superposicion" ID="pnlFondoBuscarProducto" runat="server" Visible="false"></asp:Panel>

                <asp:Panel CssClass="PopupGrandeDiv" ID="PanelBusquedaProducto" Visible="false" runat="server">
                    <div class="PopupGrandeEncabezadoDiv">                    
                        <asp:Label class="TitleDivPage" ID="lblTitleBuscarProducto" runat="server" Text="Busqueda de Productos"></asp:Label>
                    </div>

                    <div class="PopupGrandeCuerpoDiv">
                        <div>
                            <table class="TablaFormulario">
                                <tr>
                                    <td class="Nombre">Clave PreOrden</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="ClaveProductoBusqueda" MaxLength="20" runat="server" Text=""></asp:TextBox>
                                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Fecha Inicio</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="FechaFiltroInicioBox" MaxLength="11" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Fecha Fin</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="FechaFiltroFinBox" MaxLength="11" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                            </table>
                        </div>

                        <div class="DivTabla">
                            <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                                CssClass="TablaInformacion" DataKeyNames="Clave" ID="TablaPreOrdenBusqueda" OnRowCommand="TablaPreOrdenBusqueda_RowCommand"
                                 runat="server" PageSize="10">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th style="width: 30px;">Clave PreOrden</th>
                                            <th  style="width: 60px;">Nombre Empleado</th>
                                            <th  style="width: 60px;">Estatus</th>
                                            <th  style="width: 60px;">Fecha</th>
                                        </tr>
                                        <tr>
                                        <td colspan="5" style="text-align: Center;">No se encontró información con los parámetros seleccionados</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="Encabezado" />
                                <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Clave">
                                        <ItemTemplate>
                                        <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaClave" runat="server" Text='<%#Eval("Clave")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NombreEmpleado" HeaderText="Nombre Empleado" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaPreOrden" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>                    
                    </div>

                    <div class="PopupGrandePieDiv">
                         <asp:Label Font-Bold="true" CssClass="TextoError" ID="AceptarMensajeProducto" runat="server" Text="" ></asp:Label><br />
                         &nbsp;&nbsp;
                         <asp:ImageButton ID="BotonPeOrdenBusqueda" OnClick="BotonPreOrdenBusqueda_Click" runat="server" ImageUrl="~/Imagen/Boton/BotonBuscar.png" />
                         &nbsp;
                         <asp:ImageButton ID="BotonPreOrdenCerrar" OnClick="BotonCerrarPreOrdenBusqueda_Click" runat="server" ImageUrl="~/Imagen/Boton/BotonCancelar.png" />                   
                    </div>
                </asp:Panel> 




                <asp:HiddenField ID="OrdenIdHidden" runat="server" Value="" />
                <asp:HiddenField ID="JefeIdHidden" runat="server" Value="0" />
                <asp:HiddenField ID="MensajeConfirmacion" runat="server" Value=""/>
                 <asp:HiddenField ID="MensajeRangoDeFechasInvalido" runat="server" Value=""/>
                <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="../../../../Imagen/Icono/IconoCargando.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
