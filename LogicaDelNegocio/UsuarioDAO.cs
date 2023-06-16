using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogicaDelNegocio
{
    internal class UsuarioDAO
    {
        public ObservableCollection<Dominio.AcademicosRoles> ObtenerRolesDeUsuario(String usuario,String contrasenia)
        {
            string codigo = EncriptadorContrasenas.Encriptar(contrasenia);
            ObservableCollection<Dominio.AcademicosRoles> academicosRolesObtenidos = new ObservableCollection<Dominio.AcademicosRoles>(); 
            using (var entidades = new EntidadesTutorias())
            {
                var rolesEncontrados = entidades.AcademicosRoles.SqlQuery("SELECT AR.* FROM AcademicosUsuarios AU " +
                    "INNER JOIN AcademicosRoles AR ON AR.idUsuario = AU.IdUsuario" +
                    "INNER JOIN Roles R ON R.IdRol = AR.idRol " +
                    "INNER JOIN Academicos A ON A.IdAcademico = AR.idAcademico WHERE AU.NombreUsuario= '@usuario' AND AU.Contrasena = '@contrasenia' ; "
                    );
                if (rolesEncontrados.Count() > 0)
                {
                    foreach (var academicoEncontrado in rolesEncontrados)
                    {
                        academicosRolesObtenidos.Add(new Dominio.AcademicosRoles(academicoEncontrado));
                    }
                    return academicosRolesObtenidos;
                }
                else {
                    return null;
                }
                    

            }
               

        }
    }
}
