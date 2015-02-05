<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Recepcion.aspx.cs" Inherits="Activos.Almacen.Aplicacion.Almacen.Recepcion" Title="" %>
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
            SetNewCalendar("#<%= FechaDocumentoNuevo.ClientID %>","#<%= FechaOrdenCompraNuevo.ClientID %>");
            SetNewCalendar("#<%=FechaFiltroInicioOrdenBox.ClientID %>");
            SetNewCalendar("#<%=FechaFiltroFinOrdenBox.ClientID %>");
            $("#<%= CantidadNuevo.ClientID %>").SoloNumeros();
            $("#<%= MontoDatosNuevo.ClientID %>").NumerosDecimales();
            $("#<%= PrecionUnitarioNuevo.ClientID %>").NumerosDecimales();
            $("#<%= MontoDocumentoNuevo.ClientID %>").NumerosDecimales();
            $("#<%= FechaFiltroInicioOrdenBox.ClientID %>").VerificarFechas("#<%= FechaFiltroInicioOrdenBox.ClientID %>","#<%= FechaFiltroFinOrdenBox.ClientID %>","<%= MensajeRangoDeFechasInvalido.Value %>");
            $("#<%= FechaFiltroFinOrdenBox.ClientID %>").VerificarFechas("#<%= FechaFiltroInicioOrdenBox.ClientID %>","#<%= FechaFiltroFinOrdenBox.ClientID %>","<%= MensajeRangoDeFechasInvalido.Value %>");        
       
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
                               Recepción de Articulos
                            </td>
                            <td class="Search"></td>
                            <td class="Icon"></td> 
                        </tr>
                    </table>
                </div>
             
              <div class="SubTituloDiv">Datos Generales</div>
             
             
              <asp:Panel CssClass="NewRowDiv" ID="PanelNuevoRegistroDatosGenerales" Visible="true" runat="server">
              <table class="TablaFormulario">
                    <tr>
                        <td class="Nombre">Orden de Compra</td>
                        <td class="Espacio"></td>
                        <td class="Campo">                        
                           <asp:TextBox ID="OrderCompraNuevo" CssClass="CajaTextoMediana" Enabled="false" MaxLength="15"  OnTextChanged ="LinkBuscarOrdenCompra_SelectedTextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="ImagenBuscarOrden" OnClick="OrdenCompraNuevo_Click" 
                                ImageUrl="/Imagen/Icono/ImagenBuscar.gif" runat="server" />
                         </td>
                    </tr>
                     <tr>
                        <td class="Nombre">Proveedor</td>
                        <td class="Requerido">*</td>
                        <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="ProveedorIdNuevo" runat="server"></asp:DropDownList>                              
                        </td>
                     </tr>
                        
                      <tr>
                        <td class="Nombre">Tipo de Documento</td>
                        <td class="Requerido">*</td>
                        <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="TipoDocumentoIdNuevo" runat="server"></asp:DropDownList>                              
                        </td>
                     </tr>
              
                    <tr>
                        <td class="Nombre">Folio</td>
                        <td class="Requerido">*</td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="FolioNuevo"  runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    
                     <tr>
                        <td class="Nombre">Fecha Documento</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="FechaDocumentoNuevo" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    
                      <tr>
                        <td class="Nombre">Monto</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="MontoDatosNuevo" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    

                    
                      <tr>
                        <td class="Nombre">Fecha O.C</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="FechaOrdenCompraNuevo" Enabled ="false"  runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    
                    
                         <tr>
                            <td class="Nombre">Solicitante</td>
                            <td class="Requerido">*</td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="SolicitanteIdNuevo"  Enabled ="false" onselectedindexchanged="SolicitanteCombo_SelectedIndexChanged" AutoPostBack="true"  runat="server"></asp:DropDownList>                              
                            </td>
                        </tr>
                        
                        
                         <tr>
                            <td class="Nombre">Jefe Inmediato</td>
                            <td class="Requerido">*</td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="JefeInmediatoIdNuevo" Enabled ="false"  runat="server"></asp:DropDownList>                              
                            </td>
                        </tr>
                </table>
             </asp:Panel>
             
              <div class="SubTituloDiv">Detalle del documento</div>
             
             
              <asp:Panel CssClass="NewRowDiv" ID="PanelNuevoRegistroDetalle" Visible="true" runat="server">
              <table class="TablaFormulario">              
                     <tr>
                            <td class="Nombre">Clave del Producto</td>
                            <td class="Requerido">*</td>
                            <td class="Campo"> 
                             <asp:TextBox ID="ClaveNuevo" CssClass="CajaTextoMediana" Enabled="false" MaxLength="10" runat="server"></asp:TextBox>     
                             <asp:ImageButton ID="ImagenBuscarClaveProducto" ImageUrl="/Imagen/Icono/ImagenBuscar.gif" runat="server" onclick="ImagenProductoBusqueda_Click" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="Nombre">Familia</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="FamiliaIdNuevo" Enabled ="false"  runat="server" ></asp:TextBox></td>                               
                        </tr>
                        <tr>
                            <td class="Nombre">SubFamilia</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="SubFamiliaIdNuevo" Enabled ="false"  runat="server" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="Nombre">Marca</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="MarcaIdNuevo" Enabled ="false"  runat="server" ></asp:TextBox></td>                   
                        </tr>
                        <tr>
                            <td class="Nombre">Descripción</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="DescripcionNuevo" Enabled ="false"   runat="server" ></asp:TextBox></td>
                        </tr>        
                                
                         <tr>
                            <td class="Nombre">Precio Unitario</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="PrecionUnitarioNuevo" MaxLength="7" OnTextChanged="PrecionUnitarioNuevo_SelectedTextChanged" AutoPostBack="true"   runat="server" Text=""></asp:TextBox></td>
                           
                        </tr>
                    
						 <tr>
                            <td class="Nombre">Cantidad</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CantidadNuevo"  MaxLength = "4" OnTextChanged="CantidadNuevo_SelectedTextChanged" AutoPostBack="true"  runat="server" Text=""></asp:TextBox></td>
                           
                        </tr>
                        
                         <tr>
                            <td class="Nombre">Monto</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="MontoDocumentoNuevo" Enabled="false" MaxLength="7" runat="server" Text=""></asp:TextBox></td>                           
                        </tr>
                        
                         <tr>
                            <td colspan="3">
                                <asp:Label CssClass="TextoError" ID="AgregarEtiquetaMensaje" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:ImageButton AlternateText="Guardar" ID="BotonAgregar" ImageUrl="~/Imagen/Boton/BotonAgregar.png" OnClick="BotonAgregar_Click" runat="server"/>&nbsp;&nbsp;
                              </td>
                        </tr>
                </table>
             </asp:Panel>             
            <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
             	<div>
                    <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                        CssClass="TablaInformacion" DataKeyNames="RecepcionId, ProductoId" ID="TablaRecepcion" 
<<<<<<< HEAD
                        OnPageIndexChanging="TablaRecepcion_PageIndexChanging"
=======
                        OnPageIndexChanging="TablaRecepcion_PageIndexChanging" OnRowCommand="TablaRecepcion_RowCommand"
>>>>>>> origin/master
                        runat="server" PageSize="10">
                        <EmptyDataTemplate>
                            <table class="TablaVacia">
                                <tr class="Encabezado">
                                    <th style="width: 10px;"></th>
                                    <th style="width: 30px;">Clave</th>
                                    <th style="width: 100px;">Descripcion</th>  
                                    <th style="width: 80px;">Precio Unitario</th>                                    
                                    <th style="width: 60px;">Cantidad</th>                                   
                                </tr>
                                <tr>
                                    <td colspan="5" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                                </tr>
                            </table>
                      </EmptyDataTemplate>
                      
                        <HeaderStyle CssClass="Encabezado" />
                        <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                        <Columns>
                           <asp:BoundField DataField="Clave" HeaderText="Clave" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="PrecioUnitario" HeaderText="PrecioUnitario" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>    
                            <asp:BoundField DataField="Monto" HeaderText="Monto" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>    
                            <asp:TemplateField HeaderText="">
                                     <ItemTemplate>
                                         <asp:ImageButton ID="BotonEliminarActivo" CommandArgument="<%#Container.DataItemIndex%>" CommandName="EliminarRecepcion" runat="server" ImageUrl="/Imagen/Icono/IconoEliminarRegistro.gif" />
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" Width="25px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div>
                     <table width="100%">
                           <tr>
                              <td style="width:70%;">
                                 
                              <br />
                              <asp:ImageButton AlternateText="Guardar" ID="BotonGuardarPreOrden"  ImageUrl="/Imagen/Boton/BotonGuardar.png" OnClick="BotonGuardar_Click" runat="server" ValidationGroup="Save" />&nbsp;&nbsp;
                              <asp:ImageButton AlternateText="Limpiar" ID="BotonLimpiarRegistro"  ImageUrl="/Imagen/Boton/BotonLimpiar.png" runat="server" />&nbsp;&nbsp;
                              <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarPreOrden" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                              </td>  
                               <td style="width:20%; text-align:right;">
                                 <asp:Label ID="LabelEtiquetaTotal" CssClass="MontoTotal" Text="Total:" runat="server"></asp:Label>&nbsp;
                                 <asp:Label ID="LabelMontoTotal" CssClass="MontoTotal" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>                       
                           </tr>
                        </table> 
                     </div>
                <br /><br /><br /> 
               <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="../../../../Imagen/Icono/IconoCargando.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
                <br />
                <br />
                
                <asp:Panel CssClass="Superposicion" ID="pnlFondoBuscarProducto" runat="server" Visible="false"></asp:Panel>

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
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="ClaveProductoBusqueda" Enabled="false" MaxLength="20" runat="server" Text=""></asp:TextBox></td>
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
                                CssClass="TablaInformacion" DataKeyNames="ProductoId" ID="TablaProducto" OnPageIndexChanging="TablaProducto_PageIndexChanging"
                                OnRowCommand="TablaProducto_RowCommand" runat="server" PageSize="10">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th style="width: 30px;">Clave Producto</th>
                                            <th  style="width: 60px;">Nombre</th>
                                            <th  style="width: 60px;">Familia</th>
                                            <th  style="width: 60px;">SubFamilia</th>
                                            <th  style="width: 40px;">Marca</th>
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
                                    <asp:BoundField DataField="NombreProducto" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Familia" HeaderText="Familia" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubFamilia" HeaderText="SubFamilia" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
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
                         <asp:ImageButton ID="BotonCancelar" OnClick="BotonCerrarProductoBusqueda_Click" runat="server" ImageUrl="~/Imagen/Boton/BotonCancelar.png" />                   
                    </div>
                </asp:Panel>
                
                
                <%--POPUP Busqueda por Orden--%>
                <asp:Panel CssClass="Superposicion" ID="pnlFondoBuscarOrden" runat="server" Visible="false"></asp:Panel>

                <asp:Panel CssClass="PopupGrandeDiv" ID="PanelBusquedaOrden" Visible="false" runat="server">
                    <div class="PopupGrandeEncabezadoDiv">                    
                        <asp:Label class="TitleDivPage" ID="Label1" runat="server" Text="Busqueda de Productos"></asp:Label>
                    </div>

                    <div class="PopupGrandeCuerpoDiv">
                        <div>
                            <table class="TablaFormulario">
                                <tr>
                                    <td class="Nombre">Clave Orden</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoPequenia" ID="OrdenBusquedaBox" MaxLength="20" runat="server" Text=""></asp:TextBox>
                                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Fecha Inicio</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="FechaFiltroInicioOrdenBox" MaxLength="11" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Fecha Fin</td>
                                    <td class="Espacio"></td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="FechaFiltroFinOrdenBox" MaxLength="11" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                            </table>
                        </div>

                        <div class="DivTabla">
                            <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                                CssClass="TablaInformacion" DataKeyNames="Clave" ID="TablaOrdenBusqueda" OnRowCommand="TablaOrdenBusqueda_RowCommand" OnPageIndexChanging="TablaOrdenBusqueda_PageIndexChanging"
                                 runat="server" PageSize="10">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th style="width: 30px;">Clave Orden</th>
                                            <th  style="width: 60px;">Nombre Empleado</th>
                                            <th  style="width: 60px;">Estatus</th>
                                            <th  style="width: 60px;">Proveedor</th>
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
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NombreEmpleado" HeaderText="Nombre Empleado" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaOrden" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>                    
                    </div>

                    <div class="PopupGrandePieDiv">
                         <asp:Label Font-Bold="true" CssClass="TextoError" ID="Label2" runat="server" Text="" ></asp:Label><br />
                         &nbsp;&nbsp;
                         <asp:ImageButton ID="BotonOrdenBusqueda" OnClick="BotonOrdenBusqueda_Click" runat="server" ImageUrl="~/Imagen/Boton/BotonBuscar.png" />
                         &nbsp;
                         <asp:ImageButton ID="BotonOrdenCerrar" OnClick="BotonCerrarOrdenBusqueda_Click" runat="server" ImageUrl="~/Imagen/Boton/BotonCancelar.png" />                   
                    </div>
                </asp:Panel> 

                
                    <asp:HiddenField ID= "MensajeRangoDeFechasInvalido" runat="server" Value=""/>
                     <asp:HiddenField ID="TemporalRecepcionIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="ProductoIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="OrdenIdHidden" runat="server" Value="" />
             </ContentTemplate>
         </asp:UpdatePanel>


    </div>

</asp:Content>
