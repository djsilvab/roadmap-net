using System;

namespace Northwind.Entities.Interfaces;

public interface IVenta
{
    decimal CalcularTotal();
}

public class Venta : IVenta
{
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public string? Producto { get; set; }

    public virtual decimal CalcularTotal()
        => Precio * Cantidad;
}

public class VentaConDescuento : Venta
{
    public decimal Descuento { get; set; }

    public override string ToString()
        => $"{Producto} x {Cantidad} (desc {Descuento}%) - Total: {CalcularTotal()}";

    public override decimal CalcularTotal()
        => base.CalcularTotal() * (1 - Descuento / 100);
}