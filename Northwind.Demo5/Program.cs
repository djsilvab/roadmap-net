using Northwind.Entities;


var lstNumeros = Enumerable.Range(1, 20).Select(x => x);

// lstNumeros.FindAll(x => x % 2 == 0).ForEach(x => Console.WriteLine($"Número:{x}"));

// lstNumeros.Where(x => x % 2 == 0).OrderByDescending(x => x).ToList().ForEach(
//     x => Console.WriteLine($"Número par: {x}")
// );
var random = new Random();
var lstCategoria = Enumerable.Range(1, 5).Select(x => new Categoria { Id = x, Nombre = $"Categoria {(char)random.Next(65,91)}{(char)random.Next(97,123)}{(char)random.Next(97,123)}" });



var lstProductos = lstNumeros.Select(x => new Producto
{
    Nombre = $"Producto {(char)random.Next(65, 91)}{(char)random.Next(97, 123)}{(char)random.Next(97, 123)}",
    Precio = decimal.Round(x * (decimal)(random.NextDouble() * 10.0), 2),
    Stock = random.Next(50),
    IdCategoria = random.Next(1,6)
});

// lstProductos.ToList()
//             .ForEach(x =>
//                 Console.WriteLine($"Producto: {x.Nombre}, Precio: {x.Precio}, Stock: {x.Stock}"));

// lstProductos.Where(x => x.Precio > 20).OrderBy(x => x.Nombre).ToList()
//             .ForEach(x => Console.WriteLine($"Producto: {x.Nombre}, Precio: {x.Precio}"));

// lstProductos.Where(x => x.Stock < 10)
//             .Join(lstCategoria,
//                 p => p.IdCategoria,
//                 c => c.Id,
//                 (prod , cate) => new
//                 {
//                     Producto = prod.Nombre,
//                     Precio = prod.Precio,
//                     Stock = prod.Stock,
//                     Categoria = cate.Nombre
//                 }
//             )
//             .Select(x => new { x.Producto, x.Categoria , x.Stock , EsBajoStock = true })
//             .ToList()
//             .ForEach(x => Console.WriteLine($"Producto: {x.Producto}, Categoria: {x.Categoria}, Stock: {x.Stock} , EsBajo: {x.EsBajoStock}"));

lstProductos.Where(x => x.Stock < 20)
            .GroupBy(x => x.IdCategoria)
            .Join(lstCategoria,
                gr => gr.Key,
                c => c.Id,
                (g, c) => new
                {
                    Categoria = c.Nombre,
                    Cantidad = g.Count()
                }
            ).ToList()
            .ForEach(x =>
                Console.WriteLine($"{x.Categoria}: {x.Cantidad} productos")
            );

string ruta = "/home/developer/Documentos/nuevos_archivos/log.txt";
// Tarea principal: leer archivo asincrónicamente
        var tareaLectura = Task.Run(async () =>
        {
            using var reader = new StreamReader(ruta);
            return await reader.ReadToEndAsync();
        });

        // Continuación: procesar el contenido leído
        _ = tareaLectura.ContinueWith(t =>
        {
            if (t.Exception != null)
            {
                Console.WriteLine($"❌ Error al leer logs: {t.Exception.InnerException?.Message}");
                return;
            }

            string contenido = t.Result;
            Console.WriteLine("✅ Logs leídos:");
            Console.WriteLine(contenido);

            // Ejemplo de análisis: contar errores
            int errores = contenido
                            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                            .Count(linea => linea.Contains("Error", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine($"⚠️ Se detectaron {errores} errores en los logs.");
        });

        // Esperar a que termine la lectura + continuación
        await tareaLectura;