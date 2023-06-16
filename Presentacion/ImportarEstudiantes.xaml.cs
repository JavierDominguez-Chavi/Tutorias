using CsvHelper;
using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentacion
{
    /// <summary>
    /// Interaction logic for ImportarEstudiantesPorCSV.xaml
    /// </summary>
    public partial class ImportarEstudiantes : Page
    {
        ImportarEstudiantesVistaModelo vistaModelo;
        public ImportarEstudiantes()
        {
            try
            { 
                InitializeComponent();
                this.vistaModelo = this.DataContext as ImportarEstudiantesVistaModelo;
            }
            catch (XamlParseException exception)
            { 
                MessageBox.Show("No hay conexión con la base de datos.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button fuente = (Button)sender;
            switch (fuente.Name)
            {
                case "Button_ImportarCSV":
                    MostrarDialogoImportar();
                    break;
                case "Button_ExportarCSV":
                    MostrarDialogoExportar();
                    break;
                case "Button_AgregarEstudiante":
                    this.vistaModelo.AgregarEstudiante();
                    break;
                case "Button_RegistrarEstudiantes":
                    RegistrarEstudiantes();
                    break;
                case "Button_EliminarEstudiante":
                    this.vistaModelo.EliminarEstudiante(((sender as Button).Tag as EstudianteImportado));
                    break;
            }
        }

        private async void RegistrarEstudiantes()
        {
            try
            {
                await this.vistaModelo.RegistrarEstudiantes();
            }
            catch (ArgumentException AException)
            {
                MessageBox.Show(AException.Message,
                                "Registros inválidos",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            catch (EntityException)
            {
                MensajesHelper.FaltaDeConexion();
            }
        }

        private async void MostrarDialogoImportar()
        {
            var dialogo = new Microsoft.Win32.OpenFileDialog();

            dialogo.DefaultExt = ".csv";
            dialogo.Filter = "CSV Files (.csv)|*.csv";

            bool? resultado = dialogo.ShowDialog();

            if (resultado == true)
            {
                string ruta = dialogo.FileName;
                await ImportarEstudiantesPorCSV(ruta);
            }
        }

        private void MostrarDialogoExportar()
        {
            var dialogo = new Microsoft.Win32.SaveFileDialog();

            dialogo.DefaultExt = ".csv";
            dialogo.Filter = "CSV Files (.csv)|*.csv";

            bool? resultado = dialogo.ShowDialog();

            if (resultado == true)
            {
                string ruta = dialogo.FileName;
                this.vistaModelo.ExportarEstudiantesEnCSV(ruta);
            }
        }

        private async Task ImportarEstudiantesPorCSV(string ruta)
        {
            try 
            {
                await this.vistaModelo.ImportarEstudiantesPorCSVAsync(ruta);
            }
            catch (ArgumentException AException)
            {
                MessageBox.Show(AException.Message,
                                "Registros inválidos",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            catch (HeaderValidationException)
            {
                MessageBox.Show("El archivo seleccionado no es .CSV o no tiene el formato correcto. " +
                                "Puede descargar el .CSV con el formato correcto dando clic al botón Importar .CSV",
                                "Formato inválido",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            catch (IOException)
            {
                MessageBox.Show("El archivo seleccionado est'a siendo usado por otro programa. " +
                                "Por favor, cierre ese programa e inténtelo de nuevo.",
                                "Archivo en uso",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            finally
            {
                this.vistaModelo.TerminaCarga = true;
            }
        }

        private void TextBox_Filtro_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.vistaModelo.FiltrarPropiedades((sender as TextBox).Text);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.vistaModelo.FiltrarErrores((bool)(sender as CheckBox).IsChecked);
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
