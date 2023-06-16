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
    /// Lógica de interacción para PaginaPrincipalCoordinadorDeTutorias.xaml
    /// </summary>
    public partial class PaginaPrincipalCoordinadorDeTutorias : Page
    {
        public PaginaPrincipalCoordinadorDeTutorias()
        {
            InitializeComponent();
            Label_NombreUsuario.Content = SingletonUsuario.Instance.NombreUsuario;
            
        }

        private void IrAConcentradoDeAsistencias(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControlDeAsistencias()); 
        }

        private void IrAFechasTutoria(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListaPeriodosEscolares());
        }

        private void IrAAsignacionDeTutores(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TutoresYTutorados());
        }

        private void Button_CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            SingletonUsuario.Instance.BorrarSinglenton();
            this.NavigationService.Navigate(new InicioDeSesion());
        }

        private void IrAImportarEstudiantes(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ImportarEstudiantes());
        }

        private void IrARegistroDeEstudiantes(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControladorRegistroEstudiantes());
        }

        private void IrAConsultaEstudiante(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControladorListaDeEstudiantes());
        }
        private void IrAConsultarReportePorTutorAcademico(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListadoDeReportesPorTutorAcademico());
        }

        private void IrARegistrarTutor(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControladorRegistroTutorAcademico());
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
