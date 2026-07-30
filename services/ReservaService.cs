using Microsoft.EntityFrameworkCore;
using proyecto.Models;

namespace proyecto.Services
{
    public class ReservaService
    {
        private readonly RhdbContext _context;

        public ReservaService(RhdbContext context)
        {
            _context = context;
        }

        //==========================================
        // CONSULTAS
        //==========================================

        public async Task<List<Reserva>> ObtenerReservas()
        {
                    return await _context.Reservas
            .Include(r => r.IdUsuarioNavigation)
            .Include(r => r.DetalleReservas)
                .ThenInclude(d => d.IdMesaNavigation)
            .OrderBy(r => r.Fecha)
            .ThenBy(r => r.Hora)
            .ToListAsync();
            
         }

        public async Task<Reserva?> ObtenerReserva(int id)
        {
            return await _context.Reservas
                .Include(r => r.DetalleReservas)
                .FirstOrDefaultAsync(r => r.IdReserva == id);
        }

        public async Task<List<Usuario>> ObtenerClientes()
        {
            return await _context.Usuarios
                .Where(u => u.IdRol == 2)
                .OrderBy(u => u.Nombre)
                .ToListAsync();
        }

        public async Task<List<Mesa>> ObtenerMesas(DateOnly fecha,int? idReservaActual = null)
        {
            var mesasReservadas = await _context.DetalleReservas.Where(d =>
            d.IdReservaNavigation.Fecha == fecha &&
            d.IdReservaNavigation.Estado != "Cancelada" &&
            d.IdReservaNavigation.Estado != "Finalizada" &&
            (!idReservaActual.HasValue ||
             d.IdReserva != idReservaActual.Value))
        .Select(d => d.IdMesa)
        .ToListAsync();

    return await _context.Mesas
        .Where(m =>
            !mesasReservadas.Contains(m.IdMesa))
        .OrderBy(m => m.NumeroMesa)
        .ToListAsync();
}
        //==========================================
        // VALIDAR DISPONIBILIDAD
        //==========================================

        
        //==========================================
        // CREAR
        //==========================================

            public async Task<ResultadoOperacion> CrearReserva(Reserva reserva,int idMesa)
        {
            var mesa = await _context.Mesas
                .FirstOrDefaultAsync(m => m.IdMesa == idMesa);

            if (mesa == null)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = "Mesa no encontrada."
                    };
            }
            bool mesaReservada = await _context.DetalleReservas.AnyAsync(d =>
            d.IdMesa == idMesa &&
            d.IdReservaNavigation.Fecha == reserva.Fecha &&
            d.IdReservaNavigation.Estado != "Cancelada" &&
            d.IdReservaNavigation.Estado != "Finalizada");
            if (mesaReservada)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = "La mesa ya está reservada para esa fecha."
                };
            }
            reserva.Estado = "Pendiente";
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            var detalle = new DetalleReserva
            {
                IdReserva = reserva.IdReserva,
                IdMesa = idMesa
            };
            _context.DetalleReservas.Add(detalle);
            mesa.Estado = "Reservada";
            await _context.SaveChangesAsync();
            return new ResultadoOperacion
            {
                Exito = true,
                Mensaje = "Reserva registrada correctamente."
            };
        }
        //==========================================
        // ACTUALIZAR
        //==========================================
        public async Task<ResultadoOperacion> ActualizarReserva(Reserva reserva,int idMesa)
        {
            var mesaNueva = await _context.Mesas.FirstOrDefaultAsync(m => m.IdMesa == idMesa);

            if (mesaNueva == null)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = "Mesa no encontrada."
                };
            }
            var reservaDb = await _context.Reservas
                .Include(r => r.DetalleReservas)
                .FirstOrDefaultAsync(r =>
                    r.IdReserva == reserva.IdReserva);
            if (reservaDb?.Estado != "Pendiente")
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = "Solo se pueden modificar reservas que estén pendientes."
                    };
            }
            var detalle =reservaDb.DetalleReservas.FirstOrDefault();
            int? idMesaAnterior = detalle?.IdMesa;
            bool mesaOcupada = await _context.DetalleReservas.AnyAsync(d =>
            d.IdMesa == idMesa &&
            d.IdReservaNavigation.Fecha == reserva.Fecha &&
            d.IdReserva != reserva.IdReserva &&
            d.IdReservaNavigation.Estado != "Cancelada" &&
            d.IdReservaNavigation.Estado != "Finalizada");
            if (mesaOcupada)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = "La mesa ya está reservada para esa fecha."
                };
            }
            if (detalle != null)
            {
                detalle.IdMesa = idMesa;
            }
            if (idMesaAnterior.HasValue &&idMesaAnterior.Value != idMesa)
            {
                var mesaAnterior = await _context.Mesas.FirstOrDefaultAsync(m =>m.IdMesa == idMesaAnterior.Value);
                if (mesaAnterior != null)
                {
                    mesaAnterior.Estado = "Disponible";
                }
            }
            mesaNueva.Estado = "Reservada";
            await _context.SaveChangesAsync();

            return new ResultadoOperacion
            {
                Exito = true,
                Mensaje = "Reserva actualizada correctamente."
            };
        }

        //==========================================
        // CANCELAR
        //==========================================

       public async Task<ResultadoOperacion> CancelarReserva(int idReserva)
       {
        var reserva = await _context.Reservas
        .Include(r => r.DetalleReservas)
        .FirstOrDefaultAsync(r =>
            r.IdReserva == idReserva);
        if (reserva == null)
        {
        return new ResultadoOperacion
        {
            Exito = false,
            Mensaje = "Reserva no encontrada."
        };
        }
        if (reserva.Estado != "Pendiente")
    {
        return new ResultadoOperacion
        {
            Exito = false,
            Mensaje = "Solo se pueden cancelar reservas pendientes."
        };
    }

    reserva.Estado = "Cancelada";

    foreach (var detalle in reserva.DetalleReservas)
    {
        var mesa = await _context.Mesas
            .FirstOrDefaultAsync(m =>
                m.IdMesa == detalle.IdMesa);

        if (mesa != null)
        {
            mesa.Estado = "Disponible";
        }
    }

    await _context.SaveChangesAsync();

    return new ResultadoOperacion
    {
        Exito = true,
        Mensaje = "Reserva cancelada correctamente."
    };
}
        //==========================================
        // CAMBIAR ESTADO
        //==========================================

       public async Task<ResultadoOperacion> CambiarEstado(int idReserva,string nuevoEstado)
{
    var reserva = await _context.Reservas
        .Include(r => r.DetalleReservas)
        .FirstOrDefaultAsync(r =>
            r.IdReserva == idReserva);

    if (reserva == null)
    {
        return new ResultadoOperacion
        {
            Exito = false,
            Mensaje = "Reserva no encontrada."
        };
    }

    // ==========================================
    // VALIDAR TRANSICIÓN DE ESTADO
    // ==========================================

    bool transicionValida = false;

    if (reserva.Estado == "Pendiente")
    {
        if (nuevoEstado == "En Curso" ||
            nuevoEstado == "Cancelada")
        {
            transicionValida = true;
        }
    }
    else if (reserva.Estado == "En Curso")
    {
        if (nuevoEstado == "Finalizada")
        {
            transicionValida = true;
        }
    }

    if (!transicionValida)
    {
        return new ResultadoOperacion
        {
            Exito = false,
            Mensaje =
                $"No se puede cambiar una reserva de '{reserva.Estado}' a '{nuevoEstado}'."
        };
    }

    // ==========================================
    // CAMBIAR ESTADO
    // ==========================================

    reserva.Estado = nuevoEstado;

    // ==========================================
    // ACTUALIZAR MESAS
    // ==========================================

    foreach (var detalle in reserva.DetalleReservas)
    {
        var mesa = await _context.Mesas
            .FirstOrDefaultAsync(m =>
                m.IdMesa == detalle.IdMesa);

        if (mesa == null)
            continue;

        if (nuevoEstado == "En Curso")
        {
            mesa.Estado = "Ocupada";
        }
        else if (nuevoEstado == "Finalizada" ||
                 nuevoEstado == "Cancelada")
        {
            mesa.Estado = "Disponible";
        }
    }

    await _context.SaveChangesAsync();

    return new ResultadoOperacion
    {
        Exito = true,
        Mensaje = $"La reserva ahora está '{nuevoEstado}'."
    };
}
        //==========================================
        // CLIENTE
        //==========================================
        public async Task<List<Reserva>>ObtenerReservasCliente(int idCliente)
        {
            return await _context.Reservas
                .Include(r => r.DetalleReservas)
                    .ThenInclude(d => d.IdMesaNavigation)
                .Where(r => r.IdUsuario == idCliente)
                .OrderByDescending(r => r.Fecha)
                .ToListAsync();
        }

        public async Task VerificarReservasVencidas()
            {
                var reservas = await _context.Reservas
                .Include(r => r.DetalleReservas)
                .Where(r => r.Estado == "Pendiente")
                .ToListAsync();
                foreach (var reserva in reservas)
                {
                    DateTime fechaHoraReserva =
                        reserva.Fecha.ToDateTime(reserva.Hora);

                    if (DateTime.Now >= fechaHoraReserva.AddMinutes(16))
                    {
                        reserva.Estado = "Cancelada";

                        var detalle = reserva.DetalleReservas
                            .FirstOrDefault();
                        if (detalle != null)
                        {
                            var mesa = await _context.Mesas
                                .FirstOrDefaultAsync(m =>
                                    m.IdMesa == detalle.IdMesa);

                            if (mesa != null)
                            {
                                mesa.Estado = "Disponible";
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
            
        public async Task CorregirEstadosMesas()
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);
            var mesas = await _context.Mesas.ToListAsync();
            foreach (var mesa in mesas)
            {
                mesa.Estado = "Disponible";
            }
            var reservasActivas = await _context.Reservas.Include(r => r.DetalleReservas)
            .Where(r =>r.Fecha == hoy &&
            (r.Estado == "Pendiente" ||
             r.Estado == "En Curso")).ToListAsync();
             foreach (var reserva in reservasActivas)
             {
                foreach (var detalle in reserva.DetalleReservas)
                {
                    var mesa = mesas.FirstOrDefault(m => m.IdMesa == detalle.IdMesa);
                    if (mesa == null)continue;
                    if (reserva.Estado == "En Curso")
                    {
                        mesa.Estado = "Ocupada";
                    }
                    else if (reserva.Estado == "Pendiente")
                    {
                         mesa.Estado = "Reservada";
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}