using AccesoADatos;
using Dominio;
using LogicaDelNegocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
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
    /// Lógica de interacción para ModificarRolesAcademicos.xaml
    /// </summary>
    /// 

    public partial class ModificarRolesAcademicos : Page
    {
        public Dominio.Academicos academico = new Dominio.Academicos();
        ObservableCollection<Dominio.Roles> roles = new ObservableCollection<Dominio.Roles>();
        ObservableCollection<int> rolesAEliminar = new ObservableCollection<int>();
        ObservableCollection<int> rolesAAsignar = new ObservableCollection<int>();
        ObservableCollection<Dominio.ProgramasEducativos> programasEducativos = new ObservableCollection<Dominio.ProgramasEducativos>();
        AcademicosRolesDAO academicosRolesDAO = new AcademicosRolesDAO();
        ObservableCollection<Dominio.AcademicosUsuarios> usuarios = new ObservableCollection<Dominio.AcademicosUsuarios>();
        AcademicosUsuariosDAO academicosUsuariosDAO = new AcademicosUsuariosDAO();

        private int IdUsuario;
        private int IdProgramaEducativo = -1;

        public ModificarRolesAcademicos(Dominio.Academicos academicos)
        {
            InitializeComponent();
            LbNombreAcademico.Content = academicos.NombreCompleto;
            LbCorreoInstitucionalAcademico.Content = academicos.CorreoInstitucional;
            LbCorreoPersonalAcademico.Content = academicos.CorreoPersonal;
            
            academico.IdAcademico = academicos.IdAcademico;
            academico.CorreoInstitucional = academicos.CorreoInstitucional;

            programasEducativos = academicosRolesDAO.ObtenerProgramasEducativosDeAcademicos(academico.CorreoInstitucional);
            for (int i = 0; i < programasEducativos.Count(); i++)
            {
                CBProgramasEducativos.Items.Add(programasEducativos.ElementAt(i).NombreProgramaEducativo);
            }

            
        }



        public void CargarRoles(String correoInstitucional)
        {

            roles = academicosRolesDAO.ObtenerRolesDeAcademicos(correoInstitucional,IdUsuario);
            foreach (Dominio.Roles roles in roles)
            {
                if (roles.IdRol == 1039)
                    RBTutor.IsChecked = true;

                if (roles.IdRol == 2938)
                    RBAdministrador.IsChecked = true;

                if (roles.IdRol == 3807)
                    RBJefeDeCarrera.IsChecked = true;

                if (roles.IdRol == 5610)
                    RBCoordinador.IsChecked = true;

                if (roles.IdRol == 8792)
                    RBProfesor.IsChecked = true;
            }
        }

        private void RBJefeDeCarrera_Checked(object sender, RoutedEventArgs e)
        {
            if (RBJefeDeCarrera.IsChecked == true)
            {
                rolesAAsignar.Add(3807);
                rolesAEliminar.Remove(3807);
            }
            else
            {
                rolesAAsignar.Remove(3807);
                rolesAEliminar.Add(3807);
            }

        }

        private void RBCoordinador_Checked(object sender, RoutedEventArgs e)
        {
            if (RBCoordinador.IsChecked == true)
            {
                rolesAAsignar.Add(5610);
                rolesAEliminar.Remove(5610);
            }
            else
            {
                rolesAAsignar.Remove(5610);
                rolesAEliminar.Add(5610);
            }

        }

        private void RBTutor_Checked(object sender, RoutedEventArgs e)
        {
            if (RBTutor.IsChecked == true)
            {
                rolesAAsignar.Add(1039);
                rolesAEliminar.Remove(1039);
            }
            else
            {
                rolesAAsignar.Remove(1039);
                rolesAEliminar.Add(1039);
            }

        }

        private void RBProfesor_Checked(object sender, RoutedEventArgs e)
        {
            if (RBProfesor.IsChecked == true)
            {
                rolesAAsignar.Add(8792);
                rolesAEliminar.Remove(8792);
            }
            else
            {
                rolesAAsignar.Remove(8792);
                rolesAEliminar.Add(8792);
            }

        }

        private void RBAdministrador_Checked(object sender, RoutedEventArgs e)
        {
            if (RBAdministrador.IsChecked == true)
            {
                rolesAAsignar.Add(2938);
                rolesAEliminar.Remove(2938);
            }
            else
            {
                rolesAAsignar.Remove(2938);
                rolesAEliminar.Add(2938);
            }

        }

        public void CargarRolesAModificar()
        {

            ObservableCollection<int> rolesAnteriores = new ObservableCollection<int>();
            for (int i = 0; i < roles.Count(); i++)
            {
                rolesAnteriores.Add(roles.ElementAt(i).IdRol);
            }

            var itemsToRemove = rolesAEliminar.Except(rolesAnteriores).ToList();
            foreach (var item in itemsToRemove)
            {
                rolesAEliminar.Remove(item);
            }

            foreach (int item in rolesAAsignar.ToList())
            {
                if (rolesAnteriores.Contains(item))
                {
                    rolesAAsignar.Remove(item);
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool resultadoAsignacion = false;
            bool resultadoEliminacion = false;


            CargarRolesAModificar();


            if (rolesAAsignar.Count > 0)
            {
                if (IdProgramaEducativo > 0)
                {


                    resultadoAsignacion = academicosRolesDAO.ModificarRolesDeAcademicos(academico.IdAcademico, IdProgramaEducativo, IdUsuario, rolesAAsignar);
                    if (resultadoAsignacion)
                    {
                        rolesAAsignar = null;
                        MessageBox.Show("Modifiicación exitosa");
                        

                    }
                    else
                    {
                        MessageBox.Show("Modifiicación Fallida");
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione todos los campos");
                }



                if (rolesAEliminar.Count() > 0)
                {
                    if (IdProgramaEducativo > 0)
                    {
                        resultadoEliminacion = academicosRolesDAO.EliminarRolesDeAcademicos(academico.IdAcademico, IdProgramaEducativo, IdUsuario, rolesAEliminar);

                        if (resultadoEliminacion)
                        {
                            MessageBox.Show("Eliminacion exitosa");
                            
                        }
                        else
                        {
                            MessageBox.Show("No se realizaron eliminaciones");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seleccione todos los campos");
                    }

                }

                if (resultadoAsignacion || resultadoEliminacion ) {
                    this.NavigationService.GoBack();
                }

            }



        }


        private void CBProgramasEducativos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String programaEducativoSeleccionado = CBProgramasEducativos.SelectedItem.ToString();
            var elementoBuscado = programasEducativos.FirstOrDefault(x => x.NombreProgramaEducativo == programaEducativoSeleccionado);
            IdProgramaEducativo = elementoBuscado.IdProgramaEducativo;
            CBUsuarios.Items.Clear();
            usuarios.Clear();
            usuarios = academicosUsuariosDAO.ObtenerUsuariosAcademico(academico.IdAcademico, IdProgramaEducativo);

            if (usuarios != null)
            {
                for (int i = 0; i < usuarios.Count(); i++)
                {
                    CBUsuarios.Items.Add(usuarios.ElementAt(i).NombreUsuario);
                }
            }
            else 
            {
                MessageBox.Show("El academico no tiene ningún usuario");
                this.NavigationService.GoBack();
            }
          
        }

        private void Usuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBUsuarios.SelectedItem != null)
            {
                String usuarioSeleccionado = CBUsuarios.SelectedItem.ToString();
                IdUsuario = usuarios.FirstOrDefault(x => x.NombreUsuario == usuarioSeleccionado).IdUsuario;
                limpiarCampos();
                CargarRoles(academico.CorreoInstitucional);
            }
        }

        private void limpiarCampos() 
        {
            RBAdministrador.IsChecked = false;
            RBCoordinador.IsChecked = false;
            RBProfesor.IsChecked = false;
            RBJefeDeCarrera.IsChecked = false;
            RBTutor.IsChecked = false;  
        }
    }


}
