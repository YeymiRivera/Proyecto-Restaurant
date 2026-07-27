using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("delivery")]
[Index("IdPedido", Name = "delivery_id_pedido_key", IsUnique = true)]
public partial class Delivery
{
    [Key]
    [Column("id_delivery")]
    public int IdDelivery { get; set; }

    [Column("direccion_entrega")]
    [StringLength(250)]
    public string DireccionEntrega { get; set; } = null!;

    [Column("referencia")]
    [StringLength(250)]
    public string? Referencia { get; set; }

    [Column("costo_envio")]
    [Precision(10, 2)]
    public decimal CostoEnvio { get; set; }

    [Column("estado_entrega")]
    [StringLength(20)]
    public string EstadoEntrega { get; set; } = null!;

    [Column("hora_salida", TypeName = "timestamp without time zone")]
    public DateTime? HoraSalida { get; set; }

    [Column("hora_entrega", TypeName = "timestamp without time zone")]
    public DateTime? HoraEntrega { get; set; }

    [Column("id_pedido")]
    public int IdPedido { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [ForeignKey("IdPedido")]
    [InverseProperty("Delivery")]
    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Deliveries")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
