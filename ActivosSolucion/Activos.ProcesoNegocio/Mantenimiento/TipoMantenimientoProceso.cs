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
    public class TipoMantenimientoProceso : Base 
    {
        public ResultadoEntidad SeleccionarTipoMantenimiento(TipoMantenimientoEntidad TipoMantenimientoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TipoMantenimientoAcceso TipoMantenimientoAccesoObjeto = new TipoMantenimientoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TipoMantenimientoAccesoObjeto.SeleccionarTipoMantenimiento(TipoMantenimientoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

    }
}
