using AccesoADatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDelNegocio
{
    public class ExperienciasEducativasDAO
    {
        public ObservableCollection<Dominio.ExperienciasEducativas>ObtenerExperienciasEducativas()
        {
            ObservableCollection<Dominio.ExperienciasEducativas> experienciasEducativasObtenidas = 
                new ObservableCollection<Dominio.ExperienciasEducativas>();
            using (var entidades = new EntidadesTutorias()) 
            {
                var experienciasEducativasEncontradas = entidades.ExperienciasEducativas.ToList();
                foreach (AccesoADatos.ExperienciasEducativas experienciaEducativaEncontrada in experienciasEducativasEncontradas)
                {
                    experienciasEducativasObtenidas.Add(new Dominio.ExperienciasEducativas(experienciaEducativaEncontrada));
                }
            }
            return experienciasEducativasObtenidas;
        }

        public Boolean RegistrarExperienciaEducativa(Dominio.ExperienciasEducativas experienciasEducativasNueva)
        {
            Boolean registroExitoso = false;
            this.ValidarNombreExperienciaEducativa(experienciasEducativasNueva.Nombre);
            using (var entidades = new EntidadesTutorias())
            {
                entidades.ExperienciasEducativas.Add(experienciasEducativasNueva.AAccesoADatos());
                registroExitoso = entidades.SaveChanges() == 1;
            }
            return registroExitoso;
        }
        public Boolean ModificarExperienciaEducativa(Dominio.ExperienciasEducativas experienciasEducativasNueva)
        {
            Boolean registroExitoso = false;
            this.ValidarNombreExperienciaEducativa(experienciasEducativasNueva.Nombre);
            using (var entidades = new EntidadesTutorias())
            {
                var experienciaEncontrada = entidades.ExperienciasEducativas.FirstOrDefault(
                    e => e.IdExperienciaEducativa == experienciasEducativasNueva.IdExperienciaEducativa
                );
                experienciaEncontrada.Nombre = experienciasEducativasNueva.Nombre;
                registroExitoso = entidades.SaveChanges() == 1;
            }
            return registroExitoso;
        }

        public void ValidarNombreExperienciaEducativa(string nombre)
        {
            using (var entidades = new EntidadesTutorias())
            {
                Boolean existe =
                    entidades.ExperienciasEducativas.Any(experiencia =>
                        experiencia.Nombre == nombre);
                if (String.IsNullOrWhiteSpace(nombre))
                {
                    throw new DbEntityValidationException($"El nombre no puede estar vacío");
                }
                if (!Validador.ValidarCadenaAlfabetica(nombre))
                {
                    throw new DbEntityValidationException($"El campo nombre debe ser alfabético.");
                }
                if (existe)
                {
                    throw new DbEntityValidationException("La Experiencia Educativa ya existe.");
                }
            }
        }

        public Dominio.ExperienciasEducativas ObtenerExperienciaEducativaPorIdExperienciaEducativa(int idExperienciaEducativa)
        {
            Dominio.ExperienciasEducativas experienciaEducativaObtenida = new Dominio.ExperienciasEducativas();
            using (var entidadesTutorias = new EntidadesTutorias())
            {
                var experienciaEducativaEncontrada = entidadesTutorias.ExperienciasEducativas.Find(idExperienciaEducativa);
                if (experienciaEducativaEncontrada != null)
                {
                    experienciaEducativaObtenida = new Dominio.ExperienciasEducativas(experienciaEducativaEncontrada);
                }
            }
            return experienciaEducativaObtenida;
        }



        public ObservableCollection<AccesoADatos.ExperienciasEducativas_Academicos> BuscarExperienciaEducativas(String objetoBusqueda)
        {
            ObservableCollection<AccesoADatos.ExperienciasEducativas_Academicos> experienciasEducativasRecuperadas = new ObservableCollection<AccesoADatos.ExperienciasEducativas_Academicos>();
            SqlParameter busqueda = new SqlParameter("@Busqueda", objetoBusqueda);
            SqlParameter busquedaNombre = new SqlParameter("@Id", objetoBusqueda);
            using (var entidades = new EntidadesTutorias())
            {
                var experienciasEducativasObtenidas = entidades.ExperienciasEducativas_Academicos.SqlQuery("EXEC dbo.GetExperienciasEducativas @Busqueda", busqueda).ToList();

                var nombresExperienciasEducativasObtenidas = entidades.ExperienciasEducativas.SqlQuery("EXEC dbo.GetExperienciasEducativas @Id", busquedaNombre).ToList();

                if (experienciasEducativasObtenidas.Count() > 0)
                {
                    for (int i = 0; i < experienciasEducativasObtenidas.Count(); i++)
                    {
                        experienciasEducativasRecuperadas.Add(experienciasEducativasObtenidas.ElementAt(i));
                        experienciasEducativasRecuperadas.ElementAt(i).ExperienciasEducativas.Nombre = nombresExperienciasEducativasObtenidas.ElementAt(i).Nombre;
                    }

                    return experienciasEducativasRecuperadas;
                }
                else
                {
                    return experienciasEducativasRecuperadas;
                }


            }
        }


        public ObservableCollection<AccesoADatos.ExperienciasEducativas> BuscarExperienciaEducativasPorNombre(String objetoBusqueda)
        {
            ObservableCollection<AccesoADatos.ExperienciasEducativas> experienciasEducativasRecuperadas = new ObservableCollection<AccesoADatos.ExperienciasEducativas>();
            SqlParameter busqueda = new SqlParameter("@Busqueda", objetoBusqueda);
            SqlParameter busquedaNombre = new SqlParameter("@Id", objetoBusqueda);
            using (var entidades = new EntidadesTutorias())
            {
                var nombresExperienciasEducativasObtenidas = entidades.ExperienciasEducativas.SqlQuery("EXEC dbo.GetExperienciasEducativas @Id", busquedaNombre).ToList();

                if (nombresExperienciasEducativasObtenidas.Count() > 0)
                {
                    for (int i = 0; i < nombresExperienciasEducativasObtenidas.Count(); i++)
                    {
                        experienciasEducativasRecuperadas.Add(new AccesoADatos.ExperienciasEducativas());
                        experienciasEducativasRecuperadas.ElementAt(i).Nombre = nombresExperienciasEducativasObtenidas.ElementAt(i).Nombre;
                        experienciasEducativasRecuperadas.ElementAt(i).IdExperienciaEducativa = nombresExperienciasEducativasObtenidas.ElementAt(i).IdExperienciaEducativa;
                    }
                    return experienciasEducativasRecuperadas;
                }
                else
                {
                    return experienciasEducativasRecuperadas;
                }


            }
        }
    }
}
