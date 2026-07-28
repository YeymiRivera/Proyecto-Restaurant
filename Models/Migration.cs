using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("migrations", Schema = "storage")]
[Index("Name", Name = "migrations_name_key", IsUnique = true)]
public partial class Migration
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("hash")]
    [StringLength(40)]
    public string Hash { get; set; } = null!;

    [Column("executed_at", TypeName = "timestamp without time zone")]
    public DateTime? ExecutedAt { get; set; }
}
