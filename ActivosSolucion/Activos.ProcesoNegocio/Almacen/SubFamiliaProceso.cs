using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Activos.AccesoDatos.Catalogo;
using Activos.AccesoDatos.Almacen;
using Activos.ProcesoNegocio.Almacen;
using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
//using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;


namespace Activos.ProcesoNegocio.Almacen
{
  public  class SubFamiliaProceso:Base
    {


       
        public ResultadoEntidad SeleccionarSubFamilia(SubFamiliaEntidad SubFamiliaObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SubFamiliaAcceso SubFamiliaAccesoObjeto = new SubFamiliaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            Resultado = SubFamiliaAccesoObjeto.SeleccionarSubFamilia(SubFamiliaObjetoEntidad, CadenaConexion);

            return Resultado;
        }

    }
}
