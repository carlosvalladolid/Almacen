using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Catalogo;
using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;

namespace Activos.ProcesoNegocio.Catalogo
{
    public class JefeProceso : Base
    {
        public ResultadoEntidad EliminarJefe(JefeEntidad JefeObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            JefeAcceso JefeAccesoObjeto = new JefeAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = JefeAccesoObjeto.EliminarJefe(JefeObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad GuardarJefe(JefeEntidad JefeObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            JefeEntidad JefeBusquedaEntidadObjeto = new JefeEntidad();
            JefeAcceso JefeAccesoObjeto = new JefeAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            JefeBusquedaEntidadObjeto.DireccionId = JefeObjetoEntidad.DireccionId;
            JefeBusquedaEntidadObjeto.DepartamentoId = JefeObjetoEntidad.DepartamentoId;
            JefeBusquedaEntidadObjeto.PuestoId = JefeObjetoEntidad.PuestoId;
            JefeBusquedaEntidadObjeto.EmpleadoId = JefeObjetoEntidad.EmpleadoId;

            Resultado = SeleccionarJefe(JefeBusquedaEntidadObjeto);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
            {
                Resultado = JefeAccesoObjeto.InsertarJefe(JefeObjetoEntidad, CadenaConexion);
            }
            else
            {
                Resultado = JefeAccesoObjeto.ActualizarJefe(JefeObjetoEntidad, CadenaConexion);
            }

            return Resultado;
        }

        public ResultadoEntidad SeleccionarJefe(JefeEntidad JefeObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            JefeAcceso JefeAccesoObjeto = new JefeAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = JefeAccesoObjeto.SeleccionarJefe(JefeObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarJefeTitular(JefeEntidad JefeObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            JefeAcceso JefeAccesoObjeto = new JefeAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            Resultado = JefeAccesoObjeto.SeleccionarJefeTitular(JefeObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public bool SeleccionarJefeEmpleadosRelacionados(string CadenaEmpleadoId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            JefeAcceso JefeAccesoObjeto = new JefeAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = JefeAccesoObjeto.SeleccionarJefeEmpleadosRelacionados(CadenaEmpleadoId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public bool SeleccionarJefeDepartamentosRelacionados(string CadenaDepartamentoId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            JefeAcceso JefeAccesoObjeto = new JefeAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = JefeAccesoObjeto.SeleccionarJefeDepartamentosRelacionados(CadenaDepartamentoId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public bool SeleccionarJefePuestosRelacionados(string CadenaPuestoId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            JefeAcceso JefeAccesoObjeto = new JefeAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = JefeAccesoObjeto.SeleccionarJefePuestosRelacionados(CadenaPuestoId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public bool SeleccionarJefeDireccionesRelacionados(string CadenaDireccionId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            JefeAcceso JefeAccesoObjeto = new JefeAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Catalogo);

            ResultadoEntidadObjeto = JefeAccesoObjeto.SeleccionarJefeDireccionesRelacionados(CadenaDireccionId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public EmpleadoEntidad SeleccionarTitular(Int16 EmpleadoId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoEntidad EmpleadoTitularEntidadObjeto = new EmpleadoEntidad();
            JefeEntidad JefeEntidadObjeto = new JefeEntidad();

            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

            EmpleadoEntidadObjeto.EmpleadoId = EmpleadoId;

            //Primero se buscan los datos del empleado del que se quiere buscar su jefe
            Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

            EmpleadoEntidadObjeto.DepartamentoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["DepartamentoId"].ToString());
            EmpleadoEntidadObjeto.DireccionId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["DireccionId"].ToString());

            //Ahora se busca si el empleado es jefe de su departamento
            JefeEntidadObjeto.EmpleadoId = EmpleadoId;
            JefeEntidadObjeto.DepartamentoId = EmpleadoEntidadObjeto.DepartamentoId;
            Resultado = SeleccionarJefeTitular(JefeEntidadObjeto);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                //El empleado es jefe del departamento al que pertenece, entonces se busca el jefe de la direccion del departamento
                JefeEntidadObjeto = new JefeEntidad();
                JefeEntidadObjeto.DireccionId = EmpleadoEntidadObjeto.DireccionId;
                JefeEntidadObjeto.DepartamentoId = 0;

                Resultado = SeleccionarJefeTitular(JefeEntidadObjeto);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                    EmpleadoTitularEntidadObjeto.Nombre = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleado"].ToString();
            }
            else
            {
                //No es jefe de su departamento, asi que se busca el jefe de su departamento
                JefeEntidadObjeto = new JefeEntidad();
                JefeEntidadObjeto.DepartamentoId = EmpleadoEntidadObjeto.DepartamentoId;
                Resultado = SeleccionarJefeTitular(JefeEntidadObjeto);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                    EmpleadoTitularEntidadObjeto.Nombre = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEmpleado"].ToString();
                
            }
            //Se regresa solo el nombre del jefe Titular
            return EmpleadoTitularEntidadObjeto;
        }

    }
}
