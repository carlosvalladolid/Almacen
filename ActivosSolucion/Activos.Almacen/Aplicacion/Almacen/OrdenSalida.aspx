<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="OrdenSalida.aspx.cs" Inherits="Almacen.Web.Aplicacion.Almacen.OrdenSalida" Title="" %>

<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaPrivada.Master" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    <link href="/Incluir/Estilo/Privado/jquery-ui-1.8.16.custom.css" rel="Stylesheet" type="text/css" />
    <link href="/Incluir/Estilo/Privado/demos.css" rel="Stylesheet" type="text/css" />

    <script src="/Incluir/Javascript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="/Incluir/Javascript/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <script src="/Incluir/Javascript/Calendar.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad(sender, args) {
            SetNewCalendar("#<%= FechaInicioBusquedaBox.ClientID %>");
            SetNewCalendar("#<%= FechaFinBusquedaBox.ClientID %>");
            $("#<%= CantidadBox.ClientID %>").SoloNumeros();
            $("#<%= BotonGuardar.ClientID %>").Confirmar("<%= MensajeConfirmacion.Value%>");
        }
    </script>

    <script language="javascript" src="/Incluir/Javascript/ValidarFormulario.js" type="text/javascript"></script>
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
                            <asp:TextBox CssClass="CajaTextoPequenia" Enabled ="false" ID="RequisicionBox" MaxLength="10" runat="server" Text="">
                            </asp:TextBox><asp:ImageButton ID="ImagenBuscarRequisicion" ImageUrl="/Imagen/Icono/ImagenBuscar.gif" runat="server" onclick="ImagenBuscarRequisicion_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Nombre">Solicitante</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" Enabled ="false" ID="SolicitanteBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Dependencia</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" Enabled ="false" ID="DependenciaBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Dirección</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" Enabled ="false" ID="DireccionBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Puesto</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" Enabled ="false" ID="PuestoBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Jefe inmediato</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" Enabled ="false" ID="JefeBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                </table>

                <br />
                <div class="SubTituloDiv">Artículos</div>

                <table class="TablaFormulario">
                    <tr>
                        <td class="Nombre">Clave</td>
                        <td class="Espacio"></td>
                        <td class="Campo">
                            <asp:TextBox CssClass="CajaTextoPequenia" Enabled ="false" ID="ClaveProductoBox" MaxLength="10" runat="server" Text="">
                            </asp:TextBox><asp:ImageButton ID="ImagenBuscarProducto" ImageUrl="/Imagen/Icono/ImagenBuscar.gif" onclick="ImagenProductoBusqueda_Click" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Nombre">Familia</td>
                        <td class="Espacio"></td>
                          <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" Enabled ="false" ID="FamiliaBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Sub familia</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" Enabled ="false" ID="SubFamiliaBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>                 </tr>
                    <tr>
                        <td class="Nombre">Marca</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" Enabled ="false" ID="MarcaBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                   </tr>
                    <tr>
                        <td class="Nombre">Descripción</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" Enabled ="false" ID="DescripcionBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Cantidad</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="CantidadBox" MaxLength="10" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                            <asp:ImageButton AlternateText="Guardar" ID="ImageButton2" ImageUrl="~/Imagen/Boton/BotonAgregar.png" OnClick="BotonAgregar_Click" runat="server" ValidationGroup="Save" />
                        </td>
                    </tr>
                </table>
                
                
                
			    <div>
                    <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                        CssClass="TablaInformacion" DataKeyNames="ProductoId" ID="TablaOrden" OnRowCommand="TablaOrden_RowCommand" OnPageIndexChanging="TablaOrden_PageIndexChanging" runat="server" 
                        PageSize="10">
                        <EmptyDataTemplate>
                            <table class="TablaVacia">
                                <tr class="Encabezado">
                                    <th style="width: 10px;"></th>
                                    <th style="width: 30px;">Clave</th>
                                    <th style="width: 100px;">Nombre</th>  
                                    <th style="width: 80px;">Familia</th>                                    
                                    <th style="width: 60px;">SubFamilia</th>   
                                    <th style="width: 60px;">Marca</th>  
                                    <th style="width: 20px;">Cantidad</th>  
                                </tr>
                                <tr>
                                    <td colspan="7" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                                </tr>
                            </table>
                      </EmptyDataTemplate>
                        <HeaderStyle CssClass="Encabezado" />
                        <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                        <Columns>
                           <asp:BoundField DataField="ClaveProducto" HeaderText="Clave" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="15px" />
                            </asp:BoundField>                                                      
                            
                            <asp:BoundField DataField="DescripcionProducto" HeaderText="Producto" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center"/>
                            </asp:BoundField>    
                            
                              <asp:BoundField DataField="FamiliaNombre" HeaderText="Familia" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center"/>
                            </asp:BoundField>                  
                          
                            <asp:BoundField DataField="SubFamiliaNombre" HeaderText="SubFamilia" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="MarcaNombre" HeaderText="Marca" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center"  />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center"  />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="">
                                     <ItemTemplate>
                                         <asp:ImageButton ID="BotonEliminarActivo" CommandArgument='<%#Eval("ProductoId")%>'  CommandName="EliminarPreOrden" runat="server" ImageUrl="/Imagen/Icono/IconoEliminarRegistro.gif" />
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" Width="25px" />
                            </asp:TemplateField>
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

                <!-- Búsqueda de requisiciones -->
                <asp:Panel CssClass="Superposicion" ID="FondoBusquedaRequisicion" runat="server" Visible="false"></asp:Panel>

                <asp:Panel CssClass="PopupGrandeDiv" ID="PanelBusquedaRequisicion" Visible="false" runat="server">
                    <div class="PopupGrandeEncabezadoDiv">                    
                        <asp:Label class="TitleDivPage" ID="Label1" runat="server" Text="Busqueda de requisiciones"></asp:Label>
                    </div>

                    <div class="PopupGrandeCuerpoDiv">
                        <div>
                            <table class="TablaFormulario">
                                <tr>
                                    <td class="Nombre">Clave requisición</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="RequisicionBusquedaBox" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Empleado</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="EmpleadoBusquedaBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Fecha inicio</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="FechaInicioBusquedaBox" MaxLength="11" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Fecha fin</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="FechaFinBusquedaBox" MaxLength="11" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Estatus</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboMediano" ID="EstatusBusquedaCombo" runat="server"></asp:DropDownList>                              
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="DivTabla">
                            <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                                CssClass="TablaInformacion" DataKeyNames="Clave,RequisicionId" ID="TablaRequisicionBusqueda" OnPageIndexChanging = "TablaRequisicionBusqueda_PageIndexChanging" 
                                OnRowCommand="TablaRequisicionBusqueda_RowCommand" runat="server" PageSize="10">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th style="width: 75px;">Clave</th>
                                            <th>Nombre Empleado</th>
                                            <th style="width: 125px;">Estatus</th>
                                            <th style="width: 100px;">Fecha</th>
                                        </tr>
                                        <tr>
                                        <td colspan="5" style="text-align: Center;">No se encontró información con los parámetros seleccionados</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="Encabezado" />
                                <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Requisición">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandArgument='<%#Eval("RequisicionId")%>' CommandName="Select" ID="LigaClave" runat="server" Text='<%#Eval("Clave")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NombreEmpleado" HeaderText="Nombre Empleado" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NombreEstatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="125px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaInserto" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>                    
                    </div>

                    <div class="PopupGrandePieDiv">
                         <asp:Label Font-Bold="true" CssClass="TextoError" ID="Label2" runat="server" Text="" ></asp:Label><br />
                         &nbsp;&nbsp;
                         <asp:ImageButton ID="BotonRequisicionBusqueda" ImageUrl="~/Imagen/Boton/BotonBuscar.png" OnClick="BotonRequisicionBusqueda_Click" runat="server" />
                         &nbsp;
                         <asp:ImageButton ID="BotonRequisicionCerrar" ImageUrl="~/Imagen/Boton/BotonCancelar.png" OnClick="BotonRequisicionCerrar_Click" runat="server" />                   
                    </div>
                </asp:Panel>
                <!-- Termina búsqueda de requisiciones -->

                <!-- Búsqueda de productos -->
                <asp:Panel CssClass="Superposicion" ID="FondoBuscarProducto" runat="server" Visible="false"></asp:Panel>

                <asp:Panel CssClass="PopupGrandeDiv" ID="PanelBusquedaProducto" Visible="false" runat="server">
                    <div class="PopupGrandeEncabezadoDiv">                    
                        <asp:Label class="TitleDivPage" ID="lblTitleBuscarProducto" runat="server" Text="Busqueda de Productos"></asp:Label>
                    </div>

                    <div class="PopupGrandeCuerpoDiv">
                        <div>
                            <table class="TablaFormulario">
                                <tr>
                                    <td class="Nombre">Clave Producto</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="ClaveProductoBusqueda" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Nombre</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreProductoBusqueda" MaxLength="100" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                            </table>
                        </div>

                        <div class="DivTabla">
                            <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                                CssClass="TablaInformacion" DataKeyNames="ProductoId" ID="TablaProducto" OnPageIndexChanging = "TablaProducto_PageIndexChanging" 
                                OnRowCommand="TablaProducto_RowCommand" runat="server" PageSize="10">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th style="width: 75px;">Clave</th>
                                            <th>Nombre</th>
                                            <th style="width: 125px;">Familia</th>
                                            <th style="width: 125px;">SubFamilia</th>
                                            <th style="width: 125px;">Marca</th>
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
                                            <asp:LinkButton CommandArgument='<%#Eval("ProductoId")%>' CommandName="Select" ID="LigaClave" runat="server" Text='<%#Eval("Clave")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NombreProducto" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Familia" HeaderText="Familia" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="125px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubFamilia" HeaderText="SubFamilia" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="125px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="125px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>                    
                    </div>

                    <div class="PopupGrandePieDiv">
                        <asp:Label Font-Bold="true" CssClass="TextoError" ID="AceptarMensajeProducto" runat="server" Text="" ></asp:Label><br />
                         &nbsp;&nbsp;
                         <asp:ImageButton ID="BotonAceptar" OnClick="BotonProductoBusqueda_Click" runat="server" ImageUrl="~/Imagen/Boton/BotonBuscar.png" />
                         &nbsp;
                         <asp:ImageButton ID="ImageButton1" OnClick="BotonCerrarProductoBusqueda_Click" runat="server" ImageUrl="~/Imagen/Boton/BotonCancelar.png" />                   
                    </div>
                </asp:Panel>
                <!-- Termina búsqueda de productos -->

                <asp:HiddenField ID="RequisicionIdHidden" runat="server" Value="" />
                <asp:HiddenField ID="OrdenSalidaIdHidden" runat="server" Value="" />
                <asp:HiddenField ID="ProductoIdHidden" runat="server" Value="" />
                
                <asp:HiddenField ID="MensajeConfirmacion" runat="server" Value="" />
                <asp:HiddenField ID="MensajeLimpieza" runat="server" Value="" />
                
                <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
