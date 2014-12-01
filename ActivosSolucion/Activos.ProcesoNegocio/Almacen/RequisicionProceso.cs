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
   public class RequisicionProceso:Base
    {
       private int _ErrorId;
        private string _DescripcionError;
        DataSet _ResultadoDatos;
        RequisicionEntidad _RequisicionEntidad;

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
        public RequisicionEntidad RequisicionEntidad
        {
            get { return _RequisicionEntidad; }
            set { _RequisicionEntidad = value; }
        }

        /// <summary>
        ///     Constructor de la clase
        /// </summary>
        public RequisicionProceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _ResultadoDatos = null;
            _RequisicionEntidad = new RequisicionEntidad();
        }

        #region "Métodos"
            public ResultadoEntidad AgregarRequisicionDetalle(RequisicionEntidad RequisicionObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
                RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                //****************** aqui entra para revisar que no se agregue la Orden
                ResultadoValidacion = BuscarRequisicionProducto(RequisicionObjetoEntidad);

                if (ResultadoValidacion.ErrorId != 0)
                {
                    return ResultadoValidacion;
                }

                if (RequisicionObjetoEntidad.TemporalRequisicionId == "")
                {
                    RequisicionObjetoEntidad.RequisicionId = Guid.NewGuid().ToString();
                    Resultado = RequisicionAccesoObjeto.InsertarRequisicionDetalle(RequisicionObjetoEntidad, CadenaConexion);
                }
                else
                {
                    Resultado = RequisicionAccesoObjeto.InsertarRequisicionDetalle(RequisicionObjetoEntidad, CadenaConexion);
                }

                return Resultado;
            }

            public ResultadoEntidad AgregarRequisicionEncabezado(RequisicionEntidad RequisicionObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
                RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                if (RequisicionObjetoEntidad.RequisicionId != "")
                {
                    Resultado = RequisicionAccesoObjeto.InsertarRequisicionEncabezado(RequisicionObjetoEntidad, CadenaConexion);
                }
                else
                {
                    // Resultado = RecepcionAccesoObjeto.ActualizarProducto(RecepcionObjetoEntidad, CadenaConexion);
                }

                return Resultado;
            }

            public ResultadoEntidad BuscarRequisicionProducto(RequisicionEntidad RequisicionObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();

                if (RequisicionObjetoEntidad.TemporalRequisicionId != "")
                {

                    if (RequisicionObjetoEntidad.ProductoId != "")
                    {
                        Resultado = SeleccionaRequisicion(RequisicionObjetoEntidad);

                        if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                        {
                        Resultado.ErrorId = (int)ConstantePrograma.Requisicion.RequisicionTieneRegistroDuplicado;
                        Resultado.DescripcionError = TextoError.RequisicionDocumentoDuplicado;
                        }

                    }
                    //return Resultado;
                    else
                    {

                    Resultado.DescripcionError = TextoError.ErrorGenerico;
                    }


                }

                return Resultado;
            }

            public ResultadoEntidad CancelarNuevoRequisicion(RequisicionEntidad RequisicionObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();
                SqlTransaction Transaccion;
                SqlConnection Conexion;

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Conexion = new SqlConnection(CadenaConexion);
                Conexion.Open();

                Transaccion = Conexion.BeginTransaction();

                try
                {
                    //Se elimina la RecepcionDetalle del producto
                    if (RequisicionObjetoEntidad.ProductoId != "")
                    {

                        Resultado = RequisicionAccesoObjeto.EliminarRequisicionDetalle(Conexion, Transaccion, RequisicionObjetoEntidad);

                        if (Resultado.ErrorId == (int)ConstantePrograma.Requisicion.EliminadoExitosamente)
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

            public ResultadoEntidad SeleccionarEmpleado(RequisicionEntidad RequisicionObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Resultado = RequisicionAccesoObjeto.SeleccionarEmpleado(RequisicionObjetoEntidad, CadenaConexion);

                return Resultado;
            }

            public ResultadoEntidad SeleccionaRequisicion(RequisicionEntidad RequisicionObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Resultado = RequisicionAccesoObjeto.SeleccionarRequisicionDetalle(RequisicionObjetoEntidad, CadenaConexion);

                return Resultado;
            }

            /// <summary>
            ///      Busca la información de una requisición para generar la orden de salida.
            /// </summary>
            public void SeleccionarRequisicionOrdenSalida()
            {
                string CadenaConexion = string.Empty;
                RequisicionAcceso RequisicionAcceso = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                _ResultadoDatos = RequisicionAcceso.SeleccionarRequisicionOrdenSalida(_RequisicionEntidad, CadenaConexion);

                _ErrorId = RequisicionAcceso.ErrorId;
                _DescripcionError = RequisicionAcceso.DescripcionError;
            }
        #endregion
    }
}
