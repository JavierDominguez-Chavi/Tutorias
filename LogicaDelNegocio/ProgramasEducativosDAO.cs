using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

using AccesoADatos;

namespace LogicaDelNegocio
{
    public class ProgramasEducativosDAO
    {
        public List<Dominio.ProgramasEducativos> ObtenerProgramasEducativos()
        {
            List<Dominio.ProgramasEducativos> programasEducativosObtenidos =
                new List<Dominio.ProgramasEducativos>();
            using (var _entidades = new EntidadesTutorias())
            {
                List<AccesoADatos.ProgramasEducativos> programasEducativosEncontrados = new List<AccesoADatos.ProgramasEducativos>();
                programasEducativosEncontrados = _entidades.ProgramasEducativos.ToList();
                if(programasEducativosEncontrados.FirstOrDefault() != null)
                {
                    foreach (AccesoADatos.ProgramasEducativos programaEducativo in programasEducativosEncontrados)
                    {
                        programasEducativosObtenidos.Add(new Dominio.ProgramasEducativos(programaEducativo));
                    }
                }
            }
            return programasEducativosObtenidos;
        }

        public bool BuscarProgramaEducativoExistente (String programaEducativo)
        {
            bool existe = false;
            using (var entidades = new EntidadesTutorias())
            {
                existe = entidades.ProgramasEducativos.Any(pe => pe.NombreProgramaEducativo == programaEducativo);
            }
            return existe;
        }

        public bool RegistrarProgramaEducativo(Dominio.ProgramasEducativos programaEducativo)
        {
            bool registroExitoso = false;
            using (var entidades = new EntidadesTutorias())
            {
                AccesoADatos.ProgramasEducativos programaEducativoDatos = new AccesoADatos.ProgramasEducativos();
                programaEducativoDatos.NombreProgramaEducativo = programaEducativo.NombreProgramaEducativo;
                entidades.ProgramasEducativos.Add(programaEducativoDatos);
                registroExitoso = entidades.SaveChanges() > 0;
            }
            return registroExitoso;
        }

        public bool ModificarProgramaEducativo(Dominio.ProgramasEducativos programaEducativo)
        {
            bool modificado = false;
            using(var entidades = new EntidadesTutorias())
            {
                var programaEducativoEncontrado = entidades.ProgramasEducativos.FirstOrDefault(
                    pE => pE.IdProgramaEducativo == programaEducativo.IdProgramaEducativo);
                if (programaEducativoEncontrado != null)
                {
                    programaEducativoEncontrado.NombreProgramaEducativo = programaEducativo.NombreProgramaEducativo;
                }
                modificado = entidades.SaveChanges() > 0;
            }
                return modificado;
        }

        public Dominio.ProgramasEducativos ObtenerProgramaEducativoPorIdProgramaEducativo (int idProgramaEducativo)
        {
            Dominio.ProgramasEducativos programaEducativo = new Dominio.ProgramasEducativos();
            using (var entidades = new EntidadesTutorias())
            {
                var programaEducativoEncontrado = entidades.ProgramasEducativos.FirstOrDefault(
                    pE => pE.IdProgramaEducativo == idProgramaEducativo);
                if (programaEducativoEncontrado != null)
                {
                    programaEducativo = new Dominio.ProgramasEducativos(programaEducativoEncontrado);
                }
                else
                {
                    programaEducativo = null;
                }
            }
            return programaEducativo;
        }
    }
}
