<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlImprimirTransferenciaActivo.ascx.cs" Inherits="Activos.Web.Incluir.ControlesWeb.ControlImprimirTransferenciaActivo" %>

<asp:UpdatePanel ID="updControlImprimirTransferencia" runat="server" UpdateMode="Conditional">
   <ContentTemplate>
      <asp:Panel CssClass="Superposicion" ID="pnlFondoImprimirTransferencia" runat="server" Visible="False"></asp:Panel>
      
      <asp:Panel CssClass="PopupGrandeDiv" ID="pnlControlImprimirTransferencia" runat="server" Visible="False">
         <div class="PopupGrandeEncabezadoDiv">
            <asp:Label class="TitleDivPage" ID="lblTitleImprimirTransferencia" runat="server" Text="Imprimir documentos"></asp:Label>
         </div>
         
         <div class="PopupGrandeCuerpoDiv">
            <div id="DivTablaControl">
                <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                    CssClass="TablaInformacion" OnRowCommand="TablaDocumentos_RowCommand" DataKeyNames="CompraId, TipoDocumentoId, CompraFolio" ID="TablaDocumentos" runat="server">
                    <EmptyDataTemplate>
                        <table class="TablaVacia">
                            <tr class="Encabezado">
                                <th style="width: 170px;">Folio</th>
                                <th style="width: 170px;">Documento</th>
                                <th>Proveedor</th>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="Encabezado" />
                    <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                    <Columns>
                        <asp:TemplateField HeaderText="Folio">
                            <ItemTemplate>
                                <asp:LinkButton CommandArgument="<%#Container.DataItemIndex%>" CommandName="Select" ID="LigaFolio" runat="server" Text='<%#Eval("CompraFolio")%>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="170px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TipoDocumentoNombre" HeaderText="Documento" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="170px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ProveedorNombre" HeaderText="Proveedor" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
         </div> 
         
         <div class="PopupGrandePieDiv">
           <asp:Label CssClass="TextoError" ID="ControlImprimirTransferenciaMensaje" runat="server" Text=""></asp:Label>
           <br />
           <asp:ImageButton AlternateText="Aceptar" ID="BotonAceptar" OnClick="BotonAceptar_Click" ImageUrl="/Imagen/Boton/BotonAceptar.png" runat="server" />&nbsp;&nbsp;
         </div>
         
      </asp:Panel> 
   </ContentTemplate>

   <Triggers>
   </Triggers>
</asp:UpdatePanel> 
