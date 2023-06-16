using LogicaDelNegocio;
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
    /// Lógica de interacción para AgregarCurso.xaml
    /// </summary>
    public partial class AgregarCurso : Page
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
        List<Dominio.ProgramasEducativos> programasEducativos = new List<Dominio.ProgramasEducativos>();
        ProgramasEducativosDAO programaEducativoDAO = new ProgramasEducativosDAO();
        public AgregarCurso()
        {
            InitializeComponent();
            programasEducativos = programaEducativoDAO.ObtenerProgramasEducativos();
            for (int i = 0; i < programasEducativos.Count(); i++)
            {
                CBProgramasEducativos.Items.Add(programasEducativos.ElementAt(i).NombreProgramaEducativo);
            }
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
            String programaEducativoSeleccionado = CBProgramasEducativos.SelectedItem.ToString();
            experienciasEducativas_Academicos.IdProgramaEducativo = programasEducativos.FirstOrDefault(x => x.NombreProgramaEducativo == programaEducativoSeleccionado).IdProgramaEducativo;
            experienciasEducativas_Academicos.NRC = int.Parse(TBNRC.Text);
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
            int resultado = 500;
            bool status = true;
            if (experienciaEducativa.IdExperienciaEducativa <= 0) {
                MessageBox.Show("Seleccione una experiencia educativa");
                status = false;

            }



            if (CBProgramasEducativos.SelectedIndex == -1) {
                MessageBox.Show("Seleccione un programa educativo");
                status = false;
            }


            if (!int.TryParse(TBNRC.Text, out int nrc) || nrc <= 0 || TBNRC.Text.Length != 5)
            {
                MessageBox.Show("Ingrese un NRC válido, con solo números y con una longitud de 5 caracteres");
                status = false;
            }


            if (academico.IdAcademico <= 0) 
            {
                MessageBox.Show("Seleccione un academico");
                status = false;

            }
          

            if (status == true)
            {
                ConstruirCurso();
                
                if (resultado == 200)
                {
                    MessageBox.Show("Modificacion exitosa");
                    this.NavigationService.GoBack();
                }
                else
                {
                    if (resultado == 505)
                    {
                        MessageBox.Show("Error 505: Ya existe otro curso con el mismo NRC");
                    }
                    else
                    {
                        MessageBox.Show("Error 500: Error al ingresar al Guardar la información en la base de datos");

                    }
                };
            }

            

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void CBProgramasEducativos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
