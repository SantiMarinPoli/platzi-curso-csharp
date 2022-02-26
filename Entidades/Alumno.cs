using System;
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    /*La herencia se declara :*/
    public class Alumno:ObjetoEscuelaBase
    {
        public List<Evaluacion> Evaluacion {get;set;} = new List<Evaluacion>();
    }
}