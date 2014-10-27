using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Comun.Cadenas
{
    public class Comparar : Base
    {

        public static string EstandarizarCadena(string strCadena)
        {
            string strResultado = string.Empty;

            // Se cambia la cadena de texto a mayusculas
            strResultado = strCadena.ToUpper();

            // Se le quitan los acentos
            strResultado = strResultado.Replace("Á", "A");
            strResultado = strResultado.Replace("É", "E");
            strResultado = strResultado.Replace("Í", "I");
            strResultado = strResultado.Replace("Ó", "O");
            strResultado = strResultado.Replace("Ú", "U");

            return strResultado;
        }

    }
}
