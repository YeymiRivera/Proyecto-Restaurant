using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("schema_migrations", Schema = "realtime")]
public partial class SchemaMigration1
{
    [Key]
    [Column("version")]
    public long Version { get; set; }

    [Column("inserted_at", TypeName = "timestamp(0) without time zone")]
    public DateTime? InsertedAt { get; set; }
}
