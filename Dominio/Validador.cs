using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Dominio
{
    public class Validador
    {
        private static Regex alfanumericos = new Regex("^[A-Z0-9 ]*$");
        private static Regex alfabetico = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$");
        private static Regex formatoEmail = new Regex
            ("^[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\." +
            "[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:" +
            "[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\\.)+" +
            "[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?$");

        public static Boolean ValidarCadenaAlfanumerica(string cadena)
        {
            return alfanumericos.IsMatch(cadena);
        }

        public static Boolean ValidarCadenaAlfabetica(string cadena)
        {
            return alfabetico.IsMatch(cadena);
        }

        public static Boolean ValidarCorreoElectronico(string cadena)
        {
            return formatoEmail.IsMatch(cadena);
        }

        public static Boolean ValidarLongitudCaracteresNombreYApellidos(string cadena)
        {
            Boolean valida = true;
            if (cadena.Length>50)
            {
                valida = false;
            }
            return valida;
        }

        public static Boolean ValidarLongitudCaracteresCorreoElectronicoPersonal(string cadena)
        {
            Boolean valida = true;
            if (cadena.Length > 100)
            {
                valida = false;
            }
            return valida;
        }

        public static Boolean ValidarLongitudCaracteresCorreoElectronicoInstitucional(string cadena)
        {
            Boolean valida = true;
            if (cadena.Length > 30)
            {
                valida = false;
            }
            return valida;
        }

        public static Boolean ValidarLongitudCaracteresMatricula(string cadena)
        {
            Boolean valida = true;
            if (cadena.Length != 9)
            {
                valida = false;
            }
            return valida;
        }
    }
}
