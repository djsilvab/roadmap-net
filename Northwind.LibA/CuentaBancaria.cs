using System;

namespace Northwind.LibA;

public abstract class CuentaBancaria
{
    public string NumeroCuenta { get; set; }
    public decimal Saldo { get; protected set; }

    public CuentaBancaria(string NumeroCuenta, decimal Saldo)
    {
        this.NumeroCuenta = NumeroCuenta;
        this.Saldo = Saldo;
    }

    //Método abstracto (obligatorio en las subclases)
    public abstract void Retirar(decimal monto);

    public void Depositar(decimal monto)
    {
        Saldo += monto;
        Console.WriteLine($"Depósito de {monto:C}. Nuevo saldo: {Saldo:C}");
    }

}

public class CuentaAhorro : CuentaBancaria
{

    public CuentaAhorro(string numeroCuenta, decimal saldo)
        : base(numeroCuenta, saldo)
    {
        
    }

    public override void Retirar(decimal monto)
    {
        if (Saldo - monto < 0) Console.WriteLine("Saldo insuficiente, no se puede retirar");
        else
        {
            Saldo -= monto;
            Console.WriteLine($"Retiro del {monto:C}. Saldo actual: {Saldo:C}");
        }
    }
}

public class CuentaCorriente : CuentaBancaria
{
    public decimal LineaCredito { get; set; }
    public CuentaCorriente(string numeroCuenta, decimal saldoInicial, decimal lineaCredito)
        : base(numeroCuenta, saldoInicial)
    {
        this.LineaCredito = lineaCredito;
    }

    public override void Retirar(decimal monto)
    {
        if (Saldo + LineaCredito - monto < 0) Console.WriteLine("Límite de crédito excedido. No se puede retirar");
        else
        {
            Saldo -= monto;
            Console.WriteLine($"Retiro de {monto:C}. Saldo actual: {Saldo:C}");
        }
    }
}