using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Dominio;
using LogicaDelNegocio;
using mensajes = Presentacion.Properties.Mensajes;

namespace Presentacion
{
    public partial class ListaProblematicasAcademicas : Page
    {
        public ListaProblematicasAcademicas()
        {
            InitializeComponent();
    
        }

        private void CargarInformacionPagina(object sender, RoutedEventArgs e)
        {
            ProblematicasDAO problematicasDAO = new ProblematicasDAO();
            ObservableCollection<Dominio.Problematicas> problematicas = problematicasDAO.ObtenerProblematicas();
            bool problematicasCompletas = true;
            if (problematicas.FirstOrDefault() != null)
            {
                foreach (Problematicas problematica in problematicas)
                {
                    if (problematica.ExperienciasEducativas_Academicos.ExperienciasEducativas == null ||
                        problematica.TutoriasAcademicas.FechasTutorias == null)
                    {
                        problematicasCompletas = false;
                    }
                }
                LlenarTablaProblematicas(problematicas,problematicasCompletas);
            }
            else
            {
                MessageBox.Show(mensajes.messageBoxText_Conexion, mensajes.messageBoxTitle_Conexion, 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LlenarTablaProblematicas(ObservableCollection<Dominio.Problematicas> problematicas, bool problematicasCompletas)
        {
            if (problematicasCompletas)
            {
                CollectionViewSource ViewSource_Problematicas =
                        ((CollectionViewSource)(this.FindResource("ViewSource_Problematicas")));
                ViewSource_Problematicas.Source = problematicas;
            }
            else 
            {
                MessageBox.Show(mensajes.messageBoxText_Conexion, mensajes.messageBoxTitle_Conexion,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FiltrarContenidoTablaProblematicas(object sender, TextChangedEventArgs e)
        {
            Filtrar(this.TextBox_Filtrar.Text);
        }

        private void Filtrar(String query)
        {
            ICollectionView objetos = ((CollectionViewSource)this.DataGrid_Problematicas.DataContext).View;
            var filtro = new Predicate<object>(objeto => ((Problematicas)objeto).Titulo.Contains(query) || 
                ((Problematicas)objeto).TutoriasAcademicas.FechasTutorias.FechaTutoria.ToString().Contains(query) ||
                ((Problematicas)objeto).ExperienciasEducativas_Academicos.ExperienciasEducativas.Nombre.Contains(query));
            objetos.Filter = filtro;
            DataGrid_Problematicas.ItemsSource = objetos;
        }

        private void ConsultarProblematica(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Problematicas.SelectedItem != null)
            {
                if (DataGrid_Problematicas.SelectedItem is Problematicas)
                {
                    Problematicas problematicaSeleccionada = (Problematicas)DataGrid_Problematicas.SelectedItem;

                    if (problematicaSeleccionada != null)
                    {
                        ConsultaProblematicaAcademica consultaProblematicaAcademica =
                            new ConsultaProblematicaAcademica(problematicaSeleccionada);
                        this.NavigationService.Navigate(consultaProblematicaAcademica);
                       
                    }
                }
                else
                {
                    MessageBox.Show(mensajes.messageBoxText_ProblematicaNoSeleccionada,
                        mensajes.messageBoxTitle_ProblematicaNoSeleccionada, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            
        }

        private void RegresarPaginaAnterior(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
