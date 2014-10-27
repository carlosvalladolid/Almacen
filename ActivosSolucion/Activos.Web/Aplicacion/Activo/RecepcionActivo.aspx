<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="RecepcionActivo.aspx.cs" Inherits="Activos.Web.Aplicacion.Activo.RecepcionActivo" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarJefe" Src="~/Incluir/ControlesWeb/ControlBuscarJefe.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarEmpleado" Src="~/Incluir/ControlesWeb/ControlBuscarEmpleado.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
   <link href="/Incluir/Estilo/Privado/jquery-ui-1.8.16.custom.css" rel="Stylesheet" type="text/css" />
   <link href="/Incluir/Estilo/Privado/demos.css" rel="Stylesheet" type="text/css" />

   <script src="/Incluir/Javascript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
   <script src="/Incluir/Javascript/jquery.ui.datepicker-es.js" type="text/javascript"></script>
   <script src="/Incluir/Javascript/Calendar.js" type="text/javascript"></script>
   
   <script language="javascript" type="text/javascript">

      function pageLoad(sender, args) {
         SetNewCalendar("#<%= FechaCompra.ClientID %>", "#<%= FechaOC.ClientID %>");
      }
      
   </script>
</asp:Content>
<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" SeccionMenu="ActivoFijo" runat="server" />
    </div>
    
    <div class="DivContenido">
        <div class="DivContenidoTitulo">
            <div class="DivTitulo">Recepción de activo</div>
        </div>
        
        <div class="DivInformacionContenido">
            <asp:UpdatePanel ID="ActualizarTablaEmpleado" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                
                 <asp:Panel CssClass="Superposicion" ID="pnlFondo" runat="server" Visible="False"></asp:Panel>
                
                 <wuc:BuscarJefe ID="ControlBuscarJefe" runat="server" />
                 <wuc:BuscarEmpleado ID="ControlBuscarEmpleado" runat="server" />
                
                  <asp:Panel ID="PanelTablaRecepcionActivo" runat="server">
                     <br />
                     <div class="DivSubtituloPagina">
                        Datos generales
                     </div> 
                     
                     <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                        <table class="TablaFormulario">
                          <tr>
                              <td class="Nombre">Proveedor</td>
                              <td class="Espacio">&nbsp;</td>
                              <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="ProveedorId" runat="server" ></asp:DropDownList></td>
                              <td class="EspacioColumna"></td>
                          </tr>
                          <tr>
                              <td class="Nombre">Tipo de documento</td>
                              <td class="Espacio">&nbsp;</td>
                              <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="TipoDocumentoId" runat="server" ></asp:DropDownList></td>
                              <td class="EspacioColumna"></td>
                          </tr>
                          <tr>
                              <td class="Nombre">Folio</td>
                              <td class="Espacio">&nbsp;</td>
                              <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CompraFolio" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                              <td class="EspacioColumna"></td>
                          </tr>
                          <tr>
                              <td class="Nombre">Fecha del documento</td>
                              <td class="Espacio">&nbsp;</td>
                              <td class="Campo">
                                 <asp:TextBox CssClass="CajaTextoPequenia" ID="FechaCompra" MaxLength="50" runat="server" Text=""></asp:TextBox>
                                 <span class="NotaCampo">(dd/mm/aaaa)</span>
                              </td>
                              <td class="EspacioColumna"></td>
                          </tr>
                          <tr>
                              <td class="Nombre">Monto</td>
                              <td class="Espacio">&nbsp;</td>
                              <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="MontoDocumento" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                              <td class="EspacioColumna"></td>
                          </tr>
                          <tr>
                              <td class="Nombre">Orden de compra</td>
                              <td class="Espacio">&nbsp;</td>
                              <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="OrdenCompra" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                              <td class="EspacioColumna"></td>
                          </tr>
                          <tr>
                              <td class="Nombre">Fecha de O.C</td>
                              <td class="Espacio">&nbsp;</td>
                              <td class="Campo">
                                 <asp:TextBox CssClass="CajaTextoPequenia" ID="FechaOC" MaxLength="50" runat="server" Text=""></asp:TextBox>
                                 <span class="NotaCampo">(dd/mm/aaaa)</span>
                              </td>
                              <td class="EspacioColumna"></td>
                          </tr>
                          <tr>
                              <td class="Nombre">Número de Solicitante</td>
                              <td class="Espacio">&nbsp;</td>
                              <td class="Campo">
                                 <asp:Panel ID="PanelSolicitante" runat="server" DefaultButton="LinkBuscarSolicitante">
                                    <asp:TextBox ID="NumeroSolicitante" OnTextChanged="NumeroSolicitante_TextChanged" AutoPostBack="true" CssClass="CajaTextoMediana" MaxLength="10" runat="server"></asp:TextBox>&nbsp;
                                    <asp:ImageButton ImageUrl="/Imagen/Icono/ImagenBuscar.gif" ID="BotonBuscarSolicitante" OnClick="BotonBuscarSolicitante_Click" runat="server" />
                                    <asp:LinkButton ID="LinkBuscarSolicitante" OnClick="LinkBuscarSolicitante_Click" ValidationGroup="BuscarSolicitante" runat="server" Text="" Width="0px"></asp:LinkButton>
                                 </asp:Panel>
                              </td>
                              <td class="EspacioColumna"></td>
                          </tr>
                          <tr>
                              <td class="Nombre">Nombre de Solicitante</td>
                              <td class="Espacio">&nbsp;</td>
                              <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreSolicitante" Enabled="false" MaxLength="200" runat="server" Text=""></asp:TextBox></td>
                              <td class="EspacioColumna"></td>
                          </tr>
                          <tr>
                              <td class="Nombre">Jefe inmediato</td>
                              <td class="Espacio">&nbsp;</td>
                              <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreJefe" Enabled="false" MaxLength="150" runat="server" Text=""></asp:TextBox></td>
                              <td class="EspacioColumna"><asp:ImageButton ImageUrl="/Imagen/Icono/ImagenBuscar.gif" ID="BotonBuscarJefe" OnClick="BotonBuscarJefe_Click" runat="server" /></td>
                          </tr>
                        </table> 
                     </asp:Panel>
                     
                     <div class="DivSubtituloPagina">
                        Activo (detalle del documento)
                     </div>
                     
                     <asp:Panel CssClass="DivCampo" id="PanelDocumentoDetalle" runat="server">
                        <div class="DivCampoIzquierda">
                           <table class="TablaFormulario">
                                <tr>
                                    <td class="Nombre">Tipo</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="TipoActivoId" OnSelectedIndexChanged="ddlTipoActivo_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Familia</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="FamiliaId" OnSelectedIndexChanged="ddlFamilia_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">SubFamilia</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="SubFamiliaId" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Marca</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="MarcaId" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Condición</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="CondicionId" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Descripción</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="DescripcionActivo" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Numero de serie</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NumeroSerie" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Modelo</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="Modelo" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Color</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="Color" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Monto</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="MontoActivo" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                       <asp:CompareValidator CssClass="TextoError" ControlToValidate="TipoActivoId" Display="Dynamic" ErrorMessage="" ID="TipoActivoRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="AgregarActivo" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                       <asp:CompareValidator CssClass="TextoError" ControlToValidate="FamiliaId" Display="Dynamic" ErrorMessage="" ID="FamiliaRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="AgregarActivo" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                       <asp:CompareValidator CssClass="TextoError" ControlToValidate="SubFamiliaId" Display="Dynamic" ErrorMessage="" ID="SubFamiliaRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="AgregarActivo" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                       <asp:CompareValidator CssClass="TextoError" ControlToValidate="MarcaId" Display="Dynamic" ErrorMessage="" ID="MarcaRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="AgregarActivo" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                       <asp:CompareValidator CssClass="TextoError" ControlToValidate="CondicionId" Display="Dynamic" ErrorMessage="" ID="CondicionRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="AgregarActivo" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                       <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="MontoActivo" Display="Dynamic" ErrorMessage="" ID="MontoActivoRequerido" SetFocusOnError="true" ValidationGroup="AgregarActivo" runat="server"></asp:RequiredFieldValidator>
                                       <asp:RegularExpressionValidator CssClass="TextoError" ID="MontoActivoValidado" runat="server" ControlToValidate="MontoActivo" ErrorMessage="" ValidationGroup="AgregarActivo" ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                                       
                                       <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
                                       <br />
                                       <asp:ImageButton AlternateText="Agregar" ID="BotonAgregarActivo" OnClick="BotonAgregarActivo_Click" ValidationGroup="AgregarActivo" ImageUrl="/Imagen/Boton/BotonAgregar.png" runat="server" />&nbsp;&nbsp;
                                       <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarNuevo" OnClick="BotonCancelarNuevo_Click" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                                       <asp:ImageButton AlternateText="Actualizar" ID="BotonActualizarActivo" OnClick="BotonActualizarActivo_Click" ValidationGroup="AgregarActivo" ImageUrl="/Imagen/Boton/BotonActualizar.png" Visible="false" runat="server" />&nbsp;&nbsp;
                                       <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarActualizar" OnClick="BotonCancelarActualizar_Click" ImageUrl="/Imagen/Boton/BotonCancelar.png" Visible="false" runat="server" />
                                    </td>
                                </tr>
                           </table> 
                        </div>
                        <br />
                        <asp:Panel CssClass="PopupMedianoDiv" ID="pnlAccesorio" Visible="false" runat="server">
                           <div class="PopupMedianoEncabezadoDiv">
                              <asp:Label class="TitleDivPage" ID="lblTitleAccesorio" runat="server" Text="Accesorios"></asp:Label>
                           </div> 
                           <div class="PopupMedianoCuerpoDiv">
                              <table class="TablaControlWeb">
                                   <tr>
                                       <td class="Nombre">Tipo</td>
                                       <td class="Espacio">&nbsp;</td>
                                       <td class="Campo"><asp:DropDownList ID="TipoAccesorioId" OnSelectedIndexChanged="TipoAccesorioId_SelectedIndexChanged" AutoPostBack="true" CssClass="ComboGrande" runat="server" ></asp:DropDownList></td>
                                   </tr>
                                   <tr id="trDescripcionAccesorio" runat="server" style="display:none;">
                                       <td class="Nombre">Descripción</td>
                                       <td class="Espacio">&nbsp;</td>
                                       <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="DescripcionAccesorio" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                   </tr>
                                   <tr id="trCodigoActivoAccesorio" runat="server" style="display:none;">
                                       <td class="Nombre">Cód. Bar. Part.</td>
                                       <td class="Espacio">&nbsp;</td>
                                       <td class="Campo">
                                          <asp:Panel ID="PanelBuscarCodigoBarras" runat="server" DefaultButton="LinkBuscarActivoAccesorio">
                                             <asp:TextBox ID="CodigoBarrasParticular" CssClass="CajaTextoMediana" MaxLength="15" runat="server"></asp:TextBox>
                                             <asp:LinkButton ID="LinkBuscarActivoAccesorio" OnClick="LinkBuscarActivoAccesorio_Click" ValidationGroup="BuscarCodigoBarras" runat="server" Text="" Width="0px"></asp:LinkButton>
                                          </asp:Panel>
                                       </td>
                                   </tr>
                                   <tr id="trDescripcionActivoAccesorio" runat="server" style="display:none;">
                                       <td class="Nombre"></td>
                                       <td class="Espacio">&nbsp;</td>
                                       <td class="Campo"><asp:TextBox ID="DecripcionActivoAccesorio" CssClass="CajaTextoGrande" Enabled="false" runat="server"></asp:TextBox></td>
                                   </tr>
                                   <tr>
                                       <td colspan="3">
                                          <asp:CompareValidator CssClass="TextoError" ControlToValidate="TipoAccesorioId" Display="Dynamic" ErrorMessage="" ID="TipoAccesorioRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="AgregarAccesorio" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                          <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CodigoBarrasParticular" Display="Dynamic" ErrorMessage="" ID="BuscarCodigoBarrasRequerido" SetFocusOnError="true" ValidationGroup="BuscarCodigoBarras" runat="server"></asp:RequiredFieldValidator>
                                          <br />
                                          <asp:ImageButton AlternateText="Agregar" ID="BotonAgregarAccesorio" ValidationGroup="AgregarAccesorio" OnClick="BotonAgregarAccesorio_Click" ImageUrl="/Imagen/Boton/BotonAgregar.png" runat="server" />&nbsp;&nbsp;
                                          <asp:Label CssClass="TextoError" ID="EtiquetaMensajeAccesorio" runat="server" Text=""></asp:Label>
                                       </td>
                                   </tr>
                              </table>
                              <div id="DivTablaControl">
                                  <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnRowCommand="TablaAccesorio_RowCommand"
                                      CssClass="TablaInformacion" DataKeyNames="TemporalAccesorioId, Estatus" ID="TablaAccesorio" runat="server">
                                      <EmptyDataTemplate>
                                          <table class="TablaVacia">
                                              <tr class="Encabezado">
                                                  <th style="width: 25px;"></th>
                                                  <th>Tipo</th>
                                                  <th style="width: 200px;">Descripción</th>
                                                  <th style="width: 100px;">Código Barras</th>
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
                                                  <asp:ImageButton ID="BotonEliminarAccesorio" CommandArgument="<%#Container.DataItemIndex%>" CommandName="EliminarAccesorio" runat="server" ImageUrl="/Imagen/Icono/IconoEliminarRegistro.gif" />
                                              </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Center" Width="25px" />
                                          </asp:TemplateField>
                                          <asp:BoundField DataField="NombreTipoAccesorio" HeaderText="Tipo" ItemStyle-HorizontalAlign="Center">
                                              <HeaderStyle HorizontalAlign="Center" />
                                          </asp:BoundField>
                                          <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center">
                                              <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                          </asp:BoundField>
                                          <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="Código Barras" ItemStyle-HorizontalAlign="Center">
                                              <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                          </asp:BoundField>
                                      </Columns>
                                  </asp:GridView>
                                  <br />
                              </div>
                           </div> 
                        </asp:Panel> 
                     </asp:Panel>
                     
                     <div class="DivTabla">
                         <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnRowDataBound="TablaActivo_RowDataBound"
                             CssClass="TablaInformacion" DataKeyNames="TemporalActivoId" ID="TablaActivo" OnRowCommand="TablaActivo_RowCommand" runat="server">
                             <EmptyDataTemplate>
                                 <table class="TablaVacia">
                                     <tr class="Encabezado">
                                         <th>Descripción</th>
                                         <th style="width: 180px;">Número de serie</th>
                                         <th style="width: 180px;">Modelo</th>
                                         <th style="width: 180px;">Color</th>
                                         <th style="width: 180px;">Monto</th>
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
                                     <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Center">
                                     <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Color" HeaderText="Color" ItemStyle-HorizontalAlign="Center">
                                     <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Monto" HeaderText="Monto" ItemStyle-HorizontalAlign="Center">
                                     <HeaderStyle HorizontalAlign="Center" Width="180px" />
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
                              <td style="width:70%;">
                                 <asp:CompareValidator CssClass="TextoError" ControlToValidate="ProveedorId" Display="Dynamic" ErrorMessage="" ID="ProveedorRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                 <asp:CompareValidator CssClass="TextoError" ControlToValidate="TipoDocumentoId" Display="Dynamic" ErrorMessage="" ID="TipoDocumentoRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                 <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CompraFolio" Display="Dynamic" ErrorMessage="" ID="FolioRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                 <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="MontoDocumento" Display="Dynamic" ErrorMessage="" ID="MontoDocumentoRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator CssClass="TextoError" ID="MontoDocumentoValidado" runat="server" ControlToValidate="MontoDocumento" ErrorMessage="" ValidationGroup="Guardar" ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                                 <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="OrdenCompra" Display="Dynamic" ErrorMessage="" ID="OrdenCompraRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                 <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NombreJefe" Display="Dynamic" ErrorMessage="" ID="JefeRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                 <asp:CustomValidator CssClass="TextoError" ControlToValidate="FechaCompra" EnableClientScript="false" ErrorMessage="" ID="FechaDocumentoValidado" OnServerValidate="txtFechaDocumento_Validate" runat="server" SetFocusOnError="true" ValidationGroup="Guardar" ValidateEmptyText="true"></asp:CustomValidator>
                                 <asp:CustomValidator CssClass="TextoError" ControlToValidate="FechaOC" EnableClientScript="false" ErrorMessage="" ID="FechaOCValidado" OnServerValidate="txtFechaOC_Validate"  runat="server" SetFocusOnError="true" ValidationGroup="Guardar" ValidateEmptyText="True"></asp:CustomValidator>
                                 <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NumeroSolicitante" Display="Dynamic" ErrorMessage="" ID="NumeroSolicitanteGuardarRequerido" SetFocusOnError="true" ValidationGroup="Guardar" runat="server"></asp:RequiredFieldValidator>
                                 <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NumeroSolicitante" Display="Dynamic" ErrorMessage="" ID="NumeroSolicitanteBuscarRequerido" SetFocusOnError="true" ValidationGroup="BuscarSolicitante" runat="server"></asp:RequiredFieldValidator>
                                 
                                 <br />
                                 <asp:ImageButton AlternateText="Guardar" ID="BotonGuardar" OnClick="BotonGuardar_Click" ImageUrl="/Imagen/Boton/BotonGuardar.png" ValidationGroup="Guardar" runat="server" />&nbsp;&nbsp;
                                 <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelar" OnClick="BotonCancelar_Click" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                              </td>
                              <td style="width:20%; text-align:right;">
                                 <asp:Label ID="LabelEtiquetaTotal" CssClass="MontoTotal" Text="Total:" runat="server"></asp:Label>&nbsp;
                                 <asp:Label ID="LabelMontoTotal" CssClass="MontoTotal" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                           </tr>
                        </table> 
                     </div>
                     <br /><br /><br />   
                  </asp:Panel> 
                
                   <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaEmpleado" ID="ProgresoTablaEmpleado" runat="server">
                        <ProgressTemplate>
                            <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <asp:HiddenField ID="TemporalCompraIdHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="SolicitanteIdHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="TemporalActivoIdHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="AccesorioOperacionHidden" runat="server" Value="Nuevo" />
                    <asp:HiddenField ID="ActivoAccesorioIdHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="MontoTotalHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="JefeIdHidden" runat="server" Value="0" />
                </ContentTemplate>

                <Triggers>
                    
                </Triggers>
            </asp:UpdatePanel>
        </div> 
    </div> 
</asp:Content>
