using System;
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
        private int _ErrorId;
        private string _DescripcionError;
        DataSet _ResultadoDatos;
        PreOrdenEntidad _PreOrdenEntidad;

        /// <summary>
        ///     Numero de error, en caso de que haya ocurrido uno. Cero por default.
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
        ///     DataSet con el resultado de la base de datos.
        /// </summary>
        public DataSet ResultadoDatos
        {
            get { return _ResultadoDatos; }
        }

        /// <summary>
        ///     Entidad del proceso.
        /// </summary>
        public PreOrdenEntidad PreOrdenEntidad
        {
            get { return _PreOrdenEntidad; }
            set { _PreOrdenEntidad = value; }
        }

        /// <summary>
        ///     Constructor de la clase
        /// </summary>
        public PreOrdenProceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _ResultadoDatos = null;
            _PreOrdenEntidad = new PreOrdenEntidad();
        }

        #region "Métodos"
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

            /// <summary>
            ///     Realiza una búsqueda del detalle de una preorden.
            /// </summary>
            public void SeleccionarPreOrdenDetalle()
            {
                string CadenaConexion = string.Empty;
                PreOrdenAcceso PreOrdenAcceso = new PreOrdenAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                PreOrdenAcceso.SeleccionarPreOrdenDetalle(_PreOrdenEntidad, CadenaConexion);

                _ErrorId = PreOrdenAcceso.ErrorId;
                _DescripcionError = PreOrdenAcceso.DescripcionError;
            }
        #endregion
    }
}

