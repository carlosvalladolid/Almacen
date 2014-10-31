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
   public class MarcaProceso:Base
    {


        //public ResultadoEntidad GuardarMarca(MarcaEntidad MarcaObjetoEntidad)
        //{
        //    string CadenaConexion = string.Empty;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    MarcaAcceso MarcaAccesoObjeto = new MarcaAcceso();

        //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

        //    if (BuscarMarcaDuplicada(MarcaObjetoEntidad) == false)
        //    {
        //        if (MarcaObjetoEntidad.MarcaId == 0)
        //        {
        //            Resultado = MarcaAccesoObjeto.InsertarMarca(MarcaObjetoEntidad, CadenaConexion);
        //        }
        //        else
        //        {
        //            Resultado = MarcaAccesoObjeto.ActualizarMarca(MarcaObjetoEntidad, CadenaConexion);
        //        }
        //    }
        //    else
        //    {
        //        Resultado.ErrorId = (int)ConstantePrograma.Marca.MarcaConNombreDuplicado;
        //        Resultado.DescripcionError = TextoError.MarcaConNombreDuplicado;
        //    }

        //    return Resultado;
        //}

        public ResultadoEntidad SeleccionarMarca(MarcaEntidad MarcaObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MarcaAcceso MarcaAccesoObjeto = new MarcaAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            Resultado = MarcaAccesoObjeto.SeleccionarMarca(MarcaObjetoEntidad, CadenaConexion);

            return Resultado;
        }

    }
}