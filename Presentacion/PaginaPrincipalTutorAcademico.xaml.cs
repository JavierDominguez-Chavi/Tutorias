using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using mensajes = Presentacion.Properties.Mensajes;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para PaginaPrincipalTutorAcademico.xaml
    /// </summary>
    public partial class PaginaPrincipalTutorAcademico : Page
    {
        
        public PaginaPrincipalTutorAcademico()
        {
            InitializeComponent();
            Label_NombreUsuario.Content = SingletonUsuario.Instance.NombreUsuario;

        }

        private void Button_CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            SingletonUsuario.Instance.BorrarSinglenton();
            this.NavigationService.Navigate(new InicioDeSesion());
        }

        private void CrearReporteDeTutoria(object sender, RoutedEventArgs e)
        {
            FechasTutoriasDAO fechasTutoriasDAO = new FechasTutoriasDAO();
            DateTime fechaActual = DateTime.Now;
            Dominio.FechasTutorias fechaTutoria = fechasTutoriasDAO.ObtenerIdFechaTutoriaActualPorFechaDelSistema(fechaActual);
            if (fechaTutoria != null)
            {
                ValidarHorarioRegistrado(fechaTutoria);
            }
            else
            {
                MessageBox.Show(mensajes.messageBoxText_FechaTutoriaConcluida, mensajes.messageBoxTitle_FechaTutoriaConcluida,
                     MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ValidarHorarioRegistrado(Dominio.FechasTutorias fechaTutoria)
        {
            TutoriasAcademicasDAO tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
            ObservableCollection<Dominio.TutoriasAcademicas> tutoriasAcademicas =
                tutoriasAcademicasDAO.ObtenerTutoriasAcademicasPorIdFechaTutoriaEIdAcademico(fechaTutoria.IdFechaTutoria, SingletonUsuario.Instance.AcademicosRoles.FirstOrDefault().idAcademico);
            if (tutoriasAcademicas.FirstOrDefault() != null)
            {
                Boolean reporteEntregado =
                    tutoriasAcademicasDAO.ValidarQueTutorNoEntregoYaElReporte(fechaTutoria.IdFechaTutoria, SingletonUsuario.Instance.AcademicosRoles.First().idAcademico);
                if (!reporteEntregado)
                {
                    LlenarReporteDeTutoriaAcademica llenarReporteDeTutoria =
                        new LlenarReporteDeTutoriaAcademica(tutoriasAcademicas.FirstOrDefault().IdTutoriaAcademica);
                    this.NavigationService.Navigate(llenarReporteDeTutoria);
                }
                else
                {
                    MessageBox.Show(mensajes.messageBoxText_ReporteEntregado, mensajes.messageBoxTitle_ReporteEntregado,
                         MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show(mensajes.messageBoxText_HorarioSinRegistrar, mensajes.messageBoxTitle_HorarioSinRegistrar,
                         MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void IrVentanaProblematicasAcademicas(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListaProblematicasAcademicas());
        }
        
        private void IrARegistrarHorario(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControladorRegistrarHorariosSesionTutoria());
        }

        private void IrAConsultarHorarios(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControladorConsultarHorariosSesiónTutoria());
        }

        private void IrAModificarHorarios(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ControladorModificarHorariosSesionTutoria());
        }
    }
}
