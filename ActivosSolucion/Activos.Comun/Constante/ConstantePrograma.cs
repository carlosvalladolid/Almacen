using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Comun.Constante
{
    public class ConstantePrograma : Base
    {
        // Nombres de conexión
        public const string DefensoriaDB_Activos = "DefensoriaDB.Activos";
        public const string DefensoriaDB_Catalogo = "DefensoriaDB.Catalogo";
        public const string DefensoriaDB_Seguridad = "DefensoriaDB.Seguridad";
        public const string DefensoriaDB_Almacen = "DefensoriaDB.Almacen";

        // Formatos para fechas
        public const string EspañolFormatoFecha = "dd/mm/aaaa";
        public const string InglesFormatoFecha = "mm/dd/aaaa";
        public const string UniversalFormatoFecha = "aaaa/mm/dd";

        // Nombres de clases para los gridviews de la aplicación
        public const string ClaseTabla = "TablaInformacion";
        public const string ClaseTablaVacia = "TablaVacia";
        public const string ClaseTablaImpresion = "TablaInformacionImpresion";

        // Nombres de clases para divisiones
        public const string ClaseNuevoRegistro = "DivNuevoRegistro";
        public const string ClaseEditarRegistro = "DivEditarRegistro";

        // Textos para mensajes de texto
        public const string ClaseInformacion = "TextoInformacion";
        public const string ClaseError = "TextoError";

        // Textos para los controles combobox
        public const string FiltroSeleccione = "-- Seleccione --";
        public const string FiltroTodos = "-- Todos --";

        // Nombres de cookies
        public const string CookieCuenta = "Cuenta";
        public const string CookieContrasenia = "Contrasenia";
        public const string CookieRecordar = "Recordar";

        // Envío de correos
        public const string AsuntoRecuperacionContrasenia = "Recuperación de contraseña";
        public const string AsuntoUsuarioNuevo = "Usuario nuevo para la aplicación Web de Activos";
        public const string CorreoNuevoUsuario = "NuevoUsuario";
        public const string CorreoRecuperarContrasenia = "RecuperarContrasenia";

        // Alerta de mensajes
        public const string TipoMensajeAlerta = "Mensaje";
        public const string TipoErrorAlerta = "Error";

        // Nombres de comandos
        public const string ComandoAgregar = "Agregar";
        public const string ComandoSeleccionar = "Seleccionar";

        // Mensajes de confirmación para la pantalla de órdenes de compra
        //public const string

        public enum Accesorio
        {
            AccesorioGuardadoCorrectamente = 1,
            HistorialAccesorioGuardadoCorrectamente = 2,
            AccesorioEliminadoCorrectamente = 3
        }

        public enum Activo
        {
            ActivoGuardadoCorrectamente = 1,
            ActivoAsignadoCorrectamente = 2,
            ActivoEtiquetadoCorrectamente = 3
        }

        public enum Asignacion
        {
            AsignacionGuardadoCorrectamente = 1,
            AsignacionExitosa = 2,
            AsignacionTemporalSinActivos = 3,
            ActivoAsignadoBaja = 100
        }

        public enum AplicacionId
        {
            Activos = 1
        }

        public enum BajaActivo
        {
            ValorPorDefecto = 0,
            ActivoNoAsignado = 1,
            ActivoDadoDeBaja = 2,
            ActivoNoEncontrado = 3,
            BajaActivoCorrecta = 4,
            CampoCodigoBarrasVacio = 5,
            CampoFechaVacio = 6,
            OtrosTipoBajaRequerido = 7,
            TipoBajaNoSeleccionado = 8,
            CondicionIdRequerida =9,
            ActivoYaSeleccionado=10,
            ActivoConEstatusSalida=11,
            ActivoEsAccresorioAsignado=12,
            ActivoPadre=18,
            PermisoDenegado = 19
        }

        public enum Compra
        {
            CompraGuardadoCorrectamente = 1,
            DocumentoDuplicado = 2,
            RecepcionGuardadoCorrectamente = 99
        }

        public enum Departamento
        {
            DepartamentoGuardadoCorrectamente = 1,
            EliminacionExitosa = 2,
            DepartamentoTieneRegistrosRelacionados = 3,
            DepartamentoConNombreDuplicado = 4      
        }

        public enum Direccion
        {
            DireccionGuardadoCorrectamente = 1,
            EliminacionExitosa = 2,
            DireccionTieneRegistrosRelacionados = 3,
            DireccionConNombreDuplicado = 4
        }

        public enum Edificio
        {
            EdificioGuardadoCorrectamente = 1,
            EliminacionExitosa = 2,
            EdificioTieneRegistrosRelacionados = 3,
            EdificioConNombreDuplicado = 4
        }

        public enum Empleado
        {
            EmpleadoGuardadoCorrectamente = 1,
            EliminacionExitosa = 2,
            EmpleadoTieneRegistrosRelacionados = 3,
            EmpleadoConNombreDuplicado = 4,
            EmpleadoConNumeroDuplicado = 5
        }

        public enum EntradasSalidas 
        {      
            ActivoNoValidoParaEntrada=13,
            ActivoValidoParaEntrada=14,
            MovimientoCorrecto=15,
            ActivoPadreValidoParaEntrada=16,
            ActivoValidoParaSalida = 17,
            //el 18 continua en BajaActivo
            ActivoEsVehiculo = 20
        }

        public enum EnviarCorreo
        {
            ValorPorDefecto = 0,
            ErrorInesperado = 100
        }

        public enum EstatusActivos
        {
            SinAsignar = 21,
            Asignado = 22,
            Baja = 23
        }

        public enum EstatusDepartamentos
        {
            Activo = 7,
            Inactivo = 8
        }

        public enum EstatusDireccion
        {
            Activo = 5,
            Inactivo = 6
        }

        public enum EstatusEdificio
        {
            Activo = 3,
            Inactivo = 4
        }

        public enum EstatusEmpleados
        {
            Activo = 9,
            Inactivo = 10
        }

        public enum EstatusFamilia
        {
            Activo = 15,
            Inactivo = 16
        }

        public enum EstatusJefes
        {
            Activo = 19,
            Inactivo = 20
        }

        public enum EstatusLevantamiento
        {
            Localizado = 24,
            SinLocalizar = 25,
            LocalizadoYNoAsignado = 26
        }

        public enum EstatusMarca
        {
            Activo = 13,
            Inactivo = 14
        }

        public enum EstatusMantenimientos
        {
            Abierto = 27,
            Cerrado = 28
        }

        public enum EstatusOrden
        {
            SinSurtir = 31
        }

        public enum EstatusPuestos
        {
            Activo = 11,
            Inactivo = 12
        }

        public enum EstatusRequisicion
        {
            Incompleta = 32
        }

        public enum EstatusSubFamilia
        {
            Activo = 17,
            Inactivo = 18
        }

        public enum EstatusTemporalAccesorio
        {
            Eliminado = 0,
            Activo = 1,
            Nuevo = 2
        }

        public enum EstatusUsuario
        {
            Activo = 1,
            Inactivo = 2
        }

        public enum EtiquetadoActivo
        {
            NoHayCodigosBarra = 1,
            CadigoBarrasDuplicado = 2
        }

        public enum Familia
        {
            FamiliaGuardadoCorrectamente = 1,
            EliminacionExitosa = 2,
            FamiliaTieneRegistrosRelacionados = 3,
            FamiliaConNombreDuplicado = 4
        }

        public enum IdentificarUsuario
        {
            ValorPorDefecto = 0,
            UsuarioContraseniaIncorrecta = 1,
            UsuarioInactivo = 2
        }

        public enum Jefe
        {
            JefeGuardadoCorrectamente = 1,
            EliminacionExitosa = 2,
        }

        public enum Lenguaje
        {
            Español = 1,
            Ingles = 2
        }

        public enum Marca
        {
            MarcaGuardadoCorrectamente = 1,
            EliminacionExitosa = 2,
            MarcaTieneRegistrosRelacionados = 3,
            MarcaConNombreDuplicado = 4
        }

        public enum Mantenimiento
        {
            GuardadoCorrectamente = 1
        }

        public enum MantenimientoActivo
        {
            GuardadoCorrectamente = 1
        }

        public enum MantenimientoEmpleado
        {
            GuardadoCorrectamente = 1,
            EliminadoCorrectamente = 2
        }

        public enum TemporalMantenimientoActivo
        {
            GuardadoCorrectamente = 1,
            EliminadoCorrectamente = 2
        }

        public enum TemporalMantenimientoEmpleado
        {
            GuardadoCorrectamente = 1,
            EliminadoCorrectamente = 2
        }

        public enum Movimiento
        {
            MovimientoAltaGuardadoCorrectamente = 1,
            MovimientoAsignacionGuardadoCorrectamente = 2,
            MovimientoAsignacionEditadoCorrectamente = 3
        }

        public enum Paginas
        {
            Edificios = 1,
            Empleados = 2,
            Departamentos = 3,
            Direccion = 4,
            Familias = 5,
            Subfamilias = 6,
            Marcas = 7,
            Jefes = 8,
            Proveedores = 9,
            Puestos = 10,
            Usuarios = 11,
            RecepcionActivos = 12,
            AsignacionActivos = 13,
            BajaActivos = 14,
            TransferenciaActivos = 15,
            EtiquetadoActivos = 16,
            LevantamientoInventario = 17,
            HistorialActivos = 18,
            EntradasSalidas = 19,
            TransferenciaAccesorios = 20,
            AsignacionGeneralActivos = 21,
            AtencionUsuarios = 22,
            ReporteGeneralActivos = 23,
            ReporteEstatusActivos = 24,
            ReporteRastreoActivos = 25,
            ReporteRastreoAccesorios = 26,
            ReporteActivosPorEmpleado = 27,
            ReporteMantenimientosPorTecnico = 28,
            ReporteGeneralMantenimientos = 29,
            ReporteMantenimientosPorActivo = 30
        }

        public enum Puesto
        {
            PuestoGuardadoCorrectamente = 1,
            EliminacionExitosa = 2,
            PuestoTieneRegistrosRelacionados = 3,
            PuestoConNombreDuplicado = 4
        }

        public enum Producto
        {
            ProductoGuardadoCorrectamente = 1,
            EliminadoExitosamente = 2,
            ProductoTieneRegistroDuplicado = 3,
            PuestoTieneRegistrosRelacionados =4
        }

        public enum PreOrden
        {
            PreOrdenGuardadoCorrectamente = 1,
            EliminadoExitosamente = 2,
            PreOrdenTieneRegistroDuplicado = 3,
           // PreOrdenTieneRegistrosRelacionados = 4
        }

        public enum TemporalPreOrden
        {
            TemporalPreOrdenGuardadoCorrectamente = 1,
            TemporalPreOrdenEliminadoCorrectamente = 2,
            ClaveDuplicado = 3          
        }

        public enum Recepcion
        {
            RecepcionGuardadoCorrectamente = 1,
            RecepcionEliminadoCorrectamente = 2,
            FolioDuplicado = 3
        }

        public enum Requisicion
        {
            RequisicionGuardadoCorrectamente = 1,
            EliminadoExitosamente = 2,
            RequisicionTieneRegistroDuplicado = 3,
            RequisicionTieneRegistrosRelacionados = 4
        }

        public enum RecuperarContrasenia
        {
            ValorPorDefecto = 0,
            CuentaNoValida = 1,
            CuentaNoExiste = 2,
            CorreoEnviado = 3
        }

        public enum CambiarContrasenia
        {
            ValorPorDefecto = 0,
            AnteriorContrasenia = 1,
            NuevaContrasenia = 2,
            Confirmacion = 3,
            Validacion = 4,
            ErrorContrasenia = 5
        }

        public enum RolUsuario
        {
            Administrador = 1,
            Almacenista = 2,
            ActivosEquipoComputo = 3,
            ActivosMobiliario = 4,
            ActivosVehiculo = 5,
            ActivosOperacionYMantenimiento = 6,
            Mantenimientos = 7
        }

        public enum SubFamilia
        {
            SubFamiliaGuardadoCorrectamente = 1,
            EliminacionExitosa = 2,
            SubFamiliaTieneRegistrosRelacionados = 3,
            SubFamiliaConNombreDuplicado = 4
        }

        public enum TemporalAccesorio
        {
            TemporalAccesorioGuardadoCorrectamente = 1,
            TemporalAccesorioEditadoCorrectamente = 2,
            TemporalAccesorioEliminadoCorrectamente = 3
        }

        public enum TemporalActivo
        {
            TemporalActivoGuardadoCorrectamente = 1,
            TemporalActivoEliminadoCorrectamente = 2,
            NumeroSerieActivoDuplicado = 3,
            CodigoBarrasParticularActivoDuplicado = 4,
            CodigoBarrasGeneralActivoDuplicado = 5
        }

        public enum TemporalAsignacion
        {
            TemporalAsignacionGuardadoCorrectamente = 1,
            TemporalAsignacionDetalleGuardadoCorrectamente = 2,
            TemporalAsignacionDetalleEliminadoCorrectamente = 3,
            TemporalAsignacionEliminadoCorrectamente = 4
        }

        public enum TemporalBajaActivo
        {
            TemporalBajaActivoEliminadoCorrectamente = 1
        }

        public enum TemporalCompra
        {
            TemporalCompraGuardadoCorrectamente = 1,
            TemporalCompraEliminadoCorrectamente = 2
        }

        public enum TemporalTransferenciaActivo
        {
            TemporalTransferenciaActivoGuardadoCorrectamente = 1,
            TemporalTransferenciaActivoEliminadoCorrectamente = 2
        }

        public enum TipoAccesorio
        {
            ActivoFijo = 1,
            NumeroEconomico = 2,
            Placas = 3,
            Motor = 4,
            Serie = 5,
            Kilometraje = 6,
            PlacaAnterior = 7,
            Clima = 8,
            Radio = 9,
            Antena = 10,
            Extinguidor = 11,
            Refaccion = 12,
            Gato = 13,
            CrucetaL = 14,
            Herramienta = 15,
            JgoLucesPreventivas = 16,
            JgoCablesPasacorriente = 17,
            TarjetaEstacionamiento = 18
        }

        public enum TipoBaja 
        {
            InutilidadPorUsoNormal = 1,
            InaplicacionEnElServicio = 2,
            Otros = 3
        }

        public enum TipoBusquedaEmpleado
        {
            Empleado = 1,
            Solicitante = 2
        }

        public enum TipoMovimiento 
        {
            Alta = 1,
            Asignacion = 2,
            Salida = 3,
            Entrada = 4,
            Baja = 5
        }

        public enum TemporalMovimiento 
        {
            TemporalMovimientoGuardadoCorrectamente =1,
            TemporalMovimientoEliminadoCorrectamente = 2
        }

        public enum LevantamientoActivo
        {
            LevantamientoActivoGuardadoCorrectamente = 1
        }

        public enum Proveedor
        {
           ProveedorGuardadoCorrectamente = 1,
           EliminacionExitosa = 2,
           ProveedorTieneRegistrosRelacionados = 3,
           ProveedorConNombreDuplicado = 4
        }

        public enum TipoAtivoConAccesorio
        {
            TipoActivoVehículoId = 3
        }

        public enum TipoAtivo
        {
            EquipoComputo = 1,
            Mobiliario = 2,
            Vehiculo = 3,
            OperaciónYMantenimiento = 4
        }

        public enum SeccionMenu
        {
            Catalogos = 1,
            ActivoFijo = 2,
            Mantenimiento = 3,
            Reportes = 4,
            Configuracion = 5
        }

        public enum Seccion
        {
            Usuario = 1,
            Edificios = 2,
            Direccion = 3,
            Departamentos = 4,
            Empleados = 5,
            Puestos = 6,
            Marcas = 7,
            Familia = 8,
            SubFamilia = 9,
            Jefes = 10,
            Activos = 11,
            Levantamiento = 12,
            Mantenimientos = 13
        }

        public enum Usuario
        {
            ValorPorDefecto = 0,
            GuardadoExitoso = 1,
            ExisteCuentaUsuario = 2,
            EliminacionExitosa = 3,
            CuentaUsuarioAdministrador = 4,
            CambiarContraseniaExitosa = 5,
            ErrorInesperado = 6,
            ContraseniaIncorrecta = 7,
            ErrorConfirmacionContrasenia = 8,
            CorreoConfirmacionDiferentes = 9,
            UsuarioTieneRegistrosRelacionados = 10,
            UsuarioQuiereBorrarseAsiMismo = 11
        }

        public enum UbicacionActivo
        {
            Piso = 1,
            Bodega = 2
        }
    }
}
