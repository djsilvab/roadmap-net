using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


var archivos = new List<string> { "Archivo1", "Archivo2", "Archivo3" };

Console.WriteLine("\nIniciando descargas... \n");

//Crear una lista de tareas
var tareas = new List<Task<string>>();

foreach (var archivo in archivos)
{
    tareas.Add(
        Task.Run(async () =>
        {
            Console.WriteLine($"Descargando {archivo}...");
            await Task.Delay(2000);// Simula 2 seg de descarga
            // Console.WriteLine($"{archivo} descargado");
            return $"{archivo} descargado!!";
        })
    );
}

//Esperar a que todas las descargas finalicen
var resultado = await Task.WhenAll(tareas);
foreach(var res in resultado)
    Console.WriteLine(res);

Console.WriteLine("\nTodas las descargas finalizadas\n");

//Programación asincrónica con async/await


// int num = 0;
// for (int i = 0; i <= 1000000000; i++)
// {
//     num += i;
// }

Stopwatch sw = Stopwatch.StartNew();
//Task-01
var task1 = new Task(() =>
{
    Stopwatch crono = new Stopwatch();
    crono.Start();
    Thread.Sleep(1000);
    crono.Stop();
    Console.WriteLine($"Tiempo total Task-1: {crono.Elapsed} ms");

});

var task2 = new Task(() =>
{
    Stopwatch crono = new Stopwatch();
    crono.Start();
    Thread.Sleep(1000);
    crono.Stop();
    Console.WriteLine($"Tiempo total Task-2: {crono.Elapsed} ms");
    
});

var task3 = new Task(() =>
{
    Stopwatch crono = new Stopwatch();
    crono.Start();
    Thread.Sleep(1000);
    crono.Stop();
    Console.WriteLine($"Tiempo total Task-3: {crono.Elapsed} ms");

});

var task4 = new Task(async () =>
{
    var strRandom = await RandomAsync();
    Console.WriteLine(strRandom);
});

task1.Start();
task2.Start();
task3.Start();
task4.Start();

await task1;
await task2;
await task3;
await task4;

// var strRandom = await RandomAsync();
// Console.WriteLine(RandomAsync().Result);

sw.Stop();
Console.WriteLine($"Todo el programa   : {sw.Elapsed} ms");

static async Task<string> RandomAsync()
{
    Stopwatch crono = Stopwatch.StartNew();
    var num = new Random().Next(1000).ToString();
    Thread.Sleep(1000);
    crono.Stop();
    return $"{num} calculado en {crono.Elapsed}";
}

Thread hiloPrincipal = Thread.CurrentThread;
hiloPrincipal.Name = "Hilo Principal";
Console.WriteLine($"Hilo Actual: {hiloPrincipal.Name}");

