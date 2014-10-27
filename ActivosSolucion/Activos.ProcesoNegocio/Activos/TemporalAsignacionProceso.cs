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
    public class TemporalAsignacionProceso : Base
    {
        public ResultadoEntidad LimpiarTemporalTablaAsignacion(TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalAsignacionAcceso TemporalAsignacionAccesoObjeto = new TemporalAsignacionAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalAsignacionAccesoObjeto.LimpiarTemporalTablaAsignacion(TemporalAsignacionObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad InsertarTemporalAsignacion(SqlConnection Conexion, SqlTransaction Transaccion, TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalAsignacionAcceso TemporalAsignacionAccesoObjeto = new TemporalAsignacionAcceso();

            Resultado = TemporalAsignacionAccesoObjeto.InsertarTemporalAsignacion(Conexion, Transaccion, TemporalAsignacionObjetoEntidad);

            return Resultado;
        }

    }
}
