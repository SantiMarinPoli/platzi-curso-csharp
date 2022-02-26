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
                //throw new ArgumentNullException(nameof(dicObjEsc));
                _diccionario = dicObjEsc;
        }

        public IEnumerable<Escuela> GetListaEscuela()
        {
            IEnumerable<Escuela> rta;
            if( _diccionario.TryGetValue(LlaveDiccionario.Escuela,
                                                out IEnumerable<ObjetoEscuelaBase> lista))
            {
                rta = lista.Cast<Escuela>();
            }
            else
            {
                rta = null;
            }
            return rta;
        }

    }
}