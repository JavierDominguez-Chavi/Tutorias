using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
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
using AccesoADatos;
using Dominio;
using LogicaDelNegocio;
using Presentacion.Properties;
using mensajes = Presentacion.Properties.Mensajes;

namespace Presentacion
{
    public partial class LlenarReporteDeTutoriaAcademica : Page
    {
        private int idTutoria;
        private Dominio.TutoriasAcademicas TutoriaAcademica;
        private ObservableCollection<Dominio.Horarios> horarios;
        public static ObservableCollection<Dominio.Problematicas> Problematicas { get; set; }
        private int totalAsistencias;
        private int totalEnRiesgo;
        public LlenarReporteDeTutoriaAcademica(int idTutoriaAcademica)
        {
            InitializeComponent();
            idTutoria = idTutoriaAcademica;
            TutoriaAcademica = LlenarInformacionTutoriaAcademica();
            TutoriaAcademica.ComentarioGeneral = "";
            this.DataContext = TutoriaAcademica;
            Problematicas = new ObservableCollection<Dominio.Problematicas>();
        }

        private void CargarInformacionPagina(object sender, RoutedEventArgs e)
        {
            HorarioDAO horarioDAO = new HorarioDAO();
            horarios = horarioDAO.ObtenerHorariosDeTutoriaPorIdTutoria(idTutoria);
            if (horarios.FirstOrDefault() != null)
            {
                CollectionViewSource ViewSource_Estudiantes =
                        ((CollectionViewSource)(this.FindResource("ViewSource_Estudiantes")));
                ViewSource_Estudiantes.Source = horarios;
            }
            else
            {
                MensajesHelper.FaltaDeConexion();
            }
        }

        private Dominio.TutoriasAcademicas LlenarInformacionTutoriaAcademica()
        {
            this.TextBlock_ProgramaEducativo.Text = SingletonUsuario.Instance.AcademicosRoles.First().ProgramasEducativos.NombreProgramaEducativo;
            Dominio.TutoriasAcademicas tutoriaAcademica = new Dominio.TutoriasAcademicas();
            TutoriasAcademicasDAO tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
            tutoriaAcademica = tutoriasAcademicasDAO.ConsultarInformacionCompletaTutoriaAcademica(idTutoria);
            if (tutoriaAcademica == null)
            {
                MensajesHelper.FaltaDeConexion();
            }
            return tutoriaAcademica;
        }
        private void AumentarTotalYPorcentajeDeEstudiantesAsistentes(object sender, RoutedEventArgs e)
        {
            totalAsistencias += 1;
            double porcentaje = (totalAsistencias * 100) / horarios.Count;
            this.TextBox_TotalAsistentes.Text = totalAsistencias + " " + "(" + porcentaje.ToString("F2") + "%" + ")";
            HabilitarBotonProblematicaAcademica();
        }
        private void DisminuirTotalYPorcentajeDeEstudiantesAsistentes(object sender, RoutedEventArgs e)
        {
            totalAsistencias -= 1;
            float porcentaje = (totalAsistencias * 100) / horarios.Count;
            this.TextBox_TotalAsistentes.Text = totalAsistencias + " " + "(" + porcentaje.ToString("F2") + "%" + ")";
            HabilitarBotonProblematicaAcademica();
        }

        private void AumentarTotalYPorcentajeDeEstudiantesEnRiesgo(object sender, RoutedEventArgs e)
        {
            totalEnRiesgo += 1;
            float porcentaje = (totalEnRiesgo * 100) / horarios.Count;
            this.TextBox_TotalEnRiesgo.Text = totalEnRiesgo + " " + "(" + porcentaje.ToString("F2") + "%" + ")";
            HabilitarBotonProblematicaAcademica();
        }
        private void DisminuirTotalYPorcentajeDeEstudiantesEnRiesgo(object sender, RoutedEventArgs e)
        {
            totalEnRiesgo -= 1;
            float porcentaje = (totalEnRiesgo * 100) / horarios.Count;
            this.TextBox_TotalEnRiesgo.Text = totalEnRiesgo + " " + "(" + porcentaje.ToString("F2") + "%" + ")";
            HabilitarBotonProblematicaAcademica();
        }

        private void HabilitarBotonProblematicaAcademica()
        {
            if (totalAsistencias != 0 || totalEnRiesgo != 0)
            {
                this.Button_ProblematicaAcademica.IsEnabled = true;
            }
            else
            {
                this.Button_ProblematicaAcademica.IsEnabled = false;
            }
        }

        private void IngresarProblematicaAcademica(object sender, RoutedEventArgs e)
        {
            List<Dominio.Estudiantes> estudiantesTutor = new List<Dominio.Estudiantes>();
            foreach (Dominio.Horarios horario in horarios)
            {
                estudiantesTutor.Add(horario.Estudiantes);
            }
            ProblematicaAcademica problematicaAcademica = new ProblematicaAcademica(estudiantesTutor);
            this.NavigationService.Navigate(problematicaAcademica);
        }

        private void CerrarVentanaLlenarReporteDeTutoriaAcademica(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultadoMessageBox =
            MessageBox.Show(mensajes.messageBoxText_CancelarOperacionReporte, mensajes.messageBoxTitle_CancelarOperacion,
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (resultadoMessageBox == MessageBoxResult.Yes)
            {
                this.NavigationService.GoBack();
            }
        }

        private void MostrarMensajeDeConfirmacion(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultadoMessageBox =
            MessageBox.Show(mensajes.messageBoxText_ConfirmacionCambios, mensajes.messageBoxTitle_ConfirmacionCambios,
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (resultadoMessageBox == MessageBoxResult.Yes)
            {
                ValidarReporteDeTutoriaAcademicaNoVacio();
            }
        }

        private void ValidarReporteDeTutoriaAcademicaNoVacio()
        {
            if (Problematicas.Count == 0 && String.IsNullOrWhiteSpace(this.TextBlock_ComentarioGeneral.Text))
            {
                MessageBox.Show(mensajes.messageBoxText_ReporteTutoriaVacio, mensajes.messageBoxTitle_ReporteTutoriaVacio,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                GuardarInformacionTutoriaAcademica();
            }
        }

        private void GuardarInformacionTutoriaAcademica()
        {
            TutoriaAcademica.ReporteEntregado = true;
            TutoriasAcademicasDAO tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
            Boolean estadosEstudiantesRegistrados = true;
            Boolean problematicasRegistradas = true;
            Boolean tutoriaAcademicaRegistrada = true;

            if(VerificarCambioEnEstadosEstudiantes())
            {
                if (RegistrarAsistenciasYEstadosDeEstudiantes() == 0)
                {
                    estadosEstudiantesRegistrados = true;
                }
            }

            if (Problematicas.Count > 0)
            {
                if (RegistrarProblematicasAcademicas() == 0)
                {
                    problematicasRegistradas = false;
                }
            }

            if (!String.IsNullOrWhiteSpace(this.TextBlock_ComentarioGeneral.Text))
            {
                tutoriaAcademicaRegistrada = tutoriasAcademicasDAO.GuardarInformacionTutoriaAcademica(TutoriaAcademica, false);
            }

            if (estadosEstudiantesRegistrados && problematicasRegistradas && tutoriaAcademicaRegistrada)
            {
                MensajesHelper.OperacionExitosa();
                this.NavigationService.GoBack();
            }
            else
            {
                MensajesHelper.FaltaDeConexion();
            }

        }

        private Boolean VerificarCambioEnEstadosEstudiantes()
        {
            Boolean cambios = false;
            foreach (Dominio.Horarios horario in horarios)
            {
                if (horario.Asistencia || horario.Riesgo)
                {
                    return cambios = true;
                }
            }
            return cambios;
        }

        private int RegistrarAsistenciasYEstadosDeEstudiantes()
        {
            int numeroRegistros = 0;
            HorarioDAO horarioDAO = new HorarioDAO();
            foreach (Dominio.Horarios horario in horarios)
            {
                if (horarioDAO.RegistrarAsistenciasYEstadoDeEstudiantes(horario))
                {
                    numeroRegistros++;
                }
            }
            return numeroRegistros;
        }

        private int RegistrarProblematicasAcademicas()
        {
            int numeroRegistros = 0;
            ProblematicasDAO problematicasDAO = new ProblematicasDAO();
            foreach (Dominio.Problematicas problematica in Problematicas)
            {
                problematica.IdTutoriaAcademica = idTutoria;
                if (problematicasDAO.RegistrarProblematicasAcademicas(problematica))
                {
                    TutoriasAcademicasDAO tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
                    tutoriasAcademicasDAO.GuardarInformacionTutoriaAcademica(TutoriaAcademica, true);
                    numeroRegistros++;
                }
            }
            return numeroRegistros;
        }
    }
}
