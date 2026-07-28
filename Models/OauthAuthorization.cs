using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("oauth_authorizations", Schema = "auth")]
[Index("AuthorizationCode", Name = "oauth_authorizations_authorization_code_key", IsUnique = true)]
[Index("AuthorizationId", Name = "oauth_authorizations_authorization_id_key", IsUnique = true)]
public partial class OauthAuthorization
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("authorization_id")]
    public string AuthorizationId { get; set; } = null!;

    [Column("client_id")]
    public Guid ClientId { get; set; }

    [Column("user_id")]
    public Guid? UserId { get; set; }

    [Column("redirect_uri")]
    public string RedirectUri { get; set; } = null!;

    [Column("scope")]
    public string Scope { get; set; } = null!;

    [Column("state")]
    public string? State { get; set; }

    [Column("resource")]
    public string? Resource { get; set; }

    [Column("code_challenge")]
    public string? CodeChallenge { get; set; }

    [Column("authorization_code")]
    public string? AuthorizationCode { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [Column("approved_at")]
    public DateTime? ApprovedAt { get; set; }

    [Column("nonce")]
    public string? Nonce { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("OauthAuthorizations")]
    public virtual OauthClient Client { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("OauthAuthorizations")]
    public virtual User? User { get; set; }
}
