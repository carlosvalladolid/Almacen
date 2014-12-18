using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Comun.Fecha
{
    public class FormatoFecha : Base
    {
        private Int16 _idLenguaje;
        private string _strHoy;
        private DateTime _dtHoy;

        public string strHoy
        {
            get { return _strHoy; }
            set { _strHoy = value; }
        }

        public DateTime dtHoy
        {
            get { return _dtHoy; }
            set { _dtHoy = value; }
        }

        /// <summary>
        ///     Constructor de la clase, no recibe parámetros, inicializa variables con los valores de la fecha de hoy
        /// </summary>
        public FormatoFecha()
        {
            _idLenguaje = (Int16)Constante.ConstantePrograma.Lenguaje.Español;
            _dtHoy = DateTime.Today;

            _strHoy = AsignarFormato();
        }

        /// <summary>
        ///     Inicializa el lenguaje a utilizar para el fomato de fecha
        /// </summary>
        /// <returns></returns>
        protected string SeleccionarLenguajeFormato()
        {
            string strLenguajeFormato = string.Empty;

            switch (_idLenguaje)
            {
                case (Int16)Constante.ConstantePrograma.Lenguaje.Español:
                    strLenguajeFormato = Constante.ConstantePrograma.EspañolFormatoFecha;
                    break;

                case (Int16)Constante.ConstantePrograma.Lenguaje.Ingles:
                    strLenguajeFormato = Constante.ConstantePrograma.InglesFormatoFecha;
                    break;

                default:
                    strLenguajeFormato = Constante.ConstantePrograma.EspañolFormatoFecha;
                    break;
            }

            return strLenguajeFormato;
        }

        /// <summary>
        ///     Se regresa la fecha del día de hoy, con el formato de fecha por defecto
        /// </summary>
        /// <returns></returns>
        public string AsignarFormato()
        {
            return AsignarFormato(SeleccionarLenguajeFormato());
        }

        /// <summary>
        ///     Da formato a la fecha del día de hoy, según el lenguaje enviado como parámetro
        /// </summary>
        /// <param name="idLanguaje">Idenfiticador del lenguaje a utilizar (1 - Español, 2 - Inglés)</param>
        /// <returns>Fecha del día de hoy, con el formato del lenguaje enviado como parámetro</returns>
        public string AsignarFormato(Int16 idLenguaje)
        {
            _idLenguaje = idLenguaje;

            return AsignarFormato(_idLenguaje);
        }

        /// <summary>
        ///     Da formatro a la fecha del día de hoy, con el formato enviado como parámetro
        /// </summary>
        /// <param name="strFormat">Formato de fecha esperado como respuesta [dd/mm/aaaa] [mm/dd/aaaa] [aaaa/mm/dd]</param>
        /// <returns>Fecha del día de hoy en el formato especificado</returns>
        public static string AsignarFormato(string strFormato)
        {
            string strSeparador = string.Empty;
            string strFechaFormateada = string.Empty;
            DateTime dtHoy = DateTime.Today;

            if (strFormato.Trim() == "")
            {
                throw new ArgumentException("Formato inválido");
            }
            else
            {
                strSeparador = "/";

                switch (strFormato)
                {
                    case "dd/mm/aaaa":
                        strFechaFormateada = dtHoy.Day.ToString("0#") + strSeparador + dtHoy.Month.ToString("0#") + strSeparador + dtHoy.Year;
                        break;

                    case "mm/dd/aaaa":
                        strFechaFormateada = dtHoy.Month.ToString("0#") + strSeparador + dtHoy.Day.ToString("0#") + strSeparador + dtHoy.Year;
                        break;

                    case "aaaa/mm/dd":
                        strFechaFormateada = dtHoy.Year + strSeparador + dtHoy.Month.ToString("0#") + strSeparador + dtHoy.Day.ToString("0#");
                        break;

                    default:
                        throw new ArgumentException("Formato inválido");
                }
            }

            return strFechaFormateada;
        }

        /// <summary>
        ///     Da formato a la fecha enviada, en el formato indicado como parámetro
        /// </summary>
        /// <param name="strDate">Fecha en formato texto [dd/mm/aaaa] [mm/dd/aaaa] [aaaa/mm/dd]</param>
        /// <param name="strFormat">Formato de fecha esperado como respuesta [dd/mm/aaaa] [mm/dd/aaaa] [aaaa/mm/dd]</param>
        /// <returns>Fecha enviada con el formato especificado</returns>
        public static string AsignarFormato(string strFecha, string strFormato)
        {
            string strSeparador = string.Empty;
            string strFechaFormateada = string.Empty;
            string[] strValores;

            if (strFecha.IndexOf("/") > 0)
                strSeparador = "/";

            if (strSeparador == "")
            {
                throw new ArgumentException("Formato inválido");
            }
            else
            {
                strValores = strFecha.Split(strSeparador.ToCharArray());

                switch (strFormato)
                {
                    case "dd/mm/aaaa":
                        strFechaFormateada = strValores[0] + strSeparador + strValores[1] + strSeparador + strValores[2];
                        break;

                    case "mm/dd/aaaa":
                        strFechaFormateada = strValores[1] + strSeparador + strValores[0] + strSeparador + strValores[2];
                        break;

                    case "aaaa/mm/dd":
                        strFechaFormateada = strValores[2] + strSeparador + strValores[1] + strSeparador + strValores[0];
                        break;

                    default:
                        throw new ArgumentException("Formato inválido");
                }
            }

            return strFechaFormateada;
        }

        public static string MarcarHora()
        {
            string strMarcarHora = string.Empty;

            strMarcarHora = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString("0#") + DateTime.Today.Day.ToString("0#") + DateTime.Today.Hour.ToString("0#") + DateTime.Today.Minute.ToString("0#");

            return strMarcarHora;
        }

    }
}
