using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ListadoDeReportesPorTutorAcademico.xaml
    /// </summary>
    public partial class ListadoDeReportesPorTutorAcademico : Page
    {
        private PeriodosEscolaresDAO periodosEscolaresDAO;
        private static int ID_PROGRAMAEDUCATIVO = SingletonUsuario.Instance.AcademicosRoles.First().ProgramasEducativos.IdProgramaEducativo;
        private AcademicosRolesDAO academicosRolesDAO;
        private FechasTutoriasDAO fechasTutoriasDAO;
        private List<AcademicosRoles> listaTutoresAcademicos;
        ObservableCollection<FechasTutorias> fechasTutorias;
        private TutoriasAcademicasDAO tutoriasAcademicasDAO;
        private List<TutoriasAcademicas> tutoriasAcademicasTutor;
        private AcademicosDAO academicosDAO;
        public ListadoDeReportesPorTutorAcademico()
        {
            InitializeComponent();
            periodosEscolaresDAO = new PeriodosEscolaresDAO();
            academicosRolesDAO = new AcademicosRolesDAO();
            fechasTutoriasDAO = new FechasTutoriasDAO();
            tutoriasAcademicasDAO = new TutoriasAcademicasDAO();
            tutoriasAcademicasTutor = new List<TutoriasAcademicas>();
            academicosDAO = new AcademicosDAO();
            LlenarComboBoxPeriodosEscolares();
            MostrarTutoriasAcademicas();
        }

        private void LlenarComboBoxPeriodosEscolares()
        {
            ObservableCollection<PeriodosEscolares> periodosEscolares = periodosEscolaresDAO.ObtenerPeriodosEscolares();
            if (periodosEscolares != null)
            {
                foreach (PeriodosEscolares pe in periodosEscolares)
                {
                    ComboBox_PeriodosEscolares.Items.Add(pe);
                }
                ComboBox_PeriodosEscolares.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No existen periodos escolares registrados");
            }
        }

        private void ConsultarTutoresAcademicos()
        {
            listaTutoresAcademicos = academicosRolesDAO.ConsularTutoresAcademicosPorProgramaEducativo(ID_PROGRAMAEDUCATIVO);
        }

        private void ConsultarFechasTutorias()
        {
            fechasTutorias = 
                fechasTutoriasDAO.ObtenerFechasTutoriasPorPeriodoEscolarYProgramaEducativo(
                    ((PeriodosEscolares)ComboBox_PeriodosEscolares.SelectedItem).IdPeriodoEscolar, 
                    ID_PROGRAMAEDUCATIVO);
        }

        private void ConsultarTutoriasAcademicas()
        {
            ConsultarTutoresAcademicos();
            ConsultarFechasTutorias();
            tutoriasAcademicasTutor.Clear();
            for (int i = 0; i < fechasTutorias.Count; i++)
            {
                for (int j = 0; j < listaTutoresAcademicos.Count; j++)
                {
                    var tutoriasAcademicasEncontradas = tutoriasAcademicasDAO.ObtenerTutoriasAcademicasPorIdFechaTutoriaEIdAcademico(fechasTutorias.ElementAt(i).IdFechaTutoria,
                        listaTutoresAcademicos.ElementAt(j).idAcademico);
                    foreach(TutoriasAcademicas tA in tutoriasAcademicasEncontradas)
                    {
                        tutoriasAcademicasTutor.Add(tA);
                    }
                }
            }
        }
        private void MostrarTutoriasAcademicas()
        {
            ConsultarTutoriasAcademicas();
            List<ReportePorTutorAcademico> reportes = new List<ReportePorTutorAcademico>();
            if (tutoriasAcademicasTutor.Count == 0)
            {
                Label_NoTutorias.Visibility = Visibility.Visible;
            }
            foreach (TutoriasAcademicas tA in tutoriasAcademicasTutor)
            {
                reportes.Add(new ReportePorTutorAcademico() {
                    PeriodoEscolar = tA.FechasTutorias.PeriodosEscolares.Nombre,
                    NumeroDeSesion = tA.FechasTutorias.NumeroSesion,
                    IdTutorAcademico = tA.IdAcademico,
                    NombreTutorAcademico = ((Academicos)academicosDAO.ObtenerAcademicoPorIdAcademico(tA.IdAcademico)).NombreCompleto, 
                    Estado = tA.ReporteEntregado,
                    IdTutoriaAcademica = tA.IdTutoriaAcademica,
                    FechaTutoria = tA.FechasTutorias
                }) ; 
                if (reportes.Last().Estado)
                {
                    reportes.Last().EstadoEscrito = "Reporte Entregado";
                }
                else
                {
                    reportes.Last().EstadoEscrito = "Reporte no entregado";
                }
            }
            DataGrid_ReportePorTutorAcademico.ItemsSource = reportes;
        }

        private void ComboBox_PeriodosEscolares_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid_ReportePorTutorAcademico.ItemsSource = null;
            Label_NoTutorias.Visibility = Visibility.Collapsed;
            MostrarTutoriasAcademicas();
        }

        private void TextBox_Academico_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = TextBox_Academico.Text.ToLower();
            ICollectionView vista = CollectionViewSource.GetDefaultView(DataGrid_ReportePorTutorAcademico.ItemsSource);
            if (vista != null)
            {
                vista.Filter = reporteFiltrado =>
                {
                    ReportePorTutorAcademico reporte = reporteFiltrado as ReportePorTutorAcademico;
                    if (reporte != null)
                    {
                        return reporte.NombreTutorAcademico.ToLower().Contains(filtro);
                    }
                    return false;
                };
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_ReportePorTutorAcademico.SelectedItem != null)
            {
                if (((ReportePorTutorAcademico)DataGrid_ReportePorTutorAcademico.SelectedItem).Estado)
                {
                    this.NavigationService.Navigate(new ConsultarReportePorTutorAcademico((ReportePorTutorAcademico)DataGrid_ReportePorTutorAcademico.SelectedItem));
                }
                else
                {
                    MessageBox.Show("El tutor académico no ha entregado el reporte, por lo que no es posible consultarlo.", "Reporte no entregado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Seleccione el reporte por tutor académico que desee consultar.");
            }
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            if (SingletonUsuario.Instance.AcademicosRoles.First().idRol == (int)EnumRolesDeUsuario.Rol_Jefe_De_Carrrera)
            {
                this.NavigationService.Navigate(new PaginaPrincipalJefeDeCarrera());
            }
            else if (SingletonUsuario.Instance.AcademicosRoles.First().idRol == (int)EnumRolesDeUsuario.Rol_Coordinador)
            {
                this.NavigationService.Navigate(new PaginaPrincipalCoordinadorDeTutorias());
            }
            else
            {
                SingletonUsuario.Instance.BorrarSinglenton();
                this.NavigationService.Navigate(new InicioDeSesion());
            }
        }
    }
}
