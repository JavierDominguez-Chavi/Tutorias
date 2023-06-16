using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Shapes;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ControladorListaDeEstudiantes.xaml
    /// </summary>
    public partial class ControladorListaDeEstudiantes : Page
    {
        public int idProgramaEducativo = SingletonUsuario.Instance.AcademicosRoles.First().ProgramasEducativos.IdProgramaEducativo;
        EstudiantesDAO estudiantesDAO = new EstudiantesDAO();
        public ObservableCollection<Dominio.Estudiantes> estudiantesObtenidos;
        DataSet dataSetEstudiantes = new DataSet();

        public ControladorListaDeEstudiantes()
        {
            InitializeComponent();
            estudiantesObtenidos = new ObservableCollection<Dominio.Estudiantes>(estudiantesDAO.ObtenerEstudiantes(idProgramaEducativo));
            if (estudiantesObtenidos != null)
            {
                var estudiantes = estudiantesObtenidos.Select(estudiantesEncontrado => new {
                    NombreCompleto = estudiantesEncontrado.Nombre + " " + estudiantesEncontrado.ApellidoPaterno + " " + estudiantesEncontrado.ApellidoMaterno,
                    Matricula = estudiantesEncontrado.Matricula
                });
                DataGrid_ListaEstudiantes.ItemsSource = estudiantesObtenidos;
            }
            else
            {
                MessageBox.Show("No se pudo conectar con la base de datos. Por favor, inténtelo más tarde", "Error", MessageBoxButton.OK);
            }
        }
        
        
        


        private void estudianteSeleccionado(object sender, SelectionChangedEventArgs e)
        { 
            if (DataGrid_ListaEstudiantes.SelectedItem != null)
            {
                Estudiantes estudianteAMostrar = (Estudiantes)DataGrid_ListaEstudiantes.SelectedItem;
                ControladorConsultarEstudiante consultarEstudiante = new ControladorConsultarEstudiante(estudianteAMostrar);
                consultarEstudiante.DataContext = estudianteAMostrar;
                consultarEstudiante.ShowDialog();
            }
            
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            if (SingletonUsuario.Instance.AcademicosRoles.First().idRol == 5610)
            {
                this.NavigationService.Navigate(new PaginaPrincipalCoordinadorDeTutorias());
            }
            if (SingletonUsuario.Instance.AcademicosRoles.First().idRol == 3807)
            {
                this.NavigationService.Navigate(new PaginaPrincipalJefeDeCarrera());
            }
        }
    }
}
