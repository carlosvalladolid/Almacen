<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="LevantamientoActivo.aspx.cs" Inherits="Activos.Web.Aplicacion.Activo.LevantamientoActivo" Title="" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>
<%@ Register TagPrefix="wuc" TagName="BuscarEmpleado" Src="~/Incluir/ControlesWeb/ControlBuscarEmpleado.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    <script src="/Incluir/Javascript/Buscar.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">

       function ImprimirLevantamiento(LevantamientoID) {
          window.open("/Ventana/ImprimirLevantamiento.aspx?LevantamientoID=" + LevantamientoID, "ImprimirLevantamiento", " resizable=yes,scrollbars=1");

       }

       function ImprimirLevantamientoCorrecto(EmpleadoID) {
          window.open("/Ventana/ImprimirLevantamientoCorrecto.aspx?EmpleadoID=" + EmpleadoID, "ImprimirLevantamiento", " resizable=yes,scrollbars=1");

       }

   </script>
    
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" SeccionMenu="ActivoFijo" runat="server" />
    </div>
    
    <div class="DivContenido">
        <div class="DivContenidoTitulo">
            <div class="DivTitulo">Levantamiento de Activo</div>
        </div>
       
        <div class="DivInformacionContenido">
             <asp:UpdatePanel ID="ActualizarTablaEmpleado" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Panel CssClass="Superposicion" ID="pnlFondo" runat="server" Visible="False"></asp:Panel>   
                <wuc:BuscarEmpleado ID="ControlBuscarEmpleado" runat="server" />
                
                <asp:Panel ID="PanelTablaTransferenciaActivo" runat="server">
                 
                    <asp:Panel CssClass="DivCampo" id="PanelDatoGeneral" runat="server">
                        <table class="TablaFormulario">
                            <tr>
                                <td class="Nombre">No.Empleado</td>
                                <td class="Requerido">*</td>
                                <td class="Campo">
                                 <asp:Panel ID="Panel1" runat="server" DefaultButton="LinkBuscarEmpleado">
                                    <asp:TextBox ID="NumeroEmpleado" OnTextChanged="NumeroEmpleado_TextChanged" AutoPostBack="true" CssClass="CajaTextoMediana"  MaxLength="20" runat="server" Text=""></asp:TextBox>
                                    <asp:ImageButton ImageUrl="/Imagen/Icono/ImagenBuscar.gif" ID="BotonBuscarEmpleado" OnClick="BotonBuscarEmpleado_Click" runat="server" />
                                    <asp:LinkButton ID="LinkBuscarEmpleado" OnClick="LinkBuscarEmpleado_Click" ValidationGroup="BuscarCodigoBarras" runat="server" Text="" Width="0px"></asp:LinkButton>
                                 </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td class="Nombre">Nombre de Empleado</td>
                                <td class="Espacio">&nbsp;</td>
                                <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="NombreEmpleado" Enabled="false" MaxLength="200" runat="server" Text=""></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="NombreConsultaActivo">Código Barras</td>
                                <td class="Requerido">*</td>
                                <td class="Campo">
                                    <asp:Panel ID="PanelBuscarCodigoBarras" runat="server" DefaultButton="BotonBuscarCodigoBarra">
                                       <asp:TextBox CssClass="CajaTextoMediana" Enabled="false" ID="CodigoBarraParticular" MaxLength="20" runat="server" Text=""></asp:TextBox>
                                       <asp:ImageButton Enabled="true" ID="BotonBuscarCodigoBarra" ImageUrl="/Imagen/Icono/IconoCodigoBarras.jpg" OnClick="BotonBuscarCodigoBarra_Click" ValidationGroup="BuscarCodigoBarraParticular" runat="server" />&nbsp;&nbsp;
                                    </asp:Panel> 
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    Los campos marcados con <span class="TextoError">*</span> son obligatorios
                                    <br />
                                    <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CodigoBarraParticular" Display="Dynamic" ErrorMessage="" ID="BuscarCodigoBarraParticularRequerido" SetFocusOnError="true" ValidationGroup="BuscarCodigoBarraParticular" runat="server"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="NumeroEmpleado" Display="Dynamic" ErrorMessage="" ID="NumeroEmpleadoRequerido" SetFocusOnError="true" ValidationGroup="GuardarLev" runat="server"></asp:RequiredFieldValidator>
                                    <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
                                    <asp:Label CssClass="TextoInformacion" ID="EtiquetaMensajeExito" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                 <td colspan="3">
                                    <asp:ImageButton class="Opcion" ID="BotonTerminarLevantamiento" runat="server" 
                                        AlternateText="TerminarLevantamiento" ImageUrl="/Imagen/Boton/BotonTerminarLevantamiento.png" 
                                        OnClick="BotonTerminarLevantamiento_Click"/>&nbsp;&nbsp;
                                    <asp:ImageButton ID="BotonImprimir" runat="server" AlternateText="Imprimir" Enabled="true"
                                        ImageUrl="/Imagen/Boton/BotonImprimir.png" OnClick="BotonImprimir_Click" />&nbsp;&nbsp;
                                    <asp:ImageButton ID="BotonLimpiar" runat="server" AlternateText="Limpiar" 
                                        ImageUrl="/Imagen/Boton/BotonLimpiar.png" OnClick="BotonLimpiar_Click" />&nbsp;&nbsp;
                                 </td>
                            </tr>
                        </table>
                     </asp:Panel>&nbsp;&nbsp;
                      
                     <div class="DivSubtituloPagina">
                        Activos Asignados
                     </div>
                     <br />
                     <div class="DivTabla">
                        <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnRowDataBound="TablaLevantamiento_RowDataBound"
                            CssClass="TablaInformacion" DataKeyNames="ActivoId, EstatusID, DescripcionPadre, Descripcion, NumeroSerie, Modelo, Color, CodigoBarrasParticular, NombreEstatus" ID="TablaLevantamiento" runat="server">
                            <EmptyDataTemplate>
                                <table class="TablaVacia">
                                    <tr class="Encabezado">
                                        <th style="width: 150px;">Accesorio de</th>
                                        <th>Descripción</th>
                                        <th style="width: 120px;">No.Serie</th>
                                        <th style="width: 100px;">Modelo</th>
                                        <th style="width: 80px;">Color</th>
                                        <th style="width: 100px;">Codigo de Barra</th>
                                        <th style="width: 160px;">Estatus</th> 
                                        <th style="width: 30px;">Validacion</th>                                
                                     </tr>
                                    <tr>
                                        <td colspan="8" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="Encabezado" />
                            <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                            <Columns>
                                <asp:BoundField DataField="DescripcionPadre" HeaderText="Accesorio de" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NumeroSerie" HeaderText="Numero de Serie" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Color" HeaderText="Color" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                </asp:BoundField>                                   
                                <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="Codigo Barras" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="NombreEstatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="160px" />
                                </asp:BoundField> 
                                <asp:TemplateField HeaderText="Validado">
                                    <ItemTemplate>
                                       <asp:ImageButton ImageUrl="/Imagen/Icono/IconoRevisado.jpg" Enabled="false" ID="IconoRevisado" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                   </asp:Panel>

                    <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaEmpleado" ID="ProgresoTablaEmpleado" runat="server">
                        <ProgressTemplate>
                            <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                        </ProgressTemplate>
                   </asp:UpdateProgress>
                  
                    <asp:HiddenField ID="EmpleadoIdHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="LevantamientoIdHidden" runat="server" Value="0" />
                    <asp:HiddenField ID="LevantamientoCorrectoHidden" runat="server" Value="SI" />
                    <asp:HiddenField ID="EmpIdHidden" runat="server" Value="0" />
                </ContentTemplate>             
                <Triggers> 
                                   
                </Triggers>
           </asp:UpdatePanel>
          
        </div>
       </div>   
</asp:Content>
