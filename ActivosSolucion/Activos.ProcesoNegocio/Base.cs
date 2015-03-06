using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

//Para la validacion de permisos:
using System.Web; 
using Activos.Entidad.Seguridad;
using Activos.Entidad.General;
using Activos.AccesoDatos.Seguridad;
using Activos.Comun.Constante;
using System.Data;

namespace Activos.ProcesoNegocio
{
    public class Base
    {
        protected string SeleccionarConexion(string BaseDatos)
        {
            string CadenaConexion = string.Empty;

            CadenaConexion = ConfigurationManager.ConnectionStrings[BaseDatos].ConnectionString;

            return CadenaConexion;
        }

        public void ValidarPermiso(Int16 PaginaId)
        {
            HttpContext Contexto = HttpContext.Current;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            //Primero se valida que exista la variable de sesión
            if (Contexto.Session["UsuarioEntidad"] != null)
            {
                //Ahora se valida que el usuario tenga permisos de ver la página
                Resultado = SeleccionarRolPagina(PaginaId, Contexto);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    Contexto.Response.Redirect("/Aplicacion/Error/Permisos.aspx", true);
                }
            }
            else
            {
                Contexto.Response.Redirect("/Inicio.aspx", true);
            }
        }

        public ResultadoEntidad SeleccionarRolPagina(Int16 PaginaId, HttpContext Contexto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RolEntidad RolEntidadObjeto = new RolEntidad();
            RolAcceso RolAccesoDatos = new RolAcceso();
            UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

            UsuarioSessionEntidad = (UsuarioEntidad)Contexto.Session["UsuarioEntidad"];

            RolEntidadObjeto.PaginaId = PaginaId;
            RolEntidadObjeto.RolId = UsuarioSessionEntidad.RolId;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

            Resultado = RolAccesoDatos.SeleccionarRolPagina(RolEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarRolPagina(Int16 PaginaId, HttpContext Contexto, String Proyecto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RolEntidad RolEntidadObjeto = new RolEntidad();
            RolAcceso RolAccesoDatos = new RolAcceso();
            UsuarioEntidad UsuarioSessionEntidad = new UsuarioEntidad();

            UsuarioSessionEntidad = (UsuarioEntidad)Contexto.Session["UsuarioEntidad"];

            RolEntidadObjeto.PaginaId = PaginaId;
            RolEntidadObjeto.RolId = UsuarioSessionEntidad.RolId;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Seguridad);

            Resultado = RolAccesoDatos.SeleccionarRolPagina(RolEntidadObjeto,Proyecto, CadenaConexion);

            return Resultado;
        }
    }
}
