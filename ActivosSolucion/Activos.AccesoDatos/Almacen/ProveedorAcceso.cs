﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.Entidad.Almacen;

namespace Activos.AccesoDatos.Almacen
{
    public class ProveedorAcceso : Base
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
        public ProveedorAcceso()
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
            public DataSet SeleccionarProveedor(ProveedorEntidad ProveedorEntidad, string CadenaConexion)
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
                    Parametro.Value = ProveedorEntidad.ProveedorId;
                    Comando.Parameters.Add(Parametro);

                    Parametro = new SqlParameter("DependenciaId", SqlDbType.SmallInt);
                    Parametro.Value = ProveedorEntidad.DependenciaId;
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

            //public ResultadoEntidad InsertarProveedor(ProveedorEntidad ProveedorEntidadObjeto, string CadenaConexion)
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
            //        Parametro.Value = ProveedorEntidadObjeto.DependenciaId;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("CiudadId", SqlDbType.SmallInt);
            //        Parametro.Value = ProveedorEntidadObjeto.CiudadId;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("BancoId", SqlDbType.SmallInt);
            //        Parametro.Value = ProveedorEntidadObjeto.BancoId;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
            //        Parametro.Value = ProveedorEntidadObjeto.UsuarioIdInserto;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorEntidadObjeto.Nombre;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("RFC", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorEntidadObjeto.RFC;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Calle", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorEntidadObjeto.Calle;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Numero", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorEntidadObjeto.Numero;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Colonia", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorEntidadObjeto.Colonia;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("CodigoPostal", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorEntidadObjeto.CodigoPostal;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Telefono", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorEntidadObjeto.Telefono;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("NombreContacto", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorEntidadObjeto.NombreContacto;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Email", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorEntidadObjeto.Email;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Cuenta", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorEntidadObjeto.Cuenta;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("Clabe", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorEntidadObjeto.Clabe;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("CiudadOtro", SqlDbType.VarChar);
            //        Parametro.Value = ProveedorEntidadObjeto.CiudadOtro;
            //        Comando.Parameters.Add(Parametro);

            //        Parametro = new SqlParameter("MontoMaximoCompra", SqlDbType.Decimal);
            //        Parametro.Value = ProveedorEntidadObjeto.MontoMaximoCompra;
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
