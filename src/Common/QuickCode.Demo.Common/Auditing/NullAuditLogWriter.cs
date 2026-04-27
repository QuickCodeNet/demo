using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QuickCode.Demo.Common.Auditing;

/// <summary>
/// Discard-only implementation for hosts that use <c>UseSecurityAudit()</c> but have no application database (Portal, Gateway).
/// </summary>
public sealed class NullAuditLogWriter : IAuditLogWriter
{
    public Task QueueAuditLogAsync(AuditLog auditLog, CancellationToken cancellationToken = default) =>
        Task.CompletedTask;

    public Task QueueAuditLogsAsync(IEnumerable<AuditLog> auditLogs, CancellationToken cancellationToken = default) =>
        Task.CompletedTask;
}
