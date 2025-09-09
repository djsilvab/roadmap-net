using System;

namespace Northwind.LibA;

public abstract class Descuento
{
    public abstract decimal Calcular(decimal precio);
}

public class DescuentoNormal : Descuento
{
    public override decimal Calcular(decimal precio)
        => precio;
}

public class DescuentoVIP : Descuento
{
    public override decimal Calcular(decimal precio)
        => precio * 0.9m;
}

public class DescuentoEstudiante : Descuento
{
    public override decimal Calcular(decimal precio)
        => precio * 0.8m;
}

public class CalcularDescuento
{
    public decimal Calcular(Descuento descuento, decimal precio)
        => descuento.Calcular(precio);
}