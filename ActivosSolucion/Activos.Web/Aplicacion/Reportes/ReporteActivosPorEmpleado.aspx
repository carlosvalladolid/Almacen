<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="ReporteActivosPorEmpleado.aspx.cs" Inherits="Activos.Web.Aplicacion.Reportes.ReporteActivosPorEmpleado" ViewStateEncryptionMode="Never" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarEmpleado" Src="~/Incluir/ControlesWeb/ControlBuscarEmpleado.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
   <script language="javascript" type="text/javascript">

        function Imprimir() {
           var Form = document.getElementById("aspnetForm");

           Form.action = "/Ventana/ImprimirReporteActivosPorEmpleado.aspx";
           Form.target = "_blank";
           Form.submit();

           Form.action = "ReporteActivosPorEmpleado.aspx";
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
           <div class="DivTitulo">Reporte activos por empleado</div>
      </div>
      
      <div class="DivInformacionContenido">
         <asp:UpdatePanel ID="ActualizarTablaReporte" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               <asp:Panel CssClass="Superposicion" ID="pnlFondo" runat="server" Visible="False"></asp:Panel>   
               <wuc:BuscarEmpleado ID="ControlBuscarEmpleado" runat="server" />
               
               <asp:Panel ID="PanelTablaReporte" runat="server">
                  <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                     <table class="TablaFormulario">
                        <tr>
                           <td class="Nombre">No.Empleado</td>
                           <td class="Espacio"></td>
                           <td class="Campo">
                              <asp:Panel ID="Panel1" runat="server" DefaultButton="LinkBuscarEmpleado">
                                 <asp:TextBox ID="NumeroEmpleado" OnTextChanged="NumeroEmpleado_TextChanged" AutoPostBack="true" CssClass="CajaTextoMediana"  MaxLength="20" runat="server" Text=""></asp:TextBox>
                                 <asp:ImageButton ImageUrl="/Imagen/Icono/ImagenBuscar.gif" ID="BotonBuscarEmpleado" OnClick="BotonBuscarEmpleado_Click" runat="server" />
                                 <asp:LinkButton ID="LinkBuscarEmpleado" OnClick="LinkBuscarEmpleado_Click" ValidationGroup="BuscarCodigoBarras" runat="server" Text="" Width="0px"></asp:LinkButton>
                              </asp:Panel>
                           </td>
                        </tr>
                        <tr>
                           <td class="Nombre">R.F.C.</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="ReporteRFC" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Nombre de Empleado</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="ReporteNombreEmpleado" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Dirección</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="ReporteDireccion" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Departamento</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="ReporteDepartamento" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Puesto</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="ReportePuesto" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td colspan="3">
                              <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
                              <br />
                              <asp:ImageButton ID="BotonImprimir" runat="server" AlternateText="Imprimir" Enabled="false"
                                  ImageUrl="/Imagen/Boton/BotonImprimir.png" OnClick="BotonImprimir_Click" />&nbsp;&nbsp;
                              <asp:ImageButton ID="BotonLimpiar" runat="server" AlternateText="Limpiar" 
                                  ImageUrl="/Imagen/Boton/BotonLimpiar.png" OnClick="BotonLimpiar_Click" />&nbsp;&nbsp;
                           </td>
                        </tr>
                     </table> 
                  </asp:Panel> 
               </asp:Panel>
               
               <div class="DivSubtituloPagina">
                  Activos Asignados
               </div>
               <br />
               <div class="DivTabla">
                  <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                      CssClass="TablaInformacion" PageSize="10" ID="TablaActivos" OnPageIndexChanging="TablaActivos_PageIndexChanging" runat="server">
                      <EmptyDataTemplate>
                          <table class="TablaVacia">
                              <tr class="Encabezado">
                                 <th style="width: 100px;">C.B.</th>
                                 <th>Descripción</th>
                                 <th style="width: 120px;">Marca</th>
                                 <th style="width: 100px;">Modelo</th>
                                 <th style="width: 120px;">No.Serie</th>
                                 <th style="width: 80px;">Condiciones</th>
                                 <th style="width: 90px;">Factura</th>
                                 <th style="width: 150px;">Proveedor</th>
                                 <th style="width: 80px;">Monto</th>                                
                               </tr>
                              <tr>
                                  <td colspan="9" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                              </tr>
                          </table>
                      </EmptyDataTemplate>
                      <HeaderStyle CssClass="Encabezado" />
                      <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                      <Columns>
                          <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="C.B." ItemStyle-HorizontalAlign="Left">
                              <HeaderStyle HorizontalAlign="Left" Width="100px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                              <HeaderStyle HorizontalAlign="Left" />
                          </asp:BoundField>
                          <asp:BoundField DataField="MarcaNombre" HeaderText="Marca" ItemStyle-HorizontalAlign="Left">
                              <HeaderStyle HorizontalAlign="Left" Width="120px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Left">
                              <HeaderStyle HorizontalAlign="Left" Width="100px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="NumeroSerie" HeaderText="Número de Serie" ItemStyle-HorizontalAlign="Left">
                              <HeaderStyle HorizontalAlign="Left" Width="120px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="CondicionNombre" HeaderText="Condiciones" ItemStyle-HorizontalAlign="Left">
                              <HeaderStyle HorizontalAlign="Left" Width="80px" />
                          </asp:BoundField> 
                          <asp:BoundField DataField="CompraFolio" HeaderText="Factura" ItemStyle-HorizontalAlign="Left">
                              <HeaderStyle HorizontalAlign="Left" Width="90px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="ProveedorNombre" HeaderText="Proveedor" ItemStyle-HorizontalAlign="Left">
                              <HeaderStyle HorizontalAlign="Left" Width="150px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Left">
                              <HeaderStyle HorizontalAlign="Left" Width="80px" />
                          </asp:BoundField>
                      </Columns>
                  </asp:GridView>
               </div>
               
               <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaReporte" ID="ProgresoTablaReporte" runat="server">
                  <ProgressTemplate>
                      <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                  </ProgressTemplate>
               </asp:UpdateProgress>
               
               <asp:HiddenField ID="EmpleadoIdHidden" runat="server" Value="0" />
               <asp:HiddenField ID="NumeroEmpleadoHidden" runat="server" Value="" />
               <asp:HiddenField ID="RFCHidden" runat="server" Value="" />
               <asp:HiddenField ID="NombreEmpleadoHidden" runat="server" Value="" />
               <asp:HiddenField ID="DireccionHidden" runat="server" Value="" />
               <asp:HiddenField ID="DepartamentoHidden" runat="server" Value="" />
               <asp:HiddenField ID="PuestoHidden" runat="server" Value="" />
               
            </ContentTemplate>
            <Triggers>
            </Triggers>
         </asp:UpdatePanel> 
      </div> 
   </div> 
</asp:Content>
