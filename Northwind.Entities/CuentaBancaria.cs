using System;

namespace Northwind.Entities;

public class CuentaBancaria
{
    public string NumeroCuenta { get; private set; }
    public decimal Saldo { get; private set; }

    public CuentaBancaria(string NumeroCuenta, decimal Saldo)
    {
        this.NumeroCuenta = NumeroCuenta;
        this.Saldo = Saldo;
    }

    public void Depositar(decimal monto)
    {
        if (monto <= 0) throw new ArgumentException("El monto debe ser positivo");

        Saldo += monto;
    }
}
