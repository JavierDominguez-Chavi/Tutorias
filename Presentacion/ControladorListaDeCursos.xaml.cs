using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica de interacción para ControladorListaDeCursos.xaml
    /// </summary>
    public partial class ControladorListaDeCursos : Page
    {
        List<Dominio.ExperienciasEducativas_Academicos> experienciasEducativasObtenidas = new List<Dominio.ExperienciasEducativas_Academicos>();
        ExperienciasEducativas_AcademicosDAO experienciasEducativas_academicosDAO = new ExperienciasEducativas_AcademicosDAO();
        public ControladorListaDeCursos() 
        {
            InitializeComponent();
            cargarExperienciasEducativas();
        }

        
        public void cargarExperienciasEducativas()
        {
            experienciasEducativasObtenidas = experienciasEducativas_academicosDAO.ObtenerExperienciasEducativas();
            if (experienciasEducativasObtenidas != null)
            {
                DataGrid_ExperienciasEducativas.ItemsSource = experienciasEducativasObtenidas;
            }
            else
            {
                MessageBox.Show("No se pudo conectar con la base de datos. Por favor, inténtelo más tarde", "Error", MessageBoxButton.OK);
            }
        }
        private void filtroExperienciasEducativas(object sender, TextChangedEventArgs e)
        {
            string filtro = TextBox_Filtro.Text.ToLower();
            ICollectionView vista = CollectionViewSource.GetDefaultView(DataGrid_ExperienciasEducativas.ItemsSource);
            if (vista != null)
            {
                vista.Filter = experienciaEducativaFitrada =>
                {
                    ExperienciasEducativas_Academicos experienciaEducativa = experienciaEducativaFitrada as ExperienciasEducativas_Academicos;
                    if (experienciaEducativa != null)
                    {
                        return experienciaEducativa.ExperienciasEducativas.Nombre.ToLower().Contains(filtro);
                    }
                    return false;
                };
            }
        }
        private void Button_Cancelar_Click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new PaginaPrincipalAdministrador());
        }

        private void consultarCurso(object sender, SelectionChangedEventArgs e)
        {
            Dominio.ExperienciasEducativas_Academicos cursoSeleccionado = (Dominio.ExperienciasEducativas_Academicos)DataGrid_ExperienciasEducativas.SelectedItem;
            ControladorConsultarCurso consultarCurso = new ControladorConsultarCurso(cursoSeleccionado);
            consultarCurso.DataContext = cursoSeleccionado;
            consultarCurso.ShowDialog();
        }
    }
}
