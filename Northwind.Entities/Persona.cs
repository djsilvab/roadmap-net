using System;

namespace Northwind.Entities;

public class Persona
{
    public string Nombre { get; private set; }
    public int Edad { get; private set; }

    public Persona(string nombre, int edad)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es obligatorio");

        if (edad < 0 || edad > 120)
            throw new ArgumentException("La edad no es válida");

        Nombre = nombre;
        Edad = edad;
    }
}
