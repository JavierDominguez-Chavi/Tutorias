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
    /// Lógica de interacción para PaginaPrincipalJefeDeCarrera.xaml
    /// </summary>
    public partial class PaginaPrincipalJefeDeCarrera : Page
    {
        public PaginaPrincipalJefeDeCarrera()
        {
            InitializeComponent();
            Label_NombreUsuario.Content = SingletonUsuario.Instance.NombreUsuario;
        }

        private void Button_CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            SingletonUsuario.Instance.BorrarSinglenton();
            this.NavigationService.Navigate(new InicioDeSesion());
        }

        private void IrAControlDeAsistencias(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControlDeAsistencias());
        }


        private void IrAConsultaEstudiante(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControladorListaDeEstudiantes());
        } 
        private void IrAConsultarReportePorTutorAcademico(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListadoDeReportesPorTutorAcademico());
        }

        private void AbrirReporteGeneral(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ConsultarReporteGeneral());
        }

        private void IrVentanaProblematicasAcademicas(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListaProblematicasAcademicas());
        }

        private void IrAVentanaListadoTutoresAcademcos(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListadoTutoresAcademicos());
        }
    }
}
