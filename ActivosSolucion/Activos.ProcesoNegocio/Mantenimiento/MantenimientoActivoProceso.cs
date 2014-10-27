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
    public class MantenimientoActivoProceso : Base
    {
        public ResultadoEntidad InsertarMantenimientoActivo(SqlConnection Conexion, SqlTransaction Transaccion, MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoActivoAcceso MantenimientoActivoAccesoObjeto = new MantenimientoActivoAcceso();

            Resultado = MantenimientoActivoAccesoObjeto.InsertarMantenimientoActivo(Conexion, Transaccion, MantenimientoActivoEntidadObjeto);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMantenimientoActivo(MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoActivoAcceso MantenimientoActivoAccesoObjeto = new MantenimientoActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MantenimientoActivoAccesoObjeto.SeleccionarMantenimientoActivo(MantenimientoActivoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMantenimientoReporteGeneral(MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoActivoAcceso MantenimientoActivoAccesoObjeto = new MantenimientoActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MantenimientoActivoAccesoObjeto.SeleccionarMantenimientoReporteGeneral(MantenimientoActivoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMantenimientoReportePorActivo(MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoActivoAcceso MantenimientoActivoAccesoObjeto = new MantenimientoActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MantenimientoActivoAccesoObjeto.SeleccionarMantenimientoReportePorActivo(MantenimientoActivoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMantenimientoReportePorTecnico(MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoActivoAcceso MantenimientoActivoAccesoObjeto = new MantenimientoActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MantenimientoActivoAccesoObjeto.SeleccionarMantenimientoReportePorTecnico(MantenimientoActivoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

    }
}
