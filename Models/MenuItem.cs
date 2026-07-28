using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("menu_item")]
public partial class MenuItem
{
    [Key]
    [Column("id_menu")]
    public int IdMenu { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("ruta")]
    [StringLength(150)]
    public string Ruta { get; set; } = null!;

    [ForeignKey("IdMenu")]
    [InverseProperty("IdMenus")]
    public virtual ICollection<Rol> IdRols { get; set; } = new List<Rol>();
}
