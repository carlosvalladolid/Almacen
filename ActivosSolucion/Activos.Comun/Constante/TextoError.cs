using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Comun.Constante
{
    public class TextoError : Base
    {
        // Errores genéricos
        public const string ErrorGenerico = "· Ocurrió un error inesperado";
        public const string ErrorValidacion = "Obligatorio";
        public const string FormatoFechaInvalido = "Formato no válido";

        // Mensajes de error para la pantalla de login
        public const string IdentificarCuentaUsuario = "· El campo <b>Usuario</b> es obligatorio";
        public const string IdentificarContrasenia = "· El campo <b>Contraseña</b> es obligatorio";
        public const string IdentificarUsuarioContraseniaIncorrecta = "· El usuario y/o la contraseña son incorrectos";
        public const string IdentificarUsuarioInactivo = "· El usuario se encuentra temporalmente inactivo";
        public const string IdentificarCorreoInvalido = "· El campo <b>Correo electrónico</b> no es una cuenta de correo válida";

        // Mensajes de error para cambiar la contraseña
        public const string AnteriorContrasenia = "· El campo <b>Contraseña anterior</b> es obligatorio";
        public const string NuevaContrasenia = "· El campo <b>Contraseña nueva</b> es obligatorio";
        public const string Confirmacion = "· <b>Confirmar</b> el cambio de contraseña ";
        public const string ErrorConfirmacion = "· Confirmación incorrecta ";
        public const string ContraseniaIncorrecta = "· Contraseña incorrecta ";
        public const string Validacion = "· Nueva contraseña debe ser diferente a la anterior ";

        // Mensajes de error para el envío de recuperación de contraseña
        public const string RecuperarCorreo = "· El campo <b>Correo electrónico</b> es obligatorio";
        public const string RecuperarCuentaNoValida = "· Proporcione una cuenta de correo válida";
        public const string RecuperarCuentaNoExiste = "· La cuenta proporcionada no existe";

        // Mensajes de error para pantalla de baja de activos
        public const string DependenciaRequerida = "· Campo <b>Dependencia</b> requerido";
        public const string ActivoNoAsignado = "· El activo seleccionado esta actualmente en almacén";
        public const string ActivoDadoDeBaja = "· El activo ya fué dado de baja";
        public const string ActivoNoEncontrado = "· El activo no se encuentra en los registros";
        public const string BajaActivoCorrecta = "· El activo se dió de baja correctamente";
        public const string CampoFechaVacio = "· Campo <b>Fecha</b> requerido";
        public const string OtrosTipoBajaRequerido = "· Agregar <b>descripción</b>";
        public const string TipoBajaNoSeleccionado = "· Seleccionar <b>Tipo de Baja</b >";
        public const string CondicionIdRequerida = "· Campo <b>Condició</b> requerido";
        public const string ActivoYaSeleccionado = "· Activo ya registrado para baja";
        public const string ElementoEliminado = "· Elemento Eliminado";
        public const string ListaVacia = "· <b>Lista vacía</b>";
        public const string EmpleadoDiferente = "· <b>Todos los activos deben ser del mismo empleado</b>";
        public const string ActivoConEstatusSalida = "· <b>No elegible para baja por estatus \"Salida\" </b>";
        public const string ActivoPadre = "· <b>El activo tiene asignado accesorios </b>";
        public const string PermisoDenegado = "· <b>No cuenta con permisos para manejar este activo</b>";

        // Mensajes de error para pantalla de Entradas y salidas
        public const string TipoMovimientoNoSeleccionado = "· Seleccionar <b>Tipo de Movimiento</b >";
        public const string ObservacionesRequerido = "· Agregar <b>observaciones</b>";
        public const string EmpleadoRequerido = "· Seleccionar <b>Empleado</b>";
        public const string ActivoValidoParaSalida = "· <b>Activo válido para salida</b>";
        public const string ActivoValidoParaEntrada = "· <b>Activo válido para entrada</b>";
        public const string ActivoNoValidoParaEntrada = "· <b>Activo NO válido para entrada</b>";
        public const string MovimientoRegistradoCorrectamente = "· <b>El Movimiento fue guardado exitosamente</b>";
        public const string ErrorAlGuardadMovimiento = "· <b>Error Ocurrido al Guardar Movimientos</b>";
        public const string ActivoEsAccresorioAsignado = "· El Activo es un accesorio, se dará de baja";
        public const string ActivoEsPadre = "· <b>El Activo tiene accesorios asignados</b>";
        public const string TipoServicioRequerido = "· Elija el tipo de servicio";
        public const string ProveedorRequerido = "· Seleccione un Proveedor";
        public const string ActivoEsVehiculo = "· Solo se puede hacer salida de un vehiculo por documento";
        public const string ActivoAccesorio = "· El activo seleccionado es un accesorio, también se usará el activo padre";

        // Mensajes de error para el catálogo de usuarios
        public const string UsuarioNombre = "· El campo <b>Nombre(s)</b> es obligatorio";
        public const string UsuarioApellidoPaterno = "· El campo <b>Apellido paterno</b> es obligatorio";
        public const string UsuarioCuenta = "· El campo <b>Cuenta de usuario</b> es obligatorio";
        public const string UsuarioCuentaInvalida = "· La cuenta de correo no tiene un formato válido";
        public const string UsuarioRol = "· El campo <b>Rol de usuario</b> es obligatorio";
        public const string UsuarioEstatus = "· El campo <b>Estatus</b> es obligatorio";
        public const string UsuarioCuentaExiste = "· La cuenta de usuario <b>{0}</b> ya existe";
        public const string UsuarioNoExiste = "· El usuario seleccionado no existe";
        public const string UsuarioCuentaAdministrador = "· No se puede borrar al usuario default administrador del sistema";
        public const string UsuarioQuiereBorrarseAsiMismo = "· No se puede borrar a su propio usuario";
        public const string UsuarioTieneRegistrosRelacionados = "· No se pueden eliminar los usuarios seleccionados debido que uno o varios de ellos tiene registros relacionados con una o varias de las siguientes tablas <b>Departemento</b>, <b>Dirección</b>, <b>Edificio</b>, <b>Empleado</b>, <b>Familia</b>, <b>Subfamilia</b>, <b>Puesto</b>, <b>Proveedor</b>";

        // Mensajes de error para el catálogo de edificios
        public const string EdificioNombre = "· El campo <b>Nombre(s)</b> es obligatorio";
        public const string EdificioEstatus = "· El campo <b>Estatus</b> es obligatorio";
        public const string EdificioEstado = "· El campo <b>Estado</b> es obligatorio";
        public const string EdificioCiudad = "· El campo <b>Ciudad</b> es obligatorio";
        public const string EdificioConNombreDuplicado = "· Ya existe un edificio con ese nombre";
        public const string EdificioTieneRegistrosRelacionados = "· No se pueden eliminar los edificios seleccionados debido que uno o varios de ellos tiene registros relacionados con la tabla <b>Empleados</b>";

        // Mensajes de error para el catálogo de Direccion
        public const string DireccionNombre = "· El campo <b>Nombre(s)</b> es obligatorio";
        public const string DireccionEstatus = "· El campo <b>Estatus</b> es obligatorio";
        public const string DependenciaNuevo = "· El campo <b>Dependencia</b> es obligatorio";
        public const string DireccionTieneRegistrosRelacionados = "· No se pueden eliminar las direcciones seleccionadas debido que una o varias de ellas tiene registros relacionados con una o varias de las siguientes tablas <b>Departamento</b>, <b>Jefe</b>";
        public const string DireccionConNombreDuplicado = "· Ya existe una dirección con ese nombre";

        // Mensajes de error para el catálogo de Marca
        public const string MarcaNombre = "· El campo <b>Nombre(s)</b> es obligatorio";
        public const string MarcaEstatus = "· El campo <b>Estatus</b> es obligatorio";
        public const string MarcaDependencia = "· El campo <b>Dependencia</b> es obligatorio";
        public const string MarcaConNombreDuplicado = "· Ya existe una marca con ese nombre";
        public const string MarcaTieneRegistrosRelacionados = "· No se pueden eliminar las marcas seleccionadas debido que uno o varias de ellas tiene registros relacionados con la tabla <b>Activo</b>";

        // Mensajes de error para el catálogo de empleados
        public const string EmpleadoNombre = "· El campo <b>Nombre(s)</b> es obligatorio";
        public const string EmpleadoApellidoPaterno = "· El campo <b>Apellido paterno</b> es obligatorio";
        public const string EmpleadoEstado = "· El campo <b>Estado</b> es obligatorio";
        public const string EmpleadoCiudad = "· El campo <b>Ciudad</b> es obligatorio";
        public const string EmpleadoNumeroEmpleado = "· El campo <b>Número de empleado</b> es obligatorio";
        public const string EmpleadoDepartamento = "· El campo <b>Departamento</b> es obligatorio";
        public const string EmpleadoEdificio = "· El campo <b>Edificio</b> es obligatorio";
        public const string EmpleadoPuesto = "· El campo <b>Puesto</b> es obligatorio";
        public const string EmpleadoEstatus = "· El campo <b>Estatus</b> es obligatorio";
        public const string EmpleadoJefe = "· El campo <b>Jefe</b> es obligatorio";
        public const string EmpleadoTieneRegistrosRelacionados = "· No se pueden eliminar los empleados seleccionados debido que uno o varios de ellos tiene registros relacionados con una o varias de las siguientes tablas <b>Asignacion</b>, <b>Compra</b>, <b>Levantamiento</b>, <b>Movimiento</b>, <b>Jefe</b>";
        public const string EmpleadoConNombreDuplicado = "· Ya existe un empleado con ese nombre";
        public const string EmpleadoConNumeroDuplicado = "· Ya existe un empleado con ese número";

        // Mensajes de error para el catálogo de Familia
        public const string FamiliaNombre = "· El campo <b>Nombre(s)</b> es obligatorio";
        public const string FamiliaEstatus = "· El campo <b>Estatus</b> es obligatorio";
        public const string FamiliaDependencia = "· El campo <b>Dependencia</b> es obligatorio";
        public const string FamiliaSubFamilia= "· El campo <b>SubFamilia</b> es obligatorio";
        public const string FamiliaConNombreDuplicado = "· Ya existe una familia con ese nombre";
        public const string FamiliaTieneRegistrosRelacionados = "· No se pueden eliminar las familias seleccionadas debido que uno o varias de ellas tiene registros relacionados con la tabla <b>Subfamilia</b>";

        // Mensajes de error para el catálogo de SubFamilia
        public const string SubFamiliaNombre = "· El campo <b>Nombre(s)</b> es obligatorio";
        public const string SubFamiliaEstatus = "· El campo <b>Estatus</b> es obligatorio";
        public const string SubFamiliaFamilia = "· El campo <b>Familia</b> es obligatorio";
        public const string SubFamiliaConNombreDuplicado = "· Ya existe una subfamilia con ese nombre";
        public const string SubFamiliaTieneRegistrosRelacionados = "· No se pueden eliminar las subfamilias seleccionadas debido que uno o varias de ellas tiene registros relacionados con la tabla <b>Activo</b>";

        // Mensajes de error para el catalogo de puestos
        public const string PuestoTieneRegistrosRelacionados = "· No se pueden eliminar los puestos seleccionados debido que uno o varios de ellos tiene registros relacionados con una o varias de las siguientes tablas <b>Empleado</b>, <b>Jefe</b>";
        public const string PuestoConNombreDuplicado = "· Ya existe un puesto con ese nombre";

        //Mensajes de error para el catálogo de departamentos
        public const string DepartamentoNombre = "· El campo <b>Nombre(s)</b> es obligatorio";
        public const string DepartamentoEstatus = "· El campo <b>Estatus</b> es obligatorio";
        public const string DepartamentoDireccion = "· El campo <b>Direccion</b> es obligatorio";
        public const string DepartamentoConNombreDuplicado = "· Ya existe un departamento con ese nombre";
        public const string DepartamentoTieneRegistrosRelacionados = "· No se pueden eliminar los departamentos seleccionados debido que uno o varios de ellos tiene registros relacionados con una o varias de las siguientes tablas  <b>Empleado</b>, <b>Jefe</b>";

        // Mensajes de error para el catálogo de jefes
        public const string JefeDireccion = "· El campo <b>Direccion(s)</b> es obligatorio";
        public const string JefePuesto = "· El campo <b>Puesto</b> es obligatorio";
        public const string JefeEmpleado = "· El campo <b>Empleado</b> es obligatorio";
        public const string JefeEstatus = "· El campo <b>Estatus</b> es obligatorio";

        // Mensajes de error para la pantalla de Recepción de activo e Historial de activos
        public const string RecepcionProveedor = "· El campo <b>Proveedor</b> es obligatorio";
        public const string RecepcionTipoDocumento = "· El campo <b>Tipo de documento</b> es obligatorio";
        public const string RecepcionFolio = "· El campo <b>Folio</b> es obligatorio";
        public const string RecepcionFechaDocumento = "· El campo <b>Fecha de documento</b> no tiene un formato valido";
        public const string RecepcionMonto = "· El campo <b>Monto</b> es obligatorio";
        public const string RecepcionMontoValido = "· El campo <b>Monto</b> no es valido";
        public const string RecepcionOrdenCompra = "· El campo <b>Orden de compra</b> es obligatorio";
        public const string RecepcionFechaOC = "· El campo <b>Fecha de O.C.</b> no tiene un formato valido";
        public const string RecepcionSolicitante = "· El campo <b>Solicitante</b> es obligatorio";
        public const string RecepcionJefe = "· El campo <b>Jefe inmediato</b> es obligatorio";
        public const string EstatusActivoIncorrecto = "· Ese Activo ya fue asignado, fue dado de baja, o se encuentra fuera.";
        public const string TipoActivoIncorrecto = "· Ese Activo no es del tipo correcto";
        public const string NoExisteActivo = "· Ese Activo no existe";
        public const string NumeroSerieTemporalActivoDuplicado = "· Ya se agregó un Activo con ese número de serie";
        public const string NumeroSerieActivoDuplicado = "· Ya existe un Activo con ese número de serie";

        public const string CodigoBarrasParticularTemporalActivoDuplicado = "· Ya se agregó un Activo con ese código de barras particular";
        public const string CodigoBarrasParticularActivoDuplicado = "· Ya existe un Activo con ese código de barras particular";
        public const string CodigoBarrasGeneralTemporalActivoDuplicado = "· Ya se agregó un Activo con ese código de barras general";
        public const string CodigoBarrasGeneralActivoDuplicado = "· Ya existe un Activo con ese código de barras general";

        public const string RequisicionDocumentoDuplicado = "· Ya se capturó ese documento con anterioridad";
        public const string RecepcionDocumentoDuplicado = "· Ya se capturó ese documento con anterioridad";
        public const string EmpleadoInactivo = "· Ese empleado no esta activo";
        public const string EmpleadoNoEncontrado = "· Empleado no encontrado";

        public const string RecepcionAsignacionEmpleado = "· El campo <b>Número de empleado</b> es obligatorio";
        public const string RecepcionTipoActivo = "· El campo <b>Tipo</b> es obligatorio";
        public const string RecepcionFamilia = "· El campo <b>Familia</b> es obligatorio";
        public const string RecepcionSubFamilia = "· El campo <b>Sub familia</b> es obligatorio";
        public const string RecepcionMarca = "· El campo <b>Marca</b> es obligatorio";
        public const string RecepcionCondicion = "· El campo <b>Condición</b> es obligatorio";
        public const string RecepcionMontoActivo = "· El campo <b>Monto</b> es obligatorio";
        public const string RecepcionMontoActivoValido = "· El campo <b>Monto</b> no es valido";

        public const string RecepcionTipoAccesorio = "· El campo <b>Tipo</b> es obligatorio";
        public const string RecepcionDescripcionAccesorio = "· El campo <b>Descripción</b> es obligatorio";
        public const string RecepcionCodigoBarrasParticular = "· El campo <b>Código de Barras Particular</b> es obligatorio";

        // Mensajes de error para la pantalla de asignacion de activos
        public const string AsignacionCodigoBarrasParticular = "· El campo <b>Código de Barras Particular</b> es obligatorio";
        public const string AsignacionCondicion = "· El campo <b>Condición</b> es obligatorio";
        public const string AsignacionEmpleado = "· El campo <b>Empleado</b> es obligatorio";
        public const string AsignacionTipoActivo = "· En una asignación todos los activos deben ser del mismo tipo o área.";
        public const string AsignacionActivoVehiculo = "· Las asignaciones de vehículos se hacen de forma individual.";

        // Mensajes de error para el catálogo de Proveedor
        public const string ProveedorDependencia = "· El campo <b>Dependencia(s)</b> es obligatorio";
        public const string ProveedorNombre = "· El campo <b>Nombre</b> es obligatorio";
        public const string ProveedorNombreContacto = "· El campo <b>Nombre de Contacto</b> es obligatorio";
        public const string ProveedorEstado = "· El campo <b>Estado</b> es obligatorio";
        public const string ProveedorCiudad = "· El campo <b>Ciudad</b> es obligatorio";
        public const string ProveedorBanco = "· El campo <b>Banco</b> es obligatorio";
        public const string ProveedorConNombreDuplicado = "· Ya existe un proveedor con ese nombre";
        public const string ProveedorTieneRegistrosRelacionados = "· No se pueden eliminar los proveedores seleccionados debido que uno o varios de ellos tiene registros relacionados con la tabla <b>Compra</b>";

        // Mensajes de error para la pantalla de recepción de activos
        public const string AccesorioRepetido = "· Ese accesorio ya fue agregado a la recepción actual";
        public const string AccesorioExistente = "· Ya existe ese accesorio";

        // Mensajes de error para la pantalla de asignacion de activos
        public const string ActivoRepetido = "· Ese activo ya fue agregado a la asignación actual";
        public const string AsignacionTemporalSinActivos = "· Favor de agregar los activos de la asignación";
        public const string ActivoAsignadoBaja = "· Uno o varios activos fueron asignados o dados de baja por otro usuario durante el proceso";

        // Mensajes de error para la pantalla de Levantamiento de activos
        public const string LevantamientoActivoEmpleadoId = "· El campo <b>Empleado</b> es obligatorio";
        public const string ActivoDadoBaja = "· El activo ha sido dado de baja";
        public const string LevantamientoGuardadoCorrectamente = "· El levantamiento ha sido registrado.";

        // Mensajes de error para la pantalla de Etiquetado de activos
        public const string EtiquetadoNoHayCodigosBarra = "· Favor de ingresar los códigos de barra para los activos";
        public const string EtiquetadoCadigoBarrasDuplicado = "· Los códigos de barra no se pueden duplicar. Los códigos marcados con un * ya existen.";
        public const string EtiquetadoNoHayDocumentoSeleccionado = "· Favor de seleccionar el documento";
        public const string EtiquetadoExitoso = "· Los activos han sido etiquetados correctamente";

        public const string EtiquetadoProveedor = "· El campo <b>Proveedor</b> es obligatorio";
        public const string EtiquetadoTipoDocumento = "· El campo <b>Tipo de documento</b> es obligatorio";
        public const string EtiquetadoCompraFolio = "· El campo <b>Folio</b> es obligatorio";
        public const string NoSeEncontroRegistro = "· No se ubico Registro";

        // Mensajes de error para la pantalla de transferencias de accesorios
        public const string ActivoNoVehiculo = "· El activo no es del tipo vehículo";
        public const string ActivoNoPuedeTransferir = "· El activo esta dado de baja, esta en salida o es un accesorio de otro activo";
        public const string NumeroEconomicoObligatorio = "· Favor de ingresar el número económico";
        public const string NumeroSerieObligatorio = "· Favor de ingresar el número de serie";

        // Mensajes de error para la pantalla de asignacion general de activos
        public const string DocumentoProcesado = "· No se pueden asignar los activos de ese documento debido a que ya ha sido procesado.";
        public const string DocumentoNoEtiquetado = "· No se pueden asignar los activos de ese documento debido a que algunos no estan etiquetados.";
        public const string DocumentoConTiposActivosDiferentes = "· No se pueden asignar los activos de ese documento debido a que tiene activos de diferentes tipos.";
        public const string DocumentoConVariosVehiculos = "· Solo se puede asignar un vehículo a la vez.";

        // Mensajes de error para la pantalla de atencion a usuarios
        public const string MantenimientoActivosNoSeleccionados = "· Favor de seleccionar los activos que desea agregar.";
        public const string MantenimientoEmpleadoAgregadoYa = "· Ese empleado ya ha sido agregado.";
        public const string MantenimientoDescripcionObligatorio = "· La descripción del problema es obligatorio.";
        public const string MantenimientoEmpleadoAtiendeObligatorio = "· Favor de seleccionar el empleado que atiende.";

        //Mensajes de error de fecha
        public const string FechaDesdeInvalido = "· El campo <b>Fecha desde</b> no tiene un formato valido";
        public const string FechaHastaInvalido = "· El campo <b>Fecha hasta</b> no tiene un formato valido";

        // Mensajes de error para el catálogo de Producto
        public const string ProductoNombre = "· El campo <b>Nombre(s)</b> es obligatorio";
        public const string ProductoEstatus = "· El campo <b>Estatus</b> es obligatorio";
        public const string ProductoConNombreDuplicado = "· Ya existe un prodcuto con ese nombre";
        public const string ProductoTieneRegistrosRelacionados = "· No se pueden eliminar los Productos seleccionadas debido que uno o varias de ellas tiene registros relacionados con la tabla <b>Almacen</b>";
        public const string PreOrdenDuplicado = "· Ya existe una PreOrden";

        // Mesnajes de error para la pantalla de órdenes de compra
        public const string OrdenConPreOrdenIdVacio = "Se debe proporcionar un identificador de PreOrden";
        public const string OrdenConPreOrdenId = "La preorden proporcionada ya tiene una orden de compra asignada";

        //pantalla de Producto de Almacen
        public const string ClaveProducto = "· El campo <b>Clave</b> es obligatorio";
        public const string FamiliaProducto = "· El campo <b>Familia</b> es obligatorio";
        public const string SubFamiliaProducto = "· El campo <b>SubFamilia</b> es obligatorio";
        public const string MarcaProducto = "· El campo <b>Marca</b> es obligatorio";
        public const string DescripcionProducto = "· El campo <b>Descripcion</b> es obligatorio";
        public const string MinimoProducto = "· El campo <b>Minimo</b> es obligatorio";
        public const string MaximoProducto = "· El campo <b>Maximo</b> es obligatorio";
        public const string UnidaddeMedidaProducto = "· El campo <b>Unidad de Medida</b> es obligatorio";
        public const string MaximoPermitidoProducto = "· El campo <b>MaximoPermitido</b> es obligatorio";

        public enum Error
        {
            Generico = 50000
        }

        public enum Orden
        {
            PreOrdenIdVacio = 1
        }
    }
}
