using Dominio;
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
    /// Lógica de interacción para ControladorRegistroTutorAcademico.xaml
    /// </summary>

    public partial class ControladorRegistroTutorAcademico : Page
    {
        private static int ID_PROGRAMA_EDUCATIVO = SingletonUsuario.Instance.IdProgramaEducativo;
        private static AcademicosRolesDAO academicosRolesDAO;
        private static AcademicosUsuariosDAO academicosUsuariosDAO;
        private static int ID_USUARIO_INVALIDO = -1;
        private static int REGISTRO_EXITOSO = 1;
        private static int ERROR_BASE_DE_DATOS = 2;
        private static int USUARIO_DUPLICADO = 3;

        public ControladorRegistroTutorAcademico()
        {
            InitializeComponent();
            academicosRolesDAO = new AcademicosRolesDAO();
            academicosUsuariosDAO = new AcademicosUsuariosDAO();
            List<AcademicosRoles> academicosNoTutorAcademico = ConsultarAcademicosPorProgramaEducativo();
            MostrarAcademicos(academicosNoTutorAcademico);

        }

        private void MostrarAcademicos(List<AcademicosRoles> academicosNoTutorAcademico)
        {
            foreach (AcademicosRoles role in academicosNoTutorAcademico)
            {
                ComboBox_Academicos.Items.Add(role.Academicos);
            }
        }
        private List<AcademicosRoles> ConsultarAcademicosPorProgramaEducativo()
        {
            List<AcademicosRoles> academicosRoles = academicosRolesDAO.ObtenerAcademicosPorProgramaEducativo(ID_PROGRAMA_EDUCATIVO);
            List<AcademicosRoles> academicosNoTutorAcademico = FiltrarNoTutorAcademico(academicosRoles);
            return academicosNoTutorAcademico;
            
        }
        private List<AcademicosRoles>  FiltrarNoTutorAcademico(List<Dominio.AcademicosRoles> academicosRoles)
        {
            List<AcademicosRoles> academicosNoTutorAcademico = new List<AcademicosRoles>();
            foreach (AcademicosRoles academicoRol in academicosRoles)
            {
                if (academicoRol.idRol == (int)EnumRolesDeUsuario.Rol_Sin_Rol)
                {
                    academicosNoTutorAcademico.Add(academicoRol);
                }
            }
            return academicosNoTutorAcademico;
        }
        private Boolean ValidarCampos()
        {
            return !(ComboBox_Academicos.SelectedIndex == -1 || String.IsNullOrWhiteSpace(TextBox_NombreDeUsuario.Text) || String.IsNullOrWhiteSpace(TextBox_Contraseña.Password));
        }
        
        private int RegistrarTutorAcademico()
        {
            int idAcademico = ((Academicos)ComboBox_Academicos.SelectedItem).IdAcademico;
            int idUsuario = RegistrarUsuario();
            int estadoRegistro = 0;
            if (idUsuario > ID_USUARIO_INVALIDO)
            {
                AcademicosRoles academicoRoles = new AcademicosRoles();
                academicoRoles.idProgramaEducativo = ID_PROGRAMA_EDUCATIVO;
                academicoRoles.idAcademico = idAcademico;
                academicoRoles.idUsuario = idUsuario;
                academicoRoles.idRol = (int)EnumRolesDeUsuario.Rol_Tutor_Academico;
                estadoRegistro = (academicosRolesDAO.RegistrarAcademicoRol(academicoRoles) && EliminarRolSinRol(academicoRoles)) ?  REGISTRO_EXITOSO : ERROR_BASE_DE_DATOS;
            }
            else
            {
                estadoRegistro = USUARIO_DUPLICADO;
            }
            return estadoRegistro;
        }

        private int RegistrarUsuario()
        {
            int idUsuario = -1;
            AcademicosUsuarios academicosUsuarios = new AcademicosUsuarios();
            academicosUsuarios.NombreUsuario = TextBox_NombreDeUsuario.Text;
            academicosUsuarios.Contrasena = EncriptadorContrasenas.Encriptar(TextBox_Contraseña.Password);
            if (academicosUsuariosDAO.ObtenerIdAcademicoUsuarioPorNombreYContrasena(academicosUsuarios) != ID_USUARIO_INVALIDO)
            {
                MessageBox.Show("El usuario o contraseña ingresadas ya se encuentran ocupados", "Usuario duplicado");
            }
            else if (academicosUsuariosDAO.RegistrarAcademicoUsuario(academicosUsuarios))
            {
                idUsuario = academicosUsuariosDAO.ObtenerIdAcademicoUsuarioPorNombreYContrasena(academicosUsuarios);
            }
            return idUsuario;
        }
        private bool EliminarRolSinRol(AcademicosRoles academicoRolesAEliminar)
        {
            bool eliminacionExitosa = false;
            academicoRolesAEliminar.idRol = (int)EnumRolesDeUsuario.Rol_Sin_Rol;
            eliminacionExitosa = academicosRolesDAO.EliminarAcademicoRol(academicoRolesAEliminar);
            return eliminacionExitosa;
        }
        private void Button_Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
            {
                MessageBox.Show("No puede dejar ningún campo vacío. Por favor, verifique que todos los campos estén llenos.", "Campos vacíos");
            }
            else 
            {
                int estatusRegistro = RegistrarTutorAcademico();
                if (estatusRegistro == REGISTRO_EXITOSO)
                {
                    MessageBox.Show("La información se registró correctamente en el sistema", "Información guardada", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCampos();
                }
                else if (estatusRegistro == ERROR_BASE_DE_DATOS)
                {
                    MessageBox.Show("No se pudo conectar con la base de datos, por favor inténtelo más tarde", "Error al conectar con la base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void LimpiarCampos()
        {
            ComboBox_Academicos.Items.RemoveAt(ComboBox_Academicos.SelectedIndex);
            ComboBox_Academicos.SelectedIndex = -1;
            TextBox_NombreDeUsuario.Text = "";
            TextBox_Contraseña.Password = "";
        }

        private void Button_Regresar_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
