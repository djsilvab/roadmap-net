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
            new Empleado{ Id = 2, Nombre= "Fermin Gonzales", IdDepartamento = 3, IdDireccion = 2},
            new Empleado{ Id = 3, Nombre= "Yasmin Hernandez", IdDepartamento = 1, IdDireccion = 3},
            new Empleado{ Id = 4, Nombre= "Pablo Fernandez", IdDepartamento = 5, IdDireccion = 4},
            new Empleado{ Id = 5, Nombre= "Roberto Lopez", IdDepartamento = 5, IdDireccion = 4},
            new Empleado{ Id = 6, Nombre= "Roberto Fernandez", IdDepartamento = 3, IdDireccion = 5},
            new Empleado{ Id = 7, Nombre= "Mara Castilla", IdDepartamento = 2, IdDireccion = 3}
        };

        return employees;
    }

    public static List<Direccion> GetDirecciones()
    {
        return [.. Enumerable.Range(1, 5).Select(x => new Direccion { Id = x, Descripcion = $"Direccion {x}" })];
    }
}
