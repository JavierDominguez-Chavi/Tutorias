using AccesoADatos;

namespace Dominio
{
    public class AcademicosRoles
    {
        public AcademicosRoles() { }
        public AcademicosRoles(AccesoADatos.AcademicosRoles academicosRoles) 
        { 
            this.idAcademico = academicosRoles.idAcademico;
            this.idRol = academicosRoles.idRol;
            this.idProgramaEducativo = academicosRoles.idProgramaEducativo;
            this.idUsuario = academicosRoles.idUsuario;
            this.Academicos = new Academicos(academicosRoles.Academicos);
            this.ProgramasEducativos = new ProgramasEducativos(academicosRoles.ProgramasEducativos);
        }

        public AccesoADatos.AcademicosRoles ConvertirAcademicoRolDominioEnAcademicoRolAccesoADatos()
        {
            return new AccesoADatos.AcademicosRoles()
            {
                idUsuario = this.idUsuario,
                idProgramaEducativo = this.idProgramaEducativo,
                idRol = this.idRol,
                idAcademico = this.idAcademico
            };
        }

        public int idAcademico { get; set; }
        public int idRol { get; set; }
        public int idProgramaEducativo { get; set; }
        public int idUsuario { get; set; }

        public Academicos Academicos { get; set; }
        public AcademicosUsuarios AcademicosUsuarios { get; set; }
        public ProgramasEducativos ProgramasEducativos { get; set; }
        public Roles Roles { get; set; }
    }
}
