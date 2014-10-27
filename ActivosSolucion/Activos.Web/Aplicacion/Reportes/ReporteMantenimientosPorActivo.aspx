<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="ReporteMantenimientosPorActivo.aspx.cs" Inherits="Activos.Web.Aplicacion.Reportes.ReporteMantenimientosPorActivo" %>
<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">

   <link href="/Incluir/Estilo/Privado/jquery-ui-1.8.16.custom.css" rel="Stylesheet" type="text/css" />
   <link href="/Incluir/Estilo/Privado/demos.css" rel="Stylesheet" type="text/css" />
   <script src="/Incluir/Javascript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
   <script src="/Incluir/Javascript/jquery.ui.datepicker-es.js" type="text/javascript"></script>
   <script src="/Incluir/Javascript/Calendar.js" type="text/javascript"></script>
   <script language="javascript" type="text/javascript">

       function pageLoad(sender, args) {
           SetNewCalendar("#<%= FechaDesde.ClientID %>", "#<%= FechaHasta.ClientID %>");
       }

        function Imprimir() {
           var Form = document.getElementById("aspnetForm");

           Form.action = "/Ventana/ImprimirReporteMantenimientosPorActivo.aspx";
           Form.target = "_blank";
           Form.submit();

           Form.action = "ReporteMantenimientosPorActivo.aspx";
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
           <div class="DivTitulo">Reporte de mantenimientos por activo</div>
      </div>
      
      <div class="DivInformacionContenido">
         <asp:UpdatePanel ID="ActualizarTablaReporte" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            
               <asp:Panel ID="PanelTablaReporte" runat="server">
                  <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                     <table class="TablaFormulario">
                     <tr>
                           <td class="Nombre">Fechas<span class="NotaCampo"> (dd/mm/aaaa)</span></td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo">
                              Desde&nbsp;&nbsp;<asp:TextBox CssClass="CajaTextoPequenia" ID="FechaDesde" MaxLength="50" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                              Hasta&nbsp;&nbsp;<asp:TextBox CssClass="CajaTextoPequenia" ID="FechaHasta" MaxLength="50" runat="server" Text=""></asp:TextBox>
                           </td>
                        </tr>
                        <tr>
                           <td class="Nombre">Estatus</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:DropDownList CssClass="ComboPequeño" ID="EstatusId" runat="server" ></asp:DropDownList></td>
                        </tr>
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
                              <asp:CustomValidator CssClass="TextoError" ControlToValidate="FechaDesde" EnableClientScript="false" ErrorMessage="" ID="FechaDesdeValidado" OnServerValidate="FechaDesde_Validate" runat="server" SetFocusOnError="true" ValidationGroup="Imprimir" ValidateEmptyText="false"></asp:CustomValidator>
                              <asp:CustomValidator CssClass="TextoError" ControlToValidate="FechaHasta" EnableClientScript="false" ErrorMessage="" ID="FechaHastaValidado" OnServerValidate="FechaHasta_Validate" runat="server" SetFocusOnError="true" ValidationGroup="Imprimir" ValidateEmptyText="false"></asp:CustomValidator>
                           </td> 
                        </tr>
                        <tr>
                           <td colspan="3">
                              <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
                              <br />
                              <asp:ImageButton AlternateText="Buscar" ID="BotonBuscarActivo" OnClick="BotonBuscarActivo_Click" ImageUrl="/Imagen/Boton/BotonBuscar.png" runat="server" ValidationGroup="Imprimir" />&nbsp;&nbsp;
                              <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimir" Enabled="false" OnClick="BotonImprimir_Click" ImageUrl="/Imagen/Boton/BotonImprimir.png" runat="server" ValidationGroup="Imprimir"/>&nbsp;&nbsp;
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
                           <td class="Nombre">Marca</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="Marca" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Modelo</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="Modelo" Enabled="false" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td class="Nombre">Empleado asignado</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="EmpleadoAsignado" Enabled="false" runat="server" Text=""></asp:TextBox></td>
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
                     Mantenimientos
                  </div>
                  <br />
                  <div class="DivTabla">
                      <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                          CssClass="TablaInformacion" ID="TablaMantenimientos" OnPageIndexChanging="TablaMantenimientos_PageIndexChanging" PageSize="10" runat="server">
                          <EmptyDataTemplate>
                              <table class="TablaVacia">
                                  <tr class="Encabezado">
                                      <th style="width: 100px;">FECHA</th>
                                      <th style="width: 300px;">TÉCNICO QUE ATENDIÓ</th>
                                      <th style="width: 150px;">TIPO DE ASISTENCIA</th>
                                      <th style="width: 300px;">DESCRIPCIÓN</th>
                                  </tr>
                                  <tr>
                                      <td colspan="4" style="text-align: center;">No se encontraron movimientos con los parametros seleccionados</td>
                                  </tr>
                              </table>
                          </EmptyDataTemplate>
                          <HeaderStyle CssClass="Encabezado" />
                          <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                          <Columns>
                              <asp:BoundField DataField="FechaInserto" HeaderText="FECHA" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="100px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="EmpleadosNombre" HeaderText="TÉCNICO QUE ATENDIÓ" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="300px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="TipoAsistenciaNombre" HeaderText="TIPO DE ASISTENCIA" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="150px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="300px" />
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
               <asp:HiddenField ID="FechaDesdeHidden" runat="server" Value="0" />
               <asp:HiddenField ID="FechaHastaHidden" runat="server" Value="0" />
               <asp:HiddenField ID="CodigoBarrasHidden" runat="server" Value="" />
               <asp:HiddenField ID="DescripcionHidden" runat="server" Value="" />
               <asp:HiddenField ID="NumeroSerieHidden" runat="server" Value="" />
               <asp:HiddenField ID="ModeloHidden" runat="server" Value="" />
               <asp:HiddenField ID="MarcaHidden" runat="server" Value="" />
               <asp:HiddenField ID="FolioDocumentoHidden" runat="server" Value="" />
               <asp:HiddenField ID="EmpleadoAsignadoHidden" runat="server" Value="" />
               <asp:HiddenField ID="ProveedorHidden" runat="server" Value="" />
               <asp:HiddenField ID="EstatusIdHidden" runat="server" Value="" />
               <asp:HiddenField ID="EstatusNombreHidden" runat="server" Value="" />
            
            </ContentTemplate> 
            <Triggers>
            </Triggers>
         </asp:UpdatePanel> 
      </div> 
   </div> 
</asp:Content>
