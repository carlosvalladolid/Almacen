//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;


using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using Activos.AccesoDatos.Almacen;
using Activos.Comun.Cadenas;
using Activos.Comun.Constante;
using Activos.Entidad.Almacen;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Almacen;
using Activos.Comun.Correo;

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
                bool EnviarCorreoUsuario = false;
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
                    EnviarCorreoUsuario = true; 
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
                    
                    if (EnviarCorreoUsuario)
                        EnviarCorreo(RequisicionObjetoEntidad.AplicacionId, ConstantePrograma.Asunto, RequisicionObjetoEntidad.CorreoElectronico, RequisicionObjetoEntidad.Solicitante, RequisicionObjetoEntidad.Direccion, RequisicionObjetoEntidad.JefeInmediato, RequisicionObjetoEntidad.Puesto, RequisicionObjetoEntidad.Dependencia, RequisicionObjetoEntidad.FechaRequisicion, ConstantePrograma.CorreoNuevoUsuario);
                    


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

                   public void EnviarCorreo( int AplicacionId,string Asunto, string CorreoElectronico, string Solicitante, string Direccion, string JefeInmediato, string Puesto, string Dependencia, string FechaRequisicion, string CorreoAplicacion)
            {
                int Resultado = 0;
                string CuerpoCorreo = string.Empty;
                Servidor CorreoObjeto = new Servidor();
                try
                {
                    CorreoObjeto.Para = CorreoElectronico;
                    CorreoObjeto.Asunto = Asunto;
                    AplicacionId = 2;
                    switch (AplicacionId)
                    {
                        case (Int16)ConstantePrograma.AplicacionId.Almacen:
                            CorreoObjeto.DeParte = ConfigurationManager.AppSettings["Activos.Web.SmtpFrom"].ToString();
                            CorreoObjeto.SmtpHost = ConfigurationManager.AppSettings["Activos.Web.SmtpHost"].ToString();
                            CorreoObjeto.CuentaUsuario = ConfigurationManager.AppSettings["Activos.Web.SmtpLogin"].ToString();
                            CorreoObjeto.Contrasenia = ConfigurationManager.AppSettings["Activos.Web.SmtpPassword"].ToString();
                            CorreoObjeto.Puerto = int.Parse(ConfigurationManager.AppSettings["Activos.Web.SmtpPort"].ToString());
                            break;
                    }
                    CuerpoCorreo = Comun.Correo.CuerpoCorreo.SeleccionarCuerpoCorreoAlmacen(CorreoAplicacion);                  
                    CuerpoCorreo = CuerpoCorreo.Replace("{0}", Solicitante);
                    CuerpoCorreo = CuerpoCorreo.Replace("{1}", Dependencia);
                    CuerpoCorreo = CuerpoCorreo.Replace("{2}", Direccion);
                    CuerpoCorreo = CuerpoCorreo.Replace("{3}", Puesto);
                    CuerpoCorreo = CuerpoCorreo.Replace("{4}", JefeInmediato);
                    CuerpoCorreo = CuerpoCorreo.Replace("{5}", FechaRequisicion);
                    //****************************************
                    CorreoObjeto.Cuerpo = CuerpoCorreo;
                    Resultado = CorreoObjeto.EnviarCorreo();
                }
                catch
                {
                    //  Do nothing;
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

                if (RequisicionObjetoEntidad.RequisicionId == "") RequisicionObjetoEntidad.RequisicionId = Guid.NewGuid().ToString();

                Resultado = RequisicionAccesoObjeto.InsertarRequisicionEncabezadoTemp(RequisicionObjetoEntidad, CadenaConexion);
                
                return Resultado;
            }

            public ResultadoEntidad SeleccionarEmpleado(UsuarioEntidad UsuarioEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Resultado = RequisicionAccesoObjeto.SeleccionarEmpleado(UsuarioEntidad, CadenaConexion);

                return Resultado;
            }

            /// <summary>
            ///    Selecciona la tabla de existencia para la validacion.
            /// </summary>
            public void SeleccionarExistencia()
            {
                string CadenaConexion = string.Empty;
                RequisicionAcceso RequisicionAcceso = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                _ResultadoDatos = RequisicionAcceso.SeleccionarExistencia(_RequisicionEntidad, CadenaConexion);

                return;
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

            public ResultadoEntidad SeleccionarRequisicionDetalleReporte(RequisicionEntidad RequisicionObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Resultado = RequisicionAccesoObjeto.SeleccionarRequisicionDetalleReporte(RequisicionObjetoEntidad, CadenaConexion);

                return Resultado;
            }

            public ResultadoEntidad SeleccionarSolicitanteRequisicionReporte(RequisicionEntidad RequisicionObjetoEntidad)
            {
                string CadenaConexion = string.Empty;
                ResultadoEntidad Resultado = new ResultadoEntidad();
                RequisicionAcceso RequisicionAccesoObjeto = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Resultado = RequisicionAccesoObjeto.SeleccionarSolicitanteRequisicionReporte(RequisicionObjetoEntidad, CadenaConexion);

                return Resultado;
            }

            /// <summary>
            ///     Busca información de las requisiciones que coincidan con los parámetros enviados.
            /// </summary>
            public void SeleccionarRequisicionSalida()
            {
                string CadenaConexion = string.Empty;
                RequisicionAcceso RequisicionAcceso = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                _ResultadoDatos = RequisicionAcceso.SeleccionarRequisicionSalida(_RequisicionEntidad, CadenaConexion);

                return;
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
            public ResultadoEntidad SeleccionarRequisicionEncabezado()
            {
                ResultadoEntidad Resultado = new ResultadoEntidad();
                string CadenaConexion = string.Empty;
                RequisicionAcceso RequisicionAcceso = new RequisicionAcceso();

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

                Resultado = RequisicionAcceso.SeleccionarRequisicionEncabezadoPorRequisicionId(RequisicionEntidad, CadenaConexion);

                return Resultado;
            }

        #endregion
    }
}
