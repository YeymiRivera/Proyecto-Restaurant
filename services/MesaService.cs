using Microsoft.EntityFrameworkCore;
using proyecto.Models;

namespace proyecto.Services
{
    public class MesaService
    {
        private readonly RhdbContext _context;

        public MesaService(RhdbContext context)
        {
            _context = context;
        }

        //==========================================
        // CONSULTAS
        //==========================================

        public async Task<List<Mesa>> ObtenerMesas()
        {
            return await _context.Mesas
                .OrderBy(m => m.NumeroMesa)
                .ToListAsync();
        }

        public async Task<Mesa?> ObtenerMesaPorId(int id)
        {
            return await _context.Mesas
                .FirstOrDefaultAsync(m => m.IdMesa == id);
        }

        public async Task<Mesa?> ObtenerMesaPorNumero(int numero)
        {
            return await _context.Mesas
                .FirstOrDefaultAsync(m => m.NumeroMesa == numero);
        }

        //==========================================
        // CREAR
        //==========================================

        public async Task<ResultadoOperacion> AgregarMesa(Mesa mesa)
        {
            bool existe = await _context.Mesas
                .AnyAsync(m => m.NumeroMesa == mesa.NumeroMesa);

            if (existe)
            {
                return new ResultadoOperacion { Exito = false, Mensaje = $"La mesa N° {mesa.NumeroMesa} ya existe." };
            }

            _context.Mesas.Add(mesa);

            await _context.SaveChangesAsync();

            return new ResultadoOperacion { Exito = true, Mensaje = "Mesa registrada correctamente." };
        }

        //==========================================
        // MODIFICAR
        //==========================================

        public async Task<ResultadoOperacion> ActualizarMesa(Mesa mesa)
        {
            bool existe = await _context.Mesas.AnyAsync(m =>
                m.NumeroMesa == mesa.NumeroMesa &&
                m.IdMesa != mesa.IdMesa);

            if (existe)
            {
                return new ResultadoOperacion { Exito = false, Mensaje = $"Ya existe la mesa N° {mesa.NumeroMesa}." };
            }

            _context.Mesas.Update(mesa);

            await _context.SaveChangesAsync();

            return new ResultadoOperacion { Exito = true, Mensaje = "Mesa actualizada correctamente." };
        }

        //==========================================
        // CAMBIAR ESTADO
        //==========================================

        public async Task CambiarEstado(int idMesa, string estado)
        {
            var mesa = await _context.Mesas
                .FirstOrDefaultAsync(m => m.IdMesa == idMesa);

            if (mesa == null)
                return;

            mesa.Estado = estado;

            await _context.SaveChangesAsync();
        }

        //==========================================
        // ELIMINAR
        //==========================================

        public async Task<(bool Correcto, string Mensaje)> EliminarMesa(int idMesa)
        {
            var mesa = await _context.Mesas
                .FirstOrDefaultAsync(m => m.IdMesa == idMesa);

            if (mesa == null)
            {
                return (false, "La mesa no existe.");
            }

            bool tienePedidos = await _context.Pedidos
                .AnyAsync(p => p.IdMesa == idMesa);

            if (tienePedidos)
            {
                return (false,
                    "No se puede eliminar la mesa porque tiene pedidos asociados.");
            }

            bool tieneReservas = await _context.DetalleReservas
                .AnyAsync(r => r.IdMesa == idMesa);

            if (tieneReservas)
            {
                return (false,
                    "No se puede eliminar la mesa porque tiene reservas asociadas.");
            }

            _context.Mesas.Remove(mesa);

            await _context.SaveChangesAsync();

            return (true, "Mesa eliminada correctamente.");
        }
    }
}