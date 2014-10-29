<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlMenu.ascx.cs" Inherits="Activos.Web.Incluir.ControlesWeb.ControlMenu" %>

<div id="dockContainer">
	<ul id="jqDock">
		<li><a class="dockItem" href="/Aplicacion/Inicio.aspx"><img src="/Imagen/Icono/IconoMenuInicio.png" alt="Inicio" title="Inicio" /></a></li>
		<li><a id="OpcionDepartamento" runat="server" visible="false" class="dockItem" href="/Aplicacion/Catalogo/Departamento.aspx"><img src="/Imagen/Icono/Departamento.png" alt="Departamento" title="Departamento" /></a></li>
		<li><a id="OpcionDireccion" runat="server" visible="false" class="dockItem" href="/Aplicacion/Catalogo/Direccion.aspx"><img src="/Imagen/Icono/Direccion.png" alt="Dirección" title="Dirección" /></a></li>
		<li><a id="OpcionEdificio" runat="server" visible="false" class="dockItem" href="/Aplicacion/Catalogo/Edificio.aspx"><img src="/Imagen/Icono/Edificios.png" alt="Edificios" title="Edificios" /></a></li>
		<li><a id="OpcionEmpleado" runat="server" visible="false" class="dockItem" href="/Aplicacion/Catalogo/Empleado.aspx"><img src="/Imagen/Icono/Empleados.png" alt="Empleados" title="Empleados" /></a></li>
		<li><a id="OpcionFamilia" runat="server" visible="false" class="dockItem" href="/Aplicacion/Catalogo/Familia.aspx"><img src="/Imagen/Icono/Familias.png" alt="Familias" title="Familias" /></a></li>
		<li><a id="OpcionJefe" runat="server" visible="false" class="dockItem" href="/Aplicacion/Catalogo/Jefe.aspx"><img src="/Imagen/Icono/Jefes.png" alt="Jefes" title="Jefes" /></a></li>
		<li><a id="OpcionMarca" runat="server" visible="false" class="dockItem" href="/Aplicacion/Catalogo/Marca.aspx"><img src="/Imagen/Icono/Marca.png" alt="Marcas" title="Marcas" /></a></li>
		<li><a id="OpcionProveedor" runat="server" visible="false" class="dockItem" href="/Aplicacion/Catalogo/Proveedor.aspx"><img src="/Imagen/Icono/Proveedor.png" alt="Proveedor" title="Proveedor" /></a></li>
		<li><a id="OpcionPuesto" runat="server" visible="false" class="dockItem" href="/Aplicacion/Catalogo/Puesto.aspx"><img src="/Imagen/Icono/Puestos.png" alt="Puestos" title="Puestos" /></a></li>
		<li><a id="OpcionSubfamilia" runat="server" visible="false" class="dockItem" href="/Aplicacion/Catalogo/SubFamilia.aspx"><img src="/Imagen/Icono/Subfamilias.png" alt="Subfamilia" title="Subfamilia" /></a></li>
		<li><a id="OpcionUsuario" runat="server" visible="false" class="dockItem" href="/Aplicacion/Catalogo/Usuario.aspx"><img src="/Imagen/Icono/Usuarios.png" alt="Usuarios" title="Usuarios" /></a></li>
		
		<li><a id="OpcionRecepcion" runat="server" visible="false" class="dockItem" href="/Aplicacion/Activo/RecepcionActivo.aspx"><img src="/Imagen/Icono/Recepcion.png" alt="Recepción" title="Recepción" /></a></li>
		<li><a id="OpcionHistorial" runat="server" visible="false" class="dockItem" href="/Aplicacion/Activo/HistorialRecepcionActivo.aspx"><img src="/Imagen/Icono/Historial.png" alt="Historial" title="Historial" /></a></li>
		<li><a id="OpcionEtiquetado" runat="server" visible="false" class="dockItem" href="/Aplicacion/Activo/EtiquetadoActivo.aspx"><img src="/Imagen/Icono/Etiquetado.png" alt="Etiquetado" title="Etiquetado" /></a></li>
		<li><a id="OpcionAsignacion" runat="server" visible="false" class="dockItem" href="/Aplicacion/Activo/AsignacionActivo.aspx"><img src="/Imagen/Icono/Asignacion.png" alt="Asignación" title="Asignación" /></a></li>
		<li><a id="OpcionEntradaSalida" runat="server" visible="false" class="dockItem" href="/Aplicacion/Activo/EntradasSalidas.aspx"><img src="/Imagen/Icono/EntradasSalidas.png" alt="Ent_Sal" title="Ent_Sal" /></a></li>
		<li><a id="OpcionBaja" runat="server" visible="false" class="dockItem" href="/Aplicacion/Activo/BajaActivo.aspx"><img src="/Imagen/Icono/Bajas.png" alt="Bajas" title="Bajas" /></a></li>
		<li><a id="OpcionAsignacionGeneral" runat="server" visible="false" class="dockItem" href="/Aplicacion/Activo/AsignacionGeneralActivo.aspx"><img src="/Imagen/Icono/AsignacionGeneral.png" alt="Asig_Gral" title="Asig_Gral" /></a></li>
		<li><a id="OpcionLevantamiento" runat="server" visible="false" class="dockItem" href="/Aplicacion/Activo/LevantamientoActivo.aspx"><img src="/Imagen/Icono/Levantamiento.png" alt="Levantamiento" title="Levantamiento" /></a></li>
		<li><a id="OpcionTransferenciaActivo" runat="server" visible="false" class="dockItem" href="/Aplicacion/Activo/TransferenciaActivo.aspx"><img src="/Imagen/Icono/TransferenciaActivos.png" alt="Tran_Activo" title="Tran_Activo" /></a></li>
		<li><a id="OpcionTransferenciaAccesorio" runat="server" visible="false" class="dockItem" href="/Aplicacion/Activo/TransferenciaAccesorio.aspx"><img src="/Imagen/Icono/TransferenciaAccesorios.png" alt="Tran_Acc" title="Tran_Acc" /></a></li>
		
		<li><a id="OpcionAtencionUsuarios" runat="server" visible="false" class="dockItem" href="/Aplicacion/Mantenimiento/AtencionUsuarios.aspx"><img src="/Imagen/Icono/AtencionUsuarios.png" alt="Atención" title="Atención" /></a></li>
		
		<li><a id="OpcionReporteGeneralActivo" runat="server" visible="false" class="dockItem" href="/Aplicacion/Reportes/ReporteGeneralActivo.aspx"><img src="/Imagen/Icono/ReporteGeneralActivo.png" alt="Gral_Activo" title="Gral_Activo" /></a></li>
		<li><a id="OpcionReporteEstatusActivo" runat="server" visible="false" class="dockItem" href="/Aplicacion/Reportes/ReporteEstatusActivo.aspx"><img src="/Imagen/Icono/ReporteEstatusActivo.png" alt="Estatus_Activo" title="Estatus_Activo" /></a></li>
		<li><a id="OpcionReporteRastreoActivo" runat="server" visible="false" class="dockItem" href="/Aplicacion/Reportes/ReporteRastreoActivo.aspx"><img src="/Imagen/Icono/ReporteRastreoActivo.png" alt="Rasto_Activo" title="Rast_Activo" /></a></li>
		<li><a id="OpcionReporteRastreoAccesorio" runat="server" visible="false" class="dockItem" href="/Aplicacion/Reportes/ReporteRastreoAccesorio.aspx"><img src="/Imagen/Icono/ReporteRastreoAccesorio.png" alt="Rast_Acc" title="Rast_Acc" /></a></li>
		<li><a id="OpcionReporteActivosPorEmpleado" runat="server" visible="false" class="dockItem" href="/Aplicacion/Reportes/ReporteActivosPorEmpleado.aspx"><img src="/Imagen/Icono/ReporteActivoEmpleado.png" alt="Activo_Emp" title="Activo_Emp" /></a></li>
		<li><a id="OpcionReporteGeneralMantenimientos" runat="server" visible="false" class="dockItem" href="/Aplicacion/Reportes/ReporteGeneralMantenimientos.aspx"><img src="/Imagen/Icono/ReporteGeneralMantenimiento.png" alt="Gral_Mtto" title="Gral_Mtto" /></a></li>
		<li><a id="OpcionReporteMantenimientosPorTecnico" runat="server" visible="false" class="dockItem" href="/Aplicacion/Reportes/ReporteMantenimientosPorTecnico.aspx"><img src="/Imagen/Icono/ReporteMantenimientoTecnico.png" alt="Mtto_Técnico" title="Mtto_Técnico" /></a></li>
		<li><a id="OpcionReporteMantenimientosPorActivo" runat="server" visible="false" class="dockItem" href="/Aplicacion/Reportes/ReporteMantenimientosPorActivo.aspx"><img src="/Imagen/Icono/ReporteMantenimientoActivo.png" alt="Mtto_Activo" title="Mtto_Activo" /></a></li>
		
		<li><a id="OpcionCambiarContrasenia" runat="server" visible="false" class="dockItem" href="/Aplicacion/Configuracion/CambiarContrasenia.aspx"><img src="/Imagen/Icono/CambiarContrasenia.png" alt="Contraseña" title="Contraseña" /></a></li>
    </ul>
</div>


