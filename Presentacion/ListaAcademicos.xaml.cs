using LogicaDelNegocio;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para ListaAcademicos.xaml
    /// </summary>
    public partial class ListaAcademicos : Page
    {
        public List<Dominio.Academicos> academicos = new List<Dominio.Academicos>();
        private AcademicosDAO academicosDAO = new AcademicosDAO(); 
        public Dominio.Academicos academicoSeleccionado = new Dominio.Academicos();
        public ListaAcademicos()
        {
            InitializeComponent();
            AgregarColumnasDataGrid();
            cargarAcademicos();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DGAcademicos.SelectedItem != null && DGAcademicos.SelectedItem is Dominio.Academicos selectedItem && selectedItem != null)
            {
                academicoSeleccionado = (Dominio.Academicos)DGAcademicos.SelectedItem;
                if (academicoSeleccionado != null)
                {
                    this.NavigationService.Navigate(new ModificarRolesAcademicos(academicoSeleccionado));
                }
            }
        }

        public void cargarAcademicos() {
            academicos = academicosDAO.ObtenerAcademicos();
            DGAcademicos.ItemsSource = academicos; 
        }

        public void AgregarColumnasDataGrid()
        {
            // Crear las columnas del DataGrid
            var columnas = new List<DataGridColumn>();
            columnas.Add(new DataGridTextColumn() { Header = "Correo Institucional", Binding = new Binding("CorreoInstitucional") });
            columnas.Add(new DataGridTextColumn() { Header = "Nombre Academico", Binding = new Binding("Nombre ") });
            columnas.Add(new DataGridTextColumn() { Header = "Apellido paterno", Binding = new Binding("ApellidoPaterno ") });
            columnas.Add(new DataGridTextColumn() { Header = "Apellido materno", Binding = new Binding("ApellidoMaterno ") });
            // Agregar las columnas al DataGrid
            foreach (var columna in columnas)
            {
                DGAcademicos.Columns.Add(columna);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
