using System;

namespace Northwind.Entities;

public class Producto
{
    public int Id { get; set; }
    public string? Nombre { get; set; } 
    public string? Descripcion { get; set; }   
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public DateTime FechaDeAlta { get; set; }
    public int IdCategoria { get; set; }
}
