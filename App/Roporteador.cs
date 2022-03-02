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
        
        public Dictionary<string,IEnumerable<object>> GetPromAlumnoPorAsignatura()
        {
            var rta = new Dictionary<string,IEnumerable<object>> ();
            var diccEvAsig = GetDiccionarioaAsigXEvaluacion();

            foreach(var asigEval in diccEvAsig)
            {
                var promAlum = from eval in asigEval.Value
                            group eval by new {
                                eval.Alumno.UniqueId,
                                eval.Alumno.Nombre
                            } 
                            into grupoEvalAlumno
                            select new AlumnoPromedio
                            {
                                alumnoId = grupoEvalAlumno.Key.UniqueId,
                                alumnoNom = grupoEvalAlumno.Key.Nombre,
                                promedio = grupoEvalAlumno.Average(evaluacion => evaluacion.Nota)
                            };

                rta.Add(asigEval.Key,promAlum);
            }
            return rta;
        }

        public Dictionary<string,IEnumerable<AlumnoPromedio>> GetBestAlumnoPromedioPorAsignatura(int ranking)
        {
            var rta = new Dictionary<string,IEnumerable<AlumnoPromedio>>();
            var result = GetPromAlumnoPorAsignatura();

            foreach (var asignProm in result)
            {

                    var dummy = (from eval in asignProm.Value.Cast<AlumnoPromedio>()
                                    orderby eval.promedio descending
                                    select eval
                                    ).Take(ranking);
                rta.Add(asignProm.Key,dummy);
            }

            return rta;
        }
    

    }
}