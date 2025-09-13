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

var httpClient = new HttpClient();
var apiURL = "http://localhost:5144";
Console.WriteLine("Ingrese su nombre:");
string nombre = Console.ReadLine() ?? string.Empty;
await Esperar();
if (!string.IsNullOrEmpty(nombre))
{    
    try
    {
        var saludo = await ObtenerSaludo(nombre);
        Console.WriteLine(saludo);    
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

async Task<string> ObtenerSaludo(string nombre)
{
    using (var respuesta = await httpClient.GetAsync($"{apiURL}/saludos2/{nombre}"))
    {
        respuesta.EnsureSuccessStatusCode();
        var saludo = await respuesta.Content.ReadAsStringAsync();
        return saludo;
    }
}

async Task Esperar() => await Task.Delay(TimeSpan.FromSeconds(0));