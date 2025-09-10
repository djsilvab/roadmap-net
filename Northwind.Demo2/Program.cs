// See https://aka.ms/new-console-template for more information
using Northwind.Entities;

Console.WriteLine("Hello, World!");

var clientes = new List<Customer>();
bool continuar = true;

while (continuar)
{
    Console.WriteLine("\n Consola Bancaria Simple");
    Console.WriteLine("1. Registrar cliente");
    Console.WriteLine("2. Depositar");
    Console.WriteLine("3. Retirar");
    Console.WriteLine("4. Mostrar balances");
    Console.WriteLine("5. Ver transacciones");
    Console.WriteLine("0. Salir");
    Console.WriteLine("Opción: ");
    var opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1": RegistrarCliente(); break;
            case "2": Depositar(); break;
            case "3": Retirar(); break;
            case "4": MostrarBalance(); break;
            case "5": VerTransacciones(); break;
            case "0": continuar = false; break;
            default: Console.WriteLine("Opción inválida."); break;
        }
    }
    catch (System.Exception ex)
    {

        Console.WriteLine($"Error: {ex.Message}");
    }
}

void RegistrarCliente()
{
    Console.WriteLine("Ingrese nombre: ");
    string nombre = Console.ReadLine() ?? string.Empty;
    Console.WriteLine("Saldo inicial: ");
    decimal saldo = decimal.Parse(Console.ReadLine() ?? "0");

    clientes.Add(new Customer(nombre, saldo));
    Console.WriteLine("Cliente registrado correctamente.");
}   

void Depositar()
{
    var cliente = SeleccionarCliente();
    if (cliente == null) return;

    Console.WriteLine("Monto a depositar: ");
    decimal monto = decimal.Parse(Console.ReadLine() ?? "0");
    cliente.Depositar(monto);

    Console.WriteLine("Depósito realizado.");    
}   

void Retirar()
{
    var cliente = SeleccionarCliente();
    if (cliente == null) return;

    Console.WriteLine("Monto a retirar: ");
    decimal monto = decimal.Parse(Console.ReadLine() ?? "0");
    cliente.Retirar(monto);

    Console.WriteLine("Retiro realizado.");
}

void MostrarBalance()
{
    Console.WriteLine("Balances de clientes");
    foreach (var cliente in clientes)
    {
        Console.WriteLine($"{cliente.Nombre} -> Saldo: {cliente.Balance:C}");
    }
}

void VerTransacciones()
{
    var cliente = SeleccionarCliente();
    if (cliente == null) return;

    Console.WriteLine($"Transacciones de {cliente.Nombre}:");
    foreach (var transaccion in cliente.Transacciones)
    {
        Console.WriteLine($"{transaccion.Fecha}: {transaccion.Tipo} {transaccion.Monto:C}");
    }
}

Customer SeleccionarCliente()
{
    if (clientes.Count == 0)
    {
        Console.WriteLine("No hay clientes registrados");
        return null;
    }

    Console.WriteLine("\nSeleccione un cliente");
    for (int i = 0; i < clientes.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {clientes[i].Nombre}");
    }

    Console.WriteLine("Opción: ");
    int indice = int.Parse(Console.ReadLine() ?? "0") - 1;
    if (indice < 0 || indice >= clientes.Count)
    {
        Console.WriteLine("Cliente no válido");
        return null;
    }
    return clientes[indice];
}