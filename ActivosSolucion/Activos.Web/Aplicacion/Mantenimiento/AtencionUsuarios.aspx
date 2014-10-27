<%@ Page Title="" Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="AtencionUsuarios.aspx.cs" Inherits="Activos.Web.Aplicacion.Mantenimiento.AtencionUsuarios" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>
<%@ Register TagPrefix="wuc" TagName="Mantenimiento" Src="~/Incluir/ControlesWeb/ControlBuscarMantenimiento.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">

<script language="javascript" type="text/javascript">

    function ImprimirDocumento(MantenimientoId) {
          window.open("/Ventana/ImprimirAtencionUsuario.aspx?MantenimientoId=" + MantenimientoId, "ImprimirDocumento", " resizable=yes,scrollbars=1");

      }
  
   </script>

</asp:Content>
<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
   <div class="DivMenuContenido">
      <wuc:Menu ID="ControlMenu" SeccionMenu="Mantenimiento" runat="server" />
   </div>
   
   <div class="DivContenido">
      <div class="DivContenidoTitulo">
         <div class="DivTitulo">Atención usuarios</div>
      </div>
      
      <div class="DivInformacionContenido">
         <asp:UpdatePanel ID="ActualizarTablaAtencionUsuario" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            
               <asp:Panel CssClass="Superposicion" ID="pnlFondo" runat="server" Visible="False"></asp:Panel>
                
               <wuc:Mantenimiento ID="ControlBuscarMantenimiento" runat="server" />
               
               <asp:Panel ID="PanelTablaAtencionUsuario" runat="server">
                  <br />
                  <div class="DivSubtituloPagina">
                     Datos generales
                  </div>
                  <br />
                  <asp:Panel CssClass="DivCampo" id="PanelDatosGenerales" runat="server">
                     <table class="TablaFormulario">
                        <tr>
                           <td>
                              <asp:RadioButton ID="RadioEmpleado" Text=" Empleado" OnCheckedChanged="RadioEmpleado_CheckedChanged" AutoPostBack="true" runat="server" Checked="true" GroupName="Asignacion" />&nbsp;&nbsp;&nbsp;
                              <asp:RadioButton ID="RadioArea" Text=" Area" OnCheckedChanged="RadioArea_CheckedChanged" AutoPostBack="true" runat="server" GroupName="Asignacion" />&nbsp;&nbsp;&nbsp;
                              <asp:DropDownList CssClass="ComboGrande" ID="ComboAsignacion" OnSelectedIndexChanged="ComboAsignacion_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                              
                           </td>
                           <td style="width:350px;">
                              <asp:Panel ID="PanelBuscarFolio" runat="server" DefaultButton="LinkBuscarFolio" Width="300px">
                                 Folio&nbsp;
                                 <asp:TextBox CssClass="CajaTextoMediana" ID="MantenimientoFolio" OnTextChanged="MantenimientoFolio_TextChanged" AutoPostBack="true" MaxLength="50" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;
                                 <asp:ImageButton ImageUrl="/Imagen/Icono/ImagenBuscar.gif" ID="BotonBuscarMantenimiento" OnClick="BotonBuscarMantenimiento_Click" runat="server" />
                                 <asp:LinkButton ID="LinkBuscarFolio" OnClick="LinkBuscarFolio_Click" ValidationGroup="BuscarDocumento" runat="server" Text="" Width="0px"></asp:LinkButton>
                              </asp:Panel> 
                           </td>
                        </tr>
                        <tr>
                           <td colspan="2">
                              <asp:Label CssClass="TextoError" ID="EtiquetaErrorDatosGenerales" runat="server" Text=""></asp:Label>
                           </td>
                        </tr>
                     </table> 
                  </asp:Panel>
                  <br />
                  <div class="DivSubtituloPagina">
                     Activos asignados
                  </div>
                  
                  <asp:Panel CssClass="DivCampo" id="PanelSeleccionarActivos" runat="server">
                     <table class="TablaFormulario">
                        <tr>
                           <td class="Nombre">Código de Barras</td>
                           <td class="Espacio">&nbsp;</td>
                           <td class="Campo">
                              <asp:Panel ID="PanelBuscarCodigoBarras" runat="server" DefaultButton="CodigoBarrasImagen">
                                 <asp:TextBox ID="CodigoBarrasParticular" CssClass="CajaTextoMediana" MaxLength="15" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                 <asp:ImageButton ID="CodigoBarrasImagen" OnClick="CodigoBarrasImagen_Click" ValidationGroup="BuscarCodigoBarras" ImageUrl="/Imagen/Icono/IconoCodigoBarras.jpg" runat="server" />
                              </asp:Panel>
                           </td>
                        </tr>
                        <tr>
                           <td colspan="3">
                              <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="CodigoBarrasParticular" Display="Dynamic" ErrorMessage="" ID="BuscarCodigoBarrasRequerido" SetFocusOnError="true" ValidationGroup="BuscarCodigoBarras" runat="server"></asp:RequiredFieldValidator>
                              <asp:Label CssClass="TextoError" ID="EtiquetaMensajeCBError" runat="server" Text=""></asp:Label>
                           </td>
                        </tr>
                     </table> 
                  </asp:Panel> 
                  
                  <div class="DivTabla" style="overflow:auto; max-height:250px;">
                      <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0"
                          CssClass="TablaInformacion" DataKeyNames="ActivoId, CodigoBarrasParticular" ID="TablaActivosAsignados" runat="server">
                          <EmptyDataTemplate>
                              <table class="TablaVacia">
                                  <tr class="Encabezado">
                                      <th style="width: 40px;">Atender</th>
                                      <th>Descripción</th>
                                      <th style="width: 180px;">Número de serie</th>
                                      <th style="width: 180px;">Modelo</th>
                                      <th style="width: 180px;">Color</th>
                                      <th style="width: 180px;">C.B.</th>
                                  </tr>
                                  <tr>
                                      <td colspan="6" style="text-align: center;">Favor de agregar los activos</td>
                                  </tr>
                              </table>
                          </EmptyDataTemplate>
                          <HeaderStyle CssClass="Encabezado" />
                          <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                          <Columns>
                              <asp:TemplateField HeaderText="Atender">
                                  <ItemTemplate>
                                      <asp:CheckBox ID="SeleccionarAtender" runat="server" />
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" Width="40px" />
                              </asp:TemplateField>
                              <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Left" />
                              </asp:BoundField>
                              <asp:BoundField DataField="NumeroSerie" HeaderText="Número de serie" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="180px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Center">
                                  <HeaderStyle HorizontalAlign="Center" Width="180px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Color" HeaderText="Color" ItemStyle-HorizontalAlign="Center">
                                  <HeaderStyle HorizontalAlign="Center" Width="180px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="C.B." ItemStyle-HorizontalAlign="Center">
                                  <HeaderStyle HorizontalAlign="Center" Width="180px" />
                              </asp:BoundField>
                          </Columns>
                      </asp:GridView>
                  </div>
                  
                  <div class="DivSubtituloPagina">
                     Descripción del problema
                  </div>
                  <br />
                  <asp:Panel CssClass="DivCampo" id="PanelDescripcionProblema" runat="server">
                     <table class="TablaFormulario">
                        <tr>
                           <td rowspan="2">
                              <asp:TextBox ID="Descripcion" CssClass="CajaTextoGrande" runat="server" TextMode="MultiLine"></asp:TextBox>
                           </td>
                           <td class="Espacio">&nbsp;</td>
                           <td>Asistencia</td>
                           <td class="Espacio">&nbsp;</td>
                           <td>Mantenimiento</td>
                           <td class="Espacio">&nbsp;</td>
                           <td>Estatus</td>
                           <td class="Espacio">&nbsp;</td>
                           <td rowspan="2">
                              <asp:ImageButton AlternateText="Agregar" ID="BotonAgregarActivo" OnClick="BotonAgregarActivo_Click" ValidationGroup="AgregarActivo" ImageUrl="/Imagen/Boton/BotonAgregar.png" runat="server" />
                           </td>
                        </tr>
                        <tr>
                           <td class="Espacio">&nbsp;</td>
                           <td>
                              <asp:DropDownList CssClass="ComboPequeño" ID="TipoAsistenciaId" runat="server" ></asp:DropDownList>
                           </td>
                           <td class="Espacio">&nbsp;</td>
                           <td>
                              <asp:DropDownList CssClass="ComboPequeño" ID="TipoMantenimientoId" runat="server" ></asp:DropDownList>
                           </td>
                           <td class="Espacio">&nbsp;</td>
                           <td>
                              <asp:DropDownList CssClass="ComboPequeño" ID="EstatusId" runat="server" ></asp:DropDownList>
                           </td>
                           <td class="Espacio">&nbsp;</td>
                        </tr>
                        <tr>
                           <td colspan="9">
                              <asp:RequiredFieldValidator CssClass="TextoError" ControlToValidate="Descripcion" Display="Dynamic" ErrorMessage="" ID="DescripcionRequerido" SetFocusOnError="true" ValidationGroup="AgregarActivo" runat="server"></asp:RequiredFieldValidator>
                              <asp:Label CssClass="TextoError" ID="EtiquetaAgregarActivoError" runat="server" Text=""></asp:Label>
                           </td>
                        </tr>
                     </table> 
                  </asp:Panel>
                  <br />
                  <div class="DivSubtituloPagina">
                     Activos Agregados
                  </div>
                  
                  <div class="DivTabla" style="overflow:auto; max-height:250px;">
                      <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnRowCommand="TablaActivosAgregados_RowCommand"
                          CssClass="TablaInformacion" DataKeyNames="ActivoId, TemporalMantenimientoActivoId" ID="TablaActivosAgregados" runat="server">
                          <EmptyDataTemplate>
                              <table class="TablaVacia">
                                  <tr class="Encabezado">
                                      <th>Descripción</th>
                                      <th style="width: 180px;">Número de serie</th>
                                      <th style="width: 180px;">Modelo</th>
                                      <th style="width: 180px;">Color</th>
                                      <th style="width: 180px;">C.B.</th>
                                      <th style="width: 25px;"></th>
                                  </tr>
                                  <tr>
                                      <td colspan="6" style="text-align: center;">Favor de agregar los activos</td>
                                  </tr>
                              </table>
                          </EmptyDataTemplate>
                          <HeaderStyle CssClass="Encabezado" />
                          <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                          <Columns>
                              <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Left" />
                              </asp:BoundField>
                              <asp:BoundField DataField="NumeroSerie" HeaderText="Número de serie" ItemStyle-HorizontalAlign="Left">
                                  <HeaderStyle HorizontalAlign="Center" Width="180px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Center">
                                  <HeaderStyle HorizontalAlign="Center" Width="180px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Color" HeaderText="Color" ItemStyle-HorizontalAlign="Center">
                                  <HeaderStyle HorizontalAlign="Center" Width="180px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="CodigoBarrasParticular" HeaderText="C.B." ItemStyle-HorizontalAlign="Center">
                                  <HeaderStyle HorizontalAlign="Center" Width="180px" />
                              </asp:BoundField>
                              <asp:TemplateField HeaderText="">
                                  <ItemTemplate>
                                      <asp:ImageButton ID="BotonEliminarActivo" CommandArgument="<%#Container.DataItemIndex%>" CommandName="EliminarActivo" runat="server" ImageUrl="/Imagen/Icono/IconoEliminarRegistro.gif" />
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" Width="25px" />
                              </asp:TemplateField>
                          </Columns>
                      </asp:GridView>
                  </div>
                  
                  <div class="DivSubtituloPagina">
                     Tecnico(s) que atienden
                  </div>
                  <br />
                  <div>
                     <div style="width:40%; float:left;">
                        <asp:Panel CssClass="DivCampo" id="Panel1" runat="server">
                           <table class="TablaFormulario">
                              <tr>
                                 <td>
                                    <asp:DropDownList CssClass="ComboGrande" ID="ComboEmpleadoAtiende" runat="server" ></asp:DropDownList>&nbsp;
                                    <asp:ImageButton AlternateText="Agregar" ID="BotonAgregarEmpleado" OnClick="BotonAgregarEmpleado_Click" ValidationGroup="AgregarEmpleado" ImageUrl="/Imagen/Boton/BotonAgregar.png" runat="server" />
                                 </td>
                              </tr>
                              <tr>
                                 <td>
                                    <asp:CompareValidator CssClass="TextoError" ControlToValidate="ComboEmpleadoAtiende" Display="Dynamic" ErrorMessage="" ID="EmpleadoAtiendeRequerido" SetFocusOnError="true" Operator="GreaterThan" ValidationGroup="AgregarEmpleado" ValueToCompare="0" runat="server"></asp:CompareValidator>
                                 </td>
                              </tr>
                           </table> 
                        </asp:Panel> 
                     </div>
                     <div style="width:60%; float:left;">
                        
                        <div class="DivTabla" style="overflow:auto; max-height:150px;">
                            <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" OnRowCommand="TablaEmpleados_RowCommand"
                                CssClass="TablaInformacion" DataKeyNames="EmpleadoId" ID="TablaEmpleados" runat="server">
                                <EmptyDataTemplate>
                                    <table class="TablaVacia">
                                        <tr class="Encabezado">
                                            <th style="width: 180px;">Número</th>
                                            <th>Nombre</th>
                                            <th style="width: 25px;"></th>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: center;">Favor de agregar los empleados</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="Encabezado" />
                                <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                                <Columns>
                                    <asp:BoundField DataField="NumeroEmpleado" HeaderText="Número" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpleadoNombreCompleto" HeaderText="Nombre" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="BotonEliminarEmpleado" CommandArgument="<%#Container.DataItemIndex%>" CommandName="EliminarEmpleado" runat="server" ImageUrl="/Imagen/Icono/IconoEliminarRegistro.gif" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="25px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        
                     </div>
                  </div>
                  <br />
                  <div>
                     <table width="100%">
                        <tr>
                           <td>
                              <asp:Label CssClass="TextoError" ID="EtiquetaMensajeError" runat="server" Text=""></asp:Label>
                              <asp:Label CssClass="TextoInformacion" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
                              <br />
                              <asp:ImageButton AlternateText="Guardar" ID="BotonGuardar" OnClick="BotonGuardar_Click" ImageUrl="/Imagen/Boton/BotonGuardar.png" runat="server" />&nbsp;&nbsp;
                              <asp:ImageButton AlternateText="Imprimir" ID="BotonImprimir" OnClick="BotonImprimir_Click" Enabled="false" ImageUrl="/Imagen/Boton/BotonImprimir.png" runat="server" />&nbsp;&nbsp;
                              <asp:ImageButton AlternateText="Limpiar" ID="BotonLimpiar" OnClick="BotonLimpiar_Click"  ImageUrl="/Imagen/Boton/BotonLimpiar.png" runat="server" />
                           </td>
                        </tr>
                     </table> 
                  </div>
                  <br /><br /><br /> 
                  <asp:HiddenField ID="MantenimientoIdHidden" runat="server" Value="0" />
               </asp:Panel> 
               
               <asp:UpdateProgress AssociatedUpdatePanelID="ActualizarTablaAtencionUsuario" ID="ProgresoTablaAtencionUsuario" runat="server">
                  <ProgressTemplate>
                      <div class="DivCargando"><div class="DivCargandoImagen"><img alt="Cargando..." src="/Imagen/Icono/IconoCargando.gif" /></div></div>
                  </ProgressTemplate>
               </asp:UpdateProgress>
            </ContentTemplate>

            <Triggers>
              
            </Triggers>
         </asp:UpdatePanel> 
      </div> 
   </div> 
</asp:Content>
