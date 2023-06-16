using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace Pruebas
{
    [TestClass]
    public class ValidadorFechasTutoriasPrueba
    {
        [TestMethod]
        public void ValidadorFechasTutorias_Exito1()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");
            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = new DateTime(2020,02,15);
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/02/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("15/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("16/04/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias> 
            { 
                sesion1, 
                sesion2, 
                sesion3 
            };

            new Dominio.ValidadorFechasTutorias(fechas);
        }

        [TestMethod]
        public void ValidadorFechasTutorias_Fallo1()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");
            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = new DateTime(2020, 02, 15);
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/02/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("15/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("16/04/2021");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };
            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesión 3: " +
                "La fecha de entrega del reporte debe estar dentro del periodo escolar.", 
                aException.Message);
            }
        }

        [TestMethod]
        public void ValidadorFechasTutorias_Fallo2()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");
            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = new DateTime(2019, 02, 15);
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/02/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("15/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("16/04/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };
            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesión 1: " +
                "La fecha de tutoría debe estar dentro del periodo escolar.",
                aException.Message);
            }
        }

        [TestMethod]
        public void ValidadorFechasTutorias_Fallo3()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");
            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = new DateTime(2020, 02, 15);
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/02/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2019");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("15/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("16/04/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };
            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesiones 1 y 2: Las fechas deben estar en orden ascendente.",
                aException.Message);
            }
        }

        /*
            VALIDAR QUE INTEGRIDAD DE FECHAS
        */

        [TestMethod]
        public void ValidarIntegridad_Exito()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("17/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("18/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            sesion2.NumeroSesion = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("20/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            sesion3.NumeroSesion = 3;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesión 3: La fecha de tutoría debe suceder antes que la fecha de entrega de su reporte.",
                aException.Message);
            }

        }

        [TestMethod]
        public void ValidarIntegridad_Fallo_Periodo()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("17/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("18/01/2020");
            sesion2.PeriodosEscolares = periodo;

            sesion2.IdPeriodo = 0; 

            sesion2.IdProgramaEducativo = 1;
            sesion2.NumeroSesion = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("20/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            sesion3.NumeroSesion = 3;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Las fechas tienen un periodo escolar inválido.",
                aException.Message);
            }

        }

        [TestMethod]
        public void ValidarIntegridad_Fallo_Programa()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("17/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("18/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 0;
            sesion2.NumeroSesion = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("20/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            sesion3.NumeroSesion = 3;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Las fechas tienen un programa educativo inválido.",
                aException.Message);
            }

        }

        [TestMethod]
        public void ValidarIntegridad_Fallo_NumeroSesion()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("17/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("18/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            sesion2.NumeroSesion = 6;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("20/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            sesion3.NumeroSesion = 3;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Las fechas tienen un numero de sesión inválido.",
                aException.Message);
            }

        }

        /*
            VALIDAR QUE LAS FECHAS SEAN ASCENDENTES
        */

        [TestMethod]
        public void OrdenAscendente_Exito_Fronteras()
        {
            /*
             Esta cubre la prueba exito "TutoriaAntesDeCierre_Fronteras"
             */
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("17/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("18/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            sesion2.NumeroSesion = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("19/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            sesion3.NumeroSesion = 3;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            new Dominio.ValidadorFechasTutorias(fechas);

        }

        [TestMethod]
        public void OrdenAscendente_Fallo_Cierre1TraslapaTutoria2()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("16/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("18/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("19/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesiones 1 y 2: Las fechas deben estar en orden ascendente.",
                aException.Message);
            }
        }

        [TestMethod]
        public void OrdenAscendente_Fallo_Cierre1CruzaTutoria2()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("17/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("16/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("18/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("19/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesiones 1 y 2: Las fechas deben estar en orden ascendente.",
                aException.Message);
            }
        }

        [TestMethod]
        public void OrdenAscendente_Fallo_Cierre2TraslapaTutoria3()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("17/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("18/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("18/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesiones 2 y 3: Las fechas deben estar en orden ascendente.",
                aException.Message);
            }
        }

        [TestMethod]
        public void OrdenAscendente_Fallo_Cierre2CruzaTutoria3()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("17/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("19/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("18/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesiones 2 y 3: Las fechas deben estar en orden ascendente.",
                aException.Message);
            }
        }

        /*
            VALIDAR QUE LAS FECHAS DE TUTORIA SEAN ANTES DE SU CIERRE
        */

        [TestMethod]
        public void TutoriaAntesDeCierre_Fallo_Tutoria1IgualACierre1()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("15/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("17/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("18/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("19/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesión 1: La fecha de tutoría debe suceder antes que la fecha de entrega de su reporte.",
                aException.Message);
            }

        }

        [TestMethod]
        public void TutoriaAntesDeCierre_Fallo_Tutoria1DespuesDeCierre1()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("16/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("15/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("17/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("18/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("19/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesión 1: La fecha de tutoría debe suceder antes que la fecha de entrega de su reporte.",
                aException.Message);
            }

        }

        [TestMethod]
        public void TutoriaAntesDeCierre_Fallo_Tutoria2IgualACierre2()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("17/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("17/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("19/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesión 2: La fecha de tutoría debe suceder antes que la fecha de entrega de su reporte.",
                aException.Message);
            }

        }

        [TestMethod]
        public void TutoriaAntesDeCierre_Fallo_Tutoria2DespuesDeCierre2()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("18/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("17/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("19/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesión 2: La fecha de tutoría debe suceder antes que la fecha de entrega de su reporte.",
                aException.Message);
            }

        }

        [TestMethod]
        public void TutoriaAntesDeCierre_Fallo_Tutoria3IgualACierre3()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("17/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("18/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("20/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("20/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesión 3: La fecha de tutoría debe suceder antes que la fecha de entrega de su reporte.",
                aException.Message);
            }

        }

        [TestMethod]
        public void TutoriaAntesDeCierre_Fallo_Tutoria3DespuesDeCierre3()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.NumeroSesion = 1;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("17/01/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("18/01/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.NumeroSesion = 2;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("20/01/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("19/01/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesión 3: La fecha de tutoría debe suceder antes que la fecha de entrega de su reporte.",
                aException.Message);
            }

        }

        /*
            VALIDAR QUE LAS FECHAS ESTÉN DENTRO DE SU PERIODO ESCOLAR
        */
        [TestMethod]
        public void DentroDelPeriodo_Exito_Sesion1_Frontera1y2()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            sesion2.NumeroSesion = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("15/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("15/06/2020");
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            sesion3.PeriodosEscolares = periodo;
            sesion3.NumeroSesion = 3;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            new Dominio.ValidadorFechasTutorias(fechas);

        }

        [TestMethod]
        public void DentroDelPeriodo_Exito_Sesion1_Frontera1Mas1()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("16/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("17/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            sesion2.NumeroSesion = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("15/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("16/04/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            sesion3.NumeroSesion = 3;
            ObservableCollection<Dominio.FechasTutorias> fechas = 
                new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            new Dominio.ValidadorFechasTutorias(fechas);
        }


        [TestMethod]
        public void DentroDelPeriodo_Sesion1_Fallo_Frontera1Menos1()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("14/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("15/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            sesion2.NumeroSesion = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("15/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("16/04/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            sesion3.NumeroSesion = 3;
            ObservableCollection<Dominio.FechasTutorias> fechas = 
                new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesión 1: La fecha de tutoría debe estar dentro del periodo escolar.",
                aException.Message);
            }

        }
        [TestMethod]
        public void DentroDelPeriodo_Fallo_Frontera2Mas1_Sesion3()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            sesion2.NumeroSesion = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("15/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("16/06/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            sesion3.NumeroSesion = 3;
            ObservableCollection<Dominio.FechasTutorias> fechas =
                new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesión 3: " +
                "La fecha de entrega del reporte debe estar dentro del periodo escolar.",
                aException.Message);
            }

        }

        //----SESION 3--------------------

        [TestMethod]
        public void DentroDelPeriodo_Sesion3_Exito_Frontera2()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            sesion2.NumeroSesion = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("14/06/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("15/06/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            sesion3.NumeroSesion = 3;
            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            new Dominio.ValidadorFechasTutorias(fechas);

        }

        [TestMethod]
        public void DentroDelPeriodo_Sesion3_Exito_Frontera2Menos1()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            sesion2.NumeroSesion = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("13/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("14/04/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            sesion3.NumeroSesion = 3;
            ObservableCollection<Dominio.FechasTutorias> fechas =
                new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            new Dominio.ValidadorFechasTutorias(fechas);
        }

        [TestMethod]
        public void DentroDelPeriodo_Sesion3_Fallo_Frontera2Mas1()
        {
            Dominio.PeriodosEscolares periodo = new Dominio.PeriodosEscolares();
            periodo.FechaInicio = DateTime.Parse("15/01/2020");
            periodo.FechaFin = DateTime.Parse("15/06/2020");

            Dominio.FechasTutorias sesion1 = new Dominio.FechasTutorias();
            sesion1.FechaTutoria = DateTime.Parse("15/01/2020");
            sesion1.FechaCierreDeReporte = DateTime.Parse("16/01/2020");
            sesion1.PeriodosEscolares = periodo;
            sesion1.IdPeriodo = 1;
            sesion1.IdProgramaEducativo = 1;
            sesion1.NumeroSesion = 1;
            Dominio.FechasTutorias sesion2 = new Dominio.FechasTutorias();
            sesion2.FechaTutoria = DateTime.Parse("15/03/2020");
            sesion2.FechaCierreDeReporte = DateTime.Parse("16/03/2020");
            sesion2.PeriodosEscolares = periodo;
            sesion2.IdPeriodo = 1;
            sesion2.IdProgramaEducativo = 1;
            sesion2.NumeroSesion = 2;
            Dominio.FechasTutorias sesion3 = new Dominio.FechasTutorias();
            sesion3.FechaTutoria = DateTime.Parse("15/04/2020");
            sesion3.FechaCierreDeReporte = DateTime.Parse("16/06/2020");
            sesion3.PeriodosEscolares = periodo;
            sesion3.IdPeriodo = 1;
            sesion3.IdProgramaEducativo = 1;
            sesion3.NumeroSesion = 3;
            ObservableCollection<Dominio.FechasTutorias> fechas =
                new ObservableCollection<Dominio.FechasTutorias>
            {
                sesion1,
                sesion2,
                sesion3
            };

            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Problema en sesión 3: La fecha de entrega del reporte debe estar dentro del periodo escolar.",
                aException.Message);
            }

        }

        [TestMethod]
        public void ValidarFechasEntrantes_0Fechas()
        {

            ObservableCollection<Dominio.FechasTutorias> fechas = new ObservableCollection<Dominio.FechasTutorias>();
            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Se intentó registrar ["+fechas.Count+"] fecha(s) de tutoría. " +
                "Se deben registrar exactamente tres (3) fechas de " +
                "tutoría por programa educativo, cada periodo escolar.",
                aException.Message);
            }

        }

        [TestMethod]
        public void ValidarFechasEntrantes_4Fechas()
        {

            ObservableCollection<Dominio.FechasTutorias> fechas =
                new ObservableCollection<Dominio.FechasTutorias>() {
                    new Dominio.FechasTutorias(),
                    new Dominio.FechasTutorias(),
                    new Dominio.FechasTutorias(),
                    new Dominio.FechasTutorias(),
                };
            try
            {
                new Dominio.ValidadorFechasTutorias(fechas);
            }
            catch (ArgumentException aException)
            {
                Assert.AreEqual("Se intentó registrar [" + fechas.Count + "] fecha(s) de tutoría. " +
                "Se deben registrar exactamente tres (3) fechas de " +
                "tutoría por programa educativo, cada periodo escolar.",
                aException.Message);
            }

        }

    }
}
