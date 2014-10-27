<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="EntradasSalidas.aspx.cs" Inherits="Activos.Web.Aplicacion.Activo.EntradasSalidas" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarEmpleado" Src="~/Incluir/ControlesWeb/ControlBuscarEmpleado.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarAccesorioPadre" Src="~/Incluir/ControlesWeb/ControlBuscarAccesorioPadre.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarAccesorioHijo" Src="~/Incluir/ControlesWeb/ControlBuscarAccesorioHijo.ascx" %>


<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    <link href="/Incluir/Estilo/Privado/jquery-ui-1.8.16.custom.css" rel="Stylesheet" type="text/css" />
   <link href="/Incluir/Estilo/Privado/demos.css" rel="Stylesheet" type="text/css" />

   <script src="/Incluir/Javascript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
   <script src="/Incluir/Javascript/jquery.ui.datepicker-es.js" type="text/javascript"></script>
   <script src="/Incluir/Javascript/Calendar.js" type="text/javascript"></script>
   
   <script language="javascript" type="text/javascript">

       function pageLoad(sender, args)  
      {
          SetNewCalendar("#<%=FechaMovimiento.ClientID%>");
      }

      function ImprimirEntradasSalidas(EmpleadoAutorizo, TipoActivo, VehiculoPadre, ProveedorId, TipoServicioId) {
          window.open("/Ventana/ImprimirEntradasSalidas.aspx?Emp=" + EmpleadoAutorizo + "&TipoActivo=" + TipoActivo + "&VehiculoPadre=" +VehiculoPadre + "&ProveedorId="+ ProveedorId+"&TipoServicioId="+TipoServicioId , "ImprimirEntradasSalidas", "");

      }
   </script>
</asp:Content>



<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" SeccionMenu="ActivoFijo" runat="server" />
    </div>
    
    <div class="DivContenido">
        <div class="DivContenidoTitulo">
            <div class="DivTitulo">Entradas y salidas de activos</div>
        </div>
        
        <div class="DivInformacionContenido">
            <asp:UpdatePanel ID="ActualizarTablaEntradaSalida" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <wuc:BuscarEmpleado ID="ControlBuscarEmpleado" runat="server" />
                    <wuc:BuscarAccesorioPadre ID="ControlBuscarAccesorioPadre" runat="server" />
                    <wuc:BuscarAccesorioHijo ID="ControlBuscarAccesorioHijo" runat="server" />
                    <asp:Panel ID="PanelTablaEntradaSalidaActivo" runat="server">
                        <br />
                        <div class="DivSubtituloPagina">
                           Datos generales
                        </div>
                        
                        <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                           <table class="TablaFormulario">
                           <!--<tr>
                                 <td class="Nombre"></td>
                                 <td class="Espacio">&nbsp;</td>
                                 <asp:Panel ID="PanelAutorizacion" runat="server" DefaultButton="LinkBuscarSolicitante" Visible="false">
                                  <td>
                                    <asp:TextBox ID="EmpleadoAutorizo" OnTextChanged="NumeroEmpleado_TextChanged" Enabled="false" AutoPostBack="true" CssClass="CajaTextoDeshabilitadaGrande"  MaxLength="20" runat="server" Text=""></asp:TextBox>
                                  </td>
                                  <td>
                                        <asp:ImageButton ImageUrl="/Imagen/Icono/ImagenBuscar.gif" ID="BotonBuscarSolicitante" OnClick="BotonBuscarSolicitante_Click" runat="server" />
                                        <asp:LinkButton ID="LinkBuscarSolicitante" OnClick="LinkBuscarSolicitante_Click" ValidationGroup="BuscarCodigoBarras" runat="server" Text="" Width="0px"></asp:LinkButton>
                                </asp:Panel>
                                 
                             </tr>-->
                            
                            <tr>
                                 <td class="Nombre">Tipo de movimiento</td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo">
                                     
                                     
                                      <asp:RadioButtonList runat="server" ID="TipoMovimiento" Enabled="true" ValidationGroup="AgregarActivo" AutoPostBack="true" OnSelectedIndexChanged="MostrarCamposProveedor">
                                        
                                        <asp:ListItem Value="1" Text="Entrada" ></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Salida" Selected="True"></asp:ListItem>
                             
                                        </asp:RadioButtonList>
                                     
                                     
                                 </td>
                             </tr>
                              <tr id="RenglonTipoServicio" runat ="server" visible="true">

                                 <td class="Nombre"><asp:Label ID="SeleccionarServicioEtiqueta" Text="Tipo de Servicio" runat="server"></asp:Label></td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo"><asp:DropDownList ID="TipoServicio" CssClass="ComboGrande" runat="server"></asp:DropDownList></td>
                             </tr>
                              <tr id="RenglonProveedor" runat ="server" visible="true">

                                 <td class="Nombre"><asp:Label ID="SeleccionarProveedorEtiqueta" Text="Proveedor" runat="server"></asp:Label></td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo"><asp:DropDownList ID="ProveedorId" CssClass="ComboGrande" runat="server"></asp:DropDownList></td>
                             </tr>
                            <tr>
                                <td class="Nombre">Código Barras</td>
                                <td class="Espacio"></td>
                                <td>
                                    <asp:Panel ID="PanelBuscarCodigoBarras" runat="server" DefaultButton="LinkBuscarActivo">
                                        <asp:TextBox CssClass="CajaTextoDeshabilitadaMediana" Enabled="true" ID="CodigoBarrasParticular" MaxLength="20" runat="server" Text=""></asp:TextBox>
                                        <asp:ImageButton Enabled="true" ID="BotonBuscarCodigoBarra" ValidationGroup="BotonCodigoBarras" ImageUrl="/Imagen/Icono/IconoCodigoBarras.jpg" OnClick="BotonBuscarCodigoBarra_Click" runat="server" />&nbsp;&nbsp;
                                        <asp:LinkButton ID="LinkBuscarActivo" OnClick="BotonBuscarCodigoBarra_Click" ValidationGroup="BuscarCodigoBarras" runat="server" Text="" Width="0px"></asp:LinkButton>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td class="Nombre">No.Empleado</td>
                                <td class="Requerido"></td>
                                <td class="Campo">
                                 <asp:Panel ID="Panel1" runat="server" DefaultButton="LinkBuscarEmpleado">
                                     <asp:TextBox ID="NumeroEmpleado" CssClass="CajaTextoMediana" Enabled="false" MaxLength="20" runat="server" Text=""></asp:TextBox>
                                    <asp:LinkButton ID="LinkBuscarEmpleado" ValidationGroup="BuscarCodigoBarras" runat="server" Text="" Width="0px"></asp:LinkButton>
                                 </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td class="Nombre">Nombre de Empleado</td>
                                <td class="Espacio">&nbsp;</td>
                                <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="NombreEmpleado" Enabled="false" MaxLength="200" runat="server" Text=""></asp:TextBox></td>
                            </tr>
                             <tr>
                              <td class="Nombre">Fecha de movimiento</td>
                              <td class="Espacio">&nbsp;</td>
                              <td class="Campo">
                                 <asp:TextBox CssClass="CajaTextoPequenia" ID="FechaMovimiento" MaxLength="50" runat="server" Text=""></asp:TextBox>
                                 <span class="NotaCampo">(dd/mm/aaaa)</span>
                              </td>
                              <td class="EspacioColumna"></td>
                            </tr>
                             
                             <tr>
                                <td class="Nombre">Observaciones</td>
                                <td class="Espacio">&nbsp;</td>
                                <td class="Campo"><asp:TextBox TextMode="MultiLine" Enabled="true" CssClass="CajaTextoGrande" ID="Observaciones" runat="server" Rows="5"></asp:TextBox></td>
                             </tr>
                             <tr>
                                 <td class="Nombre">Condiciones</td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="CondicionId" runat="server" ></asp:DropDownList></td>
                             </tr>
                             
                            
                             <tr>
                                 <td colspan="3">
                                    
                                    <asp:CustomValidator CssClass="TextoError" ControlToValidate="FechaMovimiento" EnableClientScript="false" ErrorMessage="" ID="FechaMovimientoRequerido" OnServerValidate="FechaMovimiento_Validate" runat="server" SetFocusOnError="true" ValidationGroup="AgregarActivo" ValidateEmptyText="true"></asp:CustomValidator>
                                    <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CodigoBarrasParticular" Display="Dynamic" ErrorMessage="" ID="CodigoBarrasObligatorio" SetFocusOnError="true" ValidationGroup="BotonCodigoBarras" runat="server"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="TipoMovimiento" Display="Dynamic" ErrorMessage="" ID="TipoMovimientoRequerido" SetFocusOnError="true" ValidationGroup="AgregarActivo" runat="server"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="Observaciones" Display="Dynamic" ErrorMessage="" ID="ObservacionesRequerido" SetFocusOnError="true" ValidationGroup="AgregarActivo" runat="server"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator CssClass="TextoError" ControlToValidate="CondicionId" Display="Dynamic" ErrorMessage="" ID="CondicionIdRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="AgregarActivo" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CodigoBarrasParticular" Display="Dynamic" ErrorMessage="" ID="CodigoBarrasRequerido" SetFocusOnError="true" ValidationGroup="AgregarActivo" runat="server"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator CssClass="TextoError" ControlToValidate="TipoServicio" Display="Dynamic" ErrorMessage="" ID="TipoServicioRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="GuardarMovimientos" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                    <asp:CompareValidator CssClass="TextoError" ControlToValidate="ProveedorId" Display="Dynamic" ErrorMessage="" ID="ProveedorRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="GuardarMovimientos" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                    <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
                                    <br />
                                 </td> 
                             </tr>
                             <tr>
                                <td colspan="3">
                                    <asp:ImageButton AlternateText="Agregar" ID="BotonAgregarActivo" OnClick="BotonAgregarActivo_Click" ValidationGroup="AgregarActivo" ImageUrl="/Imagen/Boton/BotonAgregar.png" runat="server" />&nbsp;&nbsp;
                                     <asp:ImageButton AlternateText="Guardar" ID="BotonGuardarActivo" OnClick="BotonDarEntradaSalida_Click" ImageUrl="/Imagen/Boton/BotonGuardar.png" runat="server" ValidationGroup="GuardarMovimientos" />&nbsp;&nbsp;
                                    <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimir" Enabled="false" OnClick="BotonImprimir_Click" ValidationGroup="" ImageUrl="/Imagen/Boton/BotonImprimir.png" runat="server" />&nbsp;&nbsp;
                                    <asp:ImageButton AlternateText="Limpiar Campos" ID="BotonLimpiar" ImageUrl="/Imagen/Boton/BotonLimpiar.png" OnClick="BotonLimpiar_Click" runat="server" ValidationGroup="" />&nbsp;&nbsp;
                                    <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelar" ImageUrl="/Imagen/Boton/BotonCancelar.png" OnClick="BotonCancelar_Click" runat="server" ValidationGroup="" />&nbsp;&nbsp;
                                </td>
                             </tr>       
                           </table> 
                        </asp:Panel>
                        
                        <div class="DivSubtituloPagina">
                           Activos de: 
                            <asp:Label ID="EmpleadoNombreBaja" runat="server" Text="" Font-Bold="true" Font-Underline="true" Font-Size="Larger"></asp:Label>
                        </div>
                        <br />
                        <div class="DivTabla">
                            <asp:GridView AllowPaging="false" AllowSorting="false" 
                                AutoGenerateColumns="false" BorderWidth="0" OnRowDataBound="TablaActivo_RowDataBound"
                                CssClass="TablaInformacion" DataKeyNames="MovimientoId" ID="TablaActivo" 
                                OnRowCommand="TablaActivo_RowCommand" runat="server">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th>Descripción</th>
                                            <th style="width: 150px;">Tipo de Movimiento</th>
                                            <th style="width: 150px;">Número de serie</th>
                                            <th style="width: 150px;">Modelo</th>
                                            <th style="width: 100px;">Condiciones</th>
                                            <th style="width: 150px;">Código de barras</th>
                                            <th style="width: 25px;"></th>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="text-align: center;">Favor de agregar los activos</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="Encabezado" />
                                <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaDescripcion" runat="server" Text='<%#Eval("Descripcion")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TipoMovimientoNombre" HeaderText="Tipo de Movimiento" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NumeroSerie" HeaderText="Número de serie" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CondiocionNombre" HeaderText="Condiciones" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="Código de barras" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="BotonEliminarActivo" CommandArgument="<%#Container.DataItemIndex%>" CommandName="EliminarActivo" runat="server" ImageUrl="/Imagen/Icono/IconoEliminarRegistro.gif" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="25px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <table> 
                            <tr>
                                 <td>
                                    <asp:Label CssClass="TextoError" ID="Label1" runat="server" Text=""></asp:Label>
                                 </td> 
                             </tr>
                        </table>
                        
                    </asp:Panel>
                    
                    <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaEntradaSalida" ID="ProgresoTablaEntradaSalida" runat="server">
                        <ProgressTemplate>
                            <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                        </ProgressTemplate>
                   </asp:UpdateProgress>
                    
                    <asp:HiddenField ID="EmpleadoIdHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="ActivoIdHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="EmpleadoAutorizoIdHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="NumeroEmpleadoHiddden" runat="server" Value="" />
                    <asp:HiddenField ID="TemporalMovimientoIdHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="ActivoPadreHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="ActivoVehiculoHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="VehiculoPadreHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="ProveedorHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="TipoServicioIdHidden" runat="server" Value="0" />
                    
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
