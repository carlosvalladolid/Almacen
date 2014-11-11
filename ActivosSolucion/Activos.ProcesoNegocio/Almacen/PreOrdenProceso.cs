﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


using Activos.AccesoDatos.Almacen;
using Activos.ProcesoNegocio.Almacen;
using Activos.Comun.Constante;
using Activos.Comun.Cadenas;
using Activos.Entidad.General;
using Activos.Entidad.Almacen;


namespace Activos.ProcesoNegocio.Almacen
{
    public class PreOrdenProceso : Base
    {

        public ResultadoEntidad GuardarPreOrdenCompra(PreOrdenEntidad PreOrdenObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            PreOrdenAcceso PreOrdenAccesoObjeto = new PreOrdenAcceso();
            SqlTransaction Transaccion;
            SqlConnection Conexion;
           
            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);
            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();
            Transaccion = Conexion.BeginTransaction();

            try
            {

                if (PreOrdenObjetoEntidad.PreOrdenId == "")
                {
                    Conexion.Close();
                    //mesnaje de error
                    return Resultado;

                }                   
                Resultado = PreOrdenAccesoObjeto.InsertarPreOrdenEncabezado(Conexion, Transaccion, PreOrdenObjetoEntidad);

                if (Resultado.ErrorId != (int)ConstantePrograma.PreOrden.PreOrdenGuardadoCorrectamente)
                {
                    Transaccion.Rollback();
                    //devolver msg de errp
                    return Resultado;
                }
               
                  Resultado = GuardarPreOrdenDetalle(Conexion, Transaccion, PreOrdenObjetoEntidad);

                 if (Resultado.ErrorId == (int)ConstantePrograma.PreOrden.PreOrdenGuardadoCorrectamente)
                    Transaccion.Commit();                 
                    else
                     Transaccion.Rollback();               
                           
                return Resultado;


            }
            catch (Exception EX)
            {
                Transaccion.Rollback();

                if (Conexion.State == ConnectionState.Open)
                {
                    Conexion.Close();
                }
                Resultado.DescripcionError = EX.Message;
                return Resultado;

            }

         }


        public ResultadoEntidad GuardarPreOrdenDetalle(SqlConnection Conexion, SqlTransaction Transaccion, PreOrdenEntidad PreOrdenObjetoEntidad, DataSet dsPreOrden)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoMovimiento = new ResultadoEntidad();
            PreOrdenProceso PreOrdenProcesoNegocio = new PreOrdenProceso();
            //PreOrdenEntidad PreOrdenObjetoEntidad = new PreOrdenEntidad();
            TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad = new TemporalPreOrdenEntidad();
           
            //Int16 UsuarioId = 0;
            //Int16 ActivoId = 0;

           
            //Se barren los preOrden y se insertan
            foreach (DataRow dtRegistro in dsPreOrden.Tables[0].Rows)
            {
                PreOrdenObjetoEntidad.PreOrdenId = TemporalPreOrdenObjetoEntidad.TemporalPreOrdenId;
                PreOrdenObjetoEntidad.ProductoId = string.Format(dtRegistro["ProductoId"].ToString());
                PreOrdenObjetoEntidad.Cantidad = TemporalPreOrdenObjetoEntidad.Cantidad;
           
                Resultado = PreOrdenProcesoNegocio.GuardarPreOrdenDetalle(Conexion, Transaccion, PreOrdenObjetoEntidad);

                //Si el activo se guardo correctamente se obtiene su ID, se inserta el movimiento de alta y se inserta los accesorios
                if (Resultado.ErrorId == (int)ConstantePrograma.PreOrden.PreOrdenGuardadoCorrectamente)
                {
                   
                
                }
                else
                {
                    break;
                }
            }

            return Resultado;
        }



        public ResultadoEntidad GuardarPreOrdenDetalle(SqlConnection Conexion, SqlTransaction Transaccion, PreOrdenEntidad PreOrdenObjetoEntidad)
           {
               ResultadoEntidad Resultado = new ResultadoEntidad();
               PreOrdenAcceso PreOrdenAccesoObjeto = new PreOrdenAcceso();

               Resultado = PreOrdenAccesoObjeto.InsertarPreOrdenDetalle(Conexion, Transaccion, PreOrdenObjetoEntidad);

               return Resultado;
           }
       


    }
}

