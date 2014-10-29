<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlBuscarMantenimiento.ascx.cs" Inherits="Activos.Web.Incluir.ControlesWeb.ControlBuscarMantenimiento" %>


<asp:UpdatePanel ID="updControlBuscarMantenimiento" runat="server" UpdateMode="Conditional">
   <ContentTemplate>
      <asp:Panel CssClass="Superposicion" ID="pnlFondoBuscarMantenimiento" runat="server" Visible="False"></asp:Panel>
      
      <asp:Panel CssClass="PopupGrandeDiv" ID="pnlControlBuscarMantenimiento" runat="server" Visible="False">
         <div class="PopupGrandeEncabezadoDiv">
            <asp:Label class="TitleDivPage" ID="lblTitleBuscarMantenimiento" runat="server" Text="Seleccionar Mantenimiento"></asp:Label>
            <div style="float:right;"><asp:ImageButton ID="imgCancelarBuscarMantenimiento" OnClick="imgCancelarBuscarMantenimiento_Click" runat="server" ImageUrl="/Imagen/Boton/BotonCerrar.png" /></div>
         </div>
         
         <div class="PopupGrandeCuerpoDiv">
            <table class="TablaControlWeb">
                  <tr>
                     <td class="Nombre"></td>
                     <td class="Espacio"></td>
                     <td class="Campo">Folio    <asp:TextBox CssClass="CajaTextoMediana" ID="FolioBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td class="Nombre"></td>
                     <td class="Espacio"></td>
                     <td class="Campo">
                        <asp:RadioButton ID="RadioEmpleado" Text=" Empleado" OnCheckedChanged="RadioEmpleado_CheckedChanged" AutoPostBack="true" runat="server" Checked="true" GroupName="Asignacion" />&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="RadioArea" Text=" Area" OnCheckedChanged="RadioArea_CheckedChanged" AutoPostBack="true" runat="server" GroupName="Asignacion" />
                     </td>
                 </tr>
                 <tr>
                     <td class="Nombre"></td>
                     <td class="Espacio"></td>
                     <td class="Campo">
                        <asp:DropDownList CssClass="ComboGrande" ID="ComboAsignacion" runat="server" ></asp:DropDownList>
                     </td>
                 </tr>
                
                 <tr>
                     <td colspan="3">
                         <br />
                         <asp:ImageButton AlternateText="Buscar" ID="BotonBusquedaMantenimiento" OnClick="BotonBusquedaMantenimiento_Click" ImageUrl="/Imagen/Boton/BotonBuscar.png"  runat="server" />&nbsp;&nbsp;
                         <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarBusquedaMantenimiento" OnClick="BotonCancelarBusquedaMantenimiento_Click" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                     </td>
                 </tr>
             </table>
             
             <div id="DivTablaControl">
                <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnPageIndexChanging="TablaMantenimiento_PageIndexChanging"
                    CssClass="TablaInformacion" OnRowCommand="TablaMantenimiento_RowCommand" DataKeyNames="MantenimientoId" ID="TablaMantenimiento" runat="server" PageSize="10">
                    <EmptyDataTemplate>
                        <table class="TablaVacia">
                            <tr class="Encabezado">
                                <th style="width: 40px;">Atender</th>
                                <th style="width: 100px;">Folio</th>
                                <th style="width: 200px;">Empleado</th>
                                <th>Departamento</th>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="Encabezado" />
                    <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                    <Columns>
                        <asp:TemplateField HeaderText="Seleccionar">
                              <ItemTemplate>
                              <asp:ImageButton ImageUrl="~/Imagen/Icono/IconoRevisado.jpg" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaMantenimiento" runat="server" />
                            </ItemTemplate>
                              <ItemStyle HorizontalAlign="Center" Width="40px" />
                          </asp:TemplateField>
                        <asp:BoundField DataField="MantenimientoId" HeaderText="Folio" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EmpleadoNombreCompleto" HeaderText="Empleado" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DepartamentoNombre" HeaderText="Departamento" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            
         </div> 
         
         <div class="PopupGrandePieDiv">
           <asp:Label CssClass="TextoError" ID="EtiquetaControlBuscarMantenimientoMensaje" runat="server" Text=""></asp:Label>
         </div>
      </asp:Panel>
      
   </ContentTemplate>

    <Triggers>
        
    </Triggers>
</asp:UpdatePanel>