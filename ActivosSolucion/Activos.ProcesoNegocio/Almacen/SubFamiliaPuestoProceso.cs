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



       public ResultadoEntidad GuardarSubFamiliaPuesto(SubFamiliaPuestoEntidad SubFamiliaPuestoObjetoEntidad)
       {
           string CadenaConexion = string.Empty;
           SqlTransaction Transaccion;
           SqlConnection Conexion;
           ResultadoEntidad Resultado = new ResultadoEntidad();
           CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

           Conexion = new SqlConnection(CadenaConexion);
           Conexion.Open();

           Transaccion = Conexion.BeginTransaction();

           Resultado = InsertarSubFamiliaPuesto(Conexion, Transaccion, SubFamiliaPuestoObjetoEntidad);
        
           if (Resultado.ErrorId == (int)ConstantePrograma.SubFamilia.SubFamiliaGuardadoCorrectamente)
           {
               Transaccion.Commit();
           }
           else
           {
               Transaccion.Rollback();
           }


           Conexion.Close();

           return Resultado;
       }

       public ResultadoEntidad InsertarSubFamiliaPuesto(SqlConnection Conexion, SqlTransaction Transaccion, SubFamiliaPuestoEntidad SubFamiliaPuestoObjetoEntidad)
       {
           ResultadoEntidad Resultado = new ResultadoEntidad();
           SubFamiliaPuestoAcceso SubFamiliaPuestoAccesoObjeto = new SubFamiliaPuestoAcceso();

           Resultado = SubFamiliaPuestoAccesoObjeto.InsertarSubFamiliaPuesto(Conexion, Transaccion, SubFamiliaPuestoObjetoEntidad);

           return Resultado;
       }
    


    }
}
