using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Catalogo;
using Activos.AccesoDatos.Activos;
using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Activos;

namespace Activos.ProcesoNegocio.Activos
{
    public class LevantamientoActivoProceso : Base
    {

        //public bool SeleccionarLevantamientoEmpleadosRelacionados(string CadenaEmpleadoId)
        //{
        //    string CadenaConexion = string.Empty;
        //    ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
        //    LevantamientoActivoAcceso LevantamientoActivoAccesoObjeto = new LevantamientoActivoAcceso();

        //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

        //    ResultadoEntidadObjeto = LevantamientoActivoAccesoObjeto.SeleccionarLevantamientoEmpleadosRelacionados(CadenaEmpleadoId, CadenaConexion);

        //    if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
        //        return false;
        //    else
        //        return true;
        //}

        //public ResultadoEntidad SeleccionarLevantamientoActivo(LevantamientoActivoEntidad LevantamientoActivoObjetoEntidad)
        //{
        //    string CadenaConexion = string.Empty;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    LevantamientoActivoAcceso LevantamientoActivoAccesoObjeto = new LevantamientoActivoAcceso();

        //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

        //    Resultado = LevantamientoActivoAccesoObjeto.SeleccionarLevantamientoActivo(LevantamientoActivoObjetoEntidad, CadenaConexion);

        //    return Resultado;
        //}

        public ResultadoEntidad SeleccionarLevantamientoReporte(LevantamientoActivoEntidad LevantamientoActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            LevantamientoActivoAcceso LevantamientoActivoAccesoObjeto = new LevantamientoActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = LevantamientoActivoAccesoObjeto.SeleccionarLevantamientoReporte(LevantamientoActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad InsertarLevantamiento(SqlConnection Conexion, SqlTransaction Transaccion, LevantamientoActivoEntidad LevantamientoActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            LevantamientoActivoAcceso LevantamientoActivoAccesoObjeto = new LevantamientoActivoAcceso();

            Resultado = LevantamientoActivoAccesoObjeto.InsertarLevantamiento(Conexion, Transaccion, LevantamientoActivoObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad InsertarLevantamientoEncabezado(SqlConnection Conexion, SqlTransaction Transaccion, LevantamientoActivoEntidad LevantamientoActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            LevantamientoActivoAcceso LevantamientoActivoAccesoObjeto = new LevantamientoActivoAcceso();

            Resultado = LevantamientoActivoAccesoObjeto.InsertarLevantamientoEncabezado(Conexion, Transaccion, LevantamientoActivoObjetoEntidad);

            return Resultado;
        }
        
        public ResultadoEntidad GuardarLevantamiento(LevantamientoActivoEntidad LevantamientoActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            SqlTransaction Transaccion;
            SqlConnection Conexion;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            //Primero se guarda el encabezado del levantamiento
            Resultado = InsertarLevantamientoEncabezado(Conexion, Transaccion, LevantamientoActivoObjetoEntidad);

            // Si el levantamiento fue guardado correctamente, se obtiene su ID
            if (Resultado.ErrorId == (int)ConstantePrograma.LevantamientoActivo.LevantamientoActivoGuardadoCorrectamente)
            {
                LevantamientoActivoObjetoEntidad.LevantamientoID = Resultado.NuevoRegistroId;

                //Ahora se guarda el detalle del levantamiento (sus activos y estatus)
                Resultado = InsertarLevantamiento(Conexion, Transaccion, LevantamientoActivoObjetoEntidad);

                // Si el detalle del levantamiento fue guardado correctamente finaliza la transaccion
                if (Resultado.ErrorId == (int)ConstantePrograma.LevantamientoActivo.LevantamientoActivoGuardadoCorrectamente)
                {
                    Resultado.NuevoRegistroId = LevantamientoActivoObjetoEntidad.LevantamientoID;
                    Transaccion.Commit();
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

    
    
    
    }
}
