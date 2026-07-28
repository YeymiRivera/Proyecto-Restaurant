using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// Stores metadata for all OAuth/SSO login flows
/// </summary>
[Table("flow_state", Schema = "auth")]
[Index("CreatedAt", Name = "flow_state_created_at_idx", AllDescending = true)]
[Index("AuthCode", Name = "idx_auth_code")]
[Index("UserId", "AuthenticationMethod", Name = "idx_user_id_auth_method")]
public partial class FlowState
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid? UserId { get; set; }

    [Column("auth_code")]
    public string? AuthCode { get; set; }

    [Column("code_challenge")]
    public string? CodeChallenge { get; set; }

    [Column("provider_type")]
    public string ProviderType { get; set; } = null!;

    [Column("provider_access_token")]
    public string? ProviderAccessToken { get; set; }

    [Column("provider_refresh_token")]
    public string? ProviderRefreshToken { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("authentication_method")]
    public string AuthenticationMethod { get; set; } = null!;

    [Column("auth_code_issued_at")]
    public DateTime? AuthCodeIssuedAt { get; set; }

    [Column("invite_token")]
    public string? InviteToken { get; set; }

    [Column("referrer")]
    public string? Referrer { get; set; }

    [Column("oauth_client_state_id")]
    public Guid? OauthClientStateId { get; set; }

    [Column("linking_target_id")]
    public Guid? LinkingTargetId { get; set; }

    [Column("email_optional")]
    public bool EmailOptional { get; set; }

    [InverseProperty("FlowState")]
    public virtual ICollection<SamlRelayState> SamlRelayStates { get; set; } = new List<SamlRelayState>();
}
