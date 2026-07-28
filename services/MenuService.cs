using Microsoft.EntityFrameworkCore;
using proyecto.Models;

namespace proyecto.Services
{
    public class MenuService
    {
        private readonly RhdbContext _context;

        public MenuService(RhdbContext context)
        {
            _context = context;
        }

        //==========================================
        // MENÚS
        //==========================================

        public async Task<List<MenuDiario>> ObtenerMenus()
        {
            return await _context.MenuDiarios
                .OrderBy(m => m.MenuDia)
                .ToListAsync();
        }
public async Task<List<Producto>> ObtenerProductosDelMenu(string dia)
{
    var menu = await _context.MenuDiarios
        .FirstOrDefaultAsync(m => m.MenuDia == dia);

    if (menu == null)
        return new List<Producto>();

    return await _context.DetalleMenuDiarios
        .Where(d => d.IdMenuDiario == menu.IdMenuDiario)
        .Include(d => d.IdProductoNavigation)
            .ThenInclude(p => p.IdCategoriaNavigation)
        .Select(d => d.IdProductoNavigation)
        .ToListAsync();
}
        public async Task<MenuDiario?> ObtenerMenuPorId(int id)
        {
            return await _context.MenuDiarios
                .FirstOrDefaultAsync(m => m.IdMenuDiario == id);
        }

        public async Task<MenuDiario?> ObtenerMenuPorDia(string dia)
        {
            return await _context.MenuDiarios
                .FirstOrDefaultAsync(m => m.MenuDia == dia);
        }

        public async Task<MenuDiario> CrearMenu(string dia)
        {
            var menu = new MenuDiario
            {
                MenuDia = dia
            };

            _context.MenuDiarios.Add(menu);
            await _context.SaveChangesAsync();

            return menu;
        }
public async Task<MenuDiario> GuardarMenuCompleto(MenuDiario menu,List<Producto> productosSeleccionados)
{

    MenuDiario? menuExistente =
        await ObtenerMenuPorDia(menu.MenuDia);
    // Si no existe se crea
    if(menuExistente == null)
    {
        menuExistente =await CrearMenu(menu.MenuDia);
    }
    int idMenu = menuExistente.IdMenuDiario;
    // Obtener productos actuales del menú
    var detallesActuales =await _context.DetalleMenuDiarios
        .Where(d => d.IdMenuDiario == idMenu)
        .ToListAsync();
    // ================================
    // ELIMINAR PRODUCTOS QUITADOS
    // ===============================
    foreach(var detalle in detallesActuales)
    {
        bool sigueExistiendo =productosSeleccionados.Any(p =>p.IdProducto ==
                detalle.IdProducto
            );
        if(!sigueExistiendo)
        {
            _context.DetalleMenuDiarios
                .Remove(detalle);
        }

    }
    await _context.SaveChangesAsync();
    // ================================
    // AGREGAR PRODUCTOS NUEVOS
    // ================================
    foreach(var producto in productosSeleccionados)
    {
        await AgregarProducto(
            idMenu,
            producto.IdProducto
        );
    }
    return menuExistente;
}
        //==========================================
        // DETALLE MENÚ
        //==========================================
        public async Task<List<DetalleMenuDiario>> ObtenerDetalleMenu(int idMenu)
        {
            return await _context.DetalleMenuDiarios
                .Where(d => d.IdMenuDiario == idMenu)
                .Include(d => d.IdProductoNavigation)
                .ThenInclude(p => p.IdCategoriaNavigation)
                .ToListAsync();
        }
        public async Task AgregarProducto(int idMenu, int idProducto)
        {
            bool existe = await _context.DetalleMenuDiarios.AnyAsync(d =>
                d.IdMenuDiario == idMenu &&
                d.IdProducto == idProducto);

            if (existe)
                return;

            var detalle = new DetalleMenuDiario
            {
                IdMenuDiario = idMenu,
                IdProducto = idProducto
            };

            _context.DetalleMenuDiarios.Add(detalle);

            await _context.SaveChangesAsync();
        }

        public async Task QuitarProducto(int idMenu, int idProducto)
        {
            var detalle = await _context.DetalleMenuDiarios
                .FirstOrDefaultAsync(d =>
                    d.IdMenuDiario == idMenu &&
                    d.IdProducto == idProducto);

            if (detalle != null)
            {
                _context.DetalleMenuDiarios.Remove(detalle);

                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarMenu(int idMenu)
        {
            var detalles = await _context.DetalleMenuDiarios
                .Where(d => d.IdMenuDiario == idMenu)
                .ToListAsync();

            _context.DetalleMenuDiarios.RemoveRange(detalles);

            var menu = await _context.MenuDiarios
                .FirstOrDefaultAsync(m => m.IdMenuDiario == idMenu);

            if (menu != null)
            {
                _context.MenuDiarios.Remove(menu);
            }

            await _context.SaveChangesAsync();
        }
        //==========================================
        // PRODUCTOS DISPONIBLES
        //==========================================
        public async Task<List<Producto>> ObtenerProductosDisponibles()
        {
            return await _context.Productos
                .Where(p => p.Estado)
                .Include(p => p.IdCategoriaNavigation)
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }
    }
}