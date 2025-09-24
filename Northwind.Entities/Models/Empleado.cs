using System;

namespace Northwind.Entities.Models;

public class Empleado
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public int IdDireccion { get; set; }
    public int IdDepartamento { get; set; }
}
