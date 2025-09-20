using System;

namespace Northwind.Entities;

public class Producto
{
    public string? Nombre { get; set; }    
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public int IdCategoria { get; set; }
}
