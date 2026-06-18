using ProductoCRUD.Data;
using ProductoCRUD.Models;

namespace ProductoCRUD.Services;

public class ProductoService
{
    public void CrearProducto(string nombre, string? descripcion, decimal precio, int stock)
    {
        using var context = new AppDbContext();

        var nuevoProducto = new Producto
        {
            Nombre = nombre,
            Descripcion = descripcion,
            Precio = precio,
            Stock = stock,
            FechaCreacion = DateTime.UtcNow
        };

        context.Productos.Add(nuevoProducto);
        context.SaveChanges();

        Console.WriteLine($"Producto creado con ID: {nuevoProducto.Id}");
    }

    public List<Producto> ObtenerTodos()
    {
        using var context = new AppDbContext();

        return context.Productos
            .OrderBy(p => p.Nombre)
            .ToList();
    }

    public Producto? ObtenerPorId(int id)
    {
        using var context = new AppDbContext();

        return context.Productos
            .FirstOrDefault(p => p.Id == id);
    }

    public bool ActualizarProducto(int id, string nuevoNombre, decimal nuevoPrecio, int nuevoStock)
    {
        using var context = new AppDbContext();

        var producto = context.Productos.FirstOrDefault(p => p.Id == id);

        if (producto == null)
        {
            Console.WriteLine($"Producto con ID {id} no encontrado.");
            return false;
        }

        producto.Nombre = nuevoNombre;
        producto.Precio = nuevoPrecio;
        producto.Stock = nuevoStock;

        context.SaveChanges();

        Console.WriteLine($"Producto {id} actualizado correctamente.");
        return true;
    }

    public bool EliminarProducto(int id)
    {
        using var context = new AppDbContext();

        var producto = context.Productos.FirstOrDefault(p => p.Id == id);

        if (producto == null)
        {
            Console.WriteLine($"Producto con ID {id} no encontrado.");
            return false;
        }

        context.Productos.Remove(producto);
        context.SaveChanges();

        Console.WriteLine($"Producto {id} eliminado correctamente.");
        return true;
    }
}