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
using AccesoADatos;
using Dominio;
using LogicaDelNegocio;
using mensajes = Presentacion.Properties.Mensajes;

namespace Presentacion
{
    public partial class ListadoAcademicos : Page
    {
        public ListadoAcademicos()
        {
            InitializeComponent();
        }

        private void CargarInformacionPagina(object sender, RoutedEventArgs e)
        {
            AcademicosRolesDAO academicosRolesDAO = new AcademicosRolesDAO();
            ObservableCollection<Dominio.Academicos> academicos = 
                new ObservableCollection<Dominio.Academicos> (academicosRolesDAO.ConsularAcademicosSinRol());
            if (academicos.FirstOrDefault() != null)
            {
                    CollectionViewSource ViewSource_Academicos =
                            ((CollectionViewSource)(this.FindResource("ViewSource_Academicos")));
                    ViewSource_Academicos.Source = academicos;
            }
            else
            {
                MessageBox.Show(mensajes.messageBoxText_AcademicosNoEncontrados, mensajes.messageBoxTitle_AcademicosNoEncontrados,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                this.NavigationService.GoBack();
            }
        }

        private void FiltrarContenidoTablaAcademicos(object sender, TextChangedEventArgs e)
        {
            Filtrar(this.TextBox_Filtrar.Text);
        }

        private void Filtrar(String query)
        {
            ICollectionView objetos = ((CollectionViewSource)this.DataGrid_Academicos.DataContext).View;
            var filtro = new Predicate<object>(objeto => ((Dominio.Academicos)objeto).NombreCompleto.Contains(query) ||
                ((Dominio.Academicos)objeto).CorreoPersonal.Contains(query) || 
                ((Dominio.Academicos)objeto).CorreoInstitucional.Contains(query));
            objetos.Filter = filtro;
            DataGrid_Academicos.ItemsSource = objetos;
        }

        private void ModidficarAcademico(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Academicos.SelectedItem != null)
            {
                if (DataGrid_Academicos.SelectedItem is Dominio.Academicos)
                {
                    Dominio.Academicos academicoSeleccionado = (Dominio.Academicos)DataGrid_Academicos.SelectedItem;

                    if (academicoSeleccionado != null)
                    {
                         ModificarAcademico modificarAcademico = new ModificarAcademico(academicoSeleccionado);
                         this.NavigationService.Navigate(modificarAcademico);

                    }
                }
                else
                {
                    MessageBox.Show(mensajes.messageBoxText_AcademicoNoSeleccinado, mensajes.messageBoxTitle_AcademicoNoSeleccionado, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }

        private void RegresarPaginaAnterior(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
