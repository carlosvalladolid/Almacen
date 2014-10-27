using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Mantenimiento;
using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Mantenimiento;

namespace Activos.ProcesoNegocio.Mantenimiento
{
    public class TemporalMantenimientoActivoProceso : Base
    {

        public ResultadoEntidad EliminarTemporalMantenimientoActivo(TemporalMantenimientoActivoEntidad TemporalMantenimientoActivoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalMantenimientoActivoAcceso TemporalMantenimientoActivoAccesoObjeto = new TemporalMantenimientoActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalMantenimientoActivoAccesoObjeto.EliminarTemporalMantenimientoActivo(TemporalMantenimientoActivoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad InsertarTemporalMantenimientoActivo(TemporalMantenimientoActivoEntidad TemporalMantenimientoActivoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalMantenimientoActivoAcceso TemporalMantenimientoActivoAccesoObjeto = new TemporalMantenimientoActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalMantenimientoActivoAccesoObjeto.InsertarTemporalMantenimientoActivo(TemporalMantenimientoActivoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarTemporalMantenimientoActivo(TemporalMantenimientoActivoEntidad TemporalMantenimientoActivoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalMantenimientoActivoAcceso TemporalMantenimientoActivoAccesoObjeto = new TemporalMantenimientoActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalMantenimientoActivoAccesoObjeto.SeleccionarTemporalMantenimientoActivo(TemporalMantenimientoActivoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

    }
}
