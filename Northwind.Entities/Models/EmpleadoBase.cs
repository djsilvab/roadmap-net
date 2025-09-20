using System;
using Northwind.Entities.Interfaces;

namespace Northwind.Entities.Models;

public abstract class EmpleadoBase : ITrabajador
{
    public string? Nombre { get; set; }

    public EmpleadoBase(string? nombre)
    {
        Nombre = nombre;
    }

    public void Trabajar()
        => Console.WriteLine($"{Nombre} está trabajando.");
        
    public abstract void Reportar();
}
