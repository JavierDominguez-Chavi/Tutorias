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

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ReportePorTutorAcademico.xaml
    /// </summary>
    public partial class ConsultarReportePorTutorAcademico : Page
    {
        private ReportePorTutorAcademico ReporteSeleccionado { get; set; }
        private static int ID_PROGRAMA_EDUCATIVO = 1;
        private EstudiantesDAO estudiantesDAO;
        private HorarioDAO horarioDAO;
        private TutoriasAcademicasDAO tutoriasAcademicasDAO;
        private ProblematicasDAO problematicasDAO;
        private ObservableCollection<Estudiantes> estudiantes;
        public ConsultarReportePorTutorAcademico()
        {
            InitializeComponent();
        }
        public ConsultarReportePorTutorAcademico(ReportePorTutorAcademico reporteSeleccionado)
        {
            InitializeComponent();
            this.ReporteSeleccionado = reporteSeleccionado;
            estudiantesDAO = new EstudiantesDAO();
            horarioDAO = new HorarioDAO();
            tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
            problematicasDAO = new ProblematicasDAO();
            estudiantes = new ObservableCollection<Estudiantes>();
            MostrarCabeceraReporte();
            ConsultarEstudiantes();
            MostrarComentarioGeneral();
            MostrarProblematicasAcademicas();
        }
        private void MostrarCabeceraReporte()
        {
            Label_PeriodoEscolar.Content = ReporteSeleccionado.PeriodoEscolar;
            Label_NumeroDeSesion.Content = ReporteSeleccionado.NumeroDeSesion;
            Label_FechaDeTutoria.Content = ReporteSeleccionado.FechaTutoria.FechaTutoria.ToShortDateString();
            Label_NombreTutorAcademico.Content = ReporteSeleccionado.NombreTutorAcademico;
            ProgramasEducativosDAO programasEducativosDAO = new ProgramasEducativosDAO();
            Label_ProgramaEducativo.Content = programasEducativosDAO.ObtenerProgramaEducativoPorIdProgramaEducativo(ID_PROGRAMA_EDUCATIVO).NombreProgramaEducativo;
        }

        private void ConsultarEstudiantes()
        {
            estudiantes = estudiantesDAO.ObtenerTutoradosPorProgramaEducativo(
                new Academicos() { IdAcademico = ReporteSeleccionado.IdTutorAcademico }, ID_PROGRAMA_EDUCATIVO);
            ObservableCollection<Horarios> horarios =
                horarioDAO.ObtenerHorariosDeTutoriaPorIdTutoriaYMatricula(estudiantes, ReporteSeleccionado.IdTutoriaAcademica);
            List<EstadoEstudianteTutoria> estadoEstudiantes = new List<EstadoEstudianteTutoria>();
            foreach (Horarios horario in horarios)
            {
                estadoEstudiantes.Add(new EstadoEstudianteTutoria()
                {
                    Nombre = horario.Estudiantes.NombreCompleto,
                    Asistio = horario.Asistencia,
                    EnRiesgo = horario.Riesgo
                });
            }
            DataGrid_ListadoDeEstudiantes.ItemsSource = estadoEstudiantes;
        }
        private void MostrarComentarioGeneral()
        {
            TextBlock_ComentarioGeneral.Text = tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdTutoriaAcademica(ReporteSeleccionado.IdTutoriaAcademica).ComentarioGeneral;    
        }
        private void MostrarProblematicasAcademicas()
        {
            List<ProblematicaAcademica_Reporte> problematicas = new List<ProblematicaAcademica_Reporte>();
            foreach(Estudiantes e in estudiantes)
            {
                ObservableCollection<Problematicas> problematicasEstudiante = 
                    problematicasDAO.ObtenerProblematicasPorIdTutoriaYMatricula(ReporteSeleccionado.IdTutoriaAcademica, e.Matricula);
                if (problematicasEstudiante.Any())
                {
                    foreach(Problematicas p in problematicasEstudiante)
                    {
                        problematicas.Add(new ProblematicaAcademica_Reporte()
                        {
                            ExperienciaEducativa = p.ExperienciasEducativas_Academicos.ExperienciasEducativas.Nombre, 
                            Academico = p.ExperienciasEducativas_Academicos.Academicos.NombreCompleto, 
                            Titulo = p.Titulo,
                            Descripcion = p.Descripcion
                        });
                    }
                }
            }
            DataGrid_ProblematicasAcademicas.ItemsSource = problematicas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

    }
    public class EstadoEstudianteTutoria{
        public string Nombre { get; set; }
        public bool Asistio { get; set; }
        public bool EnRiesgo { get; set; }
    }
    public class ProblematicaAcademica_Reporte
    {
        public string ExperienciaEducativa { get; set; }
        public string Academico { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

    }
}
