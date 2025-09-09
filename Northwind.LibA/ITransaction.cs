using System;

namespace Northwind.LibA;

public interface ITransaction
{
    void Ejecutar(decimal monto);
}

public class Transferencia : ITransaction
{
    public void Ejecutar(decimal monto)
    {
        Console.WriteLine($"Transferencia realizada por: {monto:C}");
    }
}

public class Deposito : ITransaction
{
    public void Ejecutar(decimal monto)
    {
        Console.WriteLine($"Depósito realizado por: {monto:C}");
    }
}

public class Retiro : ITransaction
{
    public void Ejecutar(decimal monto)
    {
        Console.WriteLine($"Retiro realizado por: {monto:C}");
    }
}