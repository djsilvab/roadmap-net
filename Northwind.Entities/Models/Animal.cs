using System;

namespace Northwind.Entities.Models;

public class Animal
{
    public virtual void HacerSonido()
        => Console.WriteLine("Sonido genérico de animal");
}

public class Perro : Animal
{
    override public void HacerSonido()
        => Console.WriteLine("Guau Guau");
}