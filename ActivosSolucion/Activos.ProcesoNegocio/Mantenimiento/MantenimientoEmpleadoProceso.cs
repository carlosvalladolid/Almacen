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
    public class MantenimientoEmpleadoProceso : Base
    {

        public ResultadoEntidad EliminarMantenimientoEmpleado(SqlConnection Conexion, SqlTransaction Transaccion, MantenimientoEmpleadoEntidad MantenimientoEmpleadoEntidadObjeto)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoEmpleadoAcceso MantenimientoEmpleadoAccesoObjeto = new MantenimientoEmpleadoAcceso();

            Resultado = MantenimientoEmpleadoAccesoObjeto.EliminarMantenimientoEmpleado(Conexion, Transaccion, MantenimientoEmpleadoEntidadObjeto);

            return Resultado;
        }

        public ResultadoEntidad InsertarMantenimientoEmpleado(SqlConnection Conexion, SqlTransaction Transaccion, MantenimientoEmpleadoEntidad MantenimientoEmpleadoEntidadObjeto)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoEmpleadoAcceso MantenimientoEmpleadoAccesoObjeto = new MantenimientoEmpleadoAcceso();

            Resultado = MantenimientoEmpleadoAccesoObjeto.InsertarMantenimientoEmpleado(Conexion, Transaccion, MantenimientoEmpleadoEntidadObjeto);

            return Resultado;
        }
        public ResultadoEntidad SeleccionarMantenimientoEmpleado(MantenimientoEmpleadoEntidad MantenimientoEmpleadoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoEmpleadoAcceso MantenimientoEmpleadoAccesoObjeto = new MantenimientoEmpleadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MantenimientoEmpleadoAccesoObjeto.SeleccionarMantenimientoEmpleado(MantenimientoEmpleadoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

    }
}
