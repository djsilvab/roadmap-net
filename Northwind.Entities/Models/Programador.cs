using System;

namespace Northwind.Entities.Models;

public class Programador : EmpleadoBase
{
    public Programador(string? nombre) : base(nombre)
    {
    }  

    public override void Reportar()
    {
        Console.WriteLine($"{Nombre} está reportando su progreso.");
    }
}
