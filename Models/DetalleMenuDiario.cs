using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("detalle_menu_diario")]
[Index("IdMenuDiario", "IdProducto", Name = "uq_menu_producto", IsUnique = true)]
public partial class DetalleMenuDiario
{
    [Key]
    [Column("id_detalle_menu")]
    public int IdDetalleMenu { get; set; }

    [Column("id_menu_diario")]
    public int IdMenuDiario { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [ForeignKey("IdMenuDiario")]
    [InverseProperty("DetalleMenuDiarios")]
    public virtual MenuDiario IdMenuDiarioNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("DetalleMenuDiarios")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
