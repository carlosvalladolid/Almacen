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
    public class MantenimientoProceso : Base
    {
        public ResultadoEntidad InsertarMantenimiento(SqlConnection Conexion, SqlTransaction Transaccion, MantenimientoEntidad MantenimientoEntidadObjeto)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoAcceso MantenimientoAccesoObjeto = new MantenimientoAcceso();

            Resultado = MantenimientoAccesoObjeto.InsertarMantenimiento(Conexion, Transaccion, MantenimientoEntidadObjeto);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMantenimiento(MantenimientoEntidad MantenimientoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoAcceso MantenimientoAccesoObjeto = new MantenimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MantenimientoAccesoObjeto.SeleccionarMantenimiento(MantenimientoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMantenimientoAvanzado(MantenimientoEntidad MantenimientoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoAcceso MantenimientoAccesoObjeto = new MantenimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = MantenimientoAccesoObjeto.SeleccionarMantenimientoAvanzado(MantenimientoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad GuardarMantenimiento(MantenimientoEntidad MantenimientoEntidadObjeto)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            string CadenaConexion = string.Empty;
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            if (MantenimientoEntidadObjeto.MantenimientoId == 0)
            {
                Resultado = InsertarMantenimiento(Conexion, Transaccion, MantenimientoEntidadObjeto);
                MantenimientoEntidadObjeto.MantenimientoId = Resultado.NuevoRegistroId;

            }
            else
            {
                Resultado = EliminarMantenimientoEmpleado(Conexion, Transaccion, MantenimientoEntidadObjeto.MantenimientoId);
            }

            if (Resultado.ErrorId == (int)ConstantePrograma.Mantenimiento.GuardadoCorrectamente
                || Resultado.ErrorId == (int)ConstantePrograma.MantenimientoEmpleado.EliminadoCorrectamente)
            {
                Resultado = InsertarMantenimientoActivo(Conexion, Transaccion, MantenimientoEntidadObjeto);

                if (Resultado.ErrorId == (int)ConstantePrograma.MantenimientoActivo.GuardadoCorrectamente)
                {
                    Resultado = InsertarMantenimientoEmpleado(Conexion, Transaccion, MantenimientoEntidadObjeto);

                    if (Resultado.ErrorId == (int)ConstantePrograma.MantenimientoEmpleado.GuardadoCorrectamente)
                    {
                        Transaccion.Commit();
                        Resultado.NuevoRegistroId = MantenimientoEntidadObjeto.MantenimientoId;
                    }
                    else
                    {
                        Transaccion.Rollback();
                    }
                }
                else
                {
                    Transaccion.Rollback();
                }
            }
            else
            {
                Transaccion.Rollback();
            }


            Conexion.Close();

            return Resultado;

        }

        public ResultadoEntidad InsertarMantenimientoEmpleado(SqlConnection Conexion, SqlTransaction Transaccion, MantenimientoEntidad MantenimientoEntidadObjeto)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoEmpleadoEntidad MantenimientoEmpleadoEntidadObjeto = new MantenimientoEmpleadoEntidad();
            MantenimientoEmpleadoProceso MantenimientoEmpleadoProcesoObjeto = new MantenimientoEmpleadoProceso();

            MantenimientoEmpleadoEntidadObjeto.SesionId = MantenimientoEntidadObjeto.SesionId;
            MantenimientoEmpleadoEntidadObjeto.MantenimientoId = MantenimientoEntidadObjeto.MantenimientoId;

            Resultado = MantenimientoEmpleadoProcesoObjeto.InsertarMantenimientoEmpleado(Conexion, Transaccion, MantenimientoEmpleadoEntidadObjeto);

            return Resultado;
        }

        public ResultadoEntidad InsertarMantenimientoActivo(SqlConnection Conexion, SqlTransaction Transaccion, MantenimientoEntidad MantenimientoEntidadObjeto)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoActivoEntidad MantenimientoActivoEntidadObjeto = new MantenimientoActivoEntidad();
            MantenimientoActivoProceso MantenimientoActivoProcesoObjeto = new MantenimientoActivoProceso();

            MantenimientoActivoEntidadObjeto.SesionId = MantenimientoEntidadObjeto.SesionId;
            MantenimientoActivoEntidadObjeto.UsuarioIdInserto = MantenimientoEntidadObjeto.UsuarioIdInserto;
            MantenimientoActivoEntidadObjeto.MantenimientoId = MantenimientoEntidadObjeto.MantenimientoId;

            Resultado = MantenimientoActivoProcesoObjeto.InsertarMantenimientoActivo(Conexion, Transaccion, MantenimientoActivoEntidadObjeto);

            return Resultado;
        }

        public ResultadoEntidad EliminarMantenimientoEmpleado(SqlConnection Conexion, SqlTransaction Transaccion, int MantenimientoId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MantenimientoEmpleadoEntidad MantenimientoEmpleadoEntidadObjeto = new MantenimientoEmpleadoEntidad();
            MantenimientoEmpleadoProceso MantenimientoEmpleadoProcesoObjeto = new MantenimientoEmpleadoProceso();

            MantenimientoEmpleadoEntidadObjeto.MantenimientoId = MantenimientoId;

            Resultado = MantenimientoEmpleadoProcesoObjeto.EliminarMantenimientoEmpleado(Conexion, Transaccion, MantenimientoEmpleadoEntidadObjeto);

            return Resultado;
        }

    }
}
