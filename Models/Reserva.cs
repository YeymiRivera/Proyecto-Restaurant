using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("reserva")]
[Index("IdUsuario", Name = "idx_reserva_usuario")]
public partial class Reserva
{
    [Key]
    [Column("id_reserva")]
    public int IdReserva { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("hora")]
    public TimeOnly Hora { get; set; }

    [Column("cantidad_personas")]
    public int CantidadPersonas { get; set; }

    [Column("estado")]
    [StringLength(20)]
    public string Estado { get; set; } = null!;

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [InverseProperty("IdReservaNavigation")]
    public virtual ICollection<DetalleReserva> DetalleReservas { get; set; } = new List<DetalleReserva>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("Reservas")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
