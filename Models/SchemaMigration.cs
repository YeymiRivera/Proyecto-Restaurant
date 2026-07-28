using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// Auth: Manages updates to the auth system.
/// </summary>
[Table("schema_migrations", Schema = "auth")]
public partial class SchemaMigration
{
    [Key]
    [Column("version")]
    [StringLength(255)]
    public string Version { get; set; } = null!;
}
