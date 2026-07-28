using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("custom_oauth_providers", Schema = "auth")]
[Index("CreatedAt", Name = "custom_oauth_providers_created_at_idx")]
[Index("Enabled", Name = "custom_oauth_providers_enabled_idx")]
[Index("Identifier", Name = "custom_oauth_providers_identifier_idx")]
[Index("Identifier", Name = "custom_oauth_providers_identifier_key", IsUnique = true)]
[Index("ProviderType", Name = "custom_oauth_providers_provider_type_idx")]
public partial class CustomOauthProvider
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("provider_type")]
    public string ProviderType { get; set; } = null!;

    [Column("identifier")]
    public string Identifier { get; set; } = null!;

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("client_id")]
    public string ClientId { get; set; } = null!;

    [Column("client_secret")]
    public string ClientSecret { get; set; } = null!;

    [Column("acceptable_client_ids")]
    public List<string> AcceptableClientIds { get; set; } = null!;

    [Column("scopes")]
    public List<string> Scopes { get; set; } = null!;

    [Column("pkce_enabled")]
    public bool PkceEnabled { get; set; }

    [Column("attribute_mapping", TypeName = "jsonb")]
    public string AttributeMapping { get; set; } = null!;

    [Column("authorization_params", TypeName = "jsonb")]
    public string AuthorizationParams { get; set; } = null!;

    [Column("enabled")]
    public bool Enabled { get; set; }

    [Column("email_optional")]
    public bool EmailOptional { get; set; }

    [Column("issuer")]
    public string? Issuer { get; set; }

    [Column("discovery_url")]
    public string? DiscoveryUrl { get; set; }

    [Column("skip_nonce_check")]
    public bool SkipNonceCheck { get; set; }

    [Column("cached_discovery", TypeName = "jsonb")]
    public string? CachedDiscovery { get; set; }

    [Column("discovery_cached_at")]
    public DateTime? DiscoveryCachedAt { get; set; }

    [Column("authorization_url")]
    public string? AuthorizationUrl { get; set; }

    [Column("token_url")]
    public string? TokenUrl { get; set; }

    [Column("userinfo_url")]
    public string? UserinfoUrl { get; set; }

    [Column("jwks_uri")]
    public string? JwksUri { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("custom_claims_allowlist")]
    public List<string> CustomClaimsAllowlist { get; set; } = null!;
}
