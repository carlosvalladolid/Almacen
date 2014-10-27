<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="ReporteRastreoActivo.aspx.cs" Inherits="Activos.Web.Aplicacion.Reportes.ReporteRastreoActivo" ViewStateEncryptionMode="Never" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
   <script language="javascript" type="text/javascript">

        function Imprimir() {
           var Form = document.getElementById("aspnetForm");

           Form.action = "/Ventana/ImprimirReporteRastreoActivo.aspx";
           Form.target = "_blank";
           Form.submit();

           Form.action = "ReporteRastreoActivo.aspx";
           Form.target = "_self";
        }
       
   </script>
</asp:Content>
<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   <div class="DivMenuContenido">
      <wuc:Menu ID="ControlMenu" SeccionMenu="Reportes" runat="server" />
   </div>
   
   <div class="DivContenido">
      <div class="DivContenidoTitulo">
           <div class="DivTitulo">Reporte rastreo de activo</div>
      </div>
      
      <div class="DivInformacionContenido">
         <asp:UpdatePanel ID="ActualizarTablaReporte" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            
               <asp:Panel ID="PanelTablaReporte" runat="server">
                  <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                     <table class="TablaFormulario">
                        <tr>
                           <td class="Nombre">Código de barras</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CodigoBarrasBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Número de serie</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="NumeroSerieBusqueda" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td colspan="3">
                              <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
                              <br />
                              <asp:ImageButton AlternateText="Buscar" ID="BotonBuscarActivo" OnClick="BotonBuscarActivo_Click" ImageUrl="/Imagen/Boton/BotonBuscar.png" runat="server" />&nbsp;&nbsp;
                              <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimir" Enabled="false" OnClick="BotonImprimir_Click" ImageUrl="/Imagen/Boton/BotonImprimir.png" runat="server" />&nbsp;&nbsp;
                              <asp:ImageButton AlternateText="Limpiar" ID="BotonLimpiar" OnClick="BotonLimpiar_Click" ImageUrl="/Imagen/Boton/BotonLimpiar.png" runat="server" />
                           </td>
                        </tr>
                        <tr>
                           <td class="Nombre">Código de barras</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CodigoBarras" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Descripcion</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="Descripcion" Enabled="false" TextMode="MultiLine" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Número de serie</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="NumeroSerie" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Modelo</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="Modelo" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Marca</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="Marca" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Dirección</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="Direccion" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Departamento</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="Departamento" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Edificio</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="Edificio" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Folio documento</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="FolioDocumento" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Proveedor</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="Proveedor" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                     </table> 
                  </asp:Panel>
                  <br />
                  
                  <div class="DivSubtituloPagina">
                     Movimientos
                  </div>
                  <br />
                  <div class="DivTabla">
                      <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                          CssClass="TablaInformacion" ID="TablaMovimientos" OnPageIndexChanging="TablaMovimientos_PageIndexChanging" PageSize="10" runat="server">
                          <EmptyDataTemplate>
                              <table class="TablaVacia">
                                  <tr class="Encabezado">
                                      <th style="width: 100px;">Fecha</th>
                                      <th style="width: 150px;">Tipo de Movimiento</th>
                                      <th>Empleado que lo tiene asignado</th>
                                      <th style="width: 120px;">Condiciones</th>
                                  </tr>
                                  <tr>
                                      <td colspan="4" style="text-align: center;">No se encontraron movimientos con los parametros seleccionados</td>
                                  </tr>
                              </table>
                          </EmptyDataTemplate>
                          <HeaderStyle CssClass="Encabezado" />
                          <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                          <Columns>
                              <asp:BoundField DataField="FechaMovimiento" HeaderText="Fecha" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="100px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="TipoMovimientoNombre" HeaderText="Tipo de Movimiento" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="150px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="EmpleadoNombreCompleto" HeaderText="Empleado que lo tiene asignado" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" />
                              </asp:BoundField>
                              <asp:BoundField DataField="CondicionNombre" HeaderText="Condiciones" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="120px" />
                              </asp:BoundField>
                          </Columns>
                      </asp:GridView>
                  </div>
               
               </asp:Panel> 
            
               <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaReporte" ID="ProgresoTablaReporte" runat="server">
                  <ProgressTemplate>
                      <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                  </ProgressTemplate>
               </asp:UpdateProgress>
               
               <asp:HiddenField ID="ActivoIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="CodigoBarrasHidden" runat="server" Value="" />
               <asp:HiddenField ID="DescripcionHidden" runat="server" Value="" />
               <asp:HiddenField ID="NumeroSerieHidden" runat="server" Value="" />
               <asp:HiddenField ID="ModeloHidden" runat="server" Value="" />
               <asp:HiddenField ID="MarcaHidden" runat="server" Value="" />
               <asp:HiddenField ID="DireccionHidden" runat="server" Value="" />
               <asp:HiddenField ID="DepartamentoHidden" runat="server" Value="" />
               <asp:HiddenField ID="EdificioHidden" runat="server" Value="" />
               <asp:HiddenField ID="FolioDocumentoHidden" runat="server" Value="" />
               <asp:HiddenField ID="ProveedorHidden" runat="server" Value="" />
            
            </ContentTemplate> 
            <Triggers>
            </Triggers>
         </asp:UpdatePanel> 
      </div> 
   </div> 
</asp:Content>
