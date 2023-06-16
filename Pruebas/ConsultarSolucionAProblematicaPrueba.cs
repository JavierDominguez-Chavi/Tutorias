using AccesoADatos;
using LogicaDelNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace Pruebas
{

    [TestClass]
    public class ConsultarSolucionAProblematicaPrueba
    {
        private SolucionAProblematicaAcademicaDAO solucionAProblematicaAcademicaDAO = new SolucionAProblematicaAcademicaDAO();
        private TestContext testContext;
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

        [TestMethod]
        public void ConsultarSolucion()
        {
            
            Soluciones solucionEsperada = new Soluciones();

            int  idProblematica = this.solucionAProblematicaAcademicaDAO.ObtenerSolucion(3).IdProblematica;

            AccesoADatos.Soluciones solucionObtenida = new AccesoADatos.Soluciones();

            using (var entidades = new EntidadesTutorias())
            {
                var encontrada = entidades.Soluciones.FirstOrDefault(solucion => solucion.IdSolucion == 3);
                solucionObtenida.IdProblematica = encontrada.IdProblematica;
            }

            Assert.AreEqual(solucionObtenida.IdProblematica, idProblematica);
           
        }

        }


 }

