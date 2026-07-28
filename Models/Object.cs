using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("objects", Schema = "storage")]
[Index("BucketId", "Name", Name = "bucketid_objname", IsUnique = true)]
public partial class Object
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("bucket_id")]
    public string? BucketId { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Field is deprecated, use owner_id instead
    /// </summary>
    [Column("owner")]
    public Guid? Owner { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("last_accessed_at")]
    public DateTime? LastAccessedAt { get; set; }

    [Column("metadata", TypeName = "jsonb")]
    public string? Metadata { get; set; }

    [Column("path_tokens")]
    public List<string>? PathTokens { get; set; }

    [Column("version")]
    public string? Version { get; set; }

    [Column("owner_id")]
    public string? OwnerId { get; set; }

    [Column("user_metadata", TypeName = "jsonb")]
    public string? UserMetadata { get; set; }

    [ForeignKey("BucketId")]
    [InverseProperty("Objects")]
    public virtual Bucket? Bucket { get; set; }
}
