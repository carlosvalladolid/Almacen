using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

//using Activos.AccesoDatos.Catalogo;
using Activos.AccesoDatos.Almacen;
using Activos.ProcesoNegocio.Almacen;
using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Almacen;

namespace Activos.ProcesoNegocio.Almacen
{
   public class SubFamiliaPuestoProceso:Base
    {


       public ResultadoEntidad SeleccionarSubFamiliaPuesto(SubFamiliaPuestoEntidad SubFamiliaPuestoObjetoEntidad)
       {
           string CadenaConexion = string.Empty;
           ResultadoEntidad Resultado = new ResultadoEntidad();
           SubFamiliaPuestoAcceso SubFamiliaPuestoAccesoObjeto = new SubFamiliaPuestoAcceso();

           CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

           Resultado = SubFamiliaPuestoAccesoObjeto.SeleccionarSubFamiliaPuesto(SubFamiliaPuestoObjetoEntidad, CadenaConexion);

           return Resultado;
       }



    }
}
