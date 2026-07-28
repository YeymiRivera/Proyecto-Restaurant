using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("producto")]
[Index("IdCategoria", Name = "idx_producto_categoria")]
public partial class Producto
{
    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("nombre")]
    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [Column("precio")]
    [Precision(10, 2)]
    public decimal Precio { get; set; }

    [Column("estado")]
    public bool Estado { get; set; }

    [Column("id_categoria")]
    public int IdCategoria { get; set; }

    [Column("image")]
    [StringLength(1000)]
    public string? Image { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetalleMenuDiario> DetalleMenuDiarios { get; set; } = new List<DetalleMenuDiario>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    [ForeignKey("IdCategoria")]
    [InverseProperty("Productos")]
    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
}
