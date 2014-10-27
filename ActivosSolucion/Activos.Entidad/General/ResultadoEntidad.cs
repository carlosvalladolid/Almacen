using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Activos.Entidad.General
{
    public class ResultadoEntidad : Base
    {
        private int _NuevoRegistroId;                   // Identificador del nuevo registro de la base de datos
        private int _ErrorId;                           // Identificador del error
        private string _DescripcionError;               // Descripción del error
        private string _TextoResultado;                 // Resultado de la operación
        private string _Valor;                          // Valor devuelto por la operación
        private DataSet _ResultadoDatos;                // Registros devueltos por la consulta


        public ResultadoEntidad()
        {
            _NuevoRegistroId = 0;
            _ErrorId = 0;
            _DescripcionError = string.Empty;
            _TextoResultado = string.Empty;
            _Valor = string.Empty;
            _ResultadoDatos = new DataSet();
        }

        public int NuevoRegistroId
        {
            get { return _NuevoRegistroId; }
            set { _NuevoRegistroId = value; }
        }

        public int ErrorId
        {
            get { return _ErrorId; }
            set { _ErrorId = value; }
        }

        public string DescripcionError
        {
            get { return _DescripcionError; }
            set { _DescripcionError = value; }
        }

        public string TextoResultado
        {
            get { return _TextoResultado; }
            set { _TextoResultado = value; }
        }

        public string Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        public DataSet ResultadoDatos
        {
            get { return _ResultadoDatos; }
            set { _ResultadoDatos = value; }
        }
    }
}
