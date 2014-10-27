using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Seguridad;
using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;

namespace Activos.ProcesoNegocio.Seguridad
{
    public class RolProceso : Base
    {
        public ResultadoEntidad SeleccionarRol(RolEntidad RolEstatusObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RolAcceso RolAccesoDatos = new RolAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

            Resultado = RolAccesoDatos.SeleccionarRol(RolEstatusObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarRolPagina(RolEntidad RolEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RolAcceso RolAccesoDatos = new RolAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

            Resultado = RolAccesoDatos.SeleccionarRolPagina(RolEntidadObjeto, CadenaConexion);

            return Resultado;
        }
    }
}
