using AccesoADatos;
using LogicaDelNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using Dominio;
using FluentAssertions;
using FluentAssertions.Specialized;

namespace Pruebas
{

    [TestClass]
    public class ExperienciasEducativasDAOPrueba
    {
        private ExperienciasEducativasDAO experienciasEducativasDAO;
        private Dominio.ExperienciasEducativas experienciaEducativa;

        public ExperienciasEducativasDAOPrueba()
        {
            this.experienciasEducativasDAO = new ExperienciasEducativasDAO();
            this.experienciaEducativa = new Dominio.ExperienciasEducativas();
        }

        private TestContext testContextInstance;

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
        public void ObtenerExperienciasEducativas()
        {
            int tamañoEsperado = 8;
            int tamañoObtenido =
                this.experienciasEducativasDAO.ObtenerExperienciasEducativas().Count;

            Assert.AreEqual(tamañoEsperado, tamañoObtenido);
        }

        [TestMethod]
        public void RegistrarExperienciaEducativa()
        {
            String nombreValido = "THIS IS A TEST";
            Dominio.ExperienciasEducativas experienciaEducativa = new Dominio.ExperienciasEducativas();
            experienciaEducativa.Nombre = nombreValido;
            Boolean resultadoEsperado = true;
            Boolean resultadoObtenido =
                this.experienciasEducativasDAO.RegistrarExperienciaEducativa(experienciaEducativa);
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.ExperienciasEducativas experienciaInsertada =
                    entidades.ExperienciasEducativas.FirstOrDefault(experiencia => experiencia.Nombre == nombreValido);
                entidades.ExperienciasEducativas.Remove(experienciaInsertada);
                entidades.SaveChanges();
            }

            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void RegistrarExperienciaEducativa_Fallo()
        {
            String nombreRepetido = "PROGRAMACION";
            Dominio.ExperienciasEducativas experienciaEducativa = new Dominio.ExperienciasEducativas();
            experienciaEducativa.Nombre = nombreRepetido;
            Boolean resultadoEsperado = true;
            Boolean resultadoObtenido =
                this.experienciasEducativasDAO.RegistrarExperienciaEducativa(experienciaEducativa);


            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        public void ObtenerExperienciaEducativaPorIdExperienciaEducativa_Exito()
        {
            Dominio.ExperienciasEducativas experienciasEducativaEsperada = new Dominio.ExperienciasEducativas();
            experienciasEducativaEsperada.IdExperienciaEducativa = 1;
            experienciasEducativaEsperada.Nombre = "PROGRAMACION";
            experienciaEducativa =
                experienciasEducativasDAO.ObtenerExperienciaEducativaPorIdExperienciaEducativa(experienciasEducativaEsperada.IdExperienciaEducativa);
            experienciaEducativa.Should().BeEquivalentTo(experienciasEducativaEsperada);
        }

        [TestMethod]
        public void ObtenerExperienciaEducativaPorIdExperienciaEducativa_FalloIdExperienciaEducativaInexistente()
        {
            Dominio.ExperienciasEducativas experienciasEducativaEsperada = new Dominio.ExperienciasEducativas();
            experienciasEducativaEsperada.IdExperienciaEducativa = -1;
            experienciaEducativa =
                experienciasEducativasDAO.ObtenerExperienciaEducativaPorIdExperienciaEducativa(experienciasEducativaEsperada.IdExperienciaEducativa);
            experienciaEducativa.Nombre.Should().BeNull();
        }

        [TestMethod]
        public void ObtenerExperienciaEducativaPorIdExperienciaEducativa_FalloIdExperienciaEducativaInvalida()
        {
            Dominio.ExperienciasEducativas experienciasEducativaEsperada = new Dominio.ExperienciasEducativas();
            experienciaEducativa =
                experienciasEducativasDAO.ObtenerExperienciaEducativaPorIdExperienciaEducativa(experienciasEducativaEsperada.IdExperienciaEducativa);
            experienciaEducativa.Nombre.Should().BeNull();
        }

        [TestMethod]
        public void ValidarNombreExperienciaEducativa_Fallo_NombreRepetido()
        {
            Assert.ThrowsException<DbEntityValidationException>(() =>
                this.experienciasEducativasDAO.ValidarNombreExperienciaEducativa("PROGRAMACION")
            );
        }
        [TestMethod]
        public void ValidarNombreExperienciaEducativa_Fallo_NombreVacio()
        {
            try
            {
                this.experienciasEducativasDAO.ValidarNombreExperienciaEducativa("");
            }
            catch (DbEntityValidationException ex)
            {
                Assert.AreEqual("El nombre no puede estar vacío", ex.Message);
            }
        }
        [TestMethod]
        public void ValidarNombreExperienciaEducativa_Fallo_NombreIlegal()
        {
            try
            {
                this.experienciasEducativasDAO.ValidarNombreExperienciaEducativa("----4321234");
            }
            catch (DbEntityValidationException ex)
            {
                Assert.AreEqual("El campo nombre debe ser alfabético.", ex.Message);
            }
        }
        
        [TestMethod]
        public void ModificarExperienciaEducativa()
        {
            Dominio.ExperienciasEducativas eePreModificacion = 
                this.experienciasEducativasDAO.ObtenerExperienciaEducativaPorIdExperienciaEducativa(1);

            string nombreOriginal = eePreModificacion.Nombre;

            string nombreNuevo = "NOMBRE PARA LA PRUEBA LMAO";
            eePreModificacion.Nombre = nombreNuevo;

            bool operacionExitosa = 
                this.experienciasEducativasDAO.ModificarExperienciaEducativa(eePreModificacion);
            Dominio.ExperienciasEducativas eePostModificacion =
                this.experienciasEducativasDAO.ObtenerExperienciaEducativaPorIdExperienciaEducativa(1);
            Assert.IsTrue(operacionExitosa);
            Assert.AreEqual(eePostModificacion.Nombre, nombreNuevo);
            Assert.AreNotEqual(eePostModificacion.Nombre, nombreOriginal);

            eePreModificacion.Nombre = nombreOriginal;
            this.experienciasEducativasDAO.ModificarExperienciaEducativa(eePreModificacion);
        }

        [TestMethod]
        public void BuscarExperienciaEducativas_ExistenExperienciasEducativas()
        {
            string objetoBusqueda = "B";

            ObservableCollection<AccesoADatos.ExperienciasEducativas_Academicos> experienciasEducativasRecuperadas =
                experienciasEducativasDAO.BuscarExperienciaEducativas(objetoBusqueda);

            Assert.IsTrue(experienciasEducativasRecuperadas.Count > 0);
        }

        [TestMethod]
        public void BuscarExperienciaEducativas_NoExistenExperienciasEducativas()
        {
            string objetoBusqueda = "HOLAMUNDO";

            ObservableCollection<AccesoADatos.ExperienciasEducativas_Academicos> experienciasEducativasRecuperadas =
                experienciasEducativasDAO.BuscarExperienciaEducativas(objetoBusqueda);

            Assert.AreEqual(0, experienciasEducativasRecuperadas.Count);
        }

        [TestMethod]
        public void BuscarExperienciaEducativasPorNombre_ExistenExperienciasEducativas()
        {
            string objetoBusqueda = "Bases de Datos";

            ObservableCollection<AccesoADatos.ExperienciasEducativas> experienciasEducativasRecuperadas =
                experienciasEducativasDAO.BuscarExperienciaEducativasPorNombre(objetoBusqueda);

            Assert.IsTrue(experienciasEducativasRecuperadas.Count > 0);
        }

        [TestMethod]
        public void BuscarExperienciaEducativasPorNombre_NoExistenExperienciasEducativas()
        {
            string objetoBusqueda = "HolaMundo";

            ObservableCollection<AccesoADatos.ExperienciasEducativas> experienciasEducativasRecuperadas =
                experienciasEducativasDAO.BuscarExperienciaEducativasPorNombre(objetoBusqueda);

            Assert.AreEqual(0, experienciasEducativasRecuperadas.Count);
        }
    }
}
