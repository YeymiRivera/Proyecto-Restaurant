using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// auth: stores metadata about factors
/// </summary>
[Table("mfa_factors", Schema = "auth")]
[Index("UserId", "CreatedAt", Name = "factor_id_created_at_idx")]
[Index("LastChallengedAt", Name = "mfa_factors_last_challenged_at_key", IsUnique = true)]
[Index("UserId", Name = "mfa_factors_user_id_idx")]
[Index("UserId", "Phone", Name = "unique_phone_factor_per_user", IsUnique = true)]
public partial class MfaFactor
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("friendly_name")]
    public string? FriendlyName { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("secret")]
    public string? Secret { get; set; }

    [Column("phone")]
    public string? Phone { get; set; }

    [Column("last_challenged_at")]
    public DateTime? LastChallengedAt { get; set; }

    [Column("web_authn_credential", TypeName = "jsonb")]
    public string? WebAuthnCredential { get; set; }

    [Column("web_authn_aaguid")]
    public Guid? WebAuthnAaguid { get; set; }

    /// <summary>
    /// Stores the latest WebAuthn challenge data including attestation/assertion for customer verification
    /// </summary>
    [Column("last_webauthn_challenge_data", TypeName = "jsonb")]
    public string? LastWebauthnChallengeData { get; set; }

    [InverseProperty("Factor")]
    public virtual ICollection<MfaChallenge> MfaChallenges { get; set; } = new List<MfaChallenge>();

    [ForeignKey("UserId")]
    [InverseProperty("MfaFactors")]
    public virtual User User { get; set; } = null!;
}
