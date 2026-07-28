using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// auth: stores authenticator method reference claims for multi factor authentication
/// </summary>
[Table("mfa_amr_claims", Schema = "auth")]
[Index("SessionId", "AuthenticationMethod", Name = "mfa_amr_claims_session_id_authentication_method_pkey", IsUnique = true)]
public partial class MfaAmrClaim
{
    [Column("session_id")]
    public Guid SessionId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("authentication_method")]
    public string AuthenticationMethod { get; set; } = null!;

    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [ForeignKey("SessionId")]
    [InverseProperty("MfaAmrClaims")]
    public virtual Session Session { get; set; } = null!;
}
