using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Activos;
using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Activos;

namespace Activos.ProcesoNegocio.Activos
{
    public class TipoActivoProceso : Base
    {

        public ResultadoEntidad SeleccionarTipoActivo(TipoActivoEntidad TipoActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TipoActivoAcceso TipoActivoAccesoObjeto = new TipoActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TipoActivoAccesoObjeto.SeleccionarTipoActivo(TipoActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

    }
}
