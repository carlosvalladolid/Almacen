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
    public class CiudadProceso : Base
    {

        public ResultadoEntidad SeleccionarCiudad(CiudadEntidad CiudadEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            CiudadAcceso CiudadAccesoDatos = new CiudadAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = CiudadAccesoDatos.SeleccionarCiudad(CiudadEntidadObjeto, CadenaConexion);

            return Resultado;
        }

    }
}
