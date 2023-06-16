using AccesoADatos;
using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using Button = System.Windows.Controls.Button;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ControladorListaDeAcademicos.xaml
    /// </summary>
    public partial class ControladorListaDeAcademicos : Page
    {
        AcademicosDAO academicosDAO = new AcademicosDAO();
        public List<Dominio.Academicos> academicosObtenidos = new List<Dominio.Academicos>();
        DataSet dataSetAcademicos = new DataSet();
        public ControladorListaDeAcademicos()
        {
            InitializeComponent();
            academicosObtenidos = academicosDAO.ObtenerAcademicos();
            if (academicosObtenidos != null)
            {
                var academicos = academicosObtenidos.Select(academicoEncontrado => new {
                    NombreCompleto = academicoEncontrado.Nombre + " " + academicoEncontrado.ApellidoPaterno + " " + academicoEncontrado.ApellidoMaterno,
                });
                DataGrid_ListaDeAcademicos.ItemsSource = academicosObtenidos;
            }
            else
            {
                System.Windows.MessageBox.Show("No se pudo conectar con la base de datos. Por favor, inténtelo más tarde", "Error", MessageBoxButton.OK);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PaginaPrincipalAdministrador());
        }
        private MessageBoxResult ShowCustomMessageBox(string mensaje, string mensajeOpciones, string[] contenidoBotones)
        {
            var estilo = new Style(typeof(Button));
            estilo.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.LightGray));
            estilo.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));

            var primerDisparador = new Trigger() { Property = Button.IsMouseOverProperty, Value = true };
            primerDisparador.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Gray));
            estilo.Triggers.Add(primerDisparador);

            var segundoDisparador = new Trigger() { Property = Button.IsPressedProperty, Value = true };
            segundoDisparador.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.DarkGray));
            estilo.Triggers.Add(segundoDisparador);

            var stackPanel = new StackPanel() { Margin = new Thickness(10) };
            stackPanel.Children.Add(new TextBlock() { Text = mensaje, Margin = new Thickness(0, 0, 0, 10) });

            MessageBoxResult result = MessageBoxResult.None;

            for (int i = 0; i < contenidoBotones.Length; i++)
            {
                Button boton = new Button() { Content = contenidoBotones[i], Style = estilo, Margin = new Thickness(0, 0, 0, 10) };
                int index = i; 
                boton.Click += (sender, e) =>
                {
                    result = (MessageBoxResult)(MessageBoxResult.Yes + index); 
                    CloseMessageBox();
                };

                stackPanel.Children.Add(boton);
            }

            var messageBoxWindow = new Window()
            {
                Title = mensajeOpciones,
                Content = stackPanel,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.ToolWindow,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true
            };

            messageBoxWindow.ShowDialog();

            return result;
        }




        private void CloseMessageBox()
        {
            System.Windows.Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(w => w.IsActive)
                ?.Close();
        }

        private void academicoSeleccionado(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid_ListaDeAcademicos.SelectedItem != null)
            {
                Dominio.Academicos academicoSeleccionado = (Dominio.Academicos)DataGrid_ListaDeAcademicos.SelectedItem;
                string mensaje = $"¿Desea consultar el académico {academicoSeleccionado.NombreCompleto} o asignarle experiencias educativas?";
                String[] opciones = new String[3];
                opciones[0] = "Consultar";
                opciones[1] = "Asignar experiencias educativas";
                opciones[2] = "Cancelar";
                MessageBoxResult result = ShowCustomMessageBox(mensaje, "Notificación", opciones);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        ControladorConsultarAcademico consultarAcademico = new ControladorConsultarAcademico(academicoSeleccionado);
                        consultarAcademico.DataContext = academicoSeleccionado;
                        consultarAcademico.ShowDialog();
                        break;
                    case MessageBoxResult.No:
                        ControladorAsignarExperienciasEducativasAProfesor asignarExperienciasEducativasAProfesor = new ControladorAsignarExperienciasEducativasAProfesor(academicoSeleccionado);
                        asignarExperienciasEducativasAProfesor.DataContext = academicoSeleccionado;
                        asignarExperienciasEducativasAProfesor.ShowDialog();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
        }
    }
}
