//Tarea que simula la descarga

// var tareaDescargaArchivo = Task.Run(() =>
// {
//     Console.WriteLine("Iniciando la descarga del archivo...");
//     //Simula el proceso largo
//     Task.Delay(2000).Wait();
//     Console.WriteLine("Descarga completada.");
//     return "Contenido del archivo";
// });

// //La continuación se ejecutará solo si la tarea de descarga se completa con exito
// tareaDescargaArchivo.ContinueWith(t =>
// {
//     if (t.IsCompletedSuccessfully)
//     {
//         string Contenido = t.Result;
//         Console.WriteLine($"Procesando el archivo con el contenido: '{Contenido}'");
//     }
// }, TaskContinuationOptions.OnlyOnRanToCompletion);

// Console.WriteLine("El hilo principal continúa su ejecución sin esperar su descarga");

// Task.WaitAll(tareaDescargaArchivo);

// try
// {
//     await Task.Run(() => throw new Exception("Fallo algo mal"));
// }
// catch (System.Exception ex)
// {
//     Console.WriteLine($"Error capturado: {ex.Message}");
// } 

// string ruta = "/home/developer/Documentos/nuevos_archivos/app.log";
// string contenido = await File.ReadAllTextAsync(ruta);
// Console.WriteLine("Contenido completo del log:");
// Console.WriteLine(contenido);

// //Leer linea por linea
// Console.WriteLine("\nLectura línea por línea");
// using var stream = new StreamReader(ruta);
// string? linea;
// while ((linea = await stream.ReadLineAsync()) != null) {
//     Console.WriteLine(linea);
// }

// Console.WriteLine("Ingrese su nombre:");
// string nombre = Console.ReadLine() ?? string.Empty;
// await Esperar();
// if (!string.IsNullOrEmpty(nombre))
// {    
//     try
//     {
//         var saludo = await ObtenerSaludo(apiURL, nombre);
//         Console.WriteLine(saludo);    
//     }
//     catch (HttpRequestException ex)
//     {
//         Console.WriteLine($"Error: {ex.Message}");
//     }
// }

using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

int maxConcurrentRequests = config.GetValue<int>("AppSettings:MaxConcurrentRequests");
var apiURL = "http://localhost:5144";
Console.WriteLine("cantidad de tarjetas:");
short cantTarjetas = short.Parse(Console.ReadLine() ?? "0");
var lstTarjetas = ObtenerTarjetasDeCredito(cantTarjetas);
var stopWatch = Stopwatch.StartNew();

try
{
    await ProcesarTarjetas(lstTarjetas);
}
catch (HttpRequestException ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine($"Operación finalizada en : {stopWatch.ElapsedMilliseconds/1000.0} seconds");

async Task ProcesarTarjetas(List<string> lstTarjetas)
{
    using var httpClient = new HttpClient();
    using var semaforo = new SemaphoreSlim(maxConcurrentRequests);
    
    var tareas = lstTarjetas.Select(async (tarjeta) =>
    {
        await semaforo.WaitAsync();
        try
        {
            var json = System.Text.Json.JsonSerializer.Serialize(tarjeta);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{apiURL}/tarjetas", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Tarjeta {tarjeta}: Error {(int)response.StatusCode}");
                return;
            }

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Tarjeta {tarjeta}: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error procesando tarjeta {tarjeta}: {ex.Message}");
        }
        finally
        {
            semaforo.Release();
        }
    });

    await Task.WhenAll(tareas);
}

List<string> ObtenerTarjetasDeCredito(short cantTarjetas)
{    
    return [.. Enumerable.Range(1, cantTarjetas).Select(x =>
        x.ToString().PadLeft(16, '0')
    )];
}

async Task<string> ObtenerSaludo(string apiURL, string nombre)
{
    using var httpClient = new HttpClient();
    var respuesta = await httpClient.GetAsync($"{apiURL}/saludos2/{nombre}");
    respuesta.EnsureSuccessStatusCode();
    return await respuesta.Content.ReadAsStringAsync();
}

async Task Esperar() => await Task.Delay(TimeSpan.FromSeconds(0));