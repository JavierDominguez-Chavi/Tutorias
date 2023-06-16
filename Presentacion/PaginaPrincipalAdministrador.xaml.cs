using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para PaginaPrincipalAdministrador.xaml
    /// </summary>
    public partial class PaginaPrincipalAdministrador : Page
    {
        public PaginaPrincipalAdministrador()
        {
            InitializeComponent();
            Label_NombreUsuario.Content = SingletonUsuario.Instance.NombreUsuario;
        }

        private void Button_CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            SingletonUsuario.Instance.BorrarSinglenton();
            this.NavigationService.Navigate(new InicioDeSesion());
        }

        private void IrAConsultarAcademico(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControladorListaDeAcademicos());
        }
        private void IrAProgramasEducativos(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControladorRegistrarProgramaEducativo());
        }
        private void IrAPeriodosEscolares(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListaPeriodosEscolares());
        }

        private void IrARegistrarAcademico(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegistrarAcademico());
        }

        private void IrAListadoAcademicos(object sender, RoutedEventArgs e)
        {
            string mensaje = $"¿Desea consultar un académico o modificarlo?";
            String[] opciones = new String[3];
            opciones[0] = "Consultar académico";
            opciones[1] = "Modificar académico";
            opciones[2] = "Cancelar";
            MessageBoxResult result = ShowCustomMessageBox(mensaje, "Notificación", opciones);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.NavigationService.Navigate(new ControladorListaDeAcademicos());
                    break;
                case MessageBoxResult.No:
                    this.NavigationService.Navigate(new ListadoAcademicos());
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
            
        }
        private void IrAExperienciasEducativas(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControladorExperienciasEducativas());

        }

        private void SeleccionarGestionCursos(object sender, RoutedEventArgs e)
        {
            string mensaje = $"¿Desea agregar un nuevo curso o consultarlo?";
            String[] opciones = new String[3];
            opciones[0] = "Consultar";
            opciones[1] = "Agregar nuevo curso";
            opciones[2] = "Cancelar";
            MessageBoxResult result = ShowCustomMessageBox(mensaje, "Notificación", opciones);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.NavigationService.Navigate(new ControladorListaDeCursos());
                    break;
                case MessageBoxResult.No:
                    this.NavigationService.Navigate(new AgregarCurso());
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
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

        private void IrConsultarOfertaAcademica(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ConsultarOfertaAcademica());

        }

        private void IrAsignarExperienciaAEstudiante(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControladorAsignarExperienciaEducativaAEstudiante());

        }

        private void IrModificarRoles(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListaAcademicos());
        }
    }
}
