using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using mensajes = Presentacion.Properties.Mensajes;

namespace Presentacion
{
    internal class MensajesHelper
    {
        public static void FaltaDeConexion()
        {
            MessageBox.Show(mensajes.messageBoxText_Conexion, mensajes.messageBoxTitle_Conexion,
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void OperacionExitosa()
        {
            MessageBox.Show(mensajes.messageBoxText_RegistroExitoso, mensajes.caption_Exito, 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
