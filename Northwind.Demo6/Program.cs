// See https://aka.ms/new-console-template for more information
using Northwind.Entities.Interfaces;
using Northwind.Entities.Models;

Console.WriteLine("---- Interfaces y Clases Abstractas ----");

ITrabajador dev = new Programador("David");
dev.Trabajar();
dev.Reportar();

var empleados = new[]{
    new { Id = 1, Nombre = "Carlos", Ciudad = "Lima"},
    new { Id = 2, Nombre = "María", Ciudad = "Trujillo"},
    new { Id = 3, Nombre = "Ana", Ciudad = "Arequipa"},
    new { Id = 4, Nombre = "Luis", Ciudad = "Arequipa"},
};

var sueldos = new[]{
    new { IdEmpleado = 1, Sueldo = 5500.50 },
    new { IdEmpleado = 2, Sueldo = 4300.00 },
};

Console.WriteLine("---- Agrupamiento ----");
empleados
    .GroupBy(e => e.Ciudad)
    .Select(g => new { Ciudad = g.Key, Cantidad = g.Count() })
    .ToList()
    .ForEach(x => Console.WriteLine($"{x.Ciudad}: {x.Cantidad} empleados"));

Console.WriteLine("---- Join ----");
empleados.Join(
    sueldos,
    e => e.Id,
    s => s.IdEmpleado,
    (em, su) => new { em.Nombre, su.Sueldo }
).ToList()
.ForEach(x => Console.WriteLine($"Empleado: {x.Nombre}, Sueldo: {x.Sueldo}"));

var ventas = new List<Venta>
{
    new Venta { Producto = "Laptop", Cantidad = 2, Precio = 3500 },
    new Venta { Producto = "Mouse", Cantidad = 5, Precio = 80 },
    new VentaConDescuento { Producto = "Monitor", Cantidad = 3, Precio = 800, Descuento = 10 }
};

var grandes = ventas.Where(v => v.CalcularTotal() > 1000);

string logPath = "ventas_log.txt";
System.IO.File.WriteAllLines(logPath, grandes.Select(v => $"{v.Producto} - Total: {v.CalcularTotal()}"));
Console.WriteLine($"Logs guardados en {logPath}");