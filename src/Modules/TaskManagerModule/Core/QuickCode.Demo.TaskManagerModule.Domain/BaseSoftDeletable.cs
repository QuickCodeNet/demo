using System;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.TaskManagerModule.Domain;

public class BaseSoftDeletable : ISoftDeletable
{
    [Column("IsDeleted")]
    public bool IsDeleted { get; set; }
    
    [Column("DeletedOnUtc")]
    public DateTime? DeletedOnUtc { get; set; }
}