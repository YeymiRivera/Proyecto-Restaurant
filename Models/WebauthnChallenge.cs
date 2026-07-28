using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("webauthn_challenges", Schema = "auth")]
[Index("ExpiresAt", Name = "webauthn_challenges_expires_at_idx")]
[Index("UserId", Name = "webauthn_challenges_user_id_idx")]
public partial class WebauthnChallenge
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid? UserId { get; set; }

    [Column("challenge_type")]
    public string ChallengeType { get; set; } = null!;

    [Column("session_data", TypeName = "jsonb")]
    public string SessionData { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("WebauthnChallenges")]
    public virtual User? User { get; set; }
}
