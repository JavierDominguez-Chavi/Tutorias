using AccesoADatos;
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
using static System.Windows.Forms.LinkLabel;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ControladorConsultarHorariosSesiónTutoria.xaml
    /// </summary>
    public partial class ControladorConsultarHorariosSesiónTutoria : Page
    {
        PeriodosEscolaresDAO periodosEscolaresDAO = new PeriodosEscolaresDAO();
        FechasTutoriasDAO fechasTutoriasDAO = new FechasTutoriasDAO();
        EstudiantesDAO estudiantesDAO = new EstudiantesDAO();
        HorarioDAO horarioDAO = new HorarioDAO();
        ObservableCollection<Dominio.FechasTutorias> fechasTutoriasActuales = new ObservableCollection<Dominio.FechasTutorias>();
        ObservableCollection<Dominio.Estudiantes> estudiantesDelProfesor = new ObservableCollection<Dominio.Estudiantes>();
        ObservableCollection<Dominio.Horarios> horariosEncontrados = new ObservableCollection<Dominio.Horarios>();
        ObservableCollection<Dominio.TutoriasAcademicas> tutoriasRegistradas = new ObservableCollection<Dominio.TutoriasAcademicas>();
        Dominio.PeriodosEscolares periodoEscolarActual = new Dominio.PeriodosEscolares();
        Dominio.TutoriasAcademicas tutoriaObtenida = new Dominio.TutoriasAcademicas();
        TutoriasAcademicasDAO tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
        int idProgramaEducativo = SingletonUsuario.Instance.AcademicosRoles.First().ProgramasEducativos.IdProgramaEducativo;
        int idAcademico = SingletonUsuario.Instance.AcademicosRoles.First().idAcademico;
        public ControladorConsultarHorariosSesiónTutoria()
        {
            InitializeComponent();
            periodoEscolarActual = periodosEscolaresDAO.ObtenerPeriodoEscolarActual();
            fechasTutoriasActuales = fechasTutoriasDAO.ObtenerFechasDeTutoriaDelPeriodoEscolarPorIdPeriodoEscolarProgramaEducativo(periodoEscolarActual.IdPeriodoEscolar, idProgramaEducativo);
            estudiantesDelProfesor = estudiantesDAO.ObtenerEstudiantesPorIdAcademico(idAcademico);
            Label_PeriodoEscolarActual.Content = periodoEscolarActual.Nombre;
            List<FechaTipoSesion> fechasConTipoSesion = new List<FechaTipoSesion>();
            foreach (var fechaTutoria in fechasTutoriasActuales)
            {
                fechasConTipoSesion.Add(new FechaTipoSesion(fechaTutoria));
            }

            ComboBox_SesionesTutoria.ItemsSource = fechasConTipoSesion;
            ComboBox_SesionesTutoria.DisplayMemberPath = "FechaTipoSesionText";
            ComboBox_SesionesTutoria.SelectedIndex = 0;
        }
        private class FechaTipoSesion
        {
            public Dominio.FechasTutorias FechaTutoria { get; set; }
            public string FechaTipoSesionText { get; set; }

            public FechaTipoSesion(Dominio.FechasTutorias tutoria)
            {
                FechaTutoria = tutoria;
                FechaTipoSesionText = $"{tutoria.DeterminarTipoDeSesion()} - {tutoria.FechaTutoria.ToShortDateString()}";
            }
        }




        private class EstudianteConHorario
        {
            public string NombreCompleto {  get; set; }
            public string Matricula { get; set; }
            public TimeSpan Horario { get; set; }
        }

        private void FechaSeleccionada(object sender, SelectionChangedEventArgs e)
        {
            FechaTipoSesion fechaSeleccionada = (FechaTipoSesion)ComboBox_SesionesTutoria.SelectedItem;
            tutoriaObtenida = tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdFechaTutoria(fechaSeleccionada.FechaTutoria.IdFechaTutoria);
            horariosEncontrados = horarioDAO.ObtenerHorariosDeTutoriaPorIdTutoriaYMatricula(estudiantesDelProfesor, tutoriaObtenida.IdTutoriaAcademica);
            var estudianteHorarios = from estudiante in estudiantesDelProfesor
                                     join horario in horariosEncontrados on estudiante.Matricula equals horario.Matricula
                                     select new { Estudiante = estudiante, Horario = horario };
            ObservableCollection<EstudianteConHorario> estudiantesConHorario = new ObservableCollection<EstudianteConHorario>();
            foreach (var item in estudianteHorarios)
            {
                EstudianteConHorario estudianteConHorario = new EstudianteConHorario()
                {
                    NombreCompleto = item.Estudiante.NombreCompleto,
                    Matricula = item.Estudiante.Matricula,
                    Horario = item.Horario.HoraTutoria
                };
                estudiantesConHorario.Add(estudianteConHorario);
            } 
            if(estudiantesConHorario.Count() > 0)
            {
                DataGrid_EstudiantesHorarios.ItemsSource = estudiantesConHorario;
            }
            else
            {
                MessageBox.Show("No existen horarios registrados para esta sesión de tutoría", "Notificación", MessageBoxButton.OK);
            }
            
        }

        private void Button_Regresar_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PaginaPrincipalTutorAcademico());
        }
    }
}
