using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AccesoADatos;
using LogicaDelNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas
{
    [TestClass]
    public class SolucionAProblematicaAcademicaDAOPrueba
    {
        private SolucionAProblematicaAcademicaDAO solucionesDAO;
        public SolucionAProblematicaAcademicaDAOPrueba()
        {
            this.solucionesDAO = new SolucionAProblematicaAcademicaDAO();
        }

        [TestMethod]
        public void RegistrarSolucion()
        {
            Dominio.Soluciones solucion = new Dominio.Soluciones();
            solucion.Fecha = DateTime.Parse(DateTime.Now.ToShortDateString());
            solucion.Descripcion = "PRUEBA";
            solucion.IdAcademico = 4;
            solucion.IdProblematica = 1;
            Boolean resultadoEsperado = true;
            Boolean resultadoObtenido =
                this.solucionesDAO.RegistrarSolucion(solucion);
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.Soluciones solucionInsertada =
                    entidades.Soluciones.FirstOrDefault(solucionBD => solucionBD.IdProblematica == solucion.IdProblematica);
                entidades.Soluciones.Remove(solucionInsertada);
                entidades.SaveChanges();
            }
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }
        [TestMethod]
        public void BuscarSolucionPorIdProblematica()
        {
            int idProblematicaConSolucionExistente = 1;
            Boolean resultadoEsperado = true;
            Boolean resultadoObtenido = this.solucionesDAO.BuscarSolucionPorIdProblematica(idProblematicaConSolucionExistente);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        public void ObtenerSolucion()
        {
            int idProblematica = 7;
            int IdSolucionEsperada = 1;
            int IdSolucionObtenida = this.solucionesDAO.ObtenerSolucion(idProblematica).IdSolucion;
            Assert.AreEqual(IdSolucionEsperada, IdSolucionObtenida);
        }

        [TestMethod]
        public void ObtenerSolucion_Fallo()
        {
            int idProblematica = 8;
            int IdSolucionEsperada = 1;
            var solucionObtenida = this.solucionesDAO.ObtenerSolucion(idProblematica);
            Assert.AreEqual(null, solucionObtenida);
        }

        [TestMethod]
        public void modificarSolucion()
        {
            var Solucion = new AccesoADatos.Soluciones();
            Solucion.IdSolucion = 1;
            Solucion.IdAcademico = 1;
            Solucion.Fecha = DateTime.Parse("2023-03-29");
            Solucion.Descripcion = "Pruebas";
            Solucion.IdProblematica = 7;
            Boolean resultadoObtenido = this.solucionesDAO.ModificarSolucion(Solucion);
            Boolean resultadoEsperado = true;
            Solucion.Descripcion = "Se platico con el profesor y se llego con un acuerdo, en que conciste que dara sus clases en linea cuando no le sea posible llegar";
            this.solucionesDAO.ModificarSolucion(Solucion);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        public void modificarSolucion_SinCambios()
        {
            var Solucion = new AccesoADatos.Soluciones();
            Solucion.IdSolucion = 1;
            Solucion.IdAcademico = 1;
            Solucion.Fecha = DateTime.Parse("2023-03-29");
            Solucion.Descripcion = "Se platico con el profesor y se llego con un acuerdo, en que conciste que dara sus clases en linea cuando no le sea posible llegar";
            Solucion.IdProblematica = 7;
            Boolean resultadoObtenido = this.solucionesDAO.ModificarSolucion(Solucion);
            Boolean resultadoEsperado = false;
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

    }
}
