using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// Auth: Manages SSO email address domain mapping to an SSO Identity Provider.
/// </summary>
[Table("sso_domains", Schema = "auth")]
[Index("SsoProviderId", Name = "sso_domains_sso_provider_id_idx")]
public partial class SsoDomain
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("sso_provider_id")]
    public Guid SsoProviderId { get; set; }

    [Column("domain")]
    public string Domain { get; set; } = null!;

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("SsoProviderId")]
    [InverseProperty("SsoDomains")]
    public virtual SsoProvider SsoProvider { get; set; } = null!;
}
