using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentacion
{
    public partial class TutoresYTutorados : Page
    {

        private TutoresYTutoradosVistaModelo contexto;
        private Academicos tutorSeleccionado = new Academicos();
        public TutoresYTutorados() 
        {
            try
            {
                InitializeComponent();
                this.contexto = this.DataContext as TutoresYTutoradosVistaModelo;
            }
            catch (XamlParseException xpException)
            {
                if (xpException.InnerException is EntityException)
                {
                    MostrarMensajeFaltaDeConexion();
                }
            }
        }


        private void MostrarMensajeFaltaDeConexion()
        {
            MessageBox.Show(Properties.Mensajes.messageBoxText_Conexion,
                Properties.Mensajes.caption_Error,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        private void Button_GuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.contexto.GuardarAsignaciones())
                {
                    MostrarMensajeOperacionExitosa();
                }
                else
                {
                    MostrarMensajeFaltaDeConexion();
                }
            }
            catch (EntityCommandExecutionException eceException)
            {
                MostrarMensajeFaltaDeConexion();
            }
        }

        private void MostrarMensajeOperacionExitosa()
        {
            MessageBox.Show(Properties.Mensajes.messageBoxText_Exito,
                Properties.Mensajes.caption_Exito,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void TextBox_Filtro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.contexto.FiltrarEstudiantes(this.TextBox_Filtro.Text);
        }


        private void DataGrid_Tutores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                ObtenerTutorados();
            }
        }

        private void ObtenerTutoradosDePrimerTutor(object sender, RoutedEventArgs e)
        {
            ObtenerTutorados();
        }

        private void ObtenerTutorados()
        {
            this.tutorSeleccionado = this.contexto.Tutores.CurrentItem as Academicos;
            if (!tutorSeleccionado.ObtuvoTutorados)
            {
                this.contexto.ObtenerTutorados(tutorSeleccionado);
                tutorSeleccionado.ObtuvoTutorados = true;
            }
        }

        private void Rergesar(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
