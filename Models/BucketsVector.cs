using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("buckets_vectors", Schema = "storage")]
public partial class BucketsVector
{
    [Key]
    [Column("id")]
    public string Id { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [InverseProperty("Bucket")]
    public virtual ICollection<VectorIndex> VectorIndices { get; set; } = new List<VectorIndex>();
}
