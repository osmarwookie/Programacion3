namespace InventarioApp;

public class InventarioService
{
    private List<Producto> _productos = new();
    private int _nextId = 1;

    public void Agregar(string nombre, string categoria, decimal precio, int cantidad)
    {
        var p = new Producto(_nextId++, nombre, categoria, precio, cantidad);
        _productos.Add(p);
        Console.WriteLine($"Producto agregado con ID {p.Id}.");
    }

    public void Eliminar(int id)
    {
        var p = _productos.FirstOrDefault(x => x.Id == id);

        if (p is null)
        {
            Console.WriteLine("ID no encontrado.");
            return;
        }

        _productos.Remove(p);
        Console.WriteLine("Producto eliminado.");
    }

    public void MostrarTodos()
    {
        if (_productos.Count == 0)
        {
            Console.WriteLine("Inventario vacío.");
            return;
        }

        foreach (var p in _productos)
        {
            Console.WriteLine(p);
        }
    }

    public void FiltrarPorCategoria(string categoria)
    {
        var resultado = _productos
            .Where(p => p.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Nombre)
            .ToList();

        Console.WriteLine($"\nProductos en '{categoria}': {resultado.Count}");
        resultado.ForEach(Console.WriteLine);
    }

    public void BuscarPorNombre(string texto)
    {
        var resultado = _productos
            .Where(p => p.Nombre.Contains(texto, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (resultado.Count == 0)
            Console.WriteLine("No se encontraron productos.");
        else
            resultado.ForEach(Console.WriteLine);
    }

    public void OrdenarPorPrecio(bool descendente = false)
    {
        var query = descendente
            ? _productos.OrderByDescending(p => p.Precio)
            : _productos.OrderBy(p => p.Precio);

        query.ToList().ForEach(Console.WriteLine);
    }

    public void MostrarResumen()
    {
        if (_productos.Count == 0)
        {
            Console.WriteLine("Inventario vacío.");
            return;
        }

        decimal valorTotal = _productos.Sum(p => p.Precio * p.Cantidad);
        decimal precioPromedio = _productos.Average(p => p.Precio);
        var masCaro = _productos.MaxBy(p => p.Precio);
        var masBarato = _productos.MinBy(p => p.Precio);
        int totalUnidades = _productos.Sum(p => p.Cantidad);
        var categorias = _productos.Select(p => p.Categoria).Distinct().OrderBy(c => c);

        Console.WriteLine("\n=== RESUMEN DEL INVENTARIO ===");
        Console.WriteLine($"Total de productos: {_productos.Count}");
        Console.WriteLine($"Total de unidades: {totalUnidades}");
        Console.WriteLine($"Valor total: ${valorTotal:F2}");
        Console.WriteLine($"Precio promedio: ${precioPromedio:F2}");
        Console.WriteLine($"Más caro: {masCaro?.Nombre} (${masCaro?.Precio:F2})");
        Console.WriteLine($"Más barato: {masBarato?.Nombre} (${masBarato?.Precio:F2})");
        Console.WriteLine($"Categorías: {string.Join(", ", categorias)}");
    }
}