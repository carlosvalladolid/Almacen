using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Entidad.Catalogo
{
    public class JefeEntidad : Base
    {

        private Int16 _DireccionId;                 // Identificador de la Direccion
        private Int16 _DepartamentoId;              // Identificador de departamento
        private Int16 _PuestoId;                    // Identificador de puesto
        private Int16 _EmpleadoId;                  // Identificador de empleado
        private Int16 _EstatusId;                   // Identificador de estatus
        private Int16 _UsuarioIdInserto;            // Identificador del usuario que creó el registro
        private Int16 _UsuarioIdModifico;           // Identificador del usuario que modificó el registro por última vez
        private string _FechaInserto;               // Fecha en formato texto en que se creó el registro
        private string _FechaUltimaModificacion;    // Fecha en formato texto de la última vez que se modificó el registro

        // Otros campos
        private string _TextoBusqueda;              // Texto de búsqueda en la tabla de jefes
        private string _CadenaJefeXML;              // Cadena con el identificador del jefe

         public JefeEntidad()
        {
            _DireccionId = 0;
            _DepartamentoId = 0;
            _PuestoId = 0;
            _EmpleadoId = 0;
            _EstatusId = 0;
            _UsuarioIdInserto = 0;
            _UsuarioIdModifico = 0;
            _FechaInserto = string.Empty;
            _FechaUltimaModificacion = string.Empty;
            _TextoBusqueda = string.Empty;
            _CadenaJefeXML = string.Empty;
        }

         public Int16 DireccionId
         {
             get { return _DireccionId; }
             set { _DireccionId = value; }
         }

         public Int16 DepartamentoId
         {
             get { return _DepartamentoId; }
             set { _DepartamentoId = value; }
         }

         public Int16 PuestoId
         {
             get { return _PuestoId; }
             set { _PuestoId = value; }
         }

         public Int16 EmpleadoId
         {
             get { return _EmpleadoId; }
             set { _EmpleadoId = value; }
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

         public string TextoBusqueda
         {
             get { return _TextoBusqueda; }
             set { _TextoBusqueda = value; }
         }

         public string CadenaJefeXML
         {
             get { return _CadenaJefeXML; }
             set { _CadenaJefeXML = value; }
         }

    }
}
