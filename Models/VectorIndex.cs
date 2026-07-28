using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("vector_indexes", Schema = "storage")]
public partial class VectorIndex
{
    [Key]
    [Column("id")]
    public string Id { get; set; } = null!;

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("bucket_id")]
    public string BucketId { get; set; } = null!;

    [Column("data_type")]
    public string DataType { get; set; } = null!;

    [Column("dimension")]
    public int Dimension { get; set; }

    [Column("distance_metric")]
    public string DistanceMetric { get; set; } = null!;

    [Column("metadata_configuration", TypeName = "jsonb")]
    public string? MetadataConfiguration { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("BucketId")]
    [InverseProperty("VectorIndices")]
    public virtual BucketsVector Bucket { get; set; } = null!;
}
