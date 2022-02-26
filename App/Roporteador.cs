using System;
using System.Linq;
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>> dicObjEsc)
        {
            if(dicObjEsc == null)
                throw new ArgumentNullException(nameof(dicObjEsc));
            _diccionario = dicObjEsc;
        }

        public IEnumerable<Evaluacion> GetListaEvaluacion()
        {

            if( _diccionario.TryGetValue(LlaveDiccionario.Evaluacion,
                                                out IEnumerable<ObjetoEscuelaBase> lista))
            {
                return lista.Cast<Evaluacion>();
            }
            {
                return new List<Evaluacion>();
            }
        }

        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }

        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluacion> listaEvaluacion)
        {
            listaEvaluacion = GetListaEvaluacion();
            return (from Evaluacion ev in listaEvaluacion
                    select ev.Asignatura.Nombre).Distinct();
        }

        public Dictionary<string,IEnumerable<Evaluacion>> GetDiccionarioaAsigXEvaluacion()
        {

            var dicRta = new Dictionary<string,IEnumerable<Evaluacion>>();
            var listaAsig = GetListaAsignaturas(out var listaEval);

            foreach (var asig in listaAsig)
            {
                var evalAsig = from eval in listaEval
                                where eval.Asignatura.Nombre == asig
                                select eval;
                dicRta.Add(asig,evalAsig);
            }

            return dicRta;
            
        }
        
        
    

    }
}