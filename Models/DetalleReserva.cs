using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("detalle_reserva")]
[Index("IdMesa", Name = "idx_detalle_reserva_mesa")]
[Index("IdReserva", Name = "idx_detalle_reserva_reserva")]
[Index("IdReserva", "IdMesa", Name = "uq_reserva_mesa", IsUnique = true)]
public partial class DetalleReserva
{
    [Key]
    [Column("id_detalle_reserva")]
    public int IdDetalleReserva { get; set; }

    [Column("id_reserva")]
    public int IdReserva { get; set; }

    [Column("id_mesa")]
    public int IdMesa { get; set; }

    [ForeignKey("IdMesa")]
    [InverseProperty("DetalleReservas")]
    public virtual Mesa IdMesaNavigation { get; set; } = null!;

    [ForeignKey("IdReserva")]
    [InverseProperty("DetalleReservas")]
    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
