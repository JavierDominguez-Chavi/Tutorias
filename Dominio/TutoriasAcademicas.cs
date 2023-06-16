using System.Collections.ObjectModel;

namespace Dominio
{
    public class TutoriasAcademicas
    {
        public TutoriasAcademicas()
        {
            this.Horarios = new ObservableCollection<Horarios>();
            this.Problematicas = new ObservableCollection<Problematicas>();
        }        
        public TutoriasAcademicas(AccesoADatos.TutoriasAcademicas tutoriaAcademica)
        {
            this.IdTutoriaAcademica = tutoriaAcademica.IdTutoriaAcademica;
            this.ComentarioGeneral= tutoriaAcademica.ComentarioGeneral;
            this.ReporteEntregado = tutoriaAcademica.ReporteEntregado;
            this.IdAcademico = tutoriaAcademica.IdAcademico;
            this.IdFechaTuria = tutoriaAcademica.IdFechaTuria;
            this.FechasTutorias = new FechasTutorias(tutoriaAcademica.FechasTutorias);
            this.Academicos = new Academicos(tutoriaAcademica.Academicos);
            this.Horarios = new ObservableCollection<Horarios>();
            this.Problematicas = new ObservableCollection<Problematicas>();
        }

        public int IdTutoriaAcademica { get; set; }
        public string ComentarioGeneral { get; set; }
        public bool ReporteEntregado { get; set; }
        public int IdAcademico { get; set; }
        public int IdFechaTuria { get; set; }

        public Academicos Academicos { get; set; }
        public FechasTutorias FechasTutorias { get; set; } = new FechasTutorias();
        public  ObservableCollection<Horarios> Horarios { get; set; }
        public  ObservableCollection<Problematicas> Problematicas { get; set; }
        public AccesoADatos.TutoriasAcademicas AAccesoADatos()
        {
            return new AccesoADatos.TutoriasAcademicas()
            {
                IdTutoriaAcademica = this.IdTutoriaAcademica,
                ComentarioGeneral = this.ComentarioGeneral,
                ReporteEntregado = this.ReporteEntregado,
                IdAcademico = this.IdAcademico,
                IdFechaTuria = this.IdFechaTuria
            };
        }
    }
}
