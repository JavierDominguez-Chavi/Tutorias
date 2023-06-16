using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos;

namespace LogicaDelNegocio
{
    public class AcademicosRolesDAO
    {
        public Boolean RegistrarAcademicoRol(Dominio.AcademicosRoles academicoRolRegistrar)
        {
            Boolean registroExitoso = false;
            using (var entidades = new EntidadesTutorias())
            {
                entidades.AcademicosRoles.Add(academicoRolRegistrar.ConvertirAcademicoRolDominioEnAcademicoRolAccesoADatos());
                registroExitoso = entidades.SaveChanges() == 1;
            }
            return registroExitoso;
        }
        public List<Dominio.AcademicosRoles> ObtenerAcademicosPorProgramaEducativo(int idProgramaEducativo)
        {
            List<Dominio.AcademicosRoles> lista =
                new List<Dominio.AcademicosRoles>();
            using (var entidades = new EntidadesTutorias())
            {
                var academicosEncontrados =
                    (from AcademicosRoles in entidades.AcademicosRoles
                     where AcademicosRoles.idProgramaEducativo == idProgramaEducativo
                     select AcademicosRoles).ToList();
                foreach (AccesoADatos.AcademicosRoles academico in academicosEncontrados)
                {
                    Dominio.AcademicosRoles academicosRoles = new Dominio.AcademicosRoles();
                    academicosRoles.idAcademico = academico.idAcademico;
                    academicosRoles.idRol = academico.idRol;
                    academicosRoles.idProgramaEducativo = academico.idProgramaEducativo;
                    academicosRoles.idUsuario = academico.idUsuario;
                    academicosRoles.Academicos = new Dominio.Academicos(academico.Academicos);
                    lista.Add(academicosRoles);
                }
            }
            return lista;
        }
        public bool EliminarAcademicoRol(Dominio.AcademicosRoles academicoRolAEliminar)
        {
            bool eliminacionExitosa = false;
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.AcademicosRoles academicoRolEncontrado =
                    entidades.AcademicosRoles.FirstOrDefault(
                        academicoRolBD =>
                        academicoRolBD.idAcademico == academicoRolAEliminar.idAcademico &&
                        academicoRolBD.idProgramaEducativo == academicoRolAEliminar.idProgramaEducativo &&
                        academicoRolBD.idRol == academicoRolAEliminar.idRol);
                if (academicoRolEncontrado != null)
                {
                    entidades.AcademicosRoles.Remove(academicoRolEncontrado);
                    eliminacionExitosa = entidades.SaveChanges() > 0;
                }
            }
            return eliminacionExitosa;
        }

        public List<Dominio.AcademicosRoles> ConsultarInformacionAcademico(int idAcademico)
        {
            List<Dominio.AcademicosRoles> academicosRolesObtenidos = new List<Dominio.AcademicosRoles>();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var academicosRolesEncontrados = (from academicosRoles in entidadesTutorias.AcademicosRoles
                                                  where academicosRoles.idAcademico == idAcademico
                                                  select academicosRoles).ToList();
                if (academicosRolesEncontrados.FirstOrDefault() != null)
                {
                    foreach (AccesoADatos.AcademicosRoles academicoRolEncontrado in academicosRolesEncontrados)
                    {
                        academicosRolesObtenidos.Add(new Dominio.AcademicosRoles(academicoRolEncontrado));
                    }
                }
            }
            return academicosRolesObtenidos;
        }

        public List<Dominio.Academicos> ConsularAcademicosSinRol()
        {
            List<Dominio.Academicos> academicosSinRolObtenidos = new List<Dominio.Academicos>();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var academicosSinRoleEncontrados = (from academicosRoles in entidadesTutorias.AcademicosRoles
                                                    join academicos in entidadesTutorias.Academicos
                                                    on academicosRoles.idAcademico equals academicos.IdAcademico
                                                    where academicosRoles.idRol == (int)Dominio.EnumRolesDeUsuario.Rol_Sin_Rol
                                                    select academicos).Distinct().ToList();
                if (academicosSinRoleEncontrados.FirstOrDefault() != null)
                {
                    foreach (AccesoADatos.Academicos academicoSinRolEncontrado in academicosSinRoleEncontrados)
                    {
                        academicosSinRolObtenidos.Add(new Dominio.Academicos(academicoSinRolEncontrado));
                    }
                }
            }
            return academicosSinRolObtenidos;

        }

        public bool EliminarRolAcademicoPorProgramaEducativo(Dominio.AcademicosRoles academicoRolAEliminar)
        {
            bool academicoRolEliminado = false;
            if (academicoRolAEliminar.idAcademico > 0 && academicoRolAEliminar.idProgramaEducativo > 0)
            {
                using (var entidadesTutorias = new EntidadesTutorias())
                {
                    AccesoADatos.AcademicosRoles academicoRolObtenido = entidadesTutorias.AcademicosRoles.FirstOrDefault(
                        academicoRol=>academicoRol.idAcademico == academicoRolAEliminar.idAcademico && 
                        academicoRol.idProgramaEducativo == academicoRolAEliminar.idProgramaEducativo);
                        academicoRolObtenido.idAcademico = academicoRolAEliminar.idAcademico;
                        academicoRolObtenido.idRol = academicoRolAEliminar.idRol;
                        academicoRolObtenido.idProgramaEducativo = academicoRolAEliminar.idProgramaEducativo;
                        academicoRolObtenido.idUsuario = academicoRolAEliminar.idUsuario;
                        entidadesTutorias.AcademicosRoles.Remove(academicoRolObtenido);
                    academicoRolEliminado = entidadesTutorias.SaveChanges() == 1;
                }
            }
            return academicoRolEliminado;
        }

        public List<Dominio.AcademicosRoles> ConsularTutoresAcademicosPorProgramaEducativo(int idProgramaEducativo)
        {
            List<Dominio.AcademicosRoles> listaAcademicosRoles =
                new List<Dominio.AcademicosRoles>();
            using (var entidades = new EntidadesTutorias())
            {
                var academicosRolesEncontrados =
                    (from AcademicosRoles in entidades.AcademicosRoles
                     where AcademicosRoles.idProgramaEducativo == idProgramaEducativo &&
                     AcademicosRoles.idRol == (int)Dominio.EnumRolesDeUsuario.Rol_Tutor_Academico
                     select AcademicosRoles).ToList();
                if (academicosRolesEncontrados.FirstOrDefault() != null) {
                    foreach (AccesoADatos.AcademicosRoles academicoRol in academicosRolesEncontrados)
                    {
                        Dominio.AcademicosRoles academicosRoles = new Dominio.AcademicosRoles();
                        academicosRoles.idAcademico = academicoRol.idAcademico;
                        academicosRoles.idRol = academicoRol.idRol;
                        academicosRoles.idProgramaEducativo = academicoRol.idProgramaEducativo;
                        academicosRoles.idUsuario = academicoRol.idUsuario;
                        academicosRoles.Academicos = new Dominio.Academicos(academicoRol.Academicos);
                        listaAcademicosRoles.Add(academicosRoles);
                    }
                }
            }
            return listaAcademicosRoles;
        }

        public Dominio.AcademicosUsuarios ObtenerAcademicosUsuarios(int idAcademico, int idProgramaEducativo)
        {
            Dominio.AcademicosUsuarios academicosUsuariosObtenido = new Dominio.AcademicosUsuarios();

            using (var entidades = new EntidadesTutorias())
            {
                var academicosUsuarioEncontrado = entidades.AcademicosUsuarios.SqlQuery(
                    "SELECT AU.* FROM Academicos A " +
                    "INNER JOIN AcademicosRoles AR ON AR.idAcademico = A.IdAcademico " +
                    "INNER JOIN AcademicosUsuarios AU ON AU.IdUsuario = AR.idUsuario " +
                    "WHERE A.IdAcademico = @IdAcademico AND AR.idProgramaEducativo = @IdProgramaEducativo;",
                     new SqlParameter("@IdAcademico", idAcademico),
                     new SqlParameter("@IdProgramaEducativo", idProgramaEducativo)
                    );
                academicosUsuariosObtenido = new Dominio.AcademicosUsuarios(academicosUsuarioEncontrado.FirstOrDefault());
            }
            return academicosUsuariosObtenido;
        }

        public bool EliminarRolesDeAcademicos(int idAcademico, int idProgramaEducativo, int idUsuario, ObservableCollection<int> rolesAEliminar)
        {
            bool resultado = false;
            using (var entidades = new EntidadesTutorias())
            {
                var parametros = new List<SqlParameter>();
                var eliminaciones = new StringBuilder();

                for (int i = 0; i < rolesAEliminar.Count(); i++)
                {
                    eliminaciones.Append("DELETE FROM [dbo].[AcademicosRoles] ");
                    eliminaciones.Append("WHERE [idAcademico] = @idAcademico" + i + " AND [idRol] = @idRol" + i + " AND [idProgramaEducativo] = @idProgramaEducativo" + i + " AND [idUsuario] = @idUsuario" + i + "; ");

                    parametros.Add(new SqlParameter("@idAcademico" + i, idAcademico));
                    parametros.Add(new SqlParameter("@idRol" + i, rolesAEliminar.ElementAt(i)));
                    parametros.Add(new SqlParameter("@idProgramaEducativo" + i, idProgramaEducativo));
                    parametros.Add(new SqlParameter("@idUsuario" + i, idUsuario));
                }

                var query = eliminaciones.ToString();
                resultado = entidades.Database.ExecuteSqlCommand(query, parametros.ToArray()) == rolesAEliminar.Count();
            }

            return resultado;
        }

        public ObservableCollection<Dominio.ProgramasEducativos> ObtenerProgramasEducativosDeAcademicos(String correoInstitucional)
        {
            ObservableCollection<Dominio.ProgramasEducativos> programasEducativosObtenidos = new ObservableCollection<Dominio.ProgramasEducativos>();


            using (var entidades = new EntidadesTutorias())
            {
                var programasEducativosEncontrados = entidades.ProgramasEducativos.SqlQuery("Select DISTINCT PE.* FROM dbo.Academicos A " +
                    "INNER JOIN dbo.AcademicosRoles AR ON AR.idAcademico = A.IdAcademico " +
                    "INNER JOIN dbo.ProgramasEducativos PE ON PE.IdProgramaEducativo = AR.idProgramaEducativo " +
                    "WHERE A.CorreoInstitucional = @CorreoAcademico", new SqlParameter("@CorreoAcademico", correoInstitucional)); ;

                foreach (var ProgramaEducativo in programasEducativosEncontrados)
                {
                    programasEducativosObtenidos.Add(new Dominio.ProgramasEducativos(ProgramaEducativo));
                }
            }

            return programasEducativosObtenidos;


        }

        public bool ModificarRolesDeAcademicos(int idAcademico, int idProgramaEducativo, int idUsuario, ObservableCollection<int> rolesAAsignar)
        {
            bool resultado = false;
            using (var entidades = new EntidadesTutorias())
            {
                var parametros = new List<SqlParameter>();
                var inserciones = new StringBuilder();

                for (int i = 0; i < rolesAAsignar.Count(); i++)
                {
                    inserciones.Append("INSERT INTO [dbo].[AcademicosRoles] (idAcademico, idRol, idProgramaEducativo, idUsuario) ");
                    inserciones.Append("VALUES (@idAcademico" + i + ", @idRol" + i + ", @idProgramaEducativo" + i + ", @idUsuario" + i + "); ");

                    parametros.Add(new SqlParameter("@idAcademico" + i, idAcademico));
                    parametros.Add(new SqlParameter("@idRol" + i, rolesAAsignar.ElementAt(i)));
                    parametros.Add(new SqlParameter("@idProgramaEducativo" + i, idProgramaEducativo));
                    parametros.Add(new SqlParameter("@idUsuario" + i, idUsuario));
                }

                var query = inserciones.ToString();
                resultado = entidades.Database.ExecuteSqlCommand(query, parametros.ToArray()) == rolesAAsignar.Count();
            }

            return resultado;
        }

        public ObservableCollection<Dominio.Roles> ObtenerRolesDeAcademicos(String correoInstitucional, int idUsuario)
        {
            ObservableCollection<Dominio.Roles> rolesObtenidos = new ObservableCollection<Dominio.Roles>();

            using (var entidades = new EntidadesTutorias())
            {

                var rolesEncontrados = entidades.Roles.SqlQuery(
                    "Select * from Roles R " +
                    "INNER JOIN AcademicosRoles AR ON AR.idRol = R.IdRol " +
                    "INNER JOIN Academicos A ON A.IdAcademico = AR.idAcademico " +
                    "WHERE A.CorreoInstitucional = @Correo AND AR.idUsuario = @idUsuario",
                    new SqlParameter("@correo", correoInstitucional),
                    new SqlParameter("@idUsuario",idUsuario)
                    );

                foreach (var r in rolesEncontrados)
                {
                    rolesObtenidos.Add(new Dominio.Roles(r));
                }
            }
            return rolesObtenidos;

        }

        public bool ValidarRolJefeDeCarreraPorProgramaEducativo(int idProgramaEducativo)
        {
            bool resultado = false;
            using (var entidades = new EntidadesTutorias())
            {
                int JefeDeCarreraRol = 3807;
                SqlParameter rol = new SqlParameter("@rol", JefeDeCarreraRol);

                var result = entidades.Database.SqlQuery<int>("Select COUNT(A.IdAcademico) from AcademicosRoles AR  " +
                    "INNER JOIN Academicos A ON A.IdAcademico = AR.idAcademico " +
                    "INNER JOIN dbo.ProgramasEducativos PE ON PE.IdProgramaEducativo = AR.idProgramaEducativo  " +
                    "WHERE AR.IdRol = @rol", rol
                    );
                if (result.First().Equals(0))
                {
                    return true;
                }
                else
                {
                    return resultado;
                }

            }
        }


    }
}