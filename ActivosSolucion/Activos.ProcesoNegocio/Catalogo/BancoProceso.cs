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
    public class BancoProceso : Base
    {
        public ResultadoEntidad SeleccionarBanco(BancoEntidad BancoEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            BancoAcceso BancoAccesoDatos = new BancoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = BancoAccesoDatos.SeleccionarBanco(BancoEntidadObjeto, CadenaConexion);

            return Resultado;
        }

    }
}
