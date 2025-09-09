namespace Northwind.LibA;

public class BaseClass
{
    internal void MetodoInternal()
    {
        Console.WriteLine("Soy internal: solo accesible dentro del ensamblado");
    }

    protected internal void MetodoProtectedInternal()
    {
        Console.WriteLine("Soy protected internal; accesible en el ensamblado o por herencia externa");
    }
}
