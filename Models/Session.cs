using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// Auth: Stores session data associated to a user.
/// </summary>
[Table("sessions", Schema = "auth")]
[Index("NotAfter", Name = "sessions_not_after_idx", AllDescending = true)]
[Index("OauthClientId", Name = "sessions_oauth_client_id_idx")]
[Index("UserId", Name = "sessions_user_id_idx")]
[Index("UserId", "CreatedAt", Name = "user_id_created_at_idx")]
public partial class Session
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("factor_id")]
    public Guid? FactorId { get; set; }

    /// <summary>
    /// Auth: Not after is a nullable column that contains a timestamp after which the session should be regarded as expired.
    /// </summary>
    [Column("not_after")]
    public DateTime? NotAfter { get; set; }

    [Column("refreshed_at", TypeName = "timestamp without time zone")]
    public DateTime? RefreshedAt { get; set; }

    [Column("user_agent")]
    public string? UserAgent { get; set; }

    [Column("ip")]
    public IPAddress? Ip { get; set; }

    [Column("tag")]
    public string? Tag { get; set; }

    [Column("oauth_client_id")]
    public Guid? OauthClientId { get; set; }

    /// <summary>
    /// Holds a HMAC-SHA256 key used to sign refresh tokens for this session.
    /// </summary>
    [Column("refresh_token_hmac_key")]
    public string? RefreshTokenHmacKey { get; set; }

    /// <summary>
    /// Holds the ID (counter) of the last issued refresh token.
    /// </summary>
    [Column("refresh_token_counter")]
    public long? RefreshTokenCounter { get; set; }

    [Column("scopes")]
    public string? Scopes { get; set; }

    [InverseProperty("Session")]
    public virtual ICollection<MfaAmrClaim> MfaAmrClaims { get; set; } = new List<MfaAmrClaim>();

    [ForeignKey("OauthClientId")]
    [InverseProperty("Sessions")]
    public virtual OauthClient? OauthClient { get; set; }

    [InverseProperty("Session")]
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    [ForeignKey("UserId")]
    [InverseProperty("Sessions")]
    public virtual User User { get; set; } = null!;
}
