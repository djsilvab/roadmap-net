using System;
using System.Text.Json.Serialization;

namespace Northwind.Entities;

public class CardResponse
{
    [JsonPropertyName("tarjeta")]
    public string? Tarjeta { get; set; }
    [JsonPropertyName("aprobada")]
    public bool Aprobada { get; set; }
}
