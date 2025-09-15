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
using Northwind.Entities;

var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

int maxConcurrentRequests = config.GetValue<int>("AppSettings:MaxConcurrentRequests");
int timeoutCancellationToken = config.GetValue<int>("AppSettings:TimeoutCancellationToken");

var apiURL = "http://localhost:5144";

Console.Write("cantidad de tarjetas:");
short cantTarjetas = short.Parse(Console.ReadLine() ?? "0");

CancellationTokenSource cancellationTokenSource = new();
cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(timeoutCancellationToken));

var stopWatch = Stopwatch.StartNew();

try
{
    var lstTarjetas = ObtenerTarjetasDeCredito(cantTarjetas);
    await ProcesarTarjetas(lstTarjetas, cancellationTokenSource.Token);
}
catch (HttpRequestException ex)
{
    Console.WriteLine(ex.Message);
}
catch (TaskCanceledException ex)
{
    Console.WriteLine("La operación ha sido cancelada");
}

Console.WriteLine($"Operación finalizada en : {stopWatch.ElapsedMilliseconds / 1000.0} seconds");

async Task ProcesarTarjetas(List<string> lstTarjetas, CancellationToken cancellationToken = default)
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
            var response = await httpClient.PostAsync($"{apiURL}/tarjetas", content, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Tarjeta {tarjeta}: Error {(int)response.StatusCode}");
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Tarjeta {tarjeta}: {result}");

            return response;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error procesando tarjeta {tarjeta}: {ex.Message}");
            return null;
        }
        finally
        {
            semaforo.Release();
        }
    });

    var responses = await Task.WhenAll(tareas);
    var lstRechazadas = new List<string>();
    foreach (var response in responses)
    {
        if (response == null) continue;
        var result = await response.Content.ReadAsStringAsync();
        var resultCard = System.Text.Json.JsonSerializer.Deserialize<CardResponse>(result);
        if (resultCard != null && !resultCard.Aprobada)
        {
            if (!string.IsNullOrEmpty(resultCard.Tarjeta))
                lstRechazadas.Add(resultCard.Tarjeta);
        }
    }

    foreach (var item in lstRechazadas)
    {
        Console.WriteLine(item);
    }

    
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
