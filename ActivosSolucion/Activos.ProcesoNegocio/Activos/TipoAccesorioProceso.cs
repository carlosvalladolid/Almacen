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
    public class TipoAccesorioProceso : Base
    {

        public ResultadoEntidad SeleccionarTipoAccesorio(TipoAccesorioEntidad TipoAccesorioObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TipoAccesorioAcceso TipoAccesorioAccesoObjeto = new TipoAccesorioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TipoAccesorioAccesoObjeto.SeleccionarTipoAccesorio(TipoAccesorioObjetoEntidad, CadenaConexion);

            return Resultado;
        }

    }
}
