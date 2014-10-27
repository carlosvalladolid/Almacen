using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Catalogo
{
    public class EmpleadoEntidad : Base
    {
        private Int16 _EmpleadoId;                  // Identificador de empleado
        private Int16 _EmpleadoIdJefe;              // Identificador de jefe
        private Int16 _DepartamentoId;              // Identificador de departamento
        private Int16 _DireccionId;                 // Identificador de direccion
        private Int16 _PuestoId;                    // Identificador de puesto
        private Int16 _EdificioId;                  // Identificador de edificio
        private Int16 _CiudadId;                    // Identificador de ciudad
        private Int16 _EstatusId;                   // Identificador de estatus
        private Int16 _UsuarioIdInserto;            // Identificador del usuario que creó el registro
        private Int16 _UsuarioIdModifico;           // Identificador del usuario que modificó el registro por última vez
        private string _Nombre;                     // Nombre del empleado
        private string _ApellidoPaterno;            // Apellido paterno del empleado
        private string _ApellidoMaterno;            // Apellido materno del empleado
        private string _RFC;                        // RFC del empleado
        private string _Calle;                      // Nombre de la calle
        private string _Numero;                     // Numero de la calle
        private string _Colonia;                    // Nombre de la colonia
        private string _TelefonoCasa;               // Numero telefono 
        private string _Celular;                    // Numero de celular 
        private string _CodigoPostal;               // Numero del codigo postal
        private string _Email;                      // Email del empleado
        private string _NumeroEmpleado;              // Numero del empleado
        private string _TelefonoTrabajo;            // Telefono del trabajo del empleado
        private string _TrabajoEmail;               // Email del trabajo del empleado
        private string _FechaInserto;               // Fecha en formato texto en que se creó el registro
        private string _FechaUltimaModificacion;    // Fecha en formato texto de la última vez que se modificó el registro

        //Otros Campos
        private string _BusquedaRapida;             // Texto de búsqueda en el catálogo de empleados
        private string _CadenaEmpleadoId;           // Cadena con Ids de empleados seleccionados
        private string _BuscarNombre;               // Campo para buscar empleados por nombre exacto
        private string _BuscarApellidoPaterno;      // Campo para buscar empleados por apellido paterno exacto
        private string _BuscarApellidoMaterno;      // Campo para buscar empleados por apellido materno exacto

         public EmpleadoEntidad()
        {
            _EmpleadoId = 0;
            _EmpleadoIdJefe = 0;
            _DepartamentoId = 0;
            _DireccionId = 0;
            _PuestoId = 0;
            _EdificioId = 0;
            _CiudadId = 0;
            _EstatusId = 0;
            _UsuarioIdInserto = 0;
            _UsuarioIdModifico = 0;
            _Nombre = string.Empty;
            _ApellidoPaterno = string.Empty;
            _ApellidoMaterno = string.Empty;
            _RFC = string.Empty;
            _Calle = string.Empty;
            _Numero = string.Empty;
            _Colonia = string.Empty;
            _CodigoPostal = string.Empty;
            _TelefonoCasa = string.Empty;
            _Celular = string.Empty;
            _Email = string.Empty;
            _NumeroEmpleado = string.Empty;
            _TelefonoTrabajo = string.Empty;
            _TrabajoEmail = string.Empty;
            _FechaInserto = string.Empty;
            _FechaUltimaModificacion = string.Empty;
            _BusquedaRapida = string.Empty;
            _CadenaEmpleadoId = string.Empty;
            _BuscarNombre = string.Empty;
            _BuscarApellidoPaterno = string.Empty;
            _BuscarApellidoMaterno = string.Empty;
        }

         public Int16 EmpleadoId
         {
             get { return _EmpleadoId; }
             set { _EmpleadoId = value; }
         }

         public Int16 EmpleadoIdJefe
         {
             get { return _EmpleadoIdJefe; }
             set { _EmpleadoIdJefe = value; }
         }

         public Int16 DepartamentoId
         {
             get { return _DepartamentoId; }
             set { _DepartamentoId = value; }
         }

         public Int16 DireccionId
         {
             get { return _DireccionId; }
             set { _DireccionId = value; }
         }

         public Int16 PuestoId
         {
             get { return _PuestoId; }
             set { _PuestoId = value; }
         }

         public Int16 EdificioId
         {
             get { return _EdificioId; }
             set { _EdificioId = value; }
         }

         public Int16 CiudadId
         {
             get { return _CiudadId; }
             set { _CiudadId = value; }
         }

         public Int16 EstatusId
         {
             get { return _EstatusId; }
             set { _EstatusId = value; }
         }

         public Int16 UsuarioIdInserto
         {
             get { return _UsuarioIdInserto; }
             set { _UsuarioIdInserto = value; }
         }

         public Int16 UsuarioIdModifico
         {
             get { return _UsuarioIdModifico; }
             set { _UsuarioIdModifico = value; }
         }

         public string Nombre
         {
             get { return _Nombre; }
             set { _Nombre = value; }
         }

         public string ApellidoPaterno
         {
             get { return _ApellidoPaterno; }
             set { _ApellidoPaterno = value; }
         }

         public string ApellidoMaterno
         {
             get { return _ApellidoMaterno; }
             set { _ApellidoMaterno = value; }
         }

         public string RFC
         {
             get { return _RFC; }
             set { _RFC = value; }
         }

         public string Calle
         {
             get { return _Calle; }
             set { _Calle = value; }
         }

         public string Numero
         {
             get { return _Numero; }
             set { _Numero = value; }
         }

         public string Colonia
         {
             get { return _Colonia; }
             set { _Colonia = value; }
         }

         public string CodigoPostal
         {
             get { return _CodigoPostal; }
             set { _CodigoPostal = value; }
         }

         public string TelefonoCasa
         {
             get { return _TelefonoCasa; }
             set { _TelefonoCasa = value; }
         }

         public string Celular
         {
             get { return _Celular; }
             set { _Celular = value; }
         }

         public string Email
         {
             get { return _Email; }
             set { _Email = value; }
         }

         public string NumeroEmpleado
         {
             get { return _NumeroEmpleado; }
             set { _NumeroEmpleado = value; }
         }

         public string TelefonoTrabajo
         {
             get { return _TelefonoTrabajo; }
             set { _TelefonoTrabajo = value; }
         }

         public string TrabajoEmail
         {
             get { return _TrabajoEmail; }
             set { _TrabajoEmail = value; }
         }

         public string FechaInserto
         {
             get { return _FechaInserto; }
             set { _FechaInserto = value; }
         }

         public string FechaUltimaModificacion
         {
             get { return _FechaUltimaModificacion; }
             set { _FechaUltimaModificacion = value; }
         }

         public string BusquedaRapida
         {
             get { return _BusquedaRapida; }
             set { _BusquedaRapida = value; }
         }

         public string CadenaEmpleadoId
         {
             get { return _CadenaEmpleadoId; }
             set { _CadenaEmpleadoId = value; }
         }

         public string BuscarNombre
         {
             get { return _BuscarNombre; }
             set { _BuscarNombre = value; }
         }

         public string BuscarApellidoPaterno
         {
             get { return _BuscarApellidoPaterno; }
             set { _BuscarApellidoPaterno = value; }
         }

         public string BuscarApellidoMaterno
         {
             get { return _BuscarApellidoMaterno; }
             set { _BuscarApellidoMaterno = value; }
         }

    }
}
