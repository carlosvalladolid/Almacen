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
    public class TemporalActivoProceso : Base
    {

        public ResultadoEntidad ValidarActivoDuplicado(TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();

            if (BuscarActivoPorNumeroSerie(TemporalActivoObjetoEntidad) == true)
            {   // Se busca si ya existe un activo con ese numero de serie, si es diferente de nada
                Resultado.ErrorId = (int)ConstantePrograma.TemporalActivo.NumeroSerieActivoDuplicado;
                Resultado.DescripcionError = TextoError.NumeroSerieActivoDuplicado;
                return Resultado;
            }

            if (BuscarTemporalActivoPorNumeroSerie(TemporalActivoObjetoEntidad) == true)
            {   // Se busca si ya se agregó un activo con ese numero de serie, si es diferente de nada
                Resultado.ErrorId = (int)ConstantePrograma.TemporalActivo.NumeroSerieActivoDuplicado;
                Resultado.DescripcionError = TextoError.NumeroSerieTemporalActivoDuplicado;
                return Resultado;
            }

            if (BuscarActivoPorBarrasParticular(TemporalActivoObjetoEntidad) == true)
            {   // Se busca si ya existe un activo con ese código de barras particular, si es diferente de nada
                Resultado.ErrorId = (int)ConstantePrograma.TemporalActivo.CodigoBarrasParticularActivoDuplicado;
                Resultado.DescripcionError = TextoError.CodigoBarrasParticularActivoDuplicado;
                return Resultado;
            }

            if (BuscarTemporalActivoPorBarrasParticular(TemporalActivoObjetoEntidad) == true)
            {   // Se busca si ya se agregó un activo con ese código de barras particular, si es diferente de nada
                Resultado.ErrorId = (int)ConstantePrograma.TemporalActivo.CodigoBarrasParticularActivoDuplicado;
                Resultado.DescripcionError = TextoError.CodigoBarrasParticularTemporalActivoDuplicado;
                return Resultado;
            }

            if (BuscarActivoPorBarrasGeneral(TemporalActivoObjetoEntidad) == true)
            {   // Se busca si ya existe un activo con ese código de barras general, si es diferente de nada
                Resultado.ErrorId = (int)ConstantePrograma.TemporalActivo.CodigoBarrasGeneralActivoDuplicado;
                Resultado.DescripcionError = TextoError.CodigoBarrasGeneralActivoDuplicado;
                return Resultado;
            }

            if (BuscarTemporalActivoPorBarrasGeneral(TemporalActivoObjetoEntidad) == true)
            {   // Se busca si ya se agregó un activo con ese código de barras general, si es diferente de nada
                Resultado.ErrorId = (int)ConstantePrograma.TemporalActivo.CodigoBarrasGeneralActivoDuplicado;
                Resultado.DescripcionError = TextoError.CodigoBarrasGeneralTemporalActivoDuplicado;
                return Resultado;
            }

            return Resultado;
        }

        public ResultadoEntidad AgregarTemporalActivo(TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad = new TemporalAccesorioEntidad();
            TemporalAccesorioProceso TemporalAccesorioProcesoNegocio = new TemporalAccesorioProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoActivoDuplicado = new ResultadoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            ResultadoActivoDuplicado = ValidarActivoDuplicado(TemporalActivoObjetoEntidad);

            if (ResultadoActivoDuplicado.ErrorId != 0)
            {
                return ResultadoActivoDuplicado;
            }

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            Resultado = GuardarTemporalActivo(Conexion, Transaccion, TemporalActivoObjetoEntidad);

            // Si el activo temporal fue creado o editado exitosamente 
            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalActivo.TemporalActivoGuardadoCorrectamente)
            {
                if (TemporalActivoObjetoEntidad.TemporalActivoId != 0)
                {
                    //Se eliminan fisicamente todos los accesorios inactivos de ese activo temporal
                    TemporalAccesorioObjetoEntidad.GrupoEstatus = "," + ((int)ConstantePrograma.EstatusTemporalAccesorio.Eliminado).ToString() + ",";
                    TemporalAccesorioObjetoEntidad.TemporalActivoId = TemporalActivoObjetoEntidad.TemporalActivoId;
                    Resultado = TemporalAccesorioProcesoNegocio.EliminarTemporalAccesorio(Conexion, Transaccion, TemporalAccesorioObjetoEntidad);

                    if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEliminadoCorrectamente)
                    {
                        //Se editan los accesorios con estatus Nuevo a estatus Activo de ese activo temporal
                        TemporalAccesorioObjetoEntidad.GrupoEstatus = "," + ((int)ConstantePrograma.EstatusTemporalAccesorio.Nuevo).ToString() + ",";
                        TemporalAccesorioObjetoEntidad.Estatus = (Int16)ConstantePrograma.EstatusTemporalAccesorio.Activo;
                        TemporalAccesorioObjetoEntidad.TemporalActivoId = TemporalActivoObjetoEntidad.TemporalActivoId;
                        Resultado = TemporalAccesorioProcesoNegocio.ActualizarTemporalAccesorio(Conexion, Transaccion, TemporalAccesorioObjetoEntidad);

                        //Si se actualizaron correctamente los accesorios, se termina la transaccion
                        if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEditadoCorrectamente)
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
                }
                else
                {
                    Transaccion.Commit();
                }
            }
            else
            {
                Transaccion.Rollback();
            }

            Conexion.Close();

            return Resultado;
        }

        public bool BuscarActivoPorNumeroSerie(TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            bool ExisteActivo = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();

            if (TemporalActivoObjetoEntidad.NumeroSerie != "")
            {
                ActivoObjetoEntidad.NumeroSerie = TemporalActivoObjetoEntidad.NumeroSerie;

                Resultado = SeleccionarActivo(ActivoObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                    ExisteActivo = true;
            }

            return ExisteActivo;
        }

        public bool BuscarActivoPorBarrasParticular(TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            bool ExisteActivo = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();

            if (TemporalActivoObjetoEntidad.CodigoBarrasParticular != "")
            {
                ActivoObjetoEntidad.CodigoBarrasParticular = TemporalActivoObjetoEntidad.CodigoBarrasParticular;

                Resultado = SeleccionarActivo(ActivoObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                    ExisteActivo = true;
            }

            return ExisteActivo;
        }

        public bool BuscarActivoPorBarrasGeneral(TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            bool ExisteActivo = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();

            if (TemporalActivoObjetoEntidad.CodigoBarrasGeneral != "")
            {
                ActivoObjetoEntidad.CodigoBarrasGeneral = TemporalActivoObjetoEntidad.CodigoBarrasGeneral;

                Resultado = SeleccionarActivo(ActivoObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                    ExisteActivo = true;
            }

            return ExisteActivo;
        }

        public bool BuscarTemporalActivoPorNumeroSerie(TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            bool ExisteTemporalActivo = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalActivoEntidad TmpActivoObjetoEntidad = new TemporalActivoEntidad();

            if (TemporalActivoObjetoEntidad.NumeroSerie != "")
            {
                TmpActivoObjetoEntidad.NumeroSerie = TemporalActivoObjetoEntidad.NumeroSerie;
                TmpActivoObjetoEntidad.TemporalCompraId = TemporalActivoObjetoEntidad.TemporalCompraId;

                Resultado = SeleccionarTemporalActivo(TmpActivoObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TemporalActivoId"].ToString()) != TemporalActivoObjetoEntidad.TemporalActivoId)
                        ExisteTemporalActivo = true;
                    else
                        ExisteTemporalActivo = false;
                    
                }
            }

            return ExisteTemporalActivo;
        }

        public bool BuscarTemporalActivoPorBarrasParticular(TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            bool ExisteTemporalActivo = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalActivoEntidad TmpActivoObjetoEntidad = new TemporalActivoEntidad();

            if (TemporalActivoObjetoEntidad.CodigoBarrasParticular != "")
            {
                TmpActivoObjetoEntidad.CodigoBarrasParticular = TemporalActivoObjetoEntidad.CodigoBarrasParticular;
                TmpActivoObjetoEntidad.TemporalCompraId = TemporalActivoObjetoEntidad.TemporalCompraId;

                Resultado = SeleccionarTemporalActivo(TmpActivoObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TemporalActivoId"].ToString()) != TemporalActivoObjetoEntidad.TemporalActivoId)
                        ExisteTemporalActivo = true;
                    else
                        ExisteTemporalActivo = false;

                }
            }

            return ExisteTemporalActivo;
        }

        public bool BuscarTemporalActivoPorBarrasGeneral(TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            bool ExisteTemporalActivo = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalActivoEntidad TmpActivoObjetoEntidad = new TemporalActivoEntidad();

            if (TemporalActivoObjetoEntidad.CodigoBarrasGeneral != "")
            {
                TmpActivoObjetoEntidad.CodigoBarrasGeneral = TemporalActivoObjetoEntidad.CodigoBarrasGeneral;
                TmpActivoObjetoEntidad.TemporalCompraId = TemporalActivoObjetoEntidad.TemporalCompraId;

                Resultado = SeleccionarTemporalActivo(TmpActivoObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    if (int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TemporalActivoId"].ToString()) != TemporalActivoObjetoEntidad.TemporalActivoId)
                        ExisteTemporalActivo = true;
                    else
                        ExisteTemporalActivo = false;

                }
            }

            return ExisteTemporalActivo;
        }

        public ResultadoEntidad CancelarActualizarActivo(TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad = new TemporalAccesorioEntidad();
            TemporalAccesorioProceso TemporalAccesorioProcesoNegocio = new TemporalAccesorioProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            //Se eliminan fisicamente todos los accesorios nuevos de ese activo temporal
            TemporalAccesorioObjetoEntidad.GrupoEstatus = "," + ((int)ConstantePrograma.EstatusTemporalAccesorio.Nuevo).ToString() + ",";
            TemporalAccesorioObjetoEntidad.TemporalActivoId = TemporalActivoObjetoEntidad.TemporalActivoId;
            Resultado = TemporalAccesorioProcesoNegocio.EliminarTemporalAccesorio(Conexion, Transaccion, TemporalAccesorioObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEliminadoCorrectamente)
            {
                //Se editan los accesorios con estatus Eliminado a estatus Activo de ese activo temporal
                TemporalAccesorioObjetoEntidad.GrupoEstatus = "," + ((int)ConstantePrograma.EstatusTemporalAccesorio.Eliminado).ToString() + ",";
                TemporalAccesorioObjetoEntidad.Estatus = (Int16)ConstantePrograma.EstatusTemporalAccesorio.Activo;
                TemporalAccesorioObjetoEntidad.TemporalActivoId = TemporalActivoObjetoEntidad.TemporalActivoId;
                Resultado = TemporalAccesorioProcesoNegocio.ActualizarTemporalAccesorio(Conexion, Transaccion, TemporalAccesorioObjetoEntidad);

                //Si se actualizaron correctamente los accesorios, se termina la transaccion
                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEditadoCorrectamente)
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

        public ResultadoEntidad CancelarNuevoActivo(TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad, TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            TemporalAccesorioProceso TemporalAccesorioProcesoNegocio = new TemporalAccesorioProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            Resultado = TemporalAccesorioProcesoNegocio.EliminarTemporalAccesorio(Conexion, Transaccion, TemporalAccesorioObjetoEntidad);

            // Si se eliminaron los accesorios temporales exitosamente
            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEliminadoCorrectamente)
            {
                //Se elimina el activo temporal
                Resultado = EliminarTemporalActivo(Conexion, Transaccion, TemporalActivoObjetoEntidad);

                if (Resultado.ErrorId == (int)ConstantePrograma.TemporalActivo.TemporalActivoEliminadoCorrectamente)
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

        public ResultadoEntidad EliminarTemporalActivo(SqlConnection Conexion, SqlTransaction Transaccion, TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalActivoAcceso TemporalActivoAccesoObjeto = new TemporalActivoAcceso();

            Resultado = TemporalActivoAccesoObjeto.EliminarTemporalActivo(Conexion, Transaccion, TemporalActivoObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad GuardarTemporalActivo(SqlConnection Conexion, SqlTransaction Transaccion, TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalActivoAcceso TemporalActivoAccesoObjeto = new TemporalActivoAcceso();

            if (TemporalActivoObjetoEntidad.TemporalActivoId == 0)
                Resultado = TemporalActivoAccesoObjeto.InsertarTemporalActivo(Conexion, Transaccion, TemporalActivoObjetoEntidad);
            else
                Resultado = TemporalActivoAccesoObjeto.ActualizarTemporalActivo(Conexion, Transaccion, TemporalActivoObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarActivo(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoProceso ActivoProcesoObjeto = new ActivoProceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = ActivoProcesoObjeto.SeleccionarActivo(ActivoObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarTemporalActivo(TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalActivoAcceso TemporalActivoAccesoObjeto = new TemporalActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalActivoAccesoObjeto.SeleccionarTemporalActivo(TemporalActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        //public ResultadoEntidad SeleccionarAsignacion(AsignacionEntidad AsignacionObjetoEntidad, TemporalAsignacionEntidad TemporalAsignacionObjetoEntidad)
        //{
        //    ResultadoEntidad Resultado = new ResultadoEntidad();
        //    ResultadoEntidad ResultadoAsignacion = new ResultadoEntidad();
        //    ResultadoEntidad ResultadoTemporalAsignacion = new ResultadoEntidad();
        //    AsignacionProceso AsignacionProcesoNegocio = new AsignacionProceso();
        //    DataTable TablaAsignacion = new DataTable();
        //    DataSet ResultadoDatos = new DataSet();
        //    DataRow Registro;

        //    TablaAsignacion.Columns.Add("TemporalAsignacionDetalleId");
        //    TablaAsignacion.Columns.Add("ActivoId");
        //    TablaAsignacion.Columns.Add("CondicionId");
        //    TablaAsignacion.Columns.Add("NombreCondicion");
        //    TablaAsignacion.Columns.Add("Descripcion");
        //    TablaAsignacion.Columns.Add("NumeroSerie");
        //    TablaAsignacion.Columns.Add("Modelo");
        //    TablaAsignacion.Columns.Add("Color");
        //    TablaAsignacion.Columns.Add("CodigoBarrasParticular");

        //    // Se buscan los activos que ya tenia asignados el empleado
        //    if (AsignacionObjetoEntidad.EmpleadoId != 0)
        //    {
        //        ResultadoAsignacion = AsignacionProcesoNegocio.SeleccionarAsignacion(AsignacionObjetoEntidad);

        //        if (ResultadoAsignacion.ResultadoDatos.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow Activo in ResultadoAsignacion.ResultadoDatos.Tables[0].Rows)
        //            {
        //                Registro = TablaAsignacion.NewRow();
        //                Registro["TemporalAsignacionDetalleId"] = "0";
        //                Registro["ActivoId"] = Activo["ActivoId"].ToString();
        //                Registro["CondicionId"] = Activo["CondicionId"].ToString();
        //                Registro["NombreCondicion"] = Activo["NombreCondicion"].ToString();
        //                Registro["Descripcion"] = Activo["Descripcion"].ToString();
        //                Registro["NumeroSerie"] = Activo["NumeroSerie"].ToString();
        //                Registro["Modelo"] = Activo["Modelo"].ToString();
        //                Registro["Color"] = Activo["Color"].ToString();
        //                Registro["CodigoBarrasParticular"] = Activo["CodigoBarrasParticular"].ToString();
        //                TablaAsignacion.Rows.Add(Registro);
        //            }

        //            TablaAsignacion.AcceptChanges();
        //        }
        //    }

        //    // Se buscan los activos que se han agregado a la asignación temporal
        //    if (TemporalAsignacionObjetoEntidad.TemporalAsignacionId != 0)
        //    {
        //       // ResultadoTemporalAsignacion = SeleccionarTemporalAsignacionDetalle(TemporalAsignacionObjetoEntidad);

        //        if (ResultadoTemporalAsignacion.ResultadoDatos.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow Activo in ResultadoTemporalAsignacion.ResultadoDatos.Tables[0].Rows)
        //            {
        //                Registro = TablaAsignacion.NewRow();
        //                Registro["TemporalAsignacionDetalleId"] = Activo["TemporalAsignacionDetalleId"].ToString();
        //                Registro["ActivoId"] = Activo["ActivoId"].ToString();
        //                Registro["CondicionId"] = Activo["CondicionId"].ToString();
        //                Registro["NombreCondicion"] = Activo["NombreCondicion"].ToString();
        //                Registro["Descripcion"] = Activo["Descripcion"].ToString();
        //                Registro["NumeroSerie"] = Activo["NumeroSerie"].ToString();
        //                Registro["Modelo"] = Activo["Modelo"].ToString();
        //                Registro["Color"] = Activo["Color"].ToString();
        //                Registro["CodigoBarrasParticular"] = Activo["CodigoBarrasParticular"].ToString();
        //                TablaAsignacion.Rows.Add(Registro);
        //            }

        //            TablaAsignacion.AcceptChanges();
        //        }

        //    }

        //    ResultadoDatos.Tables.Add(TablaAsignacion);

        //    Resultado.ResultadoDatos = ResultadoDatos;

        //    return Resultado;
        //}
    }
}
