using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.Entidad.Almacen;

namespace Activos.AccesoDatos.Almacen
{
   public class ProveedorAlmacenAcceso:Base
    {
      protected int _ErrorId;
        protected string _DescripcionError;

        /// <summary>
        ///     Número de error, en caso de que haya ocurrido uno. Cero por default.
        /// </summary>
        public int ErrorId
        {
            get { return _ErrorId; }
        }

        /// <summary>
        ///     Descripción de error, en caso de que haya ocurrido uno. Empty por default.
        /// </summary>
        public string DescripcionError
        {
            get { return _DescripcionError; }
        }

        /// <summary>
        ///     Constructor de la clase.
        /// </summary>
        public ProveedorAlmacenAcceso()
        {
            _ErrorId = 0;
            _DescripcionError = string.Empty;
        }

        #region "Métodos"
            /// <summary>
            ///     Busca los proveedores en la base de datos.
            /// </summary>
            /// <param name="ProveedorEntidad">Entidad de proveedor.</param>
            /// <param name="CadenaConexion">Cadena de conexión a la base de datos.</param>
            /// <returns>Resultado de la búsqueda.</returns>
            public DataSet SeleccionarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenEntidad, string CadenaConexion)
            {
                DataSet Resultado = new DataSet();
                SqlConnection Conexion = new SqlConnection(CadenaConexion);
                SqlCommand Comando;
                SqlParameter Parametro;
                SqlDataAdapter Adaptador;

                try
                {
                    Comando = new SqlCommand("SeleccionarProveedor", Conexion);
                    Comando.CommandType = CommandType.StoredProcedure;

                    Parametro = new SqlParameter("ProveedorId", SqlDbType.SmallInt);
                    Parametro.Value = ProveedorAlmacenEntidad.ProveedorId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("DependenciaId", SqlDbType.SmallInt);
                    Parametro.Value = ProveedorAlmacenEntidad.DependenciaId;
                    Comando.Parameters.Add(Parametro);

                    Adaptador = new SqlDataAdapter(Comando);

                    Conexion.Open();
                    Adaptador.Fill(Resultado);
                    Conexion.Close();

                    return Resultado;
                }
                catch (SqlException Excepcion)
                {
                    _ErrorId = Excepcion.Number;
                    _DescripcionError = Excepcion.Message;

                    return Resultado;
                }
            }

            //public ResultadoEntidad InsertarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenEntidadObjeto, string CadenaConexion)
            //{
            //    SqlConnection Conexion = new SqlConnection(CadenaConexion);
            //    SqlCommand Comando;
            //    SqlParameter Parametro;
            //    ResultadoEntidad Resultado = new ResultadoEntidad();

            //    try
            //    {
            //        Comando = new SqlCommand("InsertarProveedorProcedimiento", Conexion);
            //        Comando.CommandType = CommandType.StoredProcedure;

            //        Parametro = new SqlParameter("DependenciaId", SqlDbType.SmallInt);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.DependenciaId;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("CiudadId", SqlDbType.SmallInt);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.CiudadId;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("BancoId", SqlDbType.SmallInt);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.BancoId;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.UsuarioIdInserto;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.Nombre;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("RFC", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.RFC;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Calle", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.Calle;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Numero", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.Numero;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Colonia", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.Colonia;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("CodigoPostal", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.CodigoPostal;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Telefono", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.Telefono;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("NombreContacto", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.NombreContacto;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Email", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.Email;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Cuenta", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.Cuenta;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Clabe", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.Clabe;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("CiudadOtro", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.CiudadOtro;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("MontoMaximoCompra", SqlDbType.Decimal);
            //        Parametro.Value = ProveedorAlmacenEntidadObjeto.MontoMaximoCompra;
            //        Comando.Parameters.Add(Parametro);


            //        Conexion.Open();
            //        Comando.ExecuteNonQuery();
            //        Conexion.Close();

            //        Resultado.ErrorId = (int)ConstantePrograma.Proveedor.ProveedorGuardadoCorrectamente;

            //        return Resultado;
            //    }
            //    catch (SqlException sqlEx)
            //    {
            //        Resultado.ErrorId = sqlEx.Number;
            //        Resultado.DescripcionError = sqlEx.Message;

            //        return Resultado;
            //    }
            //}

        #endregion
    }
}
