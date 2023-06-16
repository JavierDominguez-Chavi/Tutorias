using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ControladorAsignarExperienciasEducativasAProfesor.xaml
    /// </summary>
    public partial class ControladorAsignarExperienciasEducativasAProfesor : Window
    {
        Academicos academicoAsignado = new Academicos();
        ExperienciasEducativas_AcademicosDAO experienciasEducativas_academicosDAO = new ExperienciasEducativas_AcademicosDAO();
        public List<Dominio.ExperienciasEducativas_Academicos> experienciasEducativasObtenidas = new List<Dominio.ExperienciasEducativas_Academicos>();
        ExperienciasEducativas_AcademicosDAO experienciasEducativas_AcademicosDAO = new ExperienciasEducativas_AcademicosDAO();
        public ControladorAsignarExperienciasEducativasAProfesor(Academicos academicoAAsignar)
        {
            InitializeComponent();
            academicoAsignado = academicoAAsignar;
            Label_ProfesorSeleccionado.Content = academicoAAsignar.NombreCompleto;
            cargarExperienciasEducativas();
        }
        public void cargarExperienciasEducativas()
        {
            experienciasEducativasObtenidas = experienciasEducativas_academicosDAO.ObtenerExperienciasEducativasSinAcademico();
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


        private void asignarExperienciaEducativaAProfesor(object sender, RoutedEventArgs e)
        {
            List<ExperienciasEducativas_Academicos> experienciasEducativasAAsignar = new List<ExperienciasEducativas_Academicos>();
            List<ExperienciasEducativas_Academicos> objetosSeleccionados = new List<ExperienciasEducativas_Academicos>();
            bool seleccionado = false;
            foreach (ExperienciasEducativas_Academicos item in DataGrid_ExperienciasEducativas.Items)
            {
                var row = DataGrid_ExperienciasEducativas.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var checkBox = DataGrid_ExperienciasEducativas.Columns[3].GetCellContent(row) as CheckBox;
                    if ( checkBox.IsChecked == true)
                    {
                        objetosSeleccionados.Add(item);
                        seleccionado = true;
                    }
                   
                }
            }
            if (!seleccionado)
            {
                MessageBox.Show("Debes seleccionar un valor de la tabla antes de continuar", "Error", MessageBoxButton.OK);
            }
            foreach(ExperienciasEducativas_Academicos experienciaEducativa in objetosSeleccionados)
            {
                ExperienciasEducativas_Academicos experienciaEducativaAcademico = new ExperienciasEducativas_Academicos();
                experienciaEducativaAcademico.IdAcademico = academicoAsignado.IdAcademico;
                experienciaEducativaAcademico.IdExperienciaEducativa = experienciaEducativa.IdExperienciaEducativa;
                experienciaEducativaAcademico.NRC = experienciaEducativa.NRC;
                experienciaEducativaAcademico.IdProgramaEducativo = experienciaEducativa.IdProgramaEducativo;
                experienciasEducativasAAsignar.Add(experienciaEducativaAcademico);
            }
            foreach(ExperienciasEducativas_Academicos experienciaAAsignar in experienciasEducativasAAsignar)
            {
                Boolean registrado = experienciasEducativas_AcademicosDAO.AsignarExperienciaEducativaAAcademico(experienciaAAsignar);
                if (registrado)
                {
                    MessageBox.Show("La información se a actualizado correctamente", "Notificación", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("No se pudo conectar con la base de datos. Por favor, inténtelo más tarde", "Error", MessageBoxButton.OK);
                }
            }
            cargarExperienciasEducativas();
        }

        private void Button_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea cancelar la asignación de experiencias educativas?", "Confirmación", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
                this.DataContext = null;
            }
            
        }
    }
}
