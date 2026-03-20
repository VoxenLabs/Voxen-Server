namespace Voxen.Server.Audits.Models;

public class AuditChange
{
    public string PropertyName { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
}
