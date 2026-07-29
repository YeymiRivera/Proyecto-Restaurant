using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("pedido")]
public partial class Pedido
{
    [Key]
    [Column("id_pedido")]
    public int IdPedido { get; set; }

    [Column("fecha", TypeName = "timestamp without time zone")]
    public DateTime Fecha { get; set; }

    [Column("estado")]
    [StringLength(20)]
    public string Estado { get; set; } = null!;

    [Column("total")]
    [Precision(10, 2)]
    public decimal Total { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_mesa")]
    public int? IdMesa { get; set; }

    [InverseProperty("IdPedidoNavigation")]
    public virtual Delivery? Delivery { get; set; }

    [InverseProperty("IdPedidoNavigation")]
    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    [ForeignKey("IdMesa")]
    [InverseProperty("Pedidos")]
    public virtual Mesa? IdMesaNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Pedidos")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
