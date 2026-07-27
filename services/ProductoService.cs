using Microsoft.EntityFrameworkCore;
using proyecto.Models;

namespace proyecto.Services
{
    public class ProductoService
    {
        private readonly RhdbContext _context;
        public ProductoService(RhdbContext context)
        {
            _context = context;
        }
        // Obtener todos los productos
        public async Task<List<Producto>> ObtenerProductos()
        {
            return await _context.Productos.Include(p => p.IdCategoriaNavigation)
                .ToListAsync();
        }
        // Obtener categorías para el combobox
        public async Task<List<Categorium>> ObtenerCategorias()
        {
            return await _context.Categoria.ToListAsync();
        }
        // Crear producto
        public async Task CrearProducto(Producto producto)
        {

            _context.Productos.Add(producto);

            await _context.SaveChangesAsync();

        }
        // Buscar un producto por id
        public async Task<Producto?> ObtenerProducto(int id)
        {
            return await _context.Productos
                .FirstOrDefaultAsync(
                    p => p.IdProducto == id
                );
        }
        // Actualizar producto
        public async Task ActualizarProducto(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }
        // Eliminar producto (borrado lógico)
        public async Task EliminarProducto(int id)
        {
            var producto =
                await _context.Productos
                .FirstOrDefaultAsync(
                    p => p.IdProducto == id
                );
            if(producto != null)
            {
                producto.Estado = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}