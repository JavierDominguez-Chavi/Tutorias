using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogicaDelNegocio
{
    public class AcademicosUsuariosDAO
    {
        public Boolean RegistrarAcademicoUsuario(Dominio.AcademicosUsuarios academicoUsuarioRegistrar)
        {
            Boolean registroExitoso = false;
            using (var entidades = new EntidadesTutorias())
            {
                entidades.AcademicosUsuarios.Add(academicoUsuarioRegistrar.ConvertirAcademicoUsuarioDominioEnAcademicoUsuarioAccesoADatos());
                registroExitoso = entidades.SaveChanges() == 1;
            }
            return registroExitoso;
        }

        public int ObtenerIdAcademicoUsuarioPorNombreYContrasena(Dominio.AcademicosUsuarios academicoUsuarioBuscar)
        {
            int idAcademicoUsuario = -1;
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var idAcademicoUsuarioObtenido = (from academicosUsuarios in entidadesTutorias.AcademicosUsuarios
                                                  where academicosUsuarios.NombreUsuario == academicoUsuarioBuscar.NombreUsuario &&
                                                  academicosUsuarios.Contrasena == academicoUsuarioBuscar.Contrasena
                                                  select academicosUsuarios.IdUsuario).FirstOrDefault();
                if (idAcademicoUsuarioObtenido != 0)
                {
                    idAcademicoUsuario = idAcademicoUsuarioObtenido;
                }
            }
            return idAcademicoUsuario;
        }

         public bool EliminarUsuariosAcademico(int idUsuario)
         {
            bool usuarioEliminado = false;
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.AcademicosUsuarios academicoUsuarioEncontrado = entidades.AcademicosUsuarios.FirstOrDefault(
                        academicoUsuario => academicoUsuario.IdUsuario == idUsuario);
                if (academicoUsuarioEncontrado != null)
                {
                    entidades.AcademicosUsuarios.Remove(academicoUsuarioEncontrado);
                    usuarioEliminado = entidades.SaveChanges() > 0;
                }
            }
            return usuarioEliminado;
         }

        public ObservableCollection<Dominio.AcademicosRoles> ObtenerRolesDeUsuario(string usuario, string contrasenia)
        {
            string codigo = EncriptadorContrasenas.Encriptar(contrasenia);
            ObservableCollection<Dominio.AcademicosRoles> academicosRolesObtenidos = new ObservableCollection<Dominio.AcademicosRoles>();
            using (var entidades = new EntidadesTutorias())
            {
                try
                {
                    SqlParameter usuarioP = new SqlParameter("@usuario", usuario);
                    SqlParameter contrasenaP = new SqlParameter("@contrasenia", codigo);

                    var rolesEncontrados = entidades.AcademicosRoles.SqlQuery(
                        "SELECT AR.* FROM AcademicosUsuarios AU " +
                        "INNER JOIN AcademicosRoles AR ON AR.idUsuario = AU.IdUsuario " +
                        "INNER JOIN Roles R ON R.IdRol = AR.idRol " +
                        "INNER JOIN Academicos A ON A.IdAcademico = AR.idAcademico " +
                        "WHERE AU.NombreUsuario = @usuario COLLATE SQL_Latin1_General_CP1_CS_AS " +
                        "AND AU.Contrasena = @contrasenia",
                        usuarioP,
                        contrasenaP
                    ).ToList();
                    if (rolesEncontrados.Count() > 0)
                    {
                        foreach (var academicoEncontrado in rolesEncontrados)
                        {
                            academicosRolesObtenidos.Add(new Dominio.AcademicosRoles(academicoEncontrado));
                        }

                        return academicosRolesObtenidos;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (EntityException ex)
                {
                    return null;
                }
            }
        }

        public ObservableCollection<Dominio.AcademicosUsuarios> ObtenerUsuariosAcademico(int idAcademico , int idProgramaEducativo)
        {
            ObservableCollection<Dominio.AcademicosUsuarios> usuariosObtenidos = new ObservableCollection<Dominio.AcademicosUsuarios>();
            using (var entidades = new EntidadesTutorias())
            {
                try
                {
                    
                    var usuariosEncontrados = entidades.AcademicosUsuarios.SqlQuery(
                    "SELECT AU.* FROM Academicos A " +
                    "INNER JOIN AcademicosRoles AR ON AR.idAcademico = A.IdAcademico " +
                    "INNER JOIN AcademicosUsuarios AU ON AU.IdUsuario = AR.idUsuario " +
                    "WHERE A.IdAcademico = @idAcademico AND AR.idProgramaEducativo = @idProgramaEducativo",
                    new SqlParameter("@idAcademico", idAcademico),
                    new SqlParameter("@idProgramaEducativo", idProgramaEducativo)
                    ).ToList();

                    if (usuariosEncontrados.Count() > 0)
                    {
                        foreach (var academicoEncontrado in usuariosEncontrados)
                        {
                            usuariosObtenidos.Add(new Dominio.AcademicosUsuarios(academicoEncontrado));
                        }

                        return usuariosObtenidos;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (EntityException ex)
                {
                    return null;
                }
            }
        }

    }
}