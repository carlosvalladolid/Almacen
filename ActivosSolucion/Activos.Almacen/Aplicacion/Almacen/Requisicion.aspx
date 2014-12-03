<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Requisicion.aspx.cs" Inherits="Activos.Almacen.Aplicacion.Almacen.Requisicion" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaPrivada.Master" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>


<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">           
     <script language="javascript" src="/Incluir/Javascript/ValidarFormulario.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="LeftBodyDiv">
        <wuc:ControlMenuIzquierdo ID="ControlMenuIzquierdo" runat="server" />
    </div>

     <div class="RightBodyDiv">     
        <asp:UpdatePanel ID="PageUpdate" runat="server">
            <ContentTemplate>
                <div class="PageTitleDiv">
                    <table class="PageTitleTable">
                        <tr>
                            <td class="Title">
                                Requisición
                            </td>
                            <td class="Search"></td>
                            <td class="Icon"></td> 
                        </tr>
                    </table>
                </div>
             
                <div class="SubTituloDiv">Solicitante</div>
              
              <asp:Panel CssClass="NewRowDiv" ID="PanelNuevoRegistroSolicitante" Visible="true" Enabled="false" runat="server">
    <%--              <asp:Panel ID="PanelBusquedaSolicitante" Visible="true" runat="server"> 
                  
                       <table class="FormTable">
                         <tr>
                            <td class="Nombre">Solicitante</td>
                            <td class="Espacio">*</td>
                            <td class="Campo"> <asp:TextBox ID="SolicitanteBusqueda"  CssClass="CajaTextoGrande" MaxLength="10" runat="server"></asp:TextBox>&nbsp;</td>
                            </tr>
                            
                            <tr>
                            <td>
                               <br />
                                <asp:ImageButton AlternateText="Buscar" ID="BotonBusquedaEmpleado" OnClick="BotonBusquedaEmpleado_Click" ImageUrl="/Imagen/Boton/BotonBuscar.png"  runat="server" />&nbsp;&nbsp;
                                <asp:ImageButton AlternateText="Cancelar" ID="ImageButton2" ImageUrl="/Imagen/Boton/BotonCancelar.png"  runat="server" />
                            </td>
                            </tr>
                      </table>
                    
                      <div id="DivTablaControl"> 
                              <asp:GridView AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" CssClass="TablaInformacion" 
                                  DataKeyNames="EmpleadoId" ID="TablaEmpleado" OnPageIndexChanging="TablaEmpleado_PageIndexChanging"                                   
                                  OnRowCommand="TablaEmpleado_RowCommand" runat="server">
                                  
                                  <EmptyDataTemplate>
                                      <table class="TablaVacia">
                                          <tr class="Encabezado">
                                              <th></th>
                                              <th style="width: 25px;">No.Emp</th>
                                              <th style="width: 25px;">IdJefe</th>
                                              <th style="width: 60px;">Nombre</th>
                                              <th style="width: 60px;">Direccion</th>
                                              <th style="width: 60px;">Puesto</th>
                                             
                                          </tr>
                                          <tr>
                                              <td colspan="6" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                                          </tr>
                                      </table>
                                  </EmptyDataTemplate>
                                  <HeaderStyle CssClass="Encabezado" />
                                  <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                                  <Columns> 
                                  
                                     <asp:TemplateField HeaderText="">
                                     <ItemTemplate>
                                         <asp:ImageButton ID="BotonSeleccionarEmpleado" CommandArgument="<%#Container.DataItemIndex%>" CommandName="SeleccionarEmpleado" runat="server" ImageUrl="/Imagen/Icono/IconoEliminarRegistro.gif" />
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" Width="25px" />
                                     </asp:TemplateField>
                                                                          
                                      <asp:BoundField DataField="EmpleadoId" HeaderText="No.Emp" ItemStyle-HorizontalAlign="left">
                                          <HeaderStyle HorizontalAlign="left" Width="25px" />
                                      </asp:BoundField>
                                      
                                       <asp:BoundField DataField="EmpleadoIdJefe" HeaderText="IdJefe" ItemStyle-HorizontalAlign="left">
                                          <HeaderStyle HorizontalAlign="left" Width="25px" />
                                      </asp:BoundField>
                                      
                                      <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="left">
                                          <HeaderStyle HorizontalAlign="left" Width="60px" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="Direccion" HeaderText="Direccion" ItemStyle-HorizontalAlign="left">
                                          <HeaderStyle HorizontalAlign="left" Width="60px" />
                                      </asp:BoundField>
                                       <asp:BoundField DataField="Puesto" HeaderText="Puesto" ItemStyle-HorizontalAlign="left">
                                          <HeaderStyle HorizontalAlign="left" Width="60px" />
                                      </asp:BoundField>
                                      
                                  </Columns>
                              </asp:GridView>
                              <br />
                            
                      </div>       
                  </asp:Panel>               
             --%> 
              
                  <table class="FormTable">
                     <tr>
                            <td class="Nombre">Solicitante</td>
                            <td class="Espacio">*</td>
                            <td class="Campo"> <asp:TextBox ID="SolicitanteNuevo"  CssClass="CajaTextoGrande" MaxLength="10" runat="server"></asp:TextBox>&nbsp;</td>
                     </tr>
                  
                  
                    <tr>
                        <td class="Nombre">Dependencia</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="DependenciaNuevo" runat="server" Text=""></asp:TextBox></td> 
                     </tr>
              
                    <tr>
                        <td class="Nombre">Dirección</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="DireccionNuevo"  runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    
                    <tr>
                        <td class="Nombre">Puesto</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="PuestoNuevo" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    
                  <tr>
                        <td class="Nombre">Jefe Inmediato</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="JefeInmediatoNuevo" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                </table>
             </asp:Panel>
             
              <div class="SubTituloDiv">Articulos</div>
              
                <asp:Panel CssClass="NewRowDiv" ID="PanelNuevoRegistroDetalle" Visible="true" runat="server">
                 <table class="FormTable">              
                     <tr>
                            <td class="Nombre">Clave</td>
                            <td class="Required">*</td>
                            <td class="Campo">  
                             <asp:TextBox ID="ClaveNuevo" CssClass="CajaTextoMediana" MaxLength="15" OnTextChanged ="LinkBuscarClave_SelectedTextChanged" AutoPostBack="true"  runat="server"></asp:TextBox>     
                             
                            </td>
                        </tr>
              
                         <tr>
                            <td class="Nombre">Familia</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="FamiliaIdNuevo" OnSelectedIndexChanged="ddlFamilia_SelectedIndexChanged" AutoPostBack="true"  Enabled ="false"   runat="server"></asp:DropDownList>                              
                            </td>
                        </tr>
                        <tr>
                            <td class="Nombre">SubFamilia</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="SubFamiliaIdNuevo" Enabled ="false"    runat="server"></asp:DropDownList></td>
                        </tr>
                        
                        <tr>
                            <td class="Nombre">Marca</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="MarcaIdNuevo" Enabled ="false"   runat="server"></asp:DropDownList></td>
                        </tr> 
                        
                         <tr>
                            <td class="Nombre">Descripción</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoGrande" ID="DescripcionNuevo" Enabled ="false"   runat="server" ></asp:TextBox></td>
                        </tr>                    
						 <tr>
                            <td class="Nombre">Cantidad</td>
                            <td class="Espacio"></td>
                            <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="CantidadNuevo"  runat="server" Text=""></asp:TextBox></td>                           
                        </tr>                       
                      
                        
                         <tr>
                            <td colspan="3">
                                <asp:Label CssClass="TextoError" ID="AgregarEtiquetaMensaje" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:ImageButton AlternateText="Guardar" ID="BotonAgregar" ImageUrl="~/Imagen/Boton/BotonAgregar.png" OnClick="BotonAgregar_Click" runat="server"/>&nbsp;&nbsp;
                              </td>
                        </tr>
                </table>
             </asp:Panel>
             
            <asp:Label CssClass="TextoError" ID="EtiquetaMensaje" runat="server" Text=""></asp:Label>
            
             	<div>
                    <asp:GridView AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" BorderWidth="0" 
                        CssClass="TablaInformacion" DataKeyNames="RequisicionId, ProductoId" ID="TablaRequisicion" OnRowCommand="TablaRequisicion_RowCommand" runat="server" PageSize="10">
                        <EmptyDataTemplate>
                            <table class="TablaVacia">
                                <tr class="Encabezado">
                                    <th style="width: 10px;"></th>
                                    <th style="width: 30px;">Clave</th>
                                    <th style="width: 100px;">Descripcion</th>  
                                    <th style="width: 80px;">Familia</th>                                    
                                    <th style="width: 60px;">Marca</th>   
                                    <th style="width: 60px;">Cantidad</th>                                  
                                </tr>
                                <tr>
                                    <td colspan="6" style="text-align: center;">No se encontró información con los parámetros seleccionados</td>
                                </tr>
                            </table>
                      </EmptyDataTemplate>
                      
                        <HeaderStyle CssClass="Encabezado" />
                        <PagerStyle CssClass="Paginacion" HorizontalAlign="Right" />
                        <Columns>
                           <asp:BoundField DataField="Clave" HeaderText="Clave" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>                                                      
                            
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>    
                            
                              <asp:BoundField DataField="Familia" HeaderText="Familia" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>                  
                          
                            <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>                            
                      
                            <asp:TemplateField HeaderText="">
                                     <ItemTemplate>
                                         <asp:ImageButton ID="BotonEliminarRequisicion" CommandArgument="<%#Container.DataItemIndex%>" CommandName="EliminarRequisicion" runat="server" ImageUrl="/Imagen/Icono/IconoEliminarRegistro.gif" />
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" Width="25px" />
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
                </div>
             
             
                <div>
                        <table width="100%">
                           <tr>
                              <td style="width:70%;">                                 
                                  <br />
                                  <asp:ImageButton AlternateText="Guardar" ID="BotonGuardarPreOrden"  ImageUrl="/Imagen/Boton/BotonGuardar.png" OnClick="BotonGuardar_Click" runat="server" ValidationGroup="Save" />&nbsp;&nbsp;
                                  <asp:ImageButton AlternateText="Limpiar" ID="BotonLimpiarRegistro"  ImageUrl="/Imagen/Boton/BotonLimpiar.png" runat="server" />&nbsp;&nbsp;
                                  <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelarPreOrden" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />                                
                              </td>     
                              
                               <td style="width:20%; text-align:right;">
                                 <asp:Label ID="LabelEtiquetaTotal" CssClass="MontoTotal" Text="Total Articulos:" runat="server"></asp:Label>&nbsp;
                                 <asp:Label ID="LabelTotalArticulo" CssClass="MontoTotal" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>                       
                           </tr>
                        </table> 
                     </div>
                <br /><br /><br /> 

             
               <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="../../Image/Icon/LoadingIcon.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
                
                 <asp:HiddenField ID="TemporalRequisicionIdHidden" runat="server" Value="" />
                 <asp:HiddenField ID="ProductoIdHidden" runat="server" Value="" />
                 
                  <asp:HiddenField ID="EmpleadoIdHidden" runat="server" Value="" />
                 <asp:HiddenField ID="JefeIdHidden" runat="server" Value="" />
          </ContentTemplate>
      </asp:UpdatePanel>   
      </div>



</asp:Content>
