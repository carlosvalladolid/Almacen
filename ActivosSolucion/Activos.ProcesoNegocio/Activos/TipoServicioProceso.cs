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
    public class TipoServicioProceso : Base
    {
        public ResultadoEntidad SeleccionarTipoServicio(TipoServicioEntidad TipoServicioObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TipoServicioAcceso TipoServicioAccesoObjeto = new TipoServicioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TipoServicioAccesoObjeto.SeleccionarTipoServicio(TipoServicioObjetoEntidad, CadenaConexion);

            return Resultado;
        }
    }
}
