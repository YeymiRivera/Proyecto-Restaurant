using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// Auth: Audit trail for user actions.
/// </summary>
[Table("audit_log_entries", Schema = "auth")]
[Index("InstanceId", Name = "audit_logs_instance_id_idx")]
public partial class AuditLogEntry
{
    [Column("instance_id")]
    public Guid? InstanceId { get; set; }

    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("payload", TypeName = "json")]
    public string? Payload { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("ip_address")]
    [StringLength(64)]
    public string IpAddress { get; set; } = null!;
}
