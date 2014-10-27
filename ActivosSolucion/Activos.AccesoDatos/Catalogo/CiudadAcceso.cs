using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.Entidad.General;
using Activos.Entidad.Catalogo;

namespace Activos.AccesoDatos.Catalogo
{
    public class CiudadAcceso : Base
    {

        public ResultadoEntidad SeleccionarCiudad(CiudadEntidad CiudadEntidadObjeto, string CadenaConexion)
        {
            DataSet ResultadoDatos = new DataSet();
            SqlConnection Conexion = new SqlConnection(CadenaConexion);
            SqlCommand Comando;
            SqlParameter Parametro;
            SqlDataAdapter Adaptador;
            ResultadoEntidad Resultado = new ResultadoEntidad();

            try
            {
                Comando = new SqlCommand("SeleccionarCiudadProcedimiento", Conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                Parametro = new SqlParameter("CiudadId", SqlDbType.SmallInt);
                Parametro.Value = CiudadEntidadObjeto.CiudadId;
                Comando.Parameters.Add(Parametro);

                Parametro = new SqlParameter("EstadoId", SqlDbType.SmallInt);
                Parametro.Value = CiudadEntidadObjeto.EstadoId;
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
