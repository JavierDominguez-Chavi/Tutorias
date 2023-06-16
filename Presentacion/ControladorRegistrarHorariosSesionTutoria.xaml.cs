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
    /// Lógica de interacción para ControladorRegistrarHorariosSesionTutoria.xaml
    /// </summary>
    public partial class ControladorRegistrarHorariosSesionTutoria : Page
    {
        PeriodosEscolaresDAO periodosEscolaresDAO = new PeriodosEscolaresDAO();
        FechasTutoriasDAO fechasTutoriasDAO = new FechasTutoriasDAO();
        EstudiantesDAO estudiantesDAO = new EstudiantesDAO();
        HorarioDAO horarioDAO = new HorarioDAO();
        ObservableCollection<Dominio.FechasTutorias> fechasTutoriasActuales = new ObservableCollection<Dominio.FechasTutorias>();
        ObservableCollection<Dominio.FechasTutorias> fechasTutoriasRegistradas = new ObservableCollection<Dominio.FechasTutorias>();
        ObservableCollection<Dominio.Estudiantes> estudiantesDelProfesor = new ObservableCollection<Dominio.Estudiantes>();
        ObservableCollection<Dominio.Horarios> horariosEncontrados = new ObservableCollection<Dominio.Horarios>();
        Dominio.PeriodosEscolares periodoEscolarActual = new Dominio.PeriodosEscolares();
        Dominio.TutoriasAcademicas tutoriaObtenida = new Dominio.TutoriasAcademicas();
        Dominio.TutoriasAcademicas tutoriaNueva = new Dominio.TutoriasAcademicas();
        TutoriasAcademicasDAO tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
        List<FechaTipoSesion> fechasConTipoSesion = new List<FechaTipoSesion>();

        int idProgramaEducativo = SingletonUsuario.Instance.AcademicosRoles.First().ProgramasEducativos.IdProgramaEducativo;
        int idAcademico = SingletonUsuario.Instance.AcademicosRoles.First().idAcademico;
        public ControladorRegistrarHorariosSesionTutoria()
        {
            InitializeComponent();
            InicializarComponentes();
        }

        //Carga las fechas de sesión de tutorías abiertas y los estudiantes
        private void InicializarComponentes()
        {
            periodoEscolarActual = periodosEscolaresDAO.ObtenerPeriodoEscolarActual();
            fechasTutoriasActuales = fechasTutoriasDAO.ObtenerFechasDeTutoriaActualesPorIdPeriodoEscolarProgramaEducativo(periodoEscolarActual.IdPeriodoEscolar, idProgramaEducativo);
            estudiantesDelProfesor = estudiantesDAO.ObtenerEstudiantesPorIdAcademico(idAcademico);
            Label_PeriodoEscolarActual.Content = periodoEscolarActual.Nombre;
            foreach (var fechaTutoria in fechasTutoriasActuales)
            {
                fechasConTipoSesion.Add(new FechaTipoSesion(fechaTutoria));
            }

            ComboBox_SesionesTutoria.ItemsSource = fechasConTipoSesion;
            ComboBox_SesionesTutoria.DisplayMemberPath = "FechaTipoSesionText";
            ComboBox_SesionesTutoria.SelectedIndex = 0;
            if (fechasTutoriasActuales.Count > 0)
            {
                if (estudiantesDelProfesor.Count > 0)
                {
                    var estudiantes = estudiantesDelProfesor.Select(estudiantesEncontrado => new
                    {
                        NombreCompleto = estudiantesEncontrado.Nombre + " " + estudiantesEncontrado.ApellidoPaterno + " " + estudiantesEncontrado.ApellidoMaterno,
                        Matricula = estudiantesEncontrado.Matricula
                    });
                    DataGrid_EstudiantesHorarios.ItemsSource = estudiantesDelProfesor;
                }
                else
                {
                    MessageBox.Show("No se pudo conectar con la base de datos. Por favor, inténtelo más tarde", "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("No existen fechas de tutorías cercanas", "Notificación", MessageBoxButton.OK);
            }
            

        }
        private bool ValidacionHoraPermitidaYCamposVacios()
        {
            bool esValido = true;
            bool horarioValido = false;
            bool camposVacios = false;
            int hora = 0;
            int minutos = 0;
            TimeSpan horaMaxima = new TimeSpan(21, 0, 0);
            foreach (Dominio.Estudiantes item in DataGrid_EstudiantesHorarios.Items)
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
            if(!horarioValido)
            {
                MessageBox.Show("No se puede registrar, verifiquen que los horarios estén entre las 7 y 21 horas", "Notificación", MessageBoxButton.OK);
            }
            return esValido;
        }
        private async void Button_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            bool horariosValidos = ValidacionHoraPermitidaYCamposVacios();
            List<Dominio.Horarios> horariosAAsignar = new List<Dominio.Horarios>();
            Dominio.Horarios horarioARegistrar = new Dominio.Horarios();
            int hora = 0;
            int minutos = 0;
            bool registrado = false;
            bool tutoriaRegistrada = true;
            if (horariosValidos)
            {
                DataGrid_EstudiantesHorarios.IsEnabled = false;
                Button_Aceptar.IsEnabled = false;
                var ventanaEmergente = CrearVentanaEmergente();
                ventanaEmergente.Show();
                FechaTipoSesion fechaSeleccionada = (FechaTipoSesion)ComboBox_SesionesTutoria.SelectedItem;
                tutoriaNueva.IdFechaTuria = fechaSeleccionada.FechaTutoria.IdFechaTutoria;
                tutoriaNueva.ComentarioGeneral = "Sin comentarios";
                tutoriaNueva.ReporteEntregado = false;
                tutoriaNueva.IdAcademico = idAcademico;
                tutoriaRegistrada = tutoriasAcademicasDAO.RegistrarTutoriaAcademica(tutoriaNueva);
                if (tutoriaRegistrada)
                {

                    tutoriaObtenida = tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdFechaTutoria(fechaSeleccionada.FechaTutoria.IdFechaTutoria);
                    foreach (Dominio.Estudiantes estudiante in DataGrid_EstudiantesHorarios.Items)
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
                                registrado = horarioDAO.RegistrarHorarioDeSesionDeTutoria(horarioARegistrar);
                            }
                        }
                    }
                    ventanaEmergente.Close();
                    DataGrid_EstudiantesHorarios.IsEnabled = true;
                    Button_Aceptar.IsEnabled = true;
                    if (registrado)
                    {
                        MessageBox.Show("Se ha registrado correctamente", "Notificación", MessageBoxButton.OK);
                        Button_Aceptar.IsEnabled = false;
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo conectar con la base de datos. Por favor inténtelo más tarde", "Error", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo registrar la tutoria academica", "Error", MessageBoxButton.OK);
                }
            }
           
            
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
        private void LimpiarCampos()
        {
            foreach (Dominio.Estudiantes estudiante in DataGrid_EstudiantesHorarios.Items)
            {
                //Accede a la columna de integerUpDown de horas y le da el valor de nulo
                var row = DataGrid_EstudiantesHorarios.ItemContainerGenerator.ContainerFromItem(estudiante) as DataGridRow;
                var celdaHoras = DataGrid_EstudiantesHorarios.Columns[2].GetCellContent(row) as FrameworkElement;
                var columnnaHoras = DataGrid_EstudiantesHorarios.Columns[2] as DataGridTemplateColumn;
                var templateHoras = columnnaHoras.CellTemplate as DataTemplate;
                var integerUpDownHoras = templateHoras.FindName("IntegerUpDown_Hora", celdaHoras) as IntegerUpDown;
                integerUpDownHoras.Value = null;
                //Accede a la columna de integerUpDown de minutos y le da el valor de n
                var celdaMinutos = DataGrid_EstudiantesHorarios.Columns[3].GetCellContent(row) as FrameworkElement;
                var columnnaMinutos = DataGrid_EstudiantesHorarios.Columns[3] as DataGridTemplateColumn;
                var templateMinutos = columnnaMinutos.CellTemplate as DataTemplate;
                var integerUpDownMinutos = templateMinutos.FindName("IntegerUpDown_Minutos", celdaMinutos) as IntegerUpDown;
                integerUpDownMinutos.Value = null;
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
        private void SeleccionarFecha(object sender, SelectionChangedEventArgs e)
        {
            Button_Aceptar.IsEnabled = true;
            try
            {
                FechaTipoSesion fechaSeleccionada = (FechaTipoSesion)ComboBox_SesionesTutoria.SelectedItem;
                tutoriaObtenida = tutoriasAcademicasDAO.ObtenerTutoriaAcademicaPorIdFechaTutoria(fechaSeleccionada.FechaTutoria.IdFechaTutoria);
                horariosEncontrados = horarioDAO.ObtenerHorariosDeTutoriaPorIdTutoriaYMatricula(estudiantesDelProfesor, tutoriaObtenida.IdTutoriaAcademica);
                if (horariosEncontrados.Count != 0)
                {
                    MessageBox.Show("Se han llenado las fechas de tutoría para esta sesión", "Confirmación", MessageBoxButton.OK);
                    Button_Aceptar.IsEnabled = false;
                    MessageBox.Show("Se han llenado las fechas de tutoria para esta sesión", "Confirmación", MessageBoxButton.OK);
                }
                
            } 
            catch (NullReferenceException excepcion)
            {
                Button_Aceptar.IsEnabled = false;
            }
            
        }

        private void Button_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea cancelar el registro de horarios de sesión de tutoría?", "Confirmación", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                this.NavigationService.Navigate(new PaginaPrincipalTutorAcademico());
            }
        }
    }   
}
