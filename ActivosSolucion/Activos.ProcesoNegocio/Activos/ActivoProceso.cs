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
using Activos.ProcesoNegocio.Catalogo;

namespace Activos.ProcesoNegocio.Activos
{
    public class ActivoProceso : Base
    {
       /* public ResultadoEntidad ActualizarActivoAsignar(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            Resultado = ActivoAccesoObjeto.ActualizarActivoAsignar(Conexion, Transaccion, ActivoObjetoEntidad);

            return Resultado;
        }*/
        
        public ResultadoEntidad ActualizarActivoCodigo(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            Resultado = ActivoAccesoObjeto.ActualizarActivoCodigo(Conexion, Transaccion, ActivoObjetoEntidad);

            return Resultado; 
        }

      /*  public ResultadoEntidad ActualizarActivoEstatus(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            Resultado = ActivoAccesoObjeto.ActualizarActivoEstatus(Conexion, Transaccion, ActivoObjetoEntidad);

            return Resultado;
        }*/

        public ResultadoEntidad ActualizarFechaBajaActivo(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoObjetoEntidad)
        {
           
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
                Resultado = ActivoAccesoObjeto.ActualizarFechaBajaActivo(Conexion, Transaccion, ActivoObjetoEntidad);
            return Resultado;
        }

        public ResultadoEntidad ActualizarFechaSalida(ActivoEntidad ActivoObjetoEntidad, SqlTransaction Transaccion, SqlConnection Conexion)
         {
             string CadenaConexion = string.Empty;
             ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
             ResultadoEntidad Resultado = new ResultadoEntidad();
             
             Resultado = ActivoAccesoObjeto.ActualizarFechaSalida(Conexion, Transaccion, ActivoObjetoEntidad);
            
             return Resultado;
         }

        public ResultadoEntidad DarBajaTemporal(ActivoEntidad ActivoObjetoEntidad, bool EsPadre)
        {
            string CadenaConexion = string.Empty;
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoAccesorio = new ResultadoEntidad();
            ResultadoEntidad ResultadoInsercion = new ResultadoEntidad();
            AccesorioEntidad AccesorioEntidadObjeto = new AccesorioEntidad();
            AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();
            Transaccion = Conexion.BeginTransaction();

            if (ValidarCampos(ActivoObjetoEntidad).ErrorId == (int)ConstantePrograma.BajaActivo.ValorPorDefecto)
            {
                /// validar si ya existe en la tabla temporal
                Resultado = ActivoAccesoObjeto.SeleccionarMovimientoTemporal(ActivoObjetoEntidad, CadenaConexion);
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    
                    ResultadoInsercion = ActivoAccesoObjeto.InsertarMovimientoTemporal(Conexion, Transaccion, ActivoObjetoEntidad);
                    if (ResultadoInsercion.ErrorId == (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoGuardadoCorrectamente)
                    {

                         ActivoObjetoEntidad.MovimientoId = Int16.Parse(ResultadoInsercion.NuevoRegistroId.ToString());
                        ResultadoInsercion = ActivoAccesoObjeto.InsertarTipoBajaTemporal(Conexion, Transaccion, ActivoObjetoEntidad);

                        if (EsPadre == true)
                        {
                            AccesorioEntidadObjeto.ActivoId = ActivoObjetoEntidad.ActivoId;

                            AccesorioEntidadObjeto.TipoAccesorioId = (Int16)ConstantePrograma.TipoAccesorio.ActivoFijo;
                            ResultadoAccesorio = AccesorioProcesoObjeto.SeleccionarAccesorio(AccesorioEntidadObjeto);
                            //obtengo los hijos, que serían los ActivoAccesorioId
                            Resultado = ActivoAccesoObjeto.SeleccionarMovimientoTemporalParaAccesorio(Conexion, Transaccion, ActivoObjetoEntidad);

                            foreach (DataRow Renglon in ResultadoAccesorio.ResultadoDatos.Tables[0].Rows)
                            {

                                
                                
                               ActivoObjetoEntidad.ActivoId= Int16.Parse(Renglon["ActivoAccesorioId"].ToString());
                               
                               ActivoObjetoEntidad.UsuarioId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoAutorizoId"].ToString());

                               ActivoObjetoEntidad.EmpleadoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoResguardoId"].ToString());
                               ActivoObjetoEntidad.TipoDeMovimiento = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoMovimientoId"].ToString());
                               ActivoObjetoEntidad.CondicionId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["CondicionId"].ToString());
                               ActivoObjetoEntidad.FechaMovimiento = Resultado.ResultadoDatos.Tables[0].Rows[0]["FechaEntrada"].ToString();
                               ActivoObjetoEntidad.DescripcionMovimiento = Resultado.ResultadoDatos.Tables[0].Rows[0]["Observacion"].ToString();
                               ActivoObjetoEntidad.UsuarioId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["UsuarioIdInserto"].ToString());

                               ResultadoInsercion = ActivoAccesoObjeto.InsertarMovimientoTemporal(Conexion, Transaccion, ActivoObjetoEntidad);
                               if (ResultadoInsercion.ErrorId == (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoGuardadoCorrectamente)
                                {

                                    //AccesorioEntidadObjeto.ActivoId = ActivoObjetoEntidad.ActivoId;
                                    //Resultado = AccesorioProcesoObjeto.InsertarAccesorioBajaTemporal(Conexion, Transaccion, AccesorioEntidadObjeto);

                                    ActivoObjetoEntidad.MovimientoId = Int16.Parse(ResultadoInsercion.NuevoRegistroId.ToString());
                                    ResultadoInsercion = ActivoAccesoObjeto.InsertarTipoBajaTemporal(Conexion, Transaccion, ActivoObjetoEntidad);
                                }
                                else break;                                
                            }
                        }

                        if (ResultadoInsercion.ErrorId == (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoGuardadoCorrectamente)
                        {
                            Transaccion.Commit();
                        }
                    }
                    else
                    {
                        Transaccion.Rollback();
                    } Conexion.Close();
                }
                else
                {
                    ResultadoInsercion.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoYaSeleccionado;
                    ResultadoInsercion.DescripcionError = TextoError.ActivoYaSeleccionado;
                    return ResultadoInsercion;
                }
            }
            return Resultado;

        }

        public ResultadoEntidad EliminarRegistrosTemporales(ActivoEntidad ActivoObjetoEntidad)
        {
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            Resultado = EliminarTemporalBajaActivo(ActivoObjetoEntidad);
            Resultado = EliminarTodosTemporalMovimiento(ActivoObjetoEntidad);



            return Resultado;

        }

        public ResultadoEntidad EliminarActivoTemporalSeleccionado(ActivoEntidad ActivoObjetoEntidad)
        {
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            string CadenaConexion = string.Empty;
            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
            SqlConnection Conexion;
            Conexion = new SqlConnection(CadenaConexion);


            Resultado = ActivoAccesoObjeto.EliminarTemporalBajaActivoSeleccionado(ActivoObjetoEntidad, Conexion);
            Resultado = ActivoAccesoObjeto.EliminarActivoTemporalSeleccionado(ActivoObjetoEntidad, Conexion);



            return Resultado;
        }

        public ResultadoEntidad EliminarTemporalBajaActivo(ActivoEntidad ActivoObjetoEntidad)
        {
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            string CadenaConexion = string.Empty;
            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
            SqlConnection Conexion;
            Conexion = new SqlConnection(CadenaConexion);



            Resultado = ActivoAccesoObjeto.EliminarTemporalBajaActivo(ActivoObjetoEntidad, Conexion);



            return Resultado;
        }

        public ResultadoEntidad EliminarTodosTemporalMovimiento(ActivoEntidad ActivoObjetoEntidad)
        {
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            string CadenaConexion = string.Empty;
            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
            SqlConnection Conexion;
            Conexion = new SqlConnection(CadenaConexion);



            Resultado = ActivoAccesoObjeto.EliminarTodosTemporalMovimiento(ActivoObjetoEntidad, Conexion);



            return Resultado;
        }

        public ResultadoEntidad EtiquetadoActivo(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            Resultado = ActualizarActivoCodigo(Conexion, Transaccion, ActivoObjetoEntidad);

            if (Resultado.ErrorId == (int)ConstantePrograma.Activo.ActivoEtiquetadoCorrectamente)
            {
                Transaccion.Commit();
            }
            else
            {
                Transaccion.Rollback();
            }

            Conexion.Close();

            return Resultado;
        }

        public ResultadoEntidad GuardarActivo(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            Resultado = ActivoAccesoObjeto.InsertarActivo(Conexion, Transaccion, ActivoObjetoEntidad);

            return Resultado;
        }
       
        protected ResultadoEntidad InsertarMovimientoBaja(ActivoEntidad ActivoObjetoEntidad, SqlTransaction Transaccion, SqlConnection Conexion)
         {

             ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
             ResultadoEntidad Resultado = new ResultadoEntidad();


             Resultado = ActivoAccesoObjeto.InsertarMovimientoBaja(Conexion, Transaccion, ActivoObjetoEntidad);


             return Resultado;
         }

        public ResultadoEntidad InsertarMovimientoEntradaSalida(ActivoEntidad ActivoObjetoEntidad) 
         {
             string CadenaConexion = string.Empty;
             ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
             ResultadoEntidad Resultado = new ResultadoEntidad();
             TipoServicioProceso TipoServicioProcesoObjeto = new TipoServicioProceso();
             TipoServicioEntidad TipoServicioEntidadObjeto = new TipoServicioEntidad();
             SqlTransaction Transaccion;
             SqlConnection Conexion;
             
             int ProveedorId = ActivoObjetoEntidad.ProveedorId;
             Int16 TipoServicioId = ActivoObjetoEntidad.TipoServicioId;
             string SesionId = ActivoObjetoEntidad.SesionId; 

             CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
             Conexion = new SqlConnection(CadenaConexion);
             Conexion.Open();
             Transaccion = Conexion.BeginTransaction();

             Resultado = SeleccionarActivoTemporal(ActivoObjetoEntidad);

             foreach (DataRow Renglon in Resultado.ResultadoDatos.Tables[0].Rows) 
             {
                 if (Int16.Parse(Renglon["TipoMovimientoId"].ToString()) == (Int16)ConstantePrograma.TipoMovimiento.Salida)
                 {
                    
                     ActivoObjetoEntidad.ActivoId = Int16.Parse(Renglon["ActivoId"].ToString());
                     ActivoObjetoEntidad.UsuarioId = Int16.Parse(Renglon["EmpleadoAutorizoId"].ToString());
                     ActivoObjetoEntidad.EmpleadoId = Int16.Parse(Renglon["EmpleadoResguardoId"].ToString());
                     ActivoObjetoEntidad.TipoDeMovimiento = Int16.Parse(Renglon["TipoMovimientoId"].ToString());
                     ActivoObjetoEntidad.CondicionId = Int16.Parse(Renglon["CondicionId"].ToString());
                    
                     ActivoObjetoEntidad.FechaMovimiento = Renglon["FechaEntrada"].ToString();
                     ActivoObjetoEntidad.DescripcionMovimiento = Renglon["Observacion"].ToString();
                     ActivoObjetoEntidad.UsuarioId = Int16.Parse(Renglon["UsuarioIdInserto"].ToString());
                     ActivoObjetoEntidad.TipoServicioId = TipoServicioId;
                     ActivoObjetoEntidad.ProveedorId = ProveedorId;

                     Resultado = ActivoAccesoObjeto.InsertarMovimientoEntradaSalida(Conexion, Transaccion, ActivoObjetoEntidad);
                     if (Resultado.ErrorId != (int)ConstantePrograma.EntradasSalidas.MovimientoCorrecto)
                         break;
                         ActivoObjetoEntidad.MovimientoId = (Int16)Resultado.NuevoRegistroId;
                         ActivoObjetoEntidad.TipoServicioId = TipoServicioId;
                         ActivoObjetoEntidad.ProveedorId = ProveedorId;
                         ActivoObjetoEntidad.SesionId = SesionId;
                         Resultado = ActivoAccesoObjeto.InsertarSalidaActivo(Conexion, Transaccion, ActivoObjetoEntidad);
                         if (Resultado.ErrorId != (int)ConstantePrograma.EntradasSalidas.MovimientoCorrecto)
                             break;

                     

                 }
                 else if (Int16.Parse(Renglon["TipoMovimientoId"].ToString()) == (Int16)ConstantePrograma.TipoMovimiento.Entrada)
                 {
                     
                     ActivoObjetoEntidad.ActivoId = Int16.Parse(Renglon["ActivoId"].ToString());
                     ActivoObjetoEntidad.UsuarioId = Int16.Parse(Renglon["EmpleadoAutorizoId"].ToString());
                     ActivoObjetoEntidad.EmpleadoId = Int16.Parse(Renglon["EmpleadoResguardoId"].ToString());
                     ActivoObjetoEntidad.TipoDeMovimiento = Int16.Parse(Renglon["TipoMovimientoId"].ToString());
                     ActivoObjetoEntidad.CondicionId = Int16.Parse(Renglon["CondicionId"].ToString());
                     
                     ActivoObjetoEntidad.FechaMovimiento = Renglon["FechaEntrada"].ToString();
                     ActivoObjetoEntidad.DescripcionMovimiento = Renglon["Observacion"].ToString();
                     ActivoObjetoEntidad.UsuarioId = Int16.Parse(Renglon["UsuarioIdInserto"].ToString());


                     Resultado = ActualizarFechaSalida(ActivoObjetoEntidad, Transaccion, Conexion);
                     if (Resultado.ErrorId == (int)ConstantePrograma.EntradasSalidas.MovimientoCorrecto)
                     {
                         Resultado = ActivoAccesoObjeto.InsertarMovimientoEntradaSalida(Conexion, Transaccion, ActivoObjetoEntidad);
                         if (Resultado.ErrorId != (int)ConstantePrograma.EntradasSalidas.MovimientoCorrecto)
                             break;
                     }
                     else break;
                 }

             } if (Resultado.ErrorId == (int)ConstantePrograma.EntradasSalidas.MovimientoCorrecto)
             {
                 Transaccion.Commit();

             }
             else
             {
                 Transaccion.Rollback();
             } 
             Conexion.Close();
             return Resultado;
         }

        public ResultadoEntidad InsertarMovimientoBaja(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioEntidad AccesorioEntidadObjeto = new AccesorioEntidad();
            AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
            SqlTransaction Transaccion;
            SqlConnection Conexion;
            


            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();
            Transaccion = Conexion.BeginTransaction();
            Resultado = ActualizarFechaBajaActivo(Conexion, Transaccion, ActivoObjetoEntidad);
            if (Resultado.ErrorId == (int)ConstantePrograma.BajaActivo.BajaActivoCorrecta)
            {
                Resultado = SeleccionarActivoTemporal(Conexion, Transaccion,  ActivoObjetoEntidad);

                foreach (DataRow Renglon in Resultado.ResultadoDatos.Tables[0].Rows)
                {

                    ActivoObjetoEntidad.ActivoId = Int16.Parse(Renglon["ActivoId"].ToString());
                    ActivoObjetoEntidad.UsuarioId = Int16.Parse(Renglon["EmpleadoAutorizoId"].ToString());
                    ActivoObjetoEntidad.EmpleadoId = Int16.Parse(Renglon["EmpleadoResguardoId"].ToString());
                    ActivoObjetoEntidad.TipoDeMovimiento = Int16.Parse(Renglon["TipoMovimientoId"].ToString());
                    ActivoObjetoEntidad.CondicionId = Int16.Parse(Renglon["CondicionId"].ToString());

                    ActivoObjetoEntidad.FechaMovimiento = Renglon["FechaSalida"].ToString();
                    ActivoObjetoEntidad.DescripcionMovimiento = Renglon["Observacion"].ToString();
                    ActivoObjetoEntidad.UsuarioId = Int16.Parse(Renglon["UsuarioIdInserto"].ToString());


                    Resultado = InsertarMovimientoBaja(ActivoObjetoEntidad, Transaccion, Conexion);
                    if (Resultado.ErrorId == (int)ConstantePrograma.EntradasSalidas.MovimientoCorrecto)
                    {
                        ActivoObjetoEntidad.MovimientoId = Int16.Parse(Resultado.NuevoRegistroId.ToString());
                        ActivoObjetoEntidad.TipoBajaId = Int16.Parse(Renglon["TipoBajaId"].ToString());

                        Resultado = InsertarBajaActivo(ActivoObjetoEntidad, Transaccion, Conexion);
                        if (Resultado.ErrorId == (int)ConstantePrograma.BajaActivo.BajaActivoCorrecta)
                        {
                            AccesorioEntidadObjeto.ActivoId = ActivoObjetoEntidad.ActivoId;
                            AccesorioEntidadObjeto.UsuarioIdInserto = ActivoObjetoEntidad.UsuarioId;
                            Resultado = AccesorioProcesoObjeto.DarBajaAccesorio(AccesorioEntidadObjeto, Transaccion, Conexion);
                            AccesorioEntidadObjeto.ActivoAccesorioId = AccesorioEntidadObjeto.ActivoId;
                            AccesorioEntidadObjeto.ActivoId = 0;
                            Resultado = AccesorioProcesoObjeto.DarBajaAccesorio(AccesorioEntidadObjeto, Transaccion, Conexion);

                        }
                        //AccesorioEntidadObjeto.ActivoAccesorioId = Int16.Parse(Renglon["ActivoId"].ToString());
                        //Resultado = AccesorioProcesoObjeto.SeleccionarAccesorio(Conexion, Transaccion, AccesorioEntidadObjeto);
                       // if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                       // {
                            
                       // } else Resultado = AccesorioProcesoObjeto.DarBajaAccesorio(AccesorioEntidadObjeto, Transaccion, Conexion);



                    }


                }
            }
            if (Resultado.ErrorId == (int)ConstantePrograma.BajaActivo.BajaActivoCorrecta)
            {
                Transaccion.Commit();

            }
            else
            {
                Transaccion.Rollback();
            }

            Conexion.Close();


            return Resultado;
        }

        public ResultadoEntidad InsertarMovimientoTemporal(ActivoEntidad ActivoObjetoEntidad, bool EsPadre)
        {
            string CadenaConexion = string.Empty;
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
            AccesorioEntidad AccesorioEntidadObjeto = new AccesorioEntidad();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();
            AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
            ResultadoEntidad ResultadoInsercion = new ResultadoEntidad();
            ResultadoEntidad ResultadoAccesorio = new ResultadoEntidad();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();
            Transaccion = Conexion.BeginTransaction();

            if (ValidarCampos(ActivoObjetoEntidad).ErrorId == (int)ConstantePrograma.BajaActivo.ValorPorDefecto)
            {
                /// validar si ya existe en la tabla temporal
                Resultado = ActivoAccesoObjeto.SeleccionarMovimientoTemporal(ActivoObjetoEntidad, CadenaConexion);

              


                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    Resultado = ActivoAccesoObjeto.InsertarMovimientoTemporalSalida(Conexion, Transaccion, ActivoObjetoEntidad);
                    if (Resultado.ErrorId == (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoGuardadoCorrectamente)

                        ActivoObjetoEntidad.MovimientoId = Int16.Parse(Resultado.NuevoRegistroId.ToString());
                    ResultadoInsercion = ActivoAccesoObjeto.InsertarTipoBajaTemporal(Conexion, Transaccion, ActivoObjetoEntidad);
                    if (EsPadre == true)
                    {
                        AccesorioEntidadObjeto.ActivoId = ActivoObjetoEntidad.ActivoId;

                        ResultadoAccesorio = AccesorioProcesoObjeto.SeleccionarAccesorio(AccesorioEntidadObjeto);
                        //obtengo los hijos, que serían los ActivoAccesorioId
                        Resultado = ActivoAccesoObjeto.SeleccionarMovimientoTemporalParaAccesorio(Conexion, Transaccion, ActivoObjetoEntidad);

                        foreach (DataRow Renglon in ResultadoAccesorio.ResultadoDatos.Tables[0].Rows)
                        {



                            ActivoObjetoEntidad.ActivoId = Int16.Parse(Renglon["ActivoAccesorioId"].ToString());

                            ActivoObjetoEntidad.UsuarioId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoAutorizoId"].ToString());

                            ActivoObjetoEntidad.EmpleadoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoResguardoId"].ToString());
                            ActivoObjetoEntidad.TipoDeMovimiento = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoMovimientoId"].ToString());
                            ActivoObjetoEntidad.CondicionId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["CondicionId"].ToString());
                            ActivoObjetoEntidad.FechaMovimiento = Resultado.ResultadoDatos.Tables[0].Rows[0]["FechaEntrada"].ToString();
                            ActivoObjetoEntidad.DescripcionMovimiento = Resultado.ResultadoDatos.Tables[0].Rows[0]["Observacion"].ToString();
                            ActivoObjetoEntidad.UsuarioId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["UsuarioIdInserto"].ToString());

                            ResultadoInsercion = ActivoAccesoObjeto.InsertarMovimientoTemporal(Conexion, Transaccion, ActivoObjetoEntidad);
                            if (ResultadoInsercion.ErrorId == (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoGuardadoCorrectamente)
                            {

                                //AccesorioEntidadObjeto.ActivoId = ActivoObjetoEntidad.ActivoId;
                                //Resultado = AccesorioProcesoObjeto.InsertarAccesorioBajaTemporal(Conexion, Transaccion, AccesorioEntidadObjeto);

                                ActivoObjetoEntidad.MovimientoId = Int16.Parse(ResultadoInsercion.NuevoRegistroId.ToString());
                                ResultadoInsercion = ActivoAccesoObjeto.InsertarTipoBajaTemporal(Conexion, Transaccion, ActivoObjetoEntidad);
                            }
                            else break;
                        }
                    }

                    if (ResultadoInsercion.ErrorId == (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoGuardadoCorrectamente)
                    {
                        Transaccion.Commit();
                    }
                    else
                    {
                        Transaccion.Rollback();
                    } Conexion.Close();
                }
                else
                {
                    ResultadoInsercion.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoYaSeleccionado;
                    ResultadoInsercion.DescripcionError = TextoError.ActivoYaSeleccionado;
                    return ResultadoInsercion;
                }

            }



            return ResultadoInsercion;
        }

        public ResultadoEntidad InsertarMovimientoTemporal(ActivoEntidad ActivoObjetoEntidad) 
        {
            string CadenaConexion = string.Empty;
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();
            Transaccion = Conexion.BeginTransaction();

            if (ValidarCampos(ActivoObjetoEntidad).ErrorId == (int)ConstantePrograma.BajaActivo.ValorPorDefecto)
            {
                /// validar si ya existe en la tabla temporal
                Resultado = ActivoAccesoObjeto.SeleccionarMovimientoTemporal(ActivoObjetoEntidad, CadenaConexion);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    Resultado = ActivoAccesoObjeto.InsertarMovimientoTemporalSalida(Conexion, Transaccion, ActivoObjetoEntidad);
                    if (Resultado.ErrorId == (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoGuardadoCorrectamente)
                        
                        ActivoObjetoEntidad.MovimientoId = Int16.Parse(Resultado.NuevoRegistroId.ToString());
                        Resultado = ActivoAccesoObjeto.InsertarTipoBajaTemporal(Conexion, Transaccion, ActivoObjetoEntidad);

                    if (Resultado.ErrorId == (int)ConstantePrograma.TemporalMovimiento.TemporalMovimientoGuardadoCorrectamente)
                    {
                        Transaccion.Commit();
                    }
                    else 
                    {
                        Transaccion.Rollback();
                    } Conexion.Close();
                }
                else
                {
                    Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoYaSeleccionado;
                    Resultado.DescripcionError = TextoError.ActivoYaSeleccionado;
                    return Resultado;
                }

            }



            return Resultado;
        }

        protected ResultadoEntidad InsertarBajaActivo(ActivoEntidad ActivoObjetoEntidad, SqlTransaction Transaccion, SqlConnection Conexion)
        {
            
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            Resultado = ActivoAccesoObjeto.InsertarBajaActivo(Conexion, Transaccion, ActivoObjetoEntidad);

            return Resultado;
        }   

        public ResultadoEntidad SeleccionarActivo(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = ActivoAccesoObjeto.SeleccionarActivo(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarActivoCompleto(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = ActivoAccesoObjeto.SeleccionarActivoCompleto(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarActivoPorDocumento(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = ActivoAccesoObjeto.SeleccionarActivoPorDocumento(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarActivoAvanzado(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = ActivoAccesoObjeto.SeleccionarActivoAvanzado(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public bool SeleccionarActivoMarcasRelacionados(string CadenaMarcaId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            ResultadoEntidadObjeto = ActivoAccesoObjeto.SeleccionarActivoMarcasRelacionados(CadenaMarcaId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public ResultadoEntidad SeleccionarActivoReporteEstatusSinAsignar(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = ActivoAccesoObjeto.SeleccionarActivoReporteEstatusSinAsignar(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarActivoReporteEstatusSinEtiquetar(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = ActivoAccesoObjeto.SeleccionarActivoReporteEstatusSinEtiquetar(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarActivoMismaConexion(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            Resultado = ActivoAccesoObjeto.SeleccionarActivoMismaConexion(Conexion, Transaccion, ActivoObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarActivoPorCompra(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = ActivoAccesoObjeto.SeleccionarActivoPorCompra(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }   

       /* public ResultadoEntidad SeleccionarActivoPorEstatus(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = ActivoAccesoObjeto.SeleccionarActivoPorEstatus(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        } */     

        public bool SeleccionarActivoSubFamiliasRelacionadas(string CadenaSubFamiliaId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            ResultadoEntidadObjeto = ActivoAccesoObjeto.SeleccionarActivoSubFamiliasRelacionadas(CadenaSubFamiliaId, CadenaConexion);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public ResultadoEntidad SeleccionarActivoTemporal(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = ActivoAccesoObjeto.SeleccionarActivoTemporalMovimiento(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarMovimientoTemporal(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = ActivoAccesoObjeto.SeleccionarMovimientoTemporal(ActivoObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarActivoTemporal(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            Resultado = ActivoAccesoObjeto.SeleccionarActivoTemporalMovimiento(Conexion, Transaccion, ActivoObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarTemporalMovimientoPorSesionId(ActivoEntidad ActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = ActivoAccesoObjeto.SeleccionarTemporalMovimientoPorSesionId(ActivoObjetoEntidad, CadenaConexion);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
            {
                return Resultado;
            }
            else
                return Resultado;
        }
       
        public ResultadoEntidad ValidarCampos(ActivoEntidad ActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();

            if (ActivoObjetoEntidad.FechaMovimiento.Trim() == "")
            {
                Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.CampoFechaVacio;
                Resultado.DescripcionError = TextoError.CampoFechaVacio;
                return Resultado;
            }
            else if (ActivoObjetoEntidad.CodigoBarrasParticular.Trim() == "")
            {
                Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.CampoCodigoBarrasVacio;
                Resultado.DescripcionError = TextoError.AsignacionCodigoBarrasParticular;
                return Resultado;
            }
            else if (Int16.Parse(ActivoObjetoEntidad.TipoBaja) == 0)
            {
                Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.TipoBajaNoSeleccionado;
                Resultado.DescripcionError = TextoError.TipoBajaNoSeleccionado;
                return Resultado;
            }
            else if (ActivoObjetoEntidad.DescripcionMovimiento.Trim() == "")
            {
                Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.OtrosTipoBajaRequerido;
                Resultado.DescripcionError = TextoError.OtrosTipoBajaRequerido;
                return Resultado;
            }
            else if (ActivoObjetoEntidad.CondicionId == 0)
            {
                Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.OtrosTipoBajaRequerido;
                Resultado.DescripcionError = TextoError.OtrosTipoBajaRequerido;
                return Resultado;
            }
            else Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.ValorPorDefecto;
            return Resultado;


        }
        
        public ResultadoEntidad ValidarExistenciaActivoSalida(ActivoEntidad ActivoObjetoEntidad, Int16 RolId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();
            AccesorioEntidad AccesorioObjetoEntidad = new AccesorioEntidad();
            
            int ActivoAccesorioId=0;
            Int16 TipoActivoId = ActivoObjetoEntidad.TipoActivoId;
            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
            
            ActivoObjetoEntidad.TipoActivoId = 0;
            Resultado = ActivoAccesoObjeto.SeleccionarActivo(ActivoObjetoEntidad, CadenaConexion);
            
            
            if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
            {
                Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoNoEncontrado;
                Resultado.DescripcionError = TextoError.ActivoNoEncontrado;
                return Resultado;
            } 
            else 
            {
                if(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString())== TipoActivoId || RolId==(Int16)ConstantePrograma.RolUsuario.Administrador)
                {
                    if(Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString())== (Int16)ConstantePrograma.TipoAtivo.Vehiculo)
                    {
                        TipoActivoId = (Int16)ConstantePrograma.TipoAtivo.Vehiculo;
                    }
                AccesorioObjetoEntidad.ActivoAccesorioId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString());
                ActivoObjetoEntidad.ActivoId=Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString());
                ActivoObjetoEntidad.TipoDeMovimiento = (int)ConstantePrograma.TipoMovimiento.Baja;
              
                Resultado = ActivoAccesoObjeto.SeleccionarMovimientoPorFecha(ActivoObjetoEntidad, CadenaConexion);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                {
                    Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoDadoDeBaja;
                    Resultado.DescripcionError = TextoError.ActivoDadoDeBaja;
                    return Resultado;
                }
                else  
                     ActivoObjetoEntidad.TipoDeMovimiento=(int)ConstantePrograma.TipoMovimiento.Salida;
                     Resultado = ActivoAccesoObjeto.SeleccionarMovimientoPorFecha(ActivoObjetoEntidad, CadenaConexion);
                     if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                     {
                         Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoConEstatusSalida;
                         Resultado.DescripcionError = TextoError.ActivoConEstatusSalida;
                         return Resultado;
                     }
                     else ActivoObjetoEntidad.TipoDeMovimiento = (int)ConstantePrograma.TipoMovimiento.Asignacion;
                          Resultado = ActivoAccesoObjeto.SeleccionarMovimientoPorFecha(ActivoObjetoEntidad, CadenaConexion);
                          if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 1)
                          {
                              
                              
                              AccesorioObjetoEntidad.ActivoId=Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString());
                              AccesorioObjetoEntidad.ActivoAccesorioId = 0;
                              AccesorioObjetoEntidad.TipoAccesorioId = (Int16)ConstantePrograma.TipoAccesorio.ActivoFijo;
                              Resultado = AccesorioAccesoObjeto.SeleccionarAccesorio(AccesorioObjetoEntidad, CadenaConexion);
                              if(Resultado.ResultadoDatos.Tables[0].Rows.Count==0)
                              {
                                  Resultado = ActivoAccesoObjeto.SeleccionarMovimientoPorFecha(ActivoObjetoEntidad, CadenaConexion);
                                  Resultado.NuevoRegistroId = TipoActivoId;
                                  Resultado.ErrorId = (int)ConstantePrograma.EntradasSalidas.ActivoValidoParaSalida;
                                  Resultado.DescripcionError = TextoError.ActivoValidoParaSalida;
                                  return Resultado;
                              }else
                                 
                                  Resultado = ActivoAccesoObjeto.SeleccionarMovimientoPorFecha(ActivoObjetoEntidad, CadenaConexion);
                              if (TipoActivoId == (Int16)ConstantePrograma.TipoAtivo.Vehiculo)
                                  Resultado.NuevoRegistroId = (Int16)ConstantePrograma.TipoAtivo.Vehiculo;
                                  Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoPadre;
                                  

                              return Resultado;

                          }
                          else
                              //aqui entra si tiene asignación en la tabla movimiento
                              //y busca por el ActivoAccesorioId en a tabla accesorio
                          Resultado = AccesorioAccesoObjeto.SeleccionarAccesorio(AccesorioObjetoEntidad, CadenaConexion);
                          if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                          {
                              Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoNoAsignado;
                              Resultado.DescripcionError = TextoError.ActivoNoAsignado;
                              return Resultado;
                          }
                          else
                          {
                              ActivoAccesorioId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoAccesorioId"].ToString());
                              ActivoObjetoEntidad.ActivoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString());
                              
                              Resultado = ActivoAccesoObjeto.SeleccionarMovimientoPorFecha(ActivoObjetoEntidad, CadenaConexion);
                              Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoEsAccresorioAsignado;
                              Resultado.NuevoRegistroId = ActivoAccesorioId;
                              return Resultado;
                          }
                          
                              
                }else Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.PermisoDenegado;
                return Resultado;
            }
           
            

        }

        public ResultadoEntidad ValidarExistenciaActivoEntrada(ActivoEntidad ActivoObjetoEntidad, Int16 RolId)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ActivoAcceso ActivoAccesoObjeto = new ActivoAcceso();
            AccesorioEntidad AccesorioObjetoEntidad = new AccesorioEntidad();
            
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();

            int ActivoAccesorioId = 0;
            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
            Int16 TipoActivoId = ActivoObjetoEntidad.TipoActivoId;
            ActivoObjetoEntidad.TipoActivoId = 0;
            Resultado = ActivoAccesoObjeto.SeleccionarActivo(ActivoObjetoEntidad, CadenaConexion);


            if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
            {
                Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoNoEncontrado;
                Resultado.DescripcionError = TextoError.ActivoNoEncontrado;
                return Resultado;
            }
            else
            {
                if (Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["TipoActivoId"].ToString()) == TipoActivoId || RolId == (Int16)ConstantePrograma.RolUsuario.Administrador)
                {
                    ActivoObjetoEntidad.ActivoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString());
                    ActivoObjetoEntidad.TipoDeMovimiento = (int)ConstantePrograma.TipoMovimiento.Salida;
                    Resultado = ActivoAccesoObjeto.SeleccionarMovimientoPorFecha(ActivoObjetoEntidad, CadenaConexion);

                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                    {
                        Resultado.ErrorId = (int)ConstantePrograma.EntradasSalidas.ActivoValidoParaEntrada;
                        Resultado.DescripcionError = TextoError.ActivoValidoParaEntrada;
                        

                        AccesorioObjetoEntidad.ActivoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString());
                        AccesorioObjetoEntidad.ActivoAccesorioId = 0;
                        Resultado = AccesorioAccesoObjeto.SeleccionarAccesorio(AccesorioObjetoEntidad, CadenaConexion);
                        if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                        {


                            AccesorioObjetoEntidad.ActivoAccesorioId = AccesorioObjetoEntidad.ActivoId;
                            AccesorioObjetoEntidad.ActivoId = 0;

                            Resultado = AccesorioAccesoObjeto.SeleccionarAccesorio(AccesorioObjetoEntidad, CadenaConexion);
                            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                            {
                                ActivoObjetoEntidad.ActivoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString());
                                Resultado = ActivoAccesoObjeto.SeleccionarMovimientoPorFecha(ActivoObjetoEntidad, CadenaConexion);
                                Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.ActivoEsAccresorioAsignado;
                                Resultado.NuevoRegistroId = AccesorioObjetoEntidad.ActivoAccesorioId;
                                return Resultado;
                            }
                            else
                            {
                                //checar que hace en caso de que no encuentre el activo como hijo

                                Resultado = ActivoAccesoObjeto.SeleccionarMovimientoPorFecha(ActivoObjetoEntidad, CadenaConexion);
                                Resultado.ErrorId = (int)ConstantePrograma.EntradasSalidas.ActivoValidoParaEntrada;
                                Resultado.DescripcionError = TextoError.ActivoValidoParaEntrada;
                                return Resultado;
                            }
                        }
                        else

                            Resultado = ActivoAccesoObjeto.SeleccionarMovimientoPorFecha(ActivoObjetoEntidad, CadenaConexion);
                        Resultado.ErrorId = (int)ConstantePrograma.EntradasSalidas.ActivoPadreValidoParaEntrada;



                        return Resultado;
                    }
                    else Resultado.ErrorId = (int)ConstantePrograma.EntradasSalidas.ActivoNoValidoParaEntrada;
                    Resultado.DescripcionError = TextoError.ActivoNoValidoParaEntrada;
                    return Resultado;


                }
                else Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.PermisoDenegado;
                return Resultado;
            }



        }

        public bool ValidarAsignacionActivo(int ActivoID)
        {
            string CadenaConexion = string.Empty;
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            AccesorioEntidad AccesorioObjetoEntidad = new AccesorioEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;
            bool AsignacionPermitida = true;

            ActivoObjetoEntidad.ActivoId = ActivoID;
            AccesorioObjetoEntidad.ActivoAccesorioId = ActivoID;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();
            Transaccion = Conexion.BeginTransaction();

            //Primero se valida que el Activo no este dado de Baja, ni Asignado a un empleado ni en Salida
            AsignacionPermitida = ValidarBajaAsignacionSalidaActivo(Conexion, Transaccion, ActivoObjetoEntidad);

            Conexion.Close();

            if (AsignacionPermitida == true)
            {
                // Ahora se valida que el Activo no este asignado a otro Activo (como un Accesorio)
                AsignacionPermitida = ValidarAsignacionActivoComoAccesorio(AccesorioObjetoEntidad);
            }
            

            return AsignacionPermitida;
            
        }

        public bool ValidarBajaAsignacionSalidaActivo(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoObjetoEntidad)
        {
            bool AsignacionPermitida = true;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();

            //Primero se valida que el Activo no este dado de Baja
            ResultadoEntidadObjeto = MovimientoProcesoObjeto.SeleccionarMovimientoBaja(Conexion, Transaccion, ActivoObjetoEntidad);
            
            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                AsignacionPermitida = false;
            }
            else
            {
                //Se valida que el Activo no este Asignado
                ResultadoEntidadObjeto = MovimientoProcesoObjeto.SeleccionarMovimientoAsignacion(Conexion, Transaccion, ActivoObjetoEntidad);

                if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    AsignacionPermitida = false;
                }
                else
                {
                    //Se valida que el Activo no este en Salida (fuera)
                    ResultadoEntidadObjeto = MovimientoProcesoObjeto.SeleccionarMovimientoSalida(Conexion, Transaccion, ActivoObjetoEntidad);

                    if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count > 0)
                    {
                        AsignacionPermitida = false;
                    }
                }
            }

            return AsignacionPermitida;
        }

        public bool ValidarAsignacionActivoComoAccesorio(AccesorioEntidad AccesorioObjetoEntidad)
        {
            bool AsignacionPermitida = true;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();

            ResultadoEntidadObjeto = AccesorioProcesoObjeto.SeleccionarAccesorio(AccesorioObjetoEntidad);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count > 0)
                AsignacionPermitida = false;

            return AsignacionPermitida;
        }

        public bool ValidarTransferenciaAccesorios(int ActivoID)
        {
            string CadenaConexion = string.Empty;
            ActivoEntidad ActivoObjetoEntidad = new ActivoEntidad();
            AccesorioEntidad AccesorioObjetoEntidad = new AccesorioEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;
            bool TransferenciaPermitida = true;

            ActivoObjetoEntidad.ActivoId = ActivoID;
            AccesorioObjetoEntidad.ActivoAccesorioId = ActivoID;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);
            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();
            Transaccion = Conexion.BeginTransaction();

            //Primero se valida que el Activo no este dado de Baja, ni en Salida
            TransferenciaPermitida = ValidarBajaSalidaActivo(Conexion, Transaccion, ActivoObjetoEntidad);

            Conexion.Close();

            if (TransferenciaPermitida == true)
            {
                // Ahora se valida que el Activo no este asignado a otro Activo (como un Accesorio)
                TransferenciaPermitida = ValidarAsignacionActivoComoAccesorio(AccesorioObjetoEntidad);
            }

            return TransferenciaPermitida;

        }

        public bool ValidarBajaSalidaActivo(SqlConnection Conexion, SqlTransaction Transaccion, ActivoEntidad ActivoObjetoEntidad)
        {
            bool TransferenciaPermitida = true;
            ResultadoEntidad ResultadoEntidadObjeto = new ResultadoEntidad();
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();

            //Primero se valida que el Activo no este dado de Baja
            ResultadoEntidadObjeto = MovimientoProcesoObjeto.SeleccionarMovimientoBaja(Conexion, Transaccion, ActivoObjetoEntidad);

            if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                TransferenciaPermitida = false;
            }
            else
            {
                //Se valida que el Activo no este en Salida (fuera)
                ResultadoEntidadObjeto = MovimientoProcesoObjeto.SeleccionarMovimientoSalida(Conexion, Transaccion, ActivoObjetoEntidad);

                if (ResultadoEntidadObjeto.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    TransferenciaPermitida = false;
                }
            }

            return TransferenciaPermitida;
        }

        public ActivoEntidad ObtenerUbicacionActivo(int ActivoID, Int16 AlmacenistaEmpleadoId)
        {
            ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();
            Int16 EmpleadoResguardoId = 0;
            Int16 UbicacionActivoId = 0;

            ActivoEntidadObjeto.ActivoId = ActivoID;

            //Primero se revisa si el activo no esta dado de baja
            Resultado = MovimientoProcesoObjeto.SeleccionarMovimientoBaja(ActivoEntidadObjeto);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
            {
                //Se revisa si el activo esta asignado
                Resultado = MovimientoProcesoObjeto.SeleccionarAsignacionPorEmpleado(ActivoEntidadObjeto);

                //Si el activo esta asignado...
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    //La direccion y el departamento se obtienen del empleado al que esta asignado el activo
                    EmpleadoResguardoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoResguardoId"].ToString());
                    UbicacionActivoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["UbicacionActivoId"].ToString());

                    Resultado = SeleccionarEmpleado(EmpleadoResguardoId);
                    ActivoEntidadObjeto.DireccionId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["DireccionId"].ToString());
                    ActivoEntidadObjeto.DireccionNombre = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreDireccion"].ToString();
                    ActivoEntidadObjeto.DepartamentoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["DepartamentoId"].ToString());
                    ActivoEntidadObjeto.DepartamentoNombre = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreDepartamento"].ToString();

                    if (UbicacionActivoId == (Int16)ConstantePrograma.UbicacionActivo.Bodega)
                    {
                        //Si el activo esta ubicado en bodega entonces el edificio en el que esta es el del Almacenista empleado
                        Resultado = SeleccionarEmpleado(AlmacenistaEmpleadoId);
                    }

                    ActivoEntidadObjeto.EdificioId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EdificioId"].ToString());
                    ActivoEntidadObjeto.EdificioNombre = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEdificio"].ToString();
                }
                else //Si el activo no esta asignado
                {
                    //El activo no tiene una direccion ni departamento definido
                    //El edificio en el que esta es el del Almacenista empleado
                    Resultado = SeleccionarEmpleado(AlmacenistaEmpleadoId);

                    ActivoEntidadObjeto.EdificioId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EdificioId"].ToString());
                    ActivoEntidadObjeto.EdificioNombre = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEdificio"].ToString();
                }
            }

            return ActivoEntidadObjeto;
        }

        public ResultadoEntidad SeleccionarEmpleado(Int16 EmpleadoId)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            EmpleadoEntidad EmpleadoEntidadObjeto = new EmpleadoEntidad();
            EmpleadoProceso EmpleadoProcesoObjeto = new EmpleadoProceso();

            EmpleadoEntidadObjeto.EmpleadoId = EmpleadoId;

            Resultado = EmpleadoProcesoObjeto.SeleccionarEmpleado(EmpleadoEntidadObjeto);

            return Resultado;
        }

        public ActivoEntidad ObtenerUbicacionAccesorio(int ActivoID, Int16 AlmacenistaEmpleadoId)
        {
            ActivoEntidad ActivoEntidadObjeto = new ActivoEntidad();
            ActivoEntidad ActivoPadreEntidadObjeto = new ActivoEntidad();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            MovimientoProceso MovimientoProcesoObjeto = new MovimientoProceso();
            Int16 EmpleadoResguardoId = 0;
            Int16 UbicacionActivoId = 0;
            int ActivoPadreId = 0;

            ActivoEntidadObjeto.ActivoId = ActivoID;

            //Primero se revisa si el activo no esta dado de baja
            Resultado = MovimientoProcesoObjeto.SeleccionarMovimientoBaja(ActivoEntidadObjeto);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count == 0)
            {
                //Se busca el padre al que esta asignado el activo accesorio
                ActivoPadreId = SeleccionarActivoPadre(ActivoID);

                ActivoPadreEntidadObjeto.ActivoId = ActivoPadreId;

                //Se revisa si el activo padre esta asignado
                Resultado = MovimientoProcesoObjeto.SeleccionarAsignacionPorEmpleado(ActivoPadreEntidadObjeto);

                //Si el activo esta asignado...
                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    //La direccion y el departamento se obtienen del empleado al que esta asignado el activo
                    EmpleadoResguardoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EmpleadoResguardoId"].ToString());
                    UbicacionActivoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["UbicacionActivoId"].ToString());

                    Resultado = SeleccionarEmpleado(EmpleadoResguardoId);
                    ActivoEntidadObjeto.DireccionId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["DireccionId"].ToString());
                    ActivoEntidadObjeto.DireccionNombre = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreDireccion"].ToString();
                    ActivoEntidadObjeto.DepartamentoId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["DepartamentoId"].ToString());
                    ActivoEntidadObjeto.DepartamentoNombre = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreDepartamento"].ToString();

                    if (UbicacionActivoId == (Int16)ConstantePrograma.UbicacionActivo.Bodega)
                    {
                        //Si el activo esta ubicado en bodega entonces el edificio en el que esta es el del Almacenista empleado
                        Resultado = SeleccionarEmpleado(AlmacenistaEmpleadoId);
                    }

                    ActivoEntidadObjeto.EdificioId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EdificioId"].ToString());
                    ActivoEntidadObjeto.EdificioNombre = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEdificio"].ToString();
                }
                else //Si el activo padre no esta asignado
                {
                    //El activo no tiene una direccion ni departamento definido
                    //El edificio en el que esta es el del Almacenista empleado
                    Resultado = SeleccionarEmpleado(AlmacenistaEmpleadoId);

                    ActivoEntidadObjeto.EdificioId = Int16.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["EdificioId"].ToString());
                    ActivoEntidadObjeto.EdificioNombre = Resultado.ResultadoDatos.Tables[0].Rows[0]["NombreEdificio"].ToString();
                }
            }

            return ActivoEntidadObjeto;
        }

        public int SeleccionarActivoPadre(int ActivoAccesorioId)
        {
            int ActivoId = 0;
            AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
            AccesorioEntidad AccesorioEntidadObjeto = new AccesorioEntidad();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            AccesorioEntidadObjeto.ActivoAccesorioId = ActivoAccesorioId;

            Resultado = AccesorioProcesoObjeto.SeleccionarAccesorio(AccesorioEntidadObjeto);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                ActivoId = int.Parse(Resultado.ResultadoDatos.Tables[0].Rows[0]["ActivoId"].ToString());
            }

            return ActivoId;
        }

    }
}
