using AccesoADatos;
using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Lógica de interacción para ModificarOfertaAcademica.xaml
    /// </summary>
    public partial class ModificarOfertaAcademica : Page
    {
        AcademicosDAO academicosDAO = new AcademicosDAO();
        ExperienciasEducativasDAO experienciasEducativasDAO = new ExperienciasEducativasDAO();
        ExperienciasEducativas_AcademicosDAO experienciasEducativas_AcademicosDAO = new ExperienciasEducativas_AcademicosDAO();
        ObservableCollection<AccesoADatos.ExperienciasEducativas> experienciasEducativas = new ObservableCollection<AccesoADatos.ExperienciasEducativas>();
        ObservableCollection<Dominio.Academicos> academicosObtenidos = new ObservableCollection<Dominio.Academicos>();
        Dominio.Academicos academico = new Dominio.Academicos();
        Dominio.ProgramasEducativos programaEducativo = new Dominio.ProgramasEducativos();
        Dominio.ExperienciasEducativas experienciaEducativa = new Dominio.ExperienciasEducativas();
        Dominio.ExperienciasEducativas_Academicos experienciasEducativas_Academicos = new Dominio.ExperienciasEducativas_Academicos();


        public ModificarOfertaAcademica(Dominio.ExperienciasEducativas_Academicos experienciasEducativas_Academicos)
        {
            InitializeComponent();
            academico.IdAcademico = experienciasEducativas_Academicos.Academicos.IdAcademico;
            LBNRC.Content = "" + experienciasEducativas_Academicos.NRC;
            LBProfesor.Content = experienciasEducativas_Academicos.Academicos.NombreCompleto;
            LBEE.Content = experienciasEducativas_Academicos.ExperienciasEducativas.Nombre;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            academico.CorreoInstitucional = TXBuscarAcademico.Text;
            academicosObtenidos = academicosDAO.BuscarAcademicoPorCorreoInstitucional(academico);
            LBAcademicos.ItemsSource = academicosObtenidos;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            experienciasEducativas = experienciasEducativasDAO.BuscarExperienciaEducativasPorNombre(TBBuscarExperienciaEducativa.Text);
            LBExperienciasAcademicas.Items.Clear();
            foreach (var experiencia in experienciasEducativas)
            {
                LBExperienciasAcademicas.Items.Add(experiencia.Nombre);
            }
        }

        private void ConstruirCurso()
        {
            
            experienciasEducativas_Academicos.IdExperienciaEducativa = experienciaEducativa.IdExperienciaEducativa;
            experienciasEducativas_Academicos.NRC = int.Parse(LBNRC.Content.ToString());
            experienciasEducativas_Academicos.IdAcademico = academico.IdAcademico;
        }

        private void LBAcademicos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBAcademicos.SelectedItem != null)
            {
                academico = (Dominio.Academicos)LBAcademicos.SelectedItem;
                LBProfesor.Content = academico.NombreCompleto;
            }
        }

        private void LBExperienciasAcademicas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBExperienciasAcademicas.SelectedItem != null)
            {
                for (int i = 0; i < experienciasEducativas.Count(); i++)
                {
                    if (experienciasEducativas.ElementAt(i).Nombre == LBExperienciasAcademicas.SelectedItem.ToString())
                    {
                        experienciaEducativa.Nombre = experienciasEducativas.ElementAt(i).Nombre;
                        experienciaEducativa.IdExperienciaEducativa = experienciasEducativas.ElementAt(i).IdExperienciaEducativa;
                        LBEE.Content = experienciaEducativa.Nombre;
                    }
                }


            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           ConstruirCurso();
           int resultado = experienciasEducativas_AcademicosDAO.ActualizarExperienciaEducativas_Academicos(experienciasEducativas_Academicos);
            if (resultado == 200)
            {
                MessageBox.Show("Modificacion exitosa");
                this.NavigationService.GoBack();
            }
            else {
                MessageBox.Show("Imposible realizar la modificación");
            };

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void TXBuscarAcademico_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
