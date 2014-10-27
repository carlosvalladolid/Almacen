using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Comun.Seguridad
{
    public class CadenaAleatoria : Base
    {
        public static string GenerarNumeroAleatorio(Int16 Longitud)
        {
            int Contador = 0;
            int Semilla = 0;
            Random Numero;
            StringBuilder Texto = new StringBuilder(Longitud);

            Semilla = (int)DateTime.Now.Ticks;
            Numero = new Random(Semilla);

            for (Contador = 1; Contador <= Longitud; Contador++)
            {
                char Letra = (char)(Numero.Next(48, 58));

                Texto.Append(Letra);
            }

            return Texto.ToString();
        }

        public static string GenerarCadenaAleatoria(Int16 Longitud)
        {
            int Contador = 0;
            int Semilla = 0;
            Random Numero;
            StringBuilder Texto = new StringBuilder(Longitud);

            Semilla = (int)DateTime.Now.Ticks;
            Numero = new Random(Semilla);

            for (Contador = 1; Contador <= Longitud; Contador++)
            {
                char Letra = (char)(Numero.Next(97, 123));

                Texto.Append(Letra);
            }

            return Texto.ToString();
        }
    }
}
