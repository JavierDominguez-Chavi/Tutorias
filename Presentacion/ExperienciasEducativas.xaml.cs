using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Validation;
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

namespace Presentacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class ControladorExperienciasEducativas : Page
    {
        private Dominio.ExperienciasEducativas ods = new ExperienciasEducativas();
        private ExperienciasEducativasDAO experienciasEducativasDAO = new ExperienciasEducativasDAO();
        private ObservableCollection<ExperienciasEducativas> experienciasEducativas;
        private ExperienciasEducativas experienciaEnEdicion;

        public ControladorExperienciasEducativas()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Data.CollectionViewSource ViewSource_ExperienciasEducativas =
                    ((System.Windows.Data.CollectionViewSource)(this.FindResource("ViewSource_ExperienciasEducativas")));
                this.experienciasEducativas = experienciasEducativasDAO.ObtenerExperienciasEducativas();
                ViewSource_ExperienciasEducativas.Source = experienciasEducativas;
            }
            catch (ArgumentException AException)
            {
                Console.WriteLine(AException.Message);
            }
        }

        private void TextBox_Nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Popup_Error.IsOpen = false;
        }

        private void TextBox_Filtro_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtrar(this.TextBox_Filtro.Text);
        }

        private void Filtrar(String query)
        {
            ICollectionView objetos = ((CollectionViewSource)this.DataGrid_ExperienciasEducativas.DataContext).View;
            var filtro = new Predicate<object>(objeto => ((ExperienciasEducativas)objeto).Nombre.Contains(query));
            objetos.Filter = filtro;
            DataGrid_ExperienciasEducativas.ItemsSource = objetos;
        }

        private void Button_Registrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExperienciasEducativas nuevaExperienciaEducativa = new ExperienciasEducativas();
                nuevaExperienciaEducativa.Nombre = this.TextBox_Nombre.Text;
                nuevaExperienciaEducativa.NombreComodin = this.TextBox_Nombre.Text;
                Boolean registroExitoso =
                    experienciasEducativasDAO.RegistrarExperienciaEducativa(nuevaExperienciaEducativa);
                if (registroExitoso)
                {
                    this.experienciasEducativas.Add(nuevaExperienciaEducativa);
                    this.TextBox_Nombre.Clear();
                }
            }
            catch (DbEntityValidationException DBEVException)
            {
                this.Popup_Error.IsOpen = true;
                this.TextBlock_Error.Text = DBEVException.Message;
            }
            catch (ArgumentException AException) 
            {
                this.Popup_Error.IsOpen = true;
                this.TextBlock_Error.Text = AException.Message;
            }
        }

        private void ReservarExperienciaEducativa(object sender, RoutedEventArgs e)
        {
            this.experienciaEnEdicion = (sender as TextBox).Tag as ExperienciasEducativas;
        }

        private void ValidarExperienciaEducativa(object sender, RoutedEventArgs e)
        {
            try
            {
                this.experienciasEducativasDAO.ValidarNombreExperienciaEducativa(this.experienciaEnEdicion.NombreComodin);
                experienciaEnEdicion.AcceptChanges();
                this.experienciasEducativasDAO.ModificarExperienciaEducativa(this.experienciaEnEdicion);
            }
            catch (Exception exception) 
            when (exception is ArgumentException || exception is DbEntityValidationException)
            {
                MessageBox.Show(exception.Message);
                experienciaEnEdicion.RejectChanges();
            }

        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
