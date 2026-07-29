using Microsoft.EntityFrameworkCore;
using proyecto.Models;

namespace proyecto.Services
{
    public class PedidoService
    {
        private readonly RhdbContext _context;
        public PedidoService(RhdbContext context)
        {
            _context = context;
        }
        //=====================================================
        // PEDIDOS
        //=====================================================
        public async Task<List<Pedido>> ObtenerPedidos()
        {
            return await _context.Pedidos
                .Include(p => p.IdUsuarioNavigation)
                .Include(p => p.IdMesaNavigation)
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();
        }
        public async Task<Pedido?> ObtenerPedido(int idPedido)
        {
            return await _context.Pedidos
                .Include(p => p.DetallePedidos)
                .ThenInclude(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(p => p.IdPedido == idPedido);
        }
        //=====================================================
        // MESAS
        //=====================================================
        public async Task<List<Mesa>> ObtenerMesasDisponibles()
        {
            return await _context.Mesas
                .Where(m => m.Estado == "Disponible")
                .OrderBy(m => m.NumeroMesa)
                .ToListAsync();
        }
        //=====================================================
        // CREAR PEDIDO
        //=====================================================
        public async Task CrearPedido(List<DetallePedido> detalles)
        {
            using var transaccion =await _context.Database.BeginTransactionAsync();
            try
            {
                Pedido pedido = new Pedido();
                pedido.Fecha = DateTime.Now;
                pedido.Estado = "Pendiente";
                pedido.Total = 0;
                pedido.IdUsuario = 1; // Usuario por defecto
                pedido.IdMesa = 1; // Mesa por defecto
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();
                decimal total = 0;
                foreach (var detalle in detalles)
                {
                    detalle.IdPedido = pedido.IdPedido;
                    detalle.Subtotal =
                        detalle.Cantidad *
                        detalle.PrecioUnitario;
                    total += detalle.Subtotal;
                    _context.DetallePedidos.Add(detalle);
                }
                await _context.SaveChangesAsync();
                pedido.Total = total;
                await _context.SaveChangesAsync();
                await transaccion.CommitAsync();
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }
        //=====================================================
        // DETALLE PEDIDO
        //=====================================================
        public async Task<List<DetallePedido>> ObtenerDetallePedido(int idPedido)
        {
            return await _context.DetallePedidos
                .Where(d => d.IdPedido == idPedido)
                .Include(d => d.IdProductoNavigation)
                .OrderBy(d => d.IdProductoNavigation.Nombre)
                .ToListAsync();
        }
        //=====================================================
        // AGREGAR PRODUCTO AL PEDIDO TEMPORAL
        //=====================================================
        public void AgregarProductoTemporal(
            List<DetallePedido> pedidoTemporal,
            Producto producto)
        {
            var detalleExistente =
                pedidoTemporal.FirstOrDefault(
                    d => d.IdProducto == producto.IdProducto);

            if (detalleExistente != null)
            {
                detalleExistente.Cantidad++;

                detalleExistente.Subtotal =
                    detalleExistente.Cantidad *
                    detalleExistente.PrecioUnitario;

                return;
            }
            pedidoTemporal.Add(new DetallePedido
            {
                IdProducto = producto.IdProducto,

                IdProductoNavigation = producto,

                Cantidad = 1,

                PrecioUnitario = producto.Precio,

                Subtotal = producto.Precio
            });
        }
        //=====================================================
        // QUITAR PRODUCTO DEL PEDIDO TEMPORAL
        //=====================================================
        public void QuitarProductoTemporal(
            List<DetallePedido> pedidoTemporal,
            int idProducto)
        {
            var detalle =
                pedidoTemporal.FirstOrDefault(
                    d => d.IdProducto == idProducto);

            if (detalle == null)
                return;

            detalle.Cantidad--;

            if (detalle.Cantidad <= 0)
            {
                pedidoTemporal.Remove(detalle);
            }
            else
            {
                detalle.Subtotal =
                    detalle.Cantidad *
                    detalle.PrecioUnitario;
            }
        }
        //=====================================================
        // TOTAL
        //=====================================================
        public decimal CalcularTotal(List<DetallePedido> detalles)
        {
            return detalles.Sum(d => d.Subtotal);
        }
        //=====================================================
        // CAMBIAR ESTADO
        //=====================================================
        public async Task CambiarEstadoPedido(
            int idPedido,
            string nuevoEstado)
        {
            var pedido =
                await _context.Pedidos.FindAsync(idPedido);

            if (pedido == null)
                return;
            pedido.Estado = nuevoEstado;

            await _context.SaveChangesAsync();
        }
        //=====================================================
        // ELIMINAR PEDIDO
        //=====================================================
        public async Task EliminarPedido(int idPedido)
        {
            var detalles = await _context.DetallePedidos
                .Where(d => d.IdPedido == idPedido)
                .ToListAsync();

            _context.DetallePedidos.RemoveRange(detalles);

            var pedido = await _context.Pedidos
                .FindAsync(idPedido);

            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
        }
    }
}