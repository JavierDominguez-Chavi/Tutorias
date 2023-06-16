namespace Dominio
{
    public class Soluciones
    {
        public Soluciones() { }
        public Soluciones(AccesoADatos.Soluciones solucion) 
        {
            this.IdSolucion = solucion.IdSolucion;
            this.Fecha = solucion.Fecha;
            this.Descripcion = solucion.Descripcion;
            this.IdProblematica = solucion.IdProblematica;
            this.IdAcademico = solucion.IdAcademico;
            this.Academicos = new Academicos(solucion.Academicos);
            this.Problematicas = new Problematicas(solucion.Problematicas);
        }
        public int IdSolucion { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int IdProblematica { get; set; }
        public int IdAcademico { get; set; }

        public Academicos Academicos { get; set; }
        public Problematicas Problematicas { get; set; }
        public AccesoADatos.Soluciones SolucionesADatos()
        {
            return new AccesoADatos.Soluciones()
            {
                Fecha = this.Fecha,
                Descripcion = this.Descripcion,
                IdProblematica = this.IdProblematica,
                IdAcademico = this.IdAcademico,
            };
        }
    }
}
