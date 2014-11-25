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
    public class RecepcionProceso : Base
    {
        public ResultadoEntidad AgregarRecepcionDetalle(RecepcionEntidad RecepcionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
            RecepcionAcceso RecepcionAccesoObjeto = new RecepcionAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);


            //****************** aqui entra para revisar que no se agregue la Orden
            ResultadoValidacion = BuscarRecepcionProducto(RecepcionObjetoEntidad);

            if (ResultadoValidacion.ErrorId != 0)
            {
                return ResultadoValidacion;
            }


            //if (ResultadoValidacion.ErrorId != 0)
            //{


            if (RecepcionObjetoEntidad.TemporalRecepcionId == "")
            {
                RecepcionObjetoEntidad.RecepcionId = Guid.NewGuid().ToString();
                Resultado = RecepcionAccesoObjeto.InsertarRecepcionDetalle(RecepcionObjetoEntidad, CadenaConexion);
            }
            else
            {
                Resultado = RecepcionAccesoObjeto.InsertarRecepcionDetalle(RecepcionObjetoEntidad, CadenaConexion);
            }

            // }
            //else 
            //{
            //    Resultado = Resultado.ErrorId = (int)ConstantePrograma.Recepcion.FolioDuplicado;


            //}


            return Resultado;
        }

        public ResultadoEntidad SeleccionaRecepcion(RecepcionEntidad RecepcionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RecepcionAcceso RecepcionAccesoObjeto = new RecepcionAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            Resultado = RecepcionAccesoObjeto.SeleccionarRecepcionDetalle(RecepcionObjetoEntidad, CadenaConexion);

            return Resultado;
        }

        public ResultadoEntidad AgregarRecepcionEncabezado(RecepcionEntidad RecepcionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            ResultadoEntidad ResultadoValidacion = new ResultadoEntidad();
            RecepcionAcceso RecepcionAccesoObjeto = new RecepcionAcceso();

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            if (RecepcionObjetoEntidad.RecepcionId != "")
            {

                Resultado = RecepcionAccesoObjeto.InsertarRecepcionEncabezado(RecepcionObjetoEntidad, CadenaConexion);
            }
            else
            {
                // Resultado = RecepcionAccesoObjeto.ActualizarProducto(RecepcionObjetoEntidad, CadenaConexion);
            }

            return Resultado;
        }

        public ResultadoEntidad CancelarNuevoRecepcion(RecepcionEntidad RecepcionObjetoEntidad)
        {
            string CadenaConexion = string.Empty;
            ResultadoEntidad Resultado = new ResultadoEntidad();
            RecepcionAcceso RecepcionAccesoObjeto = new RecepcionAcceso();
            SqlTransaction Transaccion;
            SqlConnection Conexion;

            CadenaConexion = SeleccionarConexion(ConstantePrograma.DefensoriaDB_Almacen);

            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();

            Transaccion = Conexion.BeginTransaction();

            try
            {

                //Se elimina la RecepcionDetalle del producto
                if (RecepcionObjetoEntidad.ProductoId != "")
                {

                    Resultado = RecepcionAccesoObjeto.EliminarRecepcionDetalle(Conexion, Transaccion, RecepcionObjetoEntidad);

                    if (Resultado.ErrorId == (int)ConstantePrograma.Recepcion.RecepcionEliminadoCorrectamente)
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



        public ResultadoEntidad BuscarRecepcionProducto(RecepcionEntidad RecepcionObjetoEntidad)
        {
            ResultadoEntidad Resultado = new ResultadoEntidad();

            if (RecepcionObjetoEntidad.TemporalRecepcionId != "")
            {

                if (RecepcionObjetoEntidad.ProductoId != "")
                {
                    Resultado = SeleccionaRecepcion(RecepcionObjetoEntidad);

                    if (Resultado.ResultadoDatos.Tables[0].Rows.Count > 0)
                    {
                        Resultado.ErrorId = (int)ConstantePrograma.Recepcion.FolioDuplicado;
                        Resultado.DescripcionError = TextoError.RecepcionDocumentoDuplicado;
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





    }
}
