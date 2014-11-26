﻿using System;
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
        public bool BuscarSubFamiliaDuplicada(SubFamiliaEntidad SubFamiliaObjetoEntidad)
        {
            bool ExisteMarca = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SubFamiliaEntidad BuscarSubFamiliaObjetoEntidad = new SubFamiliaEntidad();

            BuscarSubFamiliaObjetoEntidad.BuscarNombre = Comparar.EstandarizarCadena(SubFamiliaObjetoEntidad.Nombre);

            Resultado = SeleccionarSubFamilia(BuscarSubFamiliaObjetoEntidad);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["SubFamiliaId"].ToString()) != SubFamiliaObjetoEntidad.SubFamiliaId)
                    ExisteMarca = true;
                else
                    ExisteMarca = false;
            }

            return ExisteMarca;
        }

        protected ResultadoEntidad EliminarSubFamilia(string CadenaSubFamiliaId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            SubFamiliaAcceso SubFamiliaAccesoObjeto = new SubFamiliaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            ResultadoEntidadObjeto = SubFamiliaAccesoObjeto.EliminarSubFamilia(CadenaSubFamiliaId, CadenaConexion);

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad EliminarSubFamilia(SubFamiliaEntidad SubFamiliaObjetoEntidad)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

            // Validar que las subfamilias no contengan información relacionada con otras tablas
            if (TieneRelacionesLaSubFamilia(SubFamiliaObjetoEntidad.CadenaSubFamiliaId))
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.SubFamilia.SubFamiliaTieneRegistrosRelacionados;
                ResultadoEntidadObjeto.DescripcionError = TextoError.SubFamiliaTieneRegistrosRelacionados;
            }
            else
            {
                // Si se pasaron todas las validaciones, hay que borrar la o las subfamilias seleccionadas
                ResultadoEntidadObjeto = EliminarSubFamilia(SubFamiliaObjetoEntidad.CadenaSubFamiliaId);
            }

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad GuardarSubFamilia(SubFamiliaEntidad SubFamiliaObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SubFamiliaAcceso SubFamiliaAccesoObjeto = new SubFamiliaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            if (BuscarSubFamiliaDuplicada(SubFamiliaObjetoEntidad) == false)
            {
                if (SubFamiliaObjetoEntidad.SubFamiliaId == 0)
                {
                    Resultado = SubFamiliaAccesoObjeto.InsertarSubFamilia(SubFamiliaObjetoEntidad, CadenaConexion);
                }
                else
                {
                    Resultado = SubFamiliaAccesoObjeto.ActualizarSubFamilia(SubFamiliaObjetoEntidad, CadenaConexion);
                }
            }
            else
            {
                Resultado.ErrorId = (int)ConstantePrograma.SubFamilia.SubFamiliaConNombreDuplicado;
                Resultado.DescripcionError = TextoError.SubFamiliaConNombreDuplicado;
            }

            return Resultado;
        }

        public ResultadoEntidad SeleccionarSubFamilia(SubFamiliaEntidad SubFamiliaObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SubFamiliaAcceso SubFamiliaAccesoObjeto = new SubFamiliaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            Resultado = SubFamiliaAccesoObjeto.SeleccionarSubFamilia(SubFamiliaObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public bool SeleccionarSubFamiliaFamiliaRelacionadas(string CadenaFamiliaId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            SubFamiliaAcceso SubFamiliaAccesoObjeto = new SubFamiliaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            ResultadoEntidadObjeto = SubFamiliaAccesoObjeto.SeleccionarSubFamiliaFamiliaRelacionadas(CadenaFamiliaId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        protected bool TieneRelacionesLaSubFamilia(string CadenaSubFamiliaId)
        {
            bool TieneRelacionesLaSubFamilia = false;

            // Revisar relaciones con Activo
            // ToDo: Revisar que no contenga información relacionada
            //if (TieneActivosRelacionados(CadenaSubFamiliaId))
            //    return true;

            return TieneRelacionesLaSubFamilia;
        }
    }
}
