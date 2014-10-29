<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlBuscarEmpleado.ascx.cs" Inherits="Activos.Web.Incluir.ControlesWeb.ControlBuscarEmpleado" %>

<asp:UpdatePanel ID="updControlBuscarEmpleado" runat="server" UpdateMode="Conditional">
   <ContentTemplate>
      <asp:Panel CssClass="Superposicion" ID="pnlFondoBuscarEmpleado" runat="server" Visible="False"></asp:Panel>
      
      <asp:Panel CssClass="PopupGrandeDiv" ID="pnlControlBuscarEmpleado" runat="server" Visible="False">
         <div class="PopupGrandeEncabezadoDiv">
            <asp:Label class="TitleDivPage" ID="lblTitleBuscarEmpleado" runat="server" Text="Seleccionar empleado"></asp:Label>
            <div style="float:right;"><asp:ImageButton ID="imgCancelarBuscarEmpleado" OnClick="imgCancelarBuscarEmpleado_Click" runat="server" ImageUrl="/Imagen/Boton/BotonCerrar.png" /></div>
         </div>
         
         <div class="PopupGrandeCuerpoDiv">
            <table class="TablaControlWeb">
                 <tr>
                     <td class="Nombre">Departamento</td>
                     <td class="Espacio">&nbsp;</td>
                     <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="DepartamentoBusqueda" runat="server" ></asp:DropDownList></td>
                 </tr>
                 <tr>
                     <td class="Nombre">Edificio</td>
                     <td class="Espacio">&nbsp;</td>
                     <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="EdificioBusqueda" runat="server" ></asp:DropDownList></td>
                 </tr>
                 <tr>
                     <td class="Nombre">Puesto</td>
                     <td class="Espacio">&nbsp;</td>
                     <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="PuestoBusqueda" runat="server"></asp:DropDownList></td>
                 </tr>
                 <tr>
                     <td class="Nombre">Nombre</td>
                     <td class="Espacio"></td>
                     <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="NombreBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td class="Nombre">Email del trabajo</td>
                     <td class="Espacio"></td>
                     <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CorreoElectronicoBusqueda" MaxLength="65" runat="server" Text=""></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td colspan="3">
                         <br />
                         <asp:ImageButton AlternateText="Buscar" ID="BotonBusquedaEmpleado" OnClick="BotonBusquedaEmpleado_Click" ImageUrl="/Imagen/Boton/BotonBuscar.png"  runat="server" />&nbsp;&nbsp;
                         <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarBusquedaEmpleado" OnClick="BotonCancelarBusquedaEmpleado_Click" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                     </td>
                 </tr>
             </table>
             
             <div id="DivTablaControl">
                <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnPageIndexChanging="TablaEmpleado_PageIndexChanging"
                    CssClass="TablaInformacion" OnRowCommand="TablaEmpleado_RowCommand" DataKeyNames="EmpleadoId" ID="TablaEmpleado" runat="server" PageSize="10">
                    <EmptyDataTemplate>
                        <table class="TablaVacia">
                            <tr class="Encabezado">
                                <th>Nombre</th>
                                <th style="width: 170px;">Email del trabajo</th>
                                <th style="width: 170px;">Dirección</th>
                                <th style="width: 170px;">Puesto</th>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="Encabezado" />
                    <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                    <Columns>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaNombre" runat="server" Text='<%#Eval("NombreEmpleadoCompleto")%>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TrabajoEmail" HeaderText="Email del trabajo" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="170px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NombreDireccion" HeaderText="Dirección" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="170px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NombrePuesto" HeaderText="Puesto" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="170px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            
         </div> 
         
         <div class="PopupGrandePieDiv">
           <asp:Label CssClass="TextoError" ID="EtiquetaControlBuscarEmpleadoMensaje" runat="server" Text=""></asp:Label>
         </div>
      </asp:Panel>
      <asp:HiddenField ID="TipoBusquedaHidden" runat="server" Value="1" />
   </ContentTemplate>

    <Triggers>
        
    </Triggers>
</asp:UpdatePanel>

