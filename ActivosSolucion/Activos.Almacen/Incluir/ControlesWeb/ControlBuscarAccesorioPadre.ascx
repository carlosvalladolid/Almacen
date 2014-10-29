<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlBuscarAccesorioPadre.ascx.cs" Inherits="Activos.Web.Incluir.ControlesWeb.ControlBuscarAccesorioPadre" %>
<asp:UpdatePanel ID="updControlBuscarAccesorio" runat="server" UpdateMode="Conditional">
   <ContentTemplate>
      <asp:Panel CssClass="Superposicion" ID="pnlFondoBuscarAccesorio" runat="server" Visible="False"></asp:Panel>
      
      <asp:Panel CssClass="PopupGrandeDiv" ID="pnlControlBuscarAccesorio" runat="server" Visible="False">
         <div class="PopupGrandeEncabezadoDiv">
            <asp:Label class="TitleDivPage" ID="lblTitleBuscarAccesorio" runat="server" Text="Accesorios asignados a activo seleccionado"></asp:Label>
            <div style="float:right;"><asp:ImageButton ID="imgCancelarBuscarActivo" OnClick="imgCancelarBuscarAccesorio_Click" runat="server" ImageUrl="/Imagen/Boton/BotonCerrar.png" /></div>
         </div>
         
         <div class="PopupGrandeCuerpoDiv">
            <table class="TablaControlWeb">
                 <tr>
                     <td class="Nombre">Código de barras particular</td>
                     <td class="Espacio"></td>
                     <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CodigoBarrasParticular" MaxLength="50" runat="server" Text="" Enabled=false></asp:TextBox></td>
                 </tr>
                  <tr>
                     <td class="Nombre">Descripción</td>
                     <td class="Espacio"></td>
                     <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="Descripcion" MaxLength="50" runat="server" Text="" Enabled=false></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td class="Nombre">Número de serie</td>
                     <td class="Espacio"></td>
                     <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="NumeroSerie" MaxLength="50" runat="server" Text="" Enabled=false></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td class="Nombre">Modelo</td>
                     <td class="Espacio"></td>
                     <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="Modelo" MaxLength="50" runat="server" Text="" Enabled=false></asp:TextBox></td>
                 </tr>
             </table>
             
             <div id="DivTablaControl">
                <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnPageIndexChanging="TablaActivosEncontrados_PageIndexChanging"
                    CssClass="TablaInformacion" OnRowCommand="TablaActivosEncontrados_RowCommand" DataKeyNames="ActivoId, Descripcion, NumeroSerie, Modelo, Color" ID="TablaAccesoriosHijos" runat="server" PageSize="10">
                    <EmptyDataTemplate>
                        <table class="TablaVacia">
                            <tr class="Encabezado">
                                <th>Descripción</th>
                                <th style="width: 150px;">Código barras</th>
                                <th style="width: 150px;">Número serie</th>
                                <th style="width: 80px;">Modelo</th>
                                <th style="width: 80px;">Color</th>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">No se encontró información</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="Encabezado" />
                    <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                    <Columns>
                        <asp:TemplateField HeaderText="Descripción">
                            <ItemTemplate>
                                <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaNombre" runat="server" Text='<%#Eval("DescripcionActivo")%>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="Código barras" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NumeroSerie" HeaderText="Número serie" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Color" HeaderText="Color" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            
         </div> 
         
         <div class="PopupGrandePieDiv">
           <asp:Label Font-Bold="true" CssClass="TextoError" ID="AceptarMensajeDarSalida" runat="server" Text="" ></asp:Label><br />
        &nbsp;&nbsp;
         <asp:ImageButton ID="BotonAceptar" OnClick="imgAceptarAccesorio_Click" runat="server" ImageUrl="~/Imagen/Boton/BotonAceptar.png" />
         &nbsp;
         <asp:ImageButton ID="BotonCancelar" OnClick="imgCancelar_Click" runat="server" ImageUrl="~/Imagen/Boton/BotonCancelar.png" />
        
         </div>
         
         
      </asp:Panel>
   </ContentTemplate>

    <Triggers>
        
    </Triggers>
</asp:UpdatePanel>