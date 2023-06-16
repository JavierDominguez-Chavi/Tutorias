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
    public partial class ListadoTutoresAcademicos : Page
    {
        public ListadoTutoresAcademicos()
        {
            InitializeComponent();
        }

        private void CargarInformacionPagina(object sender, RoutedEventArgs e)
        {
            AcademicosRolesDAO academicosRolesDAO = new AcademicosRolesDAO();
            ObservableCollection<Dominio.AcademicosRoles> academicosTutores =
                new ObservableCollection<Dominio.AcademicosRoles>(academicosRolesDAO.ConsularTutoresAcademicosPorProgramaEducativo(
                                                                  SingletonUsuario.Instance.AcademicosRoles.First().ProgramasEducativos.IdProgramaEducativo));
            if (academicosTutores.FirstOrDefault() != null)
            {
                CollectionViewSource ViewSource_TutoresAcademicos =
                        ((CollectionViewSource)(this.FindResource("ViewSource_TutoresAcademicos")));
                ViewSource_TutoresAcademicos.Source = academicosTutores;
            }
            else
            {
                MessageBox.Show(mensajes.messageBoxText_Conexion, mensajes.messageBoxTitle_Conexion,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FiltrarContenidoTablaTutoresAcademicos(object sender, TextChangedEventArgs e)
        {
            Filtrar(this.TextBox_Filtrar.Text);
        }

        private void Filtrar(String query)
        {
            ICollectionView objetos = ((CollectionViewSource)this.DataGrid_TutoresAcademicos.DataContext).View;
            var filtro = new Predicate<object>(objeto => ((Dominio.AcademicosRoles)objeto).Academicos.Nombre.Contains(query) ||
                ((Dominio.AcademicosRoles)objeto).Academicos.CorreoPersonal.Contains(query) ||
                ((Dominio.AcademicosRoles)objeto).Academicos.CorreoInstitucional.Contains(query));
            objetos.Filter = filtro;
            DataGrid_TutoresAcademicos.ItemsSource = objetos;
        }

        private void ConsultarDetallesTutorAcademico(object sender, RoutedEventArgs e)
        {
            if (DataGrid_TutoresAcademicos.SelectedItem != null)
            {
                if (DataGrid_TutoresAcademicos.SelectedItem is Dominio.AcademicosRoles)
                {
                    Dominio.AcademicosRoles academicoRolSeleccionado =  (Dominio.AcademicosRoles)DataGrid_TutoresAcademicos.SelectedItem;

                    if (academicoRolSeleccionado != null)
                    {
                        DetallesTutorAcademico detallesTutorAcademico = new DetallesTutorAcademico(academicoRolSeleccionado);
                        this.NavigationService.Navigate(detallesTutorAcademico);

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
