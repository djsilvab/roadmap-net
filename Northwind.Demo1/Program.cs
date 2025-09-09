// See https://aka.ms/new-console-template for more information
using Northwind.Entities;
using Northwind.LibA;
using Northwind.LibA.Repositories;
using Northwind.LibA.Services;

//BaseClass b = new BaseClass();
//b.MetodoInternal();//❌ Error: "internal" NO es accesible fuera del ensamblado
// b.MetodoProtectedInternal(); ❌ Error: no es accesible por instancia directa fuera del ensamblado

//var t1 = new Transferencia();
// var t2 = new Deposito();

// t1.Ejecutar(500);
// t2.Ejecutar(200);

// var transacciones = new List<ITransaction>()
// {
//     new Transferencia(),
//     new Deposito()
// };

// Console.ReadLine();

// var historial = new List<ITransaction>();
// var montos = new List<decimal>();

// while (true)
// {
//     Console.WriteLine("\n=== Menú de Transacciones ===");
//     Console.WriteLine("1. Transferencia");
//     Console.WriteLine("2. Depósito");
//     Console.WriteLine("3. Retiro");
//     Console.WriteLine("4. Mostrar Historial");
//     Console.WriteLine("5. Salir");
//     Console.WriteLine("Seleccione una opción: ");

//     string opcion = Console.ReadLine()??string.Empty;    

//     ITransaction? transaction = null;

//     if (opcion.Equals("5"))
//     {
//         Console.WriteLine("Saliendo del sistema");
//         break;
//     }

//     if (opcion.Equals("4"))
//     {
//         Console.WriteLine("\n=== Historial de transacciones ===");
//         for (int i = 0; i < historial.Count; i++)
//         {
//             historial[i].Ejecutar(montos[i]);
//         }
//         continue;
//     }

//     Console.WriteLine("Ingrese el monto: ");
//     if (!decimal.TryParse(Console.ReadLine(), out decimal monto))
//     {
//         Console.WriteLine("Monto inválido, intente de nuevo");
//         continue;
//     }

//     switch (opcion)
//     {
//         case "1":
//             transaction = new Transferencia();
//             break;

//         case "2":
//             transaction = new Deposito();
//             break;

//         case "3":
//             transaction = new Retiro();
//             break;

//         default:
//             Console.WriteLine("Opción inválida");
//             continue;
//     }

//     //Guardar historial y ejecutar
//     historial.Add(transaction);
//     montos.Add(monto);
//     transaction.Ejecutar(monto);
// }

// CuentaBancaria ahorro = new CuentaAhorro("AHO-123", 1000);
// CuentaBancaria corriente = new CuentaCorriente("COR-456", 500, 1000);

// ahorro.Depositar(200);
// ahorro.Retirar(1500);

// corriente.Retirar(1200);
// corriente.Depositar(300);

// Console.ReadLine();

// var repo = new UsuarioRepository();
// var service = new EmailService();
// var manager = new UsuarioManager(repo, service);
// manager.CrearUsuario("David", "djsilvab@gamil.com");

var clientes = new List<Cliente>()
{
    new Cliente { Nombre = "Ana", Edad = 25, Ciudad = "Lima" },
    new Cliente { Nombre = "Luis", Edad = 35, Ciudad = "Cusco" },
    new Cliente { Nombre = "Maria", Edad = 40, Ciudad = "Lima" },
    new Cliente { Nombre = "Jose", Edad = 28, Ciudad = "Arequipa" },
    new Cliente { Nombre = "Lucia", Edad = 22, Ciudad = "Cusco" },
    new Cliente { Nombre = "Pedro", Edad = 20, Ciudad = "Lima" }
};

// var filtroMetodo = clientes.Where(c => c.Edad > 30 && c.Ciudad.Equals("Lima"))
//                      .Select(c => c.Nombre);

// Console.WriteLine("Resultados (Method Syntax)");
// foreach (var nombre in filtroMetodo)
// {
//     Console.WriteLine(nombre);
// }

// var filtroQuery = from c in clientes
//                   where c.Edad > 30 && c.Ciudad.Equals("Lima")
//                   select c.Nombre;

// Console.WriteLine("\nResultados (Query Syntaxis):");                  
// foreach (var nombre in filtroQuery)
// {
//     Console.WriteLine(nombre);
// }

// var agrupados = clientes
//                     .GroupBy(c => c.Ciudad)
//                     .Select(g => new { Ciudad = g.Key, Cantidad = g.Count() });

// Console.WriteLine("Agrupados");
// foreach (var grupo in agrupados)
// {
//     Console.WriteLine($"{grupo.Ciudad}: {grupo.Cantidad} clientes");
// }

// Console.WriteLine("Ordenado Ascendentemente");
// var ordenadoPorNombre = clientes.OrderBy(c => c.Nombre);
// foreach (var cliente in ordenadoPorNombre)
// {
//     Console.WriteLine($"{cliente.Nombre} - {cliente.Edad}");
// }

// Console.WriteLine("Ordenado Descendentemente");
// var ordenadoPorEdadDesc = clientes.OrderByDescending(c => c.Edad);
// foreach (var cliente in ordenadoPorEdadDesc)
// {
//     Console.WriteLine($"{cliente.Nombre} - {cliente.Edad}");
// }

// Console.WriteLine("Ordenando con múltiples criterios");
// var superOrdenado = clientes
//                         .OrderBy(c => c.Ciudad)
//                         .ThenByDescending(c => c.Edad);

// foreach (var c in superOrdenado)
// {
//     Console.WriteLine($"{c.Ciudad} - {c.Nombre} - {c.Edad}");
// }

// Console.WriteLine("Ordenado por Ciudad y luego por Edad");
// var ordenados = clientes
//                         .Where(c => c.Edad > 25)
//                         .OrderBy(c => c.Ciudad)
//                         .ThenBy(c => c.Edad);

// foreach (var c in ordenados)
// {
//     Console.WriteLine($"{c.Ciudad} - {c.Nombre} - {c.Edad}");
// }                        

// var ciudades = clientes.OrderBy(x => x.Ciudad)
//                         .Select(x => x.Ciudad)
//                         .Distinct();

// Console.WriteLine($"Ciudades: {string.Join(",", ciudades)}");
// var allLima = ciudades.All(x => x.Equals("Lima"));
// Console.WriteLine($"Son todos Lima:{(allLima ? "Si" : "No")}");
// Console.WriteLine($"Almenos Lima:{(ciudades.Any(x => x.Equals("Lima")) ? "Si" : "No")}");

// var carrito = new List<Producto>
//         {
//             new Producto { Nombre = "Laptop", Categoria = "Electrónica", Precio = 3500 },
//             new Producto { Nombre = "Mouse", Categoria = "Electrónica", Precio = 100 },
//             new Producto { Nombre = "Silla", Categoria = "Muebles", Precio = 800 },
//             new Producto { Nombre = "Escritorio", Categoria = "Muebles", Precio = 1200 },
//             new Producto { Nombre = "Mouse", Categoria = "Electrónica", Precio = 100 }, // duplicado
//         };

// //Verificar si hay productos caros
// bool hayCaros = carrito.Any(p => p.Precio > 2000);
// Console.WriteLine($"¿Hay productos de más de 2000? {hayCaros} ");

// //Verificar si todos los productos son electrónicos
// bool todosElestronica = carrito.All(x => x.Categoria.Equals("Electrónica"));
// Console.WriteLine($"¿Todos son de Electrónica: {todosElestronica}");

// //Obtener el primer mueble
// var primerMueble = carrito.FirstOrDefault(x => x.Categoria.Equals("Muebles"));
// Console.WriteLine($"Primer mueble encontrado: {primerMueble?.Nombre}");

// //Obtener categorías duplicadas
// var categorias = carrito.Select(x => x.Categoria).Distinct();
// Console.WriteLine("Categorías distintas:");
// foreach (var c in categorias) Console.WriteLine($"- {c}");

var p1 = new PersonaClass { Nombre = "Fatima", Edad = 1 };
var p2 = new PersonaClass { Nombre = "Fatima", Edad = 1 };
Console.WriteLine(p1 == p2);

var ps1 = new PersonaStruct { Nombre = "Fatima", Edad = 1 };
var ps2 = new PersonaStruct { Nombre = "Fatima", Edad = 1 };
Console.WriteLine(ps1.Equals(ps2));

// var pr1 = new PersonaRecord("Fatima", 1);
// var pr2 = new PersonaRecord("Fatima", 1);
// Console.WriteLine(pr1.Equals(pr2));

