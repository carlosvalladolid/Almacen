<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="ReporteGeneralActivo.aspx.cs" Inherits="Activos.Web.Aplicacion.Reportes.ReporteGeneralActivo" ViewStateEncryptionMode="Never" %>

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

           Form.action = "/Ventana/ReporteGeneralActivo.aspx";
           Form.target = "_blank";
           Form.submit();

           Form.action = "ReporteGeneralActivo.aspx";
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
            <div class="DivTitulo">Reporte general de activos</div>
        </div>
        
        <div class="DivInformacionContenido">
            <asp:UpdatePanel ID="ActualizarTablaAsignacion" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <asp:Panel ID="PanelTablaAsignacionActivo" runat="server">
                        <br />
                        <div class="DivSubtituloPagina">
                           Datos generales
                        </div>
                        
                        <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                           <table class="TablaFormulario">
                             <tr>
                                    <td class="Nombre">Familia</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="FamiliaId" OnSelectedIndexChanged="ddlFamilia_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">SubFamilia</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="SubFamiliaId" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Marca</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="MarcaId" runat="server" ></asp:DropDownList></td>
                                </tr>
                                 <tr>
                                    <td class="Nombre">Modelo</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="Modelo" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Fechas<span class="NotaCampo"> (dd/mm/aaaa)</span></td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo" >Desde&nbsp;&nbsp;<asp:TextBox CssClass="CajaTextoPequenia" ID="FechaDesde" MaxLength="50" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                       Hasta&nbsp;&nbsp;<asp:TextBox CssClass="CajaTextoPequenia" ID="FechaHasta" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Proveedor</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="ProveedorId" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Numero de Factura</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="CompraFolio" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Direccion</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="DireccionId" OnSelectedIndexChanged="ddlDireccion_SelectedIndexChanged" AutoPostBack="true"  runat="server"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="Nombre">Departamento</td>
                                    <td class="Espacio">&nbsp;</td>
                                    <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="DepartamentoId" runat="server" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                  <td colspan="3">
                                    <asp:CustomValidator CssClass="TextoError" ControlToValidate="FechaDesde" EnableClientScript="false" ErrorMessage="" ID="FechaDesdeValidado" OnServerValidate="FechaDesde_Validate" runat="server" SetFocusOnError="true" ValidationGroup="Imprimir" ValidateEmptyText="false"></asp:CustomValidator>
                                    <asp:CustomValidator CssClass="TextoError" ControlToValidate="FechaHasta" EnableClientScript="false" ErrorMessage="" ID="FechaHastaValidado" OnServerValidate="FechaHasta_Validate" runat="server" SetFocusOnError="true" ValidationGroup="Imprimir" ValidateEmptyText="false"></asp:CustomValidator>
                                  </td> 
                               </tr>
                               <tr>
                                 <td colspan="3">
                                 
                                    <asp:ImageButton AlternateText="Buscar" ID="BotonBuscar" OnClick="BotonBuscar_Click" ImageUrl="~/Imagen/Boton/BotonBuscar.png" runat="server" ValidationGroup="Imprimir"/>&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimir" OnClick="BotonImprimir_Click" ImageUrl="~/Imagen/Boton/BotonImprimir.png" runat="server" ValidationGroup="Imprimir" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton AlternateText="Limpiar" ID="BotonLimpiar" OnClick="BotonLimpiar_Click" ImageUrl="~/Imagen/Boton/BotonLimpiar.png" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                 </td> 
                              </tr>
                           </table> 
                        </asp:Panel>
                        
                        <div class="DivSubtituloPagina">
                           Activos
                        </div>
                        <br />
                        <div class="DivTabla">
                            <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" PageSize="10"
                                CssClass="TablaInformacion" DataKeyNames="ActivoId" ID="TablaActivo" OnPageIndexChanging="TablaActivo_PageIndexChanging" runat="server">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th style="width: 150px;">Código de barras</th>
                                            <th>Descripción</th>
                                            <th style="width: 100px;">Modelo</th>
                                            <th style="width: 120px;">Número de serie</th>
                                            <th style="width: 150px;">Empleado asignado</th>
                                            <th style="width: 100px;">Marca</th>
                                            <th style="width: 100px;">Edificio</th>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="text-align: center;">No se encontraron activos con los parametros seleccionados</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="Encabezado" />
                                <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                                <Columns>
                                    <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="Código de barras" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NumeroSerie" HeaderText="Número de serie" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpleadoNombreCompleto" HeaderText="Empleado asignado" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MarcaNombre" HeaderText="Marca" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EdificioNombre" HeaderText="Edificio" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div>
                           <table width="100%">
                              <tr>
                                 <td>
                                    <asp:Label CssClass="TextoError" ID="EtiquetaMensajeError" runat="server" Text=""></asp:Label>
                                    
                                    <br /><br /><br />
                                 </td>
                              </tr>
                           </table> 
                        </div>
                     
                     </asp:Panel> 
                     
                     <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaAsignacion" ID="ProgresoTablaAsignacion" runat="server">
                        <ProgressTemplate>
                            <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                        </ProgressTemplate>
                     </asp:UpdateProgress>
                     
                     <asp:HiddenField ID="FamiliaIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="FamiliaNombreHidden" runat="server" Value="" />
                     <asp:HiddenField ID="SubfamiliaIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="SubfamiliaNombreHidden" runat="server" Value="" />
                     <asp:HiddenField ID="MarcaIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="MarcaNombreHidden" runat="server" Value="" />
                     <asp:HiddenField ID="ModeloHidden" runat="server" Value="" />
                     <asp:HiddenField ID="FechaInicioHidden" runat="server" Value="" />
                     <asp:HiddenField ID="FechaFinHidden" runat="server" Value="" />
                     <asp:HiddenField ID="ProveedorIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="ProveedorNombreHidden" runat="server" Value="" />
                     <asp:HiddenField ID="FolioDocumentoHidden" runat="server" Value="" />
                     <asp:HiddenField ID="DireccionIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="DireccionNombreHidden" runat="server" Value="" />
                     <asp:HiddenField ID="DepartamentoIdHidden" runat="server" Value="" />
                     <asp:HiddenField ID="DepartamentoNombreHidden" runat="server" Value="" />
                </ContentTemplate>

                <Triggers>
                    
                </Triggers>
            </asp:UpdatePanel>
        </div> 
    
    </div> 
</asp:Content>
