using System.Diagnostics;

// Parallel.For(0, 5, i => Console.WriteLine($"Tarea {i} ejecutándose en el hilo {Task.CurrentId}"));
// var nombres = new List<string> { "Ana", "Luis", "Pedro", "Sofía" };
// Parallel.ForEach(nombres, nombre => Console.WriteLine($"{nombre} procesado en el hilo {Task.CurrentId}"));

var sw = new Stopwatch();
// Parallel.For(0, 5, i =>
// {
//     var result = Enumerable.Range(1, 5_000_000).Sum(x => Math.Sqrt(x));
//     Console.WriteLine($"[Parallel] Tarea {i} completada");
// });
// sw.Stop();
// Console.WriteLine($"Tiempo CPU-Bound (Parallel): {sw.ElapsedMilliseconds} ms\n");

// sw.Restart();
// var urls = new[]
//         {
//             "https://www.google.com",
//             "https://www.bing.com",
//             "https://www.github.com",
//             "https://www.microsoft.com",
//             "https://www.stackoverflow.com"
//         };

// using var http = new HttpClient();
// var tareas = urls.Select(async url =>
// {
//     string data = await http.GetStringAsync(url);
//     Console.WriteLine($"[Async] Descargado {url} ({data.Length} caracteres)");
// });

// await Task.WhenAll(tareas);
// sw.Stop();
// Console.WriteLine($"Tiempo I/O-Bound (Async/Await): {sw.ElapsedMilliseconds} ms\n");

Console.WriteLine("=== EJEMPLOS AVANZADOS: PARALELISMO vs ASYNC/AWAIT ===\n");
// Ejemplo 1: Parallel.ForEach para procesamiento CPU-intensive
await EjemploParallelProcessing();

// Ejemplo 4: Parallel con opciones avanzadas
await EjemploParallelAvanzado();

// ============================================================
// EJEMPLO 1: Parallel.ForEach - Procesamiento CPU-intensive
// ============================================================
async Task EjemploParallelProcessing()
{
    Console.WriteLine("--- EJEMPLO 1: Parallel.ForEach (CPU-bound) ---");
    var imagenes = Enumerable.Range(1, 100).Select(i => $"imagen_{i}.jpg");

    // Escenario: Procesar 100 imágenes (redimensionar, aplicar filtros)
    sw.Start();

    // Secuencial (lento)
    foreach (var img in imagenes.Take(10))
    {
        ProcesarImagenCPU(img);
    }
    sw.Stop();
    Console.WriteLine($"Secuencial (10 imágenes): {sw.ElapsedMilliseconds}ms");

    // Paralelo (rápido)
    sw.Restart();
    Parallel.ForEach(imagenes.Take(10), img => ProcesarImagenCPU(img));
    sw.Stop();
    Console.WriteLine($"Paralelo (10 imágenes): {sw.ElapsedMilliseconds}ms");
    Console.WriteLine($"Speedup: {10 * 50 / (double)sw.ElapsedMilliseconds:F2}x\n");

    await Task.CompletedTask;
}

void ProcesarImagenCPU(string imagen)
{
    Thread.Sleep(50);
}

// ============================================================
// EJEMPLO 3: Combinando Parallel + Async
// ============================================================
async Task EjemploCombinado()
{
    Console.WriteLine("--- EJEMPLO 3: Combinando Parallel + Async ---");

    // Escenario: Procesar múltiples usuarios, cada uno requiere:
    // 1. Llamada HTTP (I/O) para obtener datos
    // 2. Procesamiento pesado (CPU) de los datos

    var userIds = Enumerable.Range(1, 10).ToList();
    sw.Restart();

    // Primero: Obtener todos los datos (I/O) - Async
    using var httpClient = new HttpClient();
    var dataTasks = userIds.Select(async id =>
    {
        var url = $"https://jsonplaceholder.typicode.com/users/{id}";
        return await httpClient.GetStringAsync(url);
    });

    var userData = await Task.WhenAll(dataTasks);
    // Segundo: Procesar datos (CPU) - Parallel
    var resultados = new string[userData.Length];
    Parallel.For(0, userData.Length, i =>
    {
        resultados[i] = ProcesarDataCPU(userData[i]);
    });

    sw.Stop();
    Console.WriteLine($"Combinado (Async→Parallel): {sw.ElapsedMilliseconds}ms");
    Console.WriteLine($"Procesados: {resultados.Length} usuarios\n");

}

string ProcesarDataCPU(string data)
{
    // Simula procesamiento CPU-intensive
    Thread.Sleep(30);
    return $"Procesado: {data.Length} bytes";
}

// ============================================================
// EJEMPLO 4: Parallel con opciones avanzadas
// ============================================================
async Task EjemploParallelAvanzado()
{
    Console.WriteLine("--- EJEMPLO 4: Parallel con opciones avanzadas ---");

    var items = Enumerable.Range(1, 1000).ToList();

    // Opción 1: Limitar grado de paralelismo
    var options = new ParallelOptions
    {
        MaxDegreeOfParallelism = 4, // Máximo 4 threads
        CancellationToken = CancellationToken.None
    };

    sw.Restart();
    Parallel.ForEach(items, options, item => { var result = item * item; });
    sw.Stop();
    Console.WriteLine($"Parallel con MaxDegree=4: {sw.ElapsedMilliseconds}ms");

    // Opción 2: Thread-local data (para evitar locks)
    long suma = 0;
    sw.Restart();
    Parallel.ForEach(
        items,
        () => 0L,
        (item, loopState, threadLocalSum) =>
        {
            return threadLocalSum + item;
        },
        threadLocalSum =>
        {
            Interlocked.Add(ref suma, threadLocalSum);
        }
    );

    sw.Stop();
    Console.WriteLine($"Parallel con thread-local: {sw.ElapsedMilliseconds}ms");
    Console.WriteLine($"Suma total: {suma}\n");

    await Task.CompletedTask;
}

// ============================================================
// EJEMPLO 5: ERROR COMÚN - Parallel con I/O (ANTI-PATTERN)
// ============================================================
async Task EjemploErrorComun()
{
    Console.WriteLine("--- EJEMPLO 5: ERROR COMÚN ❌ ---");

    var urls = Enumerable.Range(1, 5).Select(i => $"https://jsonplaceholder.typicode.com/posts/{i}").ToList();

    using var httpClient = new HttpClient();

    // ❌ MAL: Parallel.ForEach con I/O (bloquea threads)
    Console.WriteLine("❌ Parallel.ForEach con I/O (MAL):");
    sw.Restart();
    Parallel.ForEach(urls, url =>
    {
        // .Result bloquea el thread - DESPERDICIA RECURSOS
        var result = httpClient.GetStringAsync(url);
    });
    sw.Stop();
    Console.WriteLine($"Tiempo: {sw.ElapsedMilliseconds}ms (threads bloqueados)\n");

    // ✅ BIEN: Async/Await con Task.WhenAll
    Console.WriteLine("✅ Async/Await con WhenAll (BIEN):");
    sw.Restart();
    var tasks = urls.Select(url => httpClient.GetStringAsync(url));
    await Task.WhenAll(tasks);
    sw.Stop();
    Console.WriteLine($"Tiempo: {sw.ElapsedMilliseconds}ms (threads libres)\n");
    Console.WriteLine("💡 Lección: Nunca uses Parallel para I/O operations!\n");
}