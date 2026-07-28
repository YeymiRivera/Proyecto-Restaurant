using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("usuario")]
[Index("Email", Name = "usuario_email_key", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("apellido")]
    [StringLength(100)]
    public string Apellido { get; set; } = null!;

    [Column("email")]
    [StringLength(150)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("telefono")]
    [StringLength(20)]
    public string? Telefono { get; set; }

    [Column("estado")]
    public bool Estado { get; set; }

    [Column("id_rol")]
    public int? IdRol { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    [ForeignKey("IdRol")]
    [InverseProperty("Usuarios")]
    public virtual Rol? IdRolNavigation { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
