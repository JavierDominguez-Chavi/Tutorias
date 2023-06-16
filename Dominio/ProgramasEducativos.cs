using System.Collections.ObjectModel;

namespace Dominio
{
    public class ProgramasEducativos
    {
        public ProgramasEducativos()
        {
            this.Estudiantes = new ObservableCollection<Estudiantes>();
            this.ExperienciasEducativas_Academicos = new ObservableCollection<ExperienciasEducativas_Academicos>();
            this.AcademicosRoles = new ObservableCollection<AcademicosRoles>();
        }

        public ProgramasEducativos(AccesoADatos.ProgramasEducativos programasEducativos)
        {
            this.IdProgramaEducativo = programasEducativos.IdProgramaEducativo;
            this.NombreProgramaEducativo = programasEducativos.NombreProgramaEducativo;

            this.Estudiantes = new ObservableCollection<Estudiantes>();
            this.ExperienciasEducativas_Academicos = new ObservableCollection<ExperienciasEducativas_Academicos>();
            this.AcademicosRoles = new ObservableCollection<AcademicosRoles>();
        }

        public int IdProgramaEducativo { get; set; }
        public string NombreProgramaEducativo { get; set; }
        public bool ProgramaEducativoSeleccionado { get; set; }

        public ObservableCollection<Estudiantes> Estudiantes { get; set; }
        public ObservableCollection<ExperienciasEducativas_Academicos> ExperienciasEducativas_Academicos { get; set; }
        public ObservableCollection<AcademicosRoles> AcademicosRoles { get; set; }
    }
}
