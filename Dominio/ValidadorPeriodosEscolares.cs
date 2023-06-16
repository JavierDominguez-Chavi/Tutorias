using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ValidadorPeriodosEscolares
    {
        public bool ValidarPeriodoEscolarAIngresarConPeriodoEscolarExistente
            (PeriodosEscolares periodoEscolarAIngresar, PeriodosEscolares periodoEscolarExistente)
        {
            bool esValido = false;
            if (periodoEscolarAIngresar.FechaInicio < periodoEscolarExistente.FechaFin)
            {
                if (periodoEscolarAIngresar.FechaInicio < periodoEscolarExistente.FechaInicio &&
                    periodoEscolarAIngresar.FechaFin < periodoEscolarExistente.FechaInicio)
                {
                    esValido = true;
                }
            }
            if (periodoEscolarAIngresar.FechaInicio > periodoEscolarExistente.FechaFin)
            {
                esValido = true;
            }
            return esValido;
        }
    }
}
