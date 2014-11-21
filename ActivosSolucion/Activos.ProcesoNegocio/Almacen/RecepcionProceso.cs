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
   public class RecepcionProceso:Base
    {
       //public ResultadoEntidad AgregarRecepcion(RecepcionEntidad RecepcionObjetoEntidad)
       //{
       //    RecepcionAcceso RecepcionAccesoObjeto = new RecepcionAcceso();
       //    string CadenaConexion = string.Empty;
       //    ResultadoEntidad Resultado = new ResultadoEntidad();
       //    ResultadoEntidad ResultadoRecepcionDuplicado = new ResultadoEntidad();
       //    SqlTransaction Transaccion;
       //    SqlConnection Conexion;

       //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

       //    Conexion = new SqlConnection(CadenaConexion);
       //    Conexion.Open();

       //    Transaccion = Conexion.BeginTransaction();
       //    try
       //    {
       //        if (RecepcionObjetoEntidad.RecepcionId == "")
       //        {
       //            RecepcionObjetoEntidad.RecepcionId = Guid.NewGuid().ToString();

       //            Resultado = RecepcionAccesoObjeto.InsertarRecepcionDetalle(Conexion, Transaccion, RecepcionObjetoEntidad);
                  
       //        }
       //        else
       //        {
       //            //Editar encabezado
       //            // Resultado = RecepcionAccesoObjeto.ActualizarRecepcionEncabezado(Conexion, Transaccion, RecepcionObjetoEntidad);
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

       //public ResultadoEntidad SeleccionaRecepcion(RecepcionEntidad RecepcionObjetoEntidad)
       //{
       //    string CadenaConexion = string.Empty;
       //    RecepcionAcceso RecepcionAccesoObjeto = new RecepcionAcceso();
       //    ResultadoEntidad Resultado = new ResultadoEntidad();

       //    SqlTransaction Transaccion;
       //    SqlConnection Conexion;

       //    CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

       //    Conexion = new SqlConnection(CadenaConexion);
       //    Conexion.Open();

       //    Transaccion = Conexion.BeginTransaction();

       //    try
       //    {
       //        Resultado = RecepcionAccesoObjeto.SeleccionarRecepcionDetalle(Conexion, Transaccion, RecepcionObjetoEntidad);

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




        private int _ErrorId;
        private string _DescripcionError;
        DataSet _ResultadoDatos;
        RecepcionEntidad _RecepcionEntidad;

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
        public RecepcionEntidad RecepcionEntidad
        {
            get { return _RecepcionEntidad; }
            set { _RecepcionEntidad = value; }
        }

        /// <summary>
        ///     Constructor de la clase
        /// </summary>
        public RecepcionProceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _ResultadoDatos = null;
            _RecepcionEntidad = new RecepcionEntidad();
        }

        #region "Métodos"




        #endregion







    }
}
