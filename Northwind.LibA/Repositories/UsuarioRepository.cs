using System;
using Northwind.Entities;

namespace Northwind.LibA.Repositories;

public class UsuarioRepository
{
    public void Guardar(Usuario usuario)
    {
        Console.WriteLine($"Usuario {usuario.Nombre} guardado en la base de datos");
    }
}
