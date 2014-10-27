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
    public class TipoDocumentoProceso : Base
    {

        public ResultadoEntidad SeleccionarTipoDocumento(TipoDocumentoEntidad TipoDocumentoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TipoDocumentoAcceso TipoDocumentoAccesoObjeto = new TipoDocumentoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TipoDocumentoAccesoObjeto.SeleccionarTipoDocumento(TipoDocumentoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

    }
}
