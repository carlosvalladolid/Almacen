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
    public class TemporalPreOrdenProceso : Base
    {
        //public ResultadoEntidad AgregarTemporalPreOrden(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
        //{
        //    TemporalPreOrdenAcceso TemporalPreOrdenAccesoObjeto = new TemporalPreOrdenAcceso();
        //    string CadenaConexion = string.Empty;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    ResultadoEntidad ResultadoPreOrdenDuplicado = new ResultadoEntidad();
        //    SqlTransaction Transaccion;
        //    SqlConnection Conexion;

        //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

        //    ////****************** aqui entra para revisar que no se agregue la PreOrden
        //    //    ResultadoPreOrdenDuplicado = ValidarPreOrdenDuplicado(TemporalPreOrdenObjetoEntidad);

        //    //    if (ResultadoPreOrdenDuplicado.ErrorId != 0)
        //    //    {
        //    //        return ResultadoPreOrdenDuplicado;
        //    //    }

        //    ////**************************************************************************************            
        //    Conexion = new SqlConnection(CadenaConexion);
        //    Conexion.Open();

        //    Transaccion = Conexion.BeginTransaction();
        //    try
        //    {
        //        if (TemporalPreOrdenObjetoEntidad.PreOrdenId == "")
        //        {
        //            TemporalPreOrdenObjetoEntidad.PreOrdenId = Guid.NewGuid().ToString();

        //            Resultado = TemporalPreOrdenAccesoObjeto.InsertarTemporalPreOrdenEncabezadoTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
        //        }
        //        else
        //        {
        //            //Editar encabezado
        //            Resultado = TemporalPreOrdenAccesoObjeto.ActualizarPreOrdenEncabezadoTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
        //        }

        //        if (Resultado.ErrorId == (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenGuardadoCorrectamente)
        //        {
        //            Resultado = TemporalPreOrdenAccesoObjeto.SeleccionarPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);

        //            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
        //            {

        //                Resultado.ErrorId = ((int)ConstantePrograma.TemporalPreOrden.ClaveDuplicado);
        //                //Se edita el poducto
        //                // Resultado = TemporalPreOrdenAccesoObjeto.ActualizarPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
        //            }
        //            else
        //            {
        //                //Se inserta el poducto
        //                Resultado = TemporalPreOrdenAccesoObjeto.InsertarTemporalPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
        //            }

        //            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenGuardadoCorrectamente)
        //            {
        //                Transaccion.Commit();
        //            }
        //            else
        //            {
        //                Transaccion.Rollback();
        //            }

        //        }
        //        else
        //        {
        //            Transaccion.Rollback();
        //        }

        //        Conexion.Close();

        //        return Resultado;
        //    }
        //    catch (Exception EX)
        //    {
        //        Transaccion.Rollback();

        //        if (Conexion.State == ConnectionState.Open)
        //        {
        //            Conexion.Close();
        //        }
        //        Resultado.DescripcionError = EX.Message;
        //        return Resultado;

        //    }
        //}






        public ResultadoEntidad AgregarTemporalPreOrden(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
        {
            TemporalPreOrdenAcceso TemporalPreOrdenAccesoObjeto = new TemporalPreOrdenAcceso();
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoPreOrdenDuplicado = new ResultadoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            ////****************** aqui entra para revisar que no se agregue la PreOrden
            //    ResultadoPreOrdenDuplicado = ValidarPreOrdenDuplicado(TemporalPreOrdenObjetoEntidad);

            //    if (ResultadoPreOrdenDuplicado.ErrorId != 0)
            //    {
            //        return ResultadoPreOrdenDuplicado;
            //    }

            ////**************************************************************************************            
            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();
            try
            {
                if (TemporalPreOrdenObjetoEntidad.TemporalPreOrdenId !="0")
                {
                    Resultado = TemporalPreOrdenAccesoObjeto.SeleccionarPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);

                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                    {

                        Resultado.ErrorId = ((int)ConstantePrograma.TemporalPreOrden.ClaveDuplicado);
                        //Se edita el poducto
                        // Resultado = TemporalPreOrdenAccesoObjeto.ActualizarPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
                    }
                    else
                    {
                        //Se inserta el poducto
                        Resultado = TemporalPreOrdenAccesoObjeto.InsertarTemporalPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);
                    }

                    if (Resultado.ErrorId == (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenGuardadoCorrectamente)
                    {
                        Transaccion.Commit();
                    }
                    else
                    {
                        Transaccion.Rollback();
                    }

                }
                else
                {
                    Transaccion.Rollback();
                }

                Conexion.Close();

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



        public ResultadoEntidad InsertarTemporalPreOrdenEncabezado(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalPreOrdenAcceso TemporalPreOrdenAccesoObjeto = new TemporalPreOrdenAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            if (TemporalPreOrdenObjetoEntidad.PreOrdenId == "")
            {
                TemporalPreOrdenObjetoEntidad.PreOrdenId = Guid.NewGuid().ToString();

                Resultado = TemporalPreOrdenAccesoObjeto.InsertarTemporalPreOrdenEncabezadoTemp(TemporalPreOrdenObjetoEntidad, CadenaConexion);
            }
            return Resultado;
        }









        public ResultadoEntidad SeleccionarPreOrdenDetalleTemp(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            TemporalPreOrdenAcceso TemporalPreOrdenAccesoObjeto = new TemporalPreOrdenAcceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            try
            {
                Resultado = TemporalPreOrdenAccesoObjeto.SeleccionarPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);

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

        public ResultadoEntidad CancelarNuevoPreOrden(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalPreOrdenAcceso TemporalPreOrdenAccesoObjeto = new TemporalPreOrdenAcceso();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            try
            {

                //Se elimina la PreOrden temporal
                if (TemporalPreOrdenObjetoEntidad.ProductoId!= "")
                {

                    Resultado = TemporalPreOrdenAccesoObjeto.EliminarPreOrdenDetalleTemp(Conexion, Transaccion, TemporalPreOrdenObjetoEntidad);

                    if (Resultado.ErrorId == (int)ConstantePrograma.TemporalPreOrden.TemporalPreOrdenEliminadoCorrectamente)
                    {
                        Transaccion.Commit();
                    }
                    else
                    {
                        Transaccion.Rollback();
                    }
                }
                else
                {
                    Transaccion.Rollback();
                }
                Conexion.Close();

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

        //public ResultadoEntidad ValidarPreOrdenDuplicado(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();

        //    if (BuscarPreOrden(TemporalPreOrdenObjetoEntidad) == true)
        //    {   // Se busca si ya existe una preorden 
        //        Resultado.ErrorId = (int)ConstantePrograma.TemporalPreOrden.ClaveDuplicado;
        //        Resultado.DescripcionError = TextoError.PreOrdenDuplicado;
        //        return Resultado;
        //    }

        //}


        //public bool BuscarPreOrden(TemporalPreOrdenEntidad TemporalPreOrdenObjetoEntidad)
        //{
        //    bool ExistePreOrden = false;
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    PreOrdenEntidad PreOrdenObjetoEntidad = new PreOrdenEntidad();

        //    if (TemporalPreOrdenObjetoEntidad.ProductoId != "")
        //    {
        //        //PreOrdenObjetoEntidad.ProductoId = TemporalPreOrdenObjetoEntidad.ProductoId;

        //        Resultado = SeleccionarPreOrdenDetalleTemp(TemporalPreOrdenObjetoEntidad);

        //        if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
        //            ExistePreOrden = true;
        //    }

        //    return ExistePreOrden;
        //}



    }
}