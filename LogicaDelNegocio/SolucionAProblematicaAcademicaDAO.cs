using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDelNegocio
{
    public class SolucionAProblematicaAcademicaDAO
    {
        public Soluciones ObtenerSolucion(int idProblematica){
            Soluciones SolucionObtenida = new Soluciones();
            using (var entidades = new EntidadesTutorias()) {
                var SolucionEncontrada = entidades.Soluciones.Where(x => x.IdProblematica == idProblematica);
                if (SolucionEncontrada.Count() > 0)
                {
                    SolucionObtenida.IdSolucion = SolucionEncontrada.First().IdSolucion;
                    SolucionObtenida.Descripcion = SolucionEncontrada.First().Descripcion;
                    SolucionObtenida.Fecha = SolucionEncontrada.First().Fecha;
                    SolucionObtenida.IdAcademico = SolucionEncontrada.First().IdAcademico;
                    SolucionObtenida.IdProblematica = SolucionEncontrada.First().IdProblematica;
                    SolucionObtenida.Problematicas = problematicaSolucion(SolucionObtenida.IdProblematica);
                    SolucionObtenida.Academicos = academicoSolucion(SolucionObtenida.IdAcademico);
                    return SolucionObtenida;
                }
                else
                {
                    return SolucionObtenida = null;
                }
            };
        }

        public Academicos academicoSolucion(int idAcademico)
        {
            Academicos academicoObtenido = new Academicos();
            using (var entidades = new EntidadesTutorias()) 
            {
                var academicoEncontrado = entidades.Academicos.Where(x => x.IdAcademico == idAcademico);
                if (academicoEncontrado.Count() > 0) 
                {
                    academicoObtenido.IdAcademico = academicoEncontrado.First().IdAcademico;
                    academicoObtenido.Nombre = academicoEncontrado.First().Nombre;
                    academicoObtenido.ApellidoMaterno = academicoEncontrado.First().ApellidoMaterno;
                    academicoObtenido.ApellidoPaterno = academicoEncontrado.First().ApellidoPaterno;
                    
                    return academicoObtenido;
                }else
                return academicoObtenido;
            };
                
        }


        public Problematicas problematicaSolucion(int idProblematica)
        {
            Problematicas problematicaObtenida = new Problematicas();
            using (var entidades = new EntidadesTutorias())
            {
                var problematicaEncontrada = entidades.Problematicas.Where(x => x.IdProblematica == idProblematica);
                if (problematicaEncontrada.Count() > 0)
                {
                    problematicaObtenida.Titulo = problematicaEncontrada.First().Titulo;
                    problematicaObtenida.ExperienciasEducativas_Academicos = problematicaEncontrada.First().ExperienciasEducativas_Academicos;
                    problematicaObtenida.Descripcion = problematicaEncontrada.First().Descripcion;
                    problematicaObtenida.NRC = problematicaEncontrada.First().NRC;
                    return problematicaObtenida;
                }
                else
                    return problematicaObtenida;
            };
        }


        public ExperienciasEducativas ExperienciaEducativaSolucion(int NRC) {
            ExperienciasEducativas experienciaEducativaObtenida = new ExperienciasEducativas();
            ExperienciasEducativas_Academicos experienciasEducativas_Academicos = new ExperienciasEducativas_Academicos();
            using (var entidades = new EntidadesTutorias()) {

                

                var experienciasAcademicos = entidades.ExperienciasEducativas_Academicos.Where( x => x.NRC == NRC);
       
                int e = experienciasAcademicos.First().IdExperienciaEducativa;

                if (experienciasAcademicos.Count() > 0)
                {
                    var experienciaEducativa = entidades.ExperienciasEducativas.Where(x => x.IdExperienciaEducativa == e);
                    if (experienciaEducativa.Count() > 0)
                    {
                        experienciaEducativaObtenida.Nombre = experienciaEducativa.First().Nombre;
                    }
                }
                return experienciaEducativaObtenida;
                        };
        }

        public ExperienciasEducativas_Academicos CursoSolucion(int idAcademico) {
            ExperienciasEducativas_Academicos experienciasAcademicos = new ExperienciasEducativas_Academicos();
            using (var entidades = new EntidadesTutorias())
            {
                var experienciasAcademicosEncontradas = entidades.ExperienciasEducativas_Academicos.Where(y => y.IdAcademico == (entidades.ExperienciasEducativas_Academicos.Where(x => x.IdAcademico == idAcademico).First().IdAcademico));
                }
                return experienciasAcademicos; 
        
        }

        public Boolean RegistrarSolucion(Dominio.Soluciones solucionARegistrar)
        {
            Boolean registroExitoso = false;
            using (var entidades = new EntidadesTutorias())
            {
                entidades.Soluciones.Add(solucionARegistrar.SolucionesADatos());
                registroExitoso = entidades.SaveChanges() == 1;
            }
            return registroExitoso;
        }

        public Boolean BuscarSolucionPorIdProblematica(int idProblematica)
        {
            Boolean solucionEncontrada = false;
            using (var entidades = new EntidadesTutorias())
            {
                Boolean existe =
                    entidades.Soluciones.Any(solucion =>
                        solucion.IdProblematica == idProblematica);
                if (existe)
                {
                    solucionEncontrada = true;
                }
            }
            return solucionEncontrada;
        }

        public Boolean ModificarSolucion(AccesoADatos.Soluciones solucion)
        {
            var SolucionObtenida = new AccesoADatos.Soluciones();
            SolucionObtenida.IdSolucion = solucion.IdSolucion;
            SolucionObtenida.IdAcademico = solucion.IdAcademico;
            SolucionObtenida.Fecha = solucion.Fecha;
            SolucionObtenida.Descripcion = solucion.Descripcion;
            SolucionObtenida.IdProblematica = solucion.IdProblematica;

            using (var entidades = new EntidadesTutorias())
            {
                Console.Out.WriteLine(SolucionObtenida.Descripcion);
                entidades.Soluciones.AddOrUpdate(SolucionObtenida);
                if (entidades.SaveChanges() > 0)
                {
                    SolucionObtenida = null;
                    return true;
                }
                else
                {
                    return false;
                }


            };
        }

    }
    }
   

