using Voxen.Server.Audits.Models;
using Voxen.Server.Domain.Entities;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Audits.Interfaces;

/// <summary>
/// Defines the contract for an audit log service that handles logging of audit entries.
/// </summary>
public interface IAuditLogService
{
    /// <summary>
    /// Logs an audit entry to the database.
    /// </summary>
    /// <param name="actor">The user who performed the action.</param>
    /// <param name="action">The type of action being logged.</param>
    /// <param name="category">The category of the action being logged.</param>
    /// <param name="entityId">The unique identifier of the entity associated with the audit log.</param>
    /// <param name="changes">A collection of changes made to the entity, represented as <see cref="AuditChange"/> objects.</param>
    /// <param name="ct">A cancellation token used to cancel the asynchronous action if needed.</param>
    public Task LogAsync(User actor, AuditAction action, AuditCategory category, Guid entityId, IEnumerable<AuditChange> changes, CancellationToken ct = default);
}
