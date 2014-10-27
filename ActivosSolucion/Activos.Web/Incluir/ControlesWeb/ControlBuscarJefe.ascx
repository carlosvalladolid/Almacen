<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlBuscarJefe.ascx.cs" Inherits="Activos.Web.Incluir.ControlesWeb.ControlBuscarJefe" %>

<asp:UpdatePanel ID="updControlWeb" runat="server" UpdateMode="Conditional">
   <ContentTemplate>
      <asp:Panel CssClass="Superposicion" ID="pnlFondo" runat="server" Visible="False"></asp:Panel>
      
      <asp:Panel CssClass="PopupGrandeDiv" ID="pnlControl" runat="server" Visible="False">
         <div class="PopupGrandeEncabezadoDiv">
            <asp:Label class="TitleDivPage" ID="lblTitle" runat="server" Text="Seleccionar jefe"></asp:Label>
            <div style="float:right;"><asp:ImageButton ID="imgCancelarBuscarJefe" OnClick="imgCancelarBuscarJefe_Click" runat="server" ImageUrl="/Imagen/Boton/BotonCerrar.png" /></div>
         </div>
         
         <div class="PopupGrandeCuerpoDiv">
            <table class="TablaControlWeb">
                 <tr>
                     <td class="Nombre">Texto</td>
                     <td class="Espacio">&nbsp;</td>
                     <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="TextoBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td colspan="3">
                         <br />
                         <asp:ImageButton AlternateText="Buscar" ID="BotonBusquedaJefe" OnClick="BotonBusquedaJefe_Click" ImageUrl="/Imagen/Boton/BotonBuscar.png"  runat="server" />&nbsp;&nbsp;
                         <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarBusquedaJefe" OnClick="BotonCancelarBusquedaJefe_Click" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                     </td>
                 </tr>
             </table>
             
             <div id="DivTablaControl">
                <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnPageIndexChanging="TablaJefe_PageIndexChanging"
                    CssClass="TablaInformacion" OnRowCommand="TablaJefe_RowCommand" DataKeyNames="EmpleadoId" ID="TablaJefe" runat="server" PageSize="10">
                    <EmptyDataTemplate>
                        <table class="TablaVacia">
                            <tr class="Encabezado">
                                <th>Nombre</th>
                                <th style="width: 170px;">Puesto</th>
                                <th style="width: 170px;">Dirección</th>
                                <th style="width: 170px;">Departamento</th>
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
                                <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaNombre" runat="server" Text='<%#Eval("NombreEmpleado")%>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="NombrePuesto" HeaderText="Puesto" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="170px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NombreDireccion" HeaderText="Dirección" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="170px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NombreDepartamento" HeaderText="Departamento" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="170px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            
         </div> 
         
         <div class="PopupGrandePieDiv">
           <asp:Label CssClass="TextoError" ID="EtiquetaControlMensaje" runat="server" Text=""></asp:Label>
         </div>
      </asp:Panel>
   </ContentTemplate>

    <Triggers>
        
    </Triggers>
</asp:UpdatePanel>
