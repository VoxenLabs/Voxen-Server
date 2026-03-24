namespace Voxen.Server.Audits.Models;

/// <summary>
/// Represents a change made to an entity during an audit.
/// </summary>
public class AuditChange
{
    /// <summary>
    /// Gets or sets the name of the property that was changed during the audit.
    /// </summary>
    public string PropertyName { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the previous value of the property before the change was made.
    /// </summary>
    public string? OldValue { get; set; }
    
    /// <summary>
    /// Gets or sets the new value of the property after the change.
    /// </summary>
    public string NewValue { get; set; } = null!;
}
