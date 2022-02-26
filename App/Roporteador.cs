using System;
using System.Linq;
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        private Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>> dicObjEsc)
        {
            if(dicObjEsc == null)
                //throw new ArgumentNullException(nameof(dicObjEsc));
                _diccionario = dicObjEsc;
        }

        public IEnumerable<Escuela> GetListaEscuela()
        {
            var lista = _diccionario.GetValueOrDefault(LlaveDiccionario.Escuela);
            return lista.Cast<Escuela>();
        }

    }
}