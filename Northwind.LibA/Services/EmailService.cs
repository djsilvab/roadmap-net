using System;
using Northwind.Entities;

namespace Northwind.LibA.Services;

public class EmailService
{
    public void EnviarBienvenida(Usuario usuario)
    {
        Console.WriteLine($"Correo de bienvenida enviado a {usuario.Email}");
    }
}
