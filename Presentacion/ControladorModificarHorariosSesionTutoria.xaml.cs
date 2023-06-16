using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;
using WindowStartupLocation = System.Windows.WindowStartupLocation;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ControladorModificarHorariosSesionTutoria.xaml
    /// </summary>
    public partial class ControladorModificarHorariosSesionTutoria : Page
    {
        PeriodosEscolaresDAO periodosEscolaresDAO = new PeriodosEscolaresDAO();
        FechasTutoriasDAO fechasTutoriasDAO = new FechasTutoriasDAO();
        EstudiantesDAO estudiantesDAO = new EstudiantesDAO();
        HorarioDAO horarioDAO = new HorarioDAO();
        ObservableCollection<Dominio.FechasTutorias> fechasTutoriasActuales = new ObservableCollection<Dominio.FechasTutorias>();
        ObservableCollection<Dominio.FechasTutorias> fechasTutoriasRegistradas = new ObservableCollection<Dominio.FechasTutorias>();
        ObservableCollection<Dominio.Estudiantes> estudiantesDelProfesor = new ObservableCollection<Dominio.Estudiantes>();
        ObservableCollection<Dominio.Horarios> horariosEncontrados = new ObservableCollection<Dominio.Horarios>();
        ObservableCollection<Dominio.TutoriasAcademicas> tutoriasRegistradas = new ObservableCollection<Dominio.TutoriasAcademicas>();
        Dominio.PeriodosEscolares periodoEscolarActual = new Dominio.PeriodosEscolares();
        Dominio.TutoriasAcademicas tutoriaObtenida = new Dominio.TutoriasAcademicas();
        TutoriasAcademicasDAO tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
        int idProgramaEducativo = SingletonUsuario.Instance.AcademicosRoles.First().ProgramasEducativos.IdProgramaEducativo;
        int idAcademico = SingletonUsuario.Instance.AcademicosRoles.First().idAcademico;
        public ControladorModificarHorariosSesionTutoria()
        {
            InitializeComponent();
            periodoEscolarActual = periodosEscolaresDAO.ObtenerPeriodoEscolarActual();
            fechasTutoriasActuales = fechasTutoriasDAO.ObtenerFechasDeTutoriaActualesPorIdPeriodoEscolarProgramaEducativo(periodoEscolarActual.IdPeriodoEscolar, idProgramaEducativo);
            estudiantesDelProfesor = estudiantesDAO.ObtenerEstudiantesPorIdAcademico(idAcademico);
            foreach (Dominio.FechasTutorias fechaObtenida in fechasTutoriasActuales)
            {
                tutoriaObtenida = tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdFechaTutoria(fechaObtenida.IdFechaTutoria);
                if (tutoriaObtenida.IdTutoriaAcademica != 0)
                {
                    fechasTutoriasRegistradas.Add(fechaObtenida);
                }

            }
            Label_PeriodoEscolarActual.Content = periodoEscolarActual.Nombre;
            List<FechaTipoSesion> fechasConTipoSesion = new List<FechaTipoSesion>();
            foreach (var fechaTutoria in fechasTutoriasRegistradas)
            {
                fechasConTipoSesion.Add(new FechaTipoSesion(fechaTutoria));
            }

            ComboBox_SesionesTutoria.ItemsSource = fechasConTipoSesion;
            ComboBox_SesionesTutoria.DisplayMemberPath = "FechaTipoSesionText";
            ComboBox_SesionesTutoria.SelectedIndex = 0;
        }

        private class EstudianteConHorario
        {
            public string NombreCompleto { get; set; }
            public string Matricula { get; set; }
            public int Hora { get; set; }
            public int Minutos { get; set; }
        }
        private bool ValidacionHoraPermitidaYCamposVacios()
        {
            bool esValido = true;
            bool horarioValido = false;
            bool camposVacios = false;
            int hora = 0;
            int minutos = 0;
            TimeSpan horaMaxima = new TimeSpan(21, 0, 0);
            foreach (EstudianteConHorario item in DataGrid_EstudiantesHorarios.Items)
            {
                try
                {
                    var row = DataGrid_EstudiantesHorarios.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    var celdaHoras = DataGrid_EstudiantesHorarios.Columns[2].GetCellContent(row) as FrameworkElement;
                    var columnnaHoras = DataGrid_EstudiantesHorarios.Columns[2] as DataGridTemplateColumn;
                    var templateHoras = columnnaHoras.CellTemplate as DataTemplate;
                    var integerUpDownHoras = templateHoras.FindName("IntegerUpDown_Hora", celdaHoras) as IntegerUpDown;
                    var valorHoras = integerUpDownHoras.Value;
                    if (valorHoras != null)
                    {
                        hora = valorHoras.Value;
                        var celdaMinutos = DataGrid_EstudiantesHorarios.Columns[3].GetCellContent(row) as FrameworkElement;
                        var columnnaMinutos = DataGrid_EstudiantesHorarios.Columns[3] as DataGridTemplateColumn;
                        var templateMinutos = columnnaMinutos.CellTemplate as DataTemplate;
                        var integerUpDownMinutos = templateMinutos.FindName("IntegerUpDown_Minutos", celdaMinutos) as IntegerUpDown;
                        var valorMinutos = integerUpDownMinutos.Value;
                        if (valorMinutos != null)
                        {
                            minutos = valorMinutos.Value;
                            TimeSpan horarioSeleccionado = new TimeSpan(hora, minutos, 0);
                            if (horarioSeleccionado < horaMaxima)
                            {
                                horarioValido = true;
                            }
                        }
                        else
                        {
                            esValido = false;
                            camposVacios = true;
                        }
                    }
                    else
                    {
                        esValido = false;
                        camposVacios = true;
                    }
                }
                catch
                {
                    MessageBox.Show("No se han llenado todos los campos necesarios", "Notificación", MessageBoxButton.OK);
                }

            }
            if (camposVacios)
            {
                MessageBox.Show("No se han llenado todos los campos necesarios", "Notificación", MessageBoxButton.OK);
            }
            if (!horarioValido)
            {
                MessageBox.Show("No se puede registrar, verifiquen que los horarios estén entre las 7 y 21 horas", "Notificación", MessageBoxButton.OK);
            }
            return esValido;
        }
        public class FechaTipoSesion
        {
            public Dominio.FechasTutorias FechaTutoria { get; set; }
            public string FechaTipoSesionText { get; set; }

            public FechaTipoSesion(Dominio.FechasTutorias tutoria)
            {
                FechaTutoria = tutoria;
                FechaTipoSesionText = $"{tutoria.DeterminarTipoDeSesion()} - {tutoria.FechaTutoria.ToShortDateString()}";
            }
        }
        private void SeleccionarFecha(object sender, SelectionChangedEventArgs e)
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
                    Hora = item.Horario.HoraTutoria.Hours,
                    Minutos = item.Horario.HoraTutoria.Minutes
                };
                estudiantesConHorario.Add(estudianteConHorario);
            }
            if (estudiantesConHorario.Count() > 0)
            {
                DataGrid_EstudiantesHorarios.ItemsSource = estudiantesConHorario;
            }
            else
            {
                MessageBox.Show("No existen horarios registrados para esta sesión de tutoría", "Notificación", MessageBoxButton.OK);
            }

        }

        private async void Button_Guardar_Click(object sender, RoutedEventArgs e)
        {
            bool horariosValidos = ValidacionHoraPermitidaYCamposVacios();
            List<Dominio.Horarios> horariosAAsignar = new List<Dominio.Horarios>();
            Dominio.Horarios horarioARegistrar = new Dominio.Horarios();
            int hora = 0;
            int minutos = 0;
            bool registrado = false;
            if (horariosValidos)
            {
                DataGrid_EstudiantesHorarios.IsEnabled = false;
                Button_Guardar.IsEnabled = false;
                var ventanaEmergente = CrearVentanaEmergente();
                ventanaEmergente.Show();

                foreach (EstudianteConHorario estudiante in DataGrid_EstudiantesHorarios.Items)
                {
                    var row = DataGrid_EstudiantesHorarios.ItemContainerGenerator.ContainerFromItem(estudiante) as DataGridRow;
                    var celdaHoras = DataGrid_EstudiantesHorarios.Columns[2].GetCellContent(row) as FrameworkElement;
                    var columnnaHoras = DataGrid_EstudiantesHorarios.Columns[2] as DataGridTemplateColumn;
                    var templateHoras = columnnaHoras.CellTemplate as DataTemplate;
                    var integerUpDownHoras = templateHoras.FindName("IntegerUpDown_Hora", celdaHoras) as IntegerUpDown;
                    var valorHoras = integerUpDownHoras.Value;
                    if (valorHoras != null)
                    {
                        hora = valorHoras.Value;
                        var celdaMinutos = DataGrid_EstudiantesHorarios.Columns[3].GetCellContent(row) as FrameworkElement;
                        var columnnaMinutos = DataGrid_EstudiantesHorarios.Columns[3] as DataGridTemplateColumn;
                        var templateMinutos = columnnaMinutos.CellTemplate as DataTemplate;
                        var integerUpDownMinutos = templateMinutos.FindName("IntegerUpDown_Minutos", celdaMinutos) as IntegerUpDown;
                        var valorMinutos = integerUpDownMinutos.Value;
                        if (valorMinutos != null)
                        {
                            minutos = valorMinutos.Value;
                            TimeSpan horaSesionTutoria = new TimeSpan(hora, minutos, 0);
                            horarioARegistrar.Asistencia = false;
                            horarioARegistrar.Riesgo = false;
                            horarioARegistrar.HoraTutoria = horaSesionTutoria;
                            horarioARegistrar.IdTutoriaAcademica = tutoriaObtenida.IdTutoriaAcademica;
                            horarioARegistrar.Matricula = estudiante.Matricula;
                            registrado = horarioDAO.ModificarHorarioDeSesionDeTutoriaPorMatriculaYIdTutoria(horarioARegistrar);
                        }
                    }
                }

                ventanaEmergente.Close();
                DataGrid_EstudiantesHorarios.IsEnabled = true;
                Button_Guardar.IsEnabled = true;

                if (registrado)
                {
                    MessageBox.Show("Se ha modificado correctamente", "Notificación", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("No se pudo conectar con la base de datos. Por favor inténtelo más tarde", "Error", MessageBoxButton.OK);
                }
            }
        }

        private Window CrearVentanaEmergente()
        {
            var ventanaEmergente = new Window
            {
                WindowStyle = WindowStyle.None,
                AllowsTransparency = true,
                Background = Brushes.Transparent,
                Width = 300,
                Height = 150,
                Topmost = true,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Content = new Grid
                {
                    Background = Brushes.White,
                    Margin = new Thickness(10),
                }
            };

            var grid = ventanaEmergente.Content as Grid;

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });

            var textBlock = new TextBlock
            {
                Text = "Guardando horarios, por favor espere",
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var progressBar = new ProgressBar
            {
                IsIndeterminate = true,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Bottom,
                Height = 20,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Grid.SetRow(textBlock, 0);
            Grid.SetRow(progressBar, 1);

            grid.Children.Add(textBlock);
            grid.Children.Add(progressBar);

            return ventanaEmergente;
        }


       

        private void Button_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea cancelar la modificación de horarios de sesión de tutoría?", "Confirmación", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                this.NavigationService.Navigate(new PaginaPrincipalTutorAcademico());
            }
        }
    }
}
