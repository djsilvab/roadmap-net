using System;

namespace Northwind.Entities;

//public record PersonaRecord(string Nombre, int Edad);

public record Persona
{
    public string Nombre { get; init; }
    public int Edad { get; init; }
}

