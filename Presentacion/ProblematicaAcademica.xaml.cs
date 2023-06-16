using AccesoADatos;
using Dominio;
using LogicaDelNegocio;
using Presentacion.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using mensajes = Presentacion.Properties.Mensajes;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ProblematicaAcademica.xaml
    /// </summary>
    public partial class ProblematicaAcademica : Page
    {
        AcademicosDAO academicosDAO = new AcademicosDAO();
        EstudiantesDAO estudiantesDAO = new EstudiantesDAO();
        ProblematicasDAO problematicasDAO = new ProblematicasDAO();
        ObservableCollection<Dominio.Academicos> academicos = new ObservableCollection<Dominio.Academicos>();
        List<Dominio.Estudiantes> estudiantes = new List<Dominio.Estudiantes>();
        Dominio.Estudiantes estudiante = new Dominio.Estudiantes();
        private List<Dominio.Estudiantes> estudiantesFiltrados = new List<Dominio.Estudiantes>();
        private ObservableCollection<Dominio.Academicos> academicosFiltrados = new ObservableCollection<Dominio.Academicos>();
        Dominio.Problematicas problematica = new Dominio.Problematicas();
        Dominio.Problematicas problematicaActualizar = new Dominio.Problematicas();
        Dominio.Academicos academico = new Dominio.Academicos();
        //Atributos para el CU "Llenra Reporte De Tutoria"
        public Boolean modificarProblematica = true;
        ObservableCollection<Dominio.Problematicas> problematicas = new ObservableCollection<Dominio.Problematicas>();
        Dominio.Academicos academicosProbematica =new Dominio.Academicos();
        
        //Este constructor es para cuando el Caso de uso Extiende de "Llenar Reporte De Tutoría"
        public ProblematicaAcademica(List<Dominio.Estudiantes> estudiantesTutor)
        {
            InitializeComponent();
            modificarProblematica = false;
            AgregarColumnasTablaEstudiantes();
            estudiantes = estudiantesTutor;
            MostrarEstudiantes();


        }

        private void MostrarEstudiantes()
        {
            DGEstudiantes.ItemsSource = estudiantes;
        }

        //Este constructor es para cuando el Caso de uso modifica alguna prblematica
        public ProblematicaAcademica(Dominio.Problematicas problematicaAcademica)
        {

            InitializeComponent();
            AgregarColumnasTablaEstudiantes();
            estudiante.Matricula = problematicaAcademica.Matricula;
            estudiante.IdProgramaEducativo = problematicaAcademica.Estudiantes.IdProgramaEducativo;
            problematica = problematicaAcademica;
            academico = problematicaAcademica.ExperienciasEducativas_Academicos.Academicos;
            CargarEstudiantes();
            TBCriterioDeFiltroEstudiantes.Text = estudiante.Matricula;
            AplicarFiltro(estudiante.Matricula);
            AplicarFiltroAcademicos(academico.NombreCompleto);
            CargarAcademicos();
            cargarProblematicaAcademica();
        }


        public void cargarProblematicaAcademica()
        {
            TBTituloProblematica.Text = problematica.Titulo;
            TBDescripcionProblematica.Text = problematica.Descripcion;

        }

        public void AgregarColumnasTablaEstudiantes()
        {
            var columnas = new List<DataGridColumn>();
            columnas.Add(new DataGridTextColumn() { Header = "Matricula", Binding = new System.Windows.Data.Binding("Matricula") });
            columnas.Add(new DataGridTextColumn() { Header = "Estudiante", Binding = new System.Windows.Data.Binding("Nombre") });
            columnas.Add(new DataGridTextColumn() { Header = "Apellido Paterno", Binding = new System.Windows.Data.Binding("ApellidoPaterno") });
            columnas.Add(new DataGridTextColumn() { Header = "Apellido Materno", Binding = new System.Windows.Data.Binding("ApellidoMaterno") });
            columnas.Add(new DataGridTextColumn() { Header = "Semestre", Binding = new System.Windows.Data.Binding("SemestreQueCursa") });
            // Agregar las columnas al DataGrid
            foreach (var columna in columnas)
            {
                DGEstudiantes.Columns.Add(columna);
            }

        }


        private void CargarEstudiantes()
        {
            estudiantes = estudiantesDAO.ObtenerEstudiantes(estudiante.IdProgramaEducativo);
            DGEstudiantes.ItemsSource = estudiantes;
        }


        private void CargarAcademicos()
        {
            academicos = academicosDAO.ObtenerAcademicosDeEstudiante(estudiante);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Nombre", typeof(string));
            dataTable.Columns.Add("ApellidoPaterno", typeof(string));
            dataTable.Columns.Add("ApellidoMaterno", typeof(string));
            dataTable.Columns.Add("NRC", typeof(int));
            dataTable.Columns.Add("NombreExperiencia", typeof(string));

            foreach (var academico in academicos)
            {
                var row = dataTable.NewRow();
                row["Nombre"] = academico.Nombre;
                row["ApellidoPaterno"] = academico.ApellidoPaterno;
                row["ApellidoMaterno"] = academico.ApellidoMaterno;
                row["NRC"] = academico.ExperienciasEducativas_Academicos.FirstOrDefault()?.NRC;
                row["NombreExperiencia"] = academico.ExperienciasEducativas_Academicos.FirstOrDefault()?.ExperienciasEducativas.Nombre;
                dataTable.Rows.Add(row);
            }

            DGAcademicos.ItemsSource = dataTable.DefaultView;
        }


        private void AplicarFiltro(string criterioBusqueda)
        {
            if (string.IsNullOrWhiteSpace(criterioBusqueda))
            {
                estudiantesFiltrados = estudiantes;
            }
            else
            {
                estudiantesFiltrados = new List<Dominio.Estudiantes>(estudiantes.Where(e =>
                    e.Matricula.Contains(criterioBusqueda) ||
                    e.Nombre.Contains(criterioBusqueda) ||
                    e.ApellidoPaterno.Contains(criterioBusqueda) ||
                    e.ApellidoMaterno.Contains(criterioBusqueda)));
            }
            ActualizarDataGridEstudiantes(estudiantesFiltrados);
        }
        private void AplicarFiltroAcademicos(string criterioBusqueda)
        {
            if (string.IsNullOrWhiteSpace(criterioBusqueda))
            {
                academicosFiltrados = academicos;
            }
            else
            {
                academicosFiltrados = new ObservableCollection<Dominio.Academicos>(academicos.Where(a =>
                    a.NombreCompleto.Contains(criterioBusqueda) ||
                    a.Nombre.Contains(criterioBusqueda) ||
                    a.ApellidoPaterno.Contains(criterioBusqueda) ||
                    a.ApellidoMaterno.Contains(criterioBusqueda)));
            }
            ActualizarDataGridEstudiantes(estudiantesFiltrados);
        }

        private void ActualizarDataGridEstudiantes(List<Dominio.Estudiantes> estudiante)
        {
            DGEstudiantes.ItemsSource = estudiante;
        }


        private void ConstruirProblematica()
        {
            if (modificarProblematica)
            {
                problematicaActualizar = problematica;
                problematicaActualizar.Descripcion = TBDescripcionProblematica.Text;
                problematicaActualizar.Matricula = estudiante.Matricula;
                problematicaActualizar.Titulo = TBTituloProblematica.Text;
            }
            else 
            {
                ModificarProblematicasAcademicasReporte();
            }
        }

        private void ModificarProblematicasAcademicasReporte()
        {
            Dominio.Estudiantes estudianteProblematica = (Dominio.Estudiantes)DGEstudiantes.SelectedItem;
            Dominio.Problematicas problematicaGuardar = new Dominio.Problematicas();
            problematicaGuardar.Titulo = TBTituloProblematica.Text;
            problematicaGuardar.Descripcion = TBDescripcionProblematica.Text;
            problematicaGuardar.NRC = academicosProbematica.ExperienciasEducativas_Academicos.FirstOrDefault().NRC;
            problematicaGuardar.Matricula = estudianteProblematica.Matricula;
            problematicas.Add(problematicaGuardar);
            LlenarReporteDeTutoriaAcademica.Problematicas = problematicas;
            MensajesHelper.OperacionExitosa();
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            TBTituloProblematica.Text = "";
            TBDescripcionProblematica.Text = "";
            DGAcademicos.ItemsSource = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (modificarProblematica)
            {
                this.NavigationService.GoBack();
            }
            else
            {
               System.Windows.MessageBoxResult resultadoMessageBox =
               System.Windows.MessageBox.Show(mensajes.messageBoxText_CancelarOperacion, mensajes.messageBoxTitle_CancelarOperacion,
               MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultadoMessageBox == MessageBoxResult.Yes)
                {
                    this.NavigationService.GoBack();
                }
            }
        }
        private void ButtonGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidadorDeCamposVacios() == true)
            {

                ConstruirProblematica();
                if (modificarProblematica)
                {
                    bool resultado = problematicasDAO.ModificarProblematica(problematicaActualizar);
                    if (resultado == true)
                    {
                        System.Windows.MessageBox.Show("Guardado exitoso");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Error al intentar modificar la problematica academica");

                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No se pueden dejar campos vacíos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool ValidadorDeCamposVacios()
        {
            if (modificarProblematica)
            {
                if (string.IsNullOrWhiteSpace(TBDescripcionProblematica.Text) || string.IsNullOrWhiteSpace(TBTituloProblematica.Text))
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(TBDescripcionProblematica.Text) || string.IsNullOrWhiteSpace(TBTituloProblematica.Text) || 
                    DGAcademicos.SelectedItem == null)
                {
                    return false;
                }
                return true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AplicarFiltro(TBCriterioDeFiltroEstudiantes.Text);
        }

        private void DGEstudiantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (modificarProblematica)
            {
                var selectedEstudiante = DGEstudiantes.SelectedItem as Dominio.Estudiantes;

                if (selectedEstudiante != null && selectedEstudiante != estudiante)
                {
                    estudiante = selectedEstudiante;
                }
            }
            else
            {
                CargarColumnasTablaAcademicos();
            }
        }

        private void DGAcademicos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (modificarProblematica)
            {
                var selectedAcademico = DGAcademicos.SelectedItem as Dominio.Academicos;

                if (selectedAcademico != null && selectedAcademico != academico)
                {
                    academico = selectedAcademico;
                }
            }
            else
            {
                if (DGAcademicos.SelectedItem is DataRowView selectedRow)
                {
                    DataRow dataRow = selectedRow.Row;
                    int NRC = Convert.ToInt32(dataRow["NRC"]);
                    ObservableCollection<Dominio.ExperienciasEducativas_Academicos> experienciasEducativas = 
                        new ObservableCollection<Dominio.ExperienciasEducativas_Academicos>();
                    Dominio.ExperienciasEducativas_Academicos experienciaEducativa = new Dominio.ExperienciasEducativas_Academicos();
                    experienciaEducativa.NRC = NRC;
                    experienciasEducativas.Add(experienciaEducativa);
                    academicosProbematica.ExperienciasEducativas_Academicos = experienciasEducativas;
                    
                }
            }
        }

        private void CargarColumnasTablaAcademicos()
        {
            academicos = academicosDAO.ObtenerAcademicosDeEstudiante((Dominio.Estudiantes)this.DGEstudiantes.SelectedItem);
            if (academicos.FirstOrDefault() != null)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Nombre", typeof(string));
                dataTable.Columns.Add("ApellidoPaterno", typeof(string));
                dataTable.Columns.Add("ApellidoMaterno", typeof(string));
                dataTable.Columns.Add("NRC", typeof(int)).ReadOnly = true;
                dataTable.Columns.Add("NombreExperiencia", typeof(string));

                foreach (var academico in academicos)
                {
                    var row = dataTable.NewRow();
                    row["Nombre"] = academico.Nombre;
                    row["ApellidoPaterno"] = academico.ApellidoPaterno;
                    row["ApellidoMaterno"] = academico.ApellidoMaterno;
                    row["NRC"] = academico.ExperienciasEducativas_Academicos.FirstOrDefault()?.NRC;
                    row["NombreExperiencia"] = academico.ExperienciasEducativas_Academicos.FirstOrDefault()?.ExperienciasEducativas.Nombre;
                    dataTable.Rows.Add(row);
                }

                DGAcademicos.ItemsSource = dataTable.DefaultView;
            }
            else 
            {
                MensajesHelper.FaltaDeConexion();
            }
        }

    }
}

