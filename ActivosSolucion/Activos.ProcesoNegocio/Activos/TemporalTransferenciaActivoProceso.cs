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
    public class TemporalTransferenciaActivoProceso : Base
    {

        public ResultadoEntidad EliminarTemporalTransferenciaActivo(TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalTransferenciaActivoAcceso TemporalTransferenciaActivoAccesoObjeto = new TemporalTransferenciaActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalTransferenciaActivoAccesoObjeto.EliminarTemporalTransferenciaActivo(TemporalTransferenciaActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad InsertarTemporalTransferenciaActivo(SqlConnection Conexion, SqlTransaction Transaccion, TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalTransferenciaActivoAcceso TemporalTransferenciaActivoAccesoObjeto = new TemporalTransferenciaActivoAcceso();

            Resultado = TemporalTransferenciaActivoAccesoObjeto.InsertarTemporalTransferenciaActivo(Conexion, Transaccion, TemporalTransferenciaActivoObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarTemporalTransferenciaActivoPorDocumento(TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalTransferenciaActivoAcceso TemporalTransferenciaActivoAccesoObjeto = new TemporalTransferenciaActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalTransferenciaActivoAccesoObjeto.SeleccionarTemporalTransferenciaActivoPorDocumento(TemporalTransferenciaActivoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarTemporalTransferenciaActivo(TemporalTransferenciaActivoEntidad TemporalTransferenciaActivoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalTransferenciaActivoAcceso TemporalTransferenciaActivoAccesoObjeto = new TemporalTransferenciaActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalTransferenciaActivoAccesoObjeto.SeleccionarTemporalTransferenciaActivo(TemporalTransferenciaActivoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

    }
}
