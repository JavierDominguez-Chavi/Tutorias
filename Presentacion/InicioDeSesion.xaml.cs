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
    /// Lógica de interacción para InicioDeSesion.xaml
    /// </summary>
    public partial class InicioDeSesion : Page
    {
        AcademicosUsuariosDAO academicosUsuariosDAO = new AcademicosUsuariosDAO();
        private ObservableCollection<Dominio.AcademicosRoles> roles = new ObservableCollection<Dominio.AcademicosRoles>();
        private List<int> numeroRoles = new List<int>();
        private List<String> NombreRoles = new List<String>();
        Dominio.SingletonUsuario usuario = Dominio.SingletonUsuario.Instance;
        public InicioDeSesion()
        {
            InitializeComponent();
            CBRoles.Visibility = Visibility.Collapsed;
            LBSeleccionarRol.Visibility = Visibility.Collapsed;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBRoles.SelectedItem.ToString() == "Tutor") {
                ContruirUsuario(1039);
                
                MessageBox.Show("Permiso para entrar como Tutor");
                this.NavigationService.Navigate(new PaginaPrincipalTutorAcademico());
            }
            if (CBRoles.SelectedItem.ToString() == "Administrador")
            {
                ContruirUsuario(2938);
                MessageBox.Show("Permiso para entrar como Administrador");
                this.NavigationService.Navigate(new PaginaPrincipalAdministrador());
            }
            if (CBRoles.SelectedItem.ToString() == "Jefe de carrera")
            {
                ContruirUsuario(3807);
                MessageBox.Show("Permiso para entrar como Jefe de carrera");
                this.NavigationService.Navigate(new PaginaPrincipalJefeDeCarrera());
            }
            if (CBRoles.SelectedItem.ToString() == "Coordinador")
            {
                ContruirUsuario(5610);
                MessageBox.Show("Permiso para entrar como Coordinador");
                this.NavigationService.Navigate(new PaginaPrincipalCoordinadorDeTutorias());
            }
        }
        
        private void ContruirUsuario(int numRol){
            usuario.NombreUsuario = TBUsuario.Text;
            usuario.AcademicosRoles.Add(new Dominio.AcademicosRoles());
            usuario.AcademicosRoles.First().idUsuario = roles.First().idUsuario;
            usuario.AcademicosRoles.First().idAcademico = roles.First().idAcademico;
            usuario.AcademicosRoles.First().ProgramasEducativos = CargarProgramasEducativos(roles.First().idProgramaEducativo);
            usuario.AcademicosRoles.First().idRol = numRol;
        }

        private Dominio.ProgramasEducativos CargarProgramasEducativos(int idProgramaEducativo)
        {
            ProgramasEducativosDAO programasEducativosDAO = new ProgramasEducativosDAO();
            Dominio.ProgramasEducativos programaEducativo = 
                programasEducativosDAO.ObtenerProgramaEducativoPorIdProgramaEducativo(idProgramaEducativo);
            return programaEducativo;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            
            roles = academicosUsuariosDAO.ObtenerRolesDeUsuario(TBUsuario.Text, PWContrasenia.Password);

            if ( roles != null){
                foreach (var rol in roles) 
                {
                    if (rol.idRol == 1039)
                        NombreRoles.Add("Tutor");
                    if (rol.idRol == 2938)
                        NombreRoles.Add("Administrador");

                    if (rol.idRol == 3807)
                        NombreRoles.Add("Jefe de carrera");

                    if (rol.idRol == 5610)
                        NombreRoles.Add("Coordinador");
                }

                CBRoles.ItemsSource = NombreRoles;
                CBRoles.Visibility = Visibility.Visible;
                LBSeleccionarRol.Visibility = Visibility.Visible;  
            }
            else {
                MessageBox.Show("La contraseña o el usuario es incorrecto");
                CBRoles.Visibility = Visibility.Collapsed;
                LBSeleccionarRol.Visibility= Visibility.Collapsed;  
            }
            
         
        }
    }
}
