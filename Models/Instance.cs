using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// Auth: Manages users across multiple sites.
/// </summary>
[Table("instances", Schema = "auth")]
public partial class Instance
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("uuid")]
    public Guid? Uuid { get; set; }

    [Column("raw_base_config")]
    public string? RawBaseConfig { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
