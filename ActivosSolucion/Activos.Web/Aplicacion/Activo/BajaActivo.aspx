<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="BajaActivo.aspx.cs" Inherits="Activos.Web.Aplicacion.Activo.BajaActivo" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarEmpleado" Src="~/Incluir/ControlesWeb/ControlBuscarEmpleado.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarAccesorio" Src="~/Incluir/ControlesWeb/ControlBuscarAccesorios.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
   <link href="/Incluir/Estilo/Privado/jquery-ui-1.8.16.custom.css" rel="Stylesheet" type="text/css" />
   <link href="/Incluir/Estilo/Privado/demos.css" rel="Stylesheet" type="text/css" />

   <script src="/Incluir/Javascript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
   <script src="/Incluir/Javascript/jquery.ui.datepicker-es.js" type="text/javascript"></script>
   <script src="/Incluir/Javascript/Calendar.js" type="text/javascript"></script>
   
   <script language="javascript" type="text/javascript">

      function pageLoad(sender, args) {
          SetNewCalendar("#<%=FechaBaja.ClientID%>");
      }

      function ImprimirBaja(EmpleadoAsignado) {
          //alert(EmpleadoAsignado);
          window.open("/Ventana/ImprimirBaja.aspx?Emp=" + EmpleadoAsignado, "ImprimirBaja", "");

      }

      function Salir() {
          alert(salir);
          document.location.href = "/Aplicacion/Inicio.aspx";
       }
       
      
   </script>
</asp:Content>


<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" SeccionMenu="ActivoFijo" runat="server" />
    </div>
    
    <div class="DivContenido">
        <div class="DivContenidoTitulo">
            <div class="DivTitulo">Baja de activo</div>
        </div>
        
        <div class="DivInformacionContenido">
             <asp:UpdatePanel ID="ActualizarTablaAsignacion" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <wuc:BuscarEmpleado ID="ControlBuscarEmpleado" runat="server" />
                <wuc:BuscarAccesorio ID="ControlBuscarAccesorio" runat="server" />
                    <asp:Panel ID="PanelTablaAsignacionActivo" runat="server">
                        <br />
                        <div class="DivSubtituloPagina">
                           Datos generales
                        </div>
                        
                        <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                           <table class="TablaFormulario">
                           <tr>
                                <td class="Nombre">Código Barras</td>
                                <td class="Requerido">*</td>
                                <td>
                                    
                                    <asp:TextBox CssClass=" CajaTextoDeshabilitadaMediana" Enabled="true" ID="CodigoBarrasBaja" MaxLength="20" runat="server" Text=""></asp:TextBox>
                                    <asp:ImageButton Enabled="true" ID="CodigoBarrasBajaImagen" ImageUrl="/Imagen/Icono/IconoCodigoBarras.jpg" OnClick="ImagenCodigoBarras_Click" runat="server" ValidationGroup="CodigoBarras" />&nbsp;&nbsp;
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
                              <td class="Nombre">Fecha de baja</td>
                              <td class="Requerido">*</td>
                              <td class="Campo">
                                 <asp:TextBox CssClass="CajaTextoPequenia" ID="FechaBaja" MaxLength="50" runat="server" Text="" ValidationGroup="BajaActivoGrupo"></asp:TextBox>
                                 <span class="NotaCampo">(dd/mm/aaaa)</span>
                              </td>
                              <td class="EspacioColumna"></td>
                            </tr>
                            <tr>
                               <td class="Nombre">Tipo de baja</td>
                                 <td class="Requerido">*</td>
                                 <td class="Campo">  
                                 
                                        <asp:RadioButtonList runat="server" ID="TipoBaja" Enabled="true" OnSelectedIndexChanged="RadioOtrosActivo_Select" ValidationGroup="BajaActivoGrupo" AutoPostBack="true">
                                        <asp:ListItem Value="1" Text="Inutilidad por uso normal" ></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Inaplicación en el servicio"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Otros" ></asp:ListItem>
                                        </asp:RadioButtonList> 
                                 
                                 </td>  
                             <tr>
                                <td class="Nombre">Observaciones</td>
                                <td class="Requerido">*</td>
                                <td class="Campo"><asp:TextBox TextMode="MultiLine" Enabled="false" CssClass="CajaTextoGrande" ID="OtrosTipoBaja" runat="server" Rows="5" ValidationGroup="BajaActivoGrupo"></asp:TextBox></td>
                             </tr>
                             <tr>
                                 <td class="Nombre">Condiciones</td>
                                 <td class="Requerido">*</td>
                                 <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="CondicionId" runat="server" ValidationGroup="BajaActivoGrupo" ></asp:DropDownList>
      
                                <br />
                                     <asp:CustomValidator CssClass="TextoError" ControlToValidate="FechaBaja" EnableClientScript="false" ErrorMessage="" ID="FechaBajaRequerido" OnServerValidate="FechaBaja_Validate" runat="server" SetFocusOnError="true" ValidationGroup="BajaActivoGrupo" ValidateEmptyText="true"></asp:CustomValidator>
                                      <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CodigoBarrasBaja" Display="Dynamic" ErrorMessage="" ID="CodigoBarras" SetFocusOnError="true" ValidationGroup="CodigoBarras" runat="server"></asp:RequiredFieldValidator>
                                     <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="TipoBaja" Display="Dynamic" ErrorMessage="" ID="TipoBajaRequerido" SetFocusOnError="true" ValidationGroup="BajaActivoGrupo" runat="server"></asp:RequiredFieldValidator>
                                     <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="OtrosTipoBaja" Display="Dynamic" ErrorMessage="" ID="OtrosTipoBajaRequerido" SetFocusOnError="true" ValidationGroup="BajaActivoGrupo" runat="server"></asp:RequiredFieldValidator>
                                     <asp:CompareValidator CssClass="TextoError" ControlToValidate="CondicionId" Display="Dynamic" ErrorMessage="" ID="CondicionRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="BajaActivoGrupo" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                     <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CodigoBarrasBaja" Display="Dynamic" ErrorMessage="" ID="CodigoBarrasRequerido" SetFocusOnError="true" ValidationGroup="BajaActivoGrupo" runat="server"></asp:RequiredFieldValidator>
                                     <asp:CompareValidator CssClass="TextoError" ControlToValidate="CondicionId" Display="Dynamic" ErrorMessage="" ID="CompareValidator1" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="BajaActivoGrupo" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                     <br />
                                      <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
                                     <br />
                                </td>
                            </tr>
                            
                            <tr>
                            
                                <td class="Campo" style=" width:auto;" colspan="3">
                                     <asp:ImageButton AlternateText="Agregar a lista" ID="BotonAgregar" ImageUrl="/Imagen/Boton/BotonAgregar.png" OnClick="BotonAgregar_Click" runat="server" ValidationGroup="BajaActivoGrupo" />&nbsp;&nbsp;
                                     <asp:ImageButton AlternateText="DarBaja" ID="BotonDarDeBaja" Enabled="false" OnClick="BotonDarDeBaja_Click" ValidationGroup="AgregarActivo" ImageUrl="~/Imagen/Boton/BotonGuardar.png" runat="server" />&nbsp;&nbsp;
                                     <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimir" Enabled="false" OnClick="BotonImprimir_Click" ValidationGroup="AgregarActivo" ImageUrl="/Imagen/Boton/BotonImprimir.png" runat="server" />&nbsp;&nbsp;
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
                            <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnRowDataBound="TablaBaja_RowDataBound"
                                CssClass="TablaInformacion" DataKeyNames="MovimientoId" ID="TablaBaja" OnRowCommand="TablaBaja_RowCommand" runat="server">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th>Descripción</th>
                                            <th style="width: 150px;">Número de serie</th>
                                            <th style="width: 150px;">Modelo</th>
                                            <th style="width: 100px;">Condiciones</th>
                                            <th style="width: 150px;">Código de barras</th>
                                            <th style="width: 150px;">Tipo de baja</th>
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
                                    <asp:BoundField DataField="CondiocionNombre" HeaderText="Condiciones" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="Código de barras" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TipoBajaNombre" HeaderText="Tipo de baja" ItemStyle-HorizontalAlign="Left">
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

                                    
                                    
                                 </td> 
                             </tr>
                        </table>
                        
                    </asp:Panel>
                    
                    <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaAsignacion" ID="ProgresoTablaEmpleado" runat="server">
                        <ProgressTemplate>
                            <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                        </ProgressTemplate>
                   </asp:UpdateProgress>
                    
                     <asp:HiddenField ID="CodigoBarrasParticularHidden" runat="server" Value="" />                     
                     <asp:HiddenField ID="EmpleadoIdHidden" runat="server" Value="0" />
                     <asp:HiddenField ID="NumeroEmpleadoHiddden" runat="server" Value="" />
                     <asp:HiddenField ID="ActivoIdHidden" runat="server" Value="0" />
                     <asp:HiddenField ID="ActivoPadreHidden" runat=server Value="0" />
                </ContentTemplate>
             </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
