using System;
using Northwind.Entities;
using Northwind.Entities.Models;

namespace Northwind.LibA.Repositories;

public static class SeedData
{
    public static List<Departamento> GetDepartments()
    {
        var departments = new List<Departamento>
        {
            new Departamento{ Id = 1, Nombre = "Recursos Humanos" },
            new Departamento{ Id = 2, Nombre = "Finanzas" },
            new Departamento{ Id = 3, Nombre = "Operaciones" },
            new Departamento{ Id = 4, Nombre = "Contabilidad" },
            new Departamento{ Id = 5, Nombre = "Sistemas" },
        };
        return departments;
    }

    public static List<Categoria> GetCategories()
    {
        return [.. Enumerable.Range(1, 5).Select(x => new Categoria { Id = x, Nombre = $"Categoria {x}", Descripcion = $"Categoría {x}" })];
    }

    public static List<Empleado> GetEmployees()
    {
        var employees = new List<Empleado>
        {
            new Empleado{ Id = 1, Nombre= "Misael Cazares", IdDepartamento = 1, IdDireccion = 1},
            new Empleado{ Id = 2, Nombre= "Fermin Gonzales", IdDepartamento = 3, IdDireccion = 5},
            new Empleado{ Id = 3, Nombre= "Yasmin Hernandez", IdDepartamento = 1, IdDireccion = 1},
            new Empleado{ Id = 4, Nombre= "Pablo Fernandez", IdDepartamento = 5, IdDireccion = 3},
            new Empleado{ Id = 5, Nombre= "Roberto Lopez", IdDepartamento = 2, IdDireccion = 2},
            new Empleado{ Id = 6, Nombre= "Roberto Fernandez", IdDepartamento = 4, IdDireccion = 4},
            new Empleado{ Id = 7, Nombre= "Maria Castillo", IdDepartamento = 2, IdDireccion = 4}
        };

        return employees;
    }

    public static List<Direccion> GetDirecciones()
    {
        return [.. Enumerable.Range(1, 5).Select(x => new Direccion { Id = x, Descripcion = $"Direccion {x}" })];
    }

    public static List<Producto> GetProducts()
    {
        var today = DateTime.Today;

        return new List<Producto>
        {
            new Producto{ Id = 1, Nombre = "Leche", Descripcion = "Leche entera", Precio = 25.0m, FechaDeAlta = today, IdCategoria = 1 },
            new Producto{ Id = 2, Nombre = "Cafe", Descripcion = "Cafe Expreso", Precio = 250.0m, FechaDeAlta = today, IdCategoria = 3 },
            new Producto{ Id = 3, Nombre = "Coca cola", Descripcion = "coca cola de 600ml", Precio = 20.0m, FechaDeAlta = today, IdCategoria = 4 },
            new Producto{ Id = 4, Nombre = "Azucar", Descripcion = "azucar blanca", Precio = 35.0m, FechaDeAlta = today, IdCategoria = 2 },
            new Producto{ Id = 5, Nombre = "Frijol", Descripcion = "frijol", Precio = 25.0m, FechaDeAlta = today.AddDays(1), IdCategoria = 5 },
            new Producto{ Id = 6, Nombre = "Servilletas", Descripcion = "servilletas", Precio = 27.0m, FechaDeAlta = today.AddDays(8), IdCategoria = 2 },
            new Producto{ Id = 7, Nombre = "Cafe en grano", Descripcion = "cafe en grano tostado medio", Precio = 25.0m, FechaDeAlta = today.AddDays(6), IdCategoria = 5 },
            new Producto{ Id = 8, Nombre = "Estufa", Descripcion = "estufa marca mabe", Precio = 2500.0m, FechaDeAlta = today.AddDays(2), IdCategoria = 0 },
            new Producto{ Id = 9, Nombre = "Refrijerado", Descripcion = "refrijerador", Precio = 3425.0m, FechaDeAlta = today.AddDays(3), IdCategoria = 1 },
            new Producto{ Id = 10, Nombre = "Papel higienico", Descripcion = "papel higienico", Precio = 55.0m, FechaDeAlta = today.AddDays(11), IdCategoria = 3 },
            new Producto{ Id = 11, Nombre = "Leche Premiun", Descripcion = "leche entera", Precio = 45.0m, FechaDeAlta = today.AddDays(10), IdCategoria = 0 },
        };

    }
    
    public static IEnumerable<string> getPersonas()
    {
        yield return "Ana";
        yield return "Luis";
        yield return "Carlos";
    }

    public static IEnumerable<string> getApellidos(){
        yield return "García";
        yield return "Pérez";
        yield return "Sánchez";
        yield return "Ramírez";
    }
}
