namespace Ordering.Domain.Entities
{
    /// <summary>
    /// Implements audit log fields per entity
    /// </summary>
    public interface IAuditableEntity : IEntity
    {
        int CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        int? LastModifiedBy { get; set; }
        DateTime? LastModifiedAt { get; set; }
    }
}
