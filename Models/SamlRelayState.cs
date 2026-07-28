using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

/// <summary>
/// Auth: Contains SAML Relay State information for each Service Provider initiated login.
/// </summary>
[Table("saml_relay_states", Schema = "auth")]
[Index("CreatedAt", Name = "saml_relay_states_created_at_idx", AllDescending = true)]
[Index("ForEmail", Name = "saml_relay_states_for_email_idx")]
[Index("SsoProviderId", Name = "saml_relay_states_sso_provider_id_idx")]
public partial class SamlRelayState
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("sso_provider_id")]
    public Guid SsoProviderId { get; set; }

    [Column("request_id")]
    public string RequestId { get; set; } = null!;

    [Column("for_email")]
    public string? ForEmail { get; set; }

    [Column("redirect_to")]
    public string? RedirectTo { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("flow_state_id")]
    public Guid? FlowStateId { get; set; }

    [ForeignKey("FlowStateId")]
    [InverseProperty("SamlRelayStates")]
    public virtual FlowState? FlowState { get; set; }

    [ForeignKey("SsoProviderId")]
    [InverseProperty("SamlRelayStates")]
    public virtual SsoProvider SsoProvider { get; set; } = null!;
}
