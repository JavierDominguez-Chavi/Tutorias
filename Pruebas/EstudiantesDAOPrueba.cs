using LogicaDelNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Text;
using Dominio;
using FluentAssertions;
using AccesoADatos;
using System.Linq;
using System.Collections.ObjectModel;

namespace Pruebas
{
    [TestClass]
    public class EstudiantesDAOPrueba
    {
        private EstudiantesDAO estudiantesDAO;
        private Dominio.Estudiantes estudiante;
        private Dictionary<String, Dominio.Estudiantes> estudiantesReasignados;

        //No cambien esto.
        string[] matriculas = { "30000000", "30000001", "30000002" };

        public EstudiantesDAOPrueba()
        {
            this.estudiantesDAO = new EstudiantesDAO();
            this.estudiante = new Dominio.Estudiantes();
            this.estudiantesReasignados = new Dictionary<String, Dominio.Estudiantes>();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void EstudianteNoExistente_ConRegistro()
        {
            string matricula = "S20000000";
            bool resultadoEstudianteEsperado= false;
            bool resultadoObtenido =
                this.estudiantesDAO.EstudianteNoExistente(matricula);
            Assert.AreEqual(resultadoEstudianteEsperado, resultadoObtenido);
        }
        [TestMethod]
        public void EstudianteNoExistente()
        {
            string matricula = "S20015738";
            bool resultadoEstudianteEsperado = true;
            bool resultadoObtenido =
                this.estudiantesDAO.EstudianteNoExistente(matricula);
            Assert.AreEqual(resultadoEstudianteEsperado, resultadoObtenido);
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void RegistrarEstudiante_Fallo()
        {
            Dominio.Estudiantes estudiantePrueba = new Dominio.Estudiantes();
            estudiantePrueba.Nombre = "Alumno Prueba";
            estudiantePrueba.Matricula = null;
            estudiantePrueba.ApellidoMaterno = "Materno";
            estudiantePrueba.ApellidoPaterno = "Paterno";
            estudiantePrueba.CorreoPersonal = "correoPrueba@gmail.com";
            estudiantePrueba.CorreoInstitucional = "zs20000001@estudiantes.uv.mx";
            estudiantePrueba.EnRiesgo = false;
            estudiantePrueba.IdProgramaEducativo = 1;
            bool resultadoEstudianteRegistrado = false;
            bool resultadoObtenido =
                this.estudiantesDAO.RegistrarEstudiante(estudiantePrueba);
            Assert.AreEqual(resultadoEstudianteRegistrado, resultadoObtenido);
        }
        [TestMethod]
        public void RegistrarEstudiante()
        {
            Dominio.Estudiantes estudiantePrueba = new Dominio.Estudiantes();
            estudiantePrueba.Nombre = "Alumno Prueba";
            estudiantePrueba.Matricula = "S20000001";
            estudiantePrueba.ApellidoMaterno = "Materno";
            estudiantePrueba.ApellidoPaterno = "Paterno";
            estudiantePrueba.CorreoPersonal = "correoPrueba@gmail.com";
            estudiantePrueba.CorreoInstitucional = "zs20000001@estudiantes.uv.mx";
            estudiantePrueba.EnRiesgo = false;
            estudiantePrueba.IdProgramaEducativo = 1;
            bool resultadoEstudianteRegistrado = true;
            bool resultadoObtenido =
                this.estudiantesDAO.RegistrarEstudiante(estudiantePrueba);
            Assert.AreEqual(resultadoEstudianteRegistrado, resultadoObtenido);
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.Estudiantes estudianteRegistrado =
                    entidades.Estudiantes.FirstOrDefault(estudiante => estudiante.Matricula == estudiantePrueba.Matricula);
                entidades.Estudiantes.Remove(estudianteRegistrado);
                entidades.SaveChanges();
            }
        }
        [TestMethod]
        public void ConsultarEstudiante()
        {
            int estudiantesEsperados = 3;
            List<Dominio.Estudiantes> listaObtenida = new List<Dominio.Estudiantes>();
            int idProgramaEducativo = 1;
            listaObtenida = this.estudiantesDAO.ObtenerEstudiantes(idProgramaEducativo);
            int estudiantesObtenidos = listaObtenida.Count;
            Assert.AreEqual(estudiantesEsperados, estudiantesObtenidos);
        }
        [TestMethod]
        public void ConsultarEstudiante_Fallo()
        {
            int estudiantesEsperados = 4;
            List<Dominio.Estudiantes> listaObtenida = new List<Dominio.Estudiantes>();
            int idProgramaEducativo = 3;
            listaObtenida = this.estudiantesDAO.ObtenerEstudiantes(idProgramaEducativo);
            int estudiantesObtenidos = listaObtenida.Count;
            Assert.AreNotEqual(estudiantesEsperados, estudiantesObtenidos);
        }

        [TestMethod]
        public void ObtenerEstudiantePorMatricula_Exito()
        {
            Dominio.Estudiantes estudianteEsperado = new Dominio.Estudiantes();
            estudianteEsperado.Matricula = "S20000000";
            estudianteEsperado.Nombre = "PEDRO";
            estudianteEsperado.ApellidoPaterno = "PEREZ";
            estudianteEsperado.ApellidoMaterno = "PEREZ";
            estudianteEsperado.CorreoPersonal = "PEDRO@GMAIL.COM";
            estudianteEsperado.CorreoInstitucional = "S2000000@ESTUDIANTES.UV.MX";
            estudianteEsperado.EnRiesgo = true;
            estudianteEsperado.SemestreQueCursa = 1;
            estudianteEsperado.IdProgramaEducativo = 1;
            estudianteEsperado.IdAcademico = 1;
            estudiante = estudiantesDAO.ObtenerEstudiantePorMatricula(estudianteEsperado.Matricula);
            estudiante.Should().BeEquivalentTo(estudianteEsperado, options =>
            {
                options.Excluding(info => info.Academicos);
                return options;
            });
        }

        [TestMethod]
        public void ObtenerEstudiantePorMatricula_Fallo_MatriculaInexistente()
        {
            Dominio.Estudiantes estudianteEsperado = new Dominio.Estudiantes();
            estudianteEsperado.Matricula = "S20120000";
            estudiante = estudiantesDAO.ObtenerEstudiantePorMatricula(estudianteEsperado.Matricula);
            estudiante.Matricula.Should().BeNull();


        }

        [TestMethod]
        public void ObtenerEstudiantePorMatricula_Fallo_MatriculaInvalida()
        {
            Dominio.Estudiantes estudianteEsperado = new Dominio.Estudiantes();
            estudiante = estudiantesDAO.ObtenerEstudiantePorMatricula(estudianteEsperado.Matricula);
            estudiante.Matricula.Should().BeNull();
        }

        [TestMethod]
        public void ObtenerTutoradosPorProgramaEducativo_Exito1()
        {
            Dominio.Academicos tutor = new Dominio.Academicos { IdAcademico = 5 };
            int idProgramaEducativo = 1;
            var tutoradosObtenidos = estudiantesDAO.ObtenerTutoradosPorProgramaEducativo(tutor, idProgramaEducativo);

            int cantidadEsperada = 31;
            int cantidadObtenida = tutoradosObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);

            String matriculaEsperada = "20010020";
            String matriculaObtenida = tutoradosObtenidos[0].Matricula;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);

            matriculaEsperada = "S20015697";
            matriculaObtenida = tutoradosObtenidos[29].Matricula;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }

        [TestMethod]
        public void ObtenerTutoradosPorProgramaEducativo_Exito2()
        {
            Dominio.Academicos tutor = new Dominio.Academicos { IdAcademico = 5 };
            int idProgramaEducativo = 2;
            var tutoradosObtenidos = estudiantesDAO.ObtenerTutoradosPorProgramaEducativo(tutor, idProgramaEducativo);

            int cantidadEsperada = 1;
            int cantidadObtenida = tutoradosObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);

            String matriculaEsperada = "S20020099";
            String matriculaObtenida = tutoradosObtenidos[0].Matricula;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }

        [TestMethod]
        public void ObtenerTutoradosPorProgramaEducativo_Exito3()
        {
            Dominio.Academicos tutor = new Dominio.Academicos { IdAcademico = 30 };
            int idProgramaEducativo = 2;
            var tutoradosObtenidos = estudiantesDAO.ObtenerTutoradosPorProgramaEducativo(tutor, idProgramaEducativo);

            int cantidadEsperada = 0;
            int cantidadObtenida = tutoradosObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }

        [TestMethod]
        public void ObtenerTutoradosPorProgramaEducativo_Fallo_Academico()
        {
            Dominio.Academicos tutor = new Dominio.Academicos { IdAcademico = -10 };
            int idProgramaEducativo = 2;
            var tutoradosObtenidos = estudiantesDAO.ObtenerTutoradosPorProgramaEducativo(tutor, idProgramaEducativo);

            int cantidadEsperada = 0;
            int cantidadObtenida = tutoradosObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }

        [TestMethod]
        public void ObtenerTutoradosPorProgramaEducativo_Fallo_ProgramaEducativo()
        {
            Dominio.Academicos tutor = new Dominio.Academicos { IdAcademico = 5 };
            int idProgramaEducativo = -10;
            var tutoradosObtenidos = estudiantesDAO.ObtenerTutoradosPorProgramaEducativo(tutor, idProgramaEducativo);

            int cantidadEsperada = 0;
            int cantidadObtenida = tutoradosObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }

        [TestMethod]
        public void ObtenerTutoradosPorProgramaEducativo_Fallo_AcademicoYProgramaEducativo()
        {
            Dominio.Academicos tutor = new Dominio.Academicos { IdAcademico = -10 };
            int idProgramaEducativo = -10;
            var tutoradosObtenidos = estudiantesDAO.ObtenerTutoradosPorProgramaEducativo(tutor, idProgramaEducativo);

            int cantidadEsperada = 0;
            int cantidadObtenida = tutoradosObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }

        [TestMethod]
        public void ObtenerEstudiantesSinTutorPorProgramaEducativo_Exito1()
        {
            int idProgramaEducativo = 1;
            var tutoradosObtenidos
                = estudiantesDAO.ObtenerEstudiantesSinTutorPorProgramaEducativo(idProgramaEducativo);

            int cantidadEsperada = 2;
            int cantidadObtenida = tutoradosObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);

            this.estudiante.Matricula = "S20020001";
            this.estudiante.Nombre = "ALICIA";
            this.estudiante.ApellidoPaterno = "WIDAR";
            this.estudiante.ApellidoMaterno = "LOERA";
            tutoradosObtenidos[0].Should()
                                 .BeEquivalentTo(this.estudiante, option =>
                                 option.Excluding(e => e.CorreoInstitucional)
                                       .Excluding(e => e.CorreoPersonal)
                                       .Excluding(e => e.EnRiesgo)
                                       .Excluding(e => e.IdAcademico)
                                       .Excluding(e => e.IdProgramaEducativo)
                                       .Excluding(e => e.SemestreQueCursa));
            this.estudiante.Matricula = "S20020002";
            this.estudiante.Nombre = "MAKAULY";
            this.estudiante.ApellidoPaterno = "CULKIN";
            this.estudiante.ApellidoMaterno = "IDK";
            tutoradosObtenidos[1].Should()
                                 .BeEquivalentTo(this.estudiante, option =>
                                 option.Excluding(e => e.CorreoInstitucional)
                                       .Excluding(e => e.CorreoPersonal)
                                       .Excluding(e => e.EnRiesgo)
                                       .Excluding(e => e.IdAcademico)
                                       .Excluding(e => e.IdProgramaEducativo)
                                       .Excluding(e => e.SemestreQueCursa));
        }

        [TestMethod]
        public void ObtenerEstudiantesSinTutorPorProgramaEducativo_Exito2()
        {
            int idProgramaEducativo = 2;
            var tutoradosObtenidos
                = estudiantesDAO.ObtenerEstudiantesSinTutorPorProgramaEducativo(idProgramaEducativo);

            int cantidadEsperada = 1;
            int cantidadObtenida = tutoradosObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);

            this.estudiante.Matricula = "S20020003";
            this.estudiante.Nombre = "BOB";
            this.estudiante.ApellidoPaterno = "MARLEY";
            this.estudiante.ApellidoMaterno = "YEP";
            tutoradosObtenidos[0].Should()
                                 .BeEquivalentTo(this.estudiante, option =>
                                 option.Excluding(e => e.CorreoInstitucional)
                                       .Excluding(e => e.CorreoPersonal)
                                       .Excluding(e => e.EnRiesgo)
                                       .Excluding(e => e.IdAcademico)
                                       .Excluding(e => e.IdProgramaEducativo)
                                       .Excluding(e => e.SemestreQueCursa));
        }

        [TestMethod]
        public void ObtenerEstudiantesSinTutorPorProgramaEducativo_Fallo1()
        {
            int idProgramaEducativo = -10;
            var tutoradosObtenidos
                = estudiantesDAO.ObtenerEstudiantesSinTutorPorProgramaEducativo(idProgramaEducativo);

            int cantidadEsperada = 0;
            int cantidadObtenida = tutoradosObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }

        [TestMethod]
        public void ObtenerEstudiantesSinTutorPorProgramaEducativo_Fallo2()
        {
            int idProgramaEducativo = 9999;
            var tutoradosObtenidos
                = estudiantesDAO.ObtenerEstudiantesSinTutorPorProgramaEducativo(idProgramaEducativo);

            int cantidadEsperada = 0;
            int cantidadObtenida = tutoradosObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }

        [TestMethod]
        public void AsignarTutorAEstudiante_Exito_Singular()
        {
            String matricula = "S19014012";
            this.estudiante = this.estudiantesDAO.ObtenerEstudiantePorMatricula(matricula);
            int idAcademicoAnterior = (int)estudiante.IdAcademico;
            int idAcademicoNuevo = 5;
            this.estudiante.IdAcademico = idAcademicoNuevo;
            this.estudiantesReasignados.Add(matricula, estudiante);

            int asignacionesEsperadas = this.estudiantesReasignados.Count;
            int asignacionesObtenidas = this.estudiantesDAO.AsignarTutorAEstudiantes(this.estudiantesReasignados);
            Assert.AreEqual(asignacionesEsperadas, asignacionesObtenidas);

            this.estudiante = this.estudiantesDAO.ObtenerEstudiantePorMatricula(matricula);
            Assert.AreEqual(idAcademicoNuevo, estudiante.IdAcademico);

            //Regresar al estado original:
            this.estudiantesReasignados[matricula].IdAcademico = idAcademicoAnterior;
            this.estudiantesDAO.AsignarTutorAEstudiantes(this.estudiantesReasignados);
        }

        [TestMethod]
        public void AsignarTutorAEstudiante_Exito_Multiple()
        {
            var estudiantes = this.estudiantesDAO
                                  .ObtenerTutoradosPorProgramaEducativo(
                                     new Dominio.Academicos { IdAcademico = 1 },
                                     1);
            int idAcademicoAnterior = (int)estudiantes[0].IdAcademico;
            int idAcademicoNuevo = 5;
            foreach (Dominio.Estudiantes estudiante in estudiantes)
            {
                estudiante.IdAcademico = idAcademicoNuevo;
                this.estudiantesReasignados.Add(estudiante.Matricula, estudiante);
            }
            int asignacionesEsperadas = this.estudiantesReasignados.Count;
            int asignacionesObtenidas = this.estudiantesDAO.AsignarTutorAEstudiantes(this.estudiantesReasignados);
            Assert.AreEqual(asignacionesEsperadas, asignacionesObtenidas);

            //Regresar al estado original:
            foreach (KeyValuePair<String, Dominio.Estudiantes> registro in estudiantesReasignados)
            {
                registro.Value.IdAcademico = idAcademicoAnterior;
            }
            this.estudiantesDAO.AsignarTutorAEstudiantes(this.estudiantesReasignados);
        }

        [TestMethod]
        public void AsignarTutorAEstudiante_Fallo_Singular_Academico()
        {
            String matricula = "S19014012";
            this.estudiante = this.estudiantesDAO.ObtenerEstudiantePorMatricula(matricula);
            int idAcademicoAnterior = (int)estudiante.IdAcademico;
            int idAcademicoNuevo = 0;
            this.estudiante.IdAcademico = idAcademicoNuevo;
            this.estudiantesReasignados.Add(matricula, estudiante);

            int asignacionesEsperadas = 0;
            int asignacionesObtenidas = this.estudiantesDAO.AsignarTutorAEstudiantes(this.estudiantesReasignados);
            Assert.AreEqual(asignacionesEsperadas, asignacionesObtenidas);
        }

        [TestMethod]
        public void AsignarTutorAEstudiante_Fallo_Singular_Estudiante()
        {
            String matricula = "S19014069";
            this.estudiante.Matricula = matricula;
            this.estudiante.IdAcademico = 1;
            int idAcademicoAnterior = (int)estudiante.IdAcademico;
            int idAcademicoNuevo = 5;
            this.estudiante.IdAcademico = idAcademicoNuevo;
            this.estudiantesReasignados.Add(matricula, estudiante);

            int asignacionesEsperadas = 0;
            int asignacionesObtenidas = this.estudiantesDAO.AsignarTutorAEstudiantes(this.estudiantesReasignados);
            Assert.AreEqual(asignacionesEsperadas, asignacionesObtenidas);
        }

        [TestMethod]
        public void AsignarTutorAEstudiante_Fallo_Multiple()
        {
            var estudiantes = this.estudiantesDAO
                                  .ObtenerTutoradosPorProgramaEducativo(
                                     new Dominio.Academicos { IdAcademico = 1 },
                                     1);
            int idAcademicoAnterior = (int)estudiantes[0].IdAcademico;
            int idAcademicoNuevo = 5;

            estudiantes[2].Matricula = "ERROR";

            foreach (Dominio.Estudiantes estudiante in estudiantes)
            {
                estudiante.IdAcademico = idAcademicoNuevo;
                this.estudiantesReasignados.Add(estudiante.Matricula, estudiante);
            }
            int asignacionesEsperadas = 0;
            int asignacionesObtenidas = this.estudiantesDAO.AsignarTutorAEstudiantes(this.estudiantesReasignados);
            Assert.AreEqual(asignacionesEsperadas, asignacionesObtenidas);

            //Regresar al estado original:
            foreach (KeyValuePair<String, Dominio.Estudiantes> registro in estudiantesReasignados)
            {
                registro.Value.IdAcademico = idAcademicoAnterior;
            }
            this.estudiantesDAO.AsignarTutorAEstudiantes(this.estudiantesReasignados);
        }

        [TestMethod]
        public void AsignarExperienciasEducativasAEstudiante()
        {
            ObservableCollection<int> EperienciasAAsignar = new ObservableCollection<int>();
            EperienciasAAsignar.Add(3028);
            String matriculaEstudiante = "S19014012";
            Boolean resultadoObtenido = estudiantesDAO.AsignarExperienciasEducativasAEstudiante(matriculaEstudiante, EperienciasAAsignar);
            Boolean resultadoEsperado = true;
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        public void AsignarExperienciasEducativasAEstudiante_Error()
        {
            ObservableCollection<int> experienciasAAsignar = new ObservableCollection<int>();
            experienciasAAsignar.Add(1);
            String matriculaEstudiante = "S19014012";
            Boolean resultadoObtenido = estudiantesDAO.AsignarExperienciasEducativasAEstudiante(matriculaEstudiante, experienciasAAsignar);
            Boolean resultadoEsperado = false;
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        /*[TestMethod]
        public void RegistrarEstudiantesPorLote_Exito()
        {
            Dictionary<string, EstudianteImportado> estudiantes = ObtenerEstudiantesPorLote();
            Assert.IsTrue(this.estudiantesDAO.RegistrarEstudiantesPorLote(estudiantes));

            //limpiar
            using (var entidades = new EntidadesTutorias())
            {
                foreach (KeyValuePair<string, EstudianteImportado> estudiante in estudiantes)
                {
                    entidades.Estudiantes.Remove(entidades.Estudiantes.Find($"S{estudiante.Key}"));
                }
                Assert.IsTrue(entidades.SaveChanges() == 3);
            }
        }

        [TestMethod]
        public void RegistrarEstudiantesPorLote_Fallo_NombreVacio()
        {
            Dictionary<string, EstudianteImportado> estudiantes = ObtenerEstudiantesPorLote();
            estudiantes[matriculas[0]].Nombre = null;
            Assert.IsFalse(this.estudiantesDAO.RegistrarEstudiantesPorLote(estudiantes));
        }

        [TestMethod]
        public void RegistrarEstudiantesPorLote_Fallo_NombreNoAlfabetico()
        {
            Dictionary<string, EstudianteImportado> estudiantes = ObtenerEstudiantesPorLote();
            estudiantes[matriculas[1]].Nombre = "as-09df";
            Assert.IsFalse(this.estudiantesDAO.RegistrarEstudiantesPorLote(estudiantes));
        }

        [TestMethod]
        public void RegistrarEstudiantesPorLote_Fallo_ApellidoPaternoNoAlfabetico()
        {
            Dictionary<string, EstudianteImportado> estudiantes = ObtenerEstudiantesPorLote();
            estudiantes[matriculas[2]].ApellidoPaterno = "as-09df";
            Assert.IsFalse(this.estudiantesDAO.RegistrarEstudiantesPorLote(estudiantes));
        }

        [TestMethod]
        public void RegistrarEstudiantesPorLote_Fallo_ApellidoPaternoVacio()
        {
            Dictionary<string, EstudianteImportado> estudiantes = ObtenerEstudiantesPorLote();
            estudiantes[matriculas[2]].ApellidoPaterno = "";
            Assert.IsFalse(this.estudiantesDAO.RegistrarEstudiantesPorLote(estudiantes));
        }

        [TestMethod]
        public void RegistrarEstudiantesPorLote_Fallo_ApellidoMaternoNoAlfabetico()
        {
            Dictionary<string, EstudianteImportado> estudiantes = ObtenerEstudiantesPorLote();
            estudiantes[matriculas[0]].ApellidoMaterno = "as-09df";
            Assert.IsFalse(this.estudiantesDAO.RegistrarEstudiantesPorLote(estudiantes));
        }

        [TestMethod]
        public void RegistrarEstudiantesPorLote_Fallo_ApellidoMaternoVacio()
        {
            Dictionary<string, EstudianteImportado> estudiantes = ObtenerEstudiantesPorLote();
            estudiantes[matriculas[0]].ApellidoMaterno = "     ";
            Assert.IsFalse(this.estudiantesDAO.RegistrarEstudiantesPorLote(estudiantes));
        }

        [TestMethod]
        public void RegistrarEstudiantesPorLote_Fallo_CorreoVacio()
        {
            Dictionary<string, EstudianteImportado> estudiantes = ObtenerEstudiantesPorLote();
            estudiantes[matriculas[2]].CorreoPersonal = "     ";
            Assert.IsFalse(this.estudiantesDAO.RegistrarEstudiantesPorLote(estudiantes));
        }

        [TestMethod]
        public void RegistrarEstudiantesPorLote_Fallo_CorreoInvalido()
        {
            Dictionary<string, EstudianteImportado> estudiantes = ObtenerEstudiantesPorLote();
            estudiantes[matriculas[2]].CorreoPersonal = "asdf@asdf-com";
            Assert.IsFalse(this.estudiantesDAO.RegistrarEstudiantesPorLote(estudiantes));
        }

        [TestMethod]
        public void RegistrarEstudiantesPorLote_Fallo_MatriculaVacia()
        {
            Dictionary<string, EstudianteImportado> estudiantes = ObtenerEstudiantesPorLote();
            estudiantes[matriculas[1]].Matricula = null;
            Assert.IsFalse(this.estudiantesDAO.RegistrarEstudiantesPorLote(estudiantes));
        }

        [TestMethod]
        public void RegistrarEstudiantesPorLote_Fallo_MatriculaInvalida()
        {
            Dictionary<string, EstudianteImportado> estudiantes = ObtenerEstudiantesPorLote();
            estudiantes[matriculas[0]].Matricula = "S20015745";
            Assert.IsFalse(this.estudiantesDAO.RegistrarEstudiantesPorLote(estudiantes));
        }
        */
        private Dictionary<string, EstudianteImportado> ObtenerEstudiantesPorLote()
        {
            return new Dictionary<string, EstudianteImportado>()
            {
                { matriculas[0], new EstudianteImportado{ Matricula = matriculas[0],
                                                          Nombre = "Nombre",
                                                          ApellidoPaterno = "ApellidoPaterno",
                                                          ApellidoMaterno = "ApellidoMaterno",
                                                          CorreoPersonal = "Correo@Email.com",
                                                          idProgramaEducativo = 1} },
                { matriculas[1], new EstudianteImportado{ Matricula = matriculas[1],
                                                          Nombre = "Nombre",
                                                          ApellidoPaterno = "ApellidoPaterno",
                                                          ApellidoMaterno = "ApellidoMaterno",
                                                          CorreoPersonal = "Correo@Email.com",
                                                          idProgramaEducativo = 1} },
                { matriculas[2], new EstudianteImportado{ Matricula = matriculas[2],
                                                          Nombre = "Nombre",
                                                          ApellidoPaterno = "ApellidoPaterno",
                                                          ApellidoMaterno = "ApellidoMaterno",
                                                          CorreoPersonal = "Correo@Email.com",
                                                          idProgramaEducativo = 1} },
            };
        }
        [TestMethod]
        public void ObtenerEstudiantesPorIdAcademico_Exito()
        {
            int valorEsperado = 2;
            int idAcademico = 1;
            ObservableCollection<Dominio.Estudiantes> estudiantesObtenidos = estudiantesDAO.ObtenerEstudiantesPorIdAcademico(idAcademico);
            int cantidadObtenida = estudiantesObtenidos.Count();
            Assert.AreEqual(valorEsperado, cantidadObtenida);
        }
        [TestMethod]
        public void ObtenerEstudiantesPorIdAcademico_Vacio()
        {
            int valorEsperado = 0;
            int idAcademico = 9;
            ObservableCollection<Dominio.Estudiantes> estudiantesObtenidos = estudiantesDAO.ObtenerEstudiantesPorIdAcademico(idAcademico);
            int cantidadObtenida = estudiantesObtenidos.Count();
            Assert.AreEqual(valorEsperado, cantidadObtenida);
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Exito()
        {
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeTrue();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_MatriculaInexistente()
        {
            estudiante.Matricula = "S00015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_MatriculaInvalida()
        {
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_NombreInexistente()
        {
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "Dummie";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_NombreInvalido()
        {
            estudiante.Matricula = "S20015697";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_ApellidoMaternoInexistente()
        {
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Dummie";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_ApellidoMaternoInvalido()
        {
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_ApellidoPaternoInexistente()
        {
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Dummie";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_ApellidoPaternoInvalido()
        {
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_CorreoPersonalInexistente()
        {
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "Dummie.gob";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_CorreoPersonalInvalido()
        {
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_CorreoInstitucionalInexistente()
        {
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "Dummie@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_CorreoInstitucionalInvalido()
        {
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_SemestreInexistente()
        {
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.SemestreQueCursa = 99;
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ValidarEstudianteComoNuevo_Fallo_SemestreInvalido()
        {
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiantesDAO.ValidarEstudianteComoNuevo(estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ActualizarEstudianteConMatricula_Exito()
        {
            String matricula = "S20015696";
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.EnRiesgo = false;
            estudiante.IdProgramaEducativo = 1;
            estudiante.IdAcademico = 2;
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ActualizarEstudianteConMatricula(matricula, estudiante).Should().BeTrue();
        }

        [TestMethod]
        public void ActualizarEstudianteConMatricula_Fallo_MatriculaInexistente()
        {
            String matricula = "S00005696";
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.EnRiesgo = false;
            estudiante.IdProgramaEducativo = 1;
            estudiante.IdAcademico = 2;
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ActualizarEstudianteConMatricula(matricula, estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ActualizarEstudianteConMatricula_Fallo_MatriculaInvalida()
        {
            String matricula = null;
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.EnRiesgo = false;
            estudiante.IdProgramaEducativo = 1;
            estudiante.IdAcademico = 2;
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ActualizarEstudianteConMatricula(matricula, estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ActualizarEstudianteConMatricula_Fallo_EstudianteInvalido()
        {
            String matricula = "S20015697";
            estudiante = null;
            estudiantesDAO.ActualizarEstudianteConMatricula(matricula, estudiante).Should().BeFalse();
        }

        //Desmarcar la linea comentada en caso de que el parametro ya exista en la BD.
        [TestMethod]
        public void ActualizarEstudiante_Exito()
        {
            String matricula = "S20015697";
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            //.ApellidoMaterno = "Restrepo";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.EnRiesgo = false;
            estudiante.IdProgramaEducativo = 1;
            estudiante.IdAcademico = 2;
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ActualizarEstudiante(matricula, estudiante).Should().BeTrue();
        }

        [TestMethod]
        public void ActualizarEstudiante_Fallo_MatriculaInexistente()
        {
            String matricula = "S00005697";
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.EnRiesgo = false;
            estudiante.IdProgramaEducativo = 1;
            estudiante.IdAcademico = 2;
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ActualizarEstudiante(matricula, estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ActualizarEstudiante_Fallo_MatriculaInvalida()
        {
            String matricula = null;
            estudiante.Matricula = "S20015697";
            estudiante.Nombre = "José Javier";
            estudiante.ApellidoMaterno = "Carmona";
            estudiante.ApellidoPaterno = "Domínguez";
            estudiante.CorreoPersonal = "javier@gmail.com";
            estudiante.CorreoInstitucional = "zS20015697@estudiantes.uv.mx";
            estudiante.EnRiesgo = false;
            estudiante.IdProgramaEducativo = 1;
            estudiante.IdAcademico = 2;
            estudiante.SemestreQueCursa = 6;
            estudiantesDAO.ActualizarEstudiante(matricula, estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ActualizarEstudiante_Fallo_EstudianteInvalido()
        {
            String matricula = "S20015696";
            estudiante = null;
            estudiantesDAO.ActualizarEstudianteConMatricula(matricula, estudiante).Should().BeFalse();
        }

        [TestMethod]
        public void ObtenerMatriculasEn_Exito()
        {
            HashSet<string> matriculas = new HashSet<string>()
            {
                "S66666661",
                "S66666662",
                "S66666666"
            };
            HashSet<string> resultadoObtenido = this.estudiantesDAO.ObtenerMatriculasEn(matriculas);
            Assert.AreEqual(resultadoObtenido.Count, matriculas.Count);
            for (int i = 0; i<matriculas.Count; i++)
            {
                Assert.AreEqual(matriculas.ElementAt(i), resultadoObtenido.ElementAt(i));
            }
        }

        [TestMethod]
        public void ObtenerMatriculasEn_Fallo()
        {
            HashSet<string> matriculas = new HashSet<string>()
            {
                "S6666666dsaf3",
                "S666666",
                "S66666661i"
            };
            HashSet<string> resultadoObtenido = this.estudiantesDAO.ObtenerMatriculasEn(matriculas);
            Assert.IsTrue(resultadoObtenido.Count == 0);
        }

        [TestMethod]
        public void ObtenerMatriculasEn_Fallo1()
        {
            HashSet<string> matriculas = new HashSet<string>()
            {
                "S66666661",
                "S66666662",
                "S66666663"
            };
            HashSet<string> resultadoObtenido = this.estudiantesDAO.ObtenerMatriculasEn(matriculas);
            Assert.AreNotEqual(resultadoObtenido.Count, matriculas.Count);

            Assert.AreEqual(matriculas.ElementAt(0), resultadoObtenido.ElementAt(0));
            Assert.AreEqual(matriculas.ElementAt(1), resultadoObtenido.ElementAt(1));
            Assert.IsFalse(resultadoObtenido.Contains(matriculas.ElementAt(2)));
            
        }

        [TestMethod]
        public void ObtenerMatriculasActivasPorProgramaEducativo_Exito()
        {
            int programaEducativo = 1;
            HashSet<string> resultadoObtenido = 
                this.estudiantesDAO.ObtenerMatriculasActivasPorProgramaEducativo();
            Assert.AreEqual(resultadoObtenido.Count, 70);
        }

        [TestMethod]
        public void ObtenerMatriculasActivasPorProgramaEducativo_Exito2()
        {
            int programaEducativo = 2;
            HashSet<string> resultadoObtenido =
                this.estudiantesDAO.ObtenerMatriculasActivasPorProgramaEducativo();
            Assert.AreEqual(resultadoObtenido.Count, 0);
        }

        [TestMethod]
        public void ObtenerEstudiantes_Exitoso()
        {
            string objetoBusqueda = "S";
            var resultadoObtenido = this.estudiantesDAO.ObtenerEstudiantes(objetoBusqueda);

            Assert.IsNotNull(resultadoObtenido);
            Assert.IsTrue(resultadoObtenido.Count > 0);
        }

        [TestMethod]
        public void ObtenerEstudiantes_SinResultados()
        {
            string objetoBusqueda = "jj";
            var resultadoObtenido = this.estudiantesDAO.ObtenerEstudiantes(objetoBusqueda);

            Assert.IsNotNull(resultadoObtenido);
            Assert.IsTrue(resultadoObtenido.Count == 0);
        }

        [TestMethod]
        public void GetExperienciasEducativasDeEstudiante_Exitoso()
        {
            string matricula = "S18939345";
            var resultadoObtenido = this.estudiantesDAO.GetExperienciasEducativasDeEstudiante(matricula);

            Assert.IsNotNull(resultadoObtenido);
            Assert.IsTrue(resultadoObtenido.Count > 0);
        }

        [TestMethod]
        public void GetExperienciasEducativasDeEstudiante_SinResultados()
        {
            string matricula = "S189";
            var resultadoObtenido = this.estudiantesDAO.GetExperienciasEducativasDeEstudiante(matricula);

            Assert.IsNotNull(resultadoObtenido);
            Assert.IsTrue(resultadoObtenido.Count == 0);
        }

        [TestMethod]
        public void AsignarExperienciasEducativasAEstudiante_Exitoso()
        {
            string matricula = "S18939345";
            ObservableCollection<int> experienciasAAsignar = new ObservableCollection<int>() 
            {
                1,
                2,
                3
            };

            var resultadoObtenido = this.estudiantesDAO.AsignarExperienciasEducativasAEstudiante(matricula, experienciasAAsignar);

            Assert.IsTrue(resultadoObtenido);
        }

        [TestMethod]
        public void AsignarExperienciasEducativasAEstudiante_SinExperiencias()
        {
            string matricula = "S18939345";
            ObservableCollection<int> experienciasAAsignar = new ObservableCollection<int>();

            var resultadoObtenido = this.estudiantesDAO.AsignarExperienciasEducativasAEstudiante(matricula, experienciasAAsignar);

            Assert.IsFalse(resultadoObtenido);
        }


    }

}
