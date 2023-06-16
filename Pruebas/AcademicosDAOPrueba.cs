using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AccesoADatos;
using Dominio;
using FluentAssertions;
using LogicaDelNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas
{
    [TestClass]
    public class AcademicosDAOPrueba
    {
        private AcademicosDAO academicosDAO;
        private Dominio.Academicos academico;
        public AcademicosDAOPrueba()
        {
            this.academicosDAO = new AcademicosDAO();
            this.academico = new Dominio.Academicos();
        }

        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ObtenerAcademicoPorIdAcademico_Exito()
        {
            Dominio.Academicos academicoEsperado = new Dominio.Academicos();
            academicoEsperado.IdAcademico = 1;
            academicoEsperado.Nombre = "Mario Alberto";
            academicoEsperado.ApellidoPaterno = "Hernández";
            academicoEsperado.ApellidoMaterno = "Pérez";
            academicoEsperado.CorreoPersonal = "mariohernandez02@gmail.com";
            academicoEsperado.CorreoInstitucional = "mariohernandez02@uv.mx";
            academico = academicosDAO.ObtenerAcademicoPorIdAcademico(academicoEsperado.IdAcademico);
            academico.Should().BeEquivalentTo(academicoEsperado);
        }

        [TestMethod]
        public void ObtenerAcademicoPorIdAcademico_Fallo_IdAcademicoInexistente()
        {
            Dominio.Academicos academicoEsperado = new Dominio.Academicos();
            academicoEsperado.IdAcademico = -1;
            academico = academicosDAO.ObtenerAcademicoPorIdAcademico(academicoEsperado.IdAcademico);
            academico.Nombre.Should().BeNull();
        }

        [TestMethod]
        public void ObtenerAcademicoPorIdAcademico_Fallo_IdAcademicoInvalido()
        {
            Dominio.Academicos academicoEsperado = new Dominio.Academicos();
            academico = academicosDAO.ObtenerAcademicoPorIdAcademico(academicoEsperado.IdAcademico);
            academico.Nombre.Should().BeNull();
        }

        [TestMethod]
        public void BuscarAcademicoPorNombreYApellidos_Exito()
        {
            academico.Nombre = "Mario Alberto";
            academico.ApellidoMaterno = "Pérez";
            academico.ApellidoPaterno = "Hernández";
            academicosDAO.BuscarAcademicoPorNombreYApellidos(academico).Should().BeTrue();
        }

        [TestMethod]
        public void BuscarAcademicoPorNombreYApellidos_Fallo_NombreAcademicoInexistente()
        {
            academico.Nombre = "EsteNombreNoEsDePersona123";
            academico.ApellidoMaterno = "Pérez";
            academico.ApellidoPaterno = "Hernández";
            academicosDAO.BuscarAcademicoPorNombreYApellidos(academico).Should().BeFalse();
        }

        [TestMethod]
        public void BuscarAcademicoPorNombreYApellidos_Fallo_ApellidoMaternoAcademicoInexistente()
        {
            academico.Nombre = "Mario Alberto";
            academico.ApellidoMaterno = "EsteApellidoNoEsDePersona123";
            academico.ApellidoPaterno = "Hernández";
            academicosDAO.BuscarAcademicoPorNombreYApellidos(academico).Should().BeFalse();
        }

        [TestMethod]
        public void BuscarAcademicoPorNombreYApellidos_Fallo_ApellidoPaternoAcademicoInexistente()
        {
            academico.Nombre = "Mario Alberto";
            academico.ApellidoMaterno = "Pérez";
            academico.ApellidoPaterno = "EsteApellidoNoEsDePersona123";
            academicosDAO.BuscarAcademicoPorNombreYApellidos(academico).Should().BeFalse();
        }


        [TestMethod]
        public void BuscarAcademicoPorNombreYApellidos_Fallo_NombreAcademicoInvalido()
        {
            academico.ApellidoMaterno = "Pérez";
            academico.ApellidoPaterno = "Hernández";
            academicosDAO.BuscarAcademicoPorNombreYApellidos(academico).Should().BeFalse();
        }

        [TestMethod]
        public void BuscarAcademicoPorNombreYApellidos_Fallo_ApellidoMaternoAcademicoInvalido()
        {
            academico.Nombre = "Mario Alberto";
            academico.ApellidoPaterno = "Hernández";
            academicosDAO.BuscarAcademicoPorNombreYApellidos(academico).Should().BeFalse();
        }

        [TestMethod]
        public void BuscarAcademicoPorNombreYApellidos_Fallo_ApellidoPaternoAcademicoInvalido()
        {
            academico.Nombre = "Mario Alberto";
            academico.ApellidoMaterno = "Pérez";
            academicosDAO.BuscarAcademicoPorNombreYApellidos(academico).Should().BeFalse();
        }

        [TestMethod]
        public void RegistrarAcademico_Exito()
        {
            academico.Nombre = "Nombre Prueba";
            academico.ApellidoMaterno = "ApellidoMaternoPrueba";
            academico.ApellidoPaterno = "ApellidoPaternoPrueba";
            academico.CorreoPersonal = "correPersonal@prueba.prueba";
            academico.CorreoInstitucional = "correo@prueba.prueba";
            academicosDAO.RegistrarAcademico(academico).Should().BeTrue();
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.Academicos academicoRegistrado =
                    entidades.Academicos.FirstOrDefault(academicoBuscar => academicoBuscar.Nombre == academico.Nombre);
                entidades.Academicos.Remove(academicoRegistrado);
                entidades.SaveChanges();
            }
        }

        //Para la siguiente prueba es necesario desconectar la conexion con la Base de datos
        //O comentar la linea donde se registra la informacion:
        //entidades.Academicos.Add(academicoRegistrar.ConvertirAcademicoDominioEnAcademicoAccesoADatos());
        [TestMethod]
        public void RegistrarAcademico_Fallo_PerdidaDeConexion()
        {
            academico.Nombre = "Nombre Prueba";
            academico.ApellidoMaterno = "ApellidoMaternoPrueba";
            academico.ApellidoPaterno = "ApellidoPaternoPrueba";
            academico.CorreoPersonal = "correPersonal@prueba.prueba";
            academico.CorreoInstitucional = "correo@prueba.prueba";
            academicosDAO.RegistrarAcademico(academico).Should().BeFalse();
        }

        [TestMethod]
        public void ObtenerIdAcademicoPorNombreYApellidos_Exito()
        {
            int idAcademicoEsperado = 1;
            academico.Nombre = "Mario Alberto";
            academico.ApellidoMaterno = "Pérez";
            academico.ApellidoPaterno = "Hernández";
            academicosDAO.ObtenerIdAcademicoPorNombreYApellidos(academico).Should().Be(idAcademicoEsperado);
        }

        [TestMethod]
        public void ObtenerIdAcademicoPorNombreYApellidos_Fallo_NombreInexistente()
        {
            int idAcademicoEsperado = -1;
            academico.Nombre = "EsteNombreNoEsDePersona123";
            academico.ApellidoMaterno = "Pérez";
            academico.ApellidoPaterno = "Hernández";
            academicosDAO.ObtenerIdAcademicoPorNombreYApellidos(academico).Should().Be(idAcademicoEsperado);
        }

        [TestMethod]
        public void ObtenerIdAcademicoPorNombreYApellidos_Fallo_ApellidoMaternoInexistente()
        {
            int idAcademicoEsperado = -1;
            academico.Nombre = "Mario Alberto";
            academico.ApellidoMaterno = "EsteApellidoNoEsDePersona123";
            academico.ApellidoPaterno = "Hernández";
            academicosDAO.ObtenerIdAcademicoPorNombreYApellidos(academico).Should().Be(idAcademicoEsperado);
        }

        [TestMethod]
        public void ObtenerIdAcademicoPorNombreYApellidos_Fallo_ApellidoPaternoInexistente()
        {
            int idAcademicoEsperado = -1;
            academico.Nombre = "Mario Alberto";
            academico.ApellidoMaterno = "Pérez";
            academico.ApellidoPaterno = "EsteApellidoNoEsDePersona123";
            academicosDAO.ObtenerIdAcademicoPorNombreYApellidos(academico).Should().Be(idAcademicoEsperado);
        }

        [TestMethod]
        public void ObtenerIdAcademicoPorNombreYApellidos_Fallo_NombreInvalido()
        {
            int idAcademicoEsperado = -1;
            academico.ApellidoMaterno = "Pérez";
            academico.ApellidoPaterno = "Hernández";
            academicosDAO.ObtenerIdAcademicoPorNombreYApellidos(academico).Should().Be(idAcademicoEsperado);
        }

        [TestMethod]
        public void ObtenerIdAcademicoPorNombreYApellidos_Fallo_ApellidoMaternoInvalido()
        {
            int idAcademicoEsperado = -1;
            academico.Nombre = "Mario Alberto";
            academico.ApellidoPaterno = "Hernández";
            academicosDAO.ObtenerIdAcademicoPorNombreYApellidos(academico).Should().Be(idAcademicoEsperado);
        }

        [TestMethod]
        public void ObtenerIdAcademicoPorNombreYApellidos_Fallo_ApellidoPaternoInvalido()
        {
            int idAcademicoEsperado = -1;
            academico.Nombre = "Mario Alberto";
            academico.ApellidoMaterno = "Pérez";
            academicosDAO.ObtenerIdAcademicoPorNombreYApellidos(academico).Should().Be(idAcademicoEsperado);
        }

        [TestMethod]
        public void ObtenerTutoresPorIdProgramaEducativo_Exito1()
        {
            int IngenieriaDeSoftware = 1;
            ObservableCollection<Dominio.Academicos> tutoresObtenidos =
                this.academicosDAO.ObtenerTutoresPorIdProgramaEducativo(IngenieriaDeSoftware);

            int cantidadEsperada = 26;
            int cantidadObtenida = tutoresObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);

            academico.IdAcademico = 5;
            academico.Nombre = "MARIA DE LOS ANGELES";
            academico.ApellidoPaterno = "ARENAS";
            academico.ApellidoMaterno = "VALDES";
            academico.CorreoInstitucional = "asdf";
            academico.CorreoPersonal = "asdf";
            academico.NumeroTutorados = 31;

            tutoresObtenidos[3].Should().BeEquivalentTo(academico);
        }

        [TestMethod]
        public void ObtenerTutoresPorIdProgramaEducativo_Exito2()
        {
            int Redes = 2;
            ObservableCollection<Dominio.Academicos> tutoresObtenidos =
                this.academicosDAO.ObtenerTutoresPorIdProgramaEducativo(Redes);

            int cantidadEsperada = 3;
            int cantidadObtenida = tutoresObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);

            academico.IdAcademico = 5;
            academico.Nombre = "MARIA DE LOS ANGELES";
            academico.ApellidoPaterno = "ARENAS";
            academico.ApellidoMaterno = "VALDES";
            academico.CorreoInstitucional = "asdf";
            academico.CorreoPersonal = "asdf";
            academico.NumeroTutorados = 1;

            tutoresObtenidos[2].Should().BeEquivalentTo(academico);
        }

        [TestMethod]
        public void ObtenerTutoresPorIdProgramaEducativo_Fallo_IdProgramaEducativo()
        {
            int ProgramaEducativo = -1;
            ObservableCollection<Dominio.Academicos> tutoresObtenidos =
                this.academicosDAO.ObtenerTutoresPorIdProgramaEducativo(ProgramaEducativo);

            int cantidadEsperada = 0;
            int cantidadObtenida = tutoresObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }

        [TestMethod]
        public void ObtenerTutoresPorIdProgramaEducativo_Fallo_IdProgramaEducativo2()
        {
            int ProgramaEducativo = 9999;
            ObservableCollection<Dominio.Academicos> tutoresObtenidos =
                this.academicosDAO.ObtenerTutoresPorIdProgramaEducativo(ProgramaEducativo);

            int cantidadEsperada = 0;
            int cantidadObtenida = tutoresObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }

        [TestMethod]
        public void ObtenerAcademicos()
        {
            int cantidadEsperada = 2;
            List<Dominio.Academicos> academicosObtenidos = academicosDAO.ObtenerAcademicos();
            int cantidadObtenida = academicosObtenidos.Count;
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }

        [TestMethod]
        public void ObtenerAcademicos_Fallo()
        {
            int cantidadEsperada = 10;
            List<Dominio.Academicos> academicosObtenidos = academicosDAO.ObtenerAcademicos();
            int cantidadObtenida = academicosObtenidos.Count;
            Assert.AreNotEqual(cantidadEsperada, cantidadObtenida);
        }

        //Para la siguiente prueba es necesario desconectar la conexion con la Base de datos
        //O comentar la linea donde se agrega la informaciona la lista:
        //academicosObtenidos.Add(new Dominio.Academicos(academicoEncontrado));;
        [TestMethod]
        public void ObtenerAcademicos_Fallo_PerdidaDeConexion()
        {
            int cantidadEsperada = 0;
            List<Dominio.Academicos> academicosObtenidos = academicosDAO.ObtenerAcademicos();
            academicosObtenidos.Count.Should().Be(cantidadEsperada);
        }

        [TestMethod]
        public void ActualizarAcademico_Exito()
        {
            academico.IdAcademico = 1;
            academico.Nombre = "Mario Alberto";
            academico.ApellidoPaterno = "Hernández";
            academico.ApellidoMaterno = "Pérez";
            academico.CorreoPersonal = "mariohernandez02@gmail.com";
            academico.CorreoInstitucional = "mariohernandez02@uv.mx";
            academicosDAO.ActualizarAcademico(academico).Should().BeTrue();
        }

        [TestMethod]
        public void ActualizarAcademico_Fallo_IdAcademicoInexistente()
        {
            academico.IdAcademico = -1;
            academico.Nombre = "Mario Alberto";
            academico.ApellidoPaterno = "Hernández";
            academico.ApellidoMaterno = "Pérez";
            academico.CorreoPersonal = "mariohernandez02@gmail.com";
            academico.CorreoInstitucional = "mariohernandez02@uv.mx";
            academicosDAO.ActualizarAcademico(academico).Should().BeFalse();
        }

        [TestMethod]
        public void ActualizarAcademico_Fallo_IdAcademicoInvalido()
        {
            academico.Nombre = "Mario Alberto";
            academico.ApellidoPaterno = "Hernández";
            academico.ApellidoMaterno = "Pérez";
            academico.CorreoPersonal = "mariohernandez02@gmail.com";
            academico.CorreoInstitucional = "mariohernandez02@uv.mx";
            academicosDAO.ActualizarAcademico(academico).Should().BeFalse();
        }

        [TestMethod]
        public void ObtenerAcademicosDeEstudiante()
        {
            Dominio.Estudiantes estudiante = new Dominio.Estudiantes();
            estudiante.Matricula = "S18939345";
            academicosDAO.ObtenerAcademicosDeEstudiante(estudiante).Should().NotBeEmpty();
        }

        [TestMethod]
        public void ObtenerAcademicosDeEstudiante_Fallo()
        {
            Dominio.Estudiantes estudiante = new Dominio.Estudiantes();
            estudiante.Matricula = "S1";
            academicosDAO.ObtenerAcademicosDeEstudiante(estudiante).Should().BeEmpty();
        }

        [TestMethod]
        public void BuscarAcademicoPorCorreoInstitucional()
        {
            academico.Nombre = "Mario Alberto";
            academico.ApellidoPaterno = "Hernández";
            academico.ApellidoMaterno = "Pérez";
            academico.CorreoPersonal = "mariohernandez02@gmail.com";
            academico.CorreoInstitucional = "mariohernandez02@uv.mx";
            academicosDAO.BuscarAcademicoPorCorreoInstitucional(academico).Should().NotBeEmpty();
        }

        [TestMethod]
        public void BuscarAcademicoPorCorreoInstitucional_Fallo()
        {
            academico.Nombre = "Mario Alberto";
            academico.ApellidoPaterno = "Hernández";
            academico.ApellidoMaterno = "Pérez";
            academico.CorreoPersonal = "mariohernandez02@gmail.com";
            academico.CorreoInstitucional = "mariohernandez02@uv.m";
            academicosDAO.BuscarAcademicoPorCorreoInstitucional(academico).Should().BeEmpty();
        }
    }
}
