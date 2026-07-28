using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Models;

[Table("subscription", Schema = "realtime")]
public partial class Subscription
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("subscription_id")]
    public Guid SubscriptionId { get; set; }

    [Column("claims", TypeName = "jsonb")]
    public string Claims { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("action_filter")]
    public string? ActionFilter { get; set; }

    [Column("selected_columns")]
    public List<string>? SelectedColumns { get; set; }
}
