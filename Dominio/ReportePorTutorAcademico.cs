using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ReportePorTutorAcademico
    {
        public String PeriodoEscolar { get; set; }
        public int NumeroDeSesion { get; set; }
        public int IdTutorAcademico { get; set; }
        public String NombreTutorAcademico { get; set; }
        public bool Estado { get; set; }
        public string EstadoEscrito { get; set; }
        public int IdTutoriaAcademica { get; set; }
        public int IdFechaTutoria { get; set; }
        public FechasTutorias FechaTutoria { get; set; }

        public ReportePorTutorAcademico()
        {
            if (Estado)
            {
                EstadoEscrito = "Reporte Entregado";
            }
            else
            {
                EstadoEscrito = "Reporte no entregado";
            }
        }

    }
}
