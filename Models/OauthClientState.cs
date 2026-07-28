using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// Stores OAuth states for third-party provider authentication flows where Supabase acts as the OAuth client.
/// </summary>
[Table("oauth_client_states", Schema = "auth")]
[Index("CreatedAt", Name = "idx_oauth_client_states_created_at")]
public partial class OauthClientState
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("provider_type")]
    public string ProviderType { get; set; } = null!;

    [Column("code_verifier")]
    public string? CodeVerifier { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
