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
            public ResultadoEntidad AgregarTemporalRequisicion(RequisicionEntidad RequisicionObjetoEntidad)
            {
                RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                ResultadoEntidad ResultadoRequisicionDuplicado = new ResultadoEntidad();
                SqlTransaction Transaccion;
                SqlConnection Conexion;

                if (!ValidarProductoRequisicion(RequisicionObjetoEntidad))
                    return Resultado;

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Conexion = new SqlConnection(CadenaConexion);
                Conexion.Open();

                Transaccion = Conexion.BeginTransaction();

                try
                {
                    Resultado = RequisicionAccesoObjeto.SeleccionarRequisicionDetalleTemp(Conexion, Transaccion, RequisicionObjetoEntidad);

                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                        Resultado.ErrorId = ((int)ConstantePrograma.Requisicion.RequisicionTieneRegistroDuplicado);
                    else
                        Resultado = RequisicionAccesoObjeto.InsertarRequisicionDetalleTemp(Conexion, Transaccion, RequisicionObjetoEntidad);

                    if (Resultado.ErrorId == (int)ConstantePrograma.Requisicion.RequisicionGuardadoCorrectamente)
                        Transaccion.Commit();
                    else
                        Transaccion.Rollback();

                    Conexion.Close();

                    return Resultado;
                }
                catch (Exception EX)
                {
                    Transaccion.Rollback();

                    if (Conexion.State == ConnectionState.Open)
                        Conexion.Close();

                    Resultado.DescripcionError = EX.Message;
                    return Resultado;
                }
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

            public ResultadoEntidad GuardarRequisicion(RequisicionEntidad RequisicionObjetoEntidad)
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

                    if (RequisicionObjetoEntidad.RequisicionId == "")
                    {
                        Conexion.Close();
                        //mesnaje de error
                        return Resultado;

                    }
                    Resultado = RequisicionAccesoObjeto.InsertarRequisicionEncabezado(Conexion, Transaccion, RequisicionObjetoEntidad);

                    if (Resultado.ErrorId != (int)ConstantePrograma.Requisicion.RequisicionGuardadoCorrectamente)
                    {
                        Transaccion.Rollback();
                        //devolver msg de errp
                        return Resultado;
                    }

                    Resultado = GuardarRequisicionDetalle(Conexion, Transaccion, RequisicionObjetoEntidad);
                   
                    if (Resultado.ErrorId == (int)ConstantePrograma.Requisicion.RequisicionGuardadoCorrectamente)
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

            public ResultadoEntidad GuardarRequisicionDetalle(SqlConnection Conexion, SqlTransaction Transaccion, RequisicionEntidad RequisicionObjetoEntidad)
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

                Resultado = RequisicionAccesoObjeto.InsertarRequisicionDetalle(Conexion, Transaccion, RequisicionObjetoEntidad);

                return Resultado;
            }

            public ResultadoEntidad InsertarTemporalRequisicionEncabezado(RequisicionEntidad RequisicionObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                if (RequisicionObjetoEntidad.RequisicionId == "")
                {
                    RequisicionObjetoEntidad.RequisicionId = Guid.NewGuid().ToString();

                    Resultado = RequisicionAccesoObjeto.InsertarRequisicionEncabezadoTemp(RequisicionObjetoEntidad, CadenaConexion);
                }
                return Resultado;
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

            public bool ValidarProductoRequisicion(RequisicionEntidad RequisicionObjetoEntidad)
            {
                bool Resultado = true;
                int ExistenciaProducto = 0;
                int MaximoPermitido = 0;
                AlmacenProceso ProductoProceso;

                // Validar que se haya enviado un identificador de la requisición
                if (RequisicionObjetoEntidad.RequisicionId == "")
                {
                    _ErrorId = (int)TextoError.Requisicion.SinRequisicionId;
                    _DescripcionError = TextoError.SinRequisicionId;

                    return false;
                }

                // Validar que haya existencia del producto
                ProductoProceso = new AlmacenProceso();

                ExistenciaProducto = ProductoProceso.SeleccionarProductoExistencia(RequisicionObjetoEntidad.ProductoId);

                if (ProductoProceso.ErrorId != 0)
                {
                    _ErrorId = ProductoProceso.ErrorId;
                    _DescripcionError = ProductoProceso.DescripcionError;

                    return false;
                }

                if (ExistenciaProducto < RequisicionObjetoEntidad.Cantidad)
                {
                    _ErrorId = (int)TextoError.Requisicion.ExistenciaInsuficiente;
                    _DescripcionError = TextoError.ExistenciaInsuficiente;

                    return false;
                }

                // Validar que se pida una cantidad menor o igual al máximo permitido
                ProductoProceso = new AlmacenProceso();

                MaximoPermitido = ProductoProceso.SeleccionarProductoMaximoPermitido(RequisicionObjetoEntidad.ProductoId);

                if (ProductoProceso.ErrorId != 0)
                {
                    _ErrorId = ProductoProceso.ErrorId;
                    _DescripcionError = ProductoProceso.DescripcionError;

                    return false;
                }

                if (RequisicionObjetoEntidad.Cantidad > MaximoPermitido)
                {
                    _ErrorId = (int)TextoError.Requisicion.CantidadArribaDelMaximo;
                    _DescripcionError = TextoError.CantidadArribaDelMaximo;

                    return false;
                }

                return Resultado;
            }
            /// <summary>
            ///     Obtiene la info de la tabla RequisicionEncabezado
            /// </summary>
            public ResultadoEntidad SeleccionarPreOrdenEncabezado()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                string CadenaConexion = string.Empty;
                RequisicionAcceso PreOrdenAcceso = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                //Resultado = RequisicionAcceso.(_PreOrdenEntidad, CadenaConexion);

                return Resultado;
            }
        #endregion
    }
}
