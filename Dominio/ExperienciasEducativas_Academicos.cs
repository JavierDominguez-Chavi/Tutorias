using System.Collections.ObjectModel;

namespace Dominio
{
    public class ExperienciasEducativas_Academicos
    {
        public ExperienciasEducativas_Academicos()
        {
            this.Problematicas = new ObservableCollection<Problematicas>();
            this.Estudiantes = new ObservableCollection<Estudiantes>();
        }

        public ExperienciasEducativas_Academicos
            (AccesoADatos.ExperienciasEducativas_Academicos experienciasEducativas_Academicos)
        {
            this.NRC = experienciasEducativas_Academicos.NRC;
            this.IdProgramaEducativo = experienciasEducativas_Academicos.IdProgramaEducativo;
            this.IdExperienciaEducativa= experienciasEducativas_Academicos.IdExperienciaEducativa;
            this.IdAcademico = experienciasEducativas_Academicos.IdAcademico;

            this.Academicos = new Academicos(experienciasEducativas_Academicos.Academicos);
            this.ExperienciasEducativas = 
                new ExperienciasEducativas(experienciasEducativas_Academicos.ExperienciasEducativas);
            this.ProgramasEducativos = new ProgramasEducativos(experienciasEducativas_Academicos.ProgramasEducativos);
            this.Problematicas = new ObservableCollection<Problematicas>();
            this.Estudiantes = new ObservableCollection<Estudiantes>();
        }

        public int NRC { get; set; }
        public int IdProgramaEducativo { get; set; }
        public int IdExperienciaEducativa { get; set; }
        public int? IdAcademico { get; set; }

        public Academicos Academicos { get; set; }
        public ExperienciasEducativas ExperienciasEducativas { get; set; }
        public ProgramasEducativos ProgramasEducativos { get; set; }
        public ObservableCollection<Problematicas> Problematicas { get; set; }
        public PeriodosEscolares PeriodosEscolares { get; set; }
        public ObservableCollection<Estudiantes> Estudiantes { get; set; }

        public AccesoADatos.ExperienciasEducativas_Academicos AAccesoADatos()
        {
            return new AccesoADatos.ExperienciasEducativas_Academicos ()
            {
                NRC = this.NRC,
                IdProgramaEducativo = this.IdProgramaEducativo,
                IdExperienciaEducativa = this.IdExperienciaEducativa,
                IdAcademico = this.IdAcademico,
            };
        }
    }
}
