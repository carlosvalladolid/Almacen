using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using Activos.AccesoDatos.Seguridad;
using Activos.Comun.Constante;
using Activos.Comun.Correo;
using Activos.Comun.Seguridad;
using Activos.Entidad.General;
using Activos.Entidad.Seguridad;
using Activos.ProcesoNegocio.Activos;
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.ProcesoNegocio.Seguridad
{
    public class UsuarioProceso : Base
    {
        public string Password;
        public string NuevaContrasenia4;
        public string Texto;

        public ResultadoEntidad EliminarUsuario(string CadenaUsuarioId)
        {
            string CadenaConexion = string.Empty;
            string[] ArrayUsuario;
            SqlTransaction Transaccion;
            SqlConnection Conexion;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            UsuarioAcceso UsuarioAccesoObjeto = new UsuarioAcceso();

            if (CadenaUsuarioId != "")
            {
                ArrayUsuario = CadenaUsuarioId.Split(',');

                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

                Conexion = new SqlConnection(CadenaConexion);
                Conexion.Open();

                Transaccion = Conexion.BeginTransaction();

                foreach (string UsuarioId in ArrayUsuario)
                {
                    if (UsuarioId != "")
                    {
                        ResultadoEntidadObjeto = EliminarUsuario(Conexion, Transaccion, Int16.Parse(UsuarioId));

                        if (ResultadoEntidadObjeto.ErrorId != (int)ConstantePrograma.Usuario.EliminacionExitosa)
                        {
                            Transaccion.Rollback();
                            break;
                        }
                    }
                }

                if (ResultadoEntidadObjeto.ErrorId == (int)ConstantePrograma.Usuario.EliminacionExitosa)
                    Transaccion.Commit();
            }

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad EliminarUsuario(UsuarioEntidad UsuarioEntidadObjeto)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();

            // Validar que no se borre la cuenta de administrador
            if (EsCuentaDeAdministrador(UsuarioEntidadObjeto.CadenaUsuarioId))
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Usuario.CuentaUsuarioAdministrador;
                ResultadoEntidadObjeto.DescripcionError = TextoError.UsuarioCuentaAdministrador;
            }
            else
            {
                // Validar que no se borre el usuario a sí mismo
                if (EsUsuarioIdElMismoABorrar(UsuarioEntidadObjeto.UsuarioId, UsuarioEntidadObjeto.CadenaUsuarioId))
                {
                    ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Usuario.UsuarioQuiereBorrarseAsiMismo;
                    ResultadoEntidadObjeto.DescripcionError = TextoError.UsuarioQuiereBorrarseAsiMismo;
                }
                else
                {
                    // Validar que las cuentas de usuario no contengan información relacionada con otras tablas
                    if (TieneRelacionesElUsuario(UsuarioEntidadObjeto.CadenaUsuarioId))
                    {
                        ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.Usuario.UsuarioTieneRegistrosRelacionados;
                        ResultadoEntidadObjeto.DescripcionError = TextoError.UsuarioTieneRegistrosRelacionados;
                    }
                    else
                    {
                        // Si se pasaron todas las validaciones, hay que borrar el o los usuarios seleccionados
                        ResultadoEntidadObjeto = EliminarUsuario(UsuarioEntidadObjeto.CadenaUsuarioId);
                    }
                }
            }

            return ResultadoEntidadObjeto;
        }

        protected ResultadoEntidad EliminarUsuario(SqlConnection Conexion, SqlTransaction Transaccion, Int16 UsuarioId)
        {
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            UsuarioAcceso UsuarioAccesoObjeto = new UsuarioAcceso();

            ResultadoEntidadObjeto = UsuarioAccesoObjeto.EliminarUsuario(Conexion, Transaccion, UsuarioId);

            return ResultadoEntidadObjeto;
        }

        public void EnviarCorreo(Int16 AplicacionId, string CuentaUsuario, string Contrasenia, string Asunto, string CorreoAplicacion)
        {
            int Resultado = 0;
            string CuerpoCorreo = string.Empty;
            Servidor CorreoObjeto = new Servidor();

            try
            {
                CorreoObjeto.Para = CuentaUsuario;
                CorreoObjeto.Asunto = Asunto;

                switch (AplicacionId)
                {
                    case (Int16)ConstantePrograma.AplicacionId.Activos:
                        CorreoObjeto.DeParte = ConfigurationManager.AppSettings["Activos.Web.SmtpFrom"].ToString();
                        CorreoObjeto.SmtpHost = ConfigurationManager.AppSettings["Activos.Web.SmtpHost"].ToString();
                        CorreoObjeto.CuentaUsuario = ConfigurationManager.AppSettings["Activos.Web.SmtpLogin"].ToString();
                        CorreoObjeto.Contrasenia = ConfigurationManager.AppSettings["Activos.Web.SmtpPassword"].ToString();
                        CorreoObjeto.Puerto = int.Parse(ConfigurationManager.AppSettings["Activos.Web.SmtpPort"].ToString());
                        break;
                }

                CuerpoCorreo = Comun.Correo.CuerpoCorreo.SeleccionarCuerpoCorreo(CorreoAplicacion);
                CuerpoCorreo = CuerpoCorreo.Replace("{0}", CuentaUsuario);
                CuerpoCorreo = CuerpoCorreo.Replace("{1}", Contrasenia);
                CorreoObjeto.Cuerpo = CuerpoCorreo;

                Resultado = CorreoObjeto.EnviarCorreo();
            }
            catch
            {
                // Do nothing
            }
        }

        protected bool EsCuentaDeAdministrador(string CadenaUsuarioId)
        {
            string AdministradorUsuarioId = string.Empty;
            Regex ExpresionRegular;

            AdministradorUsuarioId = ConfigurationManager.AppSettings["Activos.Web.SystemAdministratorUserId"].ToString();

            ExpresionRegular = new Regex("," + AdministradorUsuarioId + ",");

            // Validar que no venga el Id del administrador dentro del conjunto de Ids de usuarios
            return ExpresionRegular.IsMatch(CadenaUsuarioId);
        }

        protected bool EsUsuarioIdElMismoABorrar(Int16 UsuarioId, string CadenaUsuarioId)
        {
            string UsuarioIdElMismoABorrar = string.Empty;
            Regex ExpresionRegular;

            ExpresionRegular = new Regex("," + UsuarioId.ToString() + ",");

            // Validar que no venga el Id del usuario actual dentro del conjunto de Ids de usuarios a eliminar
            return ExpresionRegular.IsMatch(CadenaUsuarioId);
        }

        public bool ExisteCuentaUsuario(string CuentaUsuario)
        {
            bool ExisteCuentaUsuario = false;
            ResultadoEntidad ResultadoObjetoEntidad = new ResultadoEntidad();
            UsuarioEntidad UsuarioObjetoEntidad = new UsuarioEntidad();

            UsuarioObjetoEntidad.CuentaUsuario = CuentaUsuario;

            ResultadoObjetoEntidad = SeleccionarUsuario(UsuarioObjetoEntidad);

            if (!(ResultadoObjetoEntidad.ResultadoDatos.Tables[0].Rows.Count == 0))
                ExisteCuentaUsuario = true;

            return ExisteCuentaUsuario;
        }

        protected void GuardarCookie(string Cookie, string Valor, HttpContext Contexto)
        {
            Contexto.Response.Cookies[Cookie].Value = Valor;
            Contexto.Response.Cookies[Cookie].Expires = DateTime.Now.AddDays(100);
        }

        public ResultadoEntidad IdentificarUsuario(UsuarioEntidad UsuarioObjetoEntidad)
        {
            string ContraseniaEncriptada = string.Empty;
            string DatabasePassword = string.Empty;
            string CadenaConexion = string.Empty;
            HttpContext Contexto = HttpContext.Current;
            ResultadoEntidad ResultadoObjetoEntidad = new ResultadoEntidad();
            UsuarioAcceso UsuarioObjetoAcceso = new UsuarioAcceso();
            

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

            ResultadoObjetoEntidad = UsuarioObjetoAcceso.SeleccionarUsuario(UsuarioObjetoEntidad, CadenaConexion);

            if (ResultadoObjetoEntidad.ErrorId == (int)ConstantePrograma.IdentificarUsuario.ValorPorDefecto)
            {
                if (ResultadoObjetoEntidad.ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    ResultadoObjetoEntidad.ErrorId = (int)ConstantePrograma.IdentificarUsuario.UsuarioContraseniaIncorrecta;
                }
                else
                {
                    if (UsuarioObjetoEntidad.Contrasenia == ConfigurationManager.AppSettings["Activos.Web.RememberPassword"].ToString())
                        ContraseniaEncriptada = ObtenerContrasenia();
                    else
                        ContraseniaEncriptada = EncriptarTexto.Encriptar(UsuarioObjetoEntidad.Contrasenia);
                    
                    DatabasePassword = ResultadoObjetoEntidad.ResultadoDatos.Tables[0].Rows[0]["Contrasenia"].ToString();

                    if (ContraseniaEncriptada == DatabasePassword)
                    {
                        UsuarioObjetoEntidad.UsuarioId = Int16.Parse(ResultadoObjetoEntidad.ResultadoDatos.Tables[0].Rows[0]["UsuarioId"].ToString());
                        UsuarioObjetoEntidad.AplicacionId = (int)ConstantePrograma.AplicacionId.Activos;
                        UsuarioObjetoEntidad.RolId = Int16.Parse(ResultadoObjetoEntidad.ResultadoDatos.Tables[0].Rows[0]["RolId"].ToString());
                        UsuarioObjetoEntidad.EstatusId = Int16.Parse(ResultadoObjetoEntidad.ResultadoDatos.Tables[0].Rows[0]["EstatusId"].ToString());
                        UsuarioObjetoEntidad.Nombre = ResultadoObjetoEntidad.ResultadoDatos.Tables[0].Rows[0]["Nombre"].ToString();
                        UsuarioObjetoEntidad.ApellidoPaterno = ResultadoObjetoEntidad.ResultadoDatos.Tables[0].Rows[0]["ApellidoPaterno"].ToString();
                        UsuarioObjetoEntidad.ApellidoMaterno = ResultadoObjetoEntidad.ResultadoDatos.Tables[0].Rows[0]["ApellidoMaterno"].ToString();

                        if (UsuarioObjetoEntidad.EstatusId == (int)ConstantePrograma.EstatusUsuario.Activo)
                        {
                            BoletoAutenticacion.IdentificarUsuario(UsuarioObjetoEntidad.CuentaUsuario, Contexto.Session.Timeout);
                            GuardarCookie(ConstantePrograma.CookieCuenta, UsuarioObjetoEntidad.CuentaUsuario, Contexto);
                            Contexto.Session.Add("UsuarioEntidad", UsuarioObjetoEntidad);

                            if (UsuarioObjetoEntidad.RecordarContrasenia)
                            {
                                GuardarCookie(ConstantePrograma.CookieContrasenia, DatabasePassword, Contexto);
                                GuardarCookie(ConstantePrograma.CookieRecordar, ConfigurationManager.AppSettings["Activos.Web.RememberPassword"].ToString(), Contexto);
                            }
                            else
                            {
                                GuardarCookie(ConstantePrograma.CookieContrasenia, "", Contexto);
                                GuardarCookie(ConstantePrograma.CookieRecordar, "", Contexto);
                            }
                        }
                        else
                        {
                            ResultadoObjetoEntidad.ErrorId = (int)ConstantePrograma.IdentificarUsuario.UsuarioInactivo;
                        }
                    }
                    else
                    {
                        ResultadoObjetoEntidad.ErrorId = (int)ConstantePrograma.IdentificarUsuario.UsuarioContraseniaIncorrecta;
                    }
                }
            }

            return ResultadoObjetoEntidad;
        }

        public ResultadoEntidad GuardarUsuario(UsuarioEntidad UsuarioObjetoEntidad)
        {
            bool EnviarCorreoUsuario = false;
            string CadenaConexion = string.Empty;
            string Contrasenia = string.Empty;
            ResultadoEntidad ResultadoObjetoEntidad = new ResultadoEntidad();
            UsuarioAcceso UsuarioObjetoAcceso = new UsuarioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

            if (UsuarioObjetoEntidad.UsuarioId == 0)
            {
                // Validar que el usuario no se repita
                if (ExisteCuentaUsuario(UsuarioObjetoEntidad.CuentaUsuario))
                {
                    ResultadoObjetoEntidad.ErrorId = (int)ConstantePrograma.Usuario.ExisteCuentaUsuario;
                    ResultadoObjetoEntidad.DescripcionError = TextoError.UsuarioCuentaExiste.Replace("{0}", UsuarioObjetoEntidad.CuentaUsuario);
                }
                else
                {
                    // Hay que generar una contraseña aleatoria
                    Contrasenia = CadenaAleatoria.GenerarCadenaAleatoria(5);
                    UsuarioObjetoEntidad.Contrasenia = EncriptarTexto.Encriptar(Contrasenia);
                    EnviarCorreoUsuario = true;

                    ResultadoObjetoEntidad = UsuarioObjetoAcceso.InsertarUsuario(UsuarioObjetoEntidad, CadenaConexion);
                }
            }
            else
            {
                ResultadoObjetoEntidad = UsuarioObjetoAcceso.ActualizarUsuario(UsuarioObjetoEntidad, CadenaConexion);
            }

            if (ResultadoObjetoEntidad.ErrorId == (int)ConstantePrograma.Usuario.GuardadoExitoso)
            {
                // Hay que enviar un correo electrónico con la contraseña generada
                if (EnviarCorreoUsuario)
                    EnviarCorreo(UsuarioObjetoEntidad.AplicacionId, UsuarioObjetoEntidad.CuentaUsuario, Contrasenia, ConstantePrograma.AsuntoUsuarioNuevo, ConstantePrograma.CorreoNuevoUsuario);
            }

            return ResultadoObjetoEntidad;
        }

        protected string ObtenerContrasenia()
        {
            string RememberPassword = string.Empty;

            RememberPassword = UsuarioProceso.ObtenerCookie(ConstantePrograma.CookieContrasenia, HttpContext.Current);

            return RememberPassword;
        }

        public static string ObtenerCookie(string Cookie, HttpContext Contexto)
        {
            if (!(Contexto.Request.Cookies[Cookie] == null))
                return Contexto.Server.HtmlEncode(Contexto.Request.Cookies[Cookie].Value);
            else
                return string.Empty;
        }

        public ResultadoEntidad RecuperarContrasenia(UsuarioEntidad UsuarioEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            string Contrasenia = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            UsuarioAcceso UsuarioAccesoObjeto = new UsuarioAcceso();

            if (UsuarioEntidadObjeto.CuentaUsuario == "")
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.RecuperarContrasenia.CuentaNoValida;
                ResultadoEntidadObjeto.DescripcionError = TextoError.RecuperarCuentaNoValida;
            }
            else
            {
                CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

                ResultadoEntidadObjeto = UsuarioAccesoObjeto.SeleccionarUsuario(UsuarioEntidadObjeto, CadenaConexion);

                if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.RecuperarContrasenia.CuentaNoExiste;
                    ResultadoEntidadObjeto.DescripcionError = TextoError.RecuperarCuentaNoExiste;
                }
                else
                {
                    Contrasenia = EncriptarTexto.Desencriptar(ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows[0]["Contrasenia"].ToString());

                    EnviarCorreo(UsuarioEntidadObjeto.AplicacionId, UsuarioEntidadObjeto.CuentaUsuario, Contrasenia, ConstantePrograma.AsuntoRecuperacionContrasenia, ConstantePrograma.CorreoRecuperarContrasenia);

                    ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.RecuperarContrasenia.CorreoEnviado;
                }
            }

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad CambiarContrasenia(UsuarioEntidad UsuarioEntidadActual)
        {
            string CadenaConexion = string.Empty;
            string Contrasenia = string.Empty;
            
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            UsuarioAcceso UsuarioAccesoObjeto = new UsuarioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

            if (ValidarCambiarContrasenia(UsuarioEntidadActual, ref ResultadoEntidadObjeto))
            {
               //Texto = EncriptarTexto.Encriptar(NuevaContrasenia);
                UsuarioEntidadActual.NuevaContrasenia = Texto;
                //UsuarioAccesoObjeto.Texto = EncriptarTexto.Encriptar(Contrasenia);
                UsuarioAccesoObjeto.CambiarContrasenia(UsuarioEntidadActual, CadenaConexion);
            }

            return ResultadoEntidadObjeto;
        }

        public ResultadoEntidad SeleccionarUsuario(UsuarioEntidad UsuarioEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            UsuarioAcceso UsuarioAccesoObject = new UsuarioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

            Resultado = UsuarioAccesoObject.SeleccionarUsuario(UsuarioEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        protected bool TieneDepartamentosRelacionados(string CadenaUsuarioId)
        {
            bool TieneRelaciones = false;
            DepartamentoProceso DepartamentoProcesoObjeto = new DepartamentoProceso();

            TieneRelaciones = DepartamentoProcesoObjeto.SeleccionarDepartamentoUsuariosRelacionados(CadenaUsuarioId);

            return TieneRelaciones;
        }

        protected bool TieneDireccionesRelacionadas(string CadenaUsuarioId)
        {
            bool TieneRelaciones = false;
            DireccionProceso DireccionProcesoObjecto = new DireccionProceso();

            TieneRelaciones = DireccionProcesoObjecto.SeleccionarDireccionUsuariosRelacionados(CadenaUsuarioId);

            return TieneRelaciones;
        }

        protected bool TieneEmpleadosRelacionados(string CadenaUsuarioId)
        {
            bool TieneRelaciones = false;
            EmpleadoProceso EmpleadoProcesoObjecto = new EmpleadoProceso();

            TieneRelaciones = EmpleadoProcesoObjecto.SeleccionarEmpleadoUsuariosRelacionados(CadenaUsuarioId);

            return TieneRelaciones;
        }

        protected bool TieneEdificiosRelacionados(string CadenaUsuarioId)
        {
            bool TieneRelaciones = false;
            EdificioProceso EdificioProcesoObjecto = new EdificioProceso();

            TieneRelaciones = EdificioProcesoObjecto.SeleccionarEdificioUsuariosRelacionados(CadenaUsuarioId);

            return TieneRelaciones;
        }

        protected bool TieneFamiliasRelacionadas(string CadenaUsuarioId)
        {
            bool TieneRelaciones = false;
            FamiliaProceso FamiliaProcesoObjecto = new FamiliaProceso();

            TieneRelaciones = FamiliaProcesoObjecto.SeleccionarFamiliaUsuariosRelacionados(CadenaUsuarioId);

            return TieneRelaciones;
        }

        protected bool TieneSubfamiliasRelacionadas(string CadenaUsuarioId)
        {
            bool TieneRelaciones = false;
            SubFamiliaProceso SubFamiliaProcesoObjecto = new SubFamiliaProceso();

            TieneRelaciones = SubFamiliaProcesoObjecto.SeleccionarSubFamiliaUsuariosRelacionados(CadenaUsuarioId);

            return TieneRelaciones;
        }

        protected bool TienePuestosRelacionados(string CadenaUsuarioId)
        {
            bool TieneRelaciones = false;



            return TieneRelaciones;
        }

        protected bool TieneProveedoresRelacionados(string CadenaUsuarioId)
        {
            bool TieneRelaciones = false;



            return TieneRelaciones;
        }

        protected bool TieneRelacionesElUsuario(string CadenaUsuarioId)
        {
            bool TieneRelacionesElUsuario = false;

            // Revisar relaciones con Departamento
            if (TieneDepartamentosRelacionados(CadenaUsuarioId))
                return true;

            // Revisar relaciones con Dirección
            if (TieneDireccionesRelacionadas(CadenaUsuarioId))
                return true;

            // Revisar relaciones con Edificio
            if (TieneEdificiosRelacionados(CadenaUsuarioId))
                return true;

            // Revisar relaciones con Empleado
            if (TieneEmpleadosRelacionados(CadenaUsuarioId))
                return true;

            // Revisar relaciones con Familia
            if (TieneFamiliasRelacionadas(CadenaUsuarioId))
                return true;

            // Revisar relaciones con Subfamilia
            if (TieneSubfamiliasRelacionadas(CadenaUsuarioId))
                return true;

            // Revisar relaciones con Puesto
            if (TienePuestosRelacionados(CadenaUsuarioId))
                return true;

            // Revisar relaciones con Proveedor
            if (TieneProveedoresRelacionados(CadenaUsuarioId))
                return true;

            return TieneRelacionesElUsuario;
        }

        protected bool ValidarCambiarContrasenia(UsuarioEntidad UsuarioEntidadActual, ref ResultadoEntidad ResultadoEntidadObjeto)
        {
            bool CambiarContrasenia = true;

            if (UsuarioEntidadActual.ContraseniaAnterior == "")
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.CambiarContrasenia.AnteriorContrasenia;
                ResultadoEntidadObjeto.DescripcionError = TextoError.AnteriorContrasenia;
                CambiarContrasenia = false;
            }

            if (UsuarioEntidadActual.NuevaContrasenia == "")
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.CambiarContrasenia.NuevaContrasenia;
                ResultadoEntidadObjeto.DescripcionError = TextoError.NuevaContrasenia;
                CambiarContrasenia = false;
            }

            if (UsuarioEntidadActual.Confirmacion == "")
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.CambiarContrasenia.Confirmacion;
                ResultadoEntidadObjeto.DescripcionError = TextoError.Confirmacion;
                CambiarContrasenia = false;
            }

            if (UsuarioEntidadActual.NuevaContrasenia != UsuarioEntidadActual.Confirmacion)
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.CambiarContrasenia.Confirmacion;
                ResultadoEntidadObjeto.DescripcionError = TextoError.ContraseniaIncorrecta;
                CambiarContrasenia = false;
            }

            if (UsuarioEntidadActual.NuevaContrasenia == UsuarioEntidadActual.ContraseniaAnterior)
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.CambiarContrasenia.Validacion;
                ResultadoEntidadObjeto.DescripcionError = TextoError.Validacion;
                CambiarContrasenia = false;
            }

            if (!ValidarUsuarioContrasenia(UsuarioEntidadActual))
            {
                ResultadoEntidadObjeto.ErrorId = (int)ConstantePrograma.CambiarContrasenia.ErrorContrasenia;
                ResultadoEntidadObjeto.DescripcionError = TextoError.ContraseniaIncorrecta;
                CambiarContrasenia = false;
            }

            return CambiarContrasenia;
        }

        protected bool ValidarUsuarioContrasenia(UsuarioEntidad UsuarioEntidadObjeto)
        {
            bool UsuarioContrasenia = true;
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            UsuarioAcceso UsuarioAccesoObject = new UsuarioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

            Resultado = UsuarioAccesoObject.SeleccionarUsuario(UsuarioEntidadObjeto, CadenaConexion);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
            {
                UsuarioContrasenia = false;
            }
            else
            {
                //string Texto = string.Empty;
                string Contrasenia = string.Empty;
               // UsuarioEntidad UsuarioEntidadGuardar = new UsuarioEntidad();

               Password = EncriptarTexto.Desencriptar(Resultado.ResultadoDatos.Tables[0].Rows[0]["Contrasenia"].ToString());
               //UsuarioEntidadObjeto.Contrasenia= NuevaContrasenia.Trim();
               Texto = EncriptarTexto.Encriptar(UsuarioEntidadObjeto.NuevaContrasenia.Trim());

                if (UsuarioEntidadObjeto.ContraseniaAnterior != Password)
                    UsuarioContrasenia = false;
               
            }
 
            return UsuarioContrasenia;
        }
    }
}
