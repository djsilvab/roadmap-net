using System;

namespace Northwind.Entities.Dtos;

public record class ClienteDto(string Nombre, string Email, int Edad);

