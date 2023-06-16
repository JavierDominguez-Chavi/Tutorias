using Dominio;
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
    /// Lógica de interacción para ControladorConsultarCurso.xaml
    /// </summary>
    public partial class ControladorConsultarCurso : Window
    {
        public ControladorConsultarCurso(ExperienciasEducativas_Academicos cursoConsultado)
        {
            InitializeComponent();
            try
            {
                Label_Nombre.Content = cursoConsultado.ExperienciasEducativas.Nombre;
                if (cursoConsultado.IdAcademico != null)
                {
                    Label_AcademicoObtenido.Content = cursoConsultado.Academicos.NombreCompleto.ToString();
                }
                else
                {
                    Label_AcademicoObtenido.Content = "Este curso no tiene asignado ninún académico aún";
                }
                Label_NRCObtenido.Content = cursoConsultado.NRC;
                Label_ProgramaEducativoObtenido.Content = cursoConsultado.ProgramasEducativos.NombreProgramaEducativo;
            }
            catch
            {
                MessageBox.Show("No se pudo conectar con la base de datos. Por favor, inténtelo más tarde", "Error", MessageBoxButton.OK);
            }
            
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.DataContext = null;
        }
    }
}
