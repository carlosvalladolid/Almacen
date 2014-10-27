using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Catalogo;
using Activos.AccesoDatos.Activos;
using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Activos;

namespace Activos.ProcesoNegocio.Activos
{
    public class TemporalCompraProceso : Base
    {

        public ResultadoEntidad LimpiarTemporalTabla(TemporalCompraEntidad TemporalCompraObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalCompraAcceso TemporalCompraAccesoObjeto = new TemporalCompraAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalCompraAccesoObjeto.LimpiarTemporalTabla(TemporalCompraObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad InsertarTemporalCompra(TemporalCompraEntidad TemporalCompraObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalCompraAcceso TemporalCompraAccesoObjeto = new TemporalCompraAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalCompraAccesoObjeto.InsertarTemporalCompra(TemporalCompraObjetoEntidad, CadenaConexion);

            return Resultado;
        }

    }
}
