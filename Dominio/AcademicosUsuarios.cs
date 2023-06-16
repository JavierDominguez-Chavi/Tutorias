using System.Collections.ObjectModel;

namespace Dominio
{
    public class AcademicosUsuarios
    {
        public AcademicosUsuarios()
        {
            this.AcademicosRoles = new ObservableCollection<AcademicosRoles>();
        }

        public AcademicosUsuarios(AccesoADatos.AcademicosUsuarios academicoUsuario)
        {
            this.IdUsuario = academicoUsuario.IdUsuario;
            this.NombreUsuario = academicoUsuario.NombreUsuario;
            this.Contrasena = academicoUsuario.Contrasena;
            this.AcademicosRoles = new ObservableCollection<AcademicosRoles>();
        }

        public AccesoADatos.AcademicosUsuarios ConvertirAcademicoUsuarioDominioEnAcademicoUsuarioAccesoADatos()
        {
            return new AccesoADatos.AcademicosUsuarios()
            {
                NombreUsuario = this.NombreUsuario,
                Contrasena = this.Contrasena
            };
        }

        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }

        public ObservableCollection<AcademicosRoles> AcademicosRoles { get; set; }
    }
}
