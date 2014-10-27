using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Catalogo;
using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;

namespace Activos.ProcesoNegocio.Catalogo
{
    public class EstadoProceso : Base
    {

        public ResultadoEntidad SeleccionarEstado(EstadoEntidad EstadoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EstadoAcceso EstadoAccesoDatos = new EstadoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = EstadoAccesoDatos.SeleccionarEstado(EstadoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

    }
}
