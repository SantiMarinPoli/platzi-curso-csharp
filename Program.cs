using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Declarando Eventos*/
            AppDomain.CurrentDomain.ProcessExit += AccionEvento;
            AppDomain.CurrentDomain.ProcessExit += (obj,e)=> Printer.Beep(100,1000,3);
            AppDomain.CurrentDomain.ProcessExit -= AccionEvento;


            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

            var reporteador = new Reporteador(null);
            reporteador.GetListaEscuela();

           // Printer.Beep(10000, cantidad:10);
            //ImpimirCursosEscuela(engine.Escuela);

            //var dicc = engine.GetDiccionarioObjeto();

            //engine.ImprimirDiccionario(dicc,true);

            /*var listaILugar = from obj in listaObjetos
                              where obj is ILugar
                              select (ILugar) obj;
            //engine.Escuela.LimpiarLugar();*/
            
            //No puede iniciar porque el objeto es abtracto
            //var obj = new ObjetoEscuelaBase();
            //Printer.DrawLine(20);
            //Printer.DrawLine(20);
            //Printer.DrawLine(20);
            /*Printer.WriteTitle("Pruebas de polimorfismo");
            /*Alumno hereda el objeto son copatibles con ObjetoEscuela de la herencia*/
           // var alumnoTest = new Alumno{Nombre="Peter Parker"};
           // ObjetoEscuelaBase ob = alumnoTest;
           // Printer.WriteTitle("Alumno");
           // Console.WriteLine($"Alumno: {alumnoTest.Nombre}");
            //Console.WriteLine($"Alumno: {alumnoTest.UniqueId}");
            //Console.WriteLine($"Alumno: {alumnoTest.GetType()}");

           // Printer.WriteTitle("Objeto Alumno");
            //Console.WriteLine($"Alumno: {ob.Nombre}");
            //Console.WriteLine($"Alumno: {ob.UniqueId}");
           // Console.WriteLine($"Alumno: {ob.GetType()}");

          //  var evaluacion = new Evaluacion(){Nombre="Evaluacion de Matematica",Nota=4.5f};
         //   Printer.WriteTitle("Evaluacion");
          //  Console.WriteLine($"Evaluacion: {evaluacion.Nombre}");
           // Console.WriteLine($"Evaluacion: {evaluacion.Nota}");
           // Console.WriteLine($"Evaluacion: {evaluacion.UniqueId}");
           // Console.WriteLine($"Evaluacion: {evaluacion.GetType()}");

            //ob= evaluacion;
          //  Printer.WriteTitle("Objeto Evaluacion");
           // Console.WriteLine($"Evaluacion: {ob.Nombre}");
           // Console.WriteLine($"Evaluacion: {ob.UniqueId}");
           // Console.WriteLine($"Evaluacion: {ob.GetType()}");

            /*Hay dos formas para declarar el polimorfismo*/
            /*Primero realizar la condicional que tipo de entidad esta trayendo*/
           // if(ob is Alumno){
              //  Alumno alumnoRecuperado = (Alumno) ob;
           // }
            /*La segunda esta declarar AS como la entidad Alumno si no trae informacion va traer Null,
            es muy recomendado realizar esta instruccion*/
            //Alumno alumnoRecuperado2 =  ob as Alumno;

        }

        private static void AccionEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("Saliendo ...");
            Printer.Beep(3000,1000,3);
            Printer.WriteTitle("SALIO.");   
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {
            
            Printer.WriteTitle("Cursos de la Escuela");
            
            
            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
    }
}
