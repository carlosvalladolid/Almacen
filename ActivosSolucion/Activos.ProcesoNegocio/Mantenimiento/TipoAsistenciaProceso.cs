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
    public class TipoAsistenciaProceso : Base
    {

        public ResultadoEntidad SeleccionarTipoAsistencia(TipoAsistenciaEntidad TipoAsistenciaObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TipoAsistenciaAcceso TipoAsistenciaAccesoObjeto = new TipoAsistenciaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TipoAsistenciaAccesoObjeto.SeleccionarTipoAsistencia(TipoAsistenciaObjetoEntidad, CadenaConexion);

            return Resultado;
        }

    }
}
