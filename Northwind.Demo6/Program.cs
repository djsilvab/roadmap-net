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
// System.IO.File.WriteAllLines(logPath, grandes.Select(v => $"{v.Producto} - Total: {v.CalcularTotal()}"));
using var writer = new StreamWriter(logPath, append: true); 
foreach(var item in grandes)
{
    await writer.WriteLineAsync($"{item.Producto} - Total: {item.CalcularTotal()}");
}

Console.WriteLine($"Logs guardados en {logPath}");

Console.WriteLine("---- Yield ----");
foreach(var p in getPersonas())
{   
    Console.WriteLine(p);
}

var personas = getPersonas()
                .SelectMany(persona => getApellidos().Select(apellido => new { Nombre = persona,
                                                                            Apellido = apellido }));

foreach(var persona in personas)
{
    Console.WriteLine($"{persona.Nombre} {persona.Apellido}");
}

IEnumerable<string> getPersonas()
{
    yield return "Ana";
    yield return "Luis";
    yield return "Carlos";
}

IEnumerable<string> getApellidos(){
    yield return "García";
    yield return "Pérez";
    yield return "Sánchez";
    yield return "Ramírez";
}