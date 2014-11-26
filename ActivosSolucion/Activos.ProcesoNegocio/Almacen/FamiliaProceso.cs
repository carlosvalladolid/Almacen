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
   public class FamiliaProceso:Base
    {
       public ResultadoEntidad GuardarFamilia(FamiliaEntidad FamiliaObjetoEntidad)
       {
           string CadenaConexion = string.Empty;
           ResultadoEntidad Resultado = new ResultadoEntidad();
           FamiliaAcceso FamiliaAccesoObjeto = new FamiliaAcceso();

           CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

           //if (BuscarFamiliaDuplicada(FamiliaObjetoEntidad) == false)
           //{
           if (FamiliaObjetoEntidad.FamiliaId == 0)
           {
               Resultado = FamiliaAccesoObjeto.InsertarFamilia(FamiliaObjetoEntidad, CadenaConexion);
           }
           else
           {
               Resultado = FamiliaAccesoObjeto.ActualizarFamilia(FamiliaObjetoEntidad, CadenaConexion);
           }
           //}
           //else
           //{
           //    Resultado.ErrorId = (int)ConstantePrograma.Familia.FamiliaConNombreDuplicado;
           //    Resultado.DescripcionError = TextoError.FamiliaConNombreDuplicado;
           //}

           return Resultado;
       }

        public ResultadoEntidad SeleccionarFamilia(FamiliaEntidad FamiliaObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            FamiliaAcceso FamiliaAccesoObjeto = new FamiliaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            Resultado = FamiliaAccesoObjeto.SeleccionarFamilia(FamiliaObjetoEntidad, CadenaConexion);

            return Resultado;
        }



    }
}
