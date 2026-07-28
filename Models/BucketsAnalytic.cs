using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("buckets_analytics", Schema = "storage")]
public partial class BucketsAnalytic
{
    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("format")]
    public string Format { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
}
