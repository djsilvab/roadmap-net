// See https://aka.ms/new-console-template for more information
using Northwind.Entities.Interfaces;
using Northwind.Entities.Models;
using Northwind.LibA.Repositories;

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

//usando LINQ - UNION
var res = getPersonas().Union(getApellidos());

//usando LINQ - JOIN MULTIPLE
Console.WriteLine("\n***** Ejercicio de LINQ MULTIPLE *****");
SeedData.GetEmployees() //primera fuente(Empleados)
                    .Join(
                        SeedData.GetDepartments(),//segunda fuente(Departamentos)
                        emp1 => emp1.IdDepartamento,
                        dep => dep.Id,
                        (emp1, dep) => new { emp1, dep }
                    ).Join(
                        SeedData.GetDirecciones(), //tercera fuente(Direcciones)
                        emp2 => emp2.emp1.IdDireccion,
                        dir => dir.Id,
                        (emp2, dir) => new { emp2, dir }
                    ).Select(emp3 => new
                    {
                        Id = emp3.emp2.emp1.Id,
                        Empleado = emp3.emp2.emp1.Nombre,
                        Departamento = emp3.emp2.dep.Nombre,
                        Direccion = emp3.dir.Descripcion
                    }).ToList().ForEach(e => Console.WriteLine($"{e.Id}, {e.Empleado}, {e.Departamento}, {e.Direccion}"));

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