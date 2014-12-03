using System;
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
   public class RequisicionAcceso:Base
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
        public RequisicionAcceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
        }

        #region "Métodos"
            public ResultadoEntidad EliminarRequisicionDetalle(SqlConnection Conexion, SqlTransaction Transaccion, RequisicionEntidad RequisicionEntidadObjeto)
            {
                SqlCommand Comando;
                SqlParameter Parametro;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("EliminarRequisicionDetalleProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Comando.Transaction = Transaccion;

                    Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parametro.Value = RequisicionEntidadObjeto.ProductoId;
                    Comando.Parameters.Add(Parametro);

                    Comando.ExecuteNonQuery();

                    Resultado.ErrorId = (int)ConstantePrograma.Requisicion.EliminadoExitosamente;

                    return Resultado;
                }
                catch (SqlException sqlEx)
                {
                    Resultado.ErrorId = sqlEx.Number;
                    Resultado.DescripcionError = sqlEx.Message;

                    return Resultado;
                }
            }

            public ResultadoEntidad InsertarRequisicionDetalle(RequisicionEntidad RequisicionEntidadObjeto, string CadenaConexion)
            {
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("InsertarRequisicionDetalleProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;


                    Parametro = new SqlParameter("RequisicionId", SqlDbType.VarChar);
                    Parametro.Value = RequisicionEntidadObjeto.RequisicionId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parametro.Value = RequisicionEntidadObjeto.ProductoId;
                    Comando.Parameters.Add(Parametro);               

                    Parametro = new SqlParameter("Cantidad", SqlDbType.Int);
                    Parametro.Value = RequisicionEntidadObjeto.Cantidad;
                    Comando.Parameters.Add(Parametro);

                    Conexion.Open();
                    Comando.ExecuteNonQuery();
                    Conexion.Close();

                    Resultado.ErrorId = (int)ConstantePrograma.Requisicion.RequisicionGuardadoCorrectamente;

                    return Resultado;
                }
                catch (SqlException sqlEx)
                {
                    Resultado.ErrorId = sqlEx.Number;
                    Resultado.DescripcionError = sqlEx.Message;

                    return Resultado;
                }
            }

            public ResultadoEntidad InsertarRequisicionEncabezado(RequisicionEntidad RequisicionEntidadObjeto, string CadenaConexion)
            {
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("InsertarRequisicionEncabezadoProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("RequisicionId", SqlDbType.VarChar);
                    Parametro.Value = RequisicionEntidadObjeto.RequisicionId;
                    Comando.Parameters.Add(Parametro);                             

                    Parametro = new SqlParameter("EmpleadoId", SqlDbType.Int);
                    Parametro.Value = RequisicionEntidadObjeto.EmpleadoId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("JefeId", SqlDbType.Int);
                    Parametro.Value = RequisicionEntidadObjeto.JefeId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("EstatusId", SqlDbType.SmallInt);
                    Parametro.Value = RequisicionEntidadObjeto.EstatusId;
                    Comando.Parameters.Add(Parametro);

                    //Parametro = new SqlParameter("Clave", SqlDbType.VarChar);
                    //Parametro.Value = RequisicionEntidadObjeto.Clave;
                    //Comando.Parameters.Add(Parametro);

                    Conexion.Open();
                    Comando.ExecuteNonQuery();
                    Conexion.Close();

                    Resultado.ErrorId = (int)ConstantePrograma.Requisicion.RequisicionGuardadoCorrectamente;

                    return Resultado;
                }
                catch (SqlException sqlEx)
                {
                    Resultado.ErrorId = sqlEx.Number;
                    Resultado.DescripcionError = sqlEx.Message;

                    return Resultado;
                }
            }

            public ResultadoEntidad SeleccionarEmpleado(RequisicionEntidad RequisicionEntidadObjeto, string CadenaConexion)
            {
                DataSet ResultadoDatos = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("SeleccionarEmpleadoProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("EmpleadoId", SqlDbType.Int);
                    Parametro.Value = RequisicionEntidadObjeto.EmpleadoId;
                    Comando.Parameters.Add(Parametro);

                    //Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
                    //Parametro.Value = RequisicionEntidadObjeto.Nombre;
                    //Comando.Parameters.Add(Parametro);

                    Adaptador = new SqlDataAdapter(Comando);
                    ResultadoDatos = new DataSet();

                    Conexion.Open();
                    Adaptador.Fill(ResultadoDatos);
                    Conexion.Close();

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
         
            public ResultadoEntidad SeleccionarRequisicionDetalle(RequisicionEntidad RequisicionObjetoEntidad, string CadenaConexion)
            {
                DataSet ResultadoDatos = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;
                ResultadoEntidad Resultado = new ResultadoEntidad();

                try
                {
                    Comando = new SqlCommand("SeleccionarRequisicionDetalleProcedimiento", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("RequisicionId", SqlDbType.VarChar);
                    Parametro.Value = RequisicionObjetoEntidad.RequisicionId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("ProductoId", SqlDbType.VarChar);
                    Parametro.Value = RequisicionObjetoEntidad.ProductoId;
                    Comando.Parameters.Add(Parametro);

                    // Parametro = new SqlParameter("Cantidad", SqlDbType.Int);
                    //Parametro.Value = RequisicionObjetoEntidad.Cantidad;
                    //Comando.Parameters.Add(Parametro);

                    Adaptador = new SqlDataAdapter(Comando);
                    ResultadoDatos = new DataSet();

                    Conexion.Open();
                    Adaptador.Fill(ResultadoDatos);
                    Conexion.Close();

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

            /// <summary>
            ///     Busca la información de una requisición para generar la orden de salida.
            /// </summary>
            /// <param name="RequisicionEntidad">Entidad de la requisición.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            //public DataSet SeleccionarRequisicionOrdenSalida(RequisicionEntidad RequisicionEntidad, string CadenaConexion)
            //{
            //    DataSet Resultado = new DataSet();
            //    SqlConnection Conexion = new SqlConnection(CadenaConexion);
            //    SqlCommand Comando;
            //    SqlParameter Parametro;
            //    SqlDataAdapter Adaptador;

            //    try
            //    {
            //        Comando = new SqlCommand("SeleccionarRequisicionOrdenSalida", Conexion);
            //        Comando.CommandType = CommandType.StoredProcedure;

            //        Parametro = new SqlParameter("Clave", SqlDbType.VarChar);
            //        Parametro.Value = RequisicionEntidad.Clave;
            //        Comando.Parameters.Add(Parametro);

            //        Adaptador = new SqlDataAdapter(Comando);

            //        Conexion.Open();
            //        Adaptador.Fill(Resultado);
            //        Conexion.Close();

            //        return Resultado;
            //    }
            //    catch (SqlException Excepcion)
            //    {
            //        _ErrorId = Excepcion.Number;
            //        _DescripcionError = Excepcion.Message;

            //        return Resultado;
            //    }
            //}



        #endregion
    }
}
