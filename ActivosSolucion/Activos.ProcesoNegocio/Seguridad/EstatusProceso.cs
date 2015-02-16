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
    public class EstatusProceso : Base
    {
        public ResultadoEntidad SeleccionarEstatus(EstatusEntidad EstatusEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EstatusAcceso EstatusAccesoDatos = new EstatusAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

            Resultado = EstatusAccesoDatos.SeleccionarEstatus(EstatusEntidadObjeto, CadenaConexion);

            return Resultado;
        }


        public ResultadoEntidad SeleccionarEstatusOrdenSalida(EstatusEntidad EstatusEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EstatusAcceso EstatusAccesoDatos = new EstatusAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

            Resultado = EstatusAccesoDatos.SeleccionarEstatusOrdenSalida(EstatusEntidadObjeto, CadenaConexion);

            return Resultado;
        }
    }
}
