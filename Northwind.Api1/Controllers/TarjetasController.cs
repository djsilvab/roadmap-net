using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Api1.Helpers;

namespace Northwind.Api1.Controllers
{
    [Route("tarjetas")]
    [ApiController]
    public class TarjetasController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> ProcesarTarjetas([FromBody] string tarjeta)
        {
            var valorAleatorio = RandomGeneration.NextDouble();
            var esAprobada = valorAleatorio > 0.1;
            await Task.Delay(1000);
            Console.WriteLine($"Tarjeta {tarjeta} procesada");
            return Ok(new { Tarjeta = tarjeta, Aprobada = esAprobada });
        }
    }
}
