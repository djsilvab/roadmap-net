using System;

namespace Northwind.Entities;

public class Customer
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; }
    public decimal Balance { get; private set; }
    public List<Transaction> Transacciones { get; private set; }

    public Customer(string nombre, decimal saldoInicial)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        if (saldoInicial < 0)
            throw new ArgumentException("El saldo inicial no puede ser negativo.");

        Id = Guid.NewGuid();
        Nombre = nombre;
        Balance = saldoInicial;
        Transacciones = new List<Transaction>();
    }

    public void Depositar(decimal monto)
    {
        if (monto <= 0)
            throw new ArgumentException("El monto debe ser positivo.");

        Balance += monto;
        Transacciones.Add(new Transaction("Depósito", monto, DateTime.Now));
    }

    public void Retirar(decimal monto)
    {
        if (monto <= 0) throw new ArgumentException("El monto debe ser positivo.");
        if (monto > Balance) throw new InvalidOperationException("Fondos insuficientes.");
        Balance -= monto;
        Transacciones.Add(new Transaction("Retiro", -monto, DateTime.Now));
    }
}

public record Transaction(string Tipo, decimal Monto, DateTime Fecha);