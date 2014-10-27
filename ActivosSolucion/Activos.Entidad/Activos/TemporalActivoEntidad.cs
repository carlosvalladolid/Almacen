using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Activos
{
    public class TemporalActivoEntidad : Base
    {

        private int _TemporalActivoId;          // Identificador del activo temporal
        private int _TemporalCompraId;          // Identificador de la compra temporal
        private Int16 _TipoActivoId;            // Identificador del tipo de activo
        private Int16 _SubFamiliaId;            // Identificador de la subfamilia
        private Int16 _MarcaId;                 // Identificador de la marca
        private Int16 _CondicionId;             // Identificador de la condición del activo
        private Int16 _EmpleadoId;              // Identificador de Empleado
        private string _CodigoBarrasGeneral;    // Código de barras general del activo
        private string _CodigoBarrasParticular; // Código de barras particulas del activo
        private string _Descripcion;            // Descripcion del activo temporal
        private string _NumeroSerie;            // Numero de serie del activo temporal
        private string _Modelo;                 // Modelo del activo temporal
        private string _Color;                  // Color del activo temporal
        private decimal _Monto;                 // Monto del activo temporal
        private Int16 _UbicacionActivoId;       // Identificador de la ubicación del activo
 

        public TemporalActivoEntidad()
        {
            _TemporalActivoId = 0;
            _TemporalCompraId = 0;
            _TipoActivoId = 0;
            _SubFamiliaId = 0;
            _MarcaId = 0;
            _CondicionId = 0;
            _EmpleadoId = 0;
            _CodigoBarrasGeneral = string.Empty;
            _CodigoBarrasParticular = string.Empty;
            _Descripcion = string.Empty;
            _NumeroSerie = string.Empty;
            _Modelo = string.Empty;
            _Color = string.Empty;
            _Monto = 0;
            _UbicacionActivoId = 0;
            
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

        public Int16 CondicionId
        {
            get { return _CondicionId; }
            set { _CondicionId = value; }
        }

        public Int16 EmpleadoId
        {
            get { return _EmpleadoId; }
            set { _EmpleadoId = value; }
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

        public Int16 UbicacionActivoId
        {
            get { return _UbicacionActivoId; }
            set { _UbicacionActivoId = value; }
        }

        

    }
}
