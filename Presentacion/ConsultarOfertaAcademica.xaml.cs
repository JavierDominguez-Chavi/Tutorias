using AccesoADatos;
using Dominio;
using GongSolutions.Wpf.DragDrop.Utilities;
using LogicaDelNegocio;
using Presentacion.Properties;
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
    /// Lógica de interacción para ConsultarOfertaAcademica.xaml
    /// </summary>



    public partial class ConsultarOfertaAcademica : Page
    {
        private ObservableCollection<Dominio.ExperienciasEducativas_Academicos> experienciasEducativas_Academicos =
            new ObservableCollection<Dominio.ExperienciasEducativas_Academicos>();
        public Dominio.ExperienciasEducativas_Academicos experienciasEducativas_AcademicosSeleccionado =
            new Dominio.ExperienciasEducativas_Academicos();
        public ConsultarOfertaAcademica()
        {
            InitializeComponent();
            PeriodosEscolaresDAO periodosEscolaresDAO = new PeriodosEscolaresDAO();
            ObservableCollection<Dominio.PeriodosEscolares> periodosEscolares = periodosEscolaresDAO.ObtenerPeriodosEscolaresPorProgramaEducativo(1);
            AgregarColumnasDataGrid();
            for (int i = 0; i < periodosEscolares.Count(); i++)
            {
                CBPeriodos.Items.Add(periodosEscolares.ElementAt(i).Nombre);
            }


        }

        public void AgregarColumnasDataGrid()
        {
            // Crear las columnas del DataGrid
            var columnas = new List<DataGridColumn>();
            columnas.Add(new DataGridTextColumn() { Header = "EE", Binding = new Binding("ExperienciasEducativas.Nombre") });
            columnas.Add(new DataGridTextColumn() { Header = "NRC", Binding = new Binding("NRC") });
            columnas.Add(new DataGridTextColumn() { Header = "Nombre Academico", Binding = new Binding("Academicos.Nombre ") });
            columnas.Add(new DataGridTextColumn() { Header = "Apellido paterno", Binding = new Binding("Academicos.ApellidoPaterno ") });
            columnas.Add(new DataGridTextColumn() { Header = "Apellido materno", Binding = new Binding("Academicos.ApellidoMaterno ") });
            // Agregar las columnas al DataGrid
            foreach (var columna in columnas)
            {
                DGOfertaAcademica.Columns.Add(columna);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ExperienciasEducativas_AcademicosDAO experienciasEducativas_AcademicosDAO = new ExperienciasEducativas_AcademicosDAO();

            experienciasEducativas_Academicos
                = experienciasEducativas_AcademicosDAO.obtenerExperieniciasEducativasPorPeriodo(CBPeriodos.SelectedItem.ToString());

            DGOfertaAcademica.ItemsSource = experienciasEducativas_Academicos;
        }

        public void MostrarOfertaEducativa()
        {
            Dominio.ExperienciasEducativas_Academicos ofertaEducativa = new Dominio.ExperienciasEducativas_Academicos();
            Dominio.Academicos academicos = ofertaEducativa.Academicos;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DGOfertaAcademica.SelectedItem != null && DGOfertaAcademica.SelectedItem is Dominio.ExperienciasEducativas_Academicos selectedItem && selectedItem != null)
            {
                experienciasEducativas_AcademicosSeleccionado = (Dominio.ExperienciasEducativas_Academicos)DGOfertaAcademica.SelectedItem;

                if (experienciasEducativas_AcademicosSeleccionado != null)
                {
                    this.NavigationService.Navigate(new ModificarOfertaAcademica(experienciasEducativas_AcademicosSeleccionado));
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
