using System.Collections.ObjectModel;

namespace Dominio
{
    public class Problematicas
    {
        public Problematicas()
        {
            this.Soluciones = new ObservableCollection<Soluciones>();
        }
        public Problematicas(AccesoADatos.Problematicas problematicas)
        {
            this.IdProblematica = problematicas.IdProblematica;
            this.Titulo = problematicas.Titulo;
            this.Descripcion = problematicas.Descripcion;
            this.IdTutoriaAcademica = problematicas.IdTutoriaAcademica;
            this.NRC = problematicas.NRC;
            this.Matricula = problematicas.Matricula;

            this.Estudiantes = new Estudiantes();
            this.ExperienciasEducativas_Academicos = new ExperienciasEducativas_Academicos();
            this.TutoriasAcademicas = new TutoriasAcademicas();
            this.Soluciones = new ObservableCollection<Soluciones>();
        }

        public int IdProblematica { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int IdTutoriaAcademica { get; set; }
        public int NRC { get; set; }
        public string Matricula { get; set; }

        public Estudiantes Estudiantes { get; set; }
        public ExperienciasEducativas_Academicos ExperienciasEducativas_Academicos { get; set; }
        public TutoriasAcademicas TutoriasAcademicas { get; set; }
        public ObservableCollection<Soluciones> Soluciones { get; set; }

        public AccesoADatos.Problematicas ConvertirProblematicaDominioEnProblematiicaAccesoADatos()
        {
            return new AccesoADatos.Problematicas()
            {
                Titulo = this.Titulo,
                Descripcion = this.Descripcion,
                IdTutoriaAcademica = this.IdTutoriaAcademica,
                NRC = this.NRC,
                Matricula = this.Matricula
            };
        }
    }
}
