using System;

namespace CoreEscuela.Entidades
{
    /*abstract
    Si queremos que nuestra clase sea HEREDADA pero que no fuera posible 
    INSTANCIARLA debemos utilizar el tipo de clase ABSTRACT (clase abstracta)*/
    public abstract class ObjetoEscuelaBase
    {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }

        /*Inicializando Constructor*/
        public ObjetoEscuelaBase()
        {
            UniqueId = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"{Nombre}, {UniqueId}";
        }
    }
}