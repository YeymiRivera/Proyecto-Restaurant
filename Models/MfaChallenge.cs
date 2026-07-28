using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// auth: stores metadata about challenge requests made
/// </summary>
[Table("mfa_challenges", Schema = "auth")]
[Index("CreatedAt", Name = "mfa_challenge_created_at_idx", AllDescending = true)]
public partial class MfaChallenge
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("factor_id")]
    public Guid FactorId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("verified_at")]
    public DateTime? VerifiedAt { get; set; }

    [Column("ip_address")]
    public IPAddress IpAddress { get; set; } = null!;

    [Column("otp_code")]
    public string? OtpCode { get; set; }

    [Column("web_authn_session_data", TypeName = "jsonb")]
    public string? WebAuthnSessionData { get; set; }

    [ForeignKey("FactorId")]
    [InverseProperty("MfaChallenges")]
    public virtual MfaFactor Factor { get; set; } = null!;
}
