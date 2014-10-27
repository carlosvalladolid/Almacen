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
    public class DependenciaProceso : Base
    {
        public ResultadoEntidad SeleccionarDependencia(DependenciaEntidad DependenciaEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            DependenciaAcceso DepedenciaAccesoDatos = new DependenciaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = DepedenciaAccesoDatos.SeleccionarDependencia(DependenciaEntidadObjeto, CadenaConexion);

            return Resultado;
        }

    }
}
