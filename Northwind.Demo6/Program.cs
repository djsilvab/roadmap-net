// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography.X509Certificates;
using Northwind.Entities;
using Northwind.Entities.Interfaces;
using Northwind.Entities.Models;
using Northwind.LibA.Repositories;

Console.WriteLine("---- Interfaces y Clases Abstractas ----");

ITrabajador dev = new Programador("David");
dev.Trabajar();
dev.Reportar();

var empleados = new[]{
    new { Id = 1, Nombre = "Carlos", Ciudad = "Lima" },
    new { Id = 2, Nombre = "María", Ciudad = "Trujillo" },
    new { Id = 3, Nombre = "Ana", Ciudad = "Arequipa" },
    new { Id = 4, Nombre = "Luis", Ciudad = "Arequipa" },
};

var sueldos = new[]{
    new { IdEmpleado = 1, Sueldo = 100.0m },
    new { IdEmpleado = 1, Sueldo = 500.0m },
    new { IdEmpleado = 2, Sueldo = 4300.0m },
    new { IdEmpleado = 2, Sueldo = 300.0m },
    new { IdEmpleado = 3, Sueldo = 3300.0m},
    new { IdEmpleado = 3, Sueldo = 100.0m},
    new { IdEmpleado = 4, Sueldo = 6300.0m},
    new { IdEmpleado = 4, Sueldo = 9300.0m}
};

Console.WriteLine($"{new string('*', 10)} LINQ - ANY {new string('*', 10)}");
var anyQuery = SeedData.GetProducts().Any(p => p.Nombre is not null && p.Nombre.Contains("cebolla", StringComparison.OrdinalIgnoreCase) );
Console.WriteLine($"Existen productos: {anyQuery}");

Console.WriteLine($"{new string('*', 10)} LINQ ALL {new string('*', 10)}");
var respAllQuery = sueldos.All(x => x.Sueldo > 50);
Console.WriteLine($"Todos los sueldos son mayores a 500: {respAllQuery}");

Console.WriteLine("---- Agrupamiento ----");
empleados
    .GroupBy(e => e.Ciudad)
    .Select(g => new { Ciudad = g.Key, Cantidad = g.Count() })
    .ToList()
    .ForEach(x => Console.WriteLine($"{x.Ciudad}: {x.Cantidad} empleados"));

Console.WriteLine("---- Join ----");
var salaryByEmpQuery = empleados.Join(
    sueldos,
    e => e.Id,
    s => s.IdEmpleado,
    (ex, sx) => new { Empleado = ex.Nombre, sx.Sueldo }
).GroupBy(s => s.Empleado)
.OrderBy(x => x.Key)
.Select(g => new { Empleado = g.Key, Total = g.Sum(r => r.Sueldo) });

foreach (var emp in salaryByEmpQuery)
{
    Console.WriteLine($"Empleado: {emp.Empleado}, Total: {emp.Total}");
}

var sumaTotal = SeedData.GetProducts().Sum(x => x.Precio);
var maxTotal = SeedData.GetProducts().Max(x => x.Precio);
var maxTotalProduct = SeedData.GetProducts().MaxBy(x => x.Precio);
var minTotal = SeedData.GetProducts().Min(x => x.Precio);
Console.WriteLine($"PrecioMaximo: {maxTotal}");
Console.WriteLine($"Producto: {maxTotalProduct?.Nombre}, PrecioMax: {maxTotalProduct?.Precio}");

//.ForEach(x => Console.WriteLine($"Empleado: {x.Nombre}, Sueldo: {x.Sueldo}"));

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
foreach(var p in SeedData.getPersonas())
{   
    Console.WriteLine(p);
}



//usando LINQ - UNION
var res = SeedData.getPersonas().Union(SeedData.getApellidos());

//usando LINQ - JOIN
Console.WriteLine("\n***** Ejercicio de LINQ JOIN *****");
var queryJoin = from p in SeedData.GetProducts()
                join c in SeedData.GetCategories()
                        on p.IdCategoria equals c.Id
                where p.Stock > 10
                select new { Producto = p.Nombre, p.Precio, p.Stock, Categoria = c.Nombre };

foreach (var r in queryJoin)
{
    Console.WriteLine($"Producto: {r.Producto}, Categoría: {r.Categoria}");
}                

//usando LINQ - JOIN MULTIPLE
Console.WriteLine("\n***** Ejercicio de LINQ MULTIPLE *****");
// SeedData.GetEmployees() //primera fuente(Empleados)
//                     .Join(
//                         SeedData.GetDepartments(),//segunda fuente(Departamentos)
//                         emp1 => emp1.IdDepartamento,
//                         dep => dep.Id,
//                         (emp1, dep) => new { emp1, dep }
//                     ).Join(
//                         SeedData.GetDirecciones(), //tercera fuente(Direcciones)
//                         emp2 => emp2.emp1.IdDireccion,
//                         dir => dir.Id,
//                         (emp2, dir) => new { emp2, dir }
//                     ).Select(emp3 => new
//                     {
//                         emp3.emp2.emp1.Id,
//                         Empleado = emp3.emp2.emp1.Nombre,
//                         Departamento = emp3.emp2.dep.Nombre,
//                         Direccion = emp3.dir.Descripcion
//                     }).ToList().ForEach(e => Console.WriteLine($"{e.Id}, {e.Empleado}, {e.Departamento}, {e.Direccion}"));

var empleadosJoinMult = from e in SeedData.GetEmployees()
                        join d in SeedData.GetDepartments() on e.IdDepartamento equals d.Id
                        join di in SeedData.GetDirecciones() on e.IdDireccion equals di.Id
                        select new
                        {
                            e.Id,
                            e.Nombre,
                            Departamento = d.Nombre,
                            Direccion = di.Descripcion
                        };

foreach (var emp in empleadosJoinMult)
{
    Console.WriteLine($"{emp.Id}, {emp.Nombre}, {emp.Departamento}, {emp.Direccion}");
}                        


//usando LINQ - GROUP BY
Console.WriteLine("\n***** Ejercicio de LINQ GROUP BY *****");
var prodsByCategory = SeedData.GetProducts()
        .GroupBy(p => p.IdCategoria)
        .Select(g => new
        {
            Categoria = g.Key,
            Productos = g.Select(p => p.Nombre).OrderBy(n => n)
        })
        .Join(
            SeedData.GetCategories(),
            p => p.Categoria,
            c => c.Id,
            (p, c) => new
            {
                NomCategoria = c.Nombre,
                p.Productos
            }
        )
        .OrderBy(g => g.NomCategoria);

foreach (var grp in prodsByCategory)
{
    Console.WriteLine($"Categoria: {grp.NomCategoria}, \tProductos: {string.Join("|", grp.Productos)}");
}

var query = from p in SeedData.GetProducts()
            group p by p.IdCategoria;

foreach (var grp in query)
{
    Console.WriteLine($"Categoria: {grp.Key}, \tProductos: [{string.Join("|", grp.Select(x => x.Nombre))}]");
}            

//usando LINQ - GROUP BY MULTIPLE
var groupMultEmpls = SeedData.GetEmployees()
                                .GroupBy(e => new
                                {
                                    e.IdDepartamento,
                                    e.IdDireccion
                                })
                                .OrderByDescending(g => g.Key.IdDepartamento)
                                .ThenBy(g => g.Key.IdDireccion)
                                //.Select(g => new { g.Key.IdDepartamento, g.Key.IdDireccion, Empleados = g.Select(x => x.Nombre) });
                                .Select(g => g );

foreach (var grpEmp in groupMultEmpls)
{
    //Console.WriteLine($"Departamento: {grpEmp.Key.IdDepartamento}, Dirección: {grpEmp.Key.IdDireccion}, Empleados: {string.Join('|', grpEmp.Select(x => x.Nombre))}");
    Console.WriteLine($"Departamento: {grpEmp.Key.IdDepartamento}, Dirección: {grpEmp.Key.IdDireccion}");
    foreach (var emp in grpEmp)
    {
        Console.WriteLine($"Empleado: {emp.Nombre}");
    }

    Console.WriteLine(new String('*', 10));
}

var products = from p in SeedData.GetProducts()
               orderby p.Precio ascending
               select p;

var productsDesc = from e in SeedData.GetEmployees()
                   orderby e.IdDepartamento descending
                   select e;

//usando GroupJoin
var queryGroupJoin = SeedData.GetCategories().GroupJoin(
                                                SeedData.GetProducts(),
                                                c => c.Id,
                                                p => p.IdCategoria,                                
                                                (c, p) =>
                                                new
                                                {
                                                    Categoria = c.Nombre,
                                                    Productos = p.Select( p => p.Nombre)
                                                }
                                            );

foreach (var grp in queryGroupJoin)
{
    Console.WriteLine(new String('*', 30));
    Console.WriteLine($"Categoría: {grp.Categoria}");
    Console.WriteLine($"Productos: {string.Join(", ", grp.Productos)}");
}
Console.WriteLine(new String('*', 30));

var prodsByCategoryQuery = SeedData.GetProducts().GroupJoin(
                                                    SeedData.GetCategories(),
                                                    p => p.IdCategoria,
                                                    c => c.Id,
                                                    (p, cs) => new { Producto = p, Categorias = cs.DefaultIfEmpty() }                                                
                                                ).SelectMany(
                                                    x => x.Categorias,
                                                    (x,c) => new
                                                    {
                                                        Producto = x.Producto.Nombre,
                                                        Categoria = c?.Nombre ?? "Sin Categoria"
                                                    }
                                                );

foreach (var item in prodsByCategoryQuery)
{
    Console.WriteLine($"Producto: {item.Producto,-20} | Categoría: {item.Categoria}");    
}
Console.WriteLine(new string('*', 40));

var prodsByCategoryQuery2 = SeedData.GetProducts().GroupJoin(
                                                    SeedData.GetCategories(),
                                                    p => p.IdCategoria,
                                                    c => c.Id,
                                                    (p, cs) => new { Producto = p, Categorias = cs.DefaultIfEmpty() }
                                                );

foreach (var item in prodsByCategoryQuery2)
{
    Console.WriteLine($"Producto: {item.Producto.Nombre,-20} | Categoría: {item.Categorias.FirstOrDefault()?.Nombre ?? "Sin Categoria"}");
}

Console.WriteLine($"{new string('*', 10)} LINQ - SELECT MANY {new string('*', 10)}");
var queryDeparts = SeedData.GetDepartments().Join(
    SeedData.GetEmployees(),
    d => d.Id,
    e => e.IdDepartamento,
    (dx, ex) => new { dx.Id, Departamento = dx.Nombre, Empleado = ex.Nombre }
)
.OrderByDescending(x => x.Departamento)
.GroupBy(g => g.Departamento)
.Select(x => new { Departamento = x.Key, Empleados = x.Select(r => r.Empleado) });


foreach (var dep in queryDeparts)
{
    Console.WriteLine($"Departamento: {dep.Departamento}, Empleados: {string.Join("|", dep.Empleados)}");
}

Console.WriteLine($"{new string('*', 10)} LINQ - SELECT MANY 2 {new string('*', 10)}");
var querySelectMany = queryDeparts.SelectMany(d => d.Empleados);
foreach (var empleado in querySelectMany)
{
    Console.WriteLine(empleado);
}

Console.WriteLine($"{new string('*', 10)} LINQ - SELECT MANY 3 {new string('*', 10)}");
var querySelectManyProy = queryDeparts.SelectMany(
    d => d.Empleados,
    (d, e) => new { d.Departamento, Empleado = e }
);

foreach (var dep in querySelectManyProy)
{
    Console.WriteLine($"Departamento: {dep.Departamento}, Empleado: {dep.Empleado}");
}

Console.WriteLine($"{new string('*', 10)} LINQ - SELECT MANY 1 {new string('*', 10)}");
var personas = SeedData.getPersonas()
                        .SelectMany(persona => SeedData.getApellidos()
                                                        .Select(apellido =>
                                                            new
                                                            {
                                                                Nombre = persona,
                                                                Apellido = apellido
                                                            }
                                                        ));
foreach (var persona in personas)
{
    Console.WriteLine($"{persona.Nombre} {persona.Apellido}");
}

Console.WriteLine($"{new string('*', 10)} LINQ - SELECT MANY 1.1 {new string('*', 10)}");
var selectManyPersQuery = SeedData.getPersonas().SelectMany(p => SeedData.getApellidos(), (p,a) => new { Nombre = p , Apellido = a } );
foreach (var persona in selectManyPersQuery)
{
    Console.WriteLine($"{persona.Nombre} {persona.Apellido}");
}