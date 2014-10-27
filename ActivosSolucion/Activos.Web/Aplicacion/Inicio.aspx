<%@ Page Language="C#" MasterPageFile="~/Incluir/Plantilla/PlantillaPrivada.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Activos.Web.Aplicacion.Inicio" Title="" %>

<%@ Register TagPrefix="wuc" TagName="Menu" Src="~/Incluir/ControlesWeb/ControlMenu.ascx" %>

<asp:Content ID="ContenidoEncabezado" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    
</asp:Content>

<asp:Content ID="ContenidoCuerpo" ContentPlaceHolderID="ContenedorCuerpo" runat="server">
    <div class="DivMenuContenido">
        <wuc:Menu ID="ControlMenu" SeccionMenu="MenuInicio" runat="server" />
    </div>

    <div class="DivContenido">
        <div class="DivContenidoTitulo">
            <div class="DivTitulo">Pantalla de inicio</div>
        </div>

        <div class="DivInformacionContenido">
            <table>
                <tr>
                    <td>
                        <div id="DivSeccionCatalogos" runat="server" class="DivContenidoOpcionesMenu">
                            <table class="TablaOpciones">
                                <tr>
                                    <td class="Titulo" colspan="3">
                                        <img alt="Catálogos" src="/Imagen/Icono/IconoMenuCatalogo.png" />&nbsp;
                                        Catálogos
                                    </td>
                                </tr>
                            </table>
                            <table class="TablaOpciones">
                               <tr>
                                  <td>
                                     <div id="DivEdificios" runat="server" class="OpcionesMenuDesplazableTercia">
                                          <a href="/Aplicacion/Catalogo/Edificio.aspx">Edificios</a>
                                     </div>
                                     <div id="DivSubfamilias" runat="server" class="OpcionesMenuDesplazableTercia">
                                          <a href="/Aplicacion/Catalogo/SubFamilia.aspx">Subfamilias</a>
                                     </div> 
                                     <div id="DivUsuarios" runat="server" class="OpcionesMenuDesplazableTercia">
                                          <a href="/Aplicacion/Catalogo/Usuario.aspx">Usuarios</a>
                                     </div> 
                                     <div id="DivEmpleados" runat="server" class="OpcionesMenuDesplazableTercia">
                                          <a href="/Aplicacion/Catalogo/Empleado.aspx">Empleados</a>
                                     </div>
                                     <div id="DivMarcas" runat="server" class="OpcionesMenuDesplazableTercia">
                                          <a href="/Aplicacion/Catalogo/Marca.aspx">Marcas</a>
                                     </div> 
                                     <div id="DivDepartamentos" runat="server" class="OpcionesMenuDesplazableTercia">
                                          <a href="/Aplicacion/Catalogo/Departamento.aspx">Departamentos</a>
                                     </div>
                                     <div id="DivJefes" runat="server" class="OpcionesMenuDesplazableTercia">
                                          <a href="/Aplicacion/Catalogo/Jefe.aspx">Jefes</a>
                                     </div> 
                                     <div id="DivDireccion" runat="server" class="OpcionesMenuDesplazableTercia">
                                          <a href="/Aplicacion/Catalogo/Direccion.aspx">Dirección</a>
                                     </div> 
                                     <div id="DivProveedores" runat="server" class="OpcionesMenuDesplazableTercia">
                                          <a href="/Aplicacion/Catalogo/Proveedor.aspx">Proveedores</a>
                                     </div>
                                     <div id="DivFamilias" runat="server" class="OpcionesMenuDesplazableTercia">
                                          <a href="/Aplicacion/Catalogo/Familia.aspx">Familias</a>
                                     </div> 
                                     <div id="DivPuestos" runat="server" class="OpcionesMenuDesplazableTercia">
                                          <a href="/Aplicacion/Catalogo/Puesto.aspx">Puestos</a>
                                     </div>
                                  </td>
                               </tr> 
                            </table> 
                        </div>
                        
                        <div id="DivSeccionActivo" runat="server" class="DivContenidoOpcionesMenu">
                            <table class="TablaOpciones">
                                <tr>
                                    <td class="Titulo" colspan="3">
                                        <img alt="Activo fijo" src="/Imagen/Icono/IconoMenuActivo.png" />&nbsp;
                                        Activo fijo
                                    </td>
                                </tr>
                            </table>
                            <table class="TablaOpciones">
                              <tr>
                                 <td>
                                     <div id="DivRecepcion" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Activo/RecepcionActivo.aspx">Recepción de activos</a>
                                     </div>
                                     <div id="DivEtiquetado" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Activo/EtiquetadoActivo.aspx">Etiquetado de Activos</a>
                                     </div> 
                                     <div id="DivAsignacion" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Activo/AsignacionActivo.aspx">Asignación de activos</a>
                                     </div>
                                     <div id="DivAsignacionGeneral" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Activo/AsignacionGeneralActivo.aspx">Asignación General</a>
                                     </div> 
                                     <div id="DivLevantamiento" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Activo/LevantamientoActivo.aspx">Levantamiento de inventario</a>
                                     </div>
                                     <div id="DivBaja" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Activo/BajaActivo.aspx">Baja de activos</a>
                                     </div> 
                                     <div id="DivHistorial" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Activo/HistorialRecepcionActivo.aspx">Historial de activos</a>
                                     </div>
                                     <div id="DivEntradasSalidas" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Activo/EntradasSalidas.aspx">Entradas y salidas</a>
                                     </div> 
                                     <div id="DivTransferenciaActivos" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Activo/TransferenciaActivo.aspx">Transferencia de activos</a>
                                     </div> 
                                     <div id="DivTransferenciaAccesorios" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Activo/TransferenciaAccesorio.aspx">Transferencia de accesorios</a>
                                     </div>
                                 </td>
                              </tr>
                            </table> 
                        </div>
                    
                        <div id="DivSeccionMantenimientos" runat="server" class="DivContenidoOpcionesMenu">
                            <table class="TablaOpciones">
                                <tr>
                                    <td class="Titulo" colspan="3">
                                        <img src="/Imagen/Icono/IconoMenuMantenimiento.png" />&nbsp;
                                        Mantenimientos
                                    </td>
                                </tr>
                            </table>
                            <table class="TablaOpciones">
                              <tr>
                                 <td>
                                    <div id="DivAtencionUsuarios" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Mantenimiento/AtencionUsuarios.aspx">Atención a usuarios</a>
                                    </div>
                                 </td>
                              </tr>
                            </table> 
                        </div>
                    
                        <div id="DivSeccionReportes" runat="server" class="DivContenidoOpcionesMenu">
                            <table class="TablaOpciones">
                                <tr>
                                    <td class="Titulo" colspan="3">
                                        <img alt="Reportes" src="/Imagen/Icono/IconoMenuReporte.png" />&nbsp;
                                        Reportes
                                    </td>
                                </tr>
                            </table>
                            <table class="TablaOpciones">
                              <tr>
                                 <td>
                                    <div id="DivReporteGeneralActivo" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Reportes/ReporteGeneralActivo.aspx">General Activos</a>
                                    </div>
                                    <div id="DivReporteEstatusActivo" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Reportes/ReporteEstatusActivo.aspx">Estatus de Activos</a>
                                    </div>
                                    <div id="DivReporteRastreoActivo" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Reportes/ReporteRastreoActivo.aspx">Rastreo de activos</a>
                                    </div>
                                    <div id="DivReporteRastreoAccesorio" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Reportes/ReporteRastreoAccesorio.aspx">Rastreo de accesorios</a>
                                    </div>
                                    <div id="DivReporteActivosPorEmpleado" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Reportes/ReporteActivosPorEmpleado.aspx">Activos por empleado</a>
                                    </div>
                                     <div id="DivReporteGeneralMantenimientos" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Reportes/ReporteGeneralMantenimientos.aspx">General de Mantenimientos</a>
                                    </div>
                                    <div id="DivReporteMantenimientosPorTecnico" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Reportes/ReporteMantenimientosPorTecnico.aspx">Mantenimientos por técnico</a>
                                    </div>
                                    <div id="DivReporteMantenimientosPorActivo" runat="server" class="OpcionesMenuDesplazableMedia">
                                          <a href="/Aplicacion/Reportes/ReporteMantenimientosPorActivo.aspx">Mantenimientos por activo</a>
                                    </div>
                                 </td>
                              </tr>
                            </table> 
                        </div>
                    
                        <div id="DivSeccionConfiguracion" runat="server" class="DivContenidoOpcionesMenu">
                            <table class="TablaOpciones">
                                <tr>
                                    <td class="Titulo" colspan="3">
                                        <img alt="Configuración" src="/Imagen/Icono/IconoMenuConfiguracion.png" />&nbsp;
                                        Configuración
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Opcion"><a href="/Aplicacion/Configuracion/CambiarContrasenia.aspx">Cambiar contraseña</a></td>
                                    <td class="Opcion"></td>
                                    <td class="Opcion"></td>
                                </tr>
                                <tr>
                                    <td class="Opcion"></td>
                                    <td class="Opcion"></td>
                                    <td class="Opcion"></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>

            <br /><br /><br />
        </div>
    </div>
</asp:Content>
