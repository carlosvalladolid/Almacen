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
    public class TemporalMantenimientoEmpleadoProceso : Base
    {
        public ResultadoEntidad AgregarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoEntidadObjeto)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalMantenimientoEmpleadoEntidad BusquedaTemporalMantenimientoEmpleadoEntidadObjeto = new TemporalMantenimientoEmpleadoEntidad();
            TemporalMantenimientoEmpleadoAcceso TemporalMantenimientoEmpleadoAccesoObjeto = new TemporalMantenimientoEmpleadoAcceso();

            //Primero se busca si el empleado ya fue agregado a la tabla temporal
            BusquedaTemporalMantenimientoEmpleadoEntidadObjeto.SesionId = TemporalMantenimientoEmpleadoEntidadObjeto.SesionId;
            BusquedaTemporalMantenimientoEmpleadoEntidadObjeto.EmpleadoId = TemporalMantenimientoEmpleadoEntidadObjeto.EmpleadoId;

            Resultado = SeleccionarTemporalMantenimientoEmpleado(BusquedaTemporalMantenimientoEmpleadoEntidadObjeto);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                Resultado.DescripcionError = TextoError.MantenimientoEmpleadoAgregadoYa;
            }
            else
            {
                Resultado = InsertarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoEntidadObjeto);
            }

            return Resultado;
        }

        public ResultadoEntidad EliminarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalMantenimientoEmpleadoAcceso TemporalMantenimientoEmpleadoAccesoObjeto = new TemporalMantenimientoEmpleadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalMantenimientoEmpleadoAccesoObjeto.EliminarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad InsertarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalMantenimientoEmpleadoAcceso TemporalMantenimientoEmpleadoAccesoObjeto = new TemporalMantenimientoEmpleadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalMantenimientoEmpleadoAccesoObjeto.InsertarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad InsertarTemporalMantenimientoEmpleadoAnteriores(TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalMantenimientoEmpleadoAcceso TemporalMantenimientoEmpleadoAccesoObjeto = new TemporalMantenimientoEmpleadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalMantenimientoEmpleadoAccesoObjeto.InsertarTemporalMantenimientoEmpleadoAnteriores(TemporalMantenimientoEmpleadoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoEntidad TemporalMantenimientoEmpleadoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalMantenimientoEmpleadoAcceso TemporalMantenimientoEmpleadoAccesoObjeto = new TemporalMantenimientoEmpleadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalMantenimientoEmpleadoAccesoObjeto.SeleccionarTemporalMantenimientoEmpleado(TemporalMantenimientoEmpleadoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

    }
}
