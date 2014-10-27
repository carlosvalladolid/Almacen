<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="AsignacionActivo.aspx.cs" Inherits="Activos.Web.Aplicacion.Activo.AsignacionActivo" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">

   <script language="javascript" type="text/javascript">

      function ImprimirDocumento(EmpleadoId, TemporalAsignacionId) {
         window.open("/Ventana/ImprimirAsignacionActivo.aspx?EmpleadoId=" + EmpleadoId + "&TemporalAsignacionId=" + TemporalAsignacionId, "ImprimirDocumento", " resizable=yes,scrollbars=1");

      }

      function ImprimirDocumentoVehiculo(EmpleadoId, ActivoId) {
         window.open("/Ventana/ImprimirResguardoVehiculo.aspx?EmpleadoId=" + EmpleadoId + "&ActivoId=" + ActivoId, "ImprimirDocumento", " resizable=yes,scrollbars=1");

      }

      function ImprimirDocumentoVehiculoReverso() {
         window.open("/Ventana/ImprimirResguardoVehiculoReverso.aspx", "ImprimirDocumento", " resizable=yes,scrollbars=1");

      }
       
   </script>

</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" SeccionMenu="ActivoFijo" runat="server" />
    </div>
    
    <div class="DivContenido">
        <div class="DivContenidoTitulo">
            <div class="DivTitulo">Asignación de activo</div>
        </div>
        
        <div class="DivInformacionContenido">
            <asp:UpdatePanel ID="ActualizarTablaAsignacion" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <asp:Panel ID="PanelTablaAsignacionActivo" runat="server">
                        <br />
                        <div class="DivSubtituloPagina">
                           Datos generales
                        </div>
                        
                        <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                           <table class="TablaFormulario">
                             <tr>
                                 <td class="Nombre">Empleado</td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="EmpleadoId" OnSelectedIndexChanged="EmpleadoId_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:DropDownList></td>
                             </tr>
                             <tr>
                                 <td class="Nombre">Código de Barras</td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo">
                                    <asp:Panel ID="PanelBuscarCodigoBarras" runat="server" DefaultButton="LinkBuscarActivo">
                                       <asp:TextBox ID="CodigoBarrasParticular" CssClass="CajaTextoMediana" MaxLength="15" runat="server"></asp:TextBox>
                                       <asp:LinkButton ID="LinkBuscarActivo" OnClick="LinkBuscarActivo_Click" ValidationGroup="BuscarCodigoBarras" runat="server" Text="" Width="0px"></asp:LinkButton>
                                    </asp:Panel>
                                 </td>
                             </tr>
                             <tr>
                                 <td class="Nombre">Descripción</td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" Enabled="false" ID="DescripcionActivo" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                             </tr>
                             <tr>
                                 <td class="Nombre">Número de serie</td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" Enabled="false" ID="NumeroSerie" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                             </tr>
                             <tr>
                                 <td class="Nombre">Modelo</td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" Enabled="false" ID="Modelo" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                             </tr>
                             <tr>
                                 <td class="Nombre">Color</td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" Enabled="false" ID="Color" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                             </tr>
                             <tr>
                                 <td class="Nombre">Monto</td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" Enabled="false" ID="Monto" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                             </tr>
                             <tr>
                                 <td class="Nombre">Condición</td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="CondicionId" runat="server" ></asp:DropDownList></td>
                             </tr>
                             <tr>
                                 <td class="Nombre"></td>
                                 <td class="Espacio">&nbsp;</td>
                                 <td class="Campo">
                                    <asp:CheckBox ID="UbicacionActivoBodega" Checked="false" runat="server" Text=" Bodega" />
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="3">
                                    <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CodigoBarrasParticular" Display="Dynamic" ErrorMessage="" ID="CodigoBarrasRequerido" SetFocusOnError="true" ValidationGroup="AgregarActivo" runat="server"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CodigoBarrasParticular" Display="Dynamic" ErrorMessage="" ID="BuscarCodigoBarrasRequerido" SetFocusOnError="true" ValidationGroup="BuscarCodigoBarras" runat="server"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator CssClass="TextoError" ControlToValidate="CondicionId" Display="Dynamic" ErrorMessage="" ID="CondicionRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="AgregarActivo" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                    <asp:Label CssClass="TextoError" ID="AgregarEtiquetaMensaje" runat="server" Text=""></asp:Label>
                                    <br />
                                    <asp:ImageButton AlternateText="Agregar" ID="BotonAgregarActivo" OnClick="BotonAgregarActivo_Click" ValidationGroup="AgregarActivo" ImageUrl="/Imagen/Boton/BotonAgregar.png" runat="server" />
                                    <asp:ImageButton AlternateText="Actualizar" ID="BotonActualizarActivo" OnClick="BotonActualizarActivo_Click" ValidationGroup="AgregarActivo" ImageUrl="/Imagen/Boton/BotonActualizar.png" Visible="false" runat="server" />&nbsp;&nbsp;
                                    <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarActualizar" OnClick="BotonCancelarActualizar_Click" ImageUrl="/Imagen/Boton/BotonCancelar.png" Visible="false" runat="server" />
                                 </td> 
                             </tr>
                           </table> 
                        </asp:Panel>
                        
                        <div class="DivSubtituloPagina">
                           Activos asignados
                        </div>
                        <br />
                        <div class="DivTabla">
                            <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnRowDataBound="TablaActivo_RowDataBound"
                                CssClass="TablaInformacion" DataKeyNames="TemporalAsignacionDetalleId, ActivoId" ID="TablaActivo" OnRowCommand="TablaActivo_RowCommand" runat="server">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th>Descripción</th>
                                            <th style="width: 150px;">Número de serie</th>
                                            <th style="width: 150px;">Modelo</th>
                                            <th style="width: 100px;">Color</th>
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
                                    <asp:BoundField DataField="NumeroSerie" HeaderText="Número de serie" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Color" HeaderText="Color" ItemStyle-HorizontalAlign="Left">
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
                        <div>
                           <table width="100%">
                              <tr>
                                 <td>
                                    <asp:CompareValidator CssClass="TextoError" ControlToValidate="EmpleadoId" Display="Dynamic" ErrorMessage="" ID="EmpleadoRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                    <asp:Label CssClass="TextoError" ID="EtiquetaMensajeError" runat="server" Text=""></asp:Label>
                                    <asp:Label CssClass="TextoInformacion" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
                                    <br />
                                    <asp:ImageButton AlternateText="Guardar" ID="BotonGuardar" OnClick="BotonGuardar_Click" ImageUrl="/Imagen/Boton/BotonGuardar.png" ValidationGroup="Guardar" runat="server" />&nbsp;&nbsp;
                                    <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelar" OnClick="BotonCancelar_Click" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                                    <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimir" Visible="false" OnClick="BotonImprimir_Click" ImageUrl="/Imagen/Boton/BotonImprimir.png" runat="server" />&nbsp;&nbsp;
                                    <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimirVehiculo" Visible="false" OnClick="BotonImprimirVehiculo_Click" ImageUrl="/Imagen/Boton/BotonImprimir.png" runat="server" />&nbsp;&nbsp;
                                    <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimirVehiculoReverso" Visible="false" OnClick="BotonImprimirVehiculoReverso_Click" ImageUrl="/Imagen/Boton/BotonImprimirReverso.png" runat="server" />&nbsp;&nbsp;
                                    
                                    <br /><br /><br />
                                 </td>
                              </tr>
                           </table> 
                        </div>
                     
                     </asp:Panel> 
                     
                     <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaAsignacion" ID="ProgresoTablaAsignacion" runat="server">
                        <ProgressTemplate>
                            <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                        </ProgressTemplate>
                     </asp:UpdateProgress>
                     
                     <asp:HiddenField ID="TemporalAsignacionIdHidden" runat="server" Value="0" />
                     <asp:HiddenField ID="TemporalAsignacionDetalleIdHidden" runat="server" Value="0" />
                     <asp:HiddenField ID="ActivoIdHidden" runat="server" Value="0" />
                     <asp:HiddenField ID="TempAsigIdHidden" runat="server" Value="0" />
                     <asp:HiddenField ID="EmpIdHidden" runat="server" Value="0" />
                     <asp:HiddenField ID="ActivoVehiculoIdHidden" runat="server" Value="0" />
                     <asp:HiddenField ID="TipoActivoIdHidden" runat="server" Value="0" />
                     <asp:HiddenField ID="TipoActivoIdSeleccionadoHidden" runat="server" Value="0" />
                     <asp:HiddenField ID="CantActivosAgregadosHidden" runat="server" Value="0" />
                </ContentTemplate>

                <Triggers>
                    
                </Triggers>
            </asp:UpdatePanel>
        </div> 
    
    </div> 
</asp:Content>
