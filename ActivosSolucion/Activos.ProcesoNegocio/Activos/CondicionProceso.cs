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
    public class CondicionProceso : Base
    {

        public ResultadoEntidad SeleccionarCondicion(CondicionEntidad CondicionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            CondicionAcceso CondicionAccesoObjeto = new CondicionAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = CondicionAccesoObjeto.SeleccionarCondicion(CondicionObjetoEntidad, CadenaConexion);

            return Resultado;
        }

    }
}
