namespace CoreEscuela.Entidades
{
    /*
    interface, es una definicion DE LA ESTRUPTURA DEBE TENER UN OBJETO
    */
    public interface ILugar
    {
        string Direccion { get; set; }
        void LimpiarLugar();
    }
}