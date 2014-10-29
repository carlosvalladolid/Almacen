<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlBuscarActivo.ascx.cs" Inherits="Activos.Web.Incluir.ControlesWeb.ControlBuscarActivo" %>

<asp:UpdatePanel ID="updControlBuscarActivo" runat="server" UpdateMode="Conditional">
   <ContentTemplate>
      <asp:Panel CssClass="Superposicion" ID="pnlFondoBuscarActivo" runat="server" Visible="False"></asp:Panel>
      
      <asp:Panel CssClass="PopupGrandeDiv" ID="pnlControlBuscarActivo" runat="server" Visible="False">
         <div class="PopupGrandeEncabezadoDiv">
            <asp:Label class="TitleDivPage" ID="lblTitleBuscarActivo" runat="server" Text="Seleccionar activo"></asp:Label>
            <div style="float:right;"><asp:ImageButton ID="imgCancelarBuscarActivo" OnClick="imgCancelarBuscarActivo_Click" runat="server" ImageUrl="/Imagen/Boton/BotonCerrar.png" /></div>
         </div>
         
         <div class="PopupGrandeCuerpoDiv">
            <table class="TablaControlWeb">
                 <tr>
                     <td class="Nombre">Número de serie</td>
                     <td class="Espacio"></td>
                     <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="NumeroSerieBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td class="Nombre">Código de barras particular</td>
                     <td class="Espacio"></td>
                     <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CodigoBarrasParticularBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td class="Nombre">Descripción</td>
                     <td class="Espacio"></td>
                     <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="DescripcionBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td class="Nombre">Modelo</td>
                     <td class="Espacio"></td>
                     <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="ModeloBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td class="Nombre">Color</td>
                     <td class="Espacio"></td>
                     <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="ColorBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td colspan="3">
                         <br />
                         <asp:ImageButton AlternateText="Buscar" ID="BotonBusquedaActivo" OnClick="BotonBusquedaActivo_Click" ImageUrl="/Imagen/Boton/BotonBuscar.png"  runat="server" />&nbsp;&nbsp;
                         <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarBusquedaActivo" OnClick="BotonCancelarBusquedaActivo_Click" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                     </td>
                 </tr>
             </table>
             
             <div id="DivTablaControl">
                <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnPageIndexChanging="TablaActivosEncontrados_PageIndexChanging"
                    CssClass="TablaInformacion" OnRowCommand="TablaActivosEncontrados_RowCommand" DataKeyNames="ActivoId, Descripcion, NumeroSerie, Modelo, Color" ID="TablaActivosEncontrados" runat="server" PageSize="10">
                    <EmptyDataTemplate>
                        <table class="TablaVacia">
                            <tr class="Encabezado">
                                <th>Descripción</th>
                                <th style="width: 150px;">Número serie</th>
                                <th style="width: 150px;">Código barras</th>
                                <th style="width: 80px;">Modelo</th>
                                <th style="width: 80px;">Color</th>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="Encabezado" />
                    <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                    <Columns>
                        <asp:TemplateField HeaderText="Descripción">
                            <ItemTemplate>
                                <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaNombre" runat="server" Text='<%#Eval("Descripcion")%>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="NumeroSerie" HeaderText="Número serie" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="Código barras" ItemStyle-HorizontalAlign="Center">
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
           <asp:Label CssClass="TextoError" ID="EtiquetaControlBuscarActivoMensaje" runat="server" Text=""></asp:Label>
         </div>
      </asp:Panel>
   </ContentTemplate>

    <Triggers>
        
    </Triggers>
</asp:UpdatePanel>

