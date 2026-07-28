using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("webauthn_credentials", Schema = "auth")]
[Index("CredentialId", Name = "webauthn_credentials_credential_id_key", IsUnique = true)]
[Index("UserId", Name = "webauthn_credentials_user_id_idx")]
public partial class WebauthnCredential
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("credential_id")]
    public byte[] CredentialId { get; set; } = null!;

    [Column("public_key")]
    public byte[] PublicKey { get; set; } = null!;

    [Column("attestation_type")]
    public string AttestationType { get; set; } = null!;

    [Column("aaguid")]
    public Guid? Aaguid { get; set; }

    [Column("sign_count")]
    public long SignCount { get; set; }

    [Column("transports", TypeName = "jsonb")]
    public string Transports { get; set; } = null!;

    [Column("backup_eligible")]
    public bool BackupEligible { get; set; }

    [Column("backed_up")]
    public bool BackedUp { get; set; }

    [Column("friendly_name")]
    public string FriendlyName { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("last_used_at")]
    public DateTime? LastUsedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("WebauthnCredentials")]
    public virtual User User { get; set; } = null!;
}
