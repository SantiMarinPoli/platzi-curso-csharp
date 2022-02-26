using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela.App
{
    /*
    Sealed
    Si queremos que nuestra clase sea INSTANCIADA pero que no fuera posible **HEREDAR de ella 
    ** debemos utilizar el tipo de clase SEALED (Clase sellada).
    */
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine()
        {

        }

        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academy", 2012, TiposEscuela.Primaria,
            ciudad: "Bogotá", pais: "Colombia"
            );
            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();

        }
        #region "Metodo Lista"

    public void ImprimirDiccionario(Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>> dicc,
    bool printEval = false)
{
    foreach(var obj in dicc)
    {
        Printer.WriteTitle(obj.Key.ToString());
        foreach (var value in obj.Value)
        {
            switch (obj.Key)
            {
                case LlaveDiccionario.Evaluacion:
                    if (printEval)
                        Console.WriteLine(value);
                break;

                case LlaveDiccionario.Escuela:
                    Console.WriteLine($"Escuela: {value}");
                break;

                case LlaveDiccionario.Alumno:
                    Console.WriteLine($"Alumno: {value.Nombre}");
                break;
                 case LlaveDiccionario.Curso:
                    var curtMp = value as Curso;
                    if (curtMp != null)
                    {
                        int count = curtMp.Alumnos.Count;
                        Console.WriteLine($"Curso: {value.Nombre}, La Cantidad Alumnos: {count}");

                    }
                break;
                default:
                    Console.WriteLine(value);
                break;
            }
            

            
        }
    }
}
public Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjeto()
{
    var dicc = new Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>>();
    var listaEvaluacionTmp = new List<Evaluacion>();
    var listaAsignaturasTmp = new List<Asignatura>();
    var listaAlumnoTmp= new List<Alumno>();

    dicc.Add(LlaveDiccionario.Escuela,new[] {Escuela});
    dicc.Add(LlaveDiccionario.Curso,Escuela.Cursos.Cast<ObjetoEscuelaBase>());

    foreach (var cur in Escuela.Cursos)
    {
       listaAsignaturasTmp.AddRange(cur.Asignaturas);
       listaAlumnoTmp.AddRange(cur.Alumnos);

        foreach (var alum in cur.Alumnos)
        {
            listaEvaluacionTmp.AddRange(alum.Evaluacion);
        }
    }
    dicc.Add(LlaveDiccionario.Alumno,listaAlumnoTmp);
    dicc.Add(LlaveDiccionario.Asignatura,listaAsignaturasTmp);
    dicc.Add(LlaveDiccionario.Evaluacion,listaEvaluacionTmp);

    return dicc;
}

/*
Puedes crear objetos de tipo List<T> sin embargo
no puedes crear objetos de tipo IEnumerable<> ya que al
ser una interfase es abstracta y no puede ser instanciada

List es una clase. IEnumerable<> es una interface.
*/

    public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuela(
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas =true,
            bool traerCursos = true
            )
        {
            return GetObjetoEscuela(out int dummy,out  dummy,out  dummy,out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuela(
            out int contEvaluaciones,
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas =true,
            bool traerCursos = true
            )
        {
            return GetObjetoEscuela(out contEvaluaciones,out int  dummy,out  dummy,out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuela(
            out int contEvaluaciones,
            out int contAlumnos,
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas =true,
            bool traerCursos = true
            )
        {
            return GetObjetoEscuela(out contEvaluaciones,out contAlumnos,out int  dummy,out dummy);
        }

        
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuela(
            out int contEvaluaciones,
            out int contAlumnos,
            out int contAsignaturas,
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas =true,
            bool traerCursos = true
            )
        {
            return GetObjetoEscuela(out contEvaluaciones,out contAlumnos,out contAsignaturas,out int dummy);
        }


        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuela(
            /*Parametros Salida*/
            out int contEvaluaciones,
            out int contAlumnos,
            out int contAsignaturas,
            out int contCursos,
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas =true,
            bool traerCursos = true
            )
        {
            var listaObj = new List<ObjetoEscuelaBase>();
            contEvaluaciones = contAlumnos = contAsignaturas = contCursos = 0;

            listaObj.Add(Escuela);
            if(traerCursos)
                listaObj.AddRange(Escuela.Cursos);
                contCursos += Escuela.Cursos.Count;
            foreach (var curso in Escuela.Cursos)
            {
                contAsignaturas += curso.Asignaturas.Count;
                contAlumnos += curso.Alumnos.Count;
                if(traerAsignaturas)
                    listaObj.AddRange(curso.Asignaturas);
                if(traerAlumnos == true)
                    listaObj.AddRange(curso.Alumnos);

                if(traerEvaluaciones == true){

                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluacion);
                        contEvaluaciones += alumno.Evaluacion.Count;
                    }
                }
            }
            return listaObj.AsReadOnly();
        }


        private List<Alumno> GenerarAlumnosAlAzar( int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos =  from n1 in nombre1
                                from n2 in nombre2
                                from a1 in apellido1
                                select new Alumno{ Nombre=$"{n1} {n2} {a1}" };
            
            return listaAlumnos.OrderBy( (al)=> al.UniqueId ).Take(cantidad).ToList();
        }


#endregion

#region "Metodo de Cargar"
       private void CargarEvaluaciones()
        {
            var rnd = new Random(System.Environment.TickCount);
            foreach (var curso in  Escuela.Cursos)
            {
                foreach(var asignatura in curso.Asignaturas)
                {
                    foreach(var alumno in curso.Alumnos)
                    {
                        for(int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluacion
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i +1}",
                                Nota =  MathF.Round(
                                                    5 * (float)rnd.NextDouble()
                                                ,2),
                                Alumno = alumno
                            };
                            alumno.Evaluacion.Add(ev);
                        }
                    }
                }
            }

        }
   
            
        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>(){
                            new Asignatura{Nombre="Matemáticas"} ,
                            new Asignatura{Nombre="Educación Física"},
                            new Asignatura{Nombre="Castellano"},
                            new Asignatura{Nombre="Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

  
        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                        new Curso(){ Nombre = "101", Jornada = TiposJornada.Mañana },
                        new Curso() {Nombre = "201", Jornada = TiposJornada.Mañana},
                        new Curso{Nombre = "301", Jornada = TiposJornada.Mañana},
                        new Curso(){ Nombre = "401", Jornada = TiposJornada.Tarde },
                        new Curso() {Nombre = "501", Jornada = TiposJornada.Tarde},
            };
            
            Random rnd = new Random();
            foreach(var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        }
    }
    #endregion
}