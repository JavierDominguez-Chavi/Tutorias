using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogicaDelNegocio;
using Dominio;
using FluentAssertions;
using System.Collections.ObjectModel;
using AccesoADatos;
using System.Linq;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;

namespace Pruebas
{
    [TestClass]
    public class HorarioDAOPrueba
    {
        private readonly HorarioDAO horarioDAO = new HorarioDAO();

        [TestMethod]
        public void ObtenerHorariosPorMatriculaYPeriodoEscolar_Exito1()
        {
            var horarios =
                this.horarioDAO.ObtenerHorariosPorMatriculaYPeriodoEscolar("20010075", 2);
            Assert.IsTrue(horarios.Count == 2);

        }
        [TestMethod]
        public void ObtenerHorariosPorIdTutoriaYMatricula_Exito()
        {
            ObservableCollection<Dominio.Estudiantes> estudiantesABuscar = new ObservableCollection<Dominio.Estudiantes>();
            ObservableCollection<Dominio.Horarios> horariosObtenidos = new ObservableCollection<Dominio.Horarios>();
            Dominio.Estudiantes primerEstudiante = new Dominio.Estudiantes();
            Dominio.Estudiantes segundoEstudiante = new Dominio.Estudiantes();
            primerEstudiante.Matricula = "S19014012";
            segundoEstudiante.Matricula = "S20000000";
            estudiantesABuscar.Add(primerEstudiante);
            estudiantesABuscar.Add(segundoEstudiante);
            int idTutoria = 1;
            int resultadoEsperado = 0;
            horariosObtenidos = horarioDAO.ObtenerHorariosDeTutoriaPorIdTutoriaYMatricula(estudiantesABuscar, idTutoria);
            int cantidadObtenida = horariosObtenidos.Count;
            Assert.AreEqual(resultadoEsperado, cantidadObtenida);
        }
        [TestMethod]
        public void RegistrarHorarioDeSesionDeTutoria_Exito()
        {
            Dominio.Horarios horarios = new Dominio.Horarios();
            TimeSpan horarioNuevo = new TimeSpan(1,1,1);
            horarios.HoraTutoria = horarioNuevo;
            horarios.Matricula = "S19014012";
            horarios.IdTutoriaAcademica = 1;
            horarios.Asistencia = false;
            horarios.Riesgo = false;
            bool resultadoEsperado = true;
            bool resultadoObtenido = horarioDAO.RegistrarHorarioDeSesionDeTutoria(horarios);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.Horarios horarioRegistrado =
                    entidades.Horarios.FirstOrDefault(horario => horario.Matricula == horarios.Matricula && horario.IdTutoriaAcademica == horarios.IdTutoriaAcademica);
                entidades.Horarios.Remove(horarioRegistrado);
                entidades.SaveChanges();
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void RegistrarHorarioSesionDeTutoria_Fallo()
        {
            Dominio.Horarios horarios = new Dominio.Horarios();
            TimeSpan horarioNuevo = new TimeSpan(1, 1, 1);
            horarios.HoraTutoria = horarioNuevo;
            horarios.Matricula = "00000000";
            horarios.IdTutoriaAcademica = 32;
            horarios.Asistencia = false;
            horarios.Riesgo = false;
            bool resultadoEsperado = true;
            bool resultadoObtenido = horarioDAO.RegistrarHorarioDeSesionDeTutoria(horarios);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }
        [TestMethod]
        public void ObtenerHorariosDeTutoriaPorIdTutoriaYMatricula_Exito()
        {
            int cantidadEsperada = 4;
            string matricula = "S19014012";
            Dominio.Estudiantes primerEstudiante = new Dominio.Estudiantes();
            primerEstudiante.Matricula = matricula;
            Dominio.Estudiantes segundoEstudiante = new Dominio.Estudiantes();
            segundoEstudiante.Matricula = matricula;
            Dominio.Estudiantes tercerEstudiante = new Dominio.Estudiantes();
            tercerEstudiante.Matricula = matricula;
            Dominio.Estudiantes cuartoEstudiante = new Dominio.Estudiantes();
            cuartoEstudiante.Matricula = matricula;
            ObservableCollection<Dominio.Estudiantes> estudiantesAConsultar = new ObservableCollection<Dominio.Estudiantes>();
            estudiantesAConsultar.Add(primerEstudiante);
            estudiantesAConsultar.Add(segundoEstudiante);
            estudiantesAConsultar.Add(tercerEstudiante);
            estudiantesAConsultar.Add(cuartoEstudiante);
            int idTutoria = 3;
            ObservableCollection<Dominio.Horarios> horariosObtenidos = horarioDAO.ObtenerHorariosDeTutoriaPorIdTutoriaYMatricula(estudiantesAConsultar, idTutoria);
            int cantidadObtenida = horariosObtenidos.Count();
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }
        [TestMethod]
        public void ObtenerHorariosDeTutoriaPorIdTutoriaYMatricula_EstudiantesNoExistentes()
        {
            int cantidadEsperada = 0;
            string matricula = "000000000";
            Dominio.Estudiantes primerEstudiante = new Dominio.Estudiantes();
            primerEstudiante.Matricula = matricula;
            Dominio.Estudiantes segundoEstudiante = new Dominio.Estudiantes();
            segundoEstudiante.Matricula = matricula;
            Dominio.Estudiantes tercerEstudiante = new Dominio.Estudiantes();
            tercerEstudiante.Matricula = matricula;
            Dominio.Estudiantes cuartoEstudiante = new Dominio.Estudiantes();
            cuartoEstudiante.Matricula = matricula;
            ObservableCollection<Dominio.Estudiantes> estudiantesAConsultar = new ObservableCollection<Dominio.Estudiantes>();
            estudiantesAConsultar.Add(primerEstudiante);
            estudiantesAConsultar.Add(segundoEstudiante);
            estudiantesAConsultar.Add(tercerEstudiante);
            estudiantesAConsultar.Add(cuartoEstudiante);
            int idTutoria = 3;
            ObservableCollection<Dominio.Horarios> horariosObtenidos = horarioDAO.ObtenerHorariosDeTutoriaPorIdTutoriaYMatricula(estudiantesAConsultar, idTutoria);
            int cantidadObtenida = horariosObtenidos.Count();
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }
        [TestMethod]
        public void ObtenerHorariosDeTutoriaPorIdTutoriaYMatricula_IdTutoriaNoExistente()
        {
            int cantidadEsperada = 0;
            string matricula = "S19014012";
            Dominio.Estudiantes primerEstudiante = new Dominio.Estudiantes();
            primerEstudiante.Matricula = matricula;
            Dominio.Estudiantes segundoEstudiante = new Dominio.Estudiantes();
            segundoEstudiante.Matricula = matricula;
            Dominio.Estudiantes tercerEstudiante = new Dominio.Estudiantes();
            tercerEstudiante.Matricula = matricula;
            Dominio.Estudiantes cuartoEstudiante = new Dominio.Estudiantes();
            cuartoEstudiante.Matricula = matricula;
            ObservableCollection<Dominio.Estudiantes> estudiantesAConsultar = new ObservableCollection<Dominio.Estudiantes>();
            estudiantesAConsultar.Add(primerEstudiante);
            estudiantesAConsultar.Add(segundoEstudiante);
            estudiantesAConsultar.Add(tercerEstudiante);
            estudiantesAConsultar.Add(cuartoEstudiante);
            int idTutoria = 34;
            ObservableCollection<Dominio.Horarios> horariosObtenidos = horarioDAO.ObtenerHorariosDeTutoriaPorIdTutoriaYMatricula(estudiantesAConsultar, idTutoria);
            int cantidadObtenida = horariosObtenidos.Count();
            Assert.AreEqual(cantidadEsperada, cantidadObtenida);
        }
        [TestMethod]
        public void ModificarHorarioDeSesionDeTutoriaPorMatriculaYIdTutoria_Exito()
        {
            Dominio.Horarios horarios = new Dominio.Horarios();
            TimeSpan horarioNuevo = new TimeSpan(12, 12, 1);
            horarios.HoraTutoria = horarioNuevo;
            horarios.Matricula = "S19014012";
            horarios.IdTutoriaAcademica = 5;
            horarios.Asistencia = false;
            horarios.Riesgo = false;
            bool resultadoEsperado = true;
            bool resultadoObtenido = horarioDAO.ModificarHorarioDeSesionDeTutoriaPorMatriculaYIdTutoria(horarios);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }
        [TestMethod]
        public void ModificarHorarioDeSesionDeTutoriaPorMatriculaYIdTutoria_MatriculaNoExistente()
        {
            Dominio.Horarios horarios = new Dominio.Horarios();
            TimeSpan horarioNuevo = new TimeSpan(12, 12, 1);
            horarios.HoraTutoria = horarioNuevo;
            horarios.Matricula = "00000000";
            horarios.IdTutoriaAcademica = 5;
            horarios.Asistencia = false;
            horarios.Riesgo = false;
            bool resultadoEsperado = false;
            bool resultadoObtenido = horarioDAO.ModificarHorarioDeSesionDeTutoriaPorMatriculaYIdTutoria(horarios);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }
        [TestMethod]
        public void ModificarHorarioDeSesionDeTutoriaPorMatriculaYIdTutoria_TutoriaNoExistente()
        {
            Dominio.Horarios horarios = new Dominio.Horarios();
            TimeSpan horarioNuevo = new TimeSpan(12, 12, 1);
            horarios.HoraTutoria = horarioNuevo;
            horarios.Matricula = "S19014012";
            horarios.IdTutoriaAcademica = -5;
            horarios.Asistencia = false;
            horarios.Riesgo = false;
            bool resultadoEsperado = false;
            bool resultadoObtenido = horarioDAO.ModificarHorarioDeSesionDeTutoriaPorMatriculaYIdTutoria(horarios);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        public void ObtenerHorariosDeTutoriaPorIdTutoria_Exito()
        {
            int idTutoria = 1;
            int numeroHorariosEsperados = 3;
            numeroHorariosEsperados.Should().Be(horarioDAO.ObtenerHorariosDeTutoriaPorIdTutoria(idTutoria).Count);
        }

        [TestMethod]
        public void ObtenerHorariosDeTutoriaPorIdTutoria_Fallo_IdTutoriaInvalido()
        {
            int idTutoria = -1;
            int numeroHorariosEsperados = 3;
            numeroHorariosEsperados.Should().NotBe(horarioDAO.ObtenerHorariosDeTutoriaPorIdTutoria(idTutoria).Count);
        }

        [TestMethod]
        public void RegistrarAsistenciasYEstadoDeEstudiantes_Exito()
        {
            Dominio.Horarios horarios = new Dominio.Horarios();
            horarios.IdHorarios = 1;
            horarios.Asistencia = false;
            horarios.Riesgo = false;
            horarioDAO.RegistrarAsistenciasYEstadoDeEstudiantes(horarios).Should().BeTrue();
        }

        [TestMethod]
        public void RegistrarAsistenciasYEstadoDeEstudiantes_Fallo_IdHorarioInexistente()
        {
            Dominio.Horarios horarios = new Dominio.Horarios();
            horarios.IdHorarios = -1;
            horarios.Asistencia = false;
            horarios.Riesgo = false;
            horarioDAO.RegistrarAsistenciasYEstadoDeEstudiantes(horarios).Should().BeFalse();
        }

        [TestMethod]
        public void RegistrarAsistenciasYEstadoDeEstudiantes_Fallo_IdHorarioInvalido()
        {
            Dominio.Horarios horarios = new Dominio.Horarios();
            horarios.Asistencia = false;
            horarios.Riesgo = false;
            horarioDAO.RegistrarAsistenciasYEstadoDeEstudiantes(horarios).Should().BeFalse();
        }

        [TestMethod]
        public void RecuperarTotalAsistentesYEnRiesgoDeEstudiantesPorIdTutoria_Exito()
        {
            int idFechaTutoria = 1;
            Dictionary<String, int> totalAsistentesYEnRiesgo =
                horarioDAO.RecuperarTotalAsistentesYEnRiesgoDeEstudiantesPorIdTutoria(idFechaTutoria);
            totalAsistentesYEnRiesgo["EnRiesgo"].Should().Be(1);
            totalAsistentesYEnRiesgo["Asistentes"].Should().Be(1);
        }

        [TestMethod]
        public void RecuperarTotalAsistentesYEnRiesgoDeEstudiantesPorIdTutoria_Fallo_IdTutoriaInexistente()
        {
            int idFechaTutoria = -1;
            Dictionary<String, int> totalAsistentesYEnRiesgo =
                horarioDAO.RecuperarTotalAsistentesYEnRiesgoDeEstudiantesPorIdTutoria(idFechaTutoria);
            totalAsistentesYEnRiesgo["EnRiesgo"].Should().Be(0);
            totalAsistentesYEnRiesgo["Asistentes"].Should().Be(0);
        }
    }
}
