using System;

namespace Northwind.Entities;

//public record PersonaRecord(string Nombre, int Edad);

public record PersonaRecord
{
    public string Nombre { get; init; }
    public int Edad { get; init; }
}

public record EmpleadoRecord(string Nombre, int Edad);