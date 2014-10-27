using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class ActivoEntidad : Base
    {

        private int _ActivoId;                // Identificador del activo
        private Int16 _UsuarioId;                   // Identificador del usuario
        private int _CompraId;                // Identificador de la compra
        private Int16 _TipoActivoId;            // Identificador del tipo de activo
        private Int16 _SubFamiliaId;            // Identificador de la subfamilia
        private Int16 _MarcaId;                 // Identificador de la marca
        private Int16 _EstatusId;               // Identificador del estatus del activo
        private Int16 _CondicionId;             // Identificador de la condición del activo
        private string _CodigoBarrasGeneral;    // Código de barras general del activo
        private string _CodigoBarrasParticular; // Código de barras particulas del activo
        private string _Descripcion;            // Descripcion del activo
        private string _NumeroSerie;            // Numero de serie del activo
        private string _Modelo;                 // Modelo del activo
        private string _Color;                  // Color del activo
        private decimal _Monto;                 // Monto del activo
        private Int16 _EmpleadoId;              // Numero de Empleado
        private Int16 _UbicacionActivoId;       // Identificador de la ubicación del activo
        private Int16 _UsuarioIdModifico;       // Identificador del usuario que modificó el registro por última vez
        private string _SesionId;               // Almacena una cadena aleatoria de la sesion
        private Int16 _EmpleadoAutorizoId;      // Numero del Empleado que autoriza movimientos



        // Otros campos
        private int _TemporalActivoId;          // Identificador del activo temporal
        private int _TemporalCompraId;          // Identificador de la compra temporal
        private int _TemporalAsignacionId;      // Identificador de la asignación temporal
        private string _GrupoEstatus;           // Grupo de estatus de activo separados por coma
        private string _CadenaParticularXML;    // Cadena usado como XML para guardar los codigos de barra particular
        private string _CadenaGeneralXML;       // Cadena usado como XML para guardar los codigos de barra general
        private string _FechaMovimiento;              // Cadena usada para almacenar la fecha en que se dió de baja el activo
        private string _TipoBaja;               // Cadena para almacenar el tipo de la baja
        private string _DescripcionMovimiento;  // Cadena para almacenar una breve descripción del porque de la baja
        private Int16 _TipoDeMovimiento;        // Se almacenará si fué alta, baja, asignación, salida, entrada
        private string _FechaInserto;           // Fecha en que se está generando el registro
        private Int16 _MovimientoId;            // Almacena el ID del movimiento que se hizo con este activo
        private Int16 _TipoBajaId;              // Identificador que se usará para almacenar el ID del tipo de que tipo fué la baja    
        private string _GrupoTipoActivoId;      // Grupo de tipos de activo separados por coma
        private Int16 _MostrarAsignadosSalida;   // Campo usado para mostrar o todos los productos asignados (1) o solo los asignados y que no esten en salida (0)
        private Int16 _TipoAccesorioId;         // Identificador del tipo de accesorio
        private string _ActivosXML;             // Cadena usado como XML para guardar los activos
        private Int16 _TipoServicioId;          // Identificador del tipo de servicio que se le hará a dicho activo 
        private int _ProveedorId;               // Id del proveedor que surtió el activo
        private Int16 _DepartamentoId;          // Identificador de departamento
        private Int16 _DireccionId;             // Identificador de direccion
        private Int16 _FamiliaId;               // Identificador de familia
        private string _CompraFolio;            // Folio del documento
        private string _StrFechaInicio;         // Fecha de inicio de rango
        private string _StrFechaFin;            // Fecha de fin de rango
        private Int16 _AlmacenistaEmpleadoId;   // Identificador del empleado almacenista de donde se obtendrá la ubicacion de los activos que estan en bodega
        private string _DepartamentoNombre;     // Nombre del departamento
        private string _DireccionNombre;        // Nombre de la direccion
        private Int16 _EdificioId;              // Identificador del edificio
        private string _EdificioNombre;         // Nombre del edificio

        public ActivoEntidad()
        {
            _ActivoId = 0;
            _UsuarioId = 0;
            _CompraId = 0;
            _TipoActivoId = 0;
            _SubFamiliaId = 0;
            _MarcaId = 0;
            _EstatusId = 0;
            _CondicionId = 0;
            _CodigoBarrasGeneral = string.Empty;
            _CodigoBarrasParticular = string.Empty;
            _Descripcion = string.Empty;
            _NumeroSerie = string.Empty;
            _Modelo = string.Empty;
            _Color = string.Empty;
            _Monto = 0;
            _TemporalActivoId = 0;
            _TemporalCompraId = 0;
            _TemporalAsignacionId = 0;
            _EmpleadoId = 0;
            _UbicacionActivoId = 0;
            _GrupoEstatus = string.Empty;
            _CadenaParticularXML = string.Empty;
            _CadenaGeneralXML = string.Empty;
            _UsuarioIdModifico = 0;
            _FechaMovimiento = string.Empty;
            _TipoBaja = string.Empty;
            _DescripcionMovimiento = string.Empty;
            _TipoDeMovimiento = 0;
            _FechaInserto = string.Empty;
            _SesionId = string.Empty;
            _MovimientoId = 0;
            _TipoBajaId = 0;
            _EmpleadoAutorizoId = 0;
            _GrupoTipoActivoId = string.Empty;
            _MostrarAsignadosSalida = 1;
            _TipoAccesorioId = 0;
            _ActivosXML = string.Empty;
            _TipoServicioId = 0;
            _ProveedorId = 0;
            _DepartamentoId = 0;
            _DireccionId = 0;
            _FamiliaId = 0;
            _CompraFolio = string.Empty;
            _StrFechaInicio = string.Empty;
            _StrFechaFin = string.Empty;
            _AlmacenistaEmpleadoId = 0;
            _DepartamentoNombre = string.Empty;
            _DireccionNombre = string.Empty;
            _EdificioId = 0;
            _EdificioNombre = string.Empty;
        }

        public int ActivoId
        {
            get { return _ActivoId; }
            set { _ActivoId = value; }
        }

        public Int16 UsuarioId
        {
            get { return _UsuarioId; }
            set { _UsuarioId = value; }
        }

        public int CompraId
        {
            get { return _CompraId; }
            set { _CompraId = value; }
        }

        public Int16 TipoActivoId
        {
            get { return _TipoActivoId; }
            set { _TipoActivoId = value; }
        }

        public Int16 SubFamiliaId
        {
            get { return _SubFamiliaId; }
            set { _SubFamiliaId = value; }
        }

        public Int16 MarcaId
        {
            get { return _MarcaId; }
            set { _MarcaId = value; }
        }

        public Int16 EstatusId
        {
            get { return _EstatusId; }
            set { _EstatusId = value; }
        }

        public Int16 CondicionId
        {
            get { return _CondicionId; }
            set { _CondicionId = value; }
        }

        public string CodigoBarrasGeneral
        {
            get { return _CodigoBarrasGeneral; }
            set { _CodigoBarrasGeneral = value; }
        }

        public string CodigoBarrasParticular
        {
            get { return _CodigoBarrasParticular; }
            set { _CodigoBarrasParticular = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public string NumeroSerie
        {
            get { return _NumeroSerie; }
            set { _NumeroSerie = value; }
        }

        public string Modelo
        {
            get { return _Modelo; }
            set { _Modelo = value; }
        }

        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        public decimal Monto
        {
            get { return _Monto; }
            set { _Monto = value; }
        }

        public Int16 MovimientoId
        {
            get { return _MovimientoId; }
            set { _MovimientoId = value; }
        }


        public int TemporalActivoId
        {
            get { return _TemporalActivoId; }
            set { _TemporalActivoId = value; }
        }

        public int TemporalCompraId
        {
            get { return _TemporalCompraId; }
            set { _TemporalCompraId = value; }
        }

        public Int16 TipoBajaId
        {
            get { return _TipoBajaId; }
            set { _TipoBajaId = value; }
        }


        public Int16 EmpleadoId
        {
            get { return _EmpleadoId; }
            set { _EmpleadoId = value; }
        }

        public Int16 EmpleadoAutorizoId 
        {
            get { return _EmpleadoAutorizoId; }
            set { _EmpleadoAutorizoId = value; }
        }

        public Int16 UbicacionActivoId
        {
            get { return _UbicacionActivoId; }
            set { _UbicacionActivoId = value; }
        }

        public int TemporalAsignacionId
        {
            get { return _TemporalAsignacionId; }
            set { _TemporalAsignacionId = value; }
        }

        public string GrupoEstatus
        {
            get { return _GrupoEstatus; }
            set { _GrupoEstatus = value; }
        }

        public string CadenaParticularXML
        {
            get { return _CadenaParticularXML; }
            set { _CadenaParticularXML = value; }
        }

        public string CadenaGeneralXML
        {
            get { return _CadenaGeneralXML; }
            set { _CadenaGeneralXML = value; }
        }

        public Int16 UsuarioIdModifico
        {
            get { return _UsuarioIdModifico; }
            set { _UsuarioIdModifico = value; }
        }
        public string FechaMovimiento
        {
            get { return _FechaMovimiento; }
            set { _FechaMovimiento = value; }
        }

        public string TipoBaja
        {
            get { return _TipoBaja; }
            set { _TipoBaja = value; }
        }

        public string DescripcionMovimiento
        {
            get { return _DescripcionMovimiento; }
            set { _DescripcionMovimiento = value; }
        }

        public string FechaInserto
        {
            get { return _FechaInserto; }
            set { _FechaInserto = value; }
        }

        public Int16 TipoDeMovimiento
        {
            get { return _TipoDeMovimiento; }
            set { _TipoDeMovimiento = value; }
        }

        public string SesionId
        {
            get { return _SesionId; }
            set { _SesionId = value; }
        }

        public string GrupoTipoActivoId
        {
            get { return _GrupoTipoActivoId; }
            set { _GrupoTipoActivoId = value; }
        }

        public Int16 MostrarAsignadosSalida
        {
            get { return _MostrarAsignadosSalida; }
            set { _MostrarAsignadosSalida = value; }
        }

        public Int16 TipoAccesorioId
        {
            get { return _TipoAccesorioId; }
            set { _TipoAccesorioId = value; }
        }

        public string ActivosXML
        {
            get { return _ActivosXML; }
            set { _ActivosXML = value; }
        }

        public Int16 TipoServicioId
        {
            get {return _TipoServicioId;}
            set {_TipoServicioId = value;}
        }

        public int ProveedorId
        {
            get {return _ProveedorId;}
            set {_ProveedorId = value;}
        }

        public Int16 DepartamentoId
        {
            get { return _DepartamentoId; }
            set { _DepartamentoId = value; }
        }

        public Int16 DireccionId
        {
            get { return _DireccionId; }
            set { _DireccionId = value; }
        }

        public Int16 FamiliaId
        {
            get { return _FamiliaId; }
            set { _FamiliaId = value; }
        }

        public string CompraFolio
        {
            get { return _CompraFolio; }
            set { _CompraFolio = value; }
        }

        public string StrFechaInicio
        {
            get { return _StrFechaInicio; }
            set { _StrFechaInicio = value; }
        }

        public string StrFechaFin
        {
            get { return _StrFechaFin; }
            set { _StrFechaFin = value; }
        }

        public Int16 AlmacenistaEmpleadoId
        {
            get { return _AlmacenistaEmpleadoId; }
            set { _AlmacenistaEmpleadoId = value; }
        }

        public string DepartamentoNombre
        {
            get { return _DepartamentoNombre; }
            set { _DepartamentoNombre = value; }
        }

        public string DireccionNombre
        {
            get { return _DireccionNombre; }
            set { _DireccionNombre = value; }
        }

        public Int16 EdificioId
        {
            get { return _EdificioId; }
            set { _EdificioId = value; }
        }

        public string EdificioNombre
        {
            get { return _EdificioNombre; }
            set { _EdificioNombre = value; }
        }

    }
}
