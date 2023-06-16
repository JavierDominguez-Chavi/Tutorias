using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos;

namespace LogicaDelNegocio
{
    public class ExperienciasEducativas_AcademicosDAO
    {
        public Dominio.ExperienciasEducativas_Academicos ObtenerExperienciaEducativaAcademicoPorNRC(int NRC)
        {
            Dominio.ExperienciasEducativas_Academicos experienciaEducativaAcademicoObtenida =
                new Dominio.ExperienciasEducativas_Academicos();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                ExperienciasEducativasDAO experienciasEducativasDAO = new ExperienciasEducativasDAO();
                AcademicosDAO academicosDAO = new AcademicosDAO();
                var experienciaEducativaAcademicoEncontrada = entidadesTutorias.ExperienciasEducativas_Academicos.Find(NRC);
                if (experienciaEducativaAcademicoEncontrada != null)
                {
                    experienciaEducativaAcademicoObtenida =
                        new Dominio.ExperienciasEducativas_Academicos(experienciaEducativaAcademicoEncontrada);
                    experienciaEducativaAcademicoObtenida.Academicos =
                        academicosDAO.ObtenerAcademicoPorIdAcademico((int)experienciaEducativaAcademicoObtenida.IdAcademico);
                    experienciaEducativaAcademicoObtenida.ExperienciasEducativas =
                        experienciasEducativasDAO.ObtenerExperienciaEducativaPorIdExperienciaEducativa(experienciaEducativaAcademicoObtenida.IdExperienciaEducativa);
                }
            }
            return experienciaEducativaAcademicoObtenida;
        }
        public Boolean RegistrarExperienciaEducativa(Dominio.ExperienciasEducativas experienciasEducativasNueva)
        {
            Boolean registroExitoso = false;
            using (var entidades = new EntidadesTutorias())
            {
                Boolean existe =
                    entidades.ExperienciasEducativas.Any(experiencia =>
                        experiencia.Nombre == experienciasEducativasNueva.Nombre);
                if (existe)
                {
                    throw new DbEntityValidationException("La Experiencia Educativa ya existe.");
                }
                entidades.ExperienciasEducativas.Add(experienciasEducativasNueva.AAccesoADatos());
                registroExitoso = entidades.SaveChanges() == 1;
            }
            return registroExitoso;
        }
        public Boolean AsignarExperienciaEducativaAAcademico(Dominio.ExperienciasEducativas_Academicos asignacionNueva)
        {
            Boolean registroExitoso = false;
            using (var entidades = new EntidadesTutorias())
            {
                var experienciaAAsignar = entidades.ExperienciasEducativas_Academicos.FirstOrDefault(experiencia => experiencia.NRC == asignacionNueva.NRC);
                if (experienciaAAsignar != null)
                {
                    experienciaAAsignar.IdAcademico = asignacionNueva.IdAcademico;
                    entidades.SaveChanges();
                    registroExitoso = true;
                }
            }
            return registroExitoso;
        }
        public List<Dominio.ExperienciasEducativas_Academicos> ObtenerExperienciasEducativasSinAcademico()
        {
            List<Dominio.ExperienciasEducativas_Academicos> experienciasObtenidas = new List<Dominio.ExperienciasEducativas_Academicos>();
            using (var entidades = new EntidadesTutorias())
            {
                var experienciasEncontradas = (from ExperienciasEducativas_Academicos in entidades.ExperienciasEducativas_Academicos where ExperienciasEducativas_Academicos.IdAcademico == null select ExperienciasEducativas_Academicos).ToList();
                foreach (AccesoADatos.ExperienciasEducativas_Academicos experienciaEncontrada in experienciasEncontradas)
                {
                    experienciasObtenidas.Add(new Dominio.ExperienciasEducativas_Academicos(experienciaEncontrada));
                }
            }

            return experienciasObtenidas;
        }
        public List<Dominio.ExperienciasEducativas_Academicos> ObtenerExperienciasEducativas()
        {
            List<Dominio.ExperienciasEducativas_Academicos> experienciasObtenidas = new List<Dominio.ExperienciasEducativas_Academicos>();
            using (var entidades = new EntidadesTutorias())
            {
                var experienciasEncontradas = (from ExperienciasEducativas_Academicos in entidades.ExperienciasEducativas_Academicos select ExperienciasEducativas_Academicos).ToList();
                foreach (AccesoADatos.ExperienciasEducativas_Academicos experienciaEncontrada in experienciasEncontradas)
                {
                    experienciasObtenidas.Add(new Dominio.ExperienciasEducativas_Academicos(experienciaEncontrada));
                }
            }

            return experienciasObtenidas;
        }

        public ObservableCollection<Dominio.ExperienciasEducativas_Academicos> obtenerExperieniciasEducativasPorPeriodo(string nombrePeriodo)
        {
            ObservableCollection<Dominio.ExperienciasEducativas_Academicos> experienciasEducativas_AcademicosObtenidas = new ObservableCollection<Dominio.ExperienciasEducativas_Academicos>();

            using (var entidades = new EntidadesTutorias())
            {
                var parametros1 = new SqlParameter("@Nombre1", nombrePeriodo);
                var parametros2 = new SqlParameter("@Nombre2", nombrePeriodo);
                var parametros3 = new SqlParameter("@Nombre3", nombrePeriodo);
                var parametros4 = new SqlParameter("@Nombre4", nombrePeriodo);

                var experienciasEducativasAcademicosEncontradas = entidades.Database.SqlQuery<ExperienciasEducativas_Academicos>(
                    "SELECT EEA.* " +
                    "FROM DBO.ExperienciasEducativas_Academicos EEA " +
                    "INNER JOIN ExperienciasEducativas EE ON EE.IdExperienciaEducativa = EEA.IdExperienciaEducativa " +
                    "INNER JOIN Academicos A ON A.IdAcademico = EEA.IdAcademico " +
                    "INNER JOIN ExperienciasEducativas_PeriodosEscolares EEP ON EEP.NRC = EEA.NRC " +
                    "INNER JOIN PeriodosEscolares PE ON PE.IdPeriodoEscolar = EEP.IdPeriodo " +
                    "WHERE PE.Nombre = @Nombre1;",
                    parametros1).ToList();

                var experienciasEducativasEncontradas = entidades.Database.SqlQuery<ExperienciasEducativas>(
                    "SELECT EE.* " +
                    "FROM DBO.ExperienciasEducativas_Academicos EEA " +
                    "INNER JOIN ExperienciasEducativas EE ON EE.IdExperienciaEducativa = EEA.IdExperienciaEducativa " +
                    "INNER JOIN Academicos A ON A.IdAcademico = EEA.IdAcademico " +
                    "INNER JOIN ExperienciasEducativas_PeriodosEscolares EEP ON EEP.NRC = EEA.NRC " +
                    "INNER JOIN PeriodosEscolares PE ON PE.IdPeriodoEscolar = EEP.IdPeriodo " +
                    "WHERE PE.Nombre = @Nombre2;",
                    parametros2).ToList();

                var academicosEncontrados = entidades.Database.SqlQuery<Academicos>(
                    "SELECT A.* " +
                    "FROM DBO.ExperienciasEducativas_Academicos EEA " +
                    "INNER JOIN ExperienciasEducativas EE ON EE.IdExperienciaEducativa = EEA.IdExperienciaEducativa " +
                    "INNER JOIN Academicos A ON A.IdAcademico = EEA.IdAcademico " +
                    "INNER JOIN ExperienciasEducativas_PeriodosEscolares EEP ON EEP.NRC = EEA.NRC " +
                    "INNER JOIN PeriodosEscolares PE ON PE.IdPeriodoEscolar = EEP.IdPeriodo " +
                    "WHERE PE.Nombre = @Nombre3;",
                    parametros3).ToList();

                var programaEduativosEncontrados = entidades.Database.SqlQuery<ProgramasEducativos>(
                    "SELECT PED.NombreProgramaEducativo, PED.IdProgramaEducativo " +
                    "FROM DBO.ExperienciasEducativas_Academicos EEA " +
                    "INNER JOIN ExperienciasEducativas EE ON EE.IdExperienciaEducativa = EEA.IdExperienciaEducativa " +
                    "INNER JOIN Academicos A ON A.IdAcademico = EEA.IdAcademico " +
                    "INNER JOIN ExperienciasEducativas_PeriodosEscolares EEP ON EEP.NRC = EEA.NRC " +
                    "INNER JOIN PeriodosEscolares PE ON PE.IdPeriodoEscolar = EEP.IdPeriodo " +
                    "INNER JOIN ProgramasEducativos PED ON PED.IdProgramaEducativo = EEA.IdProgramaEducativo " +
                    "WHERE PE.Nombre = @Nombre4;",
                    parametros4).ToList();



                for (int i = 0; i < experienciasEducativasAcademicosEncontradas.Count(); i++)
                {
                    Dominio.ExperienciasEducativas_Academicos experienciaEducativaAcademica = new Dominio.ExperienciasEducativas_Academicos();
                    Dominio.Academicos academico = new Dominio.Academicos();
                    Dominio.ExperienciasEducativas experienciasEducativas = new Dominio.ExperienciasEducativas();
                    Dominio.ProgramasEducativos programaEducativo = new Dominio.ProgramasEducativos();

                    academico.Nombre = academicosEncontrados.ElementAt(i).Nombre;
                    academico.ApellidoPaterno = academicosEncontrados.ElementAt(i).ApellidoPaterno;
                    academico.ApellidoMaterno = academicosEncontrados.ElementAt(i).ApellidoMaterno;
                    academico.IdAcademico = academicosEncontrados.ElementAt(i).IdAcademico;
                    
                    experienciasEducativas.Nombre = experienciasEducativasEncontradas.ElementAt(i).Nombre;

                    programaEducativo.NombreProgramaEducativo = programaEduativosEncontrados.ElementAt(i).NombreProgramaEducativo;

                    experienciaEducativaAcademica.Academicos = academico;
                    experienciaEducativaAcademica.ExperienciasEducativas = experienciasEducativas;
                    experienciaEducativaAcademica.NRC = experienciasEducativasAcademicosEncontradas.ElementAt(i).NRC;
                    experienciaEducativaAcademica.ProgramasEducativos = programaEducativo;

                    experienciasEducativas_AcademicosObtenidas.Add(experienciaEducativaAcademica);
                }

                entidades.Dispose();
            }

            return experienciasEducativas_AcademicosObtenidas;
        }

        public int ActualizarExperienciaEducativas_Academicos(Dominio.ExperienciasEducativas_Academicos experienciasEducativas_Academicos)
        {
            int resultado = 500;

            using (var entidades = new EntidadesTutorias())
            {
                var NRC = experienciasEducativas_Academicos.NRC;
                var IdProgramaEducativo = DBNull.Value;
                var IdExperienciaEducativa = experienciasEducativas_Academicos.IdExperienciaEducativa;
                var IdAcademico = experienciasEducativas_Academicos.IdAcademico;
                var estado = new SqlParameter("@estado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var mensaje = new SqlParameter("@mensaje", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

                resultado = entidades.Database.ExecuteSqlCommand("EXEC dbo.SPU_ExperienciaEducativas_Academicos @NRC, @IdProgramaEducativo, @IdExperienciaEducativa, @IdAcademico, @estado OUTPUT, @mensaje OUTPUT",
                    new SqlParameter("@NRC", NRC),
                    new SqlParameter("@IdProgramaEducativo", IdProgramaEducativo),
                    new SqlParameter("@IdExperienciaEducativa", IdExperienciaEducativa),
                    new SqlParameter("@IdAcademico", IdAcademico),
                    estado,
                    mensaje);

                var estadoSalida = estado.Value as int? ?? 0;
                var mensajeSalida = mensaje.Value as string ?? "";

                resultado = estadoSalida;
            }

            return resultado;
        }

        public int RegistrarExperienciasEducativas_Academicos(Dominio.ExperienciasEducativas_Academicos experienciasEducativas_Academicos)
        {
            int resultado = 500;

            using (var entidades = new EntidadesTutorias())
            {
                var NRC = experienciasEducativas_Academicos.NRC;
                var IdProgramaEducativo = experienciasEducativas_Academicos.IdProgramaEducativo;
                var IdExperienciaEducativa = experienciasEducativas_Academicos.IdExperienciaEducativa;
                var IdAcademico = experienciasEducativas_Academicos.IdAcademico;
                var estado = new SqlParameter("@estado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var mensaje = new SqlParameter("@mensaje", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };

                resultado = entidades.Database.ExecuteSqlCommand("EXEC dbo.SPI_ExperienciaEducativas_Academicos @NRC, @IdProgramaEducativo, @IdExperienciaEducativa, @IdAcademico, @estado OUTPUT, @mensaje OUTPUT",
                    new SqlParameter("@NRC", NRC),
                    new SqlParameter("@IdProgramaEducativo", IdProgramaEducativo),
                    new SqlParameter("@IdExperienciaEducativa", IdExperienciaEducativa),
                    new SqlParameter("@IdAcademico", IdAcademico),
                    estado,
                    mensaje);

                var estadoSalida = estado.Value as int? ?? 0;
                var mensajeSalida = mensaje.Value as string ?? "";

                resultado = estadoSalida;
            }

            return resultado;
        }

    }
}
