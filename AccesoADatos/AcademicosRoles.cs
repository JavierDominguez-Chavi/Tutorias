//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccesoADatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class AcademicosRoles
    {
        public int idAcademico { get; set; }
        public int idRol { get; set; }
        public int idProgramaEducativo { get; set; }
        public int idUsuario { get; set; }
    
        public virtual Academicos Academicos { get; set; }
        public virtual AcademicosUsuarios AcademicosUsuarios { get; set; }
        public virtual ProgramasEducativos ProgramasEducativos { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
