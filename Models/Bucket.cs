using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("buckets", Schema = "storage")]
[Index("Name", Name = "bname", IsUnique = true)]
public partial class Bucket
{
    [Key]
    [Column("id")]
    public string Id { get; set; } = null!;

    [Column("name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Field is deprecated, use owner_id instead
    /// </summary>
    [Column("owner")]
    public Guid? Owner { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("public")]
    public bool? Public { get; set; }

    [Column("avif_autodetection")]
    public bool? AvifAutodetection { get; set; }

    [Column("file_size_limit")]
    public long? FileSizeLimit { get; set; }

    [Column("allowed_mime_types")]
    public List<string>? AllowedMimeTypes { get; set; }

    [Column("owner_id")]
    public string? OwnerId { get; set; }

    [InverseProperty("Bucket")]
    public virtual ICollection<Object> Objects { get; set; } = new List<Object>();

    [InverseProperty("Bucket")]
    public virtual ICollection<S3MultipartUpload> S3MultipartUploads { get; set; } = new List<S3MultipartUpload>();

    [InverseProperty("Bucket")]
    public virtual ICollection<S3MultipartUploadsPart> S3MultipartUploadsParts { get; set; } = new List<S3MultipartUploadsPart>();
}
