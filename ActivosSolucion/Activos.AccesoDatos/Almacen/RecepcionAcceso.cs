﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Almacen;
using Activos.Comun.Constante;

namespace Activos.AccesoDatos.Almacen
{
 public   class RecepcionAcceso:Base
    {

 protected int _ErrorId;
        protected string _DescripcionError;

        /// <summary>
        ///     Número de error, en caso de que haya ocurrido uno. Cero por default.
        /// </summary>
        public int ErrorId
        {
            get { return _ErrorId; }
        }

        /// <summary>
        ///     Descripción de error, en caso de que haya ocurrido uno. Empty por default.
        /// </summary>
        public string DescripcionError
        {
            get { return _DescripcionError; }
        }

        /// <summary>
        ///     Constructor de la clase.
        /// </summary>
        public RecepcionAcceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
        }

        #region "Métodos"

        public ResultadoEntidad InsertarRecepcionDetalle(SqlConnection Conexion, SqlTransaction Transaccion, RecepcionEntidad RecepcionEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarRecepcionDetalleProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("RecepcionId", SqlDbType.VarChar);
                Parametro.Value = RecepcionEntidadObjeto.RecepcionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                Parametro.Value = RecepcionEntidadObjeto.ProductoId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("PrecioUnitario", SqlDbType.Decimal);
                Parametro.Value = RecepcionEntidadObjeto.PrecioUnitario;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Cantidad", SqlDbType.Int);
                Parametro.Value = RecepcionEntidadObjeto.Cantidad;
                Comando.Parameters.Add(Parametro);

                Comando.ExecuteNonQuery();
                Resultado.ErrorId = (int)ConstantePrograma.Recepcion.RecepcionGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad InsertarRecepcionEncabezado(SqlConnection Conexion, SqlTransaction Transaccion, RecepcionEntidad RecepcionEntidadObjeto)
        {
            SqlCommand Comando;
            SqlParameter Parametro;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("InsertarPreOrdenEncabezadoTempProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("RecepcionId", SqlDbType.VarChar);
                Parametro.Value = RecepcionEntidadObjeto.RecepcionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("OrdenId", SqlDbType.VarChar);
                Parametro.Value = RecepcionEntidadObjeto.OrdenId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EmpleadoId", SqlDbType.Int);
                Parametro.Value = RecepcionEntidadObjeto.EmpleadoId;
                Comando.Parameters.Add(Parametro);
                
                Parametro = new SqlParameter("JefeId", SqlDbType.Int);
                Parametro.Value = RecepcionEntidadObjeto.JefeId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ProveedorId", SqlDbType.SmallInt);
                Parametro.Value = RecepcionEntidadObjeto.ProveedorId;
                Comando.Parameters.Add(Parametro);


                Parametro = new SqlParameter("TipoDocumentoId", SqlDbType.SmallInt);
                Parametro.Value = RecepcionEntidadObjeto.TipoDocumentoId;
                Comando.Parameters.Add(Parametro);


                Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                Parametro.Value = RecepcionEntidadObjeto.EstatusId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("Clave", SqlDbType.VarChar);
                Parametro.Value = RecepcionEntidadObjeto.Clave;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("FechaDocumento", SqlDbType.SmallDateTime);
                Parametro.Value = RecepcionEntidadObjeto.FechaDocumento;
                Comando.Parameters.Add(Parametro);


                Comando.ExecuteNonQuery();

                Resultado.ErrorId = (int)ConstantePrograma.Recepcion.RecepcionGuardadoCorrectamente;

                return Resultado;
            }
            catch (SqlException sqlEx)
            {
                Resultado.ErrorId = sqlEx.Number;
                Resultado.DescripcionError = sqlEx.Message;

                return Resultado;
            }
        }

        public ResultadoEntidad SeleccionarRecepcionDetalle(SqlConnection Conexion, SqlTransaction Transaccion, RecepcionEntidad RecepcionObjetoEntidad)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarRecepcionDetalleProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Transaction = Transaccion;

                Parametro = new SqlParameter("RecepcionId", SqlDbType.VarChar);
                Parametro.Value = RecepcionObjetoEntidad.RecepcionId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                Parametro.Value = RecepcionObjetoEntidad.ProductoId;
                Comando.Parameters.Add(Parametro);

                Adaptador = new SqlDataAdapter(Comando);
                ResultadoDatos = new DataSet();

                Adaptador.Fill(ResultadoDatos);

                Resultado.ResultadoDatos = ResultadoDatos;

                return Resultado;
            }
            catch (SqlException Excepcion)
            {
                Resultado.ErrorId = Excepcion.Number;
                Resultado.DescripcionError = Excepcion.Message;

                return Resultado;
            }
        }













        #endregion



    }
}