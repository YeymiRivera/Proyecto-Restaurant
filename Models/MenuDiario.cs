using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("menu_diario")]
[Index("MenuDia", Name = "menu_diario_menu_dia_key", IsUnique = true)]
public partial class MenuDiario
{
    [Key]
    [Column("id_menu_diario")]
    public int IdMenuDiario { get; set; }

    [Column("menu_dia")]
    [StringLength(20)]
    public string MenuDia { get; set; } = null!;

    [InverseProperty("IdMenuDiarioNavigation")]
    public virtual ICollection<DetalleMenuDiario> DetalleMenuDiarios { get; set; } = new List<DetalleMenuDiario>();
}
