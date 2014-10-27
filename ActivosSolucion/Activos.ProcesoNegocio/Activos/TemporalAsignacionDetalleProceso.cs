using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.AccesoDatos.Catalogo;
using Activos.AccesoDatos.Activos;
using Activos.Comun.Constante;
using Activos.Entidad.General;
using Activos.Entidad.Catalogo;
using Activos.Entidad.Activos;

namespace Activos.ProcesoNegocio.Activos
{
    public class TemporalAsignacionDetalleProceso : Base
    {

        public ResultadoEntidad GuardarTemporalAsignacionDetalle(SqlConnection Conexion, SqlTransaction Transaccion, TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalAsignacionDetalleAcceso TemporalAsignacionDetalleAccesoObjeto = new TemporalAsignacionDetalleAcceso();

            if (TemporalAsignacionObjetoEntidad.TemporalAsignacionDetalleId == 0)
                Resultado = TemporalAsignacionDetalleAccesoObjeto.InsertarTemporalAsignacionDetalle(Conexion, Transaccion, TemporalAsignacionObjetoEntidad);
            else
                Resultado = TemporalAsignacionDetalleAccesoObjeto.ActualizarTemporalAsignacionDetalle(Conexion, Transaccion, TemporalAsignacionObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarTemporalAsignacionDetalle(TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalAsignacionDetalleAcceso TemporalAsignacionDetalleAccesoObjeto = new TemporalAsignacionDetalleAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalAsignacionDetalleAccesoObjeto.SeleccionarTemporalAsignacionDetalle(TemporalAsignacionObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarAsignacion(ActivoEntidad ActivoObjetoEntidad, TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoAsignacion = new ResultadoEntidad();
            ResultadoEntidad ResultadoTemporalAsignacion = new ResultadoEntidad();
            AsignacionProceso AsignacionProcesoNegocio = new AsignacionProceso();
            DataTable TablaAsignacion = new DataTable();
            DataSet ResultadoDatos = new DataSet();
            DataRow Registro;
            int CantidadActivosAgregados = 0;

            TablaAsignacion.Columns.Add("TemporalAsignacionDetalleId");
            TablaAsignacion.Columns.Add("ActivoId");
            TablaAsignacion.Columns.Add("CondicionId");
            TablaAsignacion.Columns.Add("NombreCondicion");
            TablaAsignacion.Columns.Add("Descripcion");
            TablaAsignacion.Columns.Add("NumeroSerie");
            TablaAsignacion.Columns.Add("Modelo");
            TablaAsignacion.Columns.Add("Color");
            TablaAsignacion.Columns.Add("CodigoBarrasParticular");

            // Se buscan los activos que ya tenia asignados el empleado
            if (ActivoObjetoEntidad.EmpleadoId != 0)
            {
                ResultadoAsignacion = SeleccionarAsignacionPorEmpleado(ActivoObjetoEntidad);

                if (ResultadoAsignacion.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Activo in ResultadoAsignacion.ResultadoDatos.Tables[0].Rows)
                    {
                        Registro = TablaAsignacion.NewRow();
                        Registro["TemporalAsignacionDetalleId"] = "0";
                        Registro["ActivoId"] = Activo["ActivoId"].ToString();
                        Registro["CondicionId"] = Activo["CondicionId"].ToString();
                        Registro["NombreCondicion"] = Activo["NombreCondicion"].ToString();
                        Registro["Descripcion"] = Activo["Descripcion"].ToString();
                        Registro["NumeroSerie"] = Activo["NumeroSerie"].ToString();
                        Registro["Modelo"] = Activo["Modelo"].ToString();
                        Registro["Color"] = Activo["Color"].ToString();
                        Registro["CodigoBarrasParticular"] = Activo["CodigoBarrasParticular"].ToString();
                        TablaAsignacion.Rows.Add(Registro);
                    }

                    TablaAsignacion.AcceptChanges();
                }
            }

            // Se buscan los activos que se han agregado a la asignación temporal
            if (TemporalAsignacionObjetoEntidad.TemporalAsignacionId != 0)
            {
                ResultadoTemporalAsignacion = SeleccionarTemporalAsignacionDetalle(TemporalAsignacionObjetoEntidad);

                if (ResultadoTemporalAsignacion.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    CantidadActivosAgregados = ResultadoTemporalAsignacion.ResultadoDatos.Tables[0].Rows.Count;

                    foreach (DataRow Activo in ResultadoTemporalAsignacion.ResultadoDatos.Tables[0].Rows)
                    {
                        Registro = TablaAsignacion.NewRow();
                        Registro["TemporalAsignacionDetalleId"] = Activo["TemporalAsignacionDetalleId"].ToString();
                        Registro["ActivoId"] = Activo["ActivoId"].ToString();
                        Registro["CondicionId"] = Activo["CondicionId"].ToString();
                        Registro["NombreCondicion"] = Activo["NombreCondicion"].ToString();
                        Registro["Descripcion"] = Activo["Descripcion"].ToString();
                        Registro["NumeroSerie"] = Activo["NumeroSerie"].ToString();
                        Registro["Modelo"] = Activo["Modelo"].ToString();
                        Registro["Color"] = Activo["Color"].ToString();
                        Registro["CodigoBarrasParticular"] = Activo["CodigoBarrasParticular"].ToString();
                        TablaAsignacion.Rows.Add(Registro);
                    }

                    TablaAsignacion.AcceptChanges();
                }

            }

            ResultadoDatos.Tables.Add(TablaAsignacion);

            Resultado.ResultadoDatos = ResultadoDatos;
            Resultado.NuevoRegistroId = CantidadActivosAgregados;

            return Resultado;
        }

        public ResultadoEntidad AgregarActivo(TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            TemporalAsignacionProceso TemporalAsignacionProcesoNegocio = new TemporalAsignacionProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            // Primero se valida si no se ha agregado al activo a la asignacion temporal
            if (ExisteActivoEnAsignacion(TemporalAsignacionObjetoEntidad) == false)
            {
                Conexion = new SqlConnection(CadenaConexion);
                Conexion.Open();

                Transaccion = Conexion.BeginTransaction();

                // Luego se verifica si ya se creeo la asignacion temporal, si no es asi, pues se crea
                if (TemporalAsignacionObjetoEntidad.TemporalAsignacionId == 0)
                {
                    Resultado = TemporalAsignacionProcesoNegocio.InsertarTemporalAsignacion(Conexion, Transaccion, TemporalAsignacionObjetoEntidad);
                    TemporalAsignacionObjetoEntidad.TemporalAsignacionId = Resultado.NuevoRegistroId;
                }

                // Si la asignacion temporal se creo exitosamente o ya existia, se agrega el activo a la asignacion detalle temporal
                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAsignacion.TemporalAsignacionGuardadoCorrectamente || Resultado.ErrorId == 0)
                {
                    Resultado = GuardarTemporalAsignacionDetalle(Conexion, Transaccion, TemporalAsignacionObjetoEntidad);

                    // Si se inserto/actualizó el activo en el detalle de la asignacion exitosamente termina la transaccion
                    if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAsignacion.TemporalAsignacionDetalleGuardadoCorrectamente)
                    {
                        Transaccion.Commit();
                        Resultado.NuevoRegistroId = TemporalAsignacionObjetoEntidad.TemporalAsignacionId;
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
            }
            else
            {
                // Ya se agregó el activo a la actual asignacion temporal
                Resultado.DescripcionError = TextoError.ActivoRepetido;
            }

            return Resultado;
        }

        public ResultadoEntidad EliminarTemporalAsignacionDetalle(TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            TemporalAsignacionDetalleAcceso TemporalAsignacionDetalleAccesoObjeto = new TemporalAsignacionDetalleAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            ResultadoEntidadObjeto = TemporalAsignacionDetalleAccesoObjeto.EliminarTemporalAsignacionDetalle(TemporalAsignacionObjetoEntidad, CadenaConexion);

            return ResultadoEntidadObjeto;
        }

        public bool ExisteActivoEnAsignacion(TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad)
        {
            bool ExisteActivo = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalAsignacionEntidad BuscarAsignacionObjetoEntidad = new TemporalAsignacionEntidad();

            if (TemporalAsignacionObjetoEntidad.TemporalAsignacionId != 0)
            {
                BuscarAsignacionObjetoEntidad.TemporalAsignacionId = TemporalAsignacionObjetoEntidad.TemporalAsignacionId;
                BuscarAsignacionObjetoEntidad.ActivoId = TemporalAsignacionObjetoEntidad.ActivoId;

                Resultado = SeleccionarTemporalAsignacionDetalle(BuscarAsignacionObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TemporalAsignacionDetalleId"].ToString()) != TemporalAsignacionObjetoEntidad.TemporalAsignacionDetalleId)
                        ExisteActivo = true;
                    else
                        ExisteActivo = false;

                }

            }

            return ExisteActivo;
        }

        public ResultadoEntidad SeleccionarAsignacionPorEmpleado(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            SqlTransaction Transaccion;
            SqlConnection Conexion;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();
            Transaccion = Conexion.BeginTransaction();

            Resultado = MovimientoProcesoObjeto.SeleccionarMovimientoAsignacion(Conexion, Transaccion, ActivoObjetoEntidad);

            Conexion.Close();

            return Resultado;
        }

    }
}
