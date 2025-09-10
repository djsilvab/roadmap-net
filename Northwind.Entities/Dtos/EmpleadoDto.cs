using System;

namespace Northwind.Entities.Dtos;

// public record EmpleadoDto(string Nombre, int Edad);

public record EmpleadoDto
{
    public string Nombre { get; init; }
    public int Edad { get; init; }

    //Constructor personalizado con validaciones
    public EmpleadoDto(string nombre, int edad)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.");

        if (edad < 18)
            throw new ArgumentException("El empleado debe ser mayor de edad");

        this.Nombre = nombre;
        this.Edad = edad;
    }
}

