namespace Northwind.Entities;

public class Usuario
{
    public string? Nombre { get; set; }
    public string? Email { get; set; }

    public Usuario(string Nombre, string Email)
    {
        this.Nombre = Nombre;
        this.Email = Email;
    }
}
