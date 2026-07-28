using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// Auth: Manages SSO identity provider information; see saml_providers for SAML.
/// </summary>
[Table("sso_providers", Schema = "auth")]
public partial class SsoProvider
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Auth: Uniquely identifies a SSO provider according to a user-chosen resource ID (case insensitive), useful in infrastructure as code.
    /// </summary>
    [Column("resource_id")]
    public string? ResourceId { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("disabled")]
    public bool? Disabled { get; set; }

    [InverseProperty("SsoProvider")]
    public virtual ICollection<SamlProvider> SamlProviders { get; set; } = new List<SamlProvider>();

    [InverseProperty("SsoProvider")]
    public virtual ICollection<SamlRelayState> SamlRelayStates { get; set; } = new List<SamlRelayState>();

    [InverseProperty("SsoProvider")]
    public virtual ICollection<SsoDomain> SsoDomains { get; set; } = new List<SsoDomain>();
}
