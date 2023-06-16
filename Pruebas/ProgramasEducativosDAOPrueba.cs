using AccesoADatos;
using LogicaDelNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Dominio;
using FluentAssertions;
using System.Collections.Generic;

namespace Pruebas
{
    [TestClass]
    public class ProgramasEducativosDAOPrueba
    {
        private ProgramasEducativosDAO programasEducativosDAO;
        private static string PROGRAMA_EDUCATIVO_EXISTENTE = "INGENIERIA DE SOFTWARE";
        private static int ID_PROGRAMA_EDUCATIVO_EXISTENTE = 1;
        private static string PROGRAMA_EDUCATIVO_INEXISTENTE = "INGENIERIA EN ALIMENTOS";
        public ProgramasEducativosDAOPrueba()
        {
            programasEducativosDAO = new ProgramasEducativosDAO();
        }
        [TestMethod]
        public void BuscarProgramaEducativoExistentePrueba_Existe()
        {
            bool resultadoObtenido = programasEducativosDAO.BuscarProgramaEducativoExistente(PROGRAMA_EDUCATIVO_EXISTENTE);
            Assert.IsTrue(resultadoObtenido);
        }

        [TestMethod]
        public void BuscarProgramaEducativoExistentePrueba_NoExiste()
        {
            bool resultadoObtenido = programasEducativosDAO.BuscarProgramaEducativoExistente(PROGRAMA_EDUCATIVO_INEXISTENTE);
            Assert.IsFalse(resultadoObtenido);
        }

        [TestMethod]
        public void RegistrarProgramaEducativoPrueba_Exitoso()
        {
            Dominio.ProgramasEducativos programaEducativo = new Dominio.ProgramasEducativos();
            programaEducativo.NombreProgramaEducativo = PROGRAMA_EDUCATIVO_INEXISTENTE;
            Boolean resultadoObtenido = programasEducativosDAO.RegistrarProgramaEducativo(programaEducativo);
            using(var entidades = new EntidadesTutorias())
            {
                AccesoADatos.ProgramasEducativos programaEducativoInsertado = entidades.ProgramasEducativos.FirstOrDefault(pE => pE.NombreProgramaEducativo == PROGRAMA_EDUCATIVO_INEXISTENTE);
                entidades.ProgramasEducativos.Remove(programaEducativoInsertado);
                entidades.SaveChanges();
            }
            Assert.IsTrue(resultadoObtenido);
        }

        [TestMethod]
        public void ObtenerProgramasEducativos_Exito()
        {
            int numeroProgramasEducativosEsperados = 2;
            List<Dominio.ProgramasEducativos> programasEducativos = new List<Dominio.ProgramasEducativos>();
            programasEducativos = programasEducativosDAO.ObtenerProgramasEducativos();
            programasEducativos.Count.Should().Be(numeroProgramasEducativosEsperados);
        }

        //Para la siguiente prueba es necesario desconectar la conexion con la Base de datos
        //O comentar la linea donde se recupera la informacion: programasEducativosEncontrados = _entidades.ProgramasEducativos.ToList();
        [TestMethod]
        public void ObtenerProgramasEducativos_Fallo_PerdidaConexion()
        {
            int numeroProgramasEducativosEsperados = 0;
            List<Dominio.ProgramasEducativos> programasEducativos = new List<Dominio.ProgramasEducativos>();
            programasEducativos = programasEducativosDAO.ObtenerProgramasEducativos();
            programasEducativos.Count.Should().Be(numeroProgramasEducativosEsperados);
        }
        [TestMethod]
        public void ModificarProgramaEducativoPrueba_Exitoso()
        {
            Dominio.ProgramasEducativos programaEducativoAModificar = new Dominio.ProgramasEducativos();
            programaEducativoAModificar.NombreProgramaEducativo = PROGRAMA_EDUCATIVO_INEXISTENTE;
            programaEducativoAModificar.IdProgramaEducativo = ID_PROGRAMA_EDUCATIVO_EXISTENTE;
            programasEducativosDAO.ModificarProgramaEducativo(programaEducativoAModificar);
            string resultadoObtenido = "";
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.ProgramasEducativos programaEducativoEncontrado = entidades.ProgramasEducativos.FirstOrDefault(pe => pe.IdProgramaEducativo == programaEducativoAModificar.IdProgramaEducativo);
                resultadoObtenido = programaEducativoEncontrado.NombreProgramaEducativo;
            }
            Assert.AreEqual(resultadoObtenido, programaEducativoAModificar.NombreProgramaEducativo);
            programaEducativoAModificar.NombreProgramaEducativo = PROGRAMA_EDUCATIVO_EXISTENTE;
            programasEducativosDAO.ModificarProgramaEducativo(programaEducativoAModificar);
        }
        [TestMethod]
        public void ModificarProgramaEducativoPrueba_Fallido_IdInexistente()
        {
            Dominio.ProgramasEducativos programaEducativoAModificar = new Dominio.ProgramasEducativos();
            programaEducativoAModificar.NombreProgramaEducativo = PROGRAMA_EDUCATIVO_EXISTENTE;
            programaEducativoAModificar.IdProgramaEducativo = ID_PROGRAMA_EDUCATIVO_EXISTENTE-10;
            bool resultadoObtenido = programasEducativosDAO.ModificarProgramaEducativo(programaEducativoAModificar);
            Assert.IsFalse(resultadoObtenido);
        }
        [TestMethod]
        public void ObtenerProgramaEducativoPorIdProgramaEducativoExitoso()
        {
            string NOMBRE_PROGRAMA_EDUCATIVO = "INGENIERIA DE SOFTWARE";
            int ID_PROGRAMA_EDUCATIVO = 1;
            ProgramasEducativosDAO programasEducativosDAO = new ProgramasEducativosDAO();
            Dominio.ProgramasEducativos programaEducativoObtenido = programasEducativosDAO.ObtenerProgramaEducativoPorIdProgramaEducativo(ID_PROGRAMA_EDUCATIVO);
            Assert.AreEqual(programaEducativoObtenido.NombreProgramaEducativo, NOMBRE_PROGRAMA_EDUCATIVO);
        }
        [TestMethod]
        public void ObtenerProgramaEducativoPorIdProgramaEducativoNoExistente()
        {
            int ID_PROGRAMA_EDUCATIVO = -1;
            ProgramasEducativosDAO programasEducativosDAO = new ProgramasEducativosDAO();
            Dominio.ProgramasEducativos programaEducativoObtenido = programasEducativosDAO.ObtenerProgramaEducativoPorIdProgramaEducativo(ID_PROGRAMA_EDUCATIVO);
            Assert.IsNull(programaEducativoObtenido);
        }
    }
}
