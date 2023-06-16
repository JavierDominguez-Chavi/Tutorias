using System.Collections.ObjectModel;

namespace Dominio
{
    public class Roles
    {
        public Roles()
        {
            this.AcademicosRoles = new ObservableCollection<AcademicosRoles>();
        }
        public Roles(AccesoADatos.Roles rol)
        {
            this.IdRol = rol.IdRol;
            this.NombreRol = rol.NombreRol;
            this.AcademicosRoles = new ObservableCollection<AcademicosRoles>();
        }

        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public ObservableCollection<AcademicosRoles> AcademicosRoles { get; set; }
    }
}
