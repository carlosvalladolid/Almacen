using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Activos.Entidad.Activos;
using Activos.Entidad.General;
using Activos.Comun.Constante;

namespace Activos.AccesoDatos.Activos
{
    public class TipoAccesorioAcceso : Base
    {
        public ResultadoEntidad SeleccionarTipoAccesorio(TipoAccesorioEntidad TipoAccesorioEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarTipoAccesorioProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("TipoAccesorioId", SqlDbType.SmallInt);
                Parametro.Value = TipoAccesorioEntidadObjeto.TipoAccesorioId;
                Comando.Parameters.Add(Parametro);

                Adaptador = new SqlDataAdapter(Comando);
                ResultadoDatos = new DataSet();

                Conexion.Open();
                Adaptador.Fill(ResultadoDatos);
                Conexion.Close();

                Resultado.ResultadoDatos = ResultadoDatos;

                return Resultado;
            }
            catch (SqlException Excepcion)
            {
                Resultado.ErrorId = Excepcion.Number;
                Resultado.DescripcionError = Excepcion.Message;

                return Resultado;
            }
        }
    }
}
