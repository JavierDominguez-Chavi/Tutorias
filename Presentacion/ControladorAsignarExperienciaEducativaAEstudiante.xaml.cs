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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AccesoADatos;
using LogicaDelNegocio;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ControladorAsignarExperienciaEducativaAEstudiante.xaml
    /// </summary>
    public partial class ControladorAsignarExperienciaEducativaAEstudiante : Page
    {
        private ObservableCollection<ExperienciasEducativas_Academicos> listaExperiencias = new ObservableCollection<ExperienciasEducativas_Academicos>();

        private ObservableCollection<Estudiantes> listaEstudiantes = new ObservableCollection<Estudiantes>();

        private ObservableCollection<int> listaExperienciasAsignar = new ObservableCollection<int>();

        private Estudiantes estudianteSeleccionado = new Estudiantes();
        public ControladorAsignarExperienciaEducativaAEstudiante()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListBEstudiantesResultados.Items.Clear();
            String busqueda = TBBusquedaEstudiante.Text;
            EstudiantesDAO buscarEstdudiante = new EstudiantesDAO();
            listaEstudiantes = buscarEstdudiante.ObtenerEstudiantes(busqueda);
            if (listaEstudiantes.Count() > 0) {
                for (int i = 0; i < listaEstudiantes.Count(); i++) {
                    ListBEstudiantesResultados.Items.Add(listaEstudiantes.ElementAt(i).Nombre + " " + listaEstudiantes.ElementAt(i).ApellidoPaterno + " " + listaEstudiantes.ElementAt(i).ApellidoMaterno + " " + listaEstudiantes.ElementAt(i).Matricula);
                }
            }

        }

        private void ObtenerExperienciasEducativas(object sender, RoutedEventArgs e) {
            ListBExperienciasResultados.Items.Clear();
            String busqueda = TBBusquedaExperienciaEducativa.Text;
            ExperienciasEducativasDAO busquedaEsxperiencias = new ExperienciasEducativasDAO();
            listaExperiencias = busquedaEsxperiencias.BuscarExperienciaEducativas(busqueda);
;            if (listaExperiencias.Count() > 0)
            {
                for (int i = 0; i < listaExperiencias.Count(); i++)
                {
                    ListBExperienciasResultados.Items.Add(listaExperiencias.ElementAt(i).NRC + " " + listaExperiencias.ElementAt(i).ExperienciasEducativas.Nombre);
                }
            }
        }

        private void TBBusquedaEstudiante_TextChanged(object sender, TextChangedEventArgs e)
        {
            TBBusquedaEstudiante.Clear();
            LbEstudiante.Content = null;
        }

        private void TBBusquedaExperienciaEducativa_TextChanged(object sender, TextChangedEventArgs e)
        {
            TBBusquedaExperienciaEducativa.Clear();
            
        }

        private void ListBEstudiantesResultados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBEstudiantesResultados.SelectedIndex >= 0)
            {
                LbEstudiante.Content = listaEstudiantes.ElementAt(ListBEstudiantesResultados.SelectedIndex).Nombre + " " + listaEstudiantes.ElementAt(ListBEstudiantesResultados.SelectedIndex).ApellidoPaterno + " " + listaEstudiantes.ElementAt(ListBEstudiantesResultados.SelectedIndex).ApellidoMaterno + " " + listaEstudiantes.ElementAt(ListBEstudiantesResultados.SelectedIndex).Matricula;
                LVExperienciasDeEstudiantes.Items.Clear();
                estudianteSeleccionado.Matricula = listaEstudiantes.ElementAt(ListBEstudiantesResultados.SelectedIndex).Matricula;

                String matricula = listaEstudiantes.ElementAt(ListBEstudiantesResultados.SelectedIndex).Matricula;
                EstudiantesDAO buscarExperienciasDeEstudiantes = new EstudiantesDAO();
                ObservableCollection<ExperienciasEducativas> experienciasDeEstudiante = buscarExperienciasDeEstudiantes.GetExperienciasEducativasDeEstudiante(matricula);

                if (experienciasDeEstudiante.Count() > 0)
                {
                        for (int i = 0; i < experienciasDeEstudiante.Count(); i++)
                    {
                        LVExperienciasDeEstudiantes.Items.Add(experienciasDeEstudiante.ElementAt(i).Nombre);
                    }
                }
                else {
                    MessageBox.Show("No se encontro ninguna experiencia educativa asignada");

                }
            }

        }

        private void ListBExperienciasResultados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBExperienciasResultados.SelectedIndex >= 0) 
            {
                LVExperienciasAAsignar.Items.Add(listaExperiencias.ElementAt(ListBExperienciasResultados.SelectedIndex).ExperienciasEducativas.Nombre);
                listaExperienciasAsignar.Add(listaExperiencias.ElementAt(ListBExperienciasResultados.SelectedIndex).NRC);
                ListBExperienciasResultados.SelectedIndex = -1;
            }
        }

        private void LVExperienciasAAsignar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(LVExperienciasAAsignar.SelectedIndex >= 0)
            {
                int numExperiencia = LVExperienciasAAsignar.SelectedIndex;
                LVExperienciasAAsignar.Items.RemoveAt(numExperiencia);
                listaExperienciasAsignar.RemoveAt(numExperiencia);
            }
            else { 
            
            }
        }

        private void BTNGuardarAsignacion(object sender, RoutedEventArgs e)
        {
            EstudiantesDAO asignacionAEstudiante = new EstudiantesDAO();
            bool resultado = asignacionAEstudiante.AsignarExperienciasEducativasAEstudiante(estudianteSeleccionado.Matricula, listaExperienciasAsignar);

            if (resultado) {
                MessageBox.Show("Asignación correcta");
            } else {
                MessageBox.Show("No se realizo la asignación correcta");

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
