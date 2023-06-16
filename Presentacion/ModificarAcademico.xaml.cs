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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AccesoADatos;
using Dominio;
using LogicaDelNegocio;
using Presentacion.Properties;
using mensajes = Presentacion.Properties.Mensajes;
using MessageBox = System.Windows.MessageBox;

namespace Presentacion
{
    public partial class ModificarAcademico : Page
    {
        public Dominio.Academicos Academico { get; set; }
        private ObservableCollection<Dominio.ProgramasEducativos> programasEducativos;
        private List<Dominio.AcademicosRoles> informacionGeneralcademico;
        private bool academicoRegistrado = false;
        private int numeroProgramasEducativosNoAsignados = 0;
        private List<Dominio.ProgramasEducativos> programasEducativosModificados = new List<Dominio.ProgramasEducativos>();
        private List<Dominio.ProgramasEducativos> programasEducativosNoSeleccionados = new List<Dominio.ProgramasEducativos>();
        private Dominio.Academicos academicoSinCambios = new Dominio.Academicos();
        private bool activarConfirmacionSalir = false;


        public ModificarAcademico(Dominio.Academicos academicoModificar)
        {
            InitializeComponent();
            Academico = academicoModificar;
            this.DataContext = Academico;
            academicoSinCambios.Nombre = Academico.Nombre;
            academicoSinCambios.ApellidoPaterno = Academico.ApellidoPaterno;
            academicoSinCambios.ApellidoMaterno = Academico.ApellidoMaterno;
            academicoSinCambios.CorreoInstitucional = Academico.CorreoInstitucional;
            academicoSinCambios.CorreoPersonal = Academico.CorreoPersonal;

        }

        private void CargarInformacionPagina(object sender, RoutedEventArgs e)
        {
            ProgramasEducativosDAO programasEducativosDAO = new ProgramasEducativosDAO();
            AcademicosRolesDAO academicosRolesDAO = new AcademicosRolesDAO();
            programasEducativos =
                new ObservableCollection<Dominio.ProgramasEducativos>(programasEducativosDAO.ObtenerProgramasEducativos());
            informacionGeneralcademico = academicosRolesDAO.ConsultarInformacionAcademico(Academico.IdAcademico);
            if (programasEducativos.FirstOrDefault() != null && informacionGeneralcademico.FirstOrDefault() != null)
            {
                MarcarProgramasEducativosAcademico();
                CollectionViewSource ViewSource_ProgramasEducativos =
                    ((CollectionViewSource)(this.FindResource("ViewSource_ProgramasEducativos")));
                ViewSource_ProgramasEducativos.Source = programasEducativos;
            }
            else
            {
                MensajesHelper.FaltaDeConexion();
                this.NavigationService.GoBack();
            }
        }

        private void MarcarProgramasEducativosAcademico()
        {
            foreach (Dominio.ProgramasEducativos programaEducativo in programasEducativos)
            {
                foreach (Dominio.AcademicosRoles academicoRol in informacionGeneralcademico)
                {
                    if (academicoRol.idProgramaEducativo == programaEducativo.IdProgramaEducativo)
                    {
                        programaEducativo.ProgramaEducativoSeleccionado = true;
                    }
                }
            }
        }

        private void HabilitarCamposTexto(object sender, RoutedEventArgs e)
        {
            this.TextBox_NombreAcademico.IsReadOnly = false;
            this.TextBox_ApellidoMaternoAcademico.IsReadOnly = false;
            this.TextBox_ApellidoPaternoAcademico.IsReadOnly = false;
            this.TextBox_CorreoPersonalAcademico.IsReadOnly = false;
            this.TextBox_CorreoInstitucionalAcademico.IsReadOnly = false;
            activarConfirmacionSalir = true;
        }

        private void DeshabilitarCamposTexto(object sender, RoutedEventArgs e)
        {
            this.TextBox_NombreAcademico.IsReadOnly = true;
            this.TextBox_ApellidoMaternoAcademico.IsReadOnly = true;
            this.TextBox_ApellidoPaternoAcademico.IsReadOnly = true;
            this.TextBox_CorreoPersonalAcademico.IsReadOnly = true;
            this.TextBox_CorreoInstitucionalAcademico.IsReadOnly = true;
        }

        private void GuardarCambiosAcademico(object sender, RoutedEventArgs e)
        {
            LimpiarMensajesInvalidos();
            Boolean camposNombreValidos = true;
            Boolean camposCorreoValidos = true;
            Boolean programasEducativosSeleccionados = true;
            Boolean limiteDeCaracteres = true;
            camposNombreValidos = ValidarCamposAlfabeticos();
            camposCorreoValidos = ValidarCamposCorreo();
            programasEducativosSeleccionados = ValidarProgramasEducativosSeleccionados();
            limiteDeCaracteres = ValidarLongitudDeCampos();
            if (camposNombreValidos && camposCorreoValidos && programasEducativosSeleccionados && limiteDeCaracteres)
            {
                programasEducativosModificados.Clear();
                foreach (Dominio.ProgramasEducativos programasEducativos in DataGrid_ProgramasEducativos.ItemsSource)
                {
                    if (programasEducativos.ProgramaEducativoSeleccionado)
                    {
                        programasEducativosModificados.Add(programasEducativos);
                    }
                    else
                    {
                        programasEducativosNoSeleccionados.Add(programasEducativos);
                    }
                }
                ValidarAcademicoComoNuevo();
            }
        }

        private void LimpiarMensajesInvalidos()
        {
            this.TextBlock_NombreInvalido.Text = "";
            this.TexBlock_ApellidoPaternoInvalido.Text = "";
            this.TextBlock_ApellidoMaternoInvalido.Text = "";
            this.TextBlock_CorreoPersonalInvalido.Text = "";
            this.TextBlock_CorreoInstitucionalInvalido.Text = "";
        }

        private Boolean ValidarCamposAlfabeticos()
        {
            Boolean camposAlfabeticos = true;
            if (!Validador.ValidarCadenaAlfabetica(this.TextBox_NombreAcademico.Text))
            {
                this.TextBlock_NombreInvalido.Text = mensajes.message_ContenidoAlfabeticoRequerido;
                camposAlfabeticos = false;
            }
            if (!Validador.ValidarCadenaAlfabetica(this.TextBox_ApellidoPaternoAcademico.Text))
            {
                this.TexBlock_ApellidoPaternoInvalido.Text = mensajes.message_ContenidoAlfabeticoRequerido;
                camposAlfabeticos = false;
            }
            if (!Validador.ValidarCadenaAlfabetica(this.TextBox_ApellidoMaternoAcademico.Text))
            {
                this.TextBlock_ApellidoMaternoInvalido.Text = mensajes.message_ContenidoAlfabeticoRequerido;
                camposAlfabeticos = false;
            }
            return camposAlfabeticos;
        }

        private Boolean ValidarCamposCorreo()
        {
            Boolean correoValido = true;
            if (!Validador.ValidarCorreoElectronico(this.TextBox_CorreoInstitucionalAcademico.Text))
            {
                this.TextBlock_CorreoInstitucionalInvalido.Text = mensajes.message_ContenidoCorreoInstitucionalInvalido;
                correoValido = false;
            }
            if (!Validador.ValidarCorreoElectronico(this.TextBox_CorreoPersonalAcademico.Text))
            {
                this.TextBlock_CorreoPersonalInvalido.Text = mensajes.message_ContenidoCorreoPersonalInvalido;
                correoValido = false;
            }
            return correoValido;
        }


        private Boolean ValidarProgramasEducativosSeleccionados()
        {
            bool programasEducativosSeleccionados = true;
            int numeroDeProgramasEducativosSeleccionados = 0;
            foreach (Dominio.ProgramasEducativos programasEducativos in DataGrid_ProgramasEducativos.ItemsSource)
            {
                if (programasEducativos.ProgramaEducativoSeleccionado)
                {
                    numeroDeProgramasEducativosSeleccionados++;
                }
            }
            programasEducativosSeleccionados =
                ValidarNumeroProgramasEducativosSeleccionados(numeroDeProgramasEducativosSeleccionados, programasEducativosSeleccionados);

            return programasEducativosSeleccionados;
        }


        private Boolean ValidarNumeroProgramasEducativosSeleccionados(int numeroDeProgramasEducativosSeleccionados,
                                                                bool programaEducativosSeleccionados)
        {
            if (numeroDeProgramasEducativosSeleccionados == 0)
            {
                MessageBox.Show(mensajes.messageBoxText_ProgramasEducativosNoSeleccionados, mensajes.messageBoxTitle_ProgramasEducativosNoSeleccionados,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                programaEducativosSeleccionados = false;
            }
            return programaEducativosSeleccionados;
        }

        private Boolean ValidarLongitudDeCampos()
        {
            Boolean longitudValida = true;
            if (!Validador.ValidarLongitudCaracteresNombreYApellidos(this.TextBox_ApellidoMaternoAcademico.Text) ||
                !Validador.ValidarLongitudCaracteresNombreYApellidos(this.TextBox_ApellidoPaternoAcademico.Text) ||
                !Validador.ValidarLongitudCaracteresNombreYApellidos(this.TextBox_NombreAcademico.Text))
            {
                MessageBox.Show(mensajes.messageBoxText_CamposNombreYApellidosExcedenLimite, mensajes.messageBoxTitle_LimiteDeCampos,
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                longitudValida = false;
            }
            if (!Validador.ValidarLongitudCaracteresCorreoElectronicoPersonal(this.TextBox_CorreoPersonalAcademico.Text))
            {
                MessageBox.Show(mensajes.messageBoxText_CampoCorreoPersonalExcedeLimite, mensajes.messageBoxTitle_LimiteDeCampos,
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                longitudValida = false;
            }
            if (!Validador.ValidarLongitudCaracteresCorreoElectronicoInstitucional(this.TextBox_CorreoInstitucionalAcademico.Text))
            {
                MessageBox.Show(mensajes.messageBoxText_CampoCorreoInstitucionalExcedeLimite, mensajes.messageBoxTitle_LimiteDeCampos,
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                longitudValida = false;
            }
            return longitudValida;
        }

        private void ValidarAcademicoComoNuevo()
        {
            AcademicosDAO academicosDAO = new AcademicosDAO();
            if (Academico.NombreCompleto != academicoSinCambios.NombreCompleto || Academico.CorreoInstitucional != academicoSinCambios.CorreoInstitucional
                || Academico.CorreoPersonal != academicoSinCambios.CorreoPersonal ||
                programasEducativosModificados.Count() != informacionGeneralcademico.Count())
            {
                if (programasEducativosModificados.Count() != informacionGeneralcademico.Count())
                {
                    IdentificarCambiosEnProgramasEducativos();
                }
                if (Academico.NombreCompleto != academicoSinCambios.NombreCompleto || Academico.CorreoInstitucional != academicoSinCambios.CorreoInstitucional
                    || Academico.CorreoPersonal != academicoSinCambios.CorreoPersonal)
                {
                    RegistrarNuevosDatosAcademico();
                }
                if (academicoRegistrado)
                {
                    MensajesHelper.OperacionExitosa();
                    this.NavigationService.GoBack();
                }
            }
            else
            {
                MessageBox.Show(mensajes.messageBoxText_SinCambios, mensajes.messageBoxTitle_SinCambios, MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
        }

        private void RegistrarNuevosDatosAcademico()
        {
            AcademicosDAO academicosDAO = new AcademicosDAO();
            AcademicosUsuariosDAO academicosUsuariosDAO = new AcademicosUsuariosDAO();
            if (academicosDAO.ActualizarAcademico(Academico))
            {
                academicoRegistrado = true;
            }
            else
            {
                MensajesHelper.FaltaDeConexion();
            }

        }

        private void IdentificarCambiosEnProgramasEducativos()
        {
            if (informacionGeneralcademico.Count() < programasEducativosModificados.Count())
            {
                RegistrarAcademicoUsuarioConRol();
            }
            if (informacionGeneralcademico.Count() > programasEducativosModificados.Count())
            {
                EliminarAcademicoUsuarioRol();
            }
        }

        private void RegistrarAcademicoUsuarioConRol()
        {
            bool academicoUsuarioRegistrado = false;
            string programasEducativosNoAsignados = "";
            int rolesAsignados = 0;
            
                foreach (Dominio.ProgramasEducativos programasEducativos in programasEducativosModificados)
                {
                    if (!informacionGeneralcademico.Any(rol => rol.idProgramaEducativo == programasEducativos.IdProgramaEducativo))
                    {
                        AcademicosUsuariosDAO academicoUsuarioDAO = new AcademicosUsuariosDAO();
                        Dominio.AcademicosUsuarios academicoUsuario = new Dominio.AcademicosUsuarios();
                        AcademicosRolesDAO academicosRolesDAO = new AcademicosRolesDAO();
                        academicoUsuario.NombreUsuario = Academico.NombreCompleto + programasEducativos.IdProgramaEducativo;
                        academicoUsuario.Contrasena =
                            EncriptadorContrasenas.Encriptar(Academico.NombreCompleto + programasEducativos.NombreProgramaEducativo);
                        academicoUsuarioRegistrado = academicoUsuarioDAO.RegistrarAcademicoUsuario(academicoUsuario);
                        programasEducativosNoAsignados =
                            ValidarAcademicoUsuarioRegistrado(academicoUsuarioRegistrado, programasEducativos, programasEducativosNoAsignados);
                        academicoUsuario.IdUsuario = academicoUsuarioDAO.ObtenerIdAcademicoUsuarioPorNombreYContrasena(academicoUsuario);
                        Dominio.AcademicosRoles academicosRoles = new Dominio.AcademicosRoles();
                        academicosRoles.idAcademico = Academico.IdAcademico;
                        academicosRoles.idRol = (int)EnumRolesDeUsuario.Rol_Sin_Rol;
                        academicosRoles.idProgramaEducativo = programasEducativos.IdProgramaEducativo;
                        academicosRoles.idUsuario = academicoUsuario.IdUsuario;
                        if (academicosRolesDAO.RegistrarAcademicoRol(academicosRoles))
                        {
                            rolesAsignados++;
                        }
                    }
                
            }
            ValidarProgramasEducativosAsignados(rolesAsignados, programasEducativosNoAsignados);
        }

        private string ValidarAcademicoUsuarioRegistrado(bool academicoUsuarioRegistrado, Dominio.ProgramasEducativos programasEducativos,
                                                         string programasEducativosNoAsignados)
        {
            if (!academicoUsuarioRegistrado)
            {
                programasEducativosNoAsignados = programasEducativosNoAsignados + ", " + programasEducativos;
                numeroProgramasEducativosNoAsignados++;
            }
            return programasEducativosNoAsignados;
        }

        private void ValidarProgramasEducativosAsignados(int rolesAsignados, string programasEducativosNoAsignados)
        {
            if (numeroProgramasEducativosNoAsignados != 0)
            {
                MessageBox.Show(mensajes.messageBoxText_ProgramasEducativosNoAsignados + programasEducativosNoAsignados,
                mensajes.messageBoxTitle_Conexion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ValidarRolesAsignados(rolesAsignados);
            }
        }

        private void ValidarRolesAsignados(int numeroDeRolesAsignados)
        {
            if (numeroDeRolesAsignados == 0)
            {
                MensajesHelper.FaltaDeConexion();
            }
            else
            {
                academicoRegistrado = true;
            }
        }

        private void EliminarAcademicoUsuarioRol()
        {
            AcademicosRolesDAO academicosRolesDAO = new AcademicosRolesDAO();
            AcademicosUsuariosDAO academicosUsuariosDAO = new AcademicosUsuariosDAO();
            foreach (Dominio.AcademicosRoles academicoRol in informacionGeneralcademico)
            {
                foreach (Dominio.ProgramasEducativos programaEducativo in programasEducativosNoSeleccionados)
                {
                    if (academicoRol.idProgramaEducativo == programaEducativo.IdProgramaEducativo)
                    {
                        academicosRolesDAO.EliminarRolAcademicoPorProgramaEducativo(academicoRol);
                        academicosUsuariosDAO.EliminarUsuariosAcademico(academicoRol.idUsuario);
                        academicoRegistrado = true;
                    }
                }
            }
        }
        private void RegresarPaginaAnterior(object sender, RoutedEventArgs e)
        {
            if (activarConfirmacionSalir == true)
            {
                MessageBoxResult resultadoMessageBox =
                MessageBox.Show(mensajes.messageBoxText_CancelarOperacion, mensajes.messageBoxTitle_CancelarOperacion,
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultadoMessageBox == MessageBoxResult.Yes)
                {
                    this.NavigationService.GoBack();
                }
            }
            else
            {
                this.NavigationService.GoBack();
            }
        }
    }
}
