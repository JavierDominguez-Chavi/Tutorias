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
using System.Windows.Shapes;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ControladorConsultarAcademico.xaml
    /// </summary>
    public partial class ControladorConsultarAcademico : Window
    {
        List<Dominio.AcademicosRoles> programasEducativos = new List<Dominio.AcademicosRoles>();
        AcademicosRolesDAO academicosRolesDAO = new AcademicosRolesDAO();
        public ControladorConsultarAcademico(Dominio.Academicos academicoAMostrar)
        {
            InitializeComponent();
            Label_Nombre.Content = academicoAMostrar.NombreCompleto;
            Label_CorreoPersonal.Content = academicoAMostrar.CorreoPersonal;
            Label_CorreoInstitucional.Content = academicoAMostrar.CorreoInstitucional;
            programasEducativos = academicosRolesDAO.ConsultarInformacionAcademico(academicoAMostrar.IdAcademico);
            if (programasEducativos.Count == 0 ) 
            {
                Label Label_sinProgramas = new Label();
                Label_sinProgramas.HorizontalAlignment = HorizontalAlignment.Left;
                Label_sinProgramas.Margin = new Thickness(442, 230, 0, 0);
                Label_sinProgramas.VerticalAlignment = VerticalAlignment.Top;
                Label_sinProgramas.Content = "No tiene programas educativos registrados";
                Grid_ConsultarAcademico.Children.Add(Label_sinProgramas);
            }
            else
            {
                DataGrid DataGrid_ProgramasEducativos = new DataGrid();
                DataGrid_ProgramasEducativos.HorizontalAlignment = HorizontalAlignment.Left;
                DataGrid_ProgramasEducativos.VerticalAlignment = VerticalAlignment.Top;
                DataGrid_ProgramasEducativos.Margin = new Thickness(442, 230, 0, 0);
                DataGridTextColumn columnaNombre = new DataGridTextColumn();
                columnaNombre.Header = "Programas Educativos";
                columnaNombre.Binding = new Binding("ProgramasEducativos.NombreProgramaEducativo"); 
                DataGrid_ProgramasEducativos.Columns.Add(columnaNombre);
                DataGrid_ProgramasEducativos.AutoGenerateColumns = false;
                DataGrid_ProgramasEducativos.ItemsSource = programasEducativos;
                Grid_ConsultarAcademico.Children.Add(DataGrid_ProgramasEducativos);
            }
        }

        

        private void ModificarDatosAcademico(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Regresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.DataContext = null;
        }
    }
}
