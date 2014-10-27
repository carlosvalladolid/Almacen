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
    public class TemporalAccesorioProceso : Base
    {
        public ResultadoEntidad ActualizarTemporalAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalAccesorioAcceso TemporalAccesorioAccesoObjeto = new TemporalAccesorioAcceso();

            Resultado = TemporalAccesorioAccesoObjeto.ActualizarTemporalAccesorio(Conexion, Transaccion, TemporalAccesorioObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad EliminarTemporalAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalAccesorioAcceso TemporalAccesorioAccesoObjeto = new TemporalAccesorioAcceso();

            Resultado = TemporalAccesorioAccesoObjeto.EliminarTemporalAccesorio(Conexion, Transaccion, TemporalAccesorioObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad InsertarTemporalAccesorio(SqlConnection Conexion, SqlTransaction Transaccion, TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalAccesorioAcceso TemporalAccesorioAccesoObjeto = new TemporalAccesorioAcceso();

            Resultado = TemporalAccesorioAccesoObjeto.InsertarTemporalAccesorio(Conexion, Transaccion, TemporalAccesorioObjetoEntidad);

            return Resultado;
        }

        public ResultadoEntidad SeleccionarTemporalAccesorio(TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalAccesorioAcceso TemporalAccesorioAccesoObjeto = new TemporalAccesorioAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Resultado = TemporalAccesorioAccesoObjeto.SeleccionarTemporalAccesorio(TemporalAccesorioObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad BuscarAccesorioAregadoACompra(TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad, TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalAccesorioEntidad BusquedaTmpAccesorioObjetoEntidad = new TemporalAccesorioEntidad();
            
            BusquedaTmpAccesorioObjetoEntidad.TemporalCompraId = TemporalActivoObjetoEntidad.TemporalCompraId;
            BusquedaTmpAccesorioObjetoEntidad.GrupoEstatus = "," + (int)ConstantePrograma.EstatusTemporalAccesorio.Activo + "," + (int)ConstantePrograma.EstatusTemporalAccesorio.Nuevo + ",";
            BusquedaTmpAccesorioObjetoEntidad.TipoAccesorioId = TemporalAccesorioObjetoEntidad.TipoAccesorioId;

            //Dependiendo del Tipo de Accesorio se va ha hacer la busqueda
            if (TemporalAccesorioObjetoEntidad.TipoAccesorioId == (int)ConstantePrograma.TipoAccesorio.ActivoFijo)
            {
                BusquedaTmpAccesorioObjetoEntidad.ActivoAccesorioId = TemporalAccesorioObjetoEntidad.ActivoAccesorioId;
            }
            else
            {
                BusquedaTmpAccesorioObjetoEntidad.Descripcion = TemporalAccesorioObjetoEntidad.Descripcion;
            }

            Resultado = SeleccionarTemporalAccesorio(BusquedaTmpAccesorioObjetoEntidad);

            return Resultado;
        }

        public bool BuscarAccesorioAregadoAActivo(TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad, TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            bool ExisteAccesorio = false;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            TemporalAccesorioEntidad BusquedaTmpAccesorioObjetoEntidad = new TemporalAccesorioEntidad();

            //Si el activo es nuevo entonces el accesorio No Activo no existe
            if (TemporalActivoObjetoEntidad.TemporalActivoId != 0)
            {
                //Si el tipo de accesorio No es un Activo Fijo se busca que no se haya agregado ya otro accesorio
                //de ese tipo al activo, sin importar su descripcion
                if (TemporalAccesorioObjetoEntidad.TipoAccesorioId != (int)ConstantePrograma.TipoAccesorio.ActivoFijo)
                {
                    BusquedaTmpAccesorioObjetoEntidad.TemporalCompraId = TemporalActivoObjetoEntidad.TemporalCompraId;
                    BusquedaTmpAccesorioObjetoEntidad.TemporalActivoId = TemporalActivoObjetoEntidad.TemporalActivoId;
                    BusquedaTmpAccesorioObjetoEntidad.GrupoEstatus = "," + (int)ConstantePrograma.EstatusTemporalAccesorio.Activo + "," + (int)ConstantePrograma.EstatusTemporalAccesorio.Nuevo + ",";
                    BusquedaTmpAccesorioObjetoEntidad.TipoAccesorioId = TemporalAccesorioObjetoEntidad.TipoAccesorioId;

                    Resultado = SeleccionarTemporalAccesorio(BusquedaTmpAccesorioObjetoEntidad);

                     if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                         ExisteAccesorio = true;

                }
            }

            return ExisteAccesorio;
        }

        public bool BuscarAccesorioNoActivoFijo(TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();
            AccesorioEntidad BusquedaAccesorioObjetoEntidad = new AccesorioEntidad();
            AccesorioProceso AccesorioProcesoObjeto = new AccesorioProceso();
            bool ExisteAccesorio = false;

            BusquedaAccesorioObjetoEntidad.TipoAccesorioId = TemporalAccesorioObjetoEntidad.TipoAccesorioId;

            //Dependiendo del Tipo de Accesorio se va ha hacer la busqueda
            if (TemporalAccesorioObjetoEntidad.TipoAccesorioId != (int)ConstantePrograma.TipoAccesorio.ActivoFijo)
            {
                BusquedaAccesorioObjetoEntidad.Descripcion = TemporalAccesorioObjetoEntidad.Descripcion;
                Resultado = AccesorioProcesoObjeto.SeleccionarAccesorio(BusquedaAccesorioObjetoEntidad);

                if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                    ExisteAccesorio = true;

            }

            return ExisteAccesorio;
        }

        public ResultadoEntidad AgregarTemporalAccesorio(TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad, TemporalActivoEntidad TemporalActivoObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            bool ExisteAccesorio = false;
            TemporalActivoProceso TemporalActivoProcesoNegocio = new TemporalActivoProceso();
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad BusquedaAccesorioResultado = new ResultadoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            // Primero se busca que no haya un accesorio de ese tipo (excepto si es Activo Fijo) en el Activo temporal
            // Con esto se valida que todos los acesorios que no sean activos fijos solo se puedan agregar uno de cada uno por vehiculo
            ExisteAccesorio = BuscarAccesorioAregadoAActivo(TemporalAccesorioObjetoEntidad, TemporalActivoObjetoEntidad);

            if (ExisteAccesorio == false)
            {
                // Se busca si el accesorio ya se agregó en la compra temporal actual (no solo en este activo)
                BusquedaAccesorioResultado = BuscarAccesorioAregadoACompra(TemporalAccesorioObjetoEntidad, TemporalActivoObjetoEntidad);

                if (BusquedaAccesorioResultado.ResultadoDatos.Tables[0].Rows.Count == 0)
                {
                    // Se busca que no exista ya un Accesorio de ese tipo y con esa descripcion en caso de que el Accesorio no sea un Activo Fijo
                    ExisteAccesorio = BuscarAccesorioNoActivoFijo(TemporalAccesorioObjetoEntidad);

                    if (ExisteAccesorio == false)
                    {
                        Conexion = new SqlConnection(CadenaConexion);
                        Conexion.Open();

                        Transaccion = Conexion.BeginTransaction();

                        // Se verifica si ya se creo el activo temporal
                        if (TemporalActivoObjetoEntidad.TemporalActivoId == 0)
                        {
                            Resultado = TemporalActivoProcesoNegocio.GuardarTemporalActivo(Conexion, Transaccion, TemporalActivoObjetoEntidad);

                            TemporalActivoObjetoEntidad.TemporalActivoId = Resultado.NuevoRegistroId;
                            TemporalAccesorioObjetoEntidad.TemporalActivoId = TemporalActivoObjetoEntidad.TemporalActivoId;
                        }

                        // Si el activo temporal fue creado exitosamente o ya existia se inserta el accesorio temporal
                        if (Resultado.ErrorId == (int)ConstantePrograma.TemporalActivo.TemporalActivoGuardadoCorrectamente || Resultado.ErrorId == 0)
                        {
                            Resultado = InsertarTemporalAccesorio(Conexion, Transaccion, TemporalAccesorioObjetoEntidad);

                            // Si se inserto el accesorio temporal exitosamente termina la transaccion
                            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioGuardadoCorrectamente)
                            {
                                Transaccion.Commit();
                                Resultado.NuevoRegistroId = TemporalActivoObjetoEntidad.TemporalActivoId;
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
                        Resultado.DescripcionError = TextoError.AccesorioExistente;
                    }

                }
                else
                {
                    Resultado.DescripcionError = TextoError.AccesorioRepetido;
                }
            }
            else
            {
                Resultado.DescripcionError = TextoError.AccesorioRepetido;
            }

            return Resultado;
        }

        public ResultadoEntidad EliminarTemporalAccesorioIndividual(TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            Resultado = EliminarTemporalAccesorio(Conexion, Transaccion, TemporalAccesorioObjetoEntidad);

            // Si se elimino el accesorio temporal exitosamente termina la transaccion
            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEliminadoCorrectamente)
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

        public ResultadoEntidad ActualizarTemporalAccesorioIndividual(TemporalAccesorioEntidad TemporalAccesorioObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Activos);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            Resultado = ActualizarTemporalAccesorio(Conexion, Transaccion, TemporalAccesorioObjetoEntidad);

            // Si se edito el accesorio temporal exitosamente termina la transaccion
            if (Resultado.ErrorId == (int)ConstantePrograma.TemporalAccesorio.TemporalAccesorioEditadoCorrectamente)
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

    }
}
