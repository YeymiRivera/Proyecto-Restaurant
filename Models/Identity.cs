using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// Auth: Stores identities associated to a user.
/// </summary>
[Table("identities", Schema = "auth")]
[Index("ProviderId", "Provider", Name = "identities_provider_id_provider_unique", IsUnique = true)]
[Index("UserId", Name = "identities_user_id_idx")]
public partial class Identity
{
    [Column("provider_id")]
    public string ProviderId { get; set; } = null!;

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("identity_data", TypeName = "jsonb")]
    public string IdentityData { get; set; } = null!;

    [Column("provider")]
    public string Provider { get; set; } = null!;

    [Column("last_sign_in_at")]
    public DateTime? LastSignInAt { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Auth: Email is a generated column that references the optional email property in the identity_data
    /// </summary>
    [Column("email")]
    public string? Email { get; set; }

    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Identities")]
    public virtual User User { get; set; } = null!;
}
