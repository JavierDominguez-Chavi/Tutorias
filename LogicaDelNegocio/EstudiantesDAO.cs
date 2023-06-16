using AccesoADatos;
using Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogicaDelNegocio
{
    public class EstudiantesDAO
    {

        public Dominio.Estudiantes ObtenerEstudiantePorMatricula(String matricula)
        {
            Dominio.Estudiantes estudianteObtenido = new Dominio.Estudiantes();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var estudianteEncontrado = entidadesTutorias.Estudiantes.Find(matricula);
                if (estudianteEncontrado != null)
                {
                    estudianteObtenido = new Dominio.Estudiantes(estudianteEncontrado);
                }
            }
            return estudianteObtenido;
        }

        public bool EstudianteNoExistente(string matricula)
        {
            bool estudianteNoExiste = true;

            using (var entidades = new EntidadesTutorias())
            {
                int estudianteEncontrado = (from Estudiantes in entidades.Estudiantes where Estudiantes.Matricula == matricula select Estudiantes).Count();
                if (estudianteEncontrado > 0)
                {
                    estudianteNoExiste = false;
                }
            }

            return estudianteNoExiste;
        }
        public bool RegistrarEstudiante(Dominio.Estudiantes estudianteNuevo)
        {
            bool registroRealizado = false;
            using (var entidades = new EntidadesTutorias())
            {
                entidades.Estudiantes.Add(estudianteNuevo.AAccesoADatos());
                entidades.SaveChanges();
                registroRealizado = true;
            }
            return registroRealizado;
        }
        public List<Dominio.Estudiantes> ObtenerEstudiantes(int idProgramaEducativo)
        {
            List<Dominio.Estudiantes> estudiantesObtenidos = new List<Dominio.Estudiantes>();
            using (var entidades = new EntidadesTutorias())
            {
                var estudiantesEncontrados = (from Estudiantes in entidades.Estudiantes where Estudiantes.IdProgramaEducativo == idProgramaEducativo select Estudiantes).ToList();
                foreach (AccesoADatos.Estudiantes estudianteEncontrado in estudiantesEncontrados)
                {
                    estudiantesObtenidos.Add(new Dominio.Estudiantes(estudianteEncontrado));
                }
            }

            return estudiantesObtenidos;
        }

        public ObservableCollection<Dominio.Estudiantes>
        ObtenerEstudiantesSinTutorPorProgramaEducativo(int idProgramaEducativo)
        {
            ObservableCollection<Dominio.Estudiantes> estudiantesObtenidos =
                new ObservableCollection<Dominio.Estudiantes>();
            if (idProgramaEducativo > 0)
            {
                var entidades = new EntidadesTutorias();
                var estudiantesEncontrados =
                    (from estudiante in entidades.Estudiantes
                     where estudiante.IdProgramaEducativo == idProgramaEducativo
                     && estudiante.IdAcademico == null
                     select estudiante)
                    .ToList();
                foreach (AccesoADatos.Estudiantes estudianteEncontrado in estudiantesEncontrados)
                {
                    estudiantesObtenidos.Add(new Dominio.Estudiantes(estudianteEncontrado));
                }
                entidades.Dispose();
            }
            return estudiantesObtenidos;
        }

        public int AsignarTutorAEstudiantes(Dictionary<String, Dominio.Estudiantes> estudiantes)
        {
            int asignacionExitosa = 0;
            using (var entidades = new EntidadesTutorias())
            {
                try
                {
                    foreach (KeyValuePair<String, Dominio.Estudiantes> registro in estudiantes)
                    {
                        var estudianteEncontrado = entidades.Estudiantes.Find(registro.Key);
                        estudianteEncontrado.IdAcademico = registro.Value.IdAcademico;
                    }
                    asignacionExitosa = entidades.SaveChanges();
                }
                catch (Exception exception)
                when (exception is DbUpdateException || exception is NullReferenceException)
                {
                    asignacionExitosa = 0;
                }
            }
            return asignacionExitosa;
        }

        public ObservableCollection<Dominio.Estudiantes>
        ObtenerTutoradosPorProgramaEducativo(Dominio.Academicos tutor, int idProgramaEducativo)
        {
            ObservableCollection<Dominio.Estudiantes> tutoradosObtenidos =
                new ObservableCollection<Dominio.Estudiantes>();
            using (var entidades = new EntidadesTutorias())
            {
                var tutoradosEncontrados =
                    (from estudiante in entidades.Estudiantes
                     where estudiante.IdAcademico == tutor.IdAcademico
                     && estudiante.SemestreQueCursa > 0
                     && estudiante.IdProgramaEducativo == idProgramaEducativo
                     select estudiante)
                    .ToList();
                foreach (AccesoADatos.Estudiantes tutoradoEncontrado in tutoradosEncontrados)
                {
                    tutoradosObtenidos.Add(new Dominio.Estudiantes(tutoradoEncontrado));
                }
            }
            return tutoradosObtenidos;
        }


        public ObservableCollection<AccesoADatos.Estudiantes> ObtenerEstudiantes(String objetoBusqueda)
        {
            ObservableCollection<AccesoADatos.Estudiantes> estudiantesRecuperados = new ObservableCollection<AccesoADatos.Estudiantes>();
            SqlParameter busqueda = new SqlParameter("@Busqueda", objetoBusqueda);

            using (var entidades = new EntidadesTutorias())
            {
                var EstudiantesObtenidos = entidades.Estudiantes.SqlQuery("Exec dbo.GetEstudiantes @Busqueda", busqueda).ToList();

                if (EstudiantesObtenidos.Count() > 0)
                {
                    for (int i = 0; i < EstudiantesObtenidos.Count(); i++)
                    {
                        estudiantesRecuperados.Add(EstudiantesObtenidos.ElementAt(i));
                    }

                    return estudiantesRecuperados;
                }
                else
                {
                    return estudiantesRecuperados;
                }
            };
        }
        public ObservableCollection<AccesoADatos.ExperienciasEducativas> GetExperienciasEducativasDeEstudiante(String matricula)
        {
            ObservableCollection<AccesoADatos.ExperienciasEducativas> experienciasEducativasDeEstudianteRecuperadas = new ObservableCollection<AccesoADatos.ExperienciasEducativas>();
            SqlParameter busqueda = new SqlParameter("@Busqueda", matricula);

            using (var entidades = new EntidadesTutorias())
            {
                var ExperienciasObtenidas = entidades.ExperienciasEducativas.SqlQuery("Select EE.IdExperienciaEducativa ,EE.Nombre from ExperienciasEducativas_Estudiantes EEE Inner Join Estudiantes E ON EEE.Matricula = E.Matricula INNER JOIN ExperienciasEducativas_Academicos EEA ON EEA.NRC = EEE.NRC INNER JOIN ExperienciasEducativas EE ON EE.IdExperienciaEducativa = EEA.IdExperienciaEducativa WHERE e.Matricula = @Busqueda", busqueda).ToList();
                if (ExperienciasObtenidas.Count() > 0)
                {

                    for (int i = 0; i < ExperienciasObtenidas.Count(); i++)
                    {

                        experienciasEducativasDeEstudianteRecuperadas.Add(ExperienciasObtenidas.ElementAt(i));
                    }
                    return experienciasEducativasDeEstudianteRecuperadas;
                }
                else
                {
                    return experienciasEducativasDeEstudianteRecuperadas;
                }
            }
        }

        public bool AsignarExperienciasEducativasAEstudiante(string matricula, ObservableCollection<int> ExperienciasAAsignar)
        {
            bool validador = false; 

            if (ExperienciasAAsignar.Count > 0)
            {
                using (var entidades = new EntidadesTutorias())
                {
                    for (int i = 0; i < ExperienciasAAsignar.Count; i++)
                    {
                        var experienciaId = ExperienciasAAsignar[i];

                        entidades.Database.ExecuteSqlCommand("EXEC dbo.InsertExperiencias_Estudiantes @ExperienciaId, @Matricula",
                            new SqlParameter("@ExperienciaId", experienciaId),
                            new SqlParameter("@Matricula", matricula));
                    }

                    validador = true; 
                }
            }

            return validador;
        }


        public async Task<bool> RegistrarEstudiantesPorLote(Dictionary<string, EstudianteImportado> estudiantes)
        {
            var entidades = new EntidadesTutorias();
            ValidadorEstudianteImportado validador = new ValidadorEstudianteImportado();
            foreach (KeyValuePair<string, EstudianteImportado> registro in estudiantes)
            {
                validador.Validar(registro.Value);
                if (registro.Value.HasErrors)
                {
                    return false;
                }
            }

            foreach (KeyValuePair<string, EstudianteImportado> registro in estudiantes)
            {
                entidades.Estudiantes.Add(registro.Value.AAccesoADatos());
            }

            int columnasAfectadas = entidades.SaveChanges();
            entidades.Dispose();
            return columnasAfectadas == estudiantes.Count;
        }

        public HashSet<string> ObtenerMatriculasActivasPorProgramaEducativo()
        {
            HashSet<string> matriculasObtenidas = new HashSet<string>();
            using (var entidades = new EntidadesTutorias())
            {
                matriculasObtenidas = (from estudiante in entidades.Estudiantes
                                       where estudiante.SemestreQueCursa > 0
                                       select estudiante.Matricula)
                                        .ToHashSet();
            }
            return matriculasObtenidas;
        }

        public HashSet<string> ObtenerMatriculasEn(HashSet<string> lista)
        {
            HashSet<string> matriculasObtenidas = new HashSet<string>();
            using (var entidades = new EntidadesTutorias())
            {
                matriculasObtenidas = (from estudiante in entidades.Estudiantes
                                       where lista.Any(matricula => matricula.Equals(estudiante.Matricula))
                                       select estudiante.Matricula
                                       ).ToHashSet();
            }
            return matriculasObtenidas;
        }

        public ObservableCollection<Dominio.Estudiantes> ObtenerEstudiantesPorIdAcademico(int idAcademico)
        {
            ObservableCollection<Dominio.Estudiantes> estudiantesObtenidos = new ObservableCollection<Dominio.Estudiantes>();
            using (var entidades = new EntidadesTutorias())
            {
                var estudiantesEncontrados = (from Estudiantes in entidades.Estudiantes
                    where Estudiantes.IdAcademico == idAcademico select Estudiantes).ToList();
                if (estudiantesEncontrados.FirstOrDefault() != null) {
                    foreach (AccesoADatos.Estudiantes estudianteEncontrado in estudiantesEncontrados)
                    {
                        estudiantesObtenidos.Add(new Dominio.Estudiantes(estudianteEncontrado));
                    }
                }
            }

            return estudiantesObtenidos;
        }

        public Boolean ValidarEstudianteComoNuevo(Dominio.Estudiantes estudianteBuscar)
        {
            Boolean existeEstudiante = false;
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                existeEstudiante = entidadesTutorias.Estudiantes.Any(estudiante =>
                estudiante.Matricula == estudianteBuscar.Matricula &&
                estudiante.Nombre == estudianteBuscar.Nombre &&
                estudiante.ApellidoPaterno == estudianteBuscar.ApellidoPaterno &&
                estudiante.ApellidoMaterno == estudianteBuscar.ApellidoMaterno &&
                estudiante.CorreoInstitucional == estudianteBuscar.CorreoInstitucional &&
                estudiante.CorreoPersonal == estudianteBuscar.CorreoPersonal &&
                estudiante.SemestreQueCursa == estudianteBuscar.SemestreQueCursa);

            }
            return existeEstudiante;
        }

        //Esto se hace asi por un mal diseño de la base de datos pues la matricula es primary key que no es posible modificar
        public Boolean ActualizarEstudianteConMatricula(String matricula, Dominio.Estudiantes estudianteActualizar)
        {
            Boolean estudianteActualizado = false;
            if (estudianteActualizar != null && matricula != null)
            {
                using (var entidadesTutorias = new EntidadesTutorias())
                {
                    entidadesTutorias.Estudiantes.AddOrUpdate(estudianteActualizar.AAccesoADatos());
                    estudianteActualizado = entidadesTutorias.SaveChanges() == 1;

                    ActualizarLlavesForaneasMatricula(matricula, estudianteActualizar);

                    var estudianteEncontrado = entidadesTutorias.Estudiantes.Find(matricula);
                    if (estudianteEncontrado != null)
                    {
                        entidadesTutorias.Estudiantes.Remove(estudianteEncontrado);
                        entidadesTutorias.SaveChanges();
                    }
                }

            }
            return estudianteActualizado;
        }

        private void ActualizarLlavesForaneasMatricula(String matricula, Dominio.Estudiantes estudianteActualizar)
        {
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var horariosEncontrados = (from Horarios in entidadesTutorias.Horarios
                                           where Horarios.Matricula == matricula
                                           select Horarios).ToList();
                if (horariosEncontrados.FirstOrDefault() != null)
                {
                    foreach (AccesoADatos.Horarios horario in horariosEncontrados)
                    {
                        horario.Matricula = estudianteActualizar.Matricula;
                        entidadesTutorias.Horarios.AddOrUpdate(horario);
                        entidadesTutorias.SaveChanges();
                    }
                }

                var problematicasEncontradas = (from problematicas in entidadesTutorias.Problematicas
                                                where problematicas.Matricula == matricula
                                                select problematicas).ToList();
                if (problematicasEncontradas.FirstOrDefault() != null)
                {
                    foreach (AccesoADatos.Problematicas problematica in problematicasEncontradas)
                    {
                        problematica.Matricula = estudianteActualizar.Matricula;
                        entidadesTutorias.Problematicas.AddOrUpdate(problematica);
                        entidadesTutorias.SaveChanges();
                    }
                }
            }
        }

        public Boolean ActualizarEstudiante(String matricula, Dominio.Estudiantes estudianteActualizar)
        {
            Boolean estudianteActualizado = false;
            if (estudianteActualizar != null && matricula != null)
            {
                using (var entidadesTutorias = new EntidadesTutorias())
                {
                    var estudianteEncontrado = entidadesTutorias.Estudiantes.Find(matricula);
                    if (estudianteEncontrado != null) {
                        estudianteEncontrado.Nombre = estudianteActualizar.Nombre;
                        estudianteEncontrado.ApellidoMaterno = estudianteActualizar.ApellidoMaterno;
                        estudianteEncontrado.ApellidoPaterno = estudianteActualizar.ApellidoPaterno;
                        estudianteEncontrado.CorreoInstitucional = estudianteActualizar.CorreoInstitucional;
                        estudianteEncontrado.CorreoPersonal = estudianteActualizar.CorreoPersonal;
                        estudianteEncontrado.SemestreQueCursa = estudianteActualizar.SemestreQueCursa;
                        entidadesTutorias.Estudiantes.AddOrUpdate(estudianteEncontrado);
                        estudianteActualizado = entidadesTutorias.SaveChanges() == 1;
                    }
                }

            }
            return estudianteActualizado;
        }
    }
}
