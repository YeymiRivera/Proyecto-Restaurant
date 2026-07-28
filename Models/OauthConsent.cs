using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("oauth_consents", Schema = "auth")]
[Index("UserId", "ClientId", Name = "oauth_consents_user_client_unique", IsUnique = true)]
[Index("UserId", "GrantedAt", Name = "oauth_consents_user_order_idx", IsDescending = new[] { false, true })]
public partial class OauthConsent
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("client_id")]
    public Guid ClientId { get; set; }

    [Column("scopes")]
    public string Scopes { get; set; } = null!;

    [Column("granted_at")]
    public DateTime GrantedAt { get; set; }

    [Column("revoked_at")]
    public DateTime? RevokedAt { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("OauthConsents")]
    public virtual OauthClient Client { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("OauthConsents")]
    public virtual User User { get; set; } = null!;
}
