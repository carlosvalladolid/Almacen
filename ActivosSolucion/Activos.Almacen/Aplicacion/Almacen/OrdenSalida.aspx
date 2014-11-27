<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="OrdenSalida.aspx.cs" Inherits="Almacen.Web.Aplicacion.Almacen.OrdenSalida" Title="" %>

<%@ MasterType VirtualPath="~/Incluir/Plantilla/PlantillaPrivada.Master" %>
<%@ Register TagPrefix="wuc" TagName="ControlMenuIzquierdo" Src="~/Incluir/ControlesWeb/ControlMenuIzquierdo.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    
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
                                Orden de salida
                            </td>
                            <td class="Search"></td>
                            <td class="Icon"></td> 
                        </tr>
                    </table>
                </div>

                <div class="SubTituloDiv">Solicitante</div>

                <table class="TablaFormulario">
                    <tr>
                        <td class="Nombre">Requisición</td>
                        <td class="Espacio"></td>
                        <td class="Campo">
                            <asp:TextBox CssClass="CajaTextoChica" ID="RequisicionBox" MaxLength="10" runat="server" Text="">
                            </asp:TextBox><asp:ImageButton ID="ImagenBuscarPreOrden" ImageUrl="/Imagen/Icono/ImagenBuscar.gif" runat="server" onclick="ImagenBuscarPreOrden_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Nombre">Solicitante</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="SolicitanteBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Dependencia</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="DependenciaBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Dirección</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="DireccionBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Puesto</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="PuestoBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Jefe inmediato</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="JefeBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                </table>

                <br />
                <div class="SubTituloDiv">Artículos</div>

                <table class="TablaFormulario">
                    <tr>
                        <td class="Nombre">Requisición</td>
                        <td class="Espacio"></td>
                        <td class="Campo">
                            <asp:TextBox CssClass="CajaTextoChica" ID="TextBox1" MaxLength="10" runat="server" Text="">
                            </asp:TextBox><asp:ImageButton ID="ImageButton1" ImageUrl="/Imagen/Icono/ImagenBuscar.gif" runat="server" onclick="ImagenBuscarPreOrden_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Nombre">Familia</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="FamiliaCombo" runat="server" ></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Sub familia</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="SubFamiliaCombo" runat="server" ></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Marca</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList CssClass="ComboGrande" ID="MarcaCombo" runat="server" ></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Descripción</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoMediana" ID="DescripcionBox" MaxLength="50" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Nombre">Cantidad</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox CssClass="CajaTextoChica" ID="CantidadBox" MaxLength="10" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td coslpan="3">
                            <br />
                            <asp:Button ID="BotonAgregar" runat="server" Text="Agregar" />
                        </td>
                    </tr>
                </table>

                <div class="DivTabla">
                    
                </div>

                <table class="TablaFormulario">
                    <tr>
                        <td colspan="3">
                            <br />
                            <asp:ImageButton AlternateText="Guardar" ID="BotonGuardar" ImageUrl="/Imagen/Boton/BotonGuardar.png" runat="server" onclick="BotonGuardar_Click" />&nbsp;&nbsp;
                            <asp:ImageButton AlternateText="Cancelar" ID="BotonCancelar" ImageUrl="/Imagen/Boton/BotonCancelar.png" runat="server" />
                        </td>
                    </tr>
                </table>

                <asp:HiddenField ID="OrdenIdHidden" runat="server" Value="" />

                <asp:UpdateProgress AssociatedUpdatePanelID="PageUpdate" ID="AssociatedUpdate" runat="server">
                    <ProgressTemplate>
                        <div class="LoadingDiv"><div class="LoadingImageDiv"><img alt="Cargando..." src="../../Image/Icon/LoadingIcon.gif" /></div></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>   
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
