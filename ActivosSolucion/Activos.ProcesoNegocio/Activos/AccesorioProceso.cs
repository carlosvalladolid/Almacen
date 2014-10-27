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
    public class AccesorioProceso : Base
    {
        public ResultadoEntidad ActualizarAccesorioPorTransferencia(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();

            Resultado = AccesorioAccesoObjeto.ActualizarAccesorioPorTransferencia(Conexion, Transaccion, AccesorioObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad DarBajaAccesorio(AccesorioEntidad AccesorioEntidadObjeto, SqlTransaction Transaccion, SqlConnection Conexion) 
        {

            AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();

            Resultado = InsertarHistorialAccesorio(Conexion, Transaccion, AccesorioEntidadObjeto);
            if (Resultado.ErrorId == (int)ConstantePrograma.Accesorio.HistorialAccesorioGuardadoCorrectamente)
            {
                Resultado = EliminarAccesorio(Conexion, Transaccion, AccesorioEntidadObjeto);
                if (Resultado.ErrorId == (int)ConstantePrograma.Accesorio.AccesorioEliminadoCorrectamente)
                {
                    Resultado.ErrorId = (int)ConstantePrograma.BajaActivo.BajaActivoCorrecta;
                }

            }

            return Resultado;
        
        
        }

        public ResultadoEntidad EliminarAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();

            Resultado = AccesorioAccesoObjeto.EliminarAccesorio(Conexion, Transaccion, AccesorioObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad InsertarAccesorioBajaTemporal(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioObjetoEntidad) 
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();

            Resultado = AccesorioAccesoObjeto.InsertarAccesorioBajaTemporal(Conexion, Transaccion, AccesorioObjetoEntidad);

            return Resultado;
        
        }
        
        public ResultadoEntidad InsertarHistorialAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();

            Resultado = AccesorioAccesoObjeto.InsertarHistorialAccesorio(Conexion, Transaccion, AccesorioObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad InsertarHistorialAccesorioPorTransferencia(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();

            Resultado = AccesorioAccesoObjeto.InsertarHistorialAccesorioPorTransferencia(Conexion, Transaccion, AccesorioObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad GuardarAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();

            Resultado = AccesorioAccesoObjeto.InsertarAccesorio(Conexion, Transaccion, AccesorioObjetoEntidad);

            return Resultado;
        }
        
        public ResultadoEntidad SeleccionarAccesorio(AccesorioEntidad AccesorioEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = AccesorioAccesoObjeto.SeleccionarAccesorio(AccesorioEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, AccesorioEntidad AccesorioEntidadObjeto)
        {
            
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();
            Resultado = AccesorioAccesoObjeto.SeleccionarAccesorio(Conexion, Transaccion, AccesorioEntidadObjeto);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarAccesorioParaTransferir(AccesorioEntidad AccesorioEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = AccesorioAccesoObjeto.SeleccionarAccesorioParaTransferir(AccesorioEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarAccesorioPorDocumento(AccesorioEntidad AccesorioEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = AccesorioAccesoObjeto.SeleccionarAccesorioPorDocumento(AccesorioEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad GuardarTransferenciaAccesorio(AccesorioEntidad AccesorioObjetoEntidad, int CantidadAccesorios)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            //Primero se pasan los accesorios seleccionados al historial
            Resultado = InsertarHistorialAccesorioPorTransferencia(Conexion, Transaccion, AccesorioObjetoEntidad);

            //Si se guardo el historial correctamente y solo la cantidad de registros igual a la cantidad de accesorios seleccionados
            if (Resultado.ErrorId == (int)ConstantePrograma.Accesorio.HistorialAccesorioGuardadoCorrectamente)
            {
                if (Resultado.NuevoRegistroId == CantidadAccesorios)
                {
                    //Ahora se editan los accesorios para transferirlos al nuevo activo destino (padre)
                    Resultado = ActualizarAccesorioPorTransferencia(Conexion, Transaccion, AccesorioObjetoEntidad);

                    if (Resultado.ErrorId == (int)ConstantePrograma.Accesorio.AccesorioGuardadoCorrectamente)
                    {   //Si no hubo errores y se editaron la misma cantidad de registros que la cantidad de accesorios seleccionados
                        if (Resultado.NuevoRegistroId == CantidadAccesorios)
                        {
                            Transaccion.Commit();
                        }
                        else
                        {
                            Transaccion.Rollback();
                            Resultado.DescripcionError = "Ocurrió un error inesperado.";
                        }
                    }
                    else
                    {
                        Transaccion.Rollback();
                    }
                }
                else
                {
                    Transaccion.Rollback();
                    Resultado.DescripcionError = "Ocurrió un error inesperado.";
                }
            }
            else
            {
                Transaccion.Rollback();
            }

            Conexion.Close();

            return Resultado;
        }

        public ResultadoEntidad SeleccionarHistorialAccesorio(AccesorioEntidad AccesorioEntidadObjeto)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioAcceso AccesorioAccesoObjeto = new AccesorioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = AccesorioAccesoObjeto.SeleccionarHistorialAccesorio(AccesorioEntidadObjeto, CadenaConexion);

            return Resultado;
        }

        public bool ValidarActivoAccesorio(int ActivoId)
        {
            bool ActivoEsAccesorio = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioEntidad AccesorioEntidadObjeto = new AccesorioEntidad();

            AccesorioEntidadObjeto.ActivoAccesorioId = ActivoId;

            //Se busca si es accesorio de un activo
            Resultado = SeleccionarAccesorio(AccesorioEntidadObjeto);

            if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
            {
                ActivoEsAccesorio = true;
            }
            else
            {
                //Se busca fue accesorio de un activo
                Resultado = SeleccionarHistorialAccesorio(AccesorioEntidadObjeto);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                {
                    ActivoEsAccesorio = true;
                }
            }

            return ActivoEsAccesorio;
        }

    }
}
