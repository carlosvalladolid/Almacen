using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Activos.Entidad.Almacen;
using Activos.Entidad.General;
using Activos.Comun.Constante;

namespace Activos.AccesoDatos.Almacen
{
   public class ProveedorAlmacenAcceso:Base
    {
       public ResultadoEntidad ActualizarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenEntidadObjeto, string CadenaConexion)
       {
           SqlConnection Conexion = new SqlConnection(CadenaConexion);
           SqlCommand Comando;
           SqlParameter Parametro;
           ResultadoEntidad Resultado = new ResultadoEntidad();

           try
           {
               Comando = new SqlCommand("ActualizarProveedorProcedimiento", Conexion);
               Comando.CommandType = CommandType.StoredProcedure;

               Parametro = new SqlParameter("ProveedorId", SqlDbType.SmallInt);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.ProveedorId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("DependenciaId", SqlDbType.SmallInt);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.DependenciaId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("CiudadId", SqlDbType.SmallInt);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.CiudadId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("BancoId", SqlDbType.SmallInt);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.BancoId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("UsuarioIdModifico", SqlDbType.SmallInt);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.UsuarioIdModifico;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Nombre;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("RFC", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.RFC;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Calle", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Calle;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Numero", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Numero;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Colonia", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Colonia;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("CodigoPostal", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.CodigoPostal;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Telefono", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Telefono;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("NombreContacto", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.NombreContacto;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Email", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Email;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Cuenta", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Cuenta;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Clabe", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Clabe;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("CiudadOtro", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.CiudadOtro;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("MontoMaximoCompra", SqlDbType.Decimal);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.MontoMaximoCompra;
               Comando.Parameters.Add(Parametro);

               Conexion.Open();
               Comando.ExecuteNonQuery();
               Conexion.Close();

               Resultado.ErrorId = (int)ConstantePrograma.Proveedor.ProveedorGuardadoCorrectamente;

               return Resultado;
           }
           catch (SqlException sqlEx)
           {
               Resultado.ErrorId = sqlEx.Number;
               Resultado.DescripcionError = sqlEx.Message;

               return Resultado;
           }
       }

       public ResultadoEntidad EliminarProveedor(string CadenaProveedorId, string CadenaConexion)
       {
           SqlConnection Conexion = new SqlConnection(CadenaConexion);
           SqlCommand Comando;
           SqlParameter Parametro;
           ResultadoEntidad Resultado = new ResultadoEntidad();

           try
           {
               Comando = new SqlCommand("EliminarProveedorProcedimiento", Conexion);
               Comando.CommandType = CommandType.StoredProcedure;

               Parametro = new SqlParameter("CadenaProveedorId", SqlDbType.VarChar);
               Parametro.Value = CadenaProveedorId;
               Comando.Parameters.Add(Parametro);

               Conexion.Open();
               Comando.ExecuteNonQuery();
               Conexion.Close();

               Resultado.ErrorId = (int)ConstantePrograma.Proveedor.EliminacionExitosa;

               return Resultado;
           }
           catch (SqlException sqlEx)
           {
               Resultado.ErrorId = sqlEx.Number;
               Resultado.DescripcionError = sqlEx.Message;

               return Resultado;
           }
       }

       public ResultadoEntidad InsertarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenEntidadObjeto, string CadenaConexion)
       {
           SqlConnection Conexion = new SqlConnection(CadenaConexion);
           SqlCommand Comando;
           SqlParameter Parametro;
           ResultadoEntidad Resultado = new ResultadoEntidad();

           try
           {
               Comando = new SqlCommand("InsertarProveedorProcedimiento", Conexion);
               Comando.CommandType = CommandType.StoredProcedure;

               Parametro = new SqlParameter("DependenciaId", SqlDbType.SmallInt);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.DependenciaId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("CiudadId", SqlDbType.SmallInt);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.CiudadId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("BancoId", SqlDbType.SmallInt);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.BancoId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("UsuarioIdInserto", SqlDbType.SmallInt);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.UsuarioIdInserto;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Nombre;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("RFC", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.RFC;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Calle", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Calle;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Numero", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Numero;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Colonia", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Colonia;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("CodigoPostal", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.CodigoPostal;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Telefono", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Telefono;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("NombreContacto", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.NombreContacto;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Email", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Email;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Cuenta", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Cuenta;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Clabe", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Clabe;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("CiudadOtro", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.CiudadOtro;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("MontoMaximoCompra", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.MontoMaximoCompra;
               Comando.Parameters.Add(Parametro);

               Conexion.Open();
               Comando.ExecuteNonQuery();
               Conexion.Close();

               Resultado.ErrorId = (int)ConstantePrograma.Proveedor.ProveedorGuardadoCorrectamente;

               return Resultado;
           }
           catch (SqlException sqlEx)
           {
               Resultado.ErrorId = sqlEx.Number;
               Resultado.DescripcionError = sqlEx.Message;

               return Resultado;
           }
       }

       public ResultadoEntidad SeleccionarProveedor(ProveedorAlmacenEntidad ProveedorAlmacenEntidadObjeto, string CadenaConexion)
       {
           DataSet ResultadoDatos = new DataSet();
           SqlConnection Conexion = new SqlConnection(CadenaConexion);
           SqlCommand Comando;
           SqlParameter Parametro;
           SqlDataAdapter Adaptador;
           ResultadoEntidad Resultado = new ResultadoEntidad();

           try
           {
               Comando = new SqlCommand("SeleccionarProveedorProcedimiento", Conexion);
               Comando.CommandType = CommandType.StoredProcedure;

               Parametro = new SqlParameter("ProveedorId", SqlDbType.SmallInt);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.ProveedorId;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("Nombre", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.Nombre;
               Comando.Parameters.Add(Parametro);           

               Parametro = new SqlParameter("BusquedaRapida", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.BusquedaRapida;
               Comando.Parameters.Add(Parametro);

               Parametro = new SqlParameter("BuscarNombre", SqlDbType.VarChar);
               Parametro.Value = ProveedorAlmacenEntidadObjeto.BuscarNombre;
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
