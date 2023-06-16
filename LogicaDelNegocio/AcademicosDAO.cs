using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos;
using System.Windows;

using Dominio;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LogicaDelNegocio
{
    public class AcademicosDAO
    {
        public Dominio.Academicos ObtenerAcademicoPorIdAcademico(int idAcademico)
        {
            Dominio.Academicos academicoObtenido = new Dominio.Academicos();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var academicoEncontrado = entidadesTutorias.Academicos.Find(idAcademico);
                if (academicoEncontrado != null)
                {
                    academicoObtenido = new Dominio.Academicos(academicoEncontrado);
                }
            }
            return academicoObtenido;
        }

        public Boolean RegistrarAcademico(Dominio.Academicos academicoRegistrar)
        {
            Boolean registroExitoso = false;
            using (var entidades = new EntidadesTutorias())
            {
                entidades.Academicos.Add(academicoRegistrar.ConvertirAcademicoDominioEnAcademicoAccesoADatos());
                registroExitoso = entidades.SaveChanges() == 1;
            }
            return registroExitoso;
        }

        public Boolean BuscarAcademicoPorNombreYApellidos(Dominio.Academicos academicoBuscar)
        {
            Boolean academicoEncontrado = false;
            using (var entidades = new EntidadesTutorias())
            {
                academicoEncontrado = entidades.Academicos.Any(academico => academico.Nombre == academicoBuscar.Nombre &&
                    academico.ApellidoPaterno == academicoBuscar.ApellidoPaterno &&
                    academico.ApellidoMaterno == academicoBuscar.ApellidoMaterno);
            }
            return academicoEncontrado;
        }

        public int ObtenerIdAcademicoPorNombreYApellidos(Dominio.Academicos academicoBuscar)
        {
            int idAcademico = -1;
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var idAcademicoObtenido = (from academicos in entidadesTutorias.Academicos
                                 where academicos.Nombre == academicoBuscar.Nombre &&
                                 academicos.ApellidoPaterno == academicoBuscar.ApellidoPaterno &&
                                 academicos.ApellidoMaterno == academicoBuscar.ApellidoMaterno
                                 select academicos.IdAcademico).FirstOrDefault();
                if (idAcademicoObtenido != 0)
                {
                    idAcademico = idAcademicoObtenido;
                }
            }
            return idAcademico;
        }

        public ObservableCollection<Dominio.Academicos>
        ObtenerTutoresPorIdProgramaEducativo(int idProgramaEducativo)
        {
            ObservableCollection<Dominio.Academicos> tutoresObtenidos =
                new ObservableCollection<Dominio.Academicos>();
            using (var entidades = new EntidadesTutorias())
            {
                var tutoresEncontrados = 
                    (from academico in entidades.Academicos
                    join academicoRol in entidades.AcademicosRoles
                    on academico.IdAcademico equals academicoRol.idAcademico
                    where academicoRol.idRol == 
                        (int)Dominio.EnumRolesDeUsuario.Rol_Tutor_Academico
                    && academicoRol.idProgramaEducativo == idProgramaEducativo
                    select academico)
                    .ToList();

                foreach (AccesoADatos.Academicos tutorEncontrado in tutoresEncontrados)
                {
                    Dominio.Academicos tutorObtenido = new Dominio.Academicos(tutorEncontrado);
                    tutorObtenido.NumeroTutorados =
                        tutorEncontrado
                        .Estudiantes
                        .Count(estudiante =>
                        estudiante.SemestreQueCursa > 0
                        && estudiante.IdProgramaEducativo == idProgramaEducativo);
                    tutoresObtenidos.Add(tutorObtenido);
                }
            }
            return new ObservableCollection<Dominio.Academicos>(tutoresObtenidos.GroupBy(tutor => tutor.IdAcademico, (key, group) => group.First()).ToList());
        }
        public List<Dominio.Academicos> ObtenerAcademicos()
        {
            List<Dominio.Academicos> academicosObtenidos = new List<Dominio.Academicos>();
            using (var entidades = new EntidadesTutorias())
            {
                var academicosEncontrados = (from Academicos in entidades.Academicos select Academicos).ToList();
                if (academicosEncontrados.FirstOrDefault() != null)
                {
                    foreach (AccesoADatos.Academicos academicoEncontrado in academicosEncontrados)
                    {
                       academicosObtenidos.Add(new Dominio.Academicos(academicoEncontrado));
                    }
                }
            }

            return academicosObtenidos;
        }

        public Boolean ActualizarAcademico(Dominio.Academicos academicoModificado)
        {
            bool academicoActualizado = false;
            if (academicoModificado.IdAcademico > 0)
            {
                using (var entidadesTutorias = new EntidadesTutorias())
                {
                    AccesoADatos.Academicos academicoObtenido = entidadesTutorias.Academicos.Find(academicoModificado.IdAcademico);
                    academicoObtenido.IdAcademico = academicoModificado.IdAcademico;
                    academicoObtenido.Nombre = academicoModificado.Nombre;
                    academicoObtenido.ApellidoMaterno = academicoModificado.ApellidoMaterno;
                    academicoObtenido.ApellidoPaterno = academicoModificado.ApellidoPaterno;
                    academicoObtenido.CorreoInstitucional = academicoModificado.CorreoInstitucional;
                    academicoObtenido.CorreoPersonal = academicoModificado.CorreoPersonal;
                    entidadesTutorias.Academicos.AddOrUpdate(academicoObtenido);
                    academicoActualizado = entidadesTutorias.SaveChanges() == 1;
                }
            }
            return academicoActualizado;
        }

        public ObservableCollection<Dominio.Academicos> ObtenerAcademicosDeEstudiante(Dominio.Estudiantes estudiante)
        {
            ObservableCollection<Dominio.Academicos> academicosObtenidos = new ObservableCollection<Dominio.Academicos>();

            using (var entidades = new EntidadesTutorias())
            {
                SqlParameter matricula = new SqlParameter("@Matricula", estudiante.Matricula);

                var academicosEncontrados = entidades.Academicos.SqlQuery("" +
                    "SELECT A.*,EE.*, EEA.* From Estudiantes E " +
                    "INNER JOIN ExperienciasEducativas_Estudiantes EEE ON EEE.Matricula = E.Matricula " +
                    "INNER JOIN ExperienciasEducativas_Academicos EEA ON EEA.NRC = EEE.NRC " +
                    "INNER JOIN ExperienciasEducativas EE ON EE.IdExperienciaEducativa = EEA.IdExperienciaEducativa " +
                    "INNER JOIN Academicos A ON A.IdAcademico = EEA.IdAcademico " +
                    "WHERE E.Matricula = @Matricula;", matricula).ToList();
                foreach (AccesoADatos.Academicos academicoEncontrado in academicosEncontrados)
                {
                    Dominio.Academicos academico = new Dominio.Academicos();


                    academico.ApellidoPaterno = academicoEncontrado.ApellidoPaterno;
                    academico.Nombre = academicoEncontrado.Nombre;
                    academico.ApellidoMaterno = academicoEncontrado.ApellidoMaterno;

                    Dominio.ExperienciasEducativas experienciasEducativa = new Dominio.ExperienciasEducativas();
                    Dominio.ExperienciasEducativas_Academicos experienciasEducativas_Academicos =
                        new Dominio.ExperienciasEducativas_Academicos();

                    experienciasEducativa.Nombre = academicoEncontrado.ExperienciasEducativas_Academicos.FirstOrDefault().ExperienciasEducativas.Nombre;
                    experienciasEducativas_Academicos.NRC = academicoEncontrado.ExperienciasEducativas_Academicos.FirstOrDefault().NRC;



                    academico.ExperienciasEducativas_Academicos.Add(experienciasEducativas_Academicos);
                    academico.ExperienciasEducativas_Academicos.FirstOrDefault().ExperienciasEducativas = experienciasEducativa;
                    academicosObtenidos.Add(academico);


                }

            }
            return academicosObtenidos;
        }


        public ObservableCollection<Dominio.Academicos> BuscarAcademicoPorCorreoInstitucional(Dominio.Academicos academico)
        {
            ObservableCollection<Dominio.Academicos> academicosObtenidos = new ObservableCollection<Dominio.Academicos>();

            using (var entidades = new EntidadesTutorias())
            {
                SqlParameter correo = new SqlParameter("@CorreoInstitucional", academico.CorreoInstitucional);
                var academicosEncontrados = entidades.Academicos.SqlQuery("SELECT * FROM dbo.Academicos WHERE Academicos.CorreoInstitucional = @CorreoInstitucional;", correo).ToList();

                foreach (var academicoEncontrado in academicosEncontrados)
                {
                    Dominio.Academicos academicoObtenido = new Dominio.Academicos();
                    academicoObtenido.Nombre = academicoEncontrado.Nombre;
                    academicoObtenido.ApellidoPaterno = academicoEncontrado.ApellidoPaterno;
                    academicoObtenido.ApellidoMaterno = academicoEncontrado.ApellidoMaterno;
                    academicoObtenido.IdAcademico = academicoEncontrado.IdAcademico;
                    academicosObtenidos.Add(academicoObtenido);
                }
                entidades.Dispose();
            }

            return academicosObtenidos;
        }

    }
}
