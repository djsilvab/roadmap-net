using System;
using Northwind.Entities;
using Northwind.LibA.Repositories;
using Northwind.LibA.Services;

namespace Northwind.LibA;

public class UsuarioManager
{
    private readonly UsuarioRepository repository;
    private readonly EmailService service;

    public UsuarioManager(UsuarioRepository repository, EmailService service)
    {
        this.repository = repository;
        this.service = service;
    }

    public void CrearUsuario(string nombre, string email)
    {
        var usuario = new Usuario(nombre, email);
        Console.WriteLine($"Usuario {usuario.Nombre} creado en memoria");
        this.repository.Guardar(usuario);
        this.service.EnviarBienvenida(usuario);
    }
}
