using Microsoft.EntityFrameworkCore;
using proyecto.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims; 

namespace proyecto.Services
{
    public class PedidoService
    {
        private readonly RhdbContext _context;
         private readonly AuthenticationStateProvider _auth;
        public PedidoService(RhdbContext context, AuthenticationStateProvider auth)
        {
            _context = context;
            _auth = auth;
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
        // Clientes
        //=====================================================
        public async Task<List<Usuario>> ObtenerClientes()
{
    return await _context.Usuarios
        .Where(u => u.IdRol == 2)
        .OrderBy(u => u.Nombre)
        .ToListAsync();
}
        //=====================================================
        // CREAR PEDIDO
        //=====================================================
        public async Task CrearPedido(List<DetallePedido> detalles,int idMesa,int idCliente)
        {
            var authState = await _auth.GetAuthenticationStateAsync();

            var user = authState.User;
            using var transaccion =await _context.Database.BeginTransactionAsync();
            try
            {
                Pedido pedido = new Pedido();
                pedido.Fecha = DateTime.Now;
                pedido.Estado = "Pendiente";
                pedido.Total = 0;
                pedido.IdUsuario = idCliente;              
                pedido.IdMesa = idMesa;
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
// MESAS DE RESERVA EN CURSO
//=====================================================
public async Task<List<Mesa>> ObtenerMesasReservadasCliente(int idCliente)
{
    return await _context.DetalleReservas
        .Where(r =>
            r.IdReservaNavigation.IdUsuario == idCliente &&
            r.IdReservaNavigation.Estado == "En Curso")
        .Select(r => r.IdMesaNavigation)
        .Distinct()
        .OrderBy(m => m.NumeroMesa)
        .ToListAsync();
}
        public async Task<bool> ClienteTieneReservaEnCurso(int idCliente)
{
    return await _context.DetalleReservas
        .AnyAsync(r =>
            r.IdReservaNavigation.IdUsuario == idCliente &&
            r.IdReservaNavigation.Estado == "En Curso");
}
        public async Task<List<Mesa>> ObtenerMesasDelCliente(int idCliente)
        {
        return await _context.Pedidos
        .Where(p =>
            p.IdUsuario == idCliente &&
            p.Estado != "Pagado" &&
            p.Estado != "Cancelado")
        .Select(p => p.IdMesaNavigation!)
        .Distinct()
        .OrderBy(m => m.NumeroMesa)
        .ToListAsync();
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